using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class TermFrequencyCounter
    {
        public int Count(string context, ISet<string> terms)
        {
            ISet<int> offsets = new HashSet<int>();
            if (terms != null)
            {
                foreach (string term in terms)
                {
                    foreach (int offset in CountOffsets(term, context))
                    {
                        offsets.Add(offset);
                    }
                }
            }
            return offsets.Count;
        }

        private ISet<int> CountOffsets(string noun, string context)
        {
            ISet<int> offsets = new HashSet<int>();
            int next;
            int start = 0;
            while (start <= context.Length)
            {
                next = context.IndexOf(noun, start);
                char prefix = next - 1 < 0 ? ' ' : context[next - 1];
                char suffix = next + noun.Length >= context.Length ? ' ' : context[next + noun.Length];
                if (next != -1 && IsValidChar(prefix) && IsValidChar(suffix))
                {
                    offsets.Add(next);
                }
                if (next == -1)
                {
                    break;
                }
                start = next + noun.Length;
            }
            return offsets;
        }

        private bool IsValidChar(char c)
        {
            return !Char.IsLetter(c) && !Char.IsDigit(c);
        }

    }
}
