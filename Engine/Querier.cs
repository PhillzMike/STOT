using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Engine
{
    
    /// <summary>
    /// Author Teni
    /// </summary>
    public static class Querier
    {
        static Stopwatch sw = new Stopwatch();
        static double t7, t8, t9, t10;
        //public static List<String> stopwords = File.ReadAllLines("../../../../engine/stopwords.txt").ToList<string>();

        public static List<Document> Search(String query,Inverter invt) {
            sw.Start();
            List<Format> typesPossible = new List<Format>();
           String [] words = query.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            double t1 = sw.ElapsedMilliseconds;
            //TODO autocorrect shii
           for( int j = 0;j < words.Length;j++) {
                words[j] = invt.Samantha.StemWord(words[j].ToLower().Trim());
           }
            double t2= sw.ElapsedMilliseconds;
            List<String> splitwords = new List<String>();
            //TODO  Dont call typepossible when not needed
           typesPossible = PossibleType(TypeChecker(words));
            //Useless lineDocsFound(splitwords, typesPossible,invt);
            double t3 = sw.ElapsedMilliseconds;
            for (int k= 0; k<words.Length; k++)
            {
                if (!(invt.Stopwords.Contains(words[k]) || words[k].Equals("")))
                {
                    splitwords.Add(words[k]);
                   }
                }
            double t4 = sw.ElapsedMilliseconds;
            if (splitwords.Count == 0)
                return DocsFound(splitwords, typesPossible, invt)[""].Keys.ToList<Document>();
            //throw new ArgumentNullException("File doesn't Exist");
            //TODO "words within quote"
            //TODO addwrong words to Dictionary
            //TODO search for all wrong words corrections even if the word is correct
            double t5 = sw.ElapsedMilliseconds;
            List<Document> i = Ranker.SearchQuery(splitwords, DocsFound(splitwords, typesPossible,invt),invt.DocumentCount);
            double t6 = sw.ElapsedMilliseconds;
            return null;
        }
        public static List<String> AutoComplete(String querywords)
        {
            //TODO review this later
            String[] splitwords = querywords.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            // return Semanter.Suggestions(splitwords);
            return null;
        }

        private static Dictionary<string, Dictionary<Document,List<int>>> DocsFound(List<String> querywords, List<Format> typesPossible, Inverter invt) {
            t7 = sw.ElapsedMilliseconds;
            var found = new Dictionary<string, Dictionary<Document, List<int>>>();
            //double[] count = new double[querywords.Count];
            //for (int t = 0; t < querywords.Count; t++) {
            //    count[t] = 0;
            //}
            t8 = sw.ElapsedMilliseconds;
            foreach (string word in querywords) {
                    Document[] available = invt.AllDocumentsContainingWord(word);
                    if (available.Length == 0)
                    {
                        found.Add(word, new Dictionary<Document, List<int>>());
                    }
                    foreach (var item in available) {
                        if (!found.ContainsKey(word)) {
                            found.Add(word, new Dictionary<Document, List<int>>());
                        }
                        found[word].Add(item, invt.PositionsWordOccursInDocument(word,item).ToList());
                    }
            }
            t9 = sw.ElapsedMilliseconds;
            if (querywords.Count<=0) {
                List<Document> available = invt.Files.Values.ToList<Document>();
                found.Add("", new Dictionary<Document, List<int>>());
                foreach (var item in available) {
                    if (!found[""].ContainsKey(item)) 
                        found[""].Add(item, new List<int>());
                }
            }
            t10 = sw.ElapsedMilliseconds;
            //for(int i=0; i<querywords.Count; i++)
            //{
            //    List<Document> available = invt.Table[querywords[i]].Keys.ToList();
            //    foreach (Document t in available) {
            //        count[i] = invt.Table[querywords[i]][t].Count;
            //        if (typesPossible.Count > 0) {
            //            if (typesPossible.Contains(t.Type)) {
            //                found.Add(t, count);
            //            }
            //        }
            //        else {
            //            found.Add(t, count);
            //        }
            //    }
            //}
           return found;
        }
        public static String TypeChecker(String [] s)
        { 
        for (int i = 0; i < s.Length; i++) {
                if (s[i].ToLower().Equals("type"))
                { if (s[i + 1].Equals(":"))
                     {
                      string type = s[i + 2];
                      s[i] = s[i + 1] = s[i + 2] = "";
                      return type;  
                    }
                }//TODO Else return null         
            }
            return "";
    }
        public static List<Format> PossibleType(string s)
        {
            List<Format> typesPossible = new List<Format>();
            foreach ( string m in Enum.GetNames(typeof(Format))){
                bool teni = true;
                for (int i = 0; i < s.Length; i++) {
                  if (!(s[i] == m[i]))
                {
                        teni = false; 
               }     
             }
                if (teni)
                {
                    Enum.TryParse<Format>(m, out Format doctype);
                    typesPossible.Add(doctype);
                }
            }
            return typesPossible;
        }
        

    }
}
