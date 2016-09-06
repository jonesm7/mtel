using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class WordUtil
    {
        public static string ApplyCharacterReplacement(string candidate, string pattern)
        {
            string patternStripped = Regex.Replace(candidate, pattern, " ");
            return Regex.Replace(patternStripped, @"\s+", " ").Trim();
        }

        public static string[] ApplySplitList(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (input.IndexOf(" and ") != -1)
            {
                string[] parts = Regex.Split(input, @"\band\b");
                foreach (string part in parts)
                {
                    stringBuilder.Append(part.Trim() + "|");
                }
            }
            if (input.IndexOf(" or ") != -1)
            {
                string[] parts = Regex.Split(input, @"\bor\b");
                foreach (string part in parts)
                {
                    stringBuilder.Append(part.Trim() + "|");
                }
            }
            if (input.IndexOf(",") != -1)
            {
                if (!ContainsDigit(input))
                {
                    string[] parts = input.Split(',');
                    foreach (string part in parts)
                    {
                        stringBuilder.Append(part.Trim() + "|");
                    }
                }
            }
            else
            {
                stringBuilder.Append(input);
            }
            string recomposedString = stringBuilder.ToString();
            if (recomposedString.EndsWith("|"))
            {
                recomposedString = recomposedString.Substring(0, recomposedString.LastIndexOf("|"));
            }
            return recomposedString.ToString().Split('|');
        }

        public static string ApplyTrimStopwords(string splitCandidate, StopList stopList, Normalizer normalizer)
        {
            if (stopList.IsStopWord(Regex.Replace(normalizer.Normalize(splitCandidate), @"\s+", "")))
            {
                return null;
            }
            string[] words = splitCandidate.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
            {
                return splitCandidate;
            }
            int firstIndex = words.Length;
            int lastIndex = -1;
            for (int i = 0; i < words.Length; i++)
            {
                if (!stopList.IsStopWord(words[i]))
                {
                    firstIndex = i;
                    break;
                }
            }
            for (int i = words.Length - 1; i >= 0; i--)
            {
                if (!stopList.IsStopWord(words[i]))
                {
                    lastIndex = i;
                    break;
                }
            }
            if (firstIndex <= lastIndex)
            {
                string trimmed = "";
                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    trimmed += words[i] + " ";
                }
                return trimmed.Trim();
            }
            return null;
        }

        public static bool ContainsDigit(string input)
        {
            foreach (char c in input)
            {
                if (Char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsLetter(string input)
        {
            foreach (char c in input)
            {
                if (Char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }


        public static bool HasReasonableNumChars(string input)
        {
            int length = input.Length;
            if (length < 2)
            {
                return false;
            }
            if (length < 5)
            {
                int num = 0;
                foreach (char c in input)
                {
                    if (Char.IsLetter(c) || Char.IsDigit(c))
                    {
                        num++;
                    }
                    if (num > 2)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

    }
}