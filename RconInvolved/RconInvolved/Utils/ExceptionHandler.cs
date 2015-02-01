using Framework.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RconInvolved.Utils
{
    class ExceptionHandler
    {

        public static void HandleException(Exception e , String message = null) 
        {
            if (message != null)
            {
                MessageBox.Show(message + "\n" + e.ToString(), "Critical exception");
                Logger.ExceptionLogger.Fatal("Handle exception :" + e.ToString());
            }
            else
            {
                MessageBox.Show("Critical Exception, here is the stacktrace \n" + e.ToString(), "Critical exception");
                Logger.ExceptionLogger.Fatal("Handle exception :" + e.ToString());
            }
            
        }
    }
}
