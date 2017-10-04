using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
namespace Engine {
    public class Store : IStore {
        private MongoClient client;
        private readonly string connectionString = "mongodb://127.0.0.1:27017";
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        private IMongoCollection<Document> documents;
        /// <summary>
        /// Initializes a new instance of the <see cref="Store"/> class.
        /// Connects to the Database stored in STOT.
        /// </summary>
        public Store() {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("STOT");
            //database.DropCollection("STOT");
            //database.DropCollection("Document");
            collection = database.GetCollection<BsonDocument>("STOT");
            documents = database.GetCollection<Document>("Document");
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Store"/> class.
        /// Connects to a specified Database
        /// </summary>
        /// <param name="db">The database.</param>
        public Store(IMongoDatabase db) {
            database = db;
            collection = database.GetCollection<BsonDocument>("STOT");
            documents = database.GetCollection<Document>("Document");
        }
        private void InitializeDB(Dictionary<String, Dictionary<Document, List<int>>> invertedIndexTable) {
            foreach (var item in invertedIndexTable.Keys) {
                List<BsonDocument> allDocForAWord = new List<BsonDocument>();
                foreach (var item2 in invertedIndexTable[item]) {
                    BsonArray list = new BsonArray(item2.Value);
                    BsonDocument doc = new BsonDocument { { "_id", item2.Key.Address } };
                    doc.Add("value", list);
                    allDocForAWord.Add(doc);
                }
                BsonDocument docs = new BsonDocument { { "array", new BsonArray(allDocForAWord) } };
                docs.Add("_id", item);
                collection.InsertOneAsync(docs).Wait();
            }
        }
        /// <summary>
        /// Gets all documents a word occurs in and the Positions in which they occur
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public Dictionary<Document, List<int>> WordsPositions(string word) {

            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
            var result = collection.Find(filter).FirstOrDefault();
            var x = new Dictionary<Document, List<int>>(new DocumentComparer());
            if (result != null)
                foreach (var item in result["array"].AsBsonArray) {
                    var filter2 = Builders<Document>.Filter.Eq("_id", item["_id"]);
                    //TODO ask timi to change single to First or default
                    var doc = documents.Find(filter2).FirstOrDefault();
                    if (doc == null)
                        continue;
                    var value = new List<int>();
                    foreach (var i in item["value"].AsBsonArray) {
                        value.Add((int)i);
                    }
                    x.Add(doc, value);
                }
            return x;
        }
        public void AddToDocTable(Document doc) {
            var filter = Builders<Document>.Filter.Eq("_id", doc.Address);
            var answer = documents.Find(filter).FirstOrDefault();
            try {
                if (answer == null)
                    documents.InsertOne(doc);
            } catch (Exception ex) {
                Inverter.LogMovement("!!!!!!!!!!ERROR adding to table "+ex.Message);
            }
        }
        private bool CheckWord(string word) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
            var answer = collection.Find(filter).FirstOrDefault();
            if (answer == null)
                return false;
            return true;
        }
        /// <summary>
        /// Checks if the word is in the document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        public bool CheckWordInDoc(string word, Document doc) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word) & Builders<BsonDocument>.Filter.Eq("array._id", doc.Address);
            var answer = collection.Find(filter).FirstOrDefault();
            if (answer == null)
                return false;
            return true;
        }
        private void AddAsync(string word, Document doc, int i) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word) & Builders<BsonDocument>.Filter.Eq("array._id", doc.Address);
            var update = Builders<BsonDocument>.Update.Push("array.$.value", i);
            collection.UpdateOne(filter, update);
        }
        private void AddDocToWord(string word, Document doc, int i) {
            //AddToDocTable(doc);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
            BsonDocument bdoc = new BsonDocument { { "_id", doc.Address } };
            bdoc.Add("value", new BsonArray { i });
            var update = Builders<BsonDocument>.Update.Push("array", bdoc);
            collection.UpdateOne(filter, update);
        }
        private void AddWordToInvtTableAsync(string word, Document doc, int i) {
            //AddToDocTable(doc);
            BsonDocument bdoc1 = new BsonDocument { { "_id", doc.Address } };
            bdoc1.Add("value", new BsonArray { i });
            BsonDocument bdoc = new BsonDocument { { "_id", word } };
            bdoc.Add("array", new BsonArray { bdoc1 });
            collection.InsertOne(bdoc);

        }
        private void RemoveDoc(string word, Document doc) {
            var filter1 = Builders<BsonDocument>.Filter.Eq("_id", word);
            var filter3 = Builders<BsonDocument>.Filter.Eq("_id", doc.Address);
            var filter = Builders<BsonDocument>.Update.PullFilter("array", filter3);
            collection.UpdateOne(filter1, filter);
        }
        /// <summary>
        /// Alls the words in  the table.
        /// </summary>
        /// <returns>Every word in the database</returns>
        public String[] AllWordsInTable() {
            return collection.AsQueryable().Select(x => (string)x["_id"]).ToArray();
        }
        /// <summary>
        /// Deletes the specified document from the Database.
        /// </summary>
        /// <param name="doc">The document to be deleted.</param>
        public void Delete(Document doc) {
            var filter = Builders<Document>.Filter.Eq("_id", doc.Address);
            var filter2 = Builders<BsonDocument>.Filter.Eq("array._id", doc.Address);
            var update = Builders<BsonDocument>.Update.PullFilter("array", Builders<Document>.Filter.Eq("_id", doc.Address));
            collection.UpdateManyAsync(filter2, update);
            documents.DeleteOneAsync(filter);
        }
        /// <summary>
        /// Removes the document from under the Word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        public void RemoveDocument(String word, Document doc) {
            RemoveDoc(word, doc);
            if (Get(word).Count == 0) {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
                collection.DeleteOne(filter);
            }
        }
        /// <summary>
        /// Adds the word to table under the Document.
        /// </summary>
        /// <param name="word">The word to be added.</param>
        /// <param name="doc">The document under which the word is added.</param>
        /// <param name="i">The index/Position the word occurs in the document.</param>
        public void AddWordUnderDocument(String word, Document doc, int i) {
            if (CheckWord(word)) {
                if (CheckWordInDoc(word, doc)) {
                    AddAsync(word, doc, i);
                } else {
                    AddDocToWord(word, doc, i);
                }
            } else {
                AddWordToInvtTableAsync(word, doc, i);

            }
        }
        /// <summary>
        /// Gets all Documents in the Table
        /// </summary>
        /// <returns>The Documents and their addresses</returns>
        public Dictionary<string,Document> GetAllDoc() {
            var docs = new Dictionary<string, Document>();
            var allDoc = documents.Find(new BsonDocument()).ToList();
            foreach (var item in allDoc) {
                docs.Add(item.Address, item);
            }
            return docs;
        }
        /// <summary>
        /// Gets the relevance of the Doc.
        /// </summary>
        /// <param name="address">The address of the Document.</param>
        /// <param name="pos">The position around which to get the relevance.</param>
        /// <returns></returns>
        public string GetRelevance(string address, int pos) {
            var x = "";
            for (int i = -5; i <= 5; i++) {
                if (pos + i >= 0) {
                var filter = Builders<BsonDocument>.Filter.Eq("array._id", address)
                        & Builders<BsonDocument>.Filter.AnyEq("array.value", pos + i);
                    foreach (var j in collection.Find(filter).ToList()) {
                        x += j["_id"].AsString + " ";
                    }
                }
            }
            return x;
        }
        /// <summary>
        /// Alls the documents containing a word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>List of Documents tht conting the word</returns>
        public Document[] AllDocumentsContainingWord(string word) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
            List<Document> allDocs = new List<Document>();
            if (CheckWord(word)) {
                var ans = collection.Find(filter).Single()["array"].AsBsonArray;

                foreach (BsonDocument item in ans) {
                    var filter2 = Builders<Document>.Filter.Eq("_id", item["_id"]);
                    allDocs.Add(documents.Find(filter2).Single());
                }

            }
            return allDocs.ToArray();
        }
        /// <summary>
        /// Gets the positions the word occurs in the document.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="doc">The document.</param>
        /// <returns>List of positions as integers</returns>
        public int[] PositionsWordOccursInDocument(string word, Document doc) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word) & Builders<BsonDocument>.Filter.Eq("array._id", doc.Address);
            var projection = Builders<BsonDocument>.Projection.Include("array.$").Exclude("_id");
            List<int> x = new List<int>();
            foreach (var i in collection.Find(filter).Project(projection).First()["array"][0]["value"].AsBsonArray) {
                x.Add((int)i);
            }
            return x.ToArray();
        }
        private BsonArray Get(string word) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
            return collection.FindSync(filter).Single()["array"].AsBsonArray;
        }
        private BsonArray Get(string word, Document doc) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word) & Builders<BsonDocument>.Filter.Eq("array._id", doc.Address);
            return collection.FindSync(filter).Single()["array"].AsBsonArray["value"].AsBsonArray;
        }
    }
}
