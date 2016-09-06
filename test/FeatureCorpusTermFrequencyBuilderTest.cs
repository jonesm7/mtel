using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class FeatureCorpusTermFrequencyBuilderTest
    {
        [TestMethod]
        public void TestBuild()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            Document document = new StringDocument("I love sewing; my sewing machine is a Pfaff and I love it.");
            ISet<string> terms = new HashSet<string>();
            terms.Add("machine");
            terms.Add("sewing machine");
            terms.Add("sewing");

            globalIndex.IndexDocWithCanonicalTerms(document, terms);
            IDictionary<string, ISet<string>> termVariants = new Dictionary<string, ISet<string>>();
            ISet<string> machineVariants = new HashSet<string>();
            machineVariants.Add("machine");
            termVariants.Add("machine", machineVariants);
            ISet<string> sewingMachineVariants = new HashSet<string>();
            sewingMachineVariants.Add("sewing machine");
            termVariants.Add("sewing machine", sewingMachineVariants);
            ISet<string> sewingVariants = new HashSet<string>();
            sewingVariants.Add("sewing");
            termVariants.Add("sewing", sewingVariants);
            globalIndex.IndexTermWithVariant(termVariants);

            FeatureCorpusTermFrequencyBuilder featureCorpusTermFrequencyBuilder = new FeatureCorpusTermFrequencyBuilder();
            FeatureCorpusTermFrequency featureCorpusTermFrequency = featureCorpusTermFrequencyBuilder.Build(globalIndex);
            Assert.AreEqual(13, featureCorpusTermFrequency.GetTotalCorpusTermFrequency());
            Assert.AreEqual(1, featureCorpusTermFrequency.GetTermFrequency("machine"));

            Assert.AreEqual(2, featureCorpusTermFrequency.GetTermFrequency("sewing"));
        }
    }
}
