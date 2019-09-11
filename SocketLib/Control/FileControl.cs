using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SocketLib
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class FileControl
    {
        private string _filePath = string.Empty;//操作文件路径
        private FileInfo fi = null;
        public FileControl(string filePath)
        {
            _filePath = filePath;
        }
        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            if (fi == null)
            {
                if (File.Exists(_filePath))
                {
                    fi = new FileInfo(_filePath);                    
                    return fi.Name;
                }
                else
                {
                    throw new Exception(string.Format("文件‘{0}’不存在", _filePath));
                }
            }
            else
            {
                return fi.Name;
            }
        }
        /// <summary>
        /// 获取文件格式（后缀名）
        /// </summary>
        /// <returns></returns>
        public string GetExtension()
        {
            if (fi == null)
            {
                if (File.Exists(_filePath))
                {
                    fi = new FileInfo(_filePath);
                    return fi.Extension;
                }
                else
                {
                    throw new Exception(string.Format("文件‘{0}’不存在", _filePath));
                }
            }
            else
            {
                return fi.Extension;
            }
        }
        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <returns></returns>
        public string GetFileMD5()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    using (FileStream file = new FileStream(_filePath, FileMode.Open))
                    {
                        MD5 md5 = new MD5CryptoServiceProvider();
                        byte[] retVal = md5.ComputeHash(file);
                        file.Close();

                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < retVal.Length; i++)
                        {
                            sb.Append(retVal[i].ToString("x2"));
                        }
                        return sb.ToString();
                    }
                        
                }
                catch (Exception ex)
                {
                    throw new Exception("获取文件MD5值错误，错误信息：" + ex.Message);
                }
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <returns></returns>
        public long GetFileSize()
        {
            if (fi == null)
            {
                if (File.Exists(_filePath))
                {
                    fi = new FileInfo(_filePath);
                    return fi.Length;
                }
                else
                {
                    throw new Exception(string.Format("文件‘{0}’不存在", _filePath));
                }
            }
            else
            {
                return fi.Length;
            }
        }
    }
}
