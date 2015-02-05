using Framework.UI.Controls;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RconInvolved.Windows
{
    /// <summary>
    /// Logique d'interaction pour DeploymentWindow.xaml
    /// </summary>
    public partial class DeploymentWindow
    {
        private UpdateCheckInfo infoUpdate = null;
        SplashScreenWindow splashScreen = null;
        public Boolean versionChecked = false;
        public Boolean deployEnded = false;
        public Boolean restartNeeded = false;

        BackgroundWorker checkVersionWorker;
        BackgroundWorker updateRconWorker;



        private enum UpdateStatuses
        {
            NoUpdateAvailable,
            UpdateAvailable,
            UpdateRequired,
            NotDeployedViaClickOnce,
            DeploymentDownloadException,
            InvalidDeploymentException,
            InvalidOperationException
        }

        private enum InstallationStatuses
        {
            NoUpdateAvailable,
            UpdateAvailable,
            UpdateRequired,
            NotDeployedViaClickOnce,
            DeploymentDownloadException,
            InvalidDeploymentException,
            InvalidOperationException
        }

        public DeploymentWindow(SplashScreenWindow source)
        {
            Logger.MonitoringLogger.Debug("Deployment window initialized");
            InitializeComponent();
            this.splashScreen = source;

            //Set up UI with informations ! ( and upgrade TODO )
            //InstallUpdateSyncWithInfo();
            checkVersionWorker = new BackgroundWorker();
            checkVersionWorker.WorkerReportsProgress = true;
            checkVersionWorker.DoWork += new DoWorkEventHandler(checkVersionWorker_DoWork);
            checkVersionWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(checkVersionWorker_RunWorkerCompleted);
            checkVersionWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Will be executed once it's complete...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkVersionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch ((UpdateStatuses)e.Result)
            {
                case UpdateStatuses.NoUpdateAvailable:
                    // No update available, do nothing
                    //MessageBox.Show("There's no update, thanks...");
                    Logger.MonitoringLogger.Info("No update available for the application");
                    this.splashScreen.CheckForChangelog(Configuration.applicationDeployment.IsFirstRun);
                    //Checks for changelog !
                    versionChecked = true;
                    deployEnded = true;
                    break;
                case UpdateStatuses.UpdateAvailable:
                    this.Show();
                    UpdateInformationUI(false);
                    Logger.MonitoringLogger.Info("Update detected !");
                    //MessageBox.Show("An update is available. Would you like to update the application now?", "Update available");
                    //if (dialogResult == DialogResult.OK)
                        //UpdateApplication();
                    break;
                case UpdateStatuses.UpdateRequired:
                    this.Show();
                    UpdateInformationUI(true);
                    Logger.MonitoringLogger.Info("Update is required to use the application");
                    //MessageBox.Show("A required update is available, which will be installed now", "Update available");
                    //UpdateApplication();
                    break;
                case UpdateStatuses.NotDeployedViaClickOnce:
                    Logger.MonitoringLogger.Warn("Application not deployed with clickonce");
                    //MessageBox.Show("Is this deployed via ClickOnce?");
                    versionChecked = true;
                    deployEnded = true;
                    this.splashScreen.CheckForChangelog(false);
                    break;
                case UpdateStatuses.DeploymentDownloadException:
                    Logger.MonitoringLogger.Error("Can't check for update");
                    //MessageBox.Show("Whoops, couldn't retrieve info on this app...");
                    versionChecked = true;
                    deployEnded = true;
                    this.splashScreen.CheckForChangelog(false);
                    break;
                case UpdateStatuses.InvalidDeploymentException:
                    Logger.MonitoringLogger.Fatal("Clickonce deployment is corrupt");
                    //MessageBox.Show("Cannot check for a new version. ClickOnce deployment is corrupt!");
                    versionChecked = true;
                    deployEnded = true;
                    this.splashScreen.CheckForChangelog(false);
                    break;
                case UpdateStatuses.InvalidOperationException:
                    Logger.MonitoringLogger.Warn("Not a click once application");
                    //MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application.");
                    versionChecked = true;
                    deployEnded = true;
                    this.splashScreen.CheckForChangelog(false);
                    break;
                default:
                    Logger.MonitoringLogger.Fatal("Default case deployment, this is impossible!");
                    //MessageBox.Show("Huh?");
                    break;
            }
        }

        /// <summary>
        /// Will be executed when works needs to be done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkVersionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateCheckInfo info = null;

            // Check if the application was deployed via ClickOnce.
            if (!ApplicationDeployment.IsNetworkDeployed)
            {
                e.Result = UpdateStatuses.NotDeployedViaClickOnce;
                return;
            }
            ApplicationDeployment updateCheck = Configuration.applicationDeployment;
            try
            {
                info = updateCheck.CheckForDetailedUpdate();
            }
            catch (DeploymentDownloadException dde)
            {
                e.Result = UpdateStatuses.DeploymentDownloadException;
                return;
            }
            catch (InvalidDeploymentException ide)
            {
                e.Result = UpdateStatuses.InvalidDeploymentException;
                return;
            }
            catch (InvalidOperationException ioe)
            {
                e.Result = UpdateStatuses.InvalidOperationException;
                return;
            }

            if (info.UpdateAvailable)
                if (info.IsUpdateRequired)
                    e.Result = UpdateStatuses.UpdateRequired;
                else
                    e.Result = UpdateStatuses.UpdateAvailable;
            else
                e.Result = UpdateStatuses.NoUpdateAvailable;
        }

        private void UpdateInformationUI (Boolean required)
        {
            Logger.MonitoringLogger.Debug("required status" + required);
            try
            {
                UpdateCheckInfo info = Configuration.applicationDeployment.CheckForDetailedUpdate();

                    this.loadingBarSplashScreen.Visibility = System.Windows.Visibility.Collapsed;
                    Logger.MonitoringLogger.Debug("Get info status");
                    this.CheckBoxRequired.IsChecked = required;
                    this.LabelVersion.Text = info.AvailableVersion.ToString();
                    this.LabelSizeUpdate.Text = (info.UpdateSizeBytes / 1048576).ToString();            //bytes to Mo
                    Logger.MonitoringLogger.Debug("update visibility");
                    this.LabelButton.Visibility = System.Windows.Visibility.Visible;

                if (required)
                {
                    try
                    {
                        this.LabelRequiredVersion.Text = info.MinimumRequiredVersion.ToString();
                        this.CancelCommandButton.Visibility = System.Windows.Visibility.Collapsed;
                        this.OkCommandButton.Visibility = System.Windows.Visibility.Visible;
                        this.CloseCommandButton.Visibility = System.Windows.Visibility.Collapsed;
                        this.ActualContent.Content = "Attente action utilisateur";
                        this.LabelButton.Content = "Mise à jour requise";}
                    catch (Exception e)
                    {
                        MessageBox.Show("FATAL ERROR IN UPDATE UI FUNCTION TEXT REQUIRED");
                        Logger.ExceptionLogger.Fatal("Fatal error when updating UI size  REQUIRED! \n" + e.ToString());
                    }
                }
                else
                {
                    try
                    {
                        this.LabelRequiredVersion.Text = "N/A";
                        this.CancelCommandButton.Visibility = System.Windows.Visibility.Visible;
                        this.CloseCommandButton.Visibility = System.Windows.Visibility.Collapsed;
                        this.OkCommandButton.Visibility = System.Windows.Visibility.Visible;
                        this.ActualContent.Content = "Attente action utilisateur";
                        this.LabelButton.Content = "Voulez-vous installer la mise à jour?";}
                    catch (Exception e)
                    {
                        MessageBox.Show("FATAL ERROR IN UPDATE UI FUNCTION TEXT NOT REQUIRED");
                        Logger.ExceptionLogger.Fatal("Fatal error when updating UI NOT REQUIRED ! \n" + e.ToString());
                    }
                }
                //Configuration.applicationDeployment.CurrentDeployment.UpdateLocation;
                //Configuration.applicationDeployment.CurrentVersion;
                //Configuration.applicationDeployment.TimeOfLastUpdateCheck;
            }
            catch (Exception e)
            {
                MessageBox.Show("FATAL ERROR IN UPDATE UI FUNCTION");
                Logger.ExceptionLogger.Fatal("Fatal error when updating UI ! \n" + e.ToString());
            }

        }

        private void CloseCommandButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.deployEnded = true;
            this.restartNeeded = true;
        }

        /**
         *  Cancel the update !
         * */
        private void CancelCommandButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            versionChecked = true;
            this.deployEnded = true;
        }

        private void OkCommandButton_Click(object sender, RoutedEventArgs e)
        {
            versionChecked = true;

            updateRconWorker = new BackgroundWorker();
            updateRconWorker.DoWork += new DoWorkEventHandler(updateRconWorker_DoWork);
            updateRconWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(updateRconWorker_RunWorkerCompleted);
            updateRconWorker.RunWorkerAsync();

            //Update UI !
            this.loadingBarSplashScreen.Visibility = System.Windows.Visibility.Visible;
            this.ActualContent.Content = "Mise à jour en cours !";
            this.LabelButton.Visibility= System.Windows.Visibility.Collapsed;
            this.OkCommandButton.Visibility = System.Windows.Visibility.Collapsed;
            this.CancelCommandButton.Visibility = System.Windows.Visibility.Collapsed;
            this.CloseCommandButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void updateRconWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("The application has been upgraded, and will now restart.");
            //this.Hide();
            this.loadingBarSplashScreen.Visibility = System.Windows.Visibility.Collapsed;
            this.ActualContent.Content = "Mise à jour terminée, l'application va redémarrer";
            this.LabelButton.Content = "Fermer la fenetre pour continuer";
            this.restartNeeded = true;

            this.CancelCommandButton.Visibility = System.Windows.Visibility.Collapsed;
            this.OkCommandButton.Visibility = System.Windows.Visibility.Collapsed;
            this.CloseCommandButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void updateRconWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Configuration.applicationDeployment.Update();
            }
            catch (DeploymentDownloadException dde)
            {
                MessageBox.Show("Cannot install the latest version of the application. nnPlease check your network connection, or try again later. Error: " + dde);
                return;
            }
        }

        private void HUB_changelog_click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://krisscut.legtux.org/applications/RconInvolved/changelog/changelog.html");
        }

        private void Version_changelog_click(object sender, RoutedEventArgs e)
        {
            UpdateCheckInfo info = ApplicationDeployment.CurrentDeployment.CheckForDetailedUpdate();
            Version version = info.AvailableVersion;
            String URL_CHANGELOG = String.Format("http://krisscut.legtux.org/applications/RconInvolved/changelog/content/changelog_{0}_{1}_{2}.html", version.Major, version.Minor, version.Build);
            Process.Start(URL_CHANGELOG);
        }
    }
}
