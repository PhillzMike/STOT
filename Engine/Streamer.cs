using System;
using System.Collections.Generic;
using System.IO;
using TikaOnDotNet.TextExtraction;
namespace Engine {
    /// <summary>
    /// Author 3dO
    /// Class to Help Convert a File of the following file types
    /// <para>pdf, doc, docx, ppt, pptx, xls, xlsx, txt, html and xml</para>
    /// <para>to a File Stream</para>
    /// </summary>
    public static class Streamer {

        public static void AddFileFrom(Document doc) {
            TextExtractor x = new TextExtractor();
            String[] words = x.Extract(doc.Address).Text.Split((new char[] { ' ' }),StringSplitOptions.RemoveEmptyEntries);
            Inverter.AddDocument(words,doc);
        }
        public static void RemoveFile(Document doc) {
            Inverter.DeleteDocument(doc);
        }
        public static void ModifyFile(Document doc) {

        }
        //Stop codes
    }
}
