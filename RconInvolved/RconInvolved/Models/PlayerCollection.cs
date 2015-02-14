using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconInvolved.Models
{
    public sealed class PlayerCollection : ObservableCollection<Player>
    {
        public PlayerCollection() : this(false)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerCollection"/> class.
        /// </summary>
        public PlayerCollection(bool hasErrors)
        {
           
        }
    }
}
