
namespace RconInvolved.Windows
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using RconInvolved.ViewModels;
    using RconInvolved.OverlayViews;
    using System.Xml.Linq;
    using RconInvolved.DataPersistance;
    using System.ComponentModel;
    using BattleNET;
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private static string WINDOW_NAME = "LoginWindow";
        //private readonly Collectio servers;
        //private readonly AsyncDelegateCommand pinCommand;

        public LoginWindow()
        {
            Console.WriteLine("Initializing LoginWindow");
            InitializeComponent();
            this.Style = (Style)this.FindResource("AccentTitleBarWindowStyle");
            this.DataContext = new ServersDataViewModel();

            //Event handler
            this.Loaded += new RoutedEventHandler(LoadedEventHandler);
            this.Closing += new CancelEventHandler(ClosedEventHandler);

            ResourceDictionary resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri("/Framework.UI;component/Themes/ElysiumExtra/GeometryIcon.xaml", UriKind.RelativeOrAbsolute)
            };

            BattlEyeLoginCredentials loginCredentials = new BattlEyeLoginCredentials();
        }

        #region ConfigurationFile
        private void LoadFromConfigurationFile()
        {
            XElement windowsConf = Configuration.GetRootElementByName(WINDOW_NAME);
            this.Left = Convert.ToDouble(windowsConf.Element("Left").Value);
            this.Top = Convert.ToDouble(windowsConf.Element("Top").Value);
        }

        private void SaveToConfigurationFile()
        {
            XElement windowRoot = new XElement(WINDOW_NAME);

            XElement top = new XElement("Top", this.Top);
            XElement left = new XElement("Left", this.Left);

            windowRoot.Add(top);
            windowRoot.Add(left);

            Configuration.ReplaceRootElement(windowRoot);
        }
        #endregion

        #region WindowsEvents
        void ClosedEventHandler(object sender, CancelEventArgs e)
        {
            Console.WriteLine("Login window closed");
            SaveToConfigurationFile();
        }

        void LoadedEventHandler(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Login window loaded");
            LoadFromConfigurationFile();
        }
        #endregion WindowsEvents



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
            Process.Start("http://www.google.fr");
        }

        public void DonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.google.fr");
        }
    }
}
