using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTLS.TermExtractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class WordCounterTest
    {
        [TestMethod]
        public void TestCount()
        {
            WordCounter wordCounter = new WordCounter();
            Assert.AreEqual(1, wordCounter.CountWords(new StringDocument("mouse")));
            Assert.AreEqual(2, wordCounter.CountWords(new StringDocument(" \nblue\t  table\r")));
        }
    }
}
