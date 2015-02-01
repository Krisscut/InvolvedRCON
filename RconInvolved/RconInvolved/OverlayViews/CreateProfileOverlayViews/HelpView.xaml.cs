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

namespace RconInvolved.OverlayViews.CreateProfileOverlayViews
{
    /// <summary>
    /// Logique d'interaction pour HelpView.xaml
    /// </summary>
    public partial class HelpView
    {
        private LoginWindow login;
        private string status;
        private LoginWindowDataViewModel dataView;
        private CreateProfileOverlayView masterWindow;

        public HelpView(LoginWindow value, LoginWindowDataViewModel dataView, CreateProfileOverlayView masterWindow)
        {
            InitializeComponent();
            this.login = value;
            this.dataView = dataView;
            this.masterWindow = masterWindow;
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            this.login.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
            login.TaskbarProgressValue = 0;
            //login.TaskbarIsBusy = false;

            masterWindow.Close();
        }

        private void OnClickValid(object sender, RoutedEventArgs e)
        {
            this.login.TaskbarProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
            login.TaskbarProgressValue = 0;
            //login.TaskbarIsBusy = false;

            //Check if input is ok
            masterWindow.CheckInputParameters();
        }
    }


}
