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
        public static List<String> stopwords = File.ReadAllLines("../../../../engine/stopwords.txt").ToList<string>();
        
        public static List<Document> Search(String query) {

            //Add all the documents found to a list \
            //return Ranker.SearchQuery(,);
            //type: bring out document of possible type
            //type must be followed by :, no matter the number of whitespaces.
           List<Format> typesPossible = new List<Format>();
           String [] words = query.Split((new char[]{' '}), StringSplitOptions.RemoveEmptyEntries);
           List<String> splitwords = new List<String>();
           typesPossible = PossibleType(TypeChecker(words));
           DocsFound(splitwords, typesPossible);
           
            for (int k= 0; k<words.Length; k++)
            {
                if (!(stopwords.Contains(words[k]) || words[k].Equals("")))
                {
                    splitwords.Add(words[k]);
                   }
                }
            if (splitwords.Count == 0)
                throw new ArgumentNullException("File doesn't Exist");
            return Ranker.SearchQuery(splitwords, DocsFound(splitwords, typesPossible));
        }
        public static List<String> AutoComplete(List<String> querywords)
        {
            //TODO review this later
            return Semanter.Suggestions(querywords);
          
        }

        private static Dictionary<Document, double[]> DocsFound(List<String> querywords, List<Format> typesPossible)
        {
            Dictionary<Document, double []> found = new Dictionary<Document, double[]>();
            double [] count = new double[querywords.Count];
            for(int t =0; t<querywords.Count; t++)
            {
                count[t] = 0;
            }
       
            for(int i=0; i<querywords.Count; i++)
            { 
               List<Document> available =  Inverter.Table[querywords[i]].Keys.ToList();
                foreach (Document t in available) {
                    count[i] = Inverter.Table[querywords[i]][t].Count;
                    if (typesPossible.Count>0)
                    {
                        if (typesPossible.Contains(t.Type))
                        {
                            found.Add(t, count);
                        }
                    }
                    else
                    {
                        found.Add(t, count);
                    }
              }
            }
             return found;
        }
        private static String TypeChecker(String [] s)
        { 
        for (int i = 0; i < s.Length; i++) {
                if (s[i].ToLower().Equals("type"))
                {
                    if (s[i + 1].Equals(":"))

                    {
                      string type = s[i + 2];
                      s[i] = s[i + 1] = s[i + 2] = "";
                      return type;  
                    }
                }         
            }
            return "";
    }
        private static List<Format> PossibleType(string s)
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
