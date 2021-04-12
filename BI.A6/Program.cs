using BI.A6.Files;
using BI.A6.Interfaces;
using System;
using System.Threading.Tasks;

namespace BI.A6
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string dir = @"C:\Users\hs_m1\Downloads\99-012-X2011033";
            string filename = "Generic_99-012-X2011033.xml";

            IFileManager generic = new Generic($"{dir}\\{filename}");
            await generic.ReadFile();

        }

    }
}
