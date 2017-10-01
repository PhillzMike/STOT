using System;
using TikaOnDotNet.TextExtraction;
namespace Engine {
    /// <summary>
    /// Author 3dO
    /// Class to Help Convert a File of the following file types
    /// Remeber to set Inverter before working on this class
    /// <para>pdf, doc, docx, ppt, pptx, xls, xlsx, txt, html and xml</para>
    /// <para>to a File Stream</para>
    /// </summary>
    public static class Streamer {
        //TODO Get Rid of Streamer
        public static Inverter Invt { set { invt=value; } }
        private static Inverter invt;
        static TextExtractor x = new TextExtractor();
        /// <summary>
        /// Adds words from the Specified Document to the specified Inverted Index Table
        /// </summary>
        /// <param name="doc">The document to be Tokenized.</param>
        /// <param name="invt">The inverted Index class to add the Document to.</param>
        /// <exception cref="TextExtractionException">Could not extract Files from the Document</exception>
        public static void AddFileFrom(Document doc) {
            //  try {
            String[] words = Semanter.Splitwords(x.Extract(doc.Address).Text);
            invt.AddDocument(words,doc);
          //  } catch (Exception ex) {
           //     throw new TextExtractionException("Could not extract Files from " + doc.Address,ex);
          //  }
        }
        /// <summary>
        /// Removes the specified Document from the Inverted Index Table
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="invt">The Inverted Index.</param>
        public static void RemoveFile(Document doc,Inverter invt) {
            invt.DeleteDocumentAsync(doc);
        }
        /// <summary>
        /// Modifies the file in the inverted Index Table.
        /// </summary>
        /// <param name="doc">The document that was Modified.</param>
        /// <param name="invt">The inverted Index.</param>
        /// <exception cref="TextExtractionException">Could not extract Files from the Document</exception>
        public static Document ModifyFile(Document doc) {
            
            //    try {
            String[] words = Semanter.Splitwords(x.Extract(doc.Address).Text);
                return invt.ModifyDocument(words,doc);
     //       } catch(Exception ex) {
      //          Inverter.LogMovement("../../../Resources/ErrorLog", "Extraction Error Path: " + doc.Address + " Error Message: "+ex.Message);
    //            return null;
               // throw new TextExtractionException("Could not extract Files from " + doc.Address,ex);
    //        }
        }
    }
}
