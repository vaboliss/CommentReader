using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReadComments
{
    public static class Helper
    {
        #region FindCsFiles
        /// <summary>
        /// Finds all files that is .cs type
        /// </summary>
        /// <param name="path"> Folder name where the project is</param>
        /// <returns> return IEnumerable<string> with all the paths of .cs files</returns>
        public static IEnumerable<String> FindCsFiles(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".cs"));
           
            return files;
        }
        #endregion
    }
}
