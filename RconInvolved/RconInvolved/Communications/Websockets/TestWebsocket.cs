using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace RconInvolved.Communications.Websockets
{
    
    class TestWebsocket
    {
        //static string URLService = "ws://krisscut.ddns.net:12229/TestService";
        static string URLService = "ws://127.0.0.1:12229/TestService";
        WebSocket ws;
        public TestWebsocket()
        {
            ws = new WebSocket(URLService);
            ws.OnMessage += (sender, e) =>
                Console.WriteLine("Servers says: " + e.Data);
            ws.OnOpen += (sender, e) =>
                Console.WriteLine("Connected ! ");
            ws.OnClose += (sender, e) =>
                Console.WriteLine("Connection closed with server ! ");

            ws.Connect();
            ws.Send("test");
            //Console.ReadKey(true);
        }
    }
}
