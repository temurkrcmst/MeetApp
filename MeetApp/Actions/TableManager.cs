//using MeetApp.Models;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//namespace MeetApp.Actions
//{
//    public class TableManager
//    {
//        public static MongoClient _mongoClient = new MongoClient();
//        public static string dbName = "Meeting";
//        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);

//        public static async Task<List<Salloon>> GetSallonByBranchId()
//        {

//            var List = new List<Branch>();
//            var projection = Builders<Branch>.Projection;
//            var project = projection.Exclude("_id");

//            var filter = FilterDefinition<Branch>.Empty;

//            var option = new FindOptions<Branch, BsonDocument> { Projection = project };
//            using (var cursor = await db.GetCollection<Branch>("Branches").FindAsync(filter, option))
//            {
//                while (await cursor.MoveNextAsync())
//                {
//                    var batch = cursor.Current;
//                    foreach (BsonDocument s in batch)
//                    {
//                        var branch = new Branch();
//                        if (s.Contains("Name"))
//                        {
//                            branch.Name = s["Name"].AsString;
//                        }

//                    }

//                }
//            }


//        }

//        public static async Task<List<Branch>> GetBranch()
//        {

//            var List = new List<Branch>();
//            var projection = Builders<Branch>.Projection;
//            var project = projection.Exclude("_id");

//            var filter = FilterDefinition<Branch>.Empty;

//            var option = new FindOptions<Branch, BsonDocument> { Projection = project };
//            using (var cursor = await db.GetCollection<Branch>("Branches").FindAsync(filter, option))
//            {
//                while (await cursor.MoveNextAsync())
//                {
//                    var batch = cursor.Current;
//                    foreach (BsonDocument s in batch)
//                    {
//                        var branch = new Branch();
//                        if (s.Contains("Name"))
//                        {
//                            branch.Name = s["Name"].AsString;
//                        }

//                    }

//                }
//            }


//        }
//    }
//}