using System;
using System.Threading.Tasks;
using XmlFileReader.NOC.Files;
using XmlFileReader.NOC.Interfaces;

namespace XmlFileReader.NOC
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string dir = @"C:\Users\hs_m1\Downloads\99-012-X2011033";
            string generic = "Generic_99-012-X2011033.xml";
            string structure = "Structure_99-012-X2011033.xml";

            IFileManager genericFile = new Generic($"{dir}\\{generic}");
            IFileManager geographyFile = new Geography($"{dir}\\{structure}");
            
            await geographyFile.ReadFile();
            await genericFile.ReadFile();

        }

    }
}
