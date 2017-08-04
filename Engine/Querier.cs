using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Engine
{
    /// <summary>
    /// Author Teni
    /// </summary>
    public static class Querier
    {
     

        public static List<Document> Search(String query) {
            //ignore stop words
            //Separate query into list of words
            //InvertedIndexer.Table[query].Values
            //Add all the documents found to a list 
            //return Ranker.SearchQuery(,);
            //type: bring out document of possible type
            //type must be followed by :, no matter the number of whitespaces.
           String [] words = query.Split(' ');
           List<String> splitwords = new List<String>();
           var stopwords = File.ReadAllLines("stopwords.txt");
           var stpwordlist = new List<String>(stopwords);
            foreach (string k in words)
            {
                if(!stpwordlist.Contains(k))
                   splitwords.Add(k);
            }

            return null;
        }
        public static List<String> AutoComplete(String query)
        {
            //return Semanter.Suggestions
            return null;
        }

        private static List<Document> DocsFound(List<String> querywords)
        {
            //List<Document> found = new List<Document>();
            //foreach (string t in querywords)
            //{
            //   //ound = InvertedIndexer.Table[t].Values.
 
            //}
            return found;
        }
        //Types : pdf
        //Tokenize query
        //Search Query
        //Autocomplete Query

        //teni is a goat g
    }
}
