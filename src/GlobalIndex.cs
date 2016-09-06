using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class GlobalIndex
    {
        private int termCounter = 0;
        private int variantCounter = 0;
        private int docCounter = 0;

        private IDictionary<string, int> termIdMap = new Dictionary<string, int>();

        private List<string> terms = new List<string>();

        private IDictionary<string, int> variantIdMap = new Dictionary<string, int>();

        private List<string> allVariants = new List<string>();

        private IDictionary<Document, int> docIdMap = new Dictionary<Document, int>();

        private List<Document> documents = new List<Document>();

        private IDictionary<int, ISet<int>> termToDocs = new Dictionary<int, ISet<int>>();

        private IDictionary<int, ISet<int>> docToTerms = new Dictionary<int, ISet<int>>();
        private IDictionary<int, ISet<int>> termToVariants = new Dictionary<int, ISet<int>>();
        private IDictionary<int, int> variantsToTerms = new Dictionary<int, int>();
        public GlobalIndex() { }

        public IDictionary<string, int> GetTermIdMap()
        {
            return termIdMap;
        }

        public IDictionary<string, int> GetVariantMap()
        {
            return variantIdMap;
        }

        public IDictionary<Document, int> GetDocMap()
        {
            return docIdMap;
        }

        public IDictionary<int, ISet<int>> GetTermToDocs()
        {
            return termToDocs;
        }

        public IDictionary<int, ISet<int>> GetDocToTerms()
        {
            return docToTerms;
        }

        public IDictionary<int, ISet<int>> GetTermToVariant()
        {
            return termToVariants;
        }

        public IDictionary<int, int> GetVariantToTerm()
        {
            return variantsToTerms;
        }

        private int IndexCanonicalTerm(string term)
        {
            int index;
            if (!termIdMap.TryGetValue(term, out index))
            {
                index = termCounter;
                termIdMap.Add(term, index);
                termCounter++;
                terms.Add(term);
            }
            return index;
        }

        public int RetrieveCanonicalTerm(string term)
        {
            int index;
            if (!termIdMap.TryGetValue(term, out index))
            {
                return -1;
            }
            return index;
        }

        public string RetrieveCanonicalTerm(int id)
        {
            return terms[id];
        }

        public ICollection<string> GetCanonicalTerms()
        {
            return termIdMap.Keys;
        }

        private int IndexTermVariant(string termV)
        {
            int index;
            if (!variantIdMap.TryGetValue(termV, out index))
            {
                index = variantCounter;
                variantCounter++;
                variantIdMap.Add(termV, index);
                allVariants.Add(termV);
            }
            return index;
        }

        private string RetrieveTermVariant(int id)
        {
            return allVariants[id];
        }

        private int IndexDocument(Document document)
        {
            int index;
            if (!docIdMap.TryGetValue(document, out index))
            {
                index = docCounter;
                docIdMap.Add(document, index);
                docCounter++;
                documents.Add(document);
            }
            return index;
        }

        public ICollection<Document> GetDocuments()
        {
            return docIdMap.Keys;
        }

        public void IndexTermWithVariant(IDictionary<string, ISet<string>> map)
        {
            foreach (KeyValuePair<string, ISet<string>> entry in map)
            {
                int termId = IndexCanonicalTerm(entry.Key);
                ISet<int> variants;
                if (!termToVariants.TryGetValue(termId, out variants))
                {
                    variants = new HashSet<int>();
                }
                foreach (string variant in entry.Value)
                {
                    int varId = IndexTermVariant(variant);
                    variants.Add(varId);
                    variantsToTerms.Add(varId, termId);
                }
                termToVariants.Add(termId, variants);
            }
        }

        public ISet<string> RetrieveVariantsOfCanonicalTerm(string term)
        {
            ISet<string> variants = new HashSet<string>();
            ISet<int> variantsIds;
            if (!termToVariants.TryGetValue(RetrieveCanonicalTerm(term), out variantsIds))
            {
                return variants;
            }
            foreach (int variantId in variantsIds)
            {
                variants.Add(RetrieveTermVariant(variantId));
            }
            return variants;
        }

        public void IndexCanonicalTermInDoc(string term, Document document)
        {
            IndexCanonicalTermInDoc(IndexCanonicalTerm(term), IndexDocument(document));
        }

        private void IndexCanonicalTermInDoc(int term, int document)
        {
            ISet<int> docs;
            if (!termToDocs.TryGetValue(term, out docs))
            {
                docs = new HashSet<int>();
            }
            docs.Add(document);
            termToDocs.Add(term, docs);
        }

        public void IndexDocWithCanonicalTerms(Document document, ISet<string> terms)
        {
            ISet<int> termIds = new HashSet<int>();
            foreach (string term in terms)
            {
                termIds.Add(IndexCanonicalTerm(term));
            }
            IndexDocWithCanonicalTerms(IndexDocument(document), termIds);
        }

        private void IndexDocWithCanonicalTerms(int documentId, ISet<int> terms)
        {
            ISet<int> termIds;
            if (docToTerms.TryGetValue(documentId, out termIds))
            {
                termIds.UnionWith(terms);
            }
            docToTerms.Add(documentId, terms);
        }

        public ISet<int> RetrieveCanonicalTermIdsInDoc(int documentId)
        {
            ISet<int> res;
            if (!docToTerms.TryGetValue(documentId, out res))
            {
                return new HashSet<int>();
            }
            else
            {
                return res;
            }
        }

        public ISet<string> RetrieveCanonicalTermsInDoc(Document document)
        {
            return RetrieveCanonicalTermsInDoc(IndexDocument(document));
        }

        public ISet<string> RetrieveCanonicalTermsInDoc(int document)
        {
            ISet<string> res = new HashSet<string>();
            ISet<int> termIds = RetrieveCanonicalTermIdsInDoc(document);
            foreach (int termId in termIds)
            {
                res.Add(RetrieveCanonicalTerm(termId));
            }

            return res;
        }
    }
}
