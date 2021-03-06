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
    public class Geography : XML
    {
        protected override Func<XmlReader, Task> Implement => ImplementReader;

        public Geography(string readFrom, string saveTo = @"../Geography.txt") : base(readFrom, saveTo)
        {
        }

        private async Task ImplementReader(XmlReader reader)
        {
            await ImplementGeneral(reader, "CL_GEO");
        }
    }


}

