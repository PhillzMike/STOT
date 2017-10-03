using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Engine {

    /// <summary>
    /// Author Teni
    /// Don't forget to set Querier.invt before you begin
    /// </summary>
    public static class Querier {
        public static Inverter Invt { get { return invt; } set { invt = value; } }
        static Inverter invt;
        public static List<Document> Search(String query) {
            //Separate words, remove punctiations,make lowercase
            List<String> words = Semanter.Splitwords(query, ":").ToList();
            //Obtains possible types searched by this query
            HashSet<String> typesPossible=TypeChecker(words);
            //Stem words and remove stopwords
            //slower method words = words.Except(invt.Stopwords).ToList();
            List<String> splitwords = new List<String>();
            string stem;
            foreach (string word in words) {
                stem = invt.Samantha.StemWord(word);
                if (!(invt.Stopwords.Contains(stem)))
                    splitwords.Add(stem);
            }
            if (splitwords.Count == 0)
                return new List<Document>();
            //search for documents
            Dictionary<Document, Dictionary<string, List<int>>> Results = DocsFound(splitwords, typesPossible);
            if (Results.Keys.Count<2)
                return Results.Keys.ToList();
            return Ranker.SearchQuery(splitwords, Results, invt.DocumentCount);
        }
        public static List<String> AutoComplete(String querywords,int noOfResults) {
            return invt.Samantha.Suggestions(querywords,noOfResults);
        }

        private static Dictionary<Document, Dictionary<string, List<int>>> DocsFound(List<String> querywords, HashSet<string> typesPossible) {
            var found = new Dictionary<Document, Dictionary<string, List<int>>>();
            Dictionary<string,Dictionary<Document, List<int>>> available = new Dictionary<string, Dictionary<Document, List<int>>>();
            HashSet<Document> availableDocs = new HashSet<Document>(new DocumentComparer());
            String QueryToTrie = "";
            foreach (string word in querywords) {
                QueryToTrie += word + " ";
                if (!available.ContainsKey(word)) { 
                    Dictionary<Document, List<int>> thisWords = invt.AllDocumentsPositionsContainingWord(word);

                available.Add(word, thisWords);
                    availableDocs.UnionWith(thisWords.Keys);
                }
                
            }

            foreach (Document x in availableDocs) {
                if (typesPossible.Contains(x.Type) && x.Exists) {
                    Dictionary<string, List<int>> wordDict = new Dictionary<string, List<int>>();
                    foreach (string word in querywords) {


                        if (available[word].ContainsKey(x)) {
                            if (!wordDict.ContainsKey(word)) {
                                wordDict.Add(word, available[word][x]);
                            }
                        } else {
                            if (!wordDict.ContainsKey(word)) {
                                wordDict.Add(word, new List<int>());
                            }
                        }

                    }
                    found.Add(x, wordDict);
                }
            }
            return found;
        }
        static HashSet<String> TypeChecker(List<string> words) {
            HashSet<String> typesPossible = new HashSet<string>();
            for (int k = 0; k < words.Count; k++) {
                if (words[k].Equals("type")) {
                    if ((k + 2) < words.Count && words[k + 1].Equals(":")) {
                        if (invt.Formats.ContainsKey(words[k + 2]))
                            typesPossible.UnionWith(invt.Formats[words[k + 2]]);
                        words.RemoveRange(k, 3);
                        k--;
                    } else if ((k + 1) < words.Count && words[k + 1].StartsWith(":")) {
                        if (invt.Formats.ContainsKey(words[k + 1].Substring(1)))
                            typesPossible.UnionWith(invt.Formats[words[k + 1].Substring(1)]);
                        words.RemoveRange(k, 2);
                        k--;
                    }
                } else if ((k + 1) < words.Count && words[k].Equals("type:")) {
                    if (invt.Formats.ContainsKey(words[k + 1]))
                        typesPossible.UnionWith(invt.Formats[words[k + 1]]);
                    words.RemoveRange(k, 2);
                    k--;
                } else if (words[k].StartsWith("type:")) {
                    if (invt.Formats.ContainsKey(words[k].Substring(5)))
                        typesPossible.UnionWith(invt.Formats[words[k].Substring(5)]);
                    words.RemoveAt(k);
                    k--;
                }
            }
            if (typesPossible.Count == 0)
                typesPossible.UnionWith(invt.Formats[""]);
            return typesPossible; 
        }

    }
}
