using System;
using TikaOnDotNet.TextExtraction;
namespace Engine {
    /// <summary>
    /// Extracts text word from the document.
    /// Remeber to set Inverter before working on this class
    /// <para>pdf, doc, docx, ppt, pptx, xls, xlsx, txt, html and xml</para>
    /// <para>to a File Stream</para>
    /// </summary>
    public static class Streamer {
        /// <summary>
        /// Sets the inverted Index Table.
        /// </summary>
        /// <value>
        /// The inverter object.
        /// </value>
        public static Inverter Invt { set { invt=value; } }
        private static Inverter invt;
        static TextExtractor x = new TextExtractor();
        /// <summary>
        /// Adds words from the Specified Document to the specified Inverted Index Table
        /// </summary>
        /// <param name="doc">The document to be Tokenized.</param>
        /// <exception cref="TextExtractionException">Could not extract Files from the Document</exception>
        public static void AddFileFrom(Document doc) {
            String[] words = Semanter.Splitwords(x.Extract(doc.Address).Text);
            invt.AddDocument(words,doc);
        }
        /// <summary>
        /// Removes the specified Document from the Inverted Index Table
        /// </summary>
        /// <param name="doc">The document.</param>
        public static void RemoveFile(Document doc) {
            invt.DeleteDocumentAsync(doc);
        }
        /// <summary>
        /// Modifies the file in the inverted Index Table.
        /// </summary>
        /// <param name="doc">The document that was Modified.</param>
        public static Document ModifyFile(Document doc) {
            String[] words = Semanter.Splitwords(x.Extract(doc.Address).Text);
                return invt.ModifyDocument(words,doc);
        }
    }
}
