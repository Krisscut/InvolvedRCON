using System;
using RconInvolved.DataPersistance;
using RconInvolved.Utils;


namespace RconInvolved
{
    /// <summary>
    /// Interaction logic for Application XAML
    /// </summary>
    public partial class App
    {
        public App()
        {
            log4net.ThreadContext.Properties["log_who"] = "Rcon_Involved";
            Logger.MonitoringLogger.Info("RconInvolved is starting");
            Configuration.Initialize();
            SQLiteDatabase.Initialize();
        }

        protected void AppExiting(object sender, EventArgs e)
        {
            Logger.MonitoringLogger.Info("RconInvolved is closing, see you !");
        }
    }

}
