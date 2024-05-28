using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class DictionaryList
    {
        public List<MyDictionary> Dictionaries { get; set; }
        public DictionaryList()
        {
            Dictionaries = ReadFromFile() ?? new List<MyDictionary>();
        }

        public void AddDictionary(string keyLanguage, string valueLanguage)
        {
            ReadFromFile();
            Dictionaries.Add(new MyDictionary(keyLanguage, valueLanguage));
            WriteToFile();
        }

        public void WriteToFile()
        {
            string json = JsonConvert.SerializeObject(Dictionaries);
            using(StreamWriter sw = new StreamWriter("allDictionaries.txt", false))
            {
                sw.WriteLine(json);
            }
        }
        public List<MyDictionary> ReadFromFile()
        {
            if (!File.Exists("allDictionaries.txt"))
            {
                return new List<MyDictionary>();
            }

            using (StreamReader sr = new StreamReader("allDictionaries.txt"))
            {
                var content = sr.ReadToEnd();
                return string.IsNullOrWhiteSpace(content) ? new List<MyDictionary>() : JsonConvert.DeserializeObject<List<MyDictionary>>(content);
            }
        }


        public override string ToString()
        {
            string res = "";
            foreach(MyDictionary d in Dictionaries)
            {
                res += $"{d.KeyLanguage}-{d.ValueLanguage} Dictionary, ";
            }
            return res;
        }
    }
}
