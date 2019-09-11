using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace FileServer
{
    public partial class fro_Main : Form
    {
        private Socket socketWatch;
        private Thread threadWatch;
        private string _SaveDirectory = string.Empty;
        public fro_Main()
        {
            InitializeComponent();
        }

        private void fro_Main_Load(object sender, EventArgs e)
        {
            _SaveDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecFile");
            if(!Directory.Exists(_SaveDirectory))
            {
                Directory.CreateDirectory(_SaveDirectory);
                ShowMesg("创建了‘RecFile’文件接收目录。");
            }
            this.txtSaveDirectory.Text = _SaveDirectory;
        }

        private Dictionary<string, Socket> dict;//通信套接字 集合
        private Dictionary<string, Thread> dictThread;//通信线程集合
        private void btnEditState_Click(object sender, EventArgs e)
        {
            try
            {
                dict = new Dictionary<string, Socket>();
                dictThread = new Dictionary<string, Thread>();
                // 创建负责监听的套接字，注意其中的参数；  
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipAdd = IPAddress.Parse(this.txtIP.Text);
                IPEndPoint iPoit = new IPEndPoint(ipAdd, int.Parse(this.txtPoint.Text));
                socketWatch.Bind(iPoit);//绑定IP

                this.groupBox1.Enabled = false;
                socketWatch.Listen(10);

                //创建一个监听线程
                ThreadStart ts = new ThreadStart(ThreadSocketWatchFunction);
                threadWatch = new Thread(ts);
                threadWatch.IsBackground = true;
                threadWatch.Start();                
                ShowMesg("服务器启动监听成功！");
                this.btnEditState.Enabled = false;
            }
            catch (Exception ee)
            {
                ShowMesg("服务器启动监听失败，异常信息：" + ee.Message);
                MessageBox.Show(ee.Message, "异常");
            }
        }

        private delegate void DisplayContent(string content);
        //private DisplayContent _displayContent;
        private void ThreadSocketWatchFunction()
        {
            DisplayContent _displayContent = new DisplayContent(ShowMesg);
            while (true)
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；  
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);//添加一个客户端连接
                this.Invoke(_displayContent, new object[] { string.Format("发现新的客户端连接，新客户端IP地址：{0}",sokConnection.RemoteEndPoint) });

                ParameterizedThreadStart pts = new ParameterizedThreadStart(ThreadRecmsgFunction);

                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。  
            }
        }
        
        private void ThreadRecmsgFunction(object obj)
        {
            Socket sokClient = obj as Socket;
            DisplayContent _displayContent = new DisplayContent(ShowMesg);

            while (true)
            {
                // 定义一个2M的缓存区；  
                byte[] arrMsgRec = new byte[1024 * 1024 * 20];
                // 将接受到的数据存入到输入  arrMsgRec中；  
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；  
                }
                catch (SocketException se)
                {
                    //ShowMsg("异常：" + se.Message);
                    this.Invoke(_displayContent, new object[] { string.Format("'{0}'异常，异常信息：{1}", sokClient.RemoteEndPoint,se.Message) });

                    // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；  
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP  
                    this.Invoke(_displayContent, new object[] { string.Format("从列表中移除被中断的IP:'{0}'", sokClient.RemoteEndPoint) });
                    break;
                }
                catch (Exception e)
                {
                    this.Invoke(_displayContent, new object[] { string.Format("'{0}'异常，异常信息：{1}", sokClient.RemoteEndPoint, e.Message) });
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；  
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；  
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP  
                    this.Invoke(_displayContent, new object[] { string.Format("从列表中移除被中断的IP:'{0}'", sokClient.RemoteEndPoint) });
                    break;
                }
                if (arrMsgRec[0] == 0)  // 表示接收到的是数据；  
                {
                    string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 1, length - 1);// 将接受到的字节数据转化成字符串；  
                    //ShowMsg(strMsg);
                    this.Invoke(_displayContent, new object[] { string.Format("从IP:'{0}'收到信息，内容为：{1}", sokClient.RemoteEndPoint, strMsg) });
                }
                else if (arrMsgRec[0] == 1) // 表示接收到的是文件信息
                {

                }
                else if (arrMsgRec[0] == 2) // 表示接收到的是文件；  
                {
                    int i = 0;
                    string strNewFileNamePath = string.Empty;
                    do
                    {
                        i++;
                        strNewFileNamePath = Path.Combine(_SaveDirectory, getFileName(i.ToString()));
                    }
                    while (File.Exists(strNewFileNamePath));

                    using (FileStream fs = new FileStream(strNewFileNamePath, FileMode.OpenOrCreate))
                    {
                        fs.Write(arrMsgRec, 1, length - 1);
                        fs.Close();
                    }

                    this.Invoke(_displayContent, new object[] { string.Format("从IP:'{0}'收到文件流，内容保存成功。", sokClient.RemoteEndPoint) });
                }
                else if (arrMsgRec[0] == 3) // 表示接收到的是文件传输结束
                {
                    //Path.GetExtension
                }
                else
                {
                    this.Invoke(_displayContent, new object[] { string.Format("从IP:'{0}'收到无法解析的文件头标记，代码：{0}", arrMsgRec[0]) });
                }

            }
        }

        private string getFileName(string fileName)
        {
            StringBuilder sb = new StringBuilder(fileName);

            int iLenth = 8;
            int fileLenth = fileName.Length;

            for (; fileLenth < iLenth; fileLenth++)
            {
                sb.Insert(0, "0");
            }

            return sb.ToString();
        }

        private void ShowMesg(string content)
        {
            this.rtxtLog.Text = string.Format("时间：{0}  {1}\r\n\r\n{2}", DateTime.Now.ToString(), content, this.rtxtLog.Text);
            this.linkClientCout.Text = string.Format("当前共有{0}个客户端保持连接", dict.Count);
        }

        private void linkClientCout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fro_ClientList fcl = new fro_ClientList(dict);
            fcl.ShowDialog();
        }
    }
}
