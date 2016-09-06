using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class RuntimeProperties
    {
        public static string TERM_CLEAN_PATTERN = "[^a-zA-Z0-9\\-]";
        public static int TERM_MAX_WORDS = 5;
        public static bool TERM_IGNORE_DIGITS = true;
    }
}
