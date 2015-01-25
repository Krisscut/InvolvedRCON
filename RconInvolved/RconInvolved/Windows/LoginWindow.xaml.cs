
namespace RconInvolved.Windows
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using RconInvolved.ViewModels;
    using RconInvolved.OverlayViews;
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        //private readonly Collectio servers;
        //private readonly AsyncDelegateCommand pinCommand;

        public LoginWindow()
        {
            InitializeComponent();
            this.Style = (Style)this.FindResource("BackgroundFadeWindowStyle");
            this.DataContext = new ServersDataViewModel();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            Console.WriteLine("Initializing");
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("KIKOO");
        }

        public void NewConnexionClick(object sender, RoutedEventArgs e)
        {
            CreateConnectionOverlayView window = new CreateConnectionOverlayView(this, Window.GetWindow(this));
            this.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.Paused;
            this.TaskbarProgressValue = 50;
            //this.TaskbarIsBusy = true;
            window.Show();
        }

        public void AuthorsClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://krisscut.fr");
        }

        public void LicenseClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.codeplex.com/team/view");
        }

        public void DonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://elysium.codeplex.com/team/view");
        }
    }
}
