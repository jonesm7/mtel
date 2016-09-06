using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class FeatureCorpusTermFrequencyBuilder
    {
        private WordCounter wordCounter = new WordCounter();

        public FeatureCorpusTermFrequency Build(GlobalIndex index)
        {
            FeatureCorpusTermFrequency featureCorpusTermFrequency = new FeatureCorpusTermFrequency(index);
            if (index.GetCanonicalTerms().Count == 0 || index.GetDocuments().Count == 0)
            {
                throw new ArgumentException("No resource indexed.");
            }
            int totalCorpusTermFrequency = 0;
            Count(index, featureCorpusTermFrequency);
            foreach (Document document in index.GetDocuments())
            {
                totalCorpusTermFrequency += wordCounter.CountWords(document);
            }
            featureCorpusTermFrequency.SetTotalCorpusTermFrequency(totalCorpusTermFrequency);
            return featureCorpusTermFrequency;
        }

        private void Count(GlobalIndex index, FeatureCorpusTermFrequency featureCorpusTermFrequency)
        {
            TermFrequencyCounter termFrequencyCounter = new TermFrequencyCounter();
            foreach (Document document in index.GetDocuments())
            {
                string context = WordUtil.ApplyCharacterReplacement(document.GetContent(), RuntimeProperties.TERM_CLEAN_PATTERN);
                ISet<string> candidates = index.RetrieveCanonicalTermsInDoc(document);
                foreach (string term in candidates)
                {
                    int frequency = termFrequencyCounter.Count(context, index.RetrieveVariantsOfCanonicalTerm(term));
                    featureCorpusTermFrequency.AddToTermFrequency(term, frequency);
                }
            }
        }

    }
}
