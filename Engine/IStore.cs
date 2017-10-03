﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine {
    public interface IStore {
        /// <summary>
        /// Checks the word in document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        bool CheckWordInDoc(string word, Document doc);
        /// <summary>
        /// Alls the words in table.
        /// </summary>
        /// <returns></returns>
        string[] AllWordsInTable();
        /// <summary>
        /// Deletes the specified document.
        /// </summary>
        /// <param name="doc">The document.</param>
        void Delete(Document doc);
        /// <summary>
        /// Removes the document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        void RemoveDocument(string word, Document doc);
        /// <summary>
        /// Adds the word to table.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <param name="i">The i.</param>
        void AddWordToTable(String word, Document doc, int i);
        /// <summary>
        /// Alls the documents containing word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        Document[] AllDocumentsContainingWord(string word);
        /// <summary>
        /// Positionses the word occurs in document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        int[] PositionsWordOccursInDocument(string word, Document doc);
        /// <summary>
        /// Wordses the positions.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        Dictionary<Document, List<int>> WordsPositions(string word);


    }
}
