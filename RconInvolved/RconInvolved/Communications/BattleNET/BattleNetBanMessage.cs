using RconInvolved.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RconInvolved.Communications.BattleNET
{
    public class BattleNetBanMessage : BattleNetMessage
    {
        List<Ban> banList = new List<Ban>();

        public BattleNetBanMessage(BattleNetMessageType type, String content): base(type, content)
        {
            parseMessage();
        }

        private void parseMessage()
        {
            String[] linesMessage = content.Split('\n');

            //No players are banned
            if (linesMessage[3].Equals("\n"))
            {

            } else
            {
                //input example : 
                //1804 b1e83b1223e10a90e66702a38d84ed14 perm Ban perm multiple freekill - Graincheux
                for (int i = 3; i < linesMessage.Count()-1; i++) {
                    if (linesMessage[i].Equals("")) break;

                    Match match = Regex.Match(linesMessage[i], "([0-9]+)[\\s]+(\\w+)[\\s]+(\\w+)[\\s](.*)");

                    int id = Int32.Parse(match.Groups[1].Value);
                    String GUID = match.Groups[2].Value;
                    String duration = match.Groups[3].Value;
                    String comment = match.Groups[4].Value;

                    banList.Add(new Ban(id, GUID, duration, comment));
                }
            }
        } 
    }
}
