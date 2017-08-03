using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    /// <summary>
    /// Uploads tokens from a file into the inverted index Table
    /// Author 3dO
    /// </summary>
    /// //TODO unstatic
    public static class InvertedIndexer
    {
        private static Dictionary<String,Dictionary<Document,int>> invertedIndexTable;
        public static Dictionary<string,Dictionary<Document,int>> InvertedIndexTable { get => invertedIndexTable; }

        public static void AddDocument(List<String> words,Document doc) {
            foreach(String word in words) {
                if(invertedIndexTable.ContainsKey(word)) {
                    if(invertedIndexTable[word].ContainsKey(doc)) {
                        invertedIndexTable[word][doc]++;
                    } else {
                        invertedIndexTable[word].Add(doc,1);
                    }
                } else {
                    invertedIndexTable.Add(word,new Dictionary<Document,int> { { doc,1 } });
                }
            }
        }
        public static void DeleteDocument(Document doc) {
            doc = null;
        }
        public static void RemoveDocument(String word,Document doc) {
            invertedIndexTable[word].Remove(doc);
            if(invertedIndexTable[word].Keys.Count == 0) {
                invertedIndexTable.Remove(word);
            }
        }
        public static void ModifyDocument(List<String> words,Document doc) {
                //figure out what git did
        }
        //Add Document
        //Remove Document
        //Modify Document
    }
}
