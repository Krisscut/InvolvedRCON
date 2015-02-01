using System;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;
using RconInvolved.Communications.Websockets;
using System.Windows;


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
    }

}
