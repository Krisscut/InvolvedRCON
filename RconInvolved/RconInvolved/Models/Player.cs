using Framework.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconInvolved.Models
{
    public sealed class Player : NotifyDataErrorInfo<Player>
    {
        private int id;
        private String ip;
        private int ping;
        private String guid;
        private String name;
        private String status;
        private String comment;
        
        public Player(int id, String ip, int ping, String guid, String name)
        {
            this.id = id;
            this.ip = ip;
            this.ping = ping;
            this.guid = guid;
            this.name = name;
        }

        #region get & set
        public String Comment
        {
            get { return comment; }
            set { comment = value; }
        }


        public String Status
        {
            get { return status; }
            set { status = value; }
        }


        public String Name
        {
            get { return name; }
            set { name = value; }
        }


        public String Guid
        {
            get { return guid; }
            set { guid = value; }
        }


        public int Ping
        {
            get { return ping; }
            set { ping = value; }
        }


        public String IP
        {
            get { return ip; }
            set { ip = value; }
        }


        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        #endregion
    }
}
