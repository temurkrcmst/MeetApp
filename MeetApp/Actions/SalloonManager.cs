using MeetApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MeetApp.Actions
{

    public class SalloonManager
    {
        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "Meeting";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);


        //INSERT
        public static async Task<AddSalloonOutput> AddSalloon(Salloon Salloon)
        {
            try
            {
                if (Salloon.SalloonName == "")
                {
                    return new AddSalloonOutput() { Type = 0, Message = "Kayit  Ekleme sırasında hata gerçekleşti..." };
                }

                Salloon.ID = Guid.NewGuid().ToString();
                await db.GetCollection<Salloon>("Salloons").InsertOneAsync(Salloon);
            }
            catch (Exception ex)
            {
                return new AddSalloonOutput() { Type = 0, Message = "Kayit  Ekleme sırasında hata gerçekleşti..." + ex.Message };
            }

            return new AddSalloonOutput() { Type = 1, Message = "Kayit  Başarıyla Eklendi..." };

            //Salloon.ID = Guid.NewGuid().ToString();

            //var g = new Guid();
            //var id = g.ToString();

            //var obj = new Salloon()

            //{
            //    ID = "24",
            //    Branch = "Balıkesir",
            //    Capacity = 20,
            //    InternetStatus = true,
            //    AirConditioningStatus = false,
            //    ProvisionsStatus = true,

            //};

            //db.GetCollection<Salloon>("Salloons").InsertOne(Salloon);

        }

        public static async Task<List<Salloon>> GetSalloon(GetSallooninput Parameters)
        {
            var List = new List<Salloon>();
         
            var projection = Builders<Salloon>.Projection;
            var project = projection.Exclude("_id");

            //var filter = Builders<Salloon>.Filter.Eq(x => x.ID, "14");
            var filter = FilterDefinition<Salloon>.Empty;

            BsonValue val;
            if (Parameters.ColumnType == "int")
            {
                val = Convert.ToInt64(Parameters.Value);
            }
            else if (Parameters.ColumnType == "bool")
            {
                val = Convert.ToBoolean(Parameters.Value);
            }
            else
            {
                val = Convert.ToString(Parameters.Value);
            }

            if (Parameters.Statement == "=")
            {
                filter = Builders<Salloon>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<Salloon>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<Salloon>.Filter.Gt(Parameters.Column, val);
            }

            var option = new FindOptions<Salloon, BsonDocument> { Projection = project };

            using (var cursor = await db.GetCollection<Salloon>("Salloons").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;

                    foreach (BsonDocument s in batch)
                    {
                        var salloon = new Salloon();

                        if (s.Contains("Branch"))
                            salloon.Branch = s["Branch"].AsString;
                        if (s.Contains("ID"))
                            salloon.ID = s["ID"].AsString;
                        if (s.Contains("AirConditioningStatus"))
                            salloon.AirConditioningStatus = s["AirConditioningStatus"].AsBoolean;
                        if (s.Contains("Capacity"))
                            salloon.Capacity = s["Capacity"].AsInt32;
                        if (s.Contains("InternetStatus"))
                            salloon.InternetStatus = s["InternetStatus"].AsBoolean;
                        if (s.Contains("ProjectionStatus"))
                            salloon.ProjectionStatus = s["ProjectionStatus"].AsBoolean;
                        if (s.Contains("ProvisionsStatus"))
                            salloon.ProvisionsStatus = s["ProvisionsStatus"].AsBoolean;
                        if (s.Contains("SalloonName"))
                            salloon.SalloonName = s["SalloonName"].AsString;

                        List.Add(salloon);
                    }
                }
            }

            return List;

        }
        //UPDATE
        //_id bson documente ait bu nedenle projection
        public static async Task<UpdateSalloonOutput> UpdateSalloon(UpdateSallooninput Parameters)
        {
            UpdateSalloonOutput output = new UpdateSalloonOutput()
            {
                Type = 0,
                Message = "Kayıt ekleme sırasında hata oluştu"
            };

            var filter = Builders<Salloon>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var update = Builders<Salloon>.Update.Set(x => x.Branch, "ODTÜ Şubesi")
                .Set(x => x.Branch, Parameters.Branch)
                .Set(x => x.Capacity, Parameters.Capacity)
                .Set(x => x.AirConditioningStatus, Parameters.AirConditioningStatus)
                .Set(x => x.InternetStatus, Parameters.InternetStatus)
                .Set(x => x.ProjectionStatus, Parameters.ProjectionStatus)
                .Set(x => x.ProvisionsStatus, Parameters.ProvisionsStatus)
                .Set(x => x.SalloonName, Parameters.SalloonName);
            var projection = Builders<Salloon>.Projection;
            var project = projection.Exclude("_id");

            var result = await db.GetCollection<Salloon>("Salloons").UpdateManyAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }
            return output;
            //var branches = db.GetCollection<Salloon>("Salloons").Find("{}").Project(project).ToList();
           // kolon güncelleme yapıca 04.07.2018
           
        }
        
        //public static void UpdateSalloon()
        //{
        //    db.GetCollection<Salloon>("Salloons").UpdateOne(Builders<Salloon>.Filter.Eq("_id", ObjectId.Parse("5b334d57e947489e031fc53b")),
        //    Builders<Salloon>.Update
        //    .Set("Branch", "AAA")
        //    .Set("Capacity",90)
        //        );
        //}

        public static async Task<DeleteSalloonOutput> DeleteSalloon(DeleteSallooninput Parameters)
        {

            DeleteSalloonOutput output = new DeleteSalloonOutput()
            {
                Type = 0,
                Message = "Kayıt silme sırasında hata oluştu"
            };


            var filter = Builders<Salloon>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var result = await db.GetCollection<Salloon>("Salloons").DeleteManyAsync(filter);

            return output;
        }


        //public static void ReadSalloon()
        //{
        //    var filter = Builders<Salloon>.Filter.Eq(x => x.ID, "20");
        //    var result = db.GetCollection<Salloon>("Salloons").Many(filter);
        //}









        //public static void InsertBranch()
        //{
        //    var doc = new Salloon()
        //    {
        //        Branch = "Ankara",
        //        Capacity = 10,
        //        InternetStatus = "true",
        //        AirConditioningStatus = "false",
        //        ProvisionsStatus = "true",
        //    };
        //}


        //        var document = new BsonDocument
        //{
        //    { "item", "canvas" },
        //    { "qty", 100 },
        //    { "tags", new BsonArray { "cotton" } },
        //    { "size", new BsonDocument { { "h", 28 }, { "w", 35.5 }, { "uom", "cm" } } }
        //};
        //        collection.InsertOne(document);


        //toList birden fazla kayıt

    }
}
