namespace RconInvolved
{
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using RconInvolved.Windows;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(OnLoadingEnded);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

         // Specify what you want to happen when the Elapsed event is raised.
        private void OnLoadingEnded(object sender, EventArgs e)
         {
             dispatcherTimer.Stop();
             this.loadingBarSplashScreen.IsEnabled = false;
             Console.WriteLine("Hello World!");
             new LoginWindow().Show();
             this.Close();
         }
    }
}
