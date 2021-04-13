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
    public class Generic : XML
    {
        protected override Func<XmlReader,Task> Implement => ImplementReader;

        public Generic(string readFrom, string saveTo = @"../Generic.txt") : base(readFrom,saveTo)
        {
        }

        private async Task ImplementReader(XmlReader reader)
        {
            StringBuilder stringBuilder = new StringBuilder();
            await AppendNewLine("Geo,Sex,AgeGroup,NOC2011,Value");

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
                                    // Not using data
                                    break;
                                }                                
                                default:
                                {
                                    if(reader.LocalName == "ObsValue")
                                    {
                                        if (reader.MoveToNextAttribute())
                                        {
                                            stringBuilder.Append($"{reader.GetAttribute("value")}");
                                            await AppendNewLine(stringBuilder.ToString());
                                            stringBuilder.Clear();
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Finished reading Data");
            await CloseStream();
        }        

    }
}
