using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphawolfSoftware.OS
{
    public class FileIO
    {
        public static string LoadFile(string outputFolder, string filename)
        {
            string fullpath = Path.Combine(outputFolder, filename);
            string contents = "";

            try
            {
                using (StreamReader reader = new StreamReader(fullpath))
                {
                    contents = reader.ReadToEnd();
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

            return contents;
        }

        public static void SaveFile(string outputFolder, string filename, string contents)
        {
            string fullpath = Path.Combine(outputFolder, filename);

            try
            {
                using (StreamWriter writer = new StreamWriter(fullpath))
                {
                    writer.Write(contents);
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }
    }
}
