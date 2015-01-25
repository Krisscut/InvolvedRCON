using System;
using RconInvolved.DataPersistance;

namespace RconInvolved
{
    /// <summary>
    /// Interaction logic for Application XAML
    /// </summary>
    public partial class App
    {
        public Configuration conf;

        public App()
        {
            Console.WriteLine("App is launching");
            conf = new Configuration();
        }

        protected void AppExiting(object sender, EventArgs e)
        {
            Console.WriteLine("App is closing");
        }
    }

}
