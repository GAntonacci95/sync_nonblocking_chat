using System;
using System.Collections.Generic;
using System.Text;
using AsyncLibrary;
using AsyncLibrary.Server;
using AsyncLibrary.Frame;
using System.Net.Sockets;

namespace LatoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Connected += new ConnectedEventHandler(Connect);
            server.Routing += new RoutingEventHandler(Route);
            server.Disconnected += new DisconnectedEventHandler(Disconnect);
        }

        private static void Connect(object sender, object[] infos)
        {
            Console.WriteLine("Utente: {0}, IPv4: {1}: CONNESSO.", infos[0].ToString(), ((Socket)infos[1]).RemoteEndPoint.ToString());
        }

        private static void Route(object sender, Frame frame)
        {
            Console.WriteLine("\nFRAME: {0} => \n\tda Utente: {1}, rivolto a Utente: {2}, Messaggio: {3}", frame.ToString(), frame.Mittente, frame.Destinatario, frame.Messaggio);
        }

        private static void Disconnect(object sender, object[] infos)
        {
            Console.WriteLine("Utente: {0}, IPv4: {1}: DISCONNESSO.", infos[0].ToString(), ((Socket)infos[1]).RemoteEndPoint.ToString());
        }
    }
}
