using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TextFile
{
    class WriteAFile
    {
        private static System.IO.StreamWriter file;

        internal static void WriteFile(string path, string fileName, string [] lines, bool append)
        {
            try
            {
                file = new System.IO.StreamWriter(path + "\\" + fileName, append);

                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
                file.Close();
            }
            catch 
            { 
                throw new Exception(); 
            }
        }
    }

    class ReadAFile
    {
        private static System.IO.StreamReader file;
        private static List<string> lines = new List<string>();

        private static string line = "";

        internal static string[] ReadFile(string path, string fileName)
        {
            try
            {
                file = new System.IO.StreamReader(path + "\\" + fileName);

                line = file.ReadLine();

                while (line != null)
                {
                    lines.Add(line);

                    line = file.ReadLine();
                }

                file.Close();
            }
            catch 
            { 
                throw new Exception(); 
            }
            return lines.ToArray();
        }
    }
    //Clase Innecesaria xD
    class CheckFile
    {
        internal static bool CheckIfFileExists(string path, string fileName)
        {
            if (System.IO.File.Exists(path + "\\" + fileName))
            {
                return true;
            }

            return false;
        }

        internal static void DeleteFile(string path, string fileName)
        {
            System.IO.File.Delete(path + "\\" + fileName);
        }
    }
}
