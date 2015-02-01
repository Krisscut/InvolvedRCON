namespace RconInvolved.OverlayViews.CreateProfileOverlayViews
{
    using System.Windows;
    using System.Windows.Controls;
    using Framework.UI.Controls;
    using RconInvolved.Utils;
    using System;
    /// <summary>
    /// Logique d'interaction pour NotificationView.xaml
    /// </summary>
    public partial class NotificationView
    {
        public NotificationView()
        {
            InitializeComponent();
            Logger.MonitoringLogger.Debug("Notification View Created");
        }

        ~NotificationView()
        {
            //Logger.MonitoringLogger.Debug("Notification View Destroyed");
        }


        private void OnSetPageClick(object sender, RoutedEventArgs e)
        {
            if (this.PageIntegerUpDown.Value.HasValue)
            {
                this.SetPage(this.PagingItemsControl, this.PageIntegerUpDown.Value.Value);
            }
        }

        private void SetPage(ItemsControl pagingItemsControl, int page)
        {
            PagingDecorator pagingDecorator = pagingItemsControl.FindVisualChild<PagingDecorator>();
            if ((page >= 0) &&
                (page < pagingDecorator.Items.Count))
            {
                pagingDecorator.SelectedIndex = page;
            }
        }
    }
}
