using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTool
{
    /// <summary>
    /// File operation utility tool.
    /// </summary>
    public static class FileIOTool
    {
        /// <summary>
        /// 將 byte[] 轉成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 將 stream 轉成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 設置當前流的位置為流的開始
            stream.Seek(0, SeekOrigin.Begin); //same as stream.Position = 0;
            return bytes;
        }

        /// <summary>
        /// 將 Stream 寫入 指定File path
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 轉成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 設置當前流的位置為串流的開始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 寫入檔
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 從 File 讀取 Stream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            // 開檔
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 讀取檔的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 轉成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
