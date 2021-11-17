using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace TCP_Asincrona_Console
{
    class Program
    {
        static IPEndPoint ep;
        static Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static Socket worker;
        static bool Server = false;
        static string IP = "";
        static int Porta = 0;
        static ManualResetEvent mre = new ManualResetEvent(false);
        static bool ok = false;
        static byte[] input = new byte[1024];
        static byte[] output = new byte[1024];

        static void Main(string[] args)
        {
            Console.WriteLine("Sei il server? S/N");
            string m = Console.ReadLine();
            if (m == "s" || m == "S")
            {
                Server = true;
            }
            if (m == "n" || m == "N")
            {
                Server = false;
            }
            Console.Clear();

            Console.WriteLine("IP:");
            IP = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Porta:");
            Porta = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (Server)
                Console.WriteLine("Sei il PC Server");
            else
                Console.WriteLine("Sei il PC Client");

            Console.WriteLine("IP: " + IP);
            Console.WriteLine("Porta: " + Porta.ToString());
            Console.WriteLine("Connettere o reimpostare? C/R");
            string m1 = Console.ReadLine();
            if (m1 != "r" || m1 != "R")
            {
                //Ritorna all'inizio
            }


            //Configuro e avvio Socket
            if (m1 != "c" || m1 != "C")
            {
                Console.Clear();
                ep = new IPEndPoint(IPAddress.Parse(IP), Porta);
                if (Server)
                    StartListening();
                else
                    RichiestaConnessione();
            }
        }

        public static void StartListening()
        {
            listener.Bind(ep);
            listener.Listen(50);
            Console.WriteLine("In attesa di connessioni...");
            while (true)
            {
                mre.Reset();
                listener.BeginAccept(new AsyncCallback(AccettaRichiesta), listener);
                mre.WaitOne();
            }
        }

        public static void AccettaRichiesta(IAsyncResult ar)
        {
            // Segnalo al thread StartListening di continuare ad ascoltare
            mre.Set();
            Console.WriteLine("Una richiesta è stata accettata.");
            Console.WriteLine("In attesa di altre connessioni...");
            Socket listener, worker;
            try
            {
                listener = (Socket)ar.AsyncState;
                worker = listener.EndAccept(ar);
            }
            catch (ObjectDisposedException)
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                worker = listener.EndAccept(ar);
            }
            //memorizzo socket del client
            StateObject Stato = new StateObject();
            Stato.workSocket = worker;
            worker.BeginReceive(Stato.buffer, 0, StateObject.BufferSize, 0,new AsyncCallback(LeggiInput), Stato);
        }

        public static void LeggiInput(IAsyncResult ar)
        {
            //recupero dettagli socket dal StateObject
            StateObject Stato = (StateObject)ar.AsyncState;
            Socket worker = Stato.workSocket;
            string Input ="";

            // Read data from the client socket. 
            int BytesRicevuti = worker.EndReceive(ar);

            if (BytesRicevuti > 0)
            {
                // There  might be more data, so store the data received so far.
                Stato.sb.Append(Encoding.ASCII.GetString(
                    Stato.buffer, 0, BytesRicevuti));
            }
            // Check for end-of-file tag. If it is not there, read 
            // more data.
            Input = Stato.sb.ToString();
            if (Input.IndexOf("11111111") > -1)
            {   //<EOF>
                // All the data has been read from the 
                // client. Display it on the console.
                Console.WriteLine(Input);
                // Echo the data back to the client.
                ImpostaInvio(worker, Input, Stato);
            } else 
            {
                // Not all data received. Get more.
                worker.BeginReceive(Stato.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(LeggiInput), Stato);
            }
        }

        private static void ImpostaInvio(Socket worker, string messaggio, StateObject stato)
        {
            if (!messaggio.Contains("00000000"))
            {
                byte[] byteData = Encoding.ASCII.GetBytes(messaggio);
                worker.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(CompletaInvio), stato);
            }
        }

        private static void CompletaInvio(IAsyncResult ar)
        {
            StateObject Stato = (StateObject)ar.AsyncState;
            //recupero dettagli socket dal StateObject
            Socket worker = (Socket)Stato.workSocket;
            int bytesSent = worker.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            Stato.sb = new StringBuilder();
            Stato.buffer = new byte[StateObject.BufferSize];
            worker.BeginReceive(Stato.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(LeggiInput), Stato);
            //LeggiInput(ar);
            //worker.Shutdown(SocketShutdown.Both);
            //worker.Close();
        }


        public class StateObject // Fornisce oggetti per leggere messaggi da Client
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            // Received data string.
            public StringBuilder sb = new StringBuilder();
        }



        private static void RichiestaConnessione()
        {
             if (ok == false)
                {
                    
                    worker = listener;
                    worker.Connect(ep);
                    Console.WriteLine("Connessione eseguita! Digitare 00000000 per chiudere connessione");
                    ok = true;
                }
                Console.WriteLine("Che messaggio inviare?");
                string messaggio = "";
                messaggio = Console.ReadLine()+"11111111";
                output = Encoding.ASCII.GetBytes(messaggio);
                worker.Send(output);
                if (messaggio.Contains("00000000"))
                {
                    Console.Clear();
                    Console.WriteLine("Ti sei disconnesso dal Server. Ricominciare? Y/N");
                    string m = Console.ReadLine();
                    if (m == "n" || m == "N")
                    {
                        listener.Close();
                        worker.Close();
                        Environment.Exit(0);
                    }
                    if (m == "y" || m == "Y")
                    {
                        Main(null);
                    }

                }
                Console.ForegroundColor = ConsoleColor.Blue;
                messaggio = messaggio.Replace("11111111", "");
                Console.WriteLine("Io: " + messaggio);
                Console.ForegroundColor = ConsoleColor.Gray;
                Invio();
            }

        private static void Invio()
        {
            input = new byte[1024];
            Console.WriteLine("In attesa di messaggio da Server....");
            worker.Receive(input);
            input = input.Where(elem => elem != 0).ToArray();
            String messaggio = Encoding.ASCII.GetString(input);
            if (messaggio.Contains("00000000"))
            {
                //listener.Shutdown(SocketShutdown.Both);
                listener.Close();
                //worker.Shutdown(SocketShutdown.Both);
                worker.Close();
                Console.Clear();
                Console.WriteLine("Il Server si è disconnesso. Ricominciare? Y/N");
                string m = Console.ReadLine();
                if (m == "n" || m == "N")
                {
                    Environment.Exit(0);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            messaggio = messaggio.Remove(messaggio.Length - 8, 8);
            Console.WriteLine("Server: " + messaggio);
            Console.ForegroundColor = ConsoleColor.Gray;
            RichiestaConnessione();
        }
    }
}
