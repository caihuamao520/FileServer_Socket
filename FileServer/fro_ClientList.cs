using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace FileServer
{
    public partial class fro_ClientList : Form
    {
        private Dictionary<string, Socket> _clientSocket;
        public fro_ClientList(Dictionary<string, Socket> client)
        {
            _clientSocket = client;
            InitializeComponent();
        }

        private void fro_ClientList_Load(object sender, EventArgs e)
        {
            if (_clientSocket != null)
            {
                this.listBox1.Items.Clear();
                foreach(string fkc in _clientSocket.Keys)
                {
                    this.listBox1.Items.Add(fkc);
                }

                this.groupBox1.Text = string.Format("用户列表,共{0}个", _clientSocket.Count);
            }
        }
    }
}
