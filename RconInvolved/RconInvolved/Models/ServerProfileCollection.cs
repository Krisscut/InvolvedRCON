
namespace RconInvolved.Models
{
    using System;
    using System.Collections.ObjectModel;

    public sealed class ServerProfileCollection : ObservableCollection<ServerProfile>
    {
        public ServerProfileCollection() : this(false)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ServerProfileCollection"/> class.
        /// </summary>
        public ServerProfileCollection(bool hasErrors)
        {
           
        }
    }
    
}



