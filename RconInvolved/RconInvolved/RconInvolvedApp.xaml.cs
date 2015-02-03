using System;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;
using RconInvolved.Communications.Websockets;
using System.Windows;
using System.Deployment.Application;


namespace RconInvolved
{
    /// <summary>
    /// Interaction logic for Application XAML
    /// </summary>
    public partial class RconInvolvedApp
    {

        TestWebsocket testSocket;

        public RconInvolvedApp()
        {
            try
            {
                Console.WriteLine("Launching ");
                SetPath();

                log4net.ThreadContext.Properties["log_who"] = "Rcon_Involved";
                Logger.MonitoringLogger.Info("RconInvolved is starting");
                Configuration.Initialize(this);
                SQLiteDatabase.Initialize();
                testSocket = new TestWebsocket();
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
                System.Environment.Exit(-1);
            }
        }

        protected void AppExiting(object sender, EventArgs e)
        {
            Configuration.SaveConfiguration();
            Logger.MonitoringLogger.Info("RconInvolved is closing, see you !");
        }

        private void SetPath()
        {

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Configuration.CONFIGURATION_FILE_PATH = ApplicationDeployment.CurrentDeployment.DataDirectory + "\\Conf\\";
                SQLiteDatabase.DATABASE_LOCAL_FILE_PATH = "\\Databases\\";
                SQLiteDatabase.DATABASE_FILE_PATH = ApplicationDeployment.CurrentDeployment.DataDirectory + SQLiteDatabase.DATABASE_LOCAL_FILE_PATH;
            }
            else
            {
                Configuration.CONFIGURATION_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "\\Conf\\";
            }

        

        }
    }

}
