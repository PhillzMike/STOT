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
        /// Searches the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="Results">The results.</param>
        /// <returns> A list of documents in descending order of relevance</returns>
        public static List<Document> SearchQuery(List<String> query, Dictionary<Document,double[]> Results) {
            
            foreach (Document item in Results.Keys) {
                double[] x = Results[item];
                for(int i = 0; i < x.Length; i++) {
                    x[i] = TfWeight(x[i]) * IDFWeight(Results.Keys.Count);
                }

            }
            
            return null;
        }
        private static List<int> Gun(List<string> a) {
            SortedSet<string> m = new SortedSet<string>(a);
            return null;
        }
        private static double TfWeight(double count) {
            if (count == 0)
                return 0;
            else
                return (1 + Math.Log(count));
        }
        private static double IDFWeight(int noOfDocuments) {
            return Math.Log(InvertedIndexer.DocumentCount/noOfDocuments);
        }
    }
}
