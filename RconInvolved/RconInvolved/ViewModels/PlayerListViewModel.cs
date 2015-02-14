using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RconInvolved.Models;
using System.Windows.Data;

namespace RconInvolved.ViewModels
{
    class PlayerListViewModel
    {
        #region Fields
        private int decimalPlaces = 2;
        private PlayerCollection players;
        private ICollectionView playersView;
        private double zoom = 1D; 

        #endregion

        #region Constructors

        public PlayerListViewModel()
        {
            this.players = new PlayerCollection(true);
            this.playersView = CollectionViewSource.GetDefaultView(this.players);
        }

        #endregion
    }
}
