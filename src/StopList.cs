using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class StopList
    {
        ISet<String> stopListData;
        public StopList()
        {
            stopListData = new HashSet<String>();

            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(
                "NTLS.TermExtractor.Properties.stoplist.txt");
            var reader = new StreamReader(stream);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string word = line.Trim();
                if (word.Length == 0 || word.StartsWith("//"))
                {
                    continue;
                }

                stopListData.Add(word.ToLower());
            }
        }

        public bool IsStopWord(string word)
        {
            return stopListData.Contains(word.ToLower());
        }
    }
}
