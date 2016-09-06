using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class FeatureTermNestBuilderTest
    {
        [TestMethod]
        public void TestBuild()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            FileDocument document = new FileDocument("myFile");
            ISet<string> terms = new HashSet<string>();
            terms.Add("machine");
            terms.Add("sewing machine");
            terms.Add("sewing");

            globalIndex.IndexDocWithCanonicalTerms(document, terms);

            FeatureTermNest featureTermNest = new FeatureTermNestBuilder().Build(globalIndex);

            ISet<int> expectedNestIds = new HashSet<int>();
            expectedNestIds.Add(1);
            Assert.IsTrue(Comparators.SetsAreEqual(expectedNestIds, featureTermNest.GetNestIdsOf("machine")));
            Assert.IsTrue(Comparators.SetsAreEqual(expectedNestIds, featureTermNest.GetNestIdsOf("sewing")));
        }
    }
}
