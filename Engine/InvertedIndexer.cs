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
        private static Dictionary<String,Dictionary<Document,List<int>>> invertedIndexTable;
        public static Dictionary<string,Dictionary<Document,List<int>>> Table { get => invertedIndexTable; }

        public static void AddDocument(List<String> words,Document doc) {
            int i = 0;
            foreach(String word in words) {
                if(invertedIndexTable.ContainsKey(word)) {
                    if(invertedIndexTable[word].ContainsKey(doc)) {
                        invertedIndexTable[word][doc].Add(i++);
                        List<int> ia = new List<int> {
                            1
                        };
                    } else {
                        invertedIndexTable[word].Add(doc,new List<int> { i++ });
                    }
                } else {
                    invertedIndexTable.Add(word,new Dictionary<Document,List<int>> {{ doc,new List<int> { i++ } } });
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
