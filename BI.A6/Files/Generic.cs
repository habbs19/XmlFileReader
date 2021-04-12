using BI.A6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace BI.A6.Files
{
    public class Generic : XML, IFileManager
    {
        private string _filePath { get; set; }
        private StreamWriter _sw { get; set; }
        public override Func<XmlReader, StringBuilder,Task> Implement => ImplementReader;

        public Generic(string filePath) : base(filePath)
        {
            FileStream fs = new FileStream("../Generic.txt",FileMode.Append, FileAccess.Write, FileShare.None);
            _sw = new StreamWriter(fs, Encoding.UTF8);
            _sw.AutoFlush = true;
        }

        private async Task ImplementReader(XmlReader reader, StringBuilder stringBuilder)
        {
            while (await reader.ReadAsync())
            {

                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.HasAttributes)
                        {
                            var r = reader.GetAttribute("concept");
                            switch (r)
                            {
                                case "GEO":
                                {
                                    reader.MoveToNextAttribute();
                                    stringBuilder.Append($"{reader.GetAttribute("value")},");
                                    break;
                                }
                                case "Sex":
                                {
                                    reader.MoveToNextAttribute();
                                    stringBuilder.Append($"{reader.GetAttribute("value")},");
                                    break;
                                }
                                case "AGEGR5":
                                {
                                    reader.MoveToNextAttribute();
                                    stringBuilder.Append($"{reader.GetAttribute("value")},");
                                    break;
                                }
                                case "NOC2011":
                                {
                                    reader.MoveToNextAttribute();
                                    stringBuilder.Append($"{reader.GetAttribute("value")},");
                                    break;
                                }
                                case "COWD":
                                {
                                    reader.MoveToNextAttribute();
                                    stringBuilder.Append($"{reader.GetAttribute("value")}");

                                    await AppendNewLine(stringBuilder.ToString());
                                    stringBuilder.Clear();
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            // Done reading 
            await CloseFile();

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

    }
}
