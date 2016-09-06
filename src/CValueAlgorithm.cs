using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class CValueAlgorithm
    {
        public List<Term> Execute(AlgorithmContext context)
        {
            ISet<Term> result = new HashSet<Term>();
            foreach (string term in context.GetTerms())
            {
                double score;
                double log2a = Math.Log((double)term.Split(' ').Length + 0.1) / Math.Log(2.0);
                double freqa = (double)context.GetTermFrequency(term);
                ICollection<int> nest = context.GetNestsOf(term);
                double pTa = (double)nest.Count;
                double sumFrequencyb = 0.0;
                foreach (int id in nest)
                {
                    sumFrequencyb += (double)context.GetTermFrequency(id);
                }
                score = pTa == 0 ? log2a * freqa : log2a * (freqa - (sumFrequencyb / pTa));
                result.Add(new Term(term, score));
            }

            List<Term> sortedTerms = new List<Term>();
            foreach (Term term in result)
            {
                sortedTerms.Add(term);

            }
            sortedTerms.Sort();
            return sortedTerms;
        }
    }
}
