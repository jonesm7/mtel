using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class TermExtractor
    {
        public List<Term> Run(List<string> filePaths)
        {
            var watchAll = System.Diagnostics.Stopwatch.StartNew();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            StopList stop = new StopList();
            Normalizer lemmatizer = new Normalizer();
            NounPhraseExtractor nounPhraseExtractor = new NounPhraseExtractor(stop, lemmatizer);
            GlobalIndexBuilder builder = new GlobalIndexBuilder();
            List<Document> documents = new List<Document>();
            foreach (string filePath in filePaths)
            {
                documents.Add(new FileDocument(filePath));
            }

            watch.Stop();
            Console.WriteLine("Setup: " + watch.ElapsedMilliseconds + " ms");
            watch = System.Diagnostics.Stopwatch.StartNew();

            GlobalIndex termDocIndex = builder.Build(documents, nounPhraseExtractor);

            watch.Stop();
            Console.WriteLine("GlobalIndexBuilder.Build(): " + watch.ElapsedMilliseconds + " ms");
            watch = System.Diagnostics.Stopwatch.StartNew();

            FeatureTermNest termNest = new FeatureTermNestBuilder().Build(termDocIndex);

            watch.Stop();
            Console.WriteLine("FeatureTermNestBuilder.Build: " + watch.ElapsedMilliseconds + " ms");
            watch = System.Diagnostics.Stopwatch.StartNew();

            FeatureCorpusTermFrequency termCorpusFrequency = new FeatureCorpusTermFrequencyBuilder().Build(termDocIndex);

            watch.Stop();
            Console.WriteLine("FeatureCorpusTermFrequencyBuilder.Build: " + watch.ElapsedMilliseconds + " ms");
            watch = System.Diagnostics.Stopwatch.StartNew();

            FileResultWriter writer = new FileResultWriter(termDocIndex);
            CValueAlgorithm algorithm = new CValueAlgorithm();
            AlgorithmContext context = new AlgorithmContext(termCorpusFrequency, termNest);
            List<Term> terms = algorithm.Execute(context);

            watch.Stop();
            watchAll.Stop();
            Console.WriteLine("CValueAlgorithm.Execute: " + watch.ElapsedMilliseconds + " ms");
            Console.WriteLine("Everything: " + watchAll.ElapsedMilliseconds + " ms");

            return terms;
        }
    }
}
