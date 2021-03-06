using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlFileReader.NOC.Files
{
    public class Age : XML
    {
        protected override Func<XmlReader, Task> Implement => ImplementReader;

        public Age(string readFrom, string saveTo = @"../Age.txt") : base(readFrom, saveTo)
        {
        }

        private async Task ImplementReader(XmlReader reader)
        {
            await ImplementGeneral(reader, "CL_AGEGR5");
        }

    }
}
