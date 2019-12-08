using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadComments
{
    class Program
    {
        public static String sourcePath = @""; // sourcePath is location where the project files are.
        public static String path = @""; // path is the location where the txt file with all the comment will be 


        static void Main(string[] args)
        {

            IEnumerable<string> files = Helper.FindCsFiles(sourcePath); // gets all the .cs files

            Extractor ext = new Extractor(path); // Create Extractor object and give it location where you want your comment.txt file to be
            foreach (String element in files)
            {
                ext.Extract(element); // Iterate through all the paths and give them to Extract method
            }
        }
    }
}
