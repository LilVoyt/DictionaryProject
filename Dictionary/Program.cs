using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    internal class Program
    {
        static void Main()
        {
            DictionaryList list = new DictionaryList();
            list.AddDictionary("ua", "en");
            list.Dictionaries[0].Add("word", new List<string> { "work", "adsf", "asdf" });
            Console.WriteLine(list.Dictionaries[0]);
            list.AddDictionary("en", "pl");
            list.Dictionaries[0].Add("asgkhjty", new List<string> { "asdf", "3456v", "fmkhj" });
            Console.WriteLine(list.Dictionaries[0]);
        }
    }
}
