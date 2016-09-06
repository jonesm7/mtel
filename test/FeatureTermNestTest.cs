using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class FeatureTermNestTest
    {
        [TestMethod]
        public void TestGetNestIdsOf()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            FileDocument document = new FileDocument("myFile");
            ISet<string> terms = new HashSet<string>();
            terms.Add("machine");
            terms.Add("sewing machine");

            globalIndex.IndexDocWithCanonicalTerms(document, terms);

            FeatureTermNest featureTermNest = new FeatureTermNest(globalIndex);
            featureTermNest.TermNestIn("machine", "sewing machine");

            ISet<int> expectedNestIds = new HashSet<int>();
            expectedNestIds.Add(1);
            Assert.IsTrue(Comparators.SetsAreEqual(expectedNestIds, featureTermNest.GetNestIdsOf("machine")));
        }
    }
}
