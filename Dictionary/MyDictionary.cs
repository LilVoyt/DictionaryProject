using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class MyDictionary
    {
        public string KeyLanguage { get; set; }
        public string ValueLanguage { get; set; }
        public string Path { get; set; }
        public Dictionary<string, List<string>> Pairs { get; set; }
        public MyDictionary(string keyLanguage, string valueLanguage)
        {
            KeyLanguage = keyLanguage;
            ValueLanguage = valueLanguage;
            Path = $"{this.KeyLanguage}-{this.ValueLanguage} dictionary.txt";
            WriteToFile();
            Pairs = ReadFromFile() ?? new Dictionary<string, List<string>>();
        }

        private Dictionary<string, List<string>> ReadFromFile()
        {
            using(StreamReader sr = new StreamReader(Path))
            {
                Pairs = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(sr.ReadToEnd()) ?? new Dictionary<string, List<string>>();
            }
            return Pairs;
        }

        public void WriteToFile()
        {
            string json = JsonConvert.SerializeObject(Pairs);

            using (StreamWriter sw = new StreamWriter(Path))
            {
                sw.WriteLine(json);
            }
        }

        public void Add(string key, List<string> values)
        {
            Pairs = ReadFromFile();
            Pairs.Add(key, values);
            WriteToFile();
        }
    }
}
