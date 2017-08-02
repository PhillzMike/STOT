using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    /// <summary>
    /// Uploads tokens from a file into the inverted index Table
    /// Author Ope
    /// </summary>
    public static class InvertedIndexer
    {
        private static Dictionary<String,Dictionary<Document,Int32>> invertedIndexTable;
        public static Dictionary<string,Dictionary<Document,int>> InvertedIndexTable { get => invertedIndexTable; }

        public static void AddDocument(List<String> words,Document doc) { }
        public static void DeleteDocument(List<String> words,Document doc) { }
        public static void AModifyDocument(List<String> words,Document doc) {
                //figure out what git did
        }
        //Add Document
        //Remove Document
        //Modify Document
    }
}
