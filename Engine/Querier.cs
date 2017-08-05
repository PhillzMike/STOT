﻿using System;
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

        public static string type;
        public static List<Document> Search(String query) {
           
            //ignore stop words \
            //Separate query into list of words\
            //InvertedIndexer.Table[query].Values\
            //Add all the documents found to a list \
            //return Ranker.SearchQuery(,);
            //type: bring out document of possible type
            //type must be followed by :, no matter the number of whitespaces.
           String [] words = query.Split((new char[]{' '}), StringSplitOptions.RemoveEmptyEntries);
           List<String> splitwords = new List<String>();
           var stopwords = File.ReadAllLines("../../../../engine/stopwords.txt");
           var stpwordlist = new List<String>(stopwords);
           for(int k= 0; k<words.Length; k++)
            {
                if (!stpwordlist.Contains(words[k]))
                {
                    type = TypeChecker(words);
                }
                else
                { splitwords.Add(words[k]); }
                    
                    splitwords.Add(words[k]);
                }
                
            
           // DocsFound(splitwords);
            return null;
        }
        public static List<String> AutoComplete(String query)
        {
            //return Semanter.Suggestions
            return null;
        }

        private static Dictionary<Document, double[]> DocsFound(List<String> querywords)
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
                    found.Add(t, count);
              }
            }
             return found;
        }
        public  static string TypeChecker(String [] s)
        { 
        for (int i = 0; i < s.Length; i++) {
                if (s[i] == "type")
                {
                    if (s[i + 1] == ":")
                    { return s[i + 2]; }
                }
            }
            return "";
    }
        //giving 
        //Types : pdf
        //Tokenize query
        //Search Query
        //Autocomplete Query

    }
}
