namespace RconInvolved.OverlayViews
{
    using System.Windows;
    using RconInvolved.Windows;

    /// <summary>
    /// Interaction logic for OverlayWindowExample.xaml
    /// </summary>
    public partial class CreateConnectionOverlayView
    {
        private LoginWindow login;

        public CreateConnectionOverlayView(LoginWindow value, Window master)
        {
            InitializeComponent();
            this.login = value;
            this.Owner = master;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            this.login.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
            login.TaskbarProgressValue = 0;
            //login.TaskbarIsBusy = false;
            this.Close();
        }

        public void setWindowOwner (LoginWindow value )
        {

        }
    }
}
