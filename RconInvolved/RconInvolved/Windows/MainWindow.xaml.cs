using BattleNET;
using Framework.UI.Controls;
using RconInvolved.Communications.BattleNET;
using RconInvolved.Models;
using RconInvolved.Utils;
using RconInvolved.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RconInvolved.Windows
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ServerProfile serverProfile;

        TabControl tabControl;
        private TabItem playerListPage;
        private TabItem banListPage;

        private PlayerListView playerListView;
        private BanListView banListView;

        public BattlEyeClient battleNetClient;

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Collection<String> listReceivedMessage = new Collection<String>();

        public Mutex listParsedMessageMutex;
        public Collection<BattleNetMessage> listParsedMessage = new Collection<BattleNetMessage>();

        public MainWindow(BattlEyeLoginCredentials loginCredentials, ServerProfile serverProfile)
        {
            this.serverProfile = serverProfile;
            InitializeComponent();
            this.Closing += new CancelEventHandler(ClosedEventHandler);
            this.listParsedMessageMutex = new Mutex();
            populateWindow();

            dispatcherTimer.Tick += new EventHandler(OnTimerEnded);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 60);

            battleNetClient = createBattleNetConnection(loginCredentials);
        }

        ~MainWindow()
        {
            Logger.MonitoringLogger.Info("MainWindow is destroyed");
        }

        void ClosedEventHandler(object sender, CancelEventArgs e)
        {
            Logger.MonitoringLogger.Debug("MainWindow is closing");
            dispatcherTimer.Stop();
            stopWorkers();
            battleNetClient.Disconnect();
        }

        public void populateWindow()
        {
            //Initialize Tab items for the controller
            tabControl = this.TabController;

            playerListPage = new TabItem();
            playerListPage.Header = "Liste joueurs";
            playerListView = new PlayerListView(this);
            playerListPage.Content = playerListView;

            banListPage = new TabItem();
            banListPage.Header = "Liste bans";
            banListView = new BanListView(this);
            banListPage.Content = banListView;

            tabControl.Items.Add(playerListPage);
            tabControl.Items.Add(banListPage);
        }

        public void startWorkers() {
            dispatcherTimer.Start();

            playerListView.startWorker();
            banListView.startWorker();
        }

        public void stopWorkers() {
            playerListView.stopWorker();
            banListView.stopWorker();
        }

        private void OnTimerEnded(object sender, EventArgs e)
        {
            Logger.MonitoringLogger.Debug("Main window Timer ended");

            //Clean up operation !
            Thread myCleanUpThread = new Thread(CleanUpReceivedMessage);
            myCleanUpThread.Start();

            //banListView.startWorker();
            //playerListView.startWorker();
        }

        private void CleanUpReceivedMessage(object obj)
        {
            Collection<BattleNetMessage> toDelete = new Collection<BattleNetMessage>();
            foreach( BattleNetMessage message in listParsedMessage) {
                if ((DateTime.Now - message.dateReceived).TotalSeconds > 120)
                {
                    toDelete.Add(message);
                }
            }
            Logger.MonitoringLogger.Debug("Clean up function delete " + toDelete.Count + " unreads message");
            //Critical section
            this.listParsedMessageMutex.WaitOne();
            foreach( BattleNetMessage message in toDelete) {
                listParsedMessage.Remove(message);
            }
            this.listParsedMessageMutex.ReleaseMutex();
        }

        private BattlEyeClient createBattleNetConnection(BattlEyeLoginCredentials loginCredentials)
        {
            battleNetClient = new BattlEyeClient(loginCredentials);
            battleNetClient.BattlEyeMessageReceived += BattlEyeMessageReceived;
            battleNetClient.BattlEyeConnected += BattlEyeConnected;
            battleNetClient.BattlEyeDisconnected += BattlEyeDisconnected;
            battleNetClient.ReconnectOnPacketLoss = true;
            battleNetClient.Connect();

            return battleNetClient;
        }

        private void BattlEyeConnected(BattlEyeConnectEventArgs args)
        {
            if (args.ConnectionResult == BattlEyeConnectionResult.Success) { /* Connected successfully */
                Logger.MonitoringLogger.Info("BattleEye connected !");
                startWorkers();
                Console.WriteLine(args.Message);
                NotifyBox.Show(
                              (DrawingImage)this.FindResource("SearchDrawingImage"),
                              "Connexion réussie",
                              "Connexion réussie avec battleEye !",
                              false);
            }
            else if (args.ConnectionResult == BattlEyeConnectionResult.InvalidLogin) {
                NotifyBox.Show(
                              (DrawingImage)this.FindResource("SearchDrawingImage"),
                              "Echec connexion",
                              "Vos logins sont incorrects!",
                              false); 
            }
            else if (args.ConnectionResult == BattlEyeConnectionResult.ConnectionFailed) { /* Connection failed, host unreachable */
                NotifyBox.Show(
                                 (DrawingImage)this.FindResource("SearchDrawingImage"),
                                 "Echec connexion",
                                 "Host unreacheanble!",
                                 false);
            }
        }

        private void BattlEyeDisconnected(BattlEyeDisconnectEventArgs args)
        {
            //if (args.DisconnectionType == BattlEyeDisconnectionType.ConnectionLost) { /* Connection lost (timeout), if ReconnectOnPacketLoss is set to true it will reconnect */ }
            //if (args.DisconnectionType == BattlEyeDisconnectionType.SocketException) { /* Something went terribly wrong... */ }
            //if (args.DisconnectionType == BattlEyeDisconnectionType.Manual) { /* Disconnected by implementing application, that would be you */ }
            stopWorkers();
            NotifyBox.Show(
                          (DrawingImage)this.FindResource("SearchDrawingImage"),
                          "Connexion perdue",
                          "Connexion perdue avec battleEye!",
                          false);
            Logger.MonitoringLogger.Warn("BattleEye disconnected !");
            Console.WriteLine(args.Message);
        }
        private void BattlEyeMessageReceived(BattlEyeMessageEventArgs args)
        {
            //if (args.Id == playerListId)
            //{
            //    playerList = args.Message;
            //}
            this.listReceivedMessage.Add(args.Message);
            Console.WriteLine(args.Message);
            Console.WriteLine("New message received");

            //Parse operation in another thread
            new BattleNetParser(ref listParsedMessageMutex, ref listParsedMessage, args.Message);
        }


    }
}
