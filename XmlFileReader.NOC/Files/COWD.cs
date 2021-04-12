using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlFileReader.NOC.Files
{
    public class COWD : XML
    {
        protected override Func<XmlReader, Task> Implement => ImplementReader;

        public COWD(string readFrom, string saveTo = @"../COWD.txt") : base(readFrom, saveTo)
        {
        }

        private async Task ImplementReader(XmlReader reader)
        {
            await ImplementGeneral(reader, "CL_COWD");
        }
    }
}
