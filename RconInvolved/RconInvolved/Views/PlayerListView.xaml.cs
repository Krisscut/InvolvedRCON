using RconInvolved.ViewModels;
using RconInvolved.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace RconInvolved.Views
{
    /// <summary>
    /// Logique d'interaction pour PlayerList.xaml
    /// </summary>
    public partial class PlayerListView {
        MainWindow mainWindow;
        PlayerListViewModel playerListViewModel;

        private DispatcherTimer playerListTimer = new DispatcherTimer();
        private BackgroundWorker getPlayerListWorker;


        public PlayerListView(MainWindow mainWindow) {
            this.mainWindow = mainWindow;
            InitializeComponent();
            playerListViewModel = new PlayerListViewModel();
            this.DataContext = playerListViewModel;

            playerListTimer.Tick += new EventHandler(OnTimerEnded);
            playerListTimer.Interval = new TimeSpan(0, 0, 15);
        }

        public void startWorker() {
            playerListTimer.Start();
        }

        private void OnTimerEnded(object sender, EventArgs e)
        {
            //Ask for player list
            getPlayerListWorker = new BackgroundWorker();
            getPlayerListWorker.WorkerReportsProgress = true;
            getPlayerListWorker.DoWork += new DoWorkEventHandler(getPlayerListWorker_doWork);
            getPlayerListWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getPlayerListWorker_WorkCompleted);
            getPlayerListWorker.RunWorkerAsync();
        }

        public void stopWorker(){
            playerListTimer.Stop();
        }

        private void getPlayerListWorker_doWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Player request launched");
            mainWindow.battleNetClient.SendCommand("players");
            while (mainWindow.battleNetClient.CommandQueue > 0) { /* wait until server received packet */ };
        }

        private void getPlayerListWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Getplayers list request completed");
        }
    }
}
