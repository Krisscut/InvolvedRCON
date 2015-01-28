
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
    using System.Windows.Media;
    using RconInvolved.Utils;
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private static string WINDOW_NAME = "LoginWindow";
        private LoginWindowDataViewModel loginDataView = new LoginWindowDataViewModel();

        public LoginWindow()
        {
            Logger.MonitoringLogger.Debug("Initializing LoginWindow");
            InitializeComponent();

            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#404040");
            this.TitleBarBackground = brush;
            this.DataContext = loginDataView;

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
            Logger.MonitoringLogger.Debug("Login window closed");
            SaveToConfigurationFile();
        }

        void LoadedEventHandler(object sender, RoutedEventArgs e)
        {
            Logger.MonitoringLogger.Debug("Login window loaded");
            LoadFromConfigurationFile();
        }
        #endregion WindowsEvents

        public void NewConnexionClick(object sender, RoutedEventArgs e)
        {
            CreateConnectionOverlayView window = new CreateConnectionOverlayView(this, Window.GetWindow(this), loginDataView);
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

        private void ListBoxServers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ListBoxServers_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.loginDataView.ServersProfiles.Count != 0)
            {
                this.MaskList.Text = "";
            }
            else
            {
                this.MaskList.Text = "Pas de profil";
            }
        }
    }
}
