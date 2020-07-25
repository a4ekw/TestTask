using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public class JsonHelper
    {
        public string Read(string fileName, string dir)
        {
            string jsonresult;
            string path = Path.Combine(
                Directory.GetCurrentDirectory(), dir, fileName);

            using (StreamReader sr = new StreamReader(path))
            {
                jsonresult = sr.ReadToEnd();
            }

            return jsonresult;
        }

        public void Write(string fileName, string dir, string stringToJson)
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(), dir, fileName);

            using (var sw = File.CreateText(path))
            {
                sw.Write(stringToJson);
            }
        }
    }
}
