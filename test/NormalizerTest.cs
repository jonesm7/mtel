using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTLS.TermExtractor;

namespace NTLS.TermExtractor.Tests
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    [TestClass]
    public class NormalizerTest
    {
        [TestMethod]
        public void TestNormalizer()
        {
            Normalizer name = new Normalizer();
            Assert.AreEqual("rock", name.Normalize("rocks"));
            Assert.AreEqual("barking puppy", name.Normalize("barking puppies"));
            Assert.AreEqual("wolf", name.Normalize("wolves"));
            Assert.AreEqual("archive", name.Normalize("archives"));
        }
    }
}
