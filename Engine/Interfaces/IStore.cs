using System;
using System.Collections.Generic;

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
        /// Adds the document to document table.
        /// </summary>
        /// <param name="doc">The document to be added.</param>
        void AddToDocTable(Document doc);

        /// <summary>
        /// Gets all document in the document table.
        /// </summary>
        /// <returns>All Documents in a Dictionry, with their address as key</returns>
        Dictionary<string, Document> GetAllDoc();
    }
}
