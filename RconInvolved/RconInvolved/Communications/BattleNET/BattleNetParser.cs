using RconInvolved.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RconInvolved.Communications.BattleNET
{
    public class BattleNetParser
    {
        private Mutex listParsedMessageMutex;
        private Collection<BattleNetMessage> listParsedMessage;
        private String message;

        public BattleNetParser(ref Mutex listParsedMessageMutex, ref Collection<BattleNetMessage> listParsedMessage, String message)
        {
            this.listParsedMessageMutex = listParsedMessageMutex;
            this.listParsedMessage = listParsedMessage;
            this.message = message;

            Thread parserThread = new Thread(parseMessage);
            parserThread.Start();
        }

        public void parseMessage()
        {
            BattleNetMessage messageParsed = null;

            String[] linesMessage = message.Split('\n');


            if (linesMessage[0].Contains("Rcon")) {
                Logger.MonitoringLogger.Info("Admin connection notification received! ");
                messageParsed = new BattleNetMessage(BattleNetMessageType.AdminConnected, message);
            }
            else if (linesMessage[0].Contains("Bans"))
            {
                Logger.MonitoringLogger.Info("Ban list message received! ");
                messageParsed = new BattleNetBanMessage(BattleNetMessageType.BanList, message);
            }
            else if (linesMessage[0].Contains("Players on server"))
            {
                Logger.MonitoringLogger.Info("Player list message received! ");
                messageParsed = new BattleNetPlayersMessage(BattleNetMessageType.PlayerList, message);
            }
            else {
                Logger.MonitoringLogger.Warn("Unknown message received ! ");
                messageParsed = new BattleNetMessage(BattleNetMessageType.Unknown, message);
            }

            this.listParsedMessageMutex.WaitOne();
            listParsedMessage.Add(messageParsed);
            this.listParsedMessageMutex.ReleaseMutex();
        }
    }
}
