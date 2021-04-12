using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace XmlFileReader.NOC
{
    public abstract class XML
    {
        private string _filePath { get; set; }
        private StreamWriter _sw { get; set; }

        protected abstract Func<XmlReader, StringBuilder,Task> Implement { get; }

        public XML(string readFrom, string saveTo)
        {
            if (!File.Exists(readFrom))
            {
                throw new ArgumentException("File does not exist");
            }
            _filePath = readFrom;

            FileStream fs = new FileStream(saveTo, FileMode.Append, FileAccess.Write, FileShare.None);
            _sw = new StreamWriter(fs, Encoding.UTF8);
            _sw.AutoFlush = true;
        }


        public async Task ReadFile()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            try
            {
                using (FileStream fs = File.OpenRead(_filePath))
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.Async = true;

                    using (XmlReader reader = XmlReader.Create(fs, settings))
                    {
                        await Implement(reader,stringBuilder);                        
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
        protected async Task AppendNewLine(string line)
        {
            await _sw.WriteLineAsync(line);
        }

        protected async Task CloseStream()
        {
            _sw.Close();
            await _sw.DisposeAsync();
        }
    }
}
