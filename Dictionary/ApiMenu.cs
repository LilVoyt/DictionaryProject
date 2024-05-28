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

            if (int.TryParse(input, out choose) && choose >= 0 && choose < list.Dictionaries.Count)
            {
                Console.WriteLine(list.Dictionaries[choose]);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a valid index.");
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
    }
}
