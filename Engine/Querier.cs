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
           var stopwords = File.ReadAllLines("stopwords.txt");
           var stpwordlist = new List<String>(stopwords);
           for(int k= 0; k<words.Length; k++)
            {
                if (!stpwordlist.Contains(words[k]))
                {  
                    if (words[k] == "type")
                    {  if (words[k++] == ":")
                        { string type = words[k + 1]; }
                        else
                        { splitwords.Add(words[k]); }
                    }
                    splitwords.Add(words[k]);
                   }
                }
             
          
            DocsFound(splitwords);
            return null;
        }
        public static List<String> AutoComplete(String query)
        {
            //return Semanter.Suggestions
            return null;
        }

        private static Dictionary<Document, double[]> DocsFound(List<String> querywords)
        {
            Dictionary<Document, double[]> found = new Dictionary<Document, double[]>();
            foreach (string t in querywords)
            {
                
            
            }
             return found;
        }
        //giving 
        //Types : pdf
        //Tokenize query
        //Search Query
        //Autocomplete Query

    }
}
