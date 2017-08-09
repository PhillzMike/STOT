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
        /// <param name="Results">A dictionary whose keys are documents and values, an array of the frequencies each word in the query is found in this document.</param>
        /// <returns> A list of documents in descending order of relevance</returns>
        public static List<Document> SearchQuery(List<String> query, Dictionary<Document,double[]> Results) {
            for (int i = 0; i < query.Count; i++) {
                query[i] = query[i].ToLower();
            }
            //Calculating Tf-Idf wieghting of each word in a document
            foreach (Document item in Results.Keys) {
                double[] x = Results[item];
                for(int i = 0; i < x.Length; i++) {
                    x[i] = TfWeight(x[i]) * IDFWeight(Results.Count);
                }

            }
            //Calculating Tf-Idf weighting of each word in the query
            double[] queryVector = GetVector(query);
            for(int i = 0; i < queryVector.Length; i++) {
                queryVector[i] = TfWeight(queryVector[i]);// * IDFWeight(Results.Count);
            }
            //Calculating the cosine of the angles between the query vector and the document vectors
            double[] cosOfAngle = new double[queryVector.Length];
            int counter = 0;
            List<Document> sortedDocument = new List<Document>();
            foreach (Document item in Results.Keys) {
                cosOfAngle[counter++] = GetCOSAngle(queryVector, Results[item]);
                sortedDocument.Add(item);
            }
            //Sorting the documents based on the cosine of the angles
            for(int i = 1; i < cosOfAngle.Length; i++) {
                double key = cosOfAngle[i];
                Document docKey = sortedDocument[i];
                int j = i - 1;
                while(j>0 && cosOfAngle[j] > key) {
                    cosOfAngle[j + 1] = cosOfAngle[j];
                    sortedDocument[j + 1] = sortedDocument[j];
                    j--;
                }
                cosOfAngle[j + 1] = key;
                sortedDocument[j + 1] = docKey;
            }
            return sortedDocument;
        }
        private static double GetCOSAngle(double[] queryVector, double[] docVector) {
            double sum = 0;
            double fD = 0;
            double sD = 0;
            for (int i = 0; i < queryVector.Length; i++) {
                sum += queryVector[i] * docVector[i];
                fD += Math.Pow(queryVector[i], 2);
                sD += Math.Pow(docVector[i], 2);
            }
            fD = Math.Sqrt(fD);
            sD = Math.Sqrt(sD);
            return sum / (fD * sD);
        }
        private static double GetScoreBasedOnPos(List<string> query) {
            List<Dictionary<Document,List<int>>> positions = new List<Dictionary<Document, List<int>>>();
            foreach (var item in query) {
                positions.Add(Inverter.Table[item]);
            }
              
            return 0;
        }
        public static double[] GetVector(List<string> a) {
            List<double> voice = new List<double>();
            HashSet<string> m = new HashSet<string>(a);
            int counter;
            foreach (string f in m) {
                counter= 0;
                for(int i = 0; i < a.Count; i++) {
                    if(f.Equals(a[i])) {
                        counter++;
                    }
                }
                voice.Add(counter);
            }
            return voice.ToArray();
        }
        public static double TfWeight(double count) {
            return (count == 0) ? 0 : 1 + Math.Log(count);
        }
        private static double IDFWeight(int noOfDocuments) {
            return Math.Log(Inverter.DocumentCount/noOfDocuments);
        }
    }
}
