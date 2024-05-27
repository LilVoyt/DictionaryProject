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
            list.Dictionaries[0].Add("word", new List<string> { "work" });
        }
    }
}
