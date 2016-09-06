using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class FileResultWriter
    {
        private GlobalIndex index;
        public FileResultWriter(GlobalIndex index)
        {
            this.index = index;
        }

        public void Output(List<Term> result, string path)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path))
            {
                foreach (Term term in result)
                {
                    ISet<string> originals = index.RetrieveVariantsOfCanonicalTerm(term.GetConcept());
                    file.WriteLine(term.GetConcept() + " |" + WriteToString(originals) + "\t\t\t" + term.GetConfidence());
                }
            }
        }

        private string WriteToString(ISet<string> container)
        {
            return String.Join(" |", container);
        }
    }
}
