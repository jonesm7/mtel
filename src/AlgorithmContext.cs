using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class AlgorithmContext
    {
        private FeatureCorpusTermFrequency termFrequency;
        private FeatureTermNest termNest;

        public AlgorithmContext(FeatureCorpusTermFrequency termFrequency, FeatureTermNest termNest)
        {
            this.termFrequency = termFrequency;
            this.termNest = termNest;
        }

        public virtual int GetTermFrequency(string term)
        {
            int frequency = termFrequency.GetTermFrequency(term);
            return frequency == 0 ? 1 : frequency;
        }

        public virtual int GetTermFrequency(int id)
        {
            int frequency = termFrequency.GetTermFrequency(id);
            return frequency == 0 ? 1 : frequency;
        }

        public virtual ISet<int> GetNestsOf(string nested)
        {
            return termNest.GetNestIdsOf(nested);
        }

        public virtual ICollection<string> GetTerms()
        {
            return termFrequency.GetGlobalIndex().GetCanonicalTerms();
        }

    }
}
