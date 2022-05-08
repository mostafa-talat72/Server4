using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Server4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectBTN_Click(object sender, EventArgs e)
        {
            ConnectBTN.Enabled = false;
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            newsock.Bind(ipep);
            InfoTxt.Text += "Waiting for a client...\n";
            IPEndPoint sende = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sende);
            recv = newsock.ReceiveFrom(data, ref Remote);
            InfoTxt.Text += "Message received from " + Remote.ToString() + ":\n";
            InfoTxt.Text += Encoding.ASCII.GetString(data, 0, recv) + "\n";
            IPTxt.Text = "127.0.0.1";
            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref Remote);
                InfoTxt.Text += Encoding.ASCII.GetString(data, 0, recv) + "\n";
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
            ConnectBTN.Enabled = true;
        }
    }
}