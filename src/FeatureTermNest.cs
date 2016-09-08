using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class FeatureTermNest
    {
        private GlobalIndex index;
        private IDictionary<int, ISet<int>> termNested = new Dictionary<int, ISet<int>>();

        public FeatureTermNest(GlobalIndex index)
        {
            this.index = index;
        }

        public ISet<int> GetNestIdsOf(string nested)
        {
            int termId = index.RetrieveCanonicalTerm(nested);

            if (termId == -1)
            {
                return new HashSet<int>();
            }
            else
            {
                return GetNestIdsOf(termId);
            }
        }
       
        private ISet<int> GetNestIdsOf(int nested)
        {
            ISet<int> result;
            if (termNested.TryGetValue(nested, out result))
            {
                return result;
            }
            else
            {
                return new HashSet<int>();
            }
        }

        public void TermNestIn(string nested, string nest)
        {
            int termId = index.RetrieveCanonicalTerm(nested);
            int nestId = index.RetrieveCanonicalTerm(nest);
            if (termId != -1 && nestId != -1)
            {
                TermNestIn(termId, nestId);
            }
        }

        private void TermNestIn(int nested, int nest)
        {
            ISet<int> result;
            if (!termNested.TryGetValue(nested, out result))
            {
                result = new HashSet<int>();
            }

            result.Add(nest);
            termNested[nested] = result;
        }
    }
}
