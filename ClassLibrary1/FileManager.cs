using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IFileManager
    {
        string GetContent(string filePath);
        string GetContent(string filePath, Encoding ecoding);
        void SaveContent(string content, string filePath);
        void SaveContent(string content, string filePath, Encoding ecoding);
        bool IsExist(string filePath);
    }
    public class FileManager : IFileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        public bool IsExist(string filePath)
        {
            bool IsExist = File.Exists(filePath);
            return IsExist;
        }
        public string GetContent(string filePath)
        {
            return GetContent(filePath, _defaultEncoding);
        }
        public string GetContent(string filePath, Encoding encoding)
        {
            string content = File.ReadAllText(filePath, encoding);
            return content;
        }
        public void SaveContent(string content, string filePath)
        { SaveContent(content, filePath, _defaultEncoding); }
        //content="Шапка\r\n"+ 
        //content
        public void SaveContent(string content, string filePath, Encoding encoding)
        { File.WriteAllText(filePath, content, encoding); }

    }
}
