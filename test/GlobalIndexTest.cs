using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class GlobalIndexTest
    {
        [TestMethod]
        public void TestIndexTermWithVariant()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            IDictionary<string, ISet<string>> map = new Dictionary<string, ISet<string>>();
            ISet<string> variants = new HashSet<string>();
            variants.Add("file name");
            variants.Add("filename");
            map.Add("filename", variants);
            globalIndex.IndexTermWithVariant(map);

            IDictionary<string, int> expectedTermIdMap = new Dictionary<string, int>();
            expectedTermIdMap.Add("filename", 0);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedTermIdMap, globalIndex.GetTermIdMap()));

            Assert.AreEqual(0, globalIndex.RetrieveCanonicalTerm("filename"));

            ISet<string> actualTermsCanonical = new HashSet<string>();
            actualTermsCanonical.UnionWith(globalIndex.GetCanonicalTerms());
            ISet<string> expectedTermsCanonical = new HashSet<string>();
            expectedTermsCanonical.Add("filename");
            Assert.IsTrue(Comparators.SetsAreEqual(expectedTermsCanonical, actualTermsCanonical));

            Assert.IsTrue(Comparators.SetsAreEqual(variants, globalIndex.RetrieveVariantsOfCanonicalTerm("filename")));

            Assert.AreEqual("filename", globalIndex.RetrieveCanonicalTerm(0));

            IDictionary<int, ISet<int>> expectedTermToVariant = new Dictionary<int, ISet<int>>();
            ISet<int> expectedVariantIds = new HashSet<int>();
            expectedVariantIds.Add(0);
            expectedVariantIds.Add(1);
            expectedTermToVariant.Add(0, expectedVariantIds);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedTermToVariant, globalIndex.GetTermToVariant()));

            IDictionary<int, int> expectedVariantToTerm = new Dictionary<int, int>();
            expectedVariantToTerm.Add(0, 0);
            expectedVariantToTerm.Add(1, 0);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedVariantToTerm, globalIndex.GetVariantToTerm()));

            IDictionary<string, int> expectedVariantMap = new Dictionary<string, int>();
            expectedVariantMap.Add("file name", 0);
            expectedVariantMap.Add("filename", 1);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedVariantMap, globalIndex.GetVariantMap()));


        }

        [TestMethod]
        public void TestIndexTermCanonicalInDoc()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            Document document = new FileDocument("myFile");

            globalIndex.IndexCanonicalTermInDoc("string", document);

            IDictionary<string, int> expectedTermIdMap = new Dictionary<string, int>();
            expectedTermIdMap.Add("string", 0);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedTermIdMap, globalIndex.GetTermIdMap()));

            Assert.AreEqual(0, globalIndex.RetrieveCanonicalTerm("string"));

            ISet<string> actualTermsCanonical = new HashSet<string>();
            actualTermsCanonical.UnionWith(globalIndex.GetCanonicalTerms());
            ISet<string> expectedTermsCanonical = new HashSet<string>();
            expectedTermsCanonical.Add("string");
            Assert.IsTrue(Comparators.SetsAreEqual(expectedTermsCanonical, actualTermsCanonical));

            ISet<string> expectedVariants = new HashSet<string>();
            Assert.IsTrue(Comparators.SetsAreEqual(expectedVariants, globalIndex.RetrieveVariantsOfCanonicalTerm("string")));

            Assert.AreEqual("string", globalIndex.RetrieveCanonicalTerm(0));

            IDictionary<Document, int> expectedDocMap = new Dictionary<Document, int>();
            expectedDocMap.Add(document, 0);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedDocMap, globalIndex.GetDocMap()));

            ISet<Document> expectedDocuments = new HashSet<Document>();
            expectedDocuments.Add(document);
            Assert.IsTrue(Comparators.SetsAreEqual(expectedDocuments, globalIndex.GetDocuments()));

            IDictionary<int, ISet<int>> expectedTermsToDocs = new Dictionary<int, ISet<int>>();
            ISet<int> expectedDocIds = new HashSet<int>();
            expectedDocIds.Add(0);
            expectedTermsToDocs.Add(0, expectedDocIds);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedTermsToDocs, globalIndex.GetTermToDocs()));
        }

        [TestMethod]
        public void TestIndexDocWithTermsCanonical()
        {
            GlobalIndex globalIndex = new GlobalIndex();
            Document document = new FileDocument("myFile");
            ISet<string> terms = new HashSet<string>();
            terms.Add("sewing machine");
            terms.Add("presser foot");

            globalIndex.IndexDocWithCanonicalTerms(document, terms);

            IDictionary<string, int> expectedTermIdMap = new Dictionary<string, int>();
            expectedTermIdMap.Add("sewing machine", 0);
            expectedTermIdMap.Add("presser foot", 1);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedTermIdMap, globalIndex.GetTermIdMap()));

            Assert.AreEqual(0, globalIndex.RetrieveCanonicalTerm("sewing machine"));
            Assert.AreEqual(1, globalIndex.RetrieveCanonicalTerm("presser foot"));

            ISet<string> actualTermsCanonical = new HashSet<string>();
            actualTermsCanonical.UnionWith(globalIndex.GetCanonicalTerms());
            ISet<string> expectedTermsCanonical = new HashSet<string>();
            expectedTermsCanonical.Add("sewing machine");
            expectedTermsCanonical.Add("presser foot");
            Assert.IsTrue(Comparators.SetsAreEqual(expectedTermsCanonical, actualTermsCanonical));

            ISet<string> expectedVariants = new HashSet<string>();
            Assert.IsTrue(Comparators.SetsAreEqual(expectedVariants, globalIndex.RetrieveVariantsOfCanonicalTerm("string")));

            Assert.AreEqual("sewing machine", globalIndex.RetrieveCanonicalTerm(0));
            Assert.AreEqual("presser foot", globalIndex.RetrieveCanonicalTerm(1));

            IDictionary<Document, int> expectedDocMap = new Dictionary<Document, int>();
            expectedDocMap.Add(document, 0);
            Assert.IsTrue(Comparators.DictionariesAreEqual(expectedDocMap, globalIndex.GetDocMap()));

            ISet<Document> expectedDocuments = new HashSet<Document>();
            expectedDocuments.Add(document);
            Assert.IsTrue(Comparators.SetsAreEqual(expectedDocuments, globalIndex.GetDocuments()));

            IDictionary<int, ISet<int>> expectedDocsToTerms = new Dictionary<int, ISet<int>>();
            ISet<int> expectedTermIds = new HashSet<int>();
            expectedTermIds.Add(0);
            expectedTermIds.Add(1);
            expectedDocsToTerms.Add(0, expectedTermIds);
            Assert.IsTrue(Comparators.DictionariesOfSetsAreEqual(expectedDocsToTerms, globalIndex.GetDocToTerms()));

            Assert.IsTrue(Comparators.SetsAreEqual(expectedTermIds, globalIndex.RetrieveCanonicalTermIdsInDoc(0)));

            Assert.IsTrue(Comparators.SetsAreEqual(expectedTermsCanonical, globalIndex.RetrieveCanonicalTermsInDoc(0)));

            Assert.IsTrue(Comparators.SetsAreEqual(expectedTermsCanonical, globalIndex.RetrieveCanonicalTermsInDoc(document)));
        }
    }
}
