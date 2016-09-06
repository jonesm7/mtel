using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class NounPhraseExtractor
    {
        private StopList stopList;
        private Normalizer normalizer;

        public NounPhraseExtractor(StopList stopList, Normalizer normalizer)
        {
            this.stopList = stopList;
            this.normalizer = normalizer;
        }

        public Dictionary<string, ISet<string>> Extract(Document document)
        {
            Dictionary<string, ISet<string>> result = new Dictionary<string, ISet<string>>();
            foreach (string sentence in NLPToolsController.GetInstance().GetSentenceDetector().SentenceDetect(document.GetContent()))
            {
                foreach (var pair in Extract(sentence))
                {
                    ISet<string> variants;
                    if (!result.TryGetValue(pair.Key, out variants))
                    {
                        variants = new HashSet<string>();
                    }
                    variants.UnionWith(pair.Value);
                    result[pair.Key] = variants;
                }
            }
            return result;
        }

        public Dictionary<string, ISet<string>> Extract(string content)
        {
            Dictionary<string, ISet<string>> nouns = new Dictionary<string, ISet<string>>();
            string[] tokens = NLPToolsController.GetInstance().GetTokenizer().Tokenize(content);
            string[] pos = NLPToolsController.GetInstance().GetPosTagger().Tag(tokens);
            string[] candidates = ChunkNPs(tokens, pos);
            foreach (string candidate in candidates)
            {
                string cleanedCandidate = WordUtil.ApplyCharacterReplacement(candidate, RuntimeProperties.TERM_CLEAN_PATTERN);
                string[] splitCandidates = WordUtil.ApplySplitList(cleanedCandidate);
                foreach (string splitCandidate in splitCandidates)
                {
                    string stopRemoved = WordUtil.ApplyTrimStopwords(splitCandidate, stopList, normalizer);
                    if (stopRemoved == null)
                    {
                        continue;
                    }
                    Console.WriteLine("cleanedCandidate: " + cleanedCandidate);

                    string original = stopRemoved;
                    Console.WriteLine("original: " + original);

                    string normalizedCandidate = normalizer.Normalize(stopRemoved.ToLower()).Trim();
                    Console.WriteLine("normalizedCandidate: " + normalizedCandidate);

                    string[] nElements = normalizedCandidate.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    if (nElements.Length < 1 || nElements.Length > RuntimeProperties.TERM_MAX_WORDS)
                    {
                        continue;
                    }
                    // TODO noun phrases starting with numerical digits are completely
                    // discarded but ones that are spelt out are included; this inconsistency
                    // should be fixed. Ideally numbers should be chopped off as stop words.
                    if (RuntimeProperties.TERM_IGNORE_DIGITS && WordUtil.ContainsDigit(normalizedCandidate))
                    {
                        Console.WriteLine("  . skipped - contains number");
                        continue;
                    }
                    if (!WordUtil.ContainsLetter(normalizedCandidate))
                    {
                        continue;
                    }
                    if (!WordUtil.HasReasonableNumChars(normalizedCandidate))
                    {
                        continue;
                    }
                    Console.WriteLine("cleanedCandidate <" + cleanedCandidate.ToLower() + "> "
                        + " normalizedCandidate<" + normalizedCandidate + ">");
                    // TODO handle stem-changing irregular plurals correctly - their
                    // variants should be included but aren't
                    if (cleanedCandidate.ToLower().IndexOf(normalizedCandidate) != -1)
                    {
                        ISet<string> variants;
                        if (!nouns.TryGetValue(normalizedCandidate, out variants))
                        {
                            variants = new HashSet<string>();
                        }
                        variants.Add(original);
                        nouns[normalizedCandidate] = variants;
                    }
                }
            }
            return nouns;
        }

        public string[] ChunkNPs(string[] tokens, string[] pos)
        {
            string[] phrases = NLPToolsController.GetInstance().GetPhraseChunker().Chunk(tokens, pos);
            List<string> candidates = new List<string>();
            string phrase = "";
            for (int n = 0; n < tokens.Length; n++)
            {
                if (phrases[n].Equals("B-NP"))
                {
                    phrase = tokens[n];
                    for (int m = n + 1; m < tokens.Length; m++)
                    {
                        if (phrases[m].Equals("I-NP"))
                        {
                            phrase = phrase + " " + tokens[m];
                        }
                        else
                        {
                            n = m;
                            break;
                        }
                    }
                    phrase = Regex.Replace(phrase, @"\s+", " ").Trim();
                    if (phrase.Length > 0)
                    {
                        candidates.Add(phrase);
                    }
                }

            }
            return candidates.ToArray();
        }
    }
}
