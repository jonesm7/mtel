using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class FeatureCorpusTermFrequencyTest
    {
        [TestMethod]
        public void TestGetTermFrequency()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            FileDocument document = new FileDocument("myFile");
            ISet<string> terms = new HashSet<string>();
            terms.Add("machine");

            globalIndex.IndexDocWithCanonicalTerms(document, terms);

            FeatureCorpusTermFrequency featureCorpusTermFrequency = new FeatureCorpusTermFrequency(globalIndex);

            Assert.AreEqual(0, featureCorpusTermFrequency.GetTermFrequency("machine"));

            featureCorpusTermFrequency.AddToTermFrequency("machine", 2);
            Assert.AreEqual(2, featureCorpusTermFrequency.GetTermFrequency("machine"));

            featureCorpusTermFrequency.AddToTermFrequency("machine", 3);
            Assert.AreEqual(5, featureCorpusTermFrequency.GetTermFrequency("machine"));
        }
    }
}
