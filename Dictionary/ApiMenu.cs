using System;
using System.Collections;
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
            Console.WriteLine($"[{list.Dictionaries.Count}] Add Dictionary");
            Console.WriteLine($"[{list.Dictionaries.Count + 1}] Exit");
            Console.Write("Choose an index: ");
            string input = Console.ReadLine();
            int choose;
            Thread.Sleep(1000);
            Console.Clear();

            if (int.TryParse(input, out choose) && choose >= 0 && choose < list.Dictionaries.Count)
            {
                Console.WriteLine("What you want to do");
                Console.WriteLine("1 - Add new word");
                Console.WriteLine("2 - Show word transcription");
                Console.WriteLine("3 - Show all words with transactions");
                Console.WriteLine("4 - Change translation of word");
                Console.WriteLine("5 - Change key word");
                Console.WriteLine("6 - Delete word and definition");
                Console.WriteLine("7 - Exit");
                int toDo = int.Parse(Console.ReadLine());
                Thread.Sleep(1000);
                Console.Clear();
                switch (toDo)
                {
                    case 1:
                        AddNewWord(list.Dictionaries[choose]);
                        list.WriteToFile();
                        break;
                    case 2:
                        ShowExactTranscription(list.Dictionaries[choose]);
                        break;
                    case 3:
                        ShowAllTransactions(list.Dictionaries[choose]);
                        break;
                    case 4:
                        EditTranlations(list.Dictionaries[choose]);
                        list.WriteToFile();
                        break;
                    case 5:
                        EditKey(list.Dictionaries[choose]);
                        list.WriteToFile();
                        break;
                    case 6:
                        DeleteWord(list.Dictionaries[choose]);
                        break;
                    case 7:
                        Thread.Sleep(1000);
                        Console.Clear();
                        ChooseDictionary();
                        break;
                }
            }
            else if (int.TryParse(input, out choose) && choose >= 0 && choose == list.Dictionaries.Count)
            {
                Console.Write("Enter the key language: ");
                string keyLanguage = Console.ReadLine();
                Console.Write("Enter the value language: ");
                string valueLanguage = Console.ReadLine();
                list.AddDictionary(keyLanguage, valueLanguage);

                Console.WriteLine("Do you want to add something here (y - yes, q - quit)??");
                char isContinue = char.Parse(Console.ReadLine());
                if(isContinue == 'y')
                {
                    AddNewWord(list.Dictionaries[list.Dictionaries.Count - 1]);
                    list.WriteToFile();
                }
            }
            else if (int.TryParse(input, out choose) && choose >= 0 && choose == list.Dictionaries.Count + 1)
            {

            }
            else if (int.TryParse(input, out choose) && choose >= 0 && choose == list.Dictionaries.Count + 2)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid index.");
            }
        }

        public void DeleteDictionary()

        public void DeleteWord(MyDictionary dictionary)
        {
            Console.WriteLine($"Enter the key word to delete");
            string word = Console.ReadLine();

            dictionary.Pairs.Remove(word);
            Console.WriteLine($"{word} was deleted");

            Console.WriteLine("Do you want to delete another (Y - yes, Q - quit)?");
            char isContinue = char.Parse(Console.ReadLine());
            if(isContinue == 'y')
            {
                DeleteWord(dictionary);
            }
            else if(isContinue == 'q')
            {
                ChooseDictionary();
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
                EndOrContinueGame(dictionary);
            }
            else
            {
                Console.WriteLine($"The word '{word}' was not found in the {dictionary.KeyLanguage}-{dictionary.ValueLanguage} dictionary.");
                EndOrContinueGame(dictionary);
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

        public void EndOrContinueGame(MyDictionary dictionary)
        {
            Console.WriteLine("Do you want to find next word (Y - yes, Q - quit)?");
            char isContinue = char.Parse(Console.ReadLine());
            if (isContinue == 'y')
            {
                Thread.Sleep(1000);
                Console.Clear();
                ShowExactTranscription(dictionary);
            }
            else if (isContinue == 'q')
            {
                return;
            }
        }

        public void EditTranlations(MyDictionary dictionary)
        {
            Console.Write($"Write the word to change translation in {dictionary.KeyLanguage}: ");
            string word = Console.ReadLine();
            if (dictionary.Pairs.TryGetValue(word, out List<string> translations))
            {
                string words = string.Join(", ", translations.ToArray());
                StringBuilder currentText = new StringBuilder(words);
                Console.Write(currentText.ToString());

                Console.SetCursorPosition(words.Length, Console.CursorTop);

                EditText(currentText);

                Console.WriteLine("\nFinal text: " + currentText.ToString());

                List<string> strings = currentText.ToString().Split(new string[] { ", "}, StringSplitOptions.None).ToList();
                dictionary.Pairs[word].Clear();
                dictionary.Pairs[word].AddRange(strings);
            }
        }

        public void EditKey(MyDictionary dictionary)
        {
            Console.Write($"Write the key to change in {dictionary.KeyLanguage}: ");
            string word = Console.ReadLine();

            if (dictionary.Pairs.TryGetValue(word, out List<string> translations))
            {
                Console.WriteLine($"Enter new key in {dictionary.KeyLanguage}: ");
                string newKey = Console.ReadLine();

                dictionary.Pairs.Remove(word);
                dictionary.Pairs.Add(newKey, translations);
            }

        }


        static void EditText(StringBuilder text)
        {
            int cursorPosition = text.Length;
            ConsoleKeyInfo keyInfo;

            while ((keyInfo = Console.ReadKey(intercept: true)).Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (cursorPosition > 0)
                        {
                            cursorPosition--;
                            Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (cursorPosition < text.Length)
                        {
                            cursorPosition++;
                            Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                        }
                        break;

                    case ConsoleKey.Backspace:
                        if (cursorPosition > 0)
                        {
                            text.Remove(cursorPosition - 1, 1);
                            cursorPosition--;
                            RedrawText(text, cursorPosition);
                        }
                        break;

                    case ConsoleKey.Delete:
                        if (cursorPosition < text.Length)
                        {
                            text.Remove(cursorPosition, 1);
                            RedrawText(text, cursorPosition);
                        }
                        break;

                    default:
                        if (!char.IsControl(keyInfo.KeyChar))
                        {
                            text.Insert(cursorPosition, keyInfo.KeyChar);
                            cursorPosition++;
                            RedrawText(text, cursorPosition);
                        }
                        break;
                }
            }
        }

        static void RedrawText(StringBuilder text, int cursorPosition)
        {
            int originalCursorPosition = Console.CursorLeft;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(text.ToString() + new string(' ', Console.WindowWidth - text.Length));
            Console.SetCursorPosition(cursorPosition, Console.CursorTop);
        }
    }
}