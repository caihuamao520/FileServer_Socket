using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SocketLib
{
    /// <summary>
    /// 套接字文件信息
    /// </summary>
    public class SocketFileInfor:FileInfor,IGetFileInfor<SocketFileInfor>
    {
        /// <summary>
        /// 客户端终结点地址
        /// </summary>
        public IPEndPoint SouceIPEndPoint { get; set; }
        /// <summary>
        /// 对象标识
        /// </summary>
        public object Tage { get; set; }
        /// <summary>
        /// 文件唯一码
        /// </summary>
        public string FileGUID { get; set; }

        public new SocketFileInfor GetFileInfor(string filePath)
        {
            FileControl fc = new FileControl(filePath);
            SocketFileInfor sfi = new SocketFileInfor();

            sfi.FileName = fc.GetFileName();
            sfi.FileExtension = fc.GetExtension();
            sfi.FileMD5 = fc.GetFileMD5();
            sfi.FileSize = fc.GetFileSize();
            //sfi.SouceIPEndPoint = null;
            //sfi.Tage = null;
            sfi.FileGUID = Guid.NewGuid().ToString("N"); 

            return sfi;
        }
    }
}
