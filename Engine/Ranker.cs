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
        public static List<Document> SearchQuery(List<String> query, Dictionary<Document,double[]> Results,Inverter invt) {
            //TODO we need to index every word in a document except the stop words
            //TODO: add a feature that makes searching for a particular document by name possible
            //TODO: Remember to tell Teni to review her DocsFound method
            //for (int i = 0; i < query.Count; i++) {
            //    query[i] = query[i].ToLower();
            //}
            //Calculating Tf-Idf wieghting of each word in a document
            foreach (Document item in Results.Keys) {
                double[] x = Results[item];
                for(int i = 0; i < x.Length; i++) {
                    x[i] = TfWeight(x[i]) * IDFWeight(Results.Count,invt);
                }

            }
            //Calculating Tf-Idf weighting of each word in the query
            
            double[] queryVector = GetVector(query);
            for(int i = 0; i < queryVector.Length; i++) {
                //TODO: Check the tf-idf weighting of the query
                queryVector[i] = TfWeight(queryVector[i]) * IDFWeight(Results.Count, invt);
            }
            //Calculating the cosine of the angles between the query vector and the document vectors
            //double[] cosOfAngle = new double[queryVector.Length];
            List<double> cosOfAngle = new List<double>();
            int counter = 0;
            List<double> secondGuy = new List<double>();
            List<Document> sortedDocument = new List<Document>();
            //SortedDictionary<double, Document> sortedDocument2 = new SortedDictionary<double, Document>();
            foreach (Document item in Results.Keys) {
                cosOfAngle.Add((GetCOSAngle(queryVector, Results[item]))*0.7);
                secondGuy.Add(GetScoreBasedOnPos(query, item, invt));
                cosOfAngle[cosOfAngle.Count-1] += secondGuy[secondGuy.Count-1] * 0.3;
                //sortedDocument2.Add(cosOfAngle[counter++], item);
                sortedDocument.Add(item);
            }

            //Sorting the documents based on the cosine of the angles
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
            //foreach (var item in sortedDocument2.Keys) {
            //    sortedDocument.Add(sortedDocument2[item]);
            //}
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
        private static double GetScoreBasedOnPos(List<string> query,Document doc,Inverter invt) {
            double degree = (45.0 /(query.Count - 1)) * ScoreBasedOnConWords(query, doc, invt);
            double score = Math.Tan((degree*Math.PI)/180);
            return score;
        }
        private static double ScoreBasedOnConWords(List<string> query,Document doc,Inverter invt) {
            double finalScore = 0;
            Dictionary<string, List<int>> positions = new Dictionary<string,List<int>>();
            foreach (var item in query) {
                //TODO Check this guy
                if (invt.Table.ContainsKey(item) && invt.Table[item].ContainsKey(doc))
                    positions.Add(item, new List<int>(invt.Table[item][doc]));
                else
                    positions.Add(item, new List<int>());
            }
            var wordsDistance = CalculateBestDiff(query, positions);
            int prevWord = -1;
            for (int i = 0; i < wordsDistance.Count; i++) {
                if(wordsDistance[i] != 0) {
                    //TODO: Check the formula u used here
                    //finalScore += (1 - (1 / wordsDistance[i]))/(i-prevWord);
                    //Remember dividing an int by an int would return int
                    finalScore += (1.0 / (wordsDistance[i] * (i - prevWord)));
                    prevWord = i;
                }
             
            }
            //for(int i = 0; i < query.Count-1; i++) {
            //    int firstScore = wordsDistance[i];
            //    if (firstScore == 0)
            //        continue;
            //    for (int j = i + 1; j < query.Count; j++) {
            //        int secondScore = wordsDistance[j];
            //        if (secondScore == 0)
            //            continue;
            //        finalScore = 

            //    }
                    
            //}
            return finalScore;
        }
        private static List<int> CalculateBestDiff(List<string> query, Dictionary<string,List<int>> dic) {
            //TODO: This function should locate the region of the best score used in the document
            //TODO: I hope this works, cause if it doesn't.....
            int score;
            
            List<int> output = new List<int>();
            for(int k = 1; k < query.Count; k++) {
                //while the first guy is not present
                if (dic[query[k - 1]].Count == 0) {
                    score = 0;
                    continue;
                }
                //Like Merge algorithm in merge sort
                List<int> first = new List<int>(dic[query[k-1]]);
                List<int> second = new List<int>(dic[query[k]]);
                score = int.MaxValue;
                int i = 0;
                int j = 0;
                
                //TODO: I really need to check the guy
                while(second.Count == 0) {
                    score = 0;
                    output.Add(score);
                    if (k+1 < query.Count)
                        k++;
                    else {
                       // output.Add(score);
                        return output;
                    }
                    second = new List<int>(dic[query[k]]);
                }
                first.Add(int.MaxValue);
                second.Add(int.MaxValue);
                //Check if the list contains an element first, it would not contain any element if the document does not contain
                //the word in the first place
                //So as to ignore the maxValue guyz
                for (int count = 0; count < first.Count + second.Count - 2; count++) {
                        if (first[i] < second[j]) {
                            score = (score < (second[j] - first[i])) ? score : second[j] - first[i];
                            i++;
                        }
                        else if (first[i] > second[j]) {
                            score = (score < (first[i] - second[j])) ? score : first[i] - second[j];
                            j++;
                        }

                    }
         output.Add(score);
            }
            return output;
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
        private static double IDFWeight(int noOfDocuments,Inverter invt) {
            return Math.Log(invt.DocumentCount/noOfDocuments);
        }
    }
}
