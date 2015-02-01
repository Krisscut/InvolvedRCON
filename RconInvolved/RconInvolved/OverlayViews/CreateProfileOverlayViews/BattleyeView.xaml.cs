namespace RconInvolved.OverlayViews.CreateProfileOverlayViews
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
    public partial class BattleyeView
    {
        private LoginWindow login;
        private string status;
        private LoginWindowDataViewModel dataView;
        private CreateProfileOverlayView masterWindow;

        SQLiteDatabase db;

        public BattleyeView(LoginWindow value, LoginWindowDataViewModel dataView, CreateProfileOverlayView masterWindow)
        {
            InitializeComponent();
            this.login = value;
            this.dataView = dataView;
            this.masterWindow = masterWindow;
            Logger.MonitoringLogger.Debug("BattleEye View Initialized");
        }

        ~BattleyeView()
        {
            //Logger.MonitoringLogger.Debug("BattleEye View Destroyed");
        }
    }
}
