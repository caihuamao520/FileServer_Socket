using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketLib
{
    internal interface IGetFileInfor<T>
    {
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        T GetFileInfor(string filePath);
    }
}
