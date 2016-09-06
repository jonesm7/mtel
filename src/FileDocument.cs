using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTLS.TermExtractor
{
    /// <author email="terminology@nuevalenguatranslation.com">Mindy Jones</author>
    public class FileDocument : Document
    {
        private string filePath;
        public FileDocument(string filePath)
        {
            this.filePath = filePath;
        }

        public string GetContent()
        {
            return File.ReadAllText(filePath);
        }

        public override string ToString()
        {
            return "Doc(" + filePath + ")";
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            FileDocument otherDoc = obj as FileDocument;
            if ((System.Object)otherDoc == null)
            {
                return false;
            }
            return filePath.Equals(otherDoc.filePath);
        }

        public override int GetHashCode()
        {
            return filePath.GetHashCode();
        }
    }
}
