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
            string generic =  $"{dir}\\Generic_99-012-X2011033.xml";
            string structure = $"{dir}\\Structure_99-012-X2011033.xml";

            IFileManager genericFile = new Generic(generic);
            IFileManager geographyFile = new Geography(structure);
            IFileManager sexFile = new Sex(structure);
            IFileManager occupationFile = new Occupation(structure);
            IFileManager ageFile = new Age(structure);
            IFileManager cowdFile = new COWD(structure);

            await geographyFile.ReadFile();
            await sexFile.ReadFile();
            await occupationFile.ReadFile();
            await ageFile.ReadFile();
            await cowdFile.ReadFile();
            await genericFile.ReadFile();




        }

    }
}
