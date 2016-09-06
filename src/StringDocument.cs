using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class StringDocument : Document
    {
        private string content;

        public StringDocument(string content)
        {
            this.content = content;
        }

        public string GetContent()
        {
            return content;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            StringDocument otherDoc = obj as StringDocument;
            if ((System.Object)otherDoc == null)
            {
                return false;
            }
            return content.Equals(otherDoc.content);
        }

        public override int GetHashCode()
        {
            return content.GetHashCode();
        }
    }
}
