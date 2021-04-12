using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using XmlFileReader.NOC.Interfaces;
using System.Xml;

namespace XmlFileReader.NOC.Files
{
    public class Geography : XML, IFileManager
    {
        protected override Func<XmlReader, StringBuilder, Task> Implement => ImplementReader;

        public Geography(string readFrom, string saveTo = @"../Geography.txt") : base(readFrom, saveTo)
        {
        }

        private async Task ImplementReader(XmlReader reader, StringBuilder stringBuilder)
        {

        }


    }
}
