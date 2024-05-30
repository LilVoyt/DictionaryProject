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
            Console.Clear();
            DictionaryList list = new DictionaryList();
            ShowAllDict(list);
            Console.WriteLine($"[{list.Dictionaries.Count}] Add Dictionary");
            Console.WriteLine($"[{list.Dictionaries.Count + 1}] Delete Dictionary");
            Console.WriteLine($"[{list.Dictionaries.Count + 2}] Exit");
            Console.Write("Choose an index: ");
            string input = Console.ReadLine();
            Thread.Sleep(1000);
            Console.Clear();
            WorkWithDictionary(list, input);
        }

        public void WorkWithDictionary(DictionaryList list, string input)
        {
            Console.Clear();
            int choose;
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
                        list.Dictionaries[choose].Add();
                        list.WriteToFile();
                        Console.WriteLine("Type something to enter menu: ");
                        Console.ReadKey();
                        Thread.Sleep(1000);
                        WorkWithDictionary(list, input);
                        break;
                    case 2:
                        list.Dictionaries[choose].ShowExactTranscription();
                        Console.WriteLine("Type something to enter menu: ");
                        Console.ReadKey();
                        Thread.Sleep(1000);
                        WorkWithDictionary(list, input);
                        break;
                    case 3:
                        list.Dictionaries[choose].ShowAllTranscription();
                        Console.WriteLine("Type something to enter menu: ");
                        Console.ReadKey();
                        Thread.Sleep(1000);
                        WorkWithDictionary(list, input);
                        break;
                    case 4:
                        list.Dictionaries[choose].EditTranlations();
                        list.WriteToFile();
                        Console.WriteLine("Type something to enter menu: ");
                        Console.ReadKey();
                        Thread.Sleep(1000);
                        WorkWithDictionary(list, input);
                        break;
                    case 5:
                        list.Dictionaries[choose].EditKey();
                        list.WriteToFile();
                        Console.WriteLine("Type something to enter menu: ");
                        Console.ReadKey();
                        Thread.Sleep(1000);
                        WorkWithDictionary(list, input);
                        break;
                    case 6:
                        list.Dictionaries[choose].DeleteWord();
                        list.WriteToFile();
                        Console.WriteLine("Type something to enter menu: ");
                        Console.ReadKey();
                        Thread.Sleep(1000);
                        WorkWithDictionary(list, input);
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
                if (isContinue == 'y')
                {
                    list.Dictionaries[choose].Add();
                    list.WriteToFile();
                }
            }
            else if (int.TryParse(input, out choose) && choose >= 0 && choose == list.Dictionaries.Count + 1)
            {
                DeleteDictionary(list);
                list.WriteToFile();
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

        public void DeleteDictionary(DictionaryList dictionaryList)
        {
            Console.Write("Enter the index of dictionary: ");
            int index = int.Parse(Console.ReadLine());

            dictionaryList.Dictionaries.RemoveAt(index);
            dictionaryList.WriteToFile();
            ChooseDictionary();
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
                dictionary.ShowExactTranscription();
            }
            else if (isContinue == 'q')
            {
                return;
            }
        }


        static public void EditText(StringBuilder text)
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

        static public void RedrawText(StringBuilder text, int cursorPosition)
        {
            int originalCursorPosition = Console.CursorLeft;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(text.ToString() + new string(' ', Console.WindowWidth - text.Length));
            Console.SetCursorPosition(cursorPosition, Console.CursorTop);
        }
    }
}