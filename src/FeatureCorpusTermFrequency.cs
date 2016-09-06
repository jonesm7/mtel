using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class FeatureCorpusTermFrequency
    {
        private GlobalIndex index;
        private IDictionary<int, int> termFrequencyMap = new Dictionary<int, int>();
        private int totalCorpusTermFrequency = 0;

        public FeatureCorpusTermFrequency(GlobalIndex index)
        {
            this.index = index;
        }

        public int GetTotalCorpusTermFrequency()
        {
            return totalCorpusTermFrequency;
        }

        public void SetTotalCorpusTermFrequency(int i)
        {
            totalCorpusTermFrequency = i;
        }

        public void AddToTermFrequency(string term, int i)
        {
            int termId = index.RetrieveCanonicalTerm(term);
            if (termId != -1)
            {
                AddToTermFrequency(termId, i);
            }
        }

        public void AddToTermFrequency(int t, int i)
        {
            int frequency;
            if (!termFrequencyMap.TryGetValue(t, out frequency))
            {
                frequency = 0;
            }
            termFrequencyMap[t] = frequency + i;
        }

        public GlobalIndex GetGlobalIndex()
        {
            return index;
        }

        public int GetTermFrequency(string term)
        {
            int termId = index.RetrieveCanonicalTerm(term);
            if (termId == -1)
            {
                return 0;
            }
            else
            {
                return GetTermFrequency(termId);
            }
        }

        public int GetTermFrequency(int t)
        {
            int frequency;
            if (!termFrequencyMap.TryGetValue(t, out frequency))
            {
                frequency = 0;
            }

            return frequency;
        }
    }
}
