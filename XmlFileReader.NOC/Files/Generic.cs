using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using XmlFileReader.NOC.Interfaces;

namespace XmlFileReader.NOC.Files
{
    public class Generic : XML, IFileManager
    {
        protected override Func<XmlReader, StringBuilder,Task> Implement => ImplementReader;

        public Generic(string readFrom, string saveTo = @"../Generic.txt") : base(readFrom,saveTo)
        {
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
            // Finished Read
            await CloseStream();
        }        

    }
}
