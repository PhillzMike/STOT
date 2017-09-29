using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Engine
{

    /// <summary>
    /// Author Teni
    /// Don't forget to set Querir.invt before you begin
    /// </summary>
    public static class Querier
    {
        static Stopwatch sw = new Stopwatch();
        //TODO remove stopwatch
        static double t7, t8, t10;
        static bool needsRanking;
        public static Inverter Invt { set { invt = value; } }
        static Inverter invt;
        public static List<Document> Search(String query)
        {
            sw.Start();
            Semanter.Splitwords(query, ":").ToList();
            double t0 = sw.ElapsedMilliseconds;
            //Separate words, remove punctiations,make lowercase
            List<String> words = Semanter.Splitwords(query, ":").ToList();
            double t = sw.ElapsedMilliseconds;
            //Obtains possible types searched by this code
            //methods take to long to load y?HashSet<String> typesPossible=TypeChecker(words,invt,t);
            HashSet<String> typesPossible = new HashSet<string>();
            for (int k = 0; k < words.Count; k++)
            {
                if (words[k].Equals("type"))
                {
                    if ((k + 2) < words.Count && words[k + 1].Equals(":"))
                    {
                        if (invt.Formats.ContainsKey(words[k + 2]))
                            typesPossible.UnionWith(invt.Formats[words[k + 2]]);
                        words.RemoveRange(k, 3);
                        k--;
                    }
                    else if ((k + 1) < words.Count && words[k + 1].StartsWith(":"))
                    {
                        if (invt.Formats.ContainsKey(words[k + 1].Substring(1)))
                            typesPossible.UnionWith(invt.Formats[words[k + 1].Substring(1)]);
                        words.RemoveRange(k, 2);
                        k--;
                    }
                }
                else if ((k + 1) < words.Count && words[k].Equals("type:"))
                {
                    if (invt.Formats.ContainsKey(words[k + 1]))
                        typesPossible.UnionWith(invt.Formats[words[k + 1]]);
                    words.RemoveRange(k, 2);
                    k--;
                    //words[k] = words[k + 1] = "";
                }
                else if (words[k].StartsWith("type:"))
                {
                    if (invt.Formats.ContainsKey(words[k].Substring(5)))
                        typesPossible.UnionWith(invt.Formats[words[k].Substring(5)]);
                    words.RemoveAt(k);
                    k--;
                }
            }
            double t1 = sw.ElapsedMilliseconds;
            //Fill with all types if no types possible found
            if (typesPossible.Count == 0)
                typesPossible.UnionWith(invt.Formats[""]);
            double t2 = sw.ElapsedMilliseconds;
            //Stem words and remove stopwords
            // slower words = words.Except(invt.Stopwords).ToList();
            List<String> splitwords = new List<String>();
            string stem;
            foreach (string word in words)
            {
                stem = invt.Samantha.StemWord(word);
                if (!(invt.Stopwords.Contains(stem)))
                    splitwords.Add(stem);
            }
            double t3 = sw.ElapsedMilliseconds;
            if (splitwords.Count == 0)
                return new List<Document>();
            //search for documents
            Dictionary<Document, Dictionary<string, List<int>>> Results = DocsFound(splitwords, typesPossible);
            if (!needsRanking)
                return Results.Keys.ToList();
            //throw new ArgumentNullException("File doesn't Exist");
            //TODO "words within quote"
            double t4 = sw.ElapsedMilliseconds;
            return Ranker.SearchQuery(splitwords, Results, invt.DocumentCount);
        }
        public static List<String> AutoComplete(String querywords)
        {
            //TODO review this later
            String[] splitwords = querywords.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            //  return Semanter.Suggestions(splitwords);
            return null;
        }

        private static Dictionary<Document, Dictionary<string, List<int>>> DocsFound(List<String> querywords, HashSet<string> typesPossible)
        {
            needsRanking = false;
            t7 = sw.ElapsedMilliseconds;
            var found = new Dictionary<Document, Dictionary<string, List<int>>>();
            t8 = sw.ElapsedMilliseconds;
            HashSet<Document> available = new HashSet<Document>();
            foreach (string word in querywords)
                available.UnionWith(invt.AllDocumentsContainingWord(word));
            foreach (Document x in available)
            {
                if (typesPossible.Contains(x.Type) && x.Exists)
                {
                    Dictionary<string, List<int>> wordDict = new Dictionary<string, List<int>>();
                    foreach (string word in querywords)
                    {

                        if (invt.WordIsInDoc(word, x))
                        {
                            if (!wordDict.ContainsKey(word))
                            {
                                wordDict.Add(word, invt.PositionsWordOccursInDocument(word, x).ToList());
                            }
                        }
                        else
                        {
                            if (!wordDict.ContainsKey(word))
                            {
                                wordDict.Add(word, new List<int>());
                            }
                        }

                    }
                    found.Add(x, wordDict);
                }
            }
            if (found.Keys.Count > 1)
            {
                needsRanking = true;
            }
            t10 = sw.ElapsedMilliseconds;
            return found;
        }

    }           
}
