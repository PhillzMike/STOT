using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

//TODO [assembly: InternalsVisibleTo("InverterTest")]

namespace Engine {
    [Serializable]
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
        public Inverter(String StopWords,String DictionaryPath,String CommonWordsPath,List<String> BooksPaths){
            _samantha = new Semanter(DictionaryPath,CommonWordsPath);
            //TODO tomiwas idea about weight distribution based on file size
            //TODO Add FileName
            foreach(String BookPath in BooksPaths)
                _samantha.AddToDictionary(BookPath,1);
            _stopwords = new HashSet<string>();
            _documentCount = 0;
            invertedIndexTable = new Dictionary<string,Dictionary<Document,List<int>>>();
            _files = new Dictionary<string,Document>();
            try {
                foreach(String stp in File.ReadAllLines(StopWords))
                    _stopwords.Add(stp);
            } catch(Exception ex) {
                throw new IOException("The specified path could not be Read",ex);
            }
        }
      
        /// <summary>
        /// Gets the semanter used in this inverter.
        /// </summary>
        /// <value>
        /// The semanters.
        /// </value>
        public Semanter Samantha { get => _samantha; }
        private Semanter _samantha;

        /// <summary>
        /// Gets the files currently Stored in the Inverted Index Table.
        /// </summary>
        /// <value>
        /// The files stored in the Table.
        /// </value>
        public Dictionary<String,Document> Files { get => _files; }
        private Dictionary<String,Document> _files;

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
        //TODO 3dO synchronize
        //TODO Move to Database
          private Dictionary<String,Dictionary<Document,List<int>>> invertedIndexTable;

        /// <summary>
        /// Gets the stopwords which are ignored in this inverted Index Table.
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
            foreach(String addWord in words) {
                string word =  Samantha.StemWord(Samantha.CorrectWord(addWord.ToLower().Trim()));
                if (_stopwords.Contains(word)) {
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
            _files.Add(doc.Address,doc);
            _documentCount++;
        }

        /// <summary>
        /// Delete this document
        /// </summary>
        /// <param name="doc">The document.</param>
        public void DeleteDocument(Document doc) {
            Files.Remove(doc.Address);
            doc.Delete();
            _documentCount--;
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
            //TODO 3dO test 
            //Deadlock avoidance
            Thread a = new Thread(new ThreadStart(GC));
            a.Start();
        }
        public String[] AllWordsInTable {
           get=> invertedIndexTable.Keys.ToArray<String>();
        }
        /// <summary>
        /// Returns all documents Containing this word, returns an empty array if the word isn't in any document
        /// </summary>
        /// <param name="Word">The word to be searched for</param>
        /// <returns>All documents Containing this word, returns an empty array if the word isn't in any document</returns>
        public Document[] AllDocumentsContainingWord(string Word) {
            if (invertedIndexTable.ContainsKey(Word)){
                return invertedIndexTable[Word].Keys.ToArray<Document>();
            }
            else
            {
                return new Document[0];
            }
            
        }
        public int[] PositionsWordOccursInDocument(string Word,Document doc) {
            return invertedIndexTable[Word][doc].ToArray();
        }
        private void GC() {
            foreach(string word in AllWordsInTable) {
                foreach(Document doc in AllDocumentsContainingWord(word)) {
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
        public Document ModifyDocument(String[] words,Document doc) {
            Document newDoc = new Document(doc,(new FileInfo(doc.Address)).LastAccessTime);
            DeleteDocument(doc);
            AddDocument(words,newDoc);
            return newDoc;
        }

        public void SaveThis(string SavePath) {
            try {
                using(Stream stream = File.Open(SavePath,FileMode.Create)) {
                    new BinaryFormatter().Serialize(stream,this);
                    stream.Close();
                }
            } catch(Exception ex) {

                throw new Exception("There was an Error trying to Save the Inverted Index",ex);
            }
        }
        public static Inverter Load(string loadID) {
            Inverter invt = null ;
            try {
                using (Stream stream = File.Open(loadID, FileMode.Open)) {
                    var bform = new BinaryFormatter();
                    invt = (Inverter)(bform.Deserialize(stream));
                    stream.Close();
                };
            }
            catch (FileNotFoundException) {
                //TODO Remember to create an exception class for this search Engine and remove null
            }
            return invt;
        }
    }
}
