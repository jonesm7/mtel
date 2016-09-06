using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class WordCounter
    {
        public int CountWords(Document document)
        {
            return document.GetContent().Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Length;
        }

    }
}
