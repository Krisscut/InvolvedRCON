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
        
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Label contentLoading = null;
        DeploymentWindow deployWindow = null;
        private Boolean changelogChoice = false;


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

            //deployWindow.Show();
            contentLoading.Content = "Vérification de version";
            deployWindow = new DeploymentWindow(this);
        }

        //Called from deployment window
        public async void CheckForChangelog ( Boolean doDisplay)
        {
            //Checks if we are in debug or downloaded version
            //First run, ask for the changelog
            if (doDisplay)
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
             //All verification are done?
             if (deployWindow == null) return;
             Logger.MonitoringLogger.Info("Splashscreen timer, versionChecked :" + deployWindow.versionChecked.ToString() + " - changelogChoice :" + changelogChoice.ToString());
             if (!deployWindow.versionChecked) return;
             contentLoading.Content = "Attente fin de déploiement";
             Logger.MonitoringLogger.Info("Splashscreen timer, checking if Changelog need to be displayed");

             if (!deployWindow.deployEnded) return;
             //Checks if restart is needed ,and then stop eveything !
             if (deployWindow.restartNeeded)
             {
                 dispatcherTimer.Stop();
                 deployWindow.Close();
                 contentLoading.Content = "Rédémarrage pour mise à jour !";
                 //TO DO, redémarrage ici
                 System.Windows.Forms.Application.Restart();
                 this.Close();
                 System.Environment.Exit(0);        //Destroy this window
             }
             else
             {
                 if (!changelogChoice) return;

                 //Ready to go ! close deployment window as it is not needed anymore
                 deployWindow.Close();

                 contentLoading.Content = "Lancement du RconInvolved";
                 dispatcherTimer.Stop();
                 this.loadingBarSplashScreen.IsEnabled = false;
                 new LoginWindow().Show();
                 this.Close();
             }

         }
    }
}
