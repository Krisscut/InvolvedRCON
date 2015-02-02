namespace RconInvolved.Windows
{
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using RconInvolved.Windows;
    using Framework.UI.Controls;
    using System.Windows.Media;
    using System.Deployment.Application;
    using Framework.UI.Input;
    using RconInvolved.DataPersistance;
    using System.Diagnostics;
    using RconInvolved.Utils;
    using System.Windows.Controls;
    using System.Threading.Tasks;

    /// <summary>
    /// Logique d'interaction pour SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow
    {
        public Boolean versionChecked = false;
        private Boolean changelogChoice = false;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DispatcherTimer changelogTimer = new DispatcherTimer();
        public Label contentLoading = null;

        public SplashScreenWindow()
        {
            Logger.MonitoringLogger.Debug("Splashscreen window Instanciated");
            InitializeComponent();
            this.contentLoading = this.ActualContent;

            dispatcherTimer.Tick += new EventHandler(OnTimerEnded);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            //Set application Look
            //TODO load from configFile
            Configuration.app.Theme = Elysium.Theme.Dark;
            Configuration.app.AccentColor = System.Windows.Media.Colors.DarkOrange;
            Configuration.app.ContrastColor = System.Windows.Media.Colors.LightGray;

            //Show splashscreen window
            this.Show();

            //Checks if we are in debug or downloaded version
            if (Configuration.applicationDeployment != null) AsyncOperations();
            else
            {
                Logger.MonitoringLogger.Debug("Application is launched without applicationDeployment");
                versionChecked = true;
                changelogChoice = true;
            }
        }

        private async void AsyncOperations()
        {
            Logger.MonitoringLogger.Debug("Splashscreen window async operations");
            ChecksForDeployment();
        }

        private void ChecksForDeployment()
        {
            contentLoading.Content = "Vérification de version";
            try
            {
                /* Checks if we need to make a new deployment */

                UpdateCheckInfo info = null;
                try
                {
                    info = Configuration.applicationDeployment.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                }
                Logger.MonitoringLogger.Info("Info retrieved with check For Detailed Update : " + info.UpdateAvailable.ToString());

                //Update is available, launch deployment screen !
                if (info.UpdateAvailable)
                {
                    DeploymentWindow deployWindow = new DeploymentWindow(info, this);
                    deployWindow.Show();

                    Logger.MonitoringLogger.Debug("Splashscreen window begin to wait the version to be checked");
                    this.contentLoading.Content = "En attente du système de mise à jour";
                    changelogTimer.Tick += new EventHandler(OnTimerChangelogEnded);
                    changelogTimer.Interval = new TimeSpan(0, 0, 3);
                    changelogTimer.Start();
                }
                else
                {
                    versionChecked = true;
                    CheckForChangelog();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while getting deployment info");
                Logger.ExceptionLogger.Error(e.ToString());
                throw;
            }
        }

        private void OnTimerChangelogEnded(object sender, EventArgs e)
        {
            Logger.MonitoringLogger.Debug("Splashscreen is waiting for changelog value to be set");
            if (!versionChecked) return;
            CheckForChangelog();
        }

        private async void CheckForChangelog ()
        {
            //First run, ask for the changelog
            if (Configuration.applicationDeployment.IsFirstRun)
            {
                contentLoading.Content = "Affichage changelog ?";
                await MessageDialog.ShowAsync(
                    "Nouvelle version !",
                    "Voulez-vous afficher le changelog de la nouvelle version?",
                    new[] 
                        {
                            new MessageDialogButton()
                            {
                                Command = new DelegateCommand(() => ConfirmChangelogView()),
                                Content = "Oui"
                            },
                            new MessageDialogButton()
                            {
                                Command = new DelegateCommand(() => NoChangelogView()),
                                Content = "Non"
                            }
                        },
                    MessageDialogType.Light,
                    this);
                NotifyBox.Show(
                (DrawingImage)this.FindResource("SearchDrawingImage"),
                "Nouvelle version",
                "Vous utilisez une nouvelle version de l'application : " + Configuration.applicationDeployment.CurrentVersion.ToString(),
                false);
            }
            else    //Second run or more
            {
                changelogChoice = true;
            }
        }

        private void ConfirmChangelogView()
        {
            Process.Start(Configuration.URL_CHANGELOG);
            changelogChoice = true;
        }
        private void NoChangelogView()
        {
            Logger.MonitoringLogger.Info("use have choosen to don't show the changelog.");
            changelogChoice = true;
        }

         // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimerEnded(object sender, EventArgs e)
         {
             Logger.MonitoringLogger.Info("Splashscreen timer, versionChecked :" + versionChecked.ToString() + " - changelogChoice :" + changelogChoice.ToString());
             //All verification are done?
             if (!versionChecked) return;
             Logger.MonitoringLogger.Info("Splashscreen timer, checking if Changelog need to be displayed");
             if (!changelogChoice) return;

             contentLoading.Content = "Lancement du RconInvolved";
             dispatcherTimer.Stop();
             this.loadingBarSplashScreen.IsEnabled = false;
             new LoginWindow().Show();
             this.Close();
         }
    }
}
