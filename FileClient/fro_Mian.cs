using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SocketLib;

namespace FileClient
{
    public partial class fro_Mian : Form
    {
        private Socket _MainSendMsg;
        public fro_Mian()
        {
            InitializeComponent();
        }

        private void fro_Mian_Load(object sender, EventArgs e)
        {

        }

        private void btnEditState_Click(object sender, EventArgs e)
        {
            try
            {
                string strIP = this.txtIP.Text;
                string strPoint = this.txtPoint.Text;

                IPAddress ipadd = IPAddress.Parse(strIP);
                IPEndPoint ipPoint = new IPEndPoint(ipadd, int.Parse(strPoint));

                _MainSendMsg = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _MainSendMsg.Connect(ipPoint);//建立与远程主机连接
                MessageBox.Show("连接成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.groupBox1.Enabled = false;
                this.btnEditState.Enabled = false;
            }
            catch (Exception ee)
            {
                _MainSendMsg.Close();
                _MainSendMsg.Dispose();
                _MainSendMsg = null;
                MessageBox.Show(ee.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = false;
            string strSendContent = this.txtSendContent.Text;
            if (string.IsNullOrWhiteSpace(strSendContent)) return;

            if (_MainSendMsg!=null)
            {
                byte[] buferContent = Encoding.UTF8.GetBytes(strSendContent);
                byte[] buferSendContent = new byte[buferContent.Length + 1];
                buferSendContent[0] = 0;//表示发送消息

                Buffer.BlockCopy(buferContent, 0, buferSendContent, 1, buferContent.Length);

                _MainSendMsg.Send(buferSendContent);

                if (this.ckClerContent.Checked)
                {
                    this.txtSendContent.Text = "";
                    this.txtSendContent.Focus();
                }
            }

            this.groupBox2.Enabled = true;
        }

        private void linkUpNetFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //if (_MainSendMsg == null) return;

            this.linkUpNetFile.Enabled = false;

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.AddExtension = false;
                ofd.Filter = "所有文件|*.*";
                ofd.FilterIndex = 0;
                ofd.Multiselect = false;
                ofd.Title = "选择一个需要上传的文件";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //获取文件信息
                    SocketFileInfor afi = new SocketFileInfor();
                    afi = afi.GetFileInfor(ofd.FileName);

                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                    int iFileMaxSize = 1024 * 1024 * 20;//文件缓存区大小
                    if (fs.Length < iFileMaxSize)
                    {
                        //发送文件信息

                        //发送文件内容
                        byte[] buferFS = new byte[iFileMaxSize];
                        int iReadCount = fs.Read(buferFS, 0, iFileMaxSize);
                        byte[] buferSendContent = new byte[iReadCount + 1];
                        buferSendContent[0] = 2;
                        
                        Buffer.BlockCopy(buferFS, 0, buferSendContent, 1, iReadCount);
                        _MainSendMsg.Send(buferSendContent);

                        //告知服务端内容发送完成
                    }
                    else
                    {
                        MessageBox.Show("选择的文件大于20MB因此无法上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.linkUpNetFile.Enabled = true;
        }
    }
}
