﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Engine {
    /// <summary>
    /// Uploads tokens from a file into the inverted index Table.
    /// </summary>
    [Serializable]
    public class Inverter {
        [NonSerialized]
        private IStore store;
        /// <summary>
        /// Initializes a new instance of the <see cref="Inverter" /> class.
        /// </summary>
        /// <param name="StopWords">The path to a File Containing all the stop words.</param>
        /// <param name="DictionaryPath">The path to the file holding all legal words.</param>
        /// <param name="CommonWordsPath">The path to the file holding most coomonly used words.</param>
        /// <param name="FormatsPath">The path to the file holding all supported file types.</param>
        /// <param name="BooksPaths">Paths to Books, used for frequency nalysis of words.</param>
        /// <exception cref="IOException">The specified path could not be Read</exception>
        public Inverter(String StopWords, String DictionaryPath, String CommonWordsPath, String FormatsPath, List<String> BooksPaths) {
            Formats = new Dictionary<string, List<string>>();
            AddToFormats(File.ReadAllLines(FormatsPath));
            _samantha = new Semanter(DictionaryPath, CommonWordsPath);
            //tomiwas idea about weight distribution based on file size
            foreach (String BookPath in BooksPaths)
                _samantha.AddToDictionary(BookPath, 1);
            _stopwords = new HashSet<string>();
            _documentCount = 0;
            this.store = new Store();
            _documentCount = Files.Count;
            try {
                foreach (String stp in File.ReadAllLines(StopWords))
                    _stopwords.Add(stp);
            } catch (Exception ex) {
                throw new IOException("The specified path for stopwords could not be Read", ex);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Inverter" /> class.
        /// </summary>
        /// <param name="StopWords">The path to a File Containing all the stop words.</param>
        /// <param name="DictionaryPath">The path to the file holding all legal words.</param>
        /// <param name="CommonWordsPath">The path to the file holding most coomonly used words.</param>
        /// <param name="FormatsPath">The path to the file holding all supported file types.</param>
        /// <param name="BooksPaths">Paths to Books, used for frequency nalysis of words.</param>
        /// <param name="database">The database to use with this inverter.</param>
        public Inverter(String StopWords, String DictionaryPath, String CommonWordsPath, String FormatsPath, List<String> BooksPaths, IStore database) :
            this(StopWords, DictionaryPath, CommonWordsPath, FormatsPath, BooksPaths) {
            store = database;
        }
        void AddToFormats(string[] types) {
            foreach (string type in types) {
                for (int i = 0; i <= type.Length; i++) {
                    string sub = type.Substring(0, i);
                    if (Formats.ContainsKey(sub)) {
                        Formats[sub].Add(type);
                    } else {
                        Formats.Add(sub, new List<string> { type });
                    }
                }
            }
        }
        /// <summary>
        /// The List of File Formts acceptble by the App
        /// </summary>
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
        public Dictionary<String, Document> Files { get => store.GetAllDoc(); }
        /// <summary>
        /// Gets the count of all the Documents in the inverted Index Table.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public int DocumentCount { get => _documentCount; }
        private int _documentCount;
        /// <summary>
        /// Gets the stopwords which are ignored in this inverted Index Table.
        /// </summary>
        /// <value>
        /// The stopwords.
        /// </value>
        public HashSet<string> Stopwords { get => _stopwords; }
        private HashSet<String> _stopwords;
        /// <summary>
        /// Adds the document to the Inverted Index Table.
        /// </summary>
        /// <param name="words">The words in the Documents in Form of a List of Strings.</param>
        /// <param name="doc">The document to be added.</param>
        public void AddDocument(String[] words, Document doc) {
            int i = 0;
            String NameToTrie = "";
            foreach (string addWord in Semanter.Splitwords(doc.Name)) {
                NameToTrie += addWord + " ";
                String word = addWord.ToLower().Trim();
                if (_stopwords.Contains(addWord))
                    continue;
                store.AddWordUnderDocument(Samantha.StemWord(word), doc, i++);
            }
            _samantha.TrieWord(NameToTrie, 7);
            store.AddWordUnderDocument(doc.Type, doc, i++);
            foreach (String addWord in words) {
                if (i <= 160)
                    doc.Relevance += addWord + " ";
                string word =addWord.ToLower().Trim();
                if (_stopwords.Contains(word))
                    continue;
                store.AddWordUnderDocument(Samantha.StemWord(word), doc, i++);
            }
            store.AddToDocTable(doc);
            _documentCount++;
        }
        /// <summary>
        /// Delete this document
        /// </summary>
        /// <param name="doc">The document.</param>
        public void DeleteDocumentAsync(Document doc) {
            Files.Remove(doc.Address);
            store.Delete(doc);
            doc.Delete();
            _documentCount--;
        }
        /// <summary>
        /// Gets all words in table.
        /// </summary>
        /// <value>
        /// All words in table.
        /// </value>
        public String[] AllWordsInTable {
            get => store.AllWordsInTable();
        }
        /// <summary>
        /// Checks if the Words is in the document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        public bool WordIsInDoc(string word, Document doc) {
            return store.CheckWordInDoc(word, doc);
        }
        /// <summary>
        /// Returns all documents Containing this word and the positions inwhich, returns an empty array if the word isn't in any document
        /// </summary>
        /// <param name="Word">The word to be searched for</param>
        /// <returns>All documents Containing this word, returns an empty array if the word isn't in any document</returns>
        public Dictionary<Document, List<int>> AllDocumentsPositionsContainingWord(string Word) {
            return store.WordsPositions(Word);
        }
        /// <summary>
        /// When a document has been modified.
        /// </summary>
        /// <param name="words">The words in the new Document.</param>
        /// <param name="doc">The document modified.</param>
        public Document ModifyDocument(String[] words, Document doc) {
            Document newDoc = new Document(doc, (new FileInfo(doc.Address)).LastAccessTime);
            DeleteDocumentAsync(doc);
            AddDocument(words, newDoc);
            return newDoc;
        }
        /// <summary>
        /// Saves this current instance, serializes it to a file.
        /// </summary>
        /// <param name="SavePath">The save path.</param>
        /// <exception cref="Exception">There was an Error trying to Save the Inverted Index</exception>
        public void SaveThis(string SavePath) {
            try {
                using (Stream stream = File.Open(SavePath, FileMode.Create)) {
                    new BinaryFormatter().Serialize(stream, this);
                    stream.Close();
                }
            } catch (Exception ex) {

                throw new Exception("There was an Error trying to Save the Inverted Index", ex);
            }
        }
        /// <summary>
        /// Loads the specified Inverter, Deserializes using the identifier.
        /// </summary>
        /// <param name="loadID">The identifier.</param>
        /// <returns>The Object deserialized</returns>
        /// <exception cref="Exception">An error occured trying to load Inverter from " + loadID + ". The path is either corrupted or doesn't exist</exception>
        public static Inverter Load(string loadID) {
            Inverter invt;
            try {
                using (Stream stream = File.Open(loadID, FileMode.Open)) {
                    var bform = new BinaryFormatter();
                    invt = (Inverter)(bform.Deserialize(stream));
                    stream.Close();
                };
                invt.store = new Store();
                return invt;
            } catch (Exception ex) {
                throw new Exception("An error occured trying to load Inverter from " + loadID + ". The path is either corrupted or doesn't exist", ex);
            }
        }
        /// <summary>
        /// Logs the movements in Updater, to the ActivityLog.TXT file.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogMovement(string message) {
            try {
                File.AppendAllText("ActivityLog.txt", "[" + DateTime.Now.ToString() + "]" + message + "\r\n");
            } catch { }
        }
    }
}
