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

    /// <summary>
    /// Logique d'interaction pour SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow
    {
        private Boolean versionChecked = false;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Label contentLoading = null;

        public SplashScreenWindow()
        {
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

            this.Show();
            //Checks if we are in debug or downloaded version
            if (Configuration.applicationDeployment != null) ChecksForDeployment();
            else versionChecked = true;
        }

        private async void ChecksForDeployment()
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
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }
                Logger.MonitoringLogger.Info("Info retrieved with check For Detailed Update : " + info.ToString());

                if (info.UpdateAvailable)
                {

                }

                if (Configuration.applicationDeployment.IsFirstRun)
                {
                    contentLoading.Content = "Affichage changelog";
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
                versionChecked = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while getting deployment info");
                Logger.ExceptionLogger.Error(e.ToString());
                throw;
            }
        }
        private void ConfirmChangelogView()
        {
            Process.Start(Configuration.URL_CHANGELOG);
            versionChecked = true;
        }
        private void NoChangelogView()
        {
            Logger.MonitoringLogger.Info("use have choosen to don't show the changelog.");
            versionChecked = true;
        }

         // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimerEnded(object sender, EventArgs e)
         {
             Logger.MonitoringLogger.Info("Splashscreen timer, checking all parameters");
             //All verification are done?
             if (!versionChecked) return;


             contentLoading.Content = "Lancement du RconInvolved";
             dispatcherTimer.Stop();
             this.loadingBarSplashScreen.IsEnabled = false;
             new LoginWindow().Show();
             this.Close();
         }
    }
}
