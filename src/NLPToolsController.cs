using OpenNLP.Tools.Chunker;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using OpenNLP.Tools.Util.Process;
using System;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class NLPToolsController
    {
        private ISentenceDetector sentenceDetector;
        private ITokenizer tokenizer;
        private IPosTagger posTagger;
        private static NLPToolsController instance;
        private IChunker phraseChunker;

        public NLPToolsController()
        {
            string modelPath = @"C:\Users\Garrett\Documents\Visual Studio 2015\Projects\MindysTermExtractionLibrary\src\sharpnlp-nbin-files\";
            sentenceDetector = new EnglishMaximumEntropySentenceDetector(modelPath + "EnglishSD.nbin");
            tokenizer = new EnglishMaximumEntropyTokenizer(modelPath + "EnglishTok.nbin");
            posTagger = new EnglishMaximumEntropyPosTagger(modelPath + "EnglishPOS.nbin");
            phraseChunker = new EnglishTreebankChunker(modelPath + "EnglishChunk.nbin");
        }

        public static NLPToolsController GetInstance()
        {
            if (instance == null)
            {
                instance = new NLPToolsController();
            }
            return instance;
        }

        public ISentenceDetector GetSentenceDetector()
        {
            return sentenceDetector;
        }

        public ITokenizer GetTokenizer()
        {
            return tokenizer;
        }

        public IPosTagger GetPosTagger()
        {
            return posTagger;
        }

        public IChunker GetPhraseChunker()
        {
            return phraseChunker;
        }
    }
}