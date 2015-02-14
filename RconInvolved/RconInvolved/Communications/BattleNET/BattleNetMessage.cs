using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconInvolved.Communications.BattleNET
{
    public enum BattleNetMessageType
    {
	    PlayerList,
	    BanList,
	    AdminConnected,
	    Unknown
    };

    public class BattleNetMessage
    {
        public BattleNetMessageType type;
        public String content;
        public DateTime dateReceived;

        public BattleNetMessage(BattleNetMessageType type, String content)
        {
            this.type = type;
            this.content = content;
            this.dateReceived = DateTime.Now;
        }
    }
}
