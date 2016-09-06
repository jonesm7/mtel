using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTLS.TermExtractor;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class StopListTest
    {
        [TestMethod]
        public void TestIsStopWord()
        {
            StopList stopList = new StopList();
            Assert.AreEqual(true, stopList.IsStopWord("everyone"));
        }

        [TestMethod]
        public void TestIsNotStopWord()
        {
            StopList stopList = new StopList();
            Assert.AreEqual(false, stopList.IsStopWord("rock"));
        }
    }
}
