using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using BI.A6.Interfaces;
using BI.A6.Files;

namespace BI.A6
{
    public abstract class XML
    {
        private string _filePath { get; set; }

        public abstract Func<XmlReader, StringBuilder,Task> Implement { get; }

        public XML(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist");
            }
            _filePath = filePath;

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
    }
}
