using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class ApiMenu
    {
        public ApiMenu()
        {
            ShowStart();
            ChooseDictionary();
        }
        public void ShowStart()
        {
            Console.WriteLine("Translate");
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void ChooseDictionary()
        {
            DictionaryList list = new DictionaryList();
            ShowAllDict(list);
            Console.Write("Choose an index: ");
            string input = Console.ReadLine();
            int choose;
            Thread.Sleep(1000);
            Console.Clear();

            if (int.TryParse(input, out choose) && choose >= 0 && choose < list.Dictionaries.Count)
            {
                //Console.WriteLine(list.Dictionaries[choose]);
                Console.WriteLine("What you want to do");
                Console.WriteLine("1 - add new word");
                Console.WriteLine("2 - Show word transcription");
                Console.WriteLine("3 - Show all words with transactions");
                Console.WriteLine("4 - Exit");
                int toDo = int.Parse(Console.ReadLine());
                switch (toDo)
                {
                    case 1:
                        AddNewWord(list.Dictionaries[choose]);
                        list.WriteToFile();
                        break;
                    case 2:
                        ShowExactTranscription(list.Dictionaries[choose]);
                        Thread.Sleep(1000);
                        Console.Clear();
                        ShowExactTranscription(list.Dictionaries[choose]); //change
                        break;
                    case 3:
                        ShowAllTransactions(list.Dictionaries[choose]);
                        break;
                    case 4:
                        Thread.Sleep(1000);
                        Console.Clear();
                        ChooseDictionary();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid index.");
            }
        }

        public void AddNewWord(MyDictionary dictionary)
        {
            Console.Write($"Enter new word in {dictionary.KeyLanguage}: ");
            string word = Console.ReadLine();
            Console.Write($"Enter the definitions in {dictionary.ValueLanguage} (split by ', '): ");
            string value = Console.ReadLine();
            List<string> words = value.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
            if (dictionary.Pairs.ContainsKey(word))
            {
                dictionary.Pairs[word].AddRange(words);
            }
            else
            {
                dictionary.Pairs.Add(word, words);
            }
            foreach(var pair in dictionary.Pairs)
            {
                Console.WriteLine($"{pair.Key} - {string.Join(", ", pair.Value)}");
            }
        }

        public void ShowExactTranscription(MyDictionary dictionary)
        {
            var allDictionaries = dictionary.Pairs
                .Select(pair => pair.Key);

            foreach (var transaction in allDictionaries)
            {
                Console.WriteLine(transaction);
            }

            Console.Write($"Write the word to find in {dictionary.KeyLanguage}: ");
            string word = Console.ReadLine();
            if (dictionary.Pairs.TryGetValue(word, out List<string> translations))
            {
                var exactDictionary = dictionary.Pairs.Select(pair => $"{pair.Key} - {string.Join(", ", pair.Value)}").ToList();
                Console.WriteLine($"{exactDictionary[0]}");
            }
            else
            {
                Console.WriteLine($"The word '{word}' was not found in the {dictionary.KeyLanguage}-{dictionary.ValueLanguage} dictionary.");
            }
        }

        public void ShowAllTransactions(MyDictionary dictionary)
        {
            var allTransactions = dictionary.Pairs
                .Select(pair => $"{pair.Key} - {string.Join(", ", pair.Value)}");

            foreach (var transaction in allTransactions)
            {
                Console.WriteLine(transaction);
            }
        }

        public void ShowAllDict(DictionaryList dictionaryList)
        {
            int count = 0;
            foreach (MyDictionary d in dictionaryList.Dictionaries)
            {
                Console.WriteLine($"[{count}] {d.KeyLanguage}-{d.ValueLanguage} Dictionary");
                count++;
            }
        }

        public void ShowDictByIndex(DictionaryList dictionaryList, int index)
        {
            if (index >= 0 && index < dictionaryList.Dictionaries.Count)
            {
                foreach (var pair in dictionaryList.Dictionaries[index].Pairs)
                {
                    Console.WriteLine($"{pair.Key} - {string.Join(", ", pair.Value)}");
                }
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

    }
}