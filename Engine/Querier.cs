using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// Author Teni
    /// </summary>
    public static class Querier
    { 
        
        public static List<Document> Search(String query,Inverter invt) {
           List<Format> typesPossible = new List<Format>();
           String [] words = query.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            //TODO autocorrect shii
           for(int i = 0;i < words.Length;i++) {
                words[i] = invt.Samantha.StemWord(words[i].ToLower().Trim());
           }
           List<String> splitwords = new List<String>();
           typesPossible = PossibleType(TypeChecker(words));
           DocsFound(splitwords, typesPossible,invt);
           
            for (int k= 0; k<words.Length; k++)
            {
                if (!(invt.Stopwords.Contains(words[k]) || words[k].Equals("")))
                {
                    splitwords.Add(words[k]);
                   }
                }
            if (splitwords.Count == 0)
                return DocsFound(splitwords, typesPossible, invt).Keys.ToList<Document>();
            //throw new ArgumentNullException("File doesn't Exist");
            //TODO addwrong words to Dictionary
            //TODO search for all wrong words corrections even if the word is correct
            return Ranker.SearchQuery(splitwords, DocsFound(splitwords, typesPossible,invt),invt.DocumentCount);
        }
        public static List<String> AutoComplete(String querywords)
        {
            //TODO review this later
            String[] splitwords = querywords.Split(Semanter.punctuations, StringSplitOptions.RemoveEmptyEntries);
            // return Semanter.Suggestions(splitwords);
            return null;
        }

        private static Dictionary<Document, Dictionary<string,List<int>>> DocsFound(List<String> querywords, List<Format> typesPossible, Inverter invt) {
        Dictionary<Document, Dictionary<string,List<int>>> found = new Dictionary<Document, Dictionary<string,List<int>>>();
            //double[] count = new double[querywords.Count];
            //for (int t = 0; t < querywords.Count; t++) {
            //    count[t] = 0;
            //}
            
            foreach (string word in querywords) {
                
                try {
                    Document[] available = invt.AllDocumentsContainingWord(word);
                    foreach (var item in available) {
                        if (!found.ContainsKey(item)) {
                            found.Add(item, new Dictionary<string, List<int>>());
                        }
                        found[item].Add(word, invt.PositionsWordOccursInDocument(word,item).ToList());
                    }
                }
                catch (KeyNotFoundException) {
                }
            }
            if (querywords.Count<=0) {
                List<Document> available = invt.Files.Values.ToList<Document>();
                foreach (var item in available) {
                    if (!found.ContainsKey(item)) 
                        found.Add(item, new Dictionary<string,List<int>>());
                }
            }
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
                }         
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
