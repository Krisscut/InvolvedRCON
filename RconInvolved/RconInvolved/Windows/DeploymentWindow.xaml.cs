using Framework.UI.Controls;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
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
                    this.LabelSizeUpdate.Text = (info.UpdateSizeBytes / 1048576).ToString();
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

        private void ChecksForDeployment()
        {
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
                    //DeploymentWindow deployWindow = new DeploymentWindow(this);
                    //deployWindow.Show();

                    Logger.MonitoringLogger.Debug("Splashscreen window begin to wait the version to be checked");
                    //this.contentLoading.Content = "En attente du système de mise à jour";
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

        private void InstallUpdateSyncWithInfo()
        {
                Boolean doUpdate = true;
                ApplicationDeployment ad = Configuration.applicationDeployment;

                if (!infoUpdate.IsUpdateRequired)
                {
                    Logger.MonitoringLogger.Debug("Update not required");
                    //Here, set data information for a non vital version
                    MessageDialog.ShowAsync(
                    "Nouvelle version",
                    "Version non requise mais installée en attendant une gestion plus poussée : nouvelle version : " + infoUpdate.MinimumRequiredVersion.ToString(),
                    MessageBoxButton.OK,
                    MessageDialogType.Light,
                    this);
                    doUpdate = true;
                }
                else
                {
                    // Display a message that the app MUST reboot. Display the minimum required version.
                    /*MessageBox.Show("This application has detected a mandatory update from your current " +
                        "version to version " + info.MinimumRequiredVersion.ToString() +
                        ". The application will now install the update and restart.",
                        "Update Available", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        * */
                    Logger.MonitoringLogger.Debug("Update required");
                    MessageDialog.ShowAsync(
                    "Nouvelle version requise",
                    "Cette nouvelle version est obligatoire, la mise à jour va se faire automatiquement vers la version " + infoUpdate.MinimumRequiredVersion.ToString(),
                    MessageBoxButton.OK,
                    MessageDialogType.Light,
                    this);
                }

                if (doUpdate)
                {
                    try
                    {
                        Logger.MonitoringLogger.Warn("Application update is starting");
                        ad.Update();
                        MessageDialog.ShowAsync(
                            "Installation terminée",
                            "Installation terminée, redémarrage !",
                            MessageBoxButton.OK,
                            MessageDialogType.Light,
                            this);
                        Logger.MonitoringLogger.Warn("Application update ended, restarting application !");
                        System.Windows.Forms.Application.Restart();
                    }
                    catch (DeploymentDownloadException dde)
                    {
                        MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                        versionChecked = true;
                        return;
                    }
                }
                
                //No update : version is checked !
                versionChecked = true;
                this.Close();
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
    }
}
