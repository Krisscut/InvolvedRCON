namespace RconInvolved.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.ComponentModel;
    using Framework.ComponentModel.Rules;
    using Framework.UI.Input;
    using RconInvolved.Models;

    public sealed class ServerProfile: NotifyDataErrorInfo<ServerProfile>
    {
        private String profilName;
        private String hostname;
        private int port;
        private String password;
        private bool autoReconnect;

        public ServerProfile( String profilName, String hostname, int port, String password, bool autoReconnect)
        {
            this.profilName = profilName;
            this.hostname = hostname;
            this.port = port;
            this.password = password;
            this.autoReconnect = autoReconnect;
        }

        public String ProfilName
        {
            get { return this.profilName; }
            set { this.SetProperty(ref this.profilName, value); }
        }

        public String Hostname {
            get { return this.hostname; }
            set { this.SetProperty(ref this.hostname, value); } 
        }

        public int Port
        {
            get { return this.port; }
            set { this.SetProperty(ref this.port, value); }
        }

        public String Password
        {
            get { return this.password; }
            set { this.SetProperty(ref this.password, value); }
        }

        public bool AutoReconnect
        {
            get { return this.autoReconnect; }
            set { this.SetProperty(ref this.autoReconnect, value); }
        }

    }
}
