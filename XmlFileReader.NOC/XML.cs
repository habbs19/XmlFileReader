using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using XmlFileReader.NOC.Interfaces;

namespace XmlFileReader.NOC
{
    public abstract class XML : IFileManager
    {
        private string _readFrom { get; set; }
        private string _saveTo { get; set; }
        private StreamWriter _sw { get; set; }

        protected abstract Func<XmlReader,Task> Implement { get; }

        public XML(string readFrom, string saveTo)
        {
            if (!File.Exists(readFrom))
            {
                throw new ArgumentException("File does not exist");
            }
            _readFrom = readFrom;
            _saveTo = saveTo;            
        }


        public async Task ReadFile()
        {
            FileStream fst = new FileStream(_saveTo, FileMode.Append, FileAccess.Write, FileShare.None);
            _sw = new StreamWriter(fst, Encoding.UTF8);
            _sw.AutoFlush = true;

            StringBuilder stringBuilder = new StringBuilder();
            
            try
            {
                using (FileStream fs = File.OpenRead(_readFrom))
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.Async = true;

                    using (XmlReader reader = XmlReader.Create(fs, settings))
                    {
                        await Implement(reader);                        
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await CloseStream();
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

        protected async Task ImplementGeneral(XmlReader reader, string id)
        {
            StringBuilder stringBuilder = new StringBuilder();

            reader.ReadToDescendant("CodeLists");

            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.HasAttributes)
                        {
                            if (reader.GetAttribute("id") == id)
                            {
                                var subTree = reader.ReadSubtree();
                                while (subTree.ReadToFollowing("structure:Code"))
                                {
                                    var value = reader.GetAttribute("value");
                                    stringBuilder.Append(value);

                                    reader.ReadToDescendant("structure:Description");
                                    await reader.ReadAsync();

                                    var desc = await reader.ReadContentAsStringAsync();
                                    stringBuilder.Append($";{desc.Trim()}");

                                    await AppendNewLine(stringBuilder.ToString());
                                    stringBuilder.Clear();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine($"Finished reading {id}");
            await CloseStream();
        }
    }
}
