using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("InverterTest")]
namespace Engine {
   
    /// <summary>
    /// Uploads tokens from a file into the inverted index Table
    /// Author 3dO
    /// </summary>
    /// //TODO unstatic
    public static class Inverter{
        //TODO move to Updater        
        /// <summary>
        /// Gets the count of all the Documents in the inverted Index Table.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public static int DocumentCount { get; }
        private static Dictionary<String,Dictionary<Document,List<int>>> invertedIndexTable=new Dictionary<string, Dictionary<Document, List<int>>>();
        /// <summary>
        /// Gets the Inerted Index table, A dictionary of Strings(Words) and Each Word has a Dictionary as the value, 
        /// <para>The Inner Dictionary uses the Documents containing the Word as Key, </para>
        /// <para>and a list of where they appear in the Document as Value.</para>
        /// </summary>
        /// <value>
        /// The Inverted Index Table.
        /// </value>
        public static Dictionary<string,Dictionary<Document,List<int>>> Table { get => invertedIndexTable; }
        /// <summary>
        /// Adds the document to the Inverted Index Table.
        /// </summary>
        /// <param name="words">The words in the Documents in Form of a List of Strings.</param>
        /// <param name="doc">The document to be added.</param>
        public static void AddDocument(String[] words,Document doc) {
            int i = 0;
            foreach(String word in words) {
                if(invertedIndexTable.ContainsKey(word)) {
                    if(invertedIndexTable[word].ContainsKey(doc)) {
                        invertedIndexTable[word][doc].Add(i++);
                    } else {
                        invertedIndexTable[word].Add(doc,new List<int> { i++ });
                    }
                } else {
                    invertedIndexTable.Add(word,new Dictionary<Document,List<int>> {{ doc,new List<int> { i++ } } });
                }
            }
        }
        /// <summary>
        /// Delete this document from the Inverted Index Table
        /// </summary>
        /// <param name="doc">The document.</param>
        public static void DeleteDocument(Document doc) {
            doc = null;
        }
        /// <summary>
        /// Removes a Document from the Table When it sees a Document no longer beign Tracked.
        /// </summary>
        /// <param name="word">The word that found a Document that has been Deleted.</param>
        /// <param name="doc">The document pointer to be removed.</param>
        public static void RemoveDocument(String word,Document doc) {
            //TODO run on separate Thread
            invertedIndexTable[word].Remove(doc);
            if(invertedIndexTable[word].Keys.Count == 0) {
                invertedIndexTable.Remove(word);
            }
        }
        private static void GarbageCollector() {
            //Thread a = new Thread();
            
        }
        /// <summary>
        /// When a document has been modified.
        /// </summary>
        /// <param name="words">The words in the new Document.</param>
        /// <param name="doc">The document modified.</param>
        public static void ModifyDocument(String[] words,Document doc) {
            //TODO figure out what git did here
        }
        //Add Document
        //Remove Document
        //Modify Document
    }
}
