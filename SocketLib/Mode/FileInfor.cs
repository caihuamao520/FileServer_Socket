using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketLib
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfor:IGetFileInfor<FileInfor>
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// 文件的MD5值
        /// </summary>
        public string FileMD5 { get; set; }
        /// <summary>
        /// 文件格式（后缀名）
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件信息</returns>
        public FileInfor GetFileInfor(string filePath)
        {
            FileControl fc = new FileControl(filePath);
            FileInfor fif = new FileInfor()
            {
                FileName = fc.GetFileName(),
                FileExtension = fc.GetExtension(),
                FileMD5 = fc.GetFileMD5(),
                FileSize = fc.GetFileSize()
            };
            return fif;
        }
    }
}
