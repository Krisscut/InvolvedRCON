using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = "Data/Conf/Log4Net.conf", Watch = true)]
namespace RconInvolved.Utils
{
    class Logger
    {
        public static ILog MonitoringLogger
        {
            get {
                //Console.WriteLine("Get Monitoring Logger");
                return LogManager.GetLogger("MonitoringLogger"); 
            }
        }

        public static ILog ExceptionLogger
        {
            get { return LogManager.GetLogger("ExceptionLogger"); }
        }
    }
}
