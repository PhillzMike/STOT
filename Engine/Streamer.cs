using System;
using TikaOnDotNet.TextExtraction;
namespace Engine {
    /// <summary>
    /// Author 3dO
    /// Class to Help Convert a File of the following file types
    /// <para>pdf, doc, docx, ppt, pptx, xls, xlsx, txt, html and xml</para>
    /// <para>to a File Stream</para>
    /// </summary>
    public static class Streamer {
        /// <summary>
        /// Adds words from the Specified Document to the specified Inverted Index Table
        /// </summary>
        /// <param name="doc">The document to be Tokenized.</param>
        /// <param name="invt">The inverted Index class to add the Document to.</param>
        /// <exception cref="TextExtractionException">Could not extract Files from the Document</exception>
        public static void AddFileFrom(Document doc,Inverter invt) {
            TextExtractor x = new TextExtractor();
        //    try {
                String[] words = x.Extract(doc.Address).Text.Split((new char[] { ' ' }),StringSplitOptions.RemoveEmptyEntries);
                invt.AddDocument(words,doc);
        //    } catch (Exception ex) {
         //       throw new TextExtractionException("Could not extract Files from " + doc.Address +" "+ex.Message);
          //  }
        }
        /// <summary>
        /// Removes the specified Document from the Inverted Index Table
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="invt">The Inverted Index.</param>
        public static void RemoveFile(Document doc,Inverter invt) {
            //TODO 3dO call garbage collector in Updater
            invt.DeleteDocument(doc);
        }
        /// <summary>
        /// Modifies the file in the inverted Index Table.
        /// </summary>
        /// <param name="doc">The document that was Modified.</param>
        /// <param name="invt">The inverted Index.</param>
        /// <exception cref="TextExtractionException">Could not extract Files from the Document</exception>
        public static void ModifyFile(Document doc,Inverter invt) {
            TextExtractor x = new TextExtractor();
            try {
                String[] words = x.Extract(doc.Address).Text.Split((new char[] { ' ' }),StringSplitOptions.RemoveEmptyEntries);
                invt.ModifyDocument(words,doc);
            } catch(Exception ex) {
                throw new TextExtractionException("Could not extract Files from " + doc.Address + " " + ex.Message);
            }
        }
    }
}
