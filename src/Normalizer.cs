using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNLP.Tools.SentenceDetect;
using LemmaSharp;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class Normalizer
    {
        private LemmatizerPrebuiltCompact lemmatizer;
        public Normalizer()
        {
            lemmatizer = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.English);
        }

        public string Normalize(string value)
        {
            if (value.IndexOf(' ') == -1 || value.EndsWith(" s") || value.EndsWith("'s"))
            {
                return lemmatizer.Lemmatize(value.Trim());
            }

            int lastIndexOf = value.LastIndexOf(' ');
            string part1 = value.Substring(0, lastIndexOf);
            string part2 = value.Substring(lastIndexOf + 1);
            part2 = lemmatizer.Lemmatize(part2.Trim());
            return part1 + " " + part2;
        }
    }
}
