using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Engine
{
    /// <summary>
    /// Author Timi
    /// Gets Results to the Query and ranks them by relevance.
    /// </summary>
    public static class Ranker
    {
        /// <summary>
        /// Searches the inverted Index for the shii and then ranks them, The 1st guy is the most relevant
        /// </summary>
        /// <param name="query">The query. File type still to be determined</param>
        /// <returns></returns>
        public static List<Document> SearchQuery(List<String> query, List<Document> Results) {
            return null;
        } 
    }
}
