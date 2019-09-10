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

namespace FileServer
{
    public partial class fro_Main : Form
    {
        private Socket socketWatch;
        private Thread threadWatch;
        public fro_Main()
        {
            InitializeComponent();
        }

        private void fro_Main_Load(object sender, EventArgs e)
        {

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
                byte[] arrMsgRec = new byte[1024 * 1024 * 2];
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
                    this.Invoke(_displayContent, new object[] { string.Format("从IP:'{0}'收到信息，内容为：{1}", sokClient.RemoteEndPoint,strMsg) });
                }
                if (arrMsgRec[0] == 1) // 表示接收到的是文件；  
                {
                    //SaveFileDialog sfd = new SaveFileDialog();

                    //if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    //{// 在上边的 sfd.ShowDialog（） 的括号里边一定要加上 this 否则就不会弹出 另存为 的对话框，而弹出的是本类的其他窗口，，这个一定要注意！！！【解释：加了this的sfd.ShowDialog(this)，“另存为”窗口的指针才能被SaveFileDialog的对象调用，若不加thisSaveFileDialog 的对象调用的是本类的其他窗口了，当然不弹出“另存为”窗口。】  

                    //    string fileSavePath = sfd.FileName;// 获得文件保存的路径；  
                    //    // 创建文件流，然后根据路径创建文件；  
                    //    using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
                    //    {
                    //        fs.Write(arrMsgRec, 1, length - 1);
                    //        ShowMsg("文件保存成功：" + fileSavePath);
                    //    }
                    //}

                    this.Invoke(_displayContent, new object[] { string.Format("从IP:'{0}'收到文件流，内容保存成功。", sokClient.RemoteEndPoint) });
                }

            }
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
