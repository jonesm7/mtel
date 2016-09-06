using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class FeatureTermNestBuilder
    {
        public FeatureTermNest Build(GlobalIndex index)
        {
            FeatureTermNest featureTermNest = new FeatureTermNest(index);
            if (index.GetCanonicalTerms().Count == 0 || index.GetDocuments().Count == 0)
            {
                throw new ArgumentException("No resource Indexed.");
            }

            Check(index.GetCanonicalTerms(), index.GetCanonicalTerms(), featureTermNest);

            return featureTermNest;
        }

        private void Check(ICollection<string> nounPhrases, ICollection<string> allNounPhrases, FeatureTermNest featureTermNest)
        {
            foreach (string nounPhrase in nounPhrases)
            {
                foreach (string docNounPhrase in allNounPhrases)
                {
                    if (docNounPhrase.Length <= nounPhrase.Length)
                    {
                        continue;
                    }
                    if (docNounPhrase.IndexOf(" " + nounPhrase) != -1 || docNounPhrase.IndexOf(nounPhrase + " ") != -1)
                    {
                        featureTermNest.TermNestIn(nounPhrase, docNounPhrase);
                    }
                }
            }
        }

    }
}
