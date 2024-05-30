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
            Pairs = new Dictionary<string, List<string>>();
        }

        private Dictionary<string, List<string>> ReadFromFile()
        {
            using(StreamReader sr = new StreamReader(Path))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
            return Pairs;
        }

        public void WriteToFile()
        {
            string text = "";
            foreach (var pair in Pairs)
            {
                text += $"{pair.Key} - {string.Join(", ", pair.Value)} \n";
            }
            using (StreamWriter sw = new StreamWriter(Path, false))
            {
                sw.WriteLine(text);
            }
        }

        public void Add(string key, List<string> values)
        {
            Pairs.Add(key, values);
        }

        private void AddRange(string word, List<string> definitions)
        {
            if (Pairs.ContainsKey(word))
            {
                Pairs[word].AddRange(definitions);
            }
            else
            {
                Pairs[word] = definitions;
            }
        }

        public void Add()
        {
            Console.Write($"Enter new word in {KeyLanguage}: ");
            string word = Console.ReadLine();
            Console.Write($"Enter the definitions in {ValueLanguage} (split by ', '): ");
            string value = Console.ReadLine();
            List<string> words = value.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
            if (Pairs.ContainsKey(word))
            {
                AddRange(word, words);
            }
            else
            {
                Add(word, words);
            }
            Pairs = Pairs.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            WriteToFile();
            ReadFromFile();
        }

        public override string ToString()
        {
            string res = "";
            foreach (var pair in Pairs)
            {
                res += pair.Key + " - ";
                res += string.Join(", ", pair.Value);
                res += "\n";
            }
            return res.ToString();
        }
    }
}
