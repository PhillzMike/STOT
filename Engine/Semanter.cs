using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Engine
{
    [Serializable]
    /// <summary>
    /// IDK
    /// Author 3dO
    /// </summary>
    public class Semanter
    {
        Dictionary<String,int> _dictionary;
        Dictionary<String, Dictionary<String, int>> Trie = new Dictionary<string, Dictionary<string, int>>();
        public static string[] punctuations = { " ",",","@","#","$","%","^","&","*","+","=","`","~","<",">","/","\\","|",":","(",")","?","!",";","-", "–","_","[","]","\"",".","…","\t","\n","\r" };
        public static string[] Splitwords(string query) {
            return Regex.Replace(query.Trim().ToLower(), "'", string.Empty).Split(punctuations, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] Splitwords(string query,string except)
        {
            List<string> segments = new List<string>();
            for(int i = 0; i < query.Length; i++) {
                if (query[i] == '"') {

                }
            }
            List<string> puncs = punctuations.ToList();
            puncs.Remove(except);
            return Regex.Replace(query.Trim().ToLower(), "'", string.Empty).Split(puncs.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }
        public Semanter(String toDictionary,String toCommon) {
            _dictionary = new Dictionary<string,int>();
            LoadToDictionary(File.ReadAllText(toDictionary));
            LoadToDictionary(File.ReadAllText(toCommon),2);
        }
        /// <summary>
        /// Adds a Book to dictionary with specified weight.
        /// </summary>
        /// <param name="toBook">To book.</param>
        /// <param name="weight">The weight.</param>
        public void AddToDictionary(String toBook,int weight) {
            LoadToDictionary(File.ReadAllText(toBook),weight);
        }
        /// <summary>
        /// Loads the words in the string (Space or new Line  "\n" delimeter) to dictionary.
        /// </summary>
        /// <param name="words">The words to be loaded.</param>
        /// <param name="weight">The weight of each word in the Dictionary NOTE: words with higher weight will be suggested first.</param>
        private void LoadToDictionary(String words,int weight) {
        foreach(string word in words.Split(Semanter.punctuations,StringSplitOptions.RemoveEmptyEntries).ToList()) {
                string trimmedWord = word.Trim().ToLower();
                AddWordToDictionary(trimmedWord, weight);
        }
        }
        /// <summary>
        /// Adds the word to dictionary and increments the weight if it already exists.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="weight">The weight.</param>
        public void AddWordToDictionary(string word,int weight) {
            if(weight!=1)
                TrieWord(word,(weight<3)?3:weight);
            if (_dictionary.ContainsKey(word))
                _dictionary[word] += weight;
            else 
                _dictionary.Add(word, weight);
            
        }
        /// <summary>
        /// Loads the words in the string (Space or new Line  "\n" delimeter) to dictionary.
        /// </summary>
        /// <param name="words">The words to be loaded.</param>
        private void LoadToDictionary(String words) {
            LoadToDictionary(words,1);
        }
       
        /// <summary>
        /// Suggest Search Queries Similar to the Passed in query
        /// </summary>
        /// <param name="query">The List of strings containing the query in order.</param>
        /// <returns>An array of suggested terms, sorted by relevance</returns>
        public List<String> Suggestions(String query, int noOfResults) {
            String[] Listwords = Splitwords(query.Trim().ToLower());
            query = String.Join(" ", Listwords);
            int noRemaining = noOfResults;
            String PreWord="";
            HashSet<String> Results = new HashSet<string>();
            foreach(String word in Listwords) {
                if (Trie.ContainsKey(query)) {
                    foreach (String res in Trie[query].OrderByDescending(x => x.Value).Select(k => k.Key)) {
                        if(Results.Add(PreWord + res))
                            noRemaining--;
                        if (noRemaining == 0) {
                            break;
                        }
                    }
                }
                if (noRemaining == 0) {
                    break;
                }
                PreWord += word + " ";
                if(!query.Equals(word))
                query = query.Substring(word.Length + 1);
            }

            //Autocorrect from dictionary
            //Filename
            //Previous searches
            return Results.ToList();
        }
        /// <summary>
        /// Corrects the specified query. *For DID you mean, or showing results instead
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public List<String> Autocorrect(String query) {
            List<string> results = new List<string>();
            foreach(string word in query.Split(punctuations, StringSplitOptions.RemoveEmptyEntries).ToList()) {
                results.Add(CorrectWord(word));
            }
            return results;
        }
        /// <summary>
        /// The Stemmer transforms a word into its root form.
        /// Implementing the Porter Stemming Algorithm
        /// </summary>
        #region Stemmer
        // The passed in word turned into a char array. 
        // Quicker to use to rebuilding strings each time a change is made.
        private char[] wordArray;

        // Current index to the end of the word in the character array. This will
        // change as the end of the string gets modified.
        private int endIndex;

        // Index of the (potential) end of the stem word in the char array.
        private int stemIndex;

        /// <summary>
        /// The Stemmer class transforms a word into its root form.
        /// Implementing the Porter Stemming Algorithm
        /// </summary>
        /// <remarks>
        /// Modified from: http://tartarus.org/martin/PorterStemmer/csharp2.txt
        /// </remarks>
        /// <example>
        /// var stemmer = new PorterStemmer();
        /// var stem = stemmer.StemWord(word);
        /// </example>
        /// <param name="word">Word to evaluate</param>
        /// <returns></returns>
        public string StemWord(string word) {

                // Do nothing for empty strings or short words.
                if(string.IsNullOrWhiteSpace(word) || word.Length <= 2) return word;

                wordArray = word.ToCharArray();

                stemIndex = 0;
                endIndex = word.Length - 1;
                Step1();
                Step2();
                Step3();
                Step4();
                Step5();
                Step6();

                var length = endIndex + 1;
                return new String(wordArray,0,length);
            }


            // Step1() gets rid of plurals and -ed or -ing.
            /* Examples:
                   caresses  ->  caress
                   ponies    ->  poni
                   ties      ->  ti
                   caress    ->  caress
                   cats      ->  cat

                   feed      ->  feed
                   agreed    ->  agree
                   disabled  ->  disable

                   matting   ->  mat
                   mating    ->  mate
                   meeting   ->  meet
                   milling   ->  mill
                   messing   ->  mess

                   meetings  ->  meet  		*/
            private void Step1() {
                // If the word ends with s take that off
                if(wordArray[endIndex] == 's') {
                    if(EndsWith("sses")) {
                        endIndex -= 2;
                    } else if(EndsWith("ies")) {
                        SetEnd("i");
                    } else if(wordArray[endIndex - 1] != 's') {
                        endIndex--;
                    }
                }
                if(EndsWith("eed")) {
                    if(MeasureConsontantSequence() > 0)
                        endIndex--;
                } else if((EndsWith("ed") || EndsWith("ing")) && VowelInStem()) {
                    endIndex = stemIndex;
                    if(EndsWith("at"))
                        SetEnd("ate");
                    else if(EndsWith("bl"))
                        SetEnd("ble");
                    else if(EndsWith("iz"))
                        SetEnd("ize");
                    else if(IsDoubleConsontant(endIndex)) {
                        endIndex--;
                        int ch = wordArray[endIndex];
                        if(ch == 'l' || ch == 's' || ch == 'z')
                            endIndex++;
                    } else if(MeasureConsontantSequence() == 1 && IsCVC(endIndex)) SetEnd("e");
                }
            }

            // Step2() turns terminal y to i when there is another vowel in the stem.
            private void Step2() {
                if(EndsWith("y") && VowelInStem())
                    wordArray[endIndex] = 'i';
            }

            // Step3() maps double suffices to single ones. so -ization ( = -ize plus
            // -ation) maps to -ize etc. note that the string before the suffix must give m() > 0. 
            private void Step3() {
                if(endIndex == 0) return;

                /* For Bug 1 */
                switch(wordArray[endIndex - 1]) {
                    case 'a':
                    if(EndsWith("ational")) { ReplaceEnd("ate"); break; }
                    if(EndsWith("tional")) { ReplaceEnd("tion"); }
                    break;
                    case 'c':
                    if(EndsWith("enci")) { ReplaceEnd("ence"); break; }
                    if(EndsWith("anci")) { ReplaceEnd("ance"); }
                    break;
                    case 'e':
                    if(EndsWith("izer")) { ReplaceEnd("ize"); }
                    break;
                    case 'l':
                    if(EndsWith("bli")) { ReplaceEnd("ble"); break; }
                    if(EndsWith("alli")) { ReplaceEnd("al"); break; }
                    if(EndsWith("entli")) { ReplaceEnd("ent"); break; }
                    if(EndsWith("eli")) { ReplaceEnd("e"); break; }
                    if(EndsWith("ousli")) { ReplaceEnd("ous"); }
                    break;
                    case 'o':
                    if(EndsWith("ization")) { ReplaceEnd("ize"); break; }
                    if(EndsWith("ation")) { ReplaceEnd("ate"); break; }
                    if(EndsWith("ator")) { ReplaceEnd("ate"); }
                    break;
                    case 's':
                    if(EndsWith("alism")) { ReplaceEnd("al"); break; }
                    if(EndsWith("iveness")) { ReplaceEnd("ive"); break; }
                    if(EndsWith("fulness")) { ReplaceEnd("ful"); break; }
                    if(EndsWith("ousness")) { ReplaceEnd("ous"); }
                    break;
                    case 't':
                    if(EndsWith("aliti")) { ReplaceEnd("al"); break; }
                    if(EndsWith("iviti")) { ReplaceEnd("ive"); break; }
                    if(EndsWith("biliti")) { ReplaceEnd("ble"); }
                    break;
                    case 'g':
                    if(EndsWith("logi")) {
                        ReplaceEnd("log");
                    }
                    break;
                }
            }

            /* step4() deals with -ic-, -full, -ness etc. similar strategy to step3. */
            private void Step4() {
                switch(wordArray[endIndex]) {
                    case 'e':
                    if(EndsWith("icate")) { ReplaceEnd("ic"); break; }
                    if(EndsWith("ative")) { ReplaceEnd(""); break; }
                    if(EndsWith("alize")) { ReplaceEnd("al"); }
                    break;
                    case 'i':
                    if(EndsWith("iciti")) { ReplaceEnd("ic"); }
                    break;
                    case 'l':
                    if(EndsWith("ical")) { ReplaceEnd("ic"); break; }
                    if(EndsWith("ful")) { ReplaceEnd(""); }
                    break;
                    case 's':
                    if(EndsWith("ness")) { ReplaceEnd(""); }
                    break;
                }
            }

            /* step5() takes off -ant, -ence etc., in context <c>vcvc<v>. */
            private void Step5() {
                if(endIndex == 0) return;

                switch(wordArray[endIndex - 1]) {
                    case 'a':
                    if(EndsWith("al")) break; return;
                    case 'c':
                    if(EndsWith("ance")) break;
                    if(EndsWith("ence")) break; return;
                    case 'e':
                    if(EndsWith("er")) break; return;
                    case 'i':
                    if(EndsWith("ic")) break; return;
                    case 'l':
                    if(EndsWith("able")) break;
                    if(EndsWith("ible")) break; return;
                    case 'n':
                    if(EndsWith("ant")) break;
                    if(EndsWith("ement")) break;
                    if(EndsWith("ment")) break;
                    /* element etc. not stripped before the m */
                    if(EndsWith("ent")) break; return;
                    case 'o':
                    if(EndsWith("ion") && stemIndex >= 0 && (wordArray[stemIndex] == 's' || wordArray[stemIndex] == 't')) break;
                    /* j >= 0 fixes Bug 2 */
                    if(EndsWith("ou")) break; return;
                    /* takes care of -ous */
                    case 's':
                    if(EndsWith("ism")) break; return;
                    case 't':
                    if(EndsWith("ate")) break;
                    if(EndsWith("iti")) break; return;
                    case 'u':
                    if(EndsWith("ous")) break; return;
                    case 'v':
                    if(EndsWith("ive")) break; return;
                    case 'z':
                    if(EndsWith("ize")) break; return;
                    default:
                    return;
                }
                if(MeasureConsontantSequence() > 1)
                    endIndex = stemIndex;
            }

            /* step6() removes a final -e if m() > 1. */
            private void Step6() {
                stemIndex = endIndex;

                if(wordArray[endIndex] == 'e') {
                    var a = MeasureConsontantSequence();
                    if(a > 1 || a == 1 && !IsCVC(endIndex - 1))
                        endIndex--;
                }
                if(wordArray[endIndex] == 'l' && IsDoubleConsontant(endIndex) && MeasureConsontantSequence() > 1)
                    endIndex--;
            }

            // Returns true if the character at the specified index is a consonant.
            // With special handling for 'y'.
            private bool IsConsonant(int index) {
                var c = wordArray[index];
                if(c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') return false;
                return c != 'y' || (index == 0 || !IsConsonant(index - 1));
            }

            /* m() measures the number of consonant sequences between 0 and j. if c is
               a consonant sequence and v a vowel sequence, and <..> indicates arbitrary
               presence,

                  <c><v>       gives 0
                  <c>vc<v>     gives 1
                  <c>vcvc<v>   gives 2
                  <c>vcvcvc<v> gives 3
                  ....		*/
            private int MeasureConsontantSequence() {
                var n = 0;
                var index = 0;
                while(true) {
                    if(index > stemIndex) return n;
                    if(!IsConsonant(index)) break; index++;
                }
                index++;
                while(true) {
                    while(true) {
                        if(index > stemIndex) return n;
                        if(IsConsonant(index)) break;
                        index++;
                    }
                    index++;
                    n++;
                    while(true) {
                        if(index > stemIndex) return n;
                        if(!IsConsonant(index)) break;
                        index++;
                    }
                    index++;
                }
            }

            // Return true if there is a vowel in the current stem (0 ... stemIndex)
            private bool VowelInStem() {
                int i;
                for(i = 0;i <= stemIndex;i++) {
                    if(!IsConsonant(i)) return true;
                }
                return false;
            }

            // Returns true if the char at the specified index and the one preceeding it are the same consonants.
            private bool IsDoubleConsontant(int index) {
                if(index < 1) return false;
                return wordArray[index] == wordArray[index - 1] && IsConsonant(index);
            }

            /* cvc(i) is true <=> i-2,i-1,i has the form consonant - vowel - consonant
               and also if the second c is not w,x or y. this is used when trying to
               restore an e at the end of a short word. e.g.

                  cav(e), lov(e), hop(e), crim(e), but
                  snow, box, tray.		*/
            private bool IsCVC(int index) {
                if(index < 2 || !IsConsonant(index) || IsConsonant(index - 1) || !IsConsonant(index - 2)) return false;
                var c = wordArray[index];
                return c != 'w' && c != 'x' && c != 'y';
            }

            // Does the current word array end with the specified string.
            private bool EndsWith(string s) {
                var length = s.Length;
                var index = endIndex - length + 1;
                if(index < 0) return false;

                for(var i = 0;i < length;i++) {
                    if(wordArray[index + i] != s[i]) return false;
                }
                stemIndex = endIndex - length;
                return true;
            }

            // Set the end of the word to s.
            // Starting at the current stem pointer and readjusting the end pointer.
            private void SetEnd(string s) {
                var length = s.Length;
                var index = stemIndex + 1;
                for(var i = 0;i < length;i++) {
                    wordArray[index + i] = s[i];
                }
                // Set the end pointer to the new end of the word.
                endIndex = stemIndex + length;
            }

            // Conditionally replace the end of the word
            private void ReplaceEnd(string s) {
                if(MeasureConsontantSequence() > 0) SetEnd(s);
            }
        #endregion
        /// <summary>
        /// The Correcter Corrects the specified word using the Set dictionary
        /// </summary>
        #region Correcter
        /// <summary>
        /// Corrects the word .
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="top">The max number of corrections to be suggested.</param>
        /// <returns>
        /// A list containing the specified number of suggestions correcting the specified word.
        /// </returns>
        public List<string> CorrectWord(string word,int top) {
            top = (top <0) ? 1:top;
            if(string.IsNullOrEmpty(word))
                return new List<String>();
            word = word.ToLower();
            // known and common
            if(_dictionary.ContainsKey(word)&&_dictionary[word]>2&&top==1)
                return new List<String> { word };

            List<String> list = Edits(word);
            Dictionary<string,int> candidates = new Dictionary<string,int>();

            foreach(string wordVariation in list) {
                if(_dictionary.ContainsKey(wordVariation) && !candidates.ContainsKey(wordVariation))
                    candidates.Add(wordVariation,_dictionary[wordVariation]);
            }
            
            // known_edits2()
            foreach(string item in list) {
                foreach(string wordVariation in Edits(item)) {
                    if(_dictionary.ContainsKey(wordVariation) && !candidates.ContainsKey(wordVariation))
                        candidates.Add(wordVariation,_dictionary[wordVariation]/3);
                }
            }
            return (candidates.Count > 0) ? 
                (top==0)?
                    candidates.OrderByDescending(x=>x.Value).Select(k=>k.Key).ToList()
                    :candidates.OrderByDescending(x => x.Value).Take(top).Select(k => k.Key).ToList<string>()
                : new List<String>();
        }
        /// <summary>
        /// Corrects the word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public string CorrectWord(string word) {
            if(string.IsNullOrWhiteSpace(word)) {
                return "";
            }
            List<String> result = CorrectWord(word, 1);
            if (result.Count > 0) {
                return CorrectWord(word, 1)[0];
            }
            return word;
        }


            private List<string> Edits(string word) {
            var splits = new List<Tuple<string,string>>();
            var transposes = new List<string>();
            var deletes = new List<string>();
            var replaces = new List<string>();
            var inserts = new List<string>();

            // Splits
            for(int i = 0;i < word.Length;i++) {
                var tuple = new Tuple<string,string>(word.Substring(0,i),word.Substring(i));
                splits.Add(tuple);
            }

            // Deletes
            for(int i = 0;i < splits.Count;i++) {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if(!string.IsNullOrEmpty(b)) {
                    deletes.Add(a + b.Substring(1));
                }
            }

            // Transposes
            for(int i = 0;i < splits.Count;i++) {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if(b.Length > 1) {
                    transposes.Add(a + b[1] + b[0] + b.Substring(2));
                }
            }

            // Replaces
            for(int i = 0;i < splits.Count;i++) {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if(!string.IsNullOrEmpty(b)) {
                    for(char c = 'a';c <= 'z';c++) {
                        replaces.Add(a + c + b.Substring(1));
                    }
                }
            }

            // Inserts
            for(int i = 0;i < splits.Count;i++) {
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                for(char c = 'a';c <= 'z';c++) {
                    inserts.Add(a + c + b);
                }
            }

            return deletes.Union(transposes).Union(replaces).Union(inserts).ToList();
        }
        #endregion


        public void TrieWord(String word) {
            TrieWord(word, 1);
        }
        public void TrieWord(String word,int weight) {
            for (int i = 1; i < word.Length; i++) {
                if (word[i] == ' ')
                    TrieWord(word.Substring(i + 1), weight);
                String key = word.Substring(0, i);
                if (Trie.ContainsKey(key)){
                    if (Trie[key].ContainsKey(word))
                        Trie[key][word]+=weight;
                    else
                        Trie[key].Add(word, weight);
                } else { 
                    Trie.Add(key, new Dictionary<string, int> { { word, weight } });
                }
            }
        }
    }
}