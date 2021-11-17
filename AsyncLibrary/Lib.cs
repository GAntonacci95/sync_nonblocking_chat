using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncLibrary
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Sockets;

    public class Generic
    {
        public static Socket Socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public const string IP = "127.0.0.1";

        public const int Porta = 8080;
    }

    public class StateObject
    {
        public Socket WorkSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder FrameBuilder = new StringBuilder();
    }

    namespace Frame
    {
        /*Eccezioni
        public class InvalidFrameException : Exception, System.Runtime.Serialization.ISerializable
        {
            private string _message;

            public InvalidFrameException(string invalidsender)
            {
                string mex = string.Format("Exception thrown by frame: " + invalidsender);
                this._message = mex;
            }

            public string Message
            {
                get { return this._message; }
            }
        }
        */

        public class CredentialFrame
        {
            private string _cred;
            public const char Cred = '\u01C4';
            public const int CredCount = 2;
            private int _length;

            public static bool IsValidCredentialFrame(CredentialFrame f)
            {
                if (IsValidCredentialFrame(f.ToString()))
                    return true;
                return false;
            }

            public static bool IsValidCredentialFrame(string f)
            {
                if (f.Count(elem => elem == Cred) == CredCount)
                    return true;
                return false;
            }

            #region Delegati
            delegate byte[] ByteConversionDelegate(string par);
            delegate string StringConversionDelegate(byte[] par);
            #endregion

            #region Costruttori
            public CredentialFrame()
            {
                this._cred = ""; this._length = CredCount;
            }

            public CredentialFrame(string rec)
            {
                _cred = rec.Split(Cred).Where(elem => !string.IsNullOrEmpty(elem)).ToArray()[0];
                this._length = GetLength();
            }

            public CredentialFrame(byte[] rec)
            {
                _cred = GetString(rec).Split(Cred).Where(elem => !string.IsNullOrEmpty(elem)).ToArray()[0];
                this._length = GetLength();
            }
            #endregion

            #region Attributi
            public string Credenziali
            {
                get { return this._cred; }
                set { this._cred = value; }
            }

            public int Length
            {
                get { return this._length; }
            }
            #endregion

            #region Object-Related
            public byte[] ToByteArray()
            {
                ByteConversionDelegate conv = new ByteConversionDelegate(ByteArray);
                return conv(this.ToString());
            }

            public override string ToString()
            {
                return Cred + this._cred + Cred;
            }

            private int GetLength()
            {
                return CredCount + this._cred.Length;
            }
            #endregion

            #region Conversioni
            #region Statico-UsoDelegato
            public static byte[] GetBytesFromString(string any)
            {
                ByteConversionDelegate conv = new ByteConversionDelegate(ByteArray);
                return conv(any);
            }

            public static byte[] GetBytesFromChar(char any)
            {
                return GetBytesFromString(any.ToString());
            }

            public static string GetStringFromBytes(byte[] any)
            {
                StringConversionDelegate conv = new StringConversionDelegate(GetString);
                return conv(any);
            }
            #endregion

            #region Statico-Delegato
            private static byte[] ByteArray(string par)
            {
                return Encoding.UTF8.GetBytes(par.ToString());
            }

            private static string GetString(byte[] par)
            {
                return Encoding.UTF8.GetString(par);
            }
            #endregion
            #endregion
        }

        public class Frame
        {
            private string _mitt, _dest, _mess;
            public const char Separatore = '\u01C0', End = '\u01C2';
            public const int SepCount = 4;
            private int _length;

            public static bool IsValidFrame(Frame f)
            {
                if (IsValidFrame(f.ToString()))
                    return true;
                return false;
            }

            public static bool IsValidFrame(string f)
            {
                if (f.Count(elem => elem == Separatore) == SepCount)
                    return true;
                return false;
            }

            #region Delegati
            delegate byte[] ByteConversionDelegate(string par);
            delegate string StringConversionDelegate(byte[] par);
            #endregion

            #region Costruttori
            public Frame()
            {
                this._mitt = ""; this._dest = ""; this._mess = ""; this._length = SepCount;
            }

            public Frame(string rec)
            {
                string[] infos = rec.Split(Separatore).Where(elem => !string.IsNullOrEmpty(elem)).ToArray();
                _mitt = infos[0]; _dest = infos[1]; _mess = infos[2];

                this._length = GetLength();
            }

            public Frame(byte[] rec)
            {
                string[] infos = GetString(rec).Split(Separatore).Where(elem => !string.IsNullOrEmpty(elem)).ToArray();
                _mitt = infos[0]; _dest = infos[1]; _mess = infos[2];

                this._length = GetLength();
            }

            public Frame(string mittente, string destinatario, string messaggio)
            {
                this._mitt = mittente;
                this._dest = destinatario;
                this._mess = messaggio;
                this._length = GetLength();
            }
            #endregion

            #region Attributi
            public string Mittente
            {
                get { return this._mitt; }
                set { this._mitt = value; }
            }

            public int Length
            {
                get { return this._length; }
            }

            public string Destinatario
            {
                get { return this._dest; }
                set { this._dest = value; }
            }

            public string Messaggio
            {
                get { return this._mess; }
                set { this._mess = value; }
            }
            #endregion

            #region Object-Related
            public byte[] ToByteArray()
            {
                ByteConversionDelegate conv = new ByteConversionDelegate(ByteArray);
                return conv(this.ToString());
            }

            public string FormatWrite()
            {
                string ret = string.Format("From: {0} --> Messaggio: {1}\r\n", this._mitt, this._mess);
                return ret;
            }

            public override string ToString()
            {
                return Separatore + this._mitt + Separatore + this._dest + Separatore + this._mess + Separatore;
            }

            private int GetLength()
            {
                return SepCount + (this._mitt.Length + this._dest.Length + this._mess.Length);
            }
            #endregion

            #region Conversioni
            #region Statico-UsoDelegato
            public static byte[] GetBytesFromString(string any)
            {
                ByteConversionDelegate conv = new ByteConversionDelegate(ByteArray);
                return conv(any);
            }

            public static byte[] GetBytesFromChar(char any)
            {
                return GetBytesFromString(any.ToString());
            }

            public static string GetStringFromBytes(byte[] any)
            {
                StringConversionDelegate conv = new StringConversionDelegate(GetString);
                return conv(any);
            }
            #endregion

            #region Statico-Delegato
            private static byte[] ByteArray(string par)
            {
                return Encoding.UTF8.GetBytes(par.ToString());
            }

            private static string GetString(byte[] par)
            {
                return Encoding.UTF8.GetString(par);
            }
            #endregion
            #endregion
        }
    }

    namespace Client
    {
        using Frame;

        public delegate void ConnectedEventHandler(object sender, IPEndPoint ipe);
        public delegate void SendMessageEventHandler(object sender, Frame f);
        public delegate void ReceiveMessageEventHandler(object sender, Frame f);
        public delegate void DisconnectedEventHandler(object sender, IPEndPoint ipe);

        public class Client
        {
            public event ConnectedEventHandler Connected;
            public event SendMessageEventHandler Sent;
            public event ReceiveMessageEventHandler Received;
            public event DisconnectedEventHandler Disconnected;

            private string _ip;
            private string _mitt;
            private int _porta;
            private Socket _sender = Generic.Socket(), _receiver;
            private IPEndPoint ipe;

            #region Costruttori
            public Client()
            {
                this._ip = "127.0.0.1";
                this._porta = 8080;
            }

            public Client(string ip, int porta)
            {
                this._ip = ip;
                this._porta = porta;
            }

            private void ClearAll()
            {
                this._ip = null;
                this._mitt = null;
                this._porta = -1;
                this._sender = null;
                this._receiver = null;
                this.ipe = null;
            }
            #endregion

            #region Attributi
            public string IP
            {
                get { return this._ip; }
                set { this._ip = value; }
            }

            public string Mittente
            {
                get { return this._mitt; }
                set { this._mitt = value; }
            }

            public int Porta
            {
                get { return this._porta; }
                set { this._porta = value; }
            }
            #endregion

            #region Connessione&S/R
            public void Connect()
            {
                ipe = new IPEndPoint(IPAddress.Parse(_ip), _porta);
                _sender.Connect(ipe);

                if (Connected != null)
                    Connected(this, ipe);

                SendCredentials();

                _receiver = _sender;
                StateObject stato = new StateObject();
                stato.WorkSocket = _receiver;
                _receiver.BeginReceive(stato.buffer, 0, stato.buffer.Length, 0, new AsyncCallback(GetData), stato);
            }

            public void Disconnect()
            {
                SendEnd();
                _sender.Close();
                _receiver.Close();

                if (Disconnected != null)
                    Disconnected(this, ipe);
            }

            public void SendCredentials()
            {
                CredentialFrame cred = new CredentialFrame(this._mitt);
                _sender.Send(cred.ToByteArray());
            }

            public void Dispose() { ClearAll(); }

            private void GetData(IAsyncResult res)
            {
                StateObject stato = (StateObject)res.AsyncState;
                Socket skt2 = stato.WorkSocket;
                int length = 0;
                try
                {
                    length = skt2.EndReceive(res);
                }
                catch (ObjectDisposedException)
                {
                    this.Dispose();
                    return;
                }
                catch (SocketException) { this.Disconnect(); return; }

                if (length > 0)
                    stato.FrameBuilder.Append(Frame.GetStringFromBytes(stato.buffer), 0, length);

                string rec = stato.FrameBuilder.ToString();

                if (Frame.IsValidFrame(rec))
                {
                    if (Received != null)
                        Received(this, new Frame(rec));

                    stato.buffer = new byte[StateObject.BufferSize];
                    stato.FrameBuilder = new StringBuilder();

                    skt2.BeginReceive(stato.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(GetData), stato);
                }
                else if (!rec.Contains(Frame.End))
                {
                    skt2.BeginReceive(stato.buffer, 0, StateObject.BufferSize, 0,
                       new AsyncCallback(GetData), stato);
                }
                else { this.Disconnect(); return; }
            }

            public void Send(string mittente, string destinatario, string messaggio)
            {
                Send(new Frame(mittente, destinatario, messaggio));
            }

            public void Send(Frame frame)
            {
                _sender.Send(frame.ToByteArray());

                if (Sent != null)
                    Sent(this, frame);
            }

            private void SendEnd()
            {
                _sender.Send(Frame.GetBytesFromChar(Frame.End));
            }
            #endregion
        }
    }

    namespace Server
    {
        using Frame;

        public delegate void ConnectedEventHandler(object sender, object[] infos);
        public delegate void RoutingEventHandler(object sender, Frame frm);
        public delegate void DisconnectedEventHandler(object sender, object[] infos);

        public class Server
        {
            public event ConnectedEventHandler Connected;
            public event RoutingEventHandler Routing;
            public event DisconnectedEventHandler Disconnected;

            private Thread EventSafe;

            private string _ip;
            private int _porta, _acceptreq;
            private Socket _skt = Generic.Socket();
            private IPEndPoint ipe;
            private ManualResetEvent mre = new ManualResetEvent(false);
            private Dictionary<string, Socket> dizionario = new Dictionary<string, Socket>();

            private void EventSafer()
            {
                while (!(Connected != null && Routing != null && Disconnected != null)) ;
                SetIPE();
            }

            #region Costruttori
            public Server()
            {
                this._ip = "127.0.0.1";
                this._porta = 8080;
                this._acceptreq = 10;

                ThreadColl();
            }

            private void ThreadColl()
            {
                EventSafe = new Thread(new ThreadStart(EventSafer));
                EventSafe.Start();
            }

            public Server(string ip, int porta)
            {
                this._ip = ip;
                this._porta = porta;
                this._acceptreq = 10;

                ThreadColl();
            }

            public Server(string ip, int porta, int acceptedrequests)
            {
                this._ip = ip;
                this._porta = porta;
                this._acceptreq = acceptedrequests;

                ThreadColl();
            }
            #endregion

            #region Attributi
            public string IP
            {
                get { return this._ip; }
                set { this._ip = value; }
            }

            public int Porta
            {
                get { return this._porta; }
                set { this._porta = value; }
            }

            public int AcceptRequests
            {
                get { return this._acceptreq; }
                set { this._acceptreq = value; }
            }
            #endregion

            #region Async
            private void SetIPE()
            {
                ipe = new IPEndPoint(IPAddress.Parse(this._ip), this._porta);
                _skt.Bind(ipe);
                _skt.Listen(this._acceptreq);
                while (true)
                {
                    mre.Reset();
                    _skt.BeginAccept(new AsyncCallback(Accept), this._skt);
                    mre.WaitOne();
                }
            }

            private void Accept(IAsyncResult res)
            {
                mre.Set();
                Socket skt1, skt2;

                try
                {
                    skt1 = (Socket)res.AsyncState;
                }
                catch (ObjectDisposedException)
                {
                    skt1 = Generic.Socket();
                }

                skt2 = skt1.EndAccept(res);

                StateObject stato = new StateObject();
                stato.WorkSocket = skt2;
                skt2.BeginReceive(stato.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(GetData), stato);
            }

            private void GetData(IAsyncResult res)
            {
                StateObject stato = (StateObject)res.AsyncState;
                Socket skt2 = stato.WorkSocket;

                int length = skt2.EndReceive(res);

                stato.FrameBuilder = new StringBuilder();

                if (length > 0)
                    stato.FrameBuilder.Append(Frame.GetStringFromBytes(stato.buffer), 0, length);

                string rec = stato.FrameBuilder.ToString();

                if (Frame.IsValidFrame(rec))
                {
                    Frame frm = new Frame(rec);

                    byte[] byteData = frm.ToByteArray();
                    skt2.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(RouteSend), stato);
                }
                else if (rec.Contains(Frame.End))
                {
                    string key = dizionario.Where(elem => elem.Value == skt2).Select(elem => elem.Key).ToArray()[0];

                    if (Disconnected != null)
                        Disconnected(this, new object[] { key, skt2 });

                    dizionario.Remove(key);
                    skt2.Close();
                }
                else if (CredentialFrame.IsValidCredentialFrame(rec))
                {
                    CredentialFrame cred = new CredentialFrame(rec);
                    dizionario.Add(cred.Credenziali, stato.WorkSocket);

                    if (Connected != null)
                        Connected(this, new object[] { cred.Credenziali, stato.WorkSocket });

                    skt2.BeginReceive(stato.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(GetData), stato);
                }
                else
                {
                    skt2.BeginReceive(stato.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(GetData), stato);
                }
            }

            private void RouteSend(IAsyncResult res)
            {
                StateObject stato = (StateObject)res.AsyncState;
                Socket worker = (Socket)stato.WorkSocket;
                int bytesSent = worker.EndSend(res);

                Frame frame = new Frame(stato.buffer);

                if (Routing != null)
                    Routing(this, frame);

                Socket to = dizionario.Where(elem => elem.Key == frame.Destinatario).Select(elem => elem.Value).ToArray()[0];

                stato.FrameBuilder = new StringBuilder();
                stato.buffer = new byte[StateObject.BufferSize];

                to.Send(frame.ToByteArray());

                worker.BeginReceive(stato.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(GetData), stato);
            }
            #endregion
        }
    }
}