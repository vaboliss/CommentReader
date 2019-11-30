using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReadComments
{
    public static class Helper
    {
        public static IEnumerable<String> FindCsFiles(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".cs"));
           
            return files;
        }
    }
}
