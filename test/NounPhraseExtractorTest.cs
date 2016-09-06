using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NTLS.TermExtractor;
using System.Collections.Generic;
using System.Linq;
using NTLS.TermExtractor.Tests;

namespace NLTS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class NounPhraseExtractorTest
    {
        private NounPhraseExtractor nounPhraseExtractor = new NounPhraseExtractor(new StopList(), new Normalizer());

        [TestMethod]
        public void TestChunkNPs()
        {
            string[] candidates = CallChunckNPS("Pickles is the softest dog to have ever lived.");
            CollectionAssert.AreEqual(new string[] { "Pickles", "the softest dog" }, candidates);
        }

        private string[] CallChunckNPS(string input)
        {
            string[] tokens = NLPToolsController.GetInstance().GetTokenizer().Tokenize(input);
            string[] pos = NLPToolsController.GetInstance().GetPosTagger().Tag(tokens);
            return nounPhraseExtractor.ChunkNPs(tokens, pos);
        }

        [TestMethod]
        public void TestExtractSingle()
        {
            var nounPhrases = nounPhraseExtractor.Extract("A large red flag fluttered.");
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();
            ISet<string> variants = new HashSet<string>();
            variants.Add("large red flag");
            expectedNounPhrases.Add("large red flag", variants);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

        [TestMethod]
        public void TestExtractMultipleVariants()
        {
            var nounPhrases = nounPhraseExtractor.Extract("Dogs are looking at dogs but not at a dog.");
            Comparators.PrintDictionaryOfSets(nounPhrases);
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();
            ISet<string> variants = new HashSet<string>();
            variants.Add("Dogs");
            variants.Add("dogs");
            variants.Add("dog");
            expectedNounPhrases.Add("dog", variants);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

        [TestMethod]
        public void TestExtractMultipleVariantsNumber()
        {
            var nounPhrases = nounPhraseExtractor.Extract("There are 12 dogs. A dog can't be seen.");
            Comparators.PrintDictionaryOfSets(nounPhrases);
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();
            ISet<string> variants = new HashSet<string>();
            // NOTE there is a TODO in the code to fix this problem
            //variants.Add("dogs");
            variants.Add("dog");
            expectedNounPhrases.Add("dog", variants);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

        [TestMethod]
        public void TestExtractMultipleVariantsIrregular()
        {
            var nounPhrases = nounPhraseExtractor.Extract("Wolves are looking at wolves but not at a wolf.");
            Comparators.PrintDictionaryOfSets(nounPhrases);
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();
            ISet<string> variants = new HashSet<string>();
            // NOTE there is a TODO in the code to fix this problem
            //variants.Add("Wolves");
            //variants.Add("wolves");
            variants.Add("wolf");
            expectedNounPhrases.Add("wolf", variants);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

        [TestMethod]
        public void TestExtractSingleShortWord()
        {
            var nounPhrases = nounPhraseExtractor.Extract("There is a P.");
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

        [TestMethod]
        public void TestExtractSingleLongTerm()
        {
            var nounPhrases = nounPhraseExtractor.Extract("The lovely red ironed rectangular Swiss flag.");
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

        [TestMethod]
        public void TestExtractWithConjunction()
        {
            var nounPhrases = nounPhraseExtractor.Extract("The red and flat flag.");
            Comparators.PrintDictionaryOfSets(nounPhrases);
            Dictionary<string, ISet<string>> expectedNounPhrases = new Dictionary<string, ISet<string>>();

            ISet<string> variants = new HashSet<string>();
            variants.Add("red");
            expectedNounPhrases.Add("red", variants);

            ISet<string> variants2 = new HashSet<string>();
            variants2.Add("flat flag");
            expectedNounPhrases.Add("flat flag", variants2);

            ISet<string> variants3 = new HashSet<string>();
            variants3.Add("red and flat flag");
            expectedNounPhrases.Add("red and flat flag", variants3);

            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedNounPhrases, nounPhrases));
        }

    }
}
