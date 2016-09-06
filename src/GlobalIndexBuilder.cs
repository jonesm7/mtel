using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class GlobalIndexBuilder
    {
        public GlobalIndex Build(List<Document> corpus, NounPhraseExtractor extractor)
        {
            GlobalIndex index = new GlobalIndex();
            foreach (Document document in corpus)
            {
                IDictionary<string, ISet<string>> nounPhrases = extractor.Extract(document);

                index.IndexTermWithVariant(nounPhrases);

                ISet<string> termsCanonical = new HashSet<string>();
                termsCanonical.UnionWith(nounPhrases.Keys);
                index.IndexDocWithCanonicalTerms(document, termsCanonical);

                foreach (string term in nounPhrases.Keys)
                {
                    index.IndexCanonicalTermInDoc(term, document);
                }

            }
            return index;
        }
    }
}
