using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
    public interface IStore {
        /// <summary>
        /// Checks if a word exists in a document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns>true if it contains document otherwise false</returns>
        bool CheckWordInDoc(string word, Document doc);

        /// <summary>
        /// Gets all the words in the inverted index table
        /// </summary>
        /// <returns> an array of the words contained in the inverted index table.</returns>
        string[] AllWordsInTable();

        /// <summary>
        /// deletes a document;
        /// </summary>
        /// <param name="doc">The document.</param>
        void Delete(Document doc);

        /// <summary>
        /// Removes the document from a specific word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        void RemoveDocument(string word, Document doc);

        /// <summary>
        /// Adds the word to table under the Document.
        /// </summary>
        /// <param name="word">The word to be added.</param>
        /// <param name="doc">The document under which the word is added.</param>
        /// <param name="i">The index/Position the word occurs in the document.</param>
        void AddWordUnderDocument(String word, Document doc, int i);

        /// <summary>
        /// Get all the documents containing the given word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns> an array of documents containing the word</returns>
        Document[] AllDocumentsContainingWord(string word);

        /// <summary>
        /// Gets the position a word occurs in document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns>an array of the positions the word appears in the documents</returns>
        int[] PositionsWordOccursInDocument(string word, Document doc);

        /// <summary>
        /// Get all word positions in each document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>a dictionary containing a document in which a word occurs and the list of positions where it occurs</returns>
        Dictionary<Document, List<int>> WordsPositions(string word);

        /// <summary>
        /// Gets the relevance of the document.
        /// /// </summary>
        /// <param name="address">The address of the document.</param>
        /// <param name="pos">The position from which searching through the document can be done</param>
        /// <returns> string of wordsfound</returns>
        string GetRelevance(string address, int pos);

        /// <summary>
        /// Gets all documents in the datatabase.
        /// </summary>
        /// <returns> a dictionary of document address and the document object.</returns>
        Dictionary<string, Document> GetAllDoc();
    }
}
