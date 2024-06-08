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

        public void ShowExactTranscription()
        {
            foreach (var pair in Pairs)
            {
                Console.WriteLine(pair.Key);
            }

            Console.Write($"Write the word to find in {KeyLanguage}: ");
            string word = Console.ReadLine();
            if (Pairs.TryGetValue(word, out List<string> translations))
            {
                Console.WriteLine($"{word} - {string.Join(", ", translations)}");
            }
            else
            {
                Console.WriteLine($"The word '{word}' was not found in the {KeyLanguage}-{ValueLanguage} dictionary.");
            }
        }

        public void ShowAllTranscription()
        {
            var allTransactions = Pairs
                .Select(pair => $"{pair.Key} - {string.Join(", ", pair.Value)}");

            foreach (var transaction in allTransactions)
            {
                Console.WriteLine(transaction);
            }
        }

        public void EditTranlations()
        {
            Console.WriteLine("Your keys: ");
            foreach (var pair in Pairs)
            {
                Console.WriteLine(pair.Key);
            }
            Console.Write($"Write the word to change translation in {KeyLanguage}: ");
            string word = Console.ReadLine();
            if (Pairs.TryGetValue(word, out List<string> translations))
            {
                string words = string.Join(", ", translations.ToArray());
                StringBuilder currentText = new StringBuilder(words);
                Console.Write(currentText.ToString());

                Console.SetCursorPosition(words.Length, Console.CursorTop);

                ApiMenu.EditText(currentText);

                Console.WriteLine("\nFinal text: " + currentText.ToString());

                List<string> strings = currentText.ToString().Split(new string[] { ", " }, StringSplitOptions.None).ToList();
                Pairs[word].Clear();
                Pairs[word].AddRange(strings);
            }
        }

        public void EditKey()
        {
            Console.WriteLine("Your keys: ");
            foreach (var pair in Pairs)
            {
                Console.WriteLine(pair.Key);
            }
            Console.Write($"Write the key to change in {KeyLanguage}: ");
            string word = Console.ReadLine();

            if (Pairs.TryGetValue(word, out List<string> translations))
            {
                Console.WriteLine($"Enter new key in {KeyLanguage}: ");
                string newKey = Console.ReadLine();

                Pairs.Remove(word);
                Pairs.Add(newKey, translations);
            }
        }

        public void DeleteWord()
        {
            Console.WriteLine($"Enter the key word to delete");
            string word = Console.ReadLine();

            Pairs.Remove(word);
            Console.WriteLine($"{word} was deleted");
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
