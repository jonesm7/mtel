using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTLS.TermExtractor;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class WordUtilTest
    {
        private StopList stoplist = new StopList();
        private Normalizer normalizer = new Normalizer();

        [TestMethod]
        public void TestApplyTrimStopWords()
        {
            Assert.AreEqual("rock", WordUtil.ApplyTrimStopwords("rock", stoplist, normalizer));
            Assert.AreEqual(null, WordUtil.ApplyTrimStopwords("the", stoplist, normalizer));
            Assert.AreEqual("rock", WordUtil.ApplyTrimStopwords("the rock is", stoplist, normalizer));
            Assert.AreEqual(null, WordUtil.ApplyTrimStopwords("the is a why", stoplist, normalizer));
            // TODO see if it makes sense to return null instead
            Assert.AreEqual("       ", WordUtil.ApplyTrimStopwords("       ", stoplist, normalizer));
        }

        [TestMethod]
        public void TestApplyCharacterReplacement()
        {
            Assert.AreEqual("", WordUtil.ApplyCharacterReplacement("", RuntimeProperties.TERM_CLEAN_PATTERN));
            Assert.AreEqual("", WordUtil.ApplyCharacterReplacement("   ", RuntimeProperties.TERM_CLEAN_PATTERN));
            Assert.AreEqual("rock paper 334390", WordUtil.ApplyCharacterReplacement("+ rock%paper# 334390", RuntimeProperties.TERM_CLEAN_PATTERN));
        }

        [TestMethod]
        public void TestContainsDigit()
        {
            Assert.AreEqual(false, WordUtil.ContainsDigit("  "));
            Assert.AreEqual(true, WordUtil.ContainsDigit("9"));
            Assert.AreEqual(true, WordUtil.ContainsDigit("rock7up8High Sky1"));
            Assert.AreEqual(false, WordUtil.ContainsDigit("rock up High Sky"));
        }

        [TestMethod]
        public void TestContainsLetter()
        {
            Assert.AreEqual(false, WordUtil.ContainsLetter("  "));
            Assert.AreEqual(false, WordUtil.ContainsLetter("69"));
            Assert.AreEqual(true, WordUtil.ContainsLetter(" Dum dee dum"));
        }

        [TestMethod]
        public void TestApplySplitList()
        {
            CollectionAssert.AreEqual(new string[] { "The blue bookshelf" },
                WordUtil.ApplySplitList("The blue bookshelf"));
            CollectionAssert.AreEqual(new string[] { "The blue bookshelf", "the red one",
                "The blue bookshelf and the red one"},
                WordUtil.ApplySplitList("The blue bookshelf and the red one"));
            CollectionAssert.AreEqual(new string[] { "This one", "that one" },
                WordUtil.ApplySplitList("This one, that one"));
            CollectionAssert.AreEqual(new string[] { "The blue bookshelf", "the red one",
                "The blue bookshelf or the red one" },
                WordUtil.ApplySplitList("The blue bookshelf or the red one"));
            CollectionAssert.AreEqual(new string[] { "The blue bookshelf", "the red one or the green one",
                "The blue bookshelf and the red one", "the green one",
                "The blue bookshelf and the red one or the green one" },
                WordUtil.ApplySplitList("The blue bookshelf and the red one or the green one"));
        }

        [TestMethod]
        public void TestHasReasonableNumChars()
        {
            Assert.AreEqual(false, WordUtil.HasReasonableNumChars("o"));
            Assert.AreEqual(true, WordUtil.HasReasonableNumChars("The whole world"));
            Assert.AreEqual(true, WordUtil.HasReasonableNumChars("toe"));
            Assert.AreEqual(false, WordUtil.HasReasonableNumChars("on!"));
            Assert.AreEqual(true, WordUtil.HasReasonableNumChars("$%^&*"));
        }

    }
}
