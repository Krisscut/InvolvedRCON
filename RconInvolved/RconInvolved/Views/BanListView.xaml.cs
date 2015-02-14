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
    /// Logique d'interaction pour BanList.xaml
    /// </summary>
    public partial class BanListView {
        MainWindow mainWindow;

        private DispatcherTimer banListTimer = new DispatcherTimer();
        private BackgroundWorker getBanListWorker;
        Boolean banListNeedUpdate;

        public BanListView(MainWindow mainWindow) {
            this.mainWindow = mainWindow;
            InitializeComponent();

            banListNeedUpdate = true;
            banListTimer.Tick += new EventHandler(OnTimerEnded);
            banListTimer.Interval = new TimeSpan(0, 0, 15);
        }


        public void startWorker()
        {
            banListTimer.Start();
            if (banListNeedUpdate)
            {
                getBanListWorker = new BackgroundWorker();
                getBanListWorker.WorkerReportsProgress = true;
                getBanListWorker.DoWork += new DoWorkEventHandler(getBanListWorker_doWork);
                getBanListWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getBanListWorker_WorkCompleted);
                getBanListWorker.RunWorkerAsync();
            }
        }

        private void OnTimerEnded(object sender, EventArgs e)
        {
            if (banListNeedUpdate)
            {
                getBanListWorker = new BackgroundWorker();
                getBanListWorker.WorkerReportsProgress = true;
                getBanListWorker.DoWork += new DoWorkEventHandler(getBanListWorker_doWork);
                getBanListWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getBanListWorker_WorkCompleted);
                getBanListWorker.RunWorkerAsync();
            }

            // search for new message from heap
        }

        public void stopWorker()
        {
            banListTimer.Stop();
        }

        private void getBanListWorker_doWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("getBanListWorker_doWork launched");
            mainWindow.battleNetClient.SendCommand("bans");
            while (mainWindow.battleNetClient.CommandQueue > 0) { /* wait until server received packet */ };
        }

        private void getBanListWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            banListNeedUpdate = false;
            Console.WriteLine("Ban list list request completed");
        }
    }
}
