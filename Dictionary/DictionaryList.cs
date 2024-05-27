using System;
using System.Collections.Generic;
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
            Dictionaries = new List<MyDictionary>();
        }

        public void AddDictionary(string keyLanguage, string valueLanguage)
        {
            Dictionaries.Add(new MyDictionary(keyLanguage, valueLanguage));
        }
    }
}
