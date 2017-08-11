using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;

[assembly: InternalsVisibleTo("InverterTest")]
namespace Engine {
   
    /// <summary>
    /// Uploads tokens from a file into the inverted index Table
    /// Author 3dO
    /// </summary>
    public class Inverter{
        /// <summary>
        /// Initializes a new instance of the <see cref="Inverter" /> class.
        /// </summary>
        /// <param name="StopWords">The path to a File COntaining all the stop words.</param>
        /// <exception cref="IOException">The specified path could not be Read</exception>
        public Inverter(String StopWords):this(){
            try {
                foreach(String stp in File.ReadAllLines(StopWords))
                    _stopwords.Add(stp);
            } catch(Exception ex) {
                throw new IOException("The specified path could not be Read",ex);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Inverter"/> class without any stop words to be ignored.
        /// </summary>
        public Inverter() {
            _stopwords = new HashSet<string>();
            _documentCount = 0;
            invertedIndexTable = new Dictionary<string,Dictionary<Document,List<int>>>();
        }

        /// <summary>
        /// Gets the count of all the Documents in the inverted Index Table.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public int DocumentCount { get => _documentCount;  }
        private int _documentCount;

        /// <summary>
        /// Gets the Inerted Index table, A dictionary of Strings(Words) and Each Word has a Dictionary as the value, 
        /// <para>The Inner Dictionary uses the Documents containing the Word as Key, </para>
        /// <para>and a list of their positions in the Document as Value.</para>
        /// <para>e.g if...Table["Girl"][Document1].tryGetValue = {1,17,59}, thit means that</para>
        /// <para>The word "Girl" occurs in Document1 three times, in position 1,17 and 59 </para>
        /// </summary>
        /// <value>
        /// The Inverted Index Table.
        /// </value>
        //TODO synchronize
        public Dictionary<string,Dictionary<Document,List<int>>> Table { get => invertedIndexTable; }
        private Dictionary<String,Dictionary<Document,List<int>>> invertedIndexTable;

        /// <summary>
        /// Gets the stopwords which are ignored by in this inverted Index Table.
        /// </summary>
        /// <value>
        /// The stopwords.
        /// </value>
        public HashSet<string> Stopwords { get => _stopwords;}
        private HashSet<String> _stopwords;

        /// <summary>
        /// Adds the document to the Inverted Index Table.
        /// </summary>
        /// <param name="words">The words in the Documents in Form of a List of Strings.</param>
        /// <param name="doc">The document to be added.</param>
        public void AddDocument(String[] words,Document doc) {
            int i = 0;
            foreach(String word in words) {
                if(_stopwords.Contains(word)) {
                    continue;
                }
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
            _documentCount++;
        }
        /// <summary>
        /// Delete this document
        /// </summary>
        /// <param name="doc">The document.</param>
        public void DeleteDocument(Document doc) {
            doc.Delete();
        }
        /// <summary>
        /// Removes a Document from the Table When it sees a Document no longer beign Tracked.
        /// </summary>
        /// <param name="word">The word that found a Document that has been Deleted.</param>
        /// <param name="doc">The document pointer to be removed.</param>
        public void RemoveDocument(String word,Document doc) {
            invertedIndexTable[word].Remove(doc);
            if(invertedIndexTable[word].Keys.Count == 0) {
                invertedIndexTable.Remove(word);
            }
        }
        /// <summary>
        /// Runs on a separate Thread, Deletes ALL Untracked Documents from the Inverted Index Table
        /// </summary>
        public void GarbageCollector() {
            //TODO test
            //Deadlock avoidance
            Thread a = new Thread(new ThreadStart(GC));
            a.Start();
        }
        private void GC() {
            foreach(string word in Table.Keys) {
                foreach(Document doc in Table[word].Keys) {
                    if(!doc.Exists) {
                        RemoveDocument(word,doc);
                    }
                }

            }
        }
        /// <summary>
        /// When a document has been modified.
        /// </summary>
        /// <param name="words">The words in the new Document.</param>
        /// <param name="doc">The document modified.</param>
        public void ModifyDocument(String[] words,Document doc) {
            Document newDoc = new Document(doc,(new FileInfo(doc.Address)).LastAccessTime);
            doc.Delete();
            AddDocument(words,newDoc);
        }
    }
}
