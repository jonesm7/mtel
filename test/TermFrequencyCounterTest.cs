using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class TermFrequencyCounterTest
    {
        [TestMethod]
        public void TestCount()
        {
            string text = "dog boondoggle dog";
            ISet<string> terms = new HashSet<string>();
            terms.Add("dog");
            TermFrequencyCounter termFrequencyCounter = new TermFrequencyCounter();
            Assert.AreEqual(2, termFrequencyCounter.Count(text, terms));
        }
    }
}
