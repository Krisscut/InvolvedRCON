using System;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;
using RconInvolved.Communications.Websockets;


namespace RconInvolved
{
    /// <summary>
    /// Interaction logic for Application XAML
    /// </summary>
    public partial class App
    {
        TestWebsocket testSocket;

        public App()
        {
            log4net.ThreadContext.Properties["log_who"] = "Rcon_Involved";
            Logger.MonitoringLogger.Info("RconInvolved is starting");
            Configuration.Initialize();
            SQLiteDatabase.Initialize();
            testSocket = new TestWebsocket();
        }

        protected void AppExiting(object sender, EventArgs e)
        {
            Logger.MonitoringLogger.Info("RconInvolved is closing, see you !");
        }
    }

}
