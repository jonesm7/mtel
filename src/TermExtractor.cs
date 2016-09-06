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
        static void Main(string[] args)
        {
            StopList stop = new StopList();
            Normalizer lemmatizer = new Normalizer();
            NounPhraseExtractor nounPhraseExtractor = new NounPhraseExtractor(stop, lemmatizer);
            GlobalIndexBuilder builder = new GlobalIndexBuilder();
            List<Document> documents = new List<Document>();
            documents.Add(new FileDocument(args[0]));
            GlobalIndex termDocIndex = builder.Build(documents, nounPhraseExtractor);

            FeatureTermNest termNest = new FeatureTermNestBuilder().Build(termDocIndex);
            FeatureCorpusTermFrequency termCorpusFrequency = new FeatureCorpusTermFrequencyBuilder().Build(termDocIndex);
            FileResultWriter writer = new FileResultWriter(termDocIndex);
            CValueAlgorithm algorithm = new CValueAlgorithm();
            AlgorithmContext context = new AlgorithmContext(termCorpusFrequency, termNest);
            List<Term> result = algorithm.Execute(context);
            string outFolder = args[2];
            writer.Output(result, outFolder + @"\cValue-output.txt");
        }
    }
}
