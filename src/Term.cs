using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class Term : IComparable
    {
        private string singular;
        private double confidence;

        public Term(string lemma, double confidence)
        {
            this.singular = lemma;
            this.confidence = confidence;
        }

        public string GetConcept()
        {
            return singular;
        }

        public double GetConfidence()
        {
            return confidence;
        }

        public void SetConcept(string singular)
        {
            this.singular = singular;
        }

        public void SetConfidence(double confidence)
        {
            this.confidence = confidence;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Term otherTerm = obj as Term;
            if ((System.Object)otherTerm == null)
            {
                return false;
            }
            return singular.Equals(otherTerm.singular);
        }

        public override int GetHashCode()
        {
            return singular.GetHashCode();
        }

        int IComparable.CompareTo(object obj)
        {
            Term term = (Term)obj;
            return confidence > term.confidence ? -1 : confidence < term.confidence ? 1 : 0;
        }

    }
}
