using Framework.UI.Controls;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;
using System;
using System.Collections.Generic;
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

        public DeploymentWindow(UpdateCheckInfo info, SplashScreenWindow source)
        {
            Logger.MonitoringLogger.Debug("Deployment window initialized");
            InitializeComponent();
            this.infoUpdate = info;
            this.splashScreen = source;

            //Set up UI with informations ! ( and upgrade TODO )
            InstallUpdateSyncWithInfo();
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
                        splashScreen.versionChecked = true;
                        return;
                    }
                }
                
                //No update : version is checked !
                splashScreen.versionChecked = true;
                this.Close();
        }

    }

}
