using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadComments
{
    class Program
    {
        public static String sourcePath = @""; // sourcePath is location where the project fies are.
        public static String path = @""; // path is the location where the txt file with all the comment will be 
        static void Main(string[] args)
        {

            IEnumerable<string> files = Helper.FindCsFiles(path);

            Extractor ext = new Extractor(path);
            foreach (String element in files)
            {
               // Console.WriteLine(element);
                ext.Extract(element);
            }

            

            

        }
    }
}
