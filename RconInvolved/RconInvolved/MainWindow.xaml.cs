namespace RconInvolved
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

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Boolean versionChecked = false;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(OnTimerEnded);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

            if (Configuration.applicationDeployment != null) ChecksForDeployment();
            else versionChecked = true;
        }

        private async void ChecksForDeployment()
        {
            try
            {
                if (Configuration.applicationDeployment.IsFirstRun)
                {
                    await MessageDialog.ShowAsync(
                       "Nouvelle version",
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
                       MessageDialogType.Light);

                    NotifyBox.Show(
                    (DrawingImage)this.FindResource("SearchDrawingImage"),
                    "Nouvelle version",
                    "Vous utilisez une nouvelle version de l'application : " + Configuration.applicationDeployment.CurrentVersion.ToString(),
                    false);

                }
                else
                {
                    versionChecked = true;
                }
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
        }
        private void NoChangelogView()
        {
            Logger.MonitoringLogger.Info("Pas de visionnage du changelog");
            versionChecked = true;
        }

         // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimerEnded(object sender, EventArgs e)
         {
             Logger.MonitoringLogger.Info("Splashscreen timer, checking all parameters");
             //All verification are done?
             if (!versionChecked) return;

             dispatcherTimer.Stop();
             this.loadingBarSplashScreen.IsEnabled = false;
             new LoginWindow().Show();
             this.Close();
         }
    }
}
