using Framework.UI.Controls;
using RconInvolved.DataPersistance;
using RconInvolved.Models;
using RconInvolved.OverlayViews.CreateProfileOverlayViews;
using RconInvolved.Utils;
using RconInvolved.ViewModels;
using RconInvolved.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RconInvolved.OverlayViews
{
    /// <summary>
    /// Logique d'interaction pour CreateProfileOverlayView.xaml
    /// </summary>
    public partial class CreateProfileOverlayView
    {
        private SQLiteDatabase db;

        private LoginWindow login;
        private LoginWindowDataViewModel dataView;

        TabControl tabControl;
        private TabItem helpPage;
        private TabItem battleEyePage;
        private TabItem notificationPage;

        private BattleyeView battleEyeView;
        private HelpView helpView;
        private NotificationView notificationView;

        public CreateProfileOverlayView(LoginWindow value, System.Windows.Window master, LoginWindowDataViewModel dataView)
        {
            InitializeComponent();
            this.login = value;
            this.Owner = master;
            this.dataView = dataView;

            //Initialize Tab items for the controller
            tabControl = this.TabController;

            helpPage = new TabItem();
            helpPage.Header = "Validation";
            helpView = new HelpView(value, dataView, this);
            helpPage.Content = helpView;

            battleEyePage = new TabItem();
            battleEyePage.Header = "BattleEye";
            battleEyeView = new BattleyeView(value, dataView, this);
            battleEyePage.Content = battleEyeView;

            notificationPage = new TabItem();
            notificationPage.Header = "Notification";
            notificationView = new NotificationView();
            notificationPage.Content = notificationView;

            tabControl.Items.Add(helpPage);
            tabControl.Items.Add(battleEyePage);
            tabControl.Items.Add(notificationPage);
        }

        private void TabControl_Selected(object sender, RoutedEventArgs e)
        {

        }

        public async void CheckInputParameters()
        {
            bool isValid = true;

            try
            {
                string profilName = battleEyeView.ProfilNameValue.Text;
                string hostname = battleEyeView.HostnameValue.Text;
                int port = Int32.Parse(battleEyeView.PortValue.Text);
                string password = battleEyeView.PasswordValue.Password;
                bool autoReconnect = battleEyeView.AutoReconnect.IsChecked;

                //Profil name
                if (battleEyeView.ProfilNameValue.Text == "")
                {
                    isValid = false;
                    MessageBoxResult result = await MessageDialog.ShowAsync(
                        "Erreur dans les entrées utilisateurs",
                        "Le champ du profil serveur n'est pas rempli !",
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
                }

                if (isValid)
                {
                    this.Close();
                    this.login.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
                    login.TaskbarProgressValue = 0;

                    ServerProfile serverProfile = new ServerProfile(profilName, hostname, port, password, autoReconnect);
                    dataView.ServersProfiles.Add(serverProfile);

                    db = new SQLiteDatabase();
                    Dictionary<String, String> data = new Dictionary<String, String>();
                    data.Add("profilName", profilName);
                    data.Add("hostname", hostname);
                    data.Add("port", port.ToString());
                    data.Add("password", password);
                    data.Add("autoReconnect", autoReconnect.ToString());
                    
                    try {
                        //First, search for and existing profile with this data

                        db.Insert("profileList", data);
                        NotifyBox.Show(
                           (DrawingImage)this.FindResource("SearchDrawingImage"),
                           "Nouveau profil serveur",
                           "Un nouveau profil serveur a été créé avec succès !",
                           false);
                    }
                    catch (Exception e)
                    {
                        MessageDialog.ShowAsync(
                        "Erreur d'insertion dans la base de donnée",
                        "Erreur lors de l'insertion en base de donnée ! Informations de debug :\n" + e.ToString(),
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.MonitoringLogger.Error("Exception Throwed in CreateProfileOverlayView : \n" + e.ToString());
                Logger.ExceptionLogger.Error("Exception in createProfile \n" + e.ToString());
                MessageDialog.ShowAsync(
                        "Erreur dans les entrées utilisateurs",
                        "Erreur ayant causé une exception, revoir le contenu des champs utilisateurs !",
                        MessageBoxButton.OK,
                        MessageDialogType.Light,
                        this);
            }
        }
    }
}
