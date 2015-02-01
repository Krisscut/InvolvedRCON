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
    using Framework.UI.Input;
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

            //Title Bar color change
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

            //BattlEyeLoginCredentials loginCredentials = new BattlEyeLoginCredentials();
            Logger.MonitoringLogger.Debug("Login Window Initialized");
        }

        #region ConfigurationFile
        private void LoadFromConfigurationFile()
        {
            XElement windowsConf = Configuration.GetElementByName("windows",WINDOW_NAME);
            if (windowsConf != null)
            {
                Logger.MonitoringLogger.Debug("Configuration of " + WINDOW_NAME + "loaded : \n" + windowsConf.ToString());
                this.Left = Convert.ToDouble(windowsConf.Element("Left").Value);
                this.Top = Convert.ToDouble(windowsConf.Element("Top").Value);
            }
            else
            {
                Logger.MonitoringLogger.Warn("Can't retrieve " + WINDOW_NAME + " configuration, using default");
            }
        }

        private void SaveToConfigurationFile()
        {
            Logger.MonitoringLogger.Debug("Saving configuration of " + WINDOW_NAME + "loaded");
            XElement windowRoot = new XElement(WINDOW_NAME);

            XElement top = new XElement("Top", this.Top);
            XElement left = new XElement("Left", this.Left);

            windowRoot.Add(top);
            windowRoot.Add(left);
            Configuration.ReplaceChildRootElement("windows", windowRoot);
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
            Logger.MonitoringLogger.Debug("Login window loaded event");
            LoadFromConfigurationFile();

            try
            {
                Logger.MonitoringLogger.Info("Populate profile list with information from the database");
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
            catch (Exception databaseException)
            {
                MessageDialog.ShowAsync(
                        "Erreur de lecture de la base de donnée",
                        "Erreur de lecture de la base de donnée ! Informations de debug :\n" + databaseException.ToString(),
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
            }

            NotifyBox.Show(
                           (DrawingImage)this.FindResource("SearchDrawingImage"),
                           "Astuce",
                           "Un clic droit permet d'ouvrir le menu application !",
                           false);
            Logger.MonitoringLogger.Debug(WINDOW_NAME + " loaded function ended");
        }
        #endregion WindowsEvents

        public void NewConnexionClick(object sender, RoutedEventArgs e)
        {
            this.AppBarLogin.IsOpen = false;
            CreateProfileOverlayView window = new CreateProfileOverlayView(this, GetWindow(this), loginDataView);
            this.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.Paused;
            this.TaskbarProgressValue = 50;
            //this.TaskbarIsBusy = true;
            window.Show();
        }

        public void AuthorsClick(object sender, RoutedEventArgs e)
        {
            this.AppBarLogin.IsOpen = false;
            Process.Start("http://krisscut.legtux.org/applications/RconInvolved/about.html");
        }

        public void LicenseClick(object sender, RoutedEventArgs e)
        {
            this.AppBarLogin.IsOpen = false;
            Process.Start("http://www.google.fr");
        }

        public void DonateClick(object sender, RoutedEventArgs e)
        {
            this.AppBarLogin.IsOpen = false;
            Process.Start("http://www.google.fr");
        }

        private void ListBoxServers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int index = this.ListBoxServers.SelectedIndex;
            Logger.MonitoringLogger.Debug("Selection changed to index " + index);

            Boolean activeButton = false;
            String hostnameValue = "";
            String portValue = "";
            String passwordValue = "";
            Boolean autoReconnectValue = false;

            //Checks if index is selected or unselected 
            if (index != -1)
            {
                activeButton = true;
                foreach (ServerProfile prof in loginDataView.ServersProfiles)
                {
                    if (loginDataView.ServersProfiles.IndexOf(prof) == index) profileSelected = prof;
                };
                //Set UI
                hostnameValue = profileSelected.Hostname;
                portValue = profileSelected.Port.ToString();
                passwordValue = profileSelected.Password;
                autoReconnectValue = profileSelected.AutoReconnect;
            }

            //Now update the UI
            Logger.MonitoringLogger.Debug("Updating UI with new parameters");
            this.HostnameValue.Text = hostnameValue;
            this.PortValue.Text = portValue;
            this.PasswordValue.Text = passwordValue;
            this.AutoReconnectSwitch.IsChecked = autoReconnectValue;
            this.ConnectButton.IsEnabled = activeButton;
            this.ModifyButton.IsEnabled = activeButton;
            this.DeleteButton.IsEnabled = activeButton;
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

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await MessageDialog.ShowAsync(
                "Suppression de profil",
                "Voulez-vous vraiment supprimer le profil selectionné?",
                new[] 
                {
                    new MessageDialogButton()
                    {
                        Command = new DelegateCommand(() => ConfirmDeleteProfile()),
                        Content = "Oui"
                    },
                    new MessageDialogButton()
                    {
                        Command = new DelegateCommand(() => Logger.MonitoringLogger.Info("Suppression de profil annule")),
                        Content = "Non"
                    }
                },
                MessageDialogType.Light);
        }

        private void ConfirmDeleteProfile()
        {
            try
            {
                //Delete from Database
                db = new SQLiteDatabase();
                db.Delete("profileList", String.Format("profilName = '{0}'", profileSelected.ProfilName));
                loginDataView.ServersProfiles.Remove(profileSelected);
                this.profileSelected = null;

                NotifyBox.Show(
                           (DrawingImage)this.FindResource("SearchDrawingImage"),
                           "Profil Manager",
                           "Profil supprimé avec succès !",
                           false);
            }
            catch (Exception e)
            {
                MessageDialog.ShowAsync(
                        "Exception !",
                        "Problème lors de la suppression du profil selectionné, annulation !",
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
                Logger.ExceptionLogger.Error("Attempt to delete from database error : " + profileSelected.ToString() + "\n" + e.ToString());
            }
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
