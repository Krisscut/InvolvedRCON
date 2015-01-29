namespace RconInvolved.OverlayViews
{
    using System.Windows;
    using RconInvolved.Windows;
    using Framework.UI.Controls;
    using RconInvolved.ViewModels;
    using System;
    using RconInvolved.Models;
    using RconInvolved.Utils;
    using RconInvolved.DataPersistance;
    using System.Collections.Generic;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for OverlayWindowExample.xaml
    /// </summary>
    public partial class CreateConnectionOverlayView
    {
        private LoginWindow login;
        private string status;
        private LoginWindowDataViewModel dataView;

        SQLiteDatabase db;

        public CreateConnectionOverlayView(LoginWindow value, System.Windows.Window master, LoginWindowDataViewModel dataView)
        {
            InitializeComponent();
            this.login = value;
            this.Owner = master;
            this.dataView = dataView;
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            this.login.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
            login.TaskbarProgressValue = 0;
            //login.TaskbarIsBusy = false;

            this.Close();
        }

        private void OnClickValid(object sender, RoutedEventArgs e)
        {
            this.login.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
            login.TaskbarProgressValue = 0;
            //login.TaskbarIsBusy = false;

            CheckInputParameters();
        }

        public async void CheckInputParameters()
        {
            bool isValid = true;

            try
            {
                string profilName = this.ProfilNameValue.Text;
                string hostname = this.HostnameValue.Text;
                int port = Int32.Parse(this.PortValue.Text);
                string password = this.PasswordValue.Password;
                bool autoReconnect = this.AutoReconnect.IsChecked;

                //Profil name
                if (this.ProfilNameValue.Text == "")
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

                    ServerProfile serverProfile = new ServerProfile( profilName, hostname, port, password, autoReconnect);
                    dataView.ServersProfiles.Add(serverProfile);

                    db = new SQLiteDatabase();
                    Dictionary<String, String> data = new Dictionary<String, String>();
                    data.Add("profilName", profilName);
                    data.Add("hostname", hostname);
                    data.Add("port", port.ToString());
                    data.Add("password", password);
                    data.Add("autoReconnect", autoReconnect.ToString());
                    try
                    {
                        db.Insert("profileList", data);
                        NotifyBox.Show(
                           (DrawingImage)this.FindResource("SearchDrawingImage"),
                           "Nouveau profil serveur",
                           "Un nouveau profil serveur a été créé avec succès !",
                           false);
                    }
                    catch (Exception crap)
                    {
                        MessageBox.Show(crap.Message);
                    }
                }
            }
            catch (Exception)
            {
                Logger.MonitoringLogger.Error("Exception Throwed in CreateConnectionOverlay !");
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
