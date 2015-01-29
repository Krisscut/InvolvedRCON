
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
    using System.Data;
    using RconInvolved.Models;
    using System.Collections.Specialized;
    using Framework.UI.Controls;
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private static string WINDOW_NAME = "LoginWindow";
        private LoginWindowDataViewModel loginDataView = new LoginWindowDataViewModel();
        SQLiteDatabase db;
        ServerProfile profileSelected = null;

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
            loginDataView.ServersProfiles.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedEventHandler);

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

            try
            {
                db = new SQLiteDatabase();
                DataTable resultQuery;
                String query = "select * FROM profileList;";
                resultQuery = db.GetDataTable(query);
                
                // Loop over all data
                foreach (DataRow r in resultQuery.Rows)
                {
                    ServerProfile serverProfile = new ServerProfile(r["profilName"].ToString(), r["hostname"].ToString(), Int32.Parse(r["port"].ToString()), r["password"].ToString(), Boolean.Parse(r["autoReconnect"].ToString()));
                    loginDataView.ServersProfiles.Add(serverProfile);
                    Logger.MonitoringLogger.Info(r.ToString());
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            NotifyBox.Show(
                           (DrawingImage)this.FindResource("SearchDrawingImage"),
                           "Astuce",
                           "Un clic droit permet d'ouvrir le menu application !",
                           false);
        }
        #endregion WindowsEvents

        public void NewConnexionClick(object sender, RoutedEventArgs e)
        {
            CreateConnectionOverlayView window = new CreateConnectionOverlayView(this, GetWindow(this), loginDataView);
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
            this.ConnectButton.IsEnabled = true;
            this.ModifyButton.IsEnabled = true;
            this.DeleteButton.IsEnabled = true;

            int index = this.ListBoxServers.SelectedIndex;
            Logger.MonitoringLogger.Debug("Selection changed to index " + index);
            foreach (ServerProfile prof in loginDataView.ServersProfiles)
            {
                if (loginDataView.ServersProfiles.IndexOf(prof) == index) profileSelected = prof;
            };

            Logger.MonitoringLogger.Debug("Updating UI with parameters");
            //Set UI
            this.HostnameValue.Text = profileSelected.Hostname;
            this.PortValue.Text = profileSelected.Port.ToString();
            this.PasswordValue.Text = profileSelected.Password;
            this.AutoReconnectSwitch.IsChecked = profileSelected.AutoReconnect;
        }

        private void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            Logger.MonitoringLogger.Info("Selection changed" + e.ToString());
            if (this.loginDataView.ServersProfiles.Count != 0)
            {
                this.MaskList.Opacity = 0.0;

            }
            else
            {
                this.MaskList.Opacity = 1.0;
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
             MessageDialog.ShowAsync(
                        "WIP",
                        "Functionnalité pas encore implémentée !",
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog.ShowAsync(
                        "WIP",
                        "Functionnalité pas encore implémentée !",
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog.ShowAsync(
                        "WIP",
                        "Functionnalité pas encore implémentée !",
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
        }


    }
}
