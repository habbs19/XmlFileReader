using BI.A6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BI.A6.Files
{
    public class Geography : IFileManager
    {
        private string _filePath { get; set; }
        private StreamWriter _sw { get; set; }

        public Geography()
        {
            FileStream fs = new FileStream("../Geography.txt",FileMode.Append, FileAccess.Write, FileShare.None);
            _sw = new StreamWriter(fs, Encoding.UTF8);
            _sw.AutoFlush = true;
        }


        public async Task AppendNewLine(string line)
        {
            await _sw.WriteLineAsync(line);
        }

        public async Task CloseFile()
        {
            _sw.Close();
            await _sw.DisposeAsync();

        }

        public Task ReadFile()
        {
            throw new NotImplementedException();
        }
    }
}
