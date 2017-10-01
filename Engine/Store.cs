using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
namespace Engine
{
    public class Store : IStore{
        private MongoClient client;
        private readonly string connectionString = "mongodb://127.0.0.1:27017";
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        private IMongoCollection<Document> documents;
        public Store() {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("STOT");
            //database.DropCollection("STOT");
            //database.DropCollection("Document");
            collection = database.GetCollection<BsonDocument>("STOT");
            documents = database.GetCollection<Document>("Document");
        }
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

        private async void AddToDocTable(Document doc) {
            var filter = Builders<Document>.Filter.Eq("_id", doc.Address);
            var answer = documents.Find(filter).FirstOrDefault();
            if (answer == null)
                await documents.InsertOneAsync(doc);
        }
        private bool CheckWord(string word) {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
             var answer = collection.Find(filter).FirstOrDefault();
            if (answer == null)
                return false;
            return true;
        }
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
            AddToDocTable(doc);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
            BsonDocument bdoc = new BsonDocument { { "_id", doc.Address } };
            bdoc.Add("value", new BsonArray { i });
            var update = Builders<BsonDocument>.Update.Push("array", bdoc);
            collection.UpdateOne(filter, update);
        }
        private void AddWordToInvtTableAsync(string word, Document doc, int i) {
            AddToDocTable(doc);
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
        public String[] AllWordsInTable() {
            return collection.AsQueryable().Select(x => (string)x["_id"]).ToArray();
        }
        public void Delete(Document doc) {
            var filter = Builders<Document>.Filter.Eq("_id", doc.Address);
            var filter2 = Builders<BsonDocument>.Filter.Eq("array._id", doc.Address);
            var update = Builders<BsonDocument>.Update.PullFilter("array", Builders<Document>.Filter.Eq("_id", doc.Address));
            collection.UpdateManyAsync(filter2, update);
            documents.DeleteOneAsync(filter);
        }
        public void RemoveDocument(String word, Document doc) {
            RemoveDoc(word, doc);
            if (Get(word).Count == 0) {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", word);
                collection.DeleteOne(filter);
            }
        }
        public void AddWordToTable(String word, Document doc, int i) {
            if (CheckWord(word)) {
                if (CheckWordInDoc(word,doc)) {
                    AddAsync(word, doc, i);
                } else {
                    AddDocToWord(word, doc, i);
                }
            } else {
                AddWordToInvtTableAsync(word, doc, i);

            }

        }
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
 