using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class CValueAlgorithmTest
    {
        [TestMethod]
        public void TestExecute()
        {
            AlgorithmContext context = new MockAlgorithmContext();
            CValueAlgorithm cValueAlgorithm = new CValueAlgorithm();
            List<Term> terms = cValueAlgorithm.Execute(context);
            Assert.AreEqual(1, terms.Count);
            Term term = terms[0];
            Assert.AreEqual("machine", term.GetConcept());
            Assert.AreEqual(0.0, term.GetConfidence(), 0.00001);
        }
    }

    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    internal class MockAlgorithmContext : AlgorithmContext
    {
        public MockAlgorithmContext() : base(null, null)
        {
        }
        public override int GetTermFrequency(string term)
        {
            return 5;
        }

        public override int GetTermFrequency(int id)
        {
            return 5;
        }

        public override ISet<int> GetNestsOf(string nested)
        {
            ISet<int> nests = new HashSet<int>();
            nests.Add(0);
            return nests;
        }

        public override ICollection<string> GetTerms()
        {
            ICollection<string> terms = new HashSet<string>();
            terms.Add("machine");
            return terms;
        }
    }
}
