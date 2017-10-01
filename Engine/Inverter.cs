using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Engine {
    [Serializable]
    /// <summary>
    /// Uploads tokens from a file into the inverted index Table
    /// Author 3dO
    /// </summary>
    public class Inverter{
        [NonSerialized]
        private IStore store;
        /// <summary>
        /// Initializes a new instance of the <see cref="Inverter" /> class.
        /// </summary>
        /// <param name="StopWords">The path to a File COntaining all the stop words.</param>
        /// <exception cref="IOException">The specified path could not be Read</exception>
        public Inverter(String StopWords,String DictionaryPath,String CommonWordsPath,String FormatsPath,List<String> BooksPaths){
            Formats = new Dictionary<string, List<string>>();
            AddToFormats(File.ReadAllLines(FormatsPath));
            _samantha = new Semanter(DictionaryPath,CommonWordsPath);
            
            //TODO tomiwas idea about weight distribution based on file size
            foreach(String BookPath in BooksPaths)
                _samantha.AddToDictionary(BookPath,1);
            _stopwords = new HashSet<string>();
            _documentCount = 0;
            this.store = new Store();
          
            //invertedIndexTable = new Dictionary<string,Dictionary<Document,List<int>>>();
            _files = new Dictionary<string,Document>();
            try {
                foreach(String stp in File.ReadAllLines(StopWords))
                    _stopwords.Add(stp);
            } catch(Exception ex) {
                throw new IOException("The specified path for stopwords could not be Read", ex);
            }
        }
        public Inverter(String StopWords, String DictionaryPath, String CommonWordsPath, String FormatsPath, List<String> BooksPaths, IStore database):
            this(StopWords,DictionaryPath,CommonWordsPath,FormatsPath,BooksPaths){
            this.store = database;
        }
            private void AddToFormats(string[] types)
        {
            foreach(string type in types)
            {
                for(int i = 0; i <= type.Length; i++)
                {
                    string sub = type.Substring(0, i);
                    if (Formats.ContainsKey(sub))
                    {
                            Formats[sub].Add(type);
                    }
                    else
                    {
                        Formats.Add(sub, new List<string> { type } );
                    }
                }
            }
        }
        public Dictionary<string, List<string>> Formats;
      
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
            String NameToTrie="";
            foreach(string addWord in Semanter.Splitwords(doc.Name))
            {
                NameToTrie += addWord + " ";
                if (_stopwords.Contains(addWord)) {
                    continue;
                }
                string word = Samantha.StemWord(addWord);
                HashSet<string> correctedwords = new HashSet<string> {word};
                foreach (string Stemm in Samantha.CorrectWord(addWord, 2))
                    correctedwords.Add(Samantha.StemWord(Stemm));
                foreach(string correctedword in correctedwords)
                    AddWordToTable(correctedword, doc, i);
                i++;
            }
            _samantha.TrieWord(NameToTrie,7);
            AddWordToTable(doc.Type, doc, i++);
            foreach (String addWord in words) {
                string word = Samantha.StemWord(addWord.ToLower().Trim());
                if (_stopwords.Contains(word))
                    continue;
             
                AddWordToTable(word, doc, i);
                /*HashSet<string> correctedwords = new HashSet<string> { word };
                foreach (string Stemm in Samantha.CorrectWord(addWord, 3))
                    correctedwords.Add(Samantha.StemWord(Stemm));
                foreach (string correctedword in correctedwords)
                    AddWordToTable(correctedword, doc, i);*/
                i++;
               // if ((i % 50000) == 0)
                 //   i = i + 1 - 1;
            }
            _files.Add(doc.Address,doc); 
            //LogMovement("########Added document to index : " + doc.Address);
            _documentCount++;
        }
        private void AddWordToTable(String word,Document doc,int i) {
            store.AddWordToTable(word, doc, i);
            //LogMovement("../../../Resources/InvtLogs.txt", "Added doc in word " + word+"document path: "+doc.Address);
        }

        /// <summary>
        /// Delete this document
        /// </summary>
        /// <param name="doc">The document.</param>
        public void DeleteDocumentAsync(Document doc) {
            //LogMovement("../../../Resources/InvtLogs.txt", "Deletes" + doc.Address);
            Files.Remove(doc.Address);
            store.Delete(doc);
            doc.Delete();
            _documentCount--;
        }

        /// <summary>
        /// Removes a Document from the Table When it sees a Document no longer beign Tracked.
        /// </summary>
        /// <param name="word">The word that found a Document that has been Deleted.</param>
        /// <param name="doc">The document pointer to be removed.</param>
        public void RemoveDocument(String word,Document doc) {
            store.RemoveDocument(word,doc);
        }

        /// <summary>
        /// Runs on a separate Thread, Deletes ALL Untracked Documents from the Inverted Index Table
        /// </summary>
        public void GarbageCollector() {
            //TODO 3dO test 
            //Deadlock avoidance in database
           // Thread a = new Thread(new ThreadStart(GC));
            //a.Start();
        }
        public String[] AllWordsInTable {
            get => store.AllWordsInTable();
        }
        public bool WordIsInDoc(string word,Document doc)
        {
            return store.CheckWordInDoc(word, doc);
        }
        /// <summary>
        /// Returns all documents Containing this word, returns an empty array if the word isn't in any document
        /// </summary>
        /// <param name="Word">The word to be searched for</param>
        /// <returns>All documents Containing this word, returns an empty array if the word isn't in any document</returns>
        public Document[] AllDocumentsContainingWord(string Word) {
            return store.AllDocumentsContainingWord(Word);
            
        }
        public int[] PositionsWordOccursInDocument(string Word,Document doc) {
            return store.PositionsWordOccursInDocument(Word, doc);
        }
        public void GC() {
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
            DeleteDocumentAsync(doc);
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
            Inverter invt;
            try {
                using (Stream stream = File.Open(loadID, FileMode.Open)) {
                    var bform = new BinaryFormatter();
                    invt = (Inverter)(bform.Deserialize(stream));
                    stream.Close();
                };
                return invt;
            }
            catch (Exception ex){
                throw new Exception("An error occured trying to load Inverter from " + loadID + ". The path is either corrupted or doesn't exist",ex);
            }
            invt.store = new Store();
            return invt;
        }
        public static void LogMovement(string message)
        {
            try
            {
                File.AppendAllText("ActivityLog.txt", "["+DateTime.Now.ToString()+"]"+ message + "\r\n");
            }
            catch { }
        }
    }
}
