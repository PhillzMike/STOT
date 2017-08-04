using System;
using System.Collections.Generic;
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
            return null;
        }
        public static List<String> AutoComplete(String query)
        {
            //return Semanter.Suggestions
            return null;
        }
        //Types : pdf
        //Tokenize query
        //Search Query
        //Autocomplete Query

        //teni is a goat g
    }
}
