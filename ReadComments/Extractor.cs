using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReadComments
{
    public class Extractor
    {
        #region Variables
        private string path; // path of txt file with comments
        private string sourcePath;
        private List<List<string>> comments; // List<List<string>> to make it easier to print when the comments are multiline
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor takes a path where do you want your txt file to be and also creates a file
        /// </summary>
        public Extractor(string path)
        {
            this.path = path + "\\Comments.txt";
            var fs = File.Create(this.path);
            fs.Close();

        }
        #endregion
        #region Extract
        /// <summary>
        /// Method used to get all comments from one file and write them to the comments.txt file.
        /// To make it simpler I added 3 methods. 
        /// </summary>
        public void Extract(string sourcePath)
        {
            comments = new List<List<string>>();
            this.sourcePath = sourcePath;
            WriteHeader(); 
            FindComments();
            WriteComents();
        }
        #endregion
        #region WriteComments
        /// <summary>
        /// Writes Comments to comment txt file
        /// </summary>
        private void WriteComents()
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                int i = 1;
                bool status = true; 
                foreach (var list in comments)
                {
                    status = true;
                    foreach ( string temp in list)
                    {
                        if (status)
                        {
                          sw.WriteLine(i + ". " + temp);
                          status = false;
                        }
                        else
                        {
                            sw.WriteLine(temp);
                        }
                    }
                    i++;
                }
            }
        }
        #endregion
        #region FindComments
        /// <summary>
        /// Finds comments and put them into the comment list.
        /// Also trying to escape some common problems like @"//comment", "//comment",""ads//asdads"", and so on
        /// </summary>


        private void FindComments()
        {
            using (StreamReader sr = new StreamReader(sourcePath, true))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("@\""))
                    {
                        int index = line.IndexOf("@\"");
                        line = line.Substring(index+2, line.Length - (index+2));


                        if (line.Contains("\""))
                        {
                            int index1 = line.LastIndexOf("\"");

                            line = line.Substring(index1 + 1, line.Length - (index1 + 1));
                            if (line.Contains(@"//"))
                            {
                                List<string> temp = new List<string>();
                                temp.Add(line);
                                comments.Add(temp);
                            }
                        }
                        else
                        {
                            while (!line.Contains("\"") && line != null)
                            {
                                line = sr.ReadLine();
                            }
                            int index2 = line.IndexOf("\"");
                            line = line.Substring(index2 + 1, line.Length - index2 - 1);
                            if (line.Contains(@"//"))
                            {
                                int index3 = line.IndexOf(@"//");
                                line = line.Substring(index3, line.Length - index3);
                                List<string> temp = new List<string>();
                                temp.Add(line);
                                comments.Add(temp);
                            }
                            else
                            {
                                line = sr.ReadLine();
                            }
                            
                        }
                    }
                    else if (line.Contains(@"//"))
                    {
                        if (!line.Contains("\"//"))
                        {
                            List<string> temp = new List<string>();
                            temp.Add(line);
                            comments.Add(temp);

                        }
                    }
                    else if (line.Contains(@"/*"))
                    {

                        if (line.Contains(@"*/"))
                        {

                            string starttext = "/*";
                            string endtext = "*/";

                            var start = line.IndexOf(starttext);
                            var end = line.LastIndexOf(endtext) - (start - starttext.Length);
                            line = line.Substring(start, end);

                            List<string> temp = new List<string>();
                            temp.Add(line);
                            comments.Add(temp);
                        }
                        else
                        {
                            List<string> subList = new List<string>();

                            string starttext = "/*";

                            var start = line.IndexOf(starttext);

                            line = line.Substring(start, line.Length - start);
                            subList.Add(line);
                            
                            while (!line.Contains(@"*/") && line != null)
                            {
                                line = sr.ReadLine();
                                
                                subList.Add(line);
                            }
                            comments.Add(subList);

                        }



                    }

                 
                }
                    
                   
                    
                }
            }
        #endregion
        #region WriteHeader
        /// <summary>
        /// Writes a header to comment.txt file like ====filename.css====
        /// </summary>
        private void WriteHeader()
        {
           
            string fileName = Path.GetFileName(sourcePath);
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine("=====" + fileName + "====="); // Writes Header to txt file
            }
        }
        #endregion
    }
}
