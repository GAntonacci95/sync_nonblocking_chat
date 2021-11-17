using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AsyncLibrary;
using AsyncLibrary.Client;
using AsyncLibrary.Frame;
using System.Net.Sockets;
using System.Net;

namespace LatoClient
{
    public partial class Form1 : Form
    {
        static Client client;

        public Form1()
        {
            InitializeComponent();
            txtGen.Enabled = false;
            this.AcceptButton = btnInvia;
        }

        private void GB(bool what)
        {
            txtIP.Enabled = !what;
            txtPorta.Enabled = !what;
            btnConnect.Enabled = !what;
            txtMitt.Enabled = !what;

            btnDisconnect.Enabled = what;
            txtGen.Enabled = what;
            txtMess.Enabled = what;
            btnInvia.Enabled = what;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new Client();
            string[] cred = {txtIP.Text, txtMitt.Text, txtPorta.Text};
            client.Connected += new ConnectedEventHandler(Connected);
            client.Disconnected += new DisconnectedEventHandler(Disconnected);
            client.Received += new ReceiveMessageEventHandler(Receive);
            client.Sent += new SendMessageEventHandler(Send);

            client.IP = string.IsNullOrEmpty(cred[0]) ? Generic.IP : cred[0];
            int temp = 0;
            Int32.TryParse(cred[2], out temp);
            client.Porta = temp==0 ? Generic.Porta : temp;
            client.Mittente = cred[1];

            client.Connect();
        }

        private void Receive(object sender, Frame f)
        {
            if(isForMe(f))
                txtGen.Invoke(new MethodInvoker(delegate{txtGen.AppendText(f.FormatWrite());}));
        }

        private void Send(object sender, Frame f)
        {
            txtGen.AppendText("IO: "+txtMess.Text+"\r\n");
            txtMess.Clear();
        }

        private void Connected(object sender, IPEndPoint ipe){GB(true);}

        private void Disconnected(object sender, IPEndPoint ipe){GB(false);}

        private void btnDisconnect_Click(object sender, EventArgs e){client.Disconnect();}

        private void Form1_Load(object sender, EventArgs e){GB(false);}

        private bool Needed(string[] info)
        {
            if (!string.IsNullOrEmpty(info[0]) && !string.IsNullOrEmpty(info[1]))
                return true;
            return false;
        }

        private void btnInvia_Click(object sender, EventArgs e)
        {
            string[] infos = { txtDest.Text, txtMess.Text };
            if (Needed(infos))
            {
                Frame frame = new Frame(txtMitt.Text, infos[0], infos[1]);
                client.Send(frame);
            }
            else
            {
                MessageBox.Show("Inserire Destinatario e/o messaggio");
            }
        }

        private bool isForMe(Frame f)
        {
            if (client.Mittente == f.Destinatario)
                return true;
            return false;
        }
    }
}
