using RconInvolved.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RconInvolved.Communications.BattleNET
{
    public class BattleNetPlayersMessage : BattleNetMessage
    {
        List<Player> playerList = new List<Player>();

        public BattleNetPlayersMessage(BattleNetMessageType type, String content): base(type, content)
        {
            parseMessage();
        }

        private void parseMessage()
        {
            String[] linesMessage = content.Split('\n');

            //No players are online
            if (linesMessage[3].Contains("players in total"))
            {

            } else
            {
                //input example : 
                //0   88.125.22.111:2306    32   71fbad6ce399e98d46a6772374b6a135(OK) Maxim Ivanov
                for (int i = 3; i < linesMessage.Count()-1; i++) {
                    Match match = Regex.Match(linesMessage[i], "([0-9]+)[\\s]+(\\S+:[0-9]+)[\\s]+([0-9]+)[\\s]+(\\w+)\\(OK\\)[\\s]+(.+)");

                    int id = Int32.Parse(match.Groups[1].Value);
                    String[] ipAndPort = match.Groups[2].Value.Split(':');
                    String ip = ipAndPort[0];
                    int ping = Int32.Parse(match.Groups[3].Value);
                    String GUID = match.Groups[4].Value;
                    String name = match.Groups[5].Value;

                    playerList.Add(new Player(id, ip, ping, GUID, name));
                }
            }
        } 
    }
}
