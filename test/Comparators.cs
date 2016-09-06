using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    class Comparators
    {
        public static bool DictionariesOfSetsAreEqual<K, SV>(IDictionary<K, ISet<SV>> dict1,
            IDictionary<K, ISet<SV>> dict2)
        {
            return dict1.Keys.Count == dict2.Keys.Count
                && dict1.Keys.All(
                    k => dict2.ContainsKey(k)
                    && SetsAreEqual(dict1[k], dict2[k]));
        }

        public static bool DictionariesAreEqual<K, SV>(IDictionary<K, SV> dict1,
          IDictionary<K, SV> dict2)
        {
            return dict1.Keys.Count == dict2.Keys.Count
                && dict1.Keys.All(
                    k => dict2.ContainsKey(k)
                    && dict1[k].Equals(dict2[k]));
        }

        public static bool SetsAreEqual<V>(ICollection<V> set1, ICollection<V> set2)
        {
            return set1.Count == set2.Count
                   && set1.All(sv => set2.Contains(sv));
        }

        public static void PrintDictionaryOfSets<K, SV>(IDictionary<K, ISet<SV>> dict)
        {
            foreach (var x in dict.Keys)
            {
                Console.WriteLine(x);
                foreach (var y in dict[x])
                {
                    Console.WriteLine(" > " + y);
                }
            }
        }

        public static void PrintDictionary<K, V>(IDictionary<K, V> dict)
        {
            Console.WriteLine("size: " + dict.Count);
            foreach (var x in dict)
            {
                Console.WriteLine(x.Key + " = " + x.Value);
            }
        }

        public static void PrintSet<V>(ISet<V> set)
        {
            foreach (var x in set)
            {
                Console.WriteLine(x);
            }
        }
    }
}
