using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Engine
{
    /// <summary>
    /// Author Timi
    /// Gets Results to the Query and ranks them by relevance.
    /// </summary>
    public static class Ranker
    {
        private static Stopwatch sw = new Stopwatch();
        //TODO Optimize Ranker, takes way too much time
        private static Dictionary<Document, Dictionary<string, List<int>>> Results;
        private static double noOfWordsWeight = 0.5;
        private static double consecutiveWeight = 0.5;
        private static int howFarApartTheWordsCanBe = 2;
        /// <summary>
        /// Searches the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="Results">A dictionary whose keys are documents and values, an array of the frequencies (in ascending order) each word in the query is found in this document.</param>
        /// <returns> A list of documents in descending order of relevance</returns>
        public static List<Document> SearchQuery(List<String> query, Dictionary<Document, Dictionary<string, List<int>>> Results,int documentCount) {
            //TODO: add a feature that makes searching for a particular document by name possible
            //Calculating Tf-Idf wieghting of each word in a document
            sw.Start();
            double t = sw.ElapsedMilliseconds;
            Ranker.Results = Results;
            Dictionary<Document, double[]> CountOfAllWords = new Dictionary<Document, double[]>();
            //TODO Fix Idf wieghting
            double t2 = sw.ElapsedMilliseconds;
            foreach (Document doc in Results.Keys) {
                Dictionary<string, List<int>> positions = Results[doc];
                CountOfAllWords.Add(doc, new double[query.Count]);
                for(int i = 0;i<query.Count;i++) {
                    CountOfAllWords[doc][i] = TfWeight(positions[query[i]].Count) * IDFWeight(Results.Keys.Count, documentCount);
                }
            }

            double t4 = sw.ElapsedMilliseconds;
            //Calculating Tf-Idf weighting of each word in the query
            double[] queryVector = GetVector(query);
            for(int i = 0; i < queryVector.Length; i++) {
                queryVector[i] = TfWeight(queryVector[i]) * IDFWeight(CountOfAllWords.Count,documentCount);
            }
            double t5 = sw.ElapsedMilliseconds;
            //Calculating the cosine of the angles between the query vector and the document vectors
            List<double> cosOfAngle = new List<double>();
            List<double> secondGuy = new List<double>();
            //List<Document> sortedDocument = new List<Document>(CountOfAllWords.Keys);
            List<Document> sortedDocument = new List<Document>();

            foreach (Document item in CountOfAllWords.Keys) {
                cosOfAngle.Add((GetCOSAngle(queryVector, CountOfAllWords[item])) * noOfWordsWeight);
                secondGuy.Add(GetScoreBasedOnPos(query, item));
                cosOfAngle[cosOfAngle.Count - 1] += secondGuy[secondGuy.Count - 1] * consecutiveWeight;
                sortedDocument.Add(item);
            }
            double t9 = sw.ElapsedMilliseconds;
            //double t6 = sw.ElapsedMilliseconds;
            //Sorting the documents based on the cosine of the angles using insertion sort
            for (int i = 1; i < cosOfAngle.Count; i++) {
                double key = cosOfAngle[i];
                Document docKey = sortedDocument[i];
                int j = i - 1;
                while (j >= 0 && cosOfAngle[j] < key) {
                    cosOfAngle[j + 1] = cosOfAngle[j];
                    sortedDocument[j + 1] = sortedDocument[j];
                    j--;
                }
                cosOfAngle[j + 1] = key;
                sortedDocument[j + 1] = docKey;
            }
            double t10 = sw.ElapsedMilliseconds;
            return sortedDocument;
        }
        //private static bool IsIt(List<string> query) {
        //    return (query.Count > 1) && query[0].StartsWith("\"") && query[query.Count - 1].EndsWith("\"");
        //}
        private static double GetCOSAngle(double[] queryVector, double[] docVector) {
            double sum = 0;
            double fD = 0;
            double sD = 0;
            for (int i = 0; i < queryVector.Length; i++) {
                sum += queryVector[i] * docVector[i];
                fD += queryVector[i] * queryVector[i];
                sD += docVector[i] * docVector[i];
            }
            return sum / (Math.Sqrt(fD * sD));
        }
        /// <summary>
        /// Gets the score based on positions of elements in the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="doc">The document.</param>
        /// <returns> a score between 0 and 1 based on the positions of elements in the query or 0 if the query contains only one element</returns>
        private static double GetScoreBasedOnPos(List<string> query,Document doc) {
            if (query.Count == 1)
                return 0;
            double degree = (45.0 /(query.Count - 1)) * GetBest(query,Results[doc],doc);
            double score = Math.Tan((degree*Math.PI)/180);
            return score;
        }
        //private static double ScoreBasedOnConWords(List<string> query,Document doc) {
        //    double t7 = sw.ElapsedMilliseconds;
        //    var positions = Results[doc];
        //    return GetBest(query,positions,doc);
        //}
        //TODO Change the return type to a tuple of the score and d pos the best match was found
        private static double GetBest(List<string> query, Dictionary<string, List<int>> dic, Document doc) {
            int count = 0;
            string firstWord = query[count];
            while (dic[firstWord].Count == 0)
                firstWord = query[++count];
            double finalScore = -1;
            double score;
            for(int i = 0; i < dic[firstWord].Count; i++) {
                score = GetSum(GetBestDiff(firstWord, query, dic, i));
                if (finalScore < score) {
                    finalScore = score;
                    doc.Relevance = dic[firstWord][i].ToString();
                }
                finalScore = (finalScore > score) ? finalScore : score;
            }
            double t8 = sw.ElapsedMilliseconds;
            return finalScore;
        }
        private static int[] GetBestDiff(string first, List<string> query, Dictionary<string, List<int>> dic, int pos) {
            //Using the first word in the query that appears at position pos to calculate best difference
            bool found = false;
            int[] output = new int[query.Count];
            for (int i = 1; i < query.Count; i++) {
                int min = int.MaxValue;
                int absoluteDiff;
                for (int j = 0; j < dic[query[i]].Count; j++) {
                    found = true;
                    absoluteDiff = Math.Abs(dic[first][pos] - dic[query[i]][j]);
                    if (absoluteDiff >= howFarApartTheWordsCanBe*query.Count) {
                        break;
                    }
                    min = (min < absoluteDiff) ? min : absoluteDiff;
                }
                if (found)
                    output[i] = min;
            }

            return output;
        }
        private static double GetSum(int[] scores) {
            //trying to get the best score
            double sum = 0;
            int prevWord = 0;
            for (int i = 1; i < scores.Length; i++) {
                if (scores[i] != 0) {
                    //If the relative position of 2 differnt guyz from the refernce word is the same
                    if (scores[i] == scores[prevWord])
                        scores[prevWord] *= -1;
                    sum += (1.0 / (Math.Abs(scores[i] - scores[prevWord])) * (i - prevWord));
                    prevWord = i;
                }
                    
            }
            return sum;
        }
        private static double[] GetVector(List<string> a) {
            var voice = new List<double>();
            var m = new HashSet<string>(a);
            foreach (string f in m) {
                voice.Add(a.FindAll(x => x == f).Count);
            }
            return voice.ToArray();
        }
        private static double TfWeight(double count) {
            return (count == 0) ? 0 : 1 + Math.Log(count);
        }
        private static double IDFWeight(int noOfDocuments,int documentCount) {
            return (noOfDocuments > 0) ? Math.Log(documentCount/noOfDocuments) : 0;
        }
        //TODO use Code contract here
        /// <summary>
        /// Gets or sets the weight given based on the number of the types the words occur in the document
        /// </summary>
        /// <value>
        /// The weight given between 0 and 1
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when a value less than 0 or greater than 1 is given has the weight</exception>
        public static double NoOfWordsWeight {
            get => noOfWordsWeight;
            set {
                if (value <= 0 && value >= 1) { 
                    noOfWordsWeight = value;
                    consecutiveWeight = 1 - value;
                }else
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Gets weight given based on the how the words in the query appear in the documents
        /// </summary>
        /// <value>
        /// The consecutive weight.
        /// </value>
        public static double ConsecutiveWeight { get => consecutiveWeight;}
        /// <summary>
        /// Gets or sets the how far apart the words could be.
        /// </summary>
        /// <value>
        /// the number of words in between the words
        /// </value>
        public static int HowFarApartTheWordsCanBe { get => howFarApartTheWordsCanBe; set => howFarApartTheWordsCanBe = value; }
    }

}
