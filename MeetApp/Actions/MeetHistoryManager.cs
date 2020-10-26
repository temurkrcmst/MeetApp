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


    public class MeetHistoryManager
    {
        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "Meeting";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);

        public static async Task<AddMeetingHistoryOutput> AddMeetingHistory(MeetingHistory MeetingHistory)
        {
            try
            {
                if (MeetingHistory.Title == "")
                {
                    return new AddMeetingHistoryOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Hata Gerçekleşti.. " };

                }
                MeetingHistory.ID = Guid.NewGuid().ToString();
                await db.GetCollection<MeetingHistory>("MeetingHistory").InsertOneAsync(MeetingHistory);
            }
            catch (Exception ex)
            {
                return new AddMeetingHistoryOutput() { Type = 0, Message = "Kayıt Ekleme sırasında hata var.. Hata : " + ex.Message };
            }

            return new AddMeetingHistoryOutput() { Type = 1, Message = "Kayıt Başarıyla Eklendi.." };

        }


        public static async Task<List<MeetingHistory>> GetMeetingHistory(GetMeetingHistoryInput Parameters)
        {
            var List = new List<MeetingHistory>();
            var projection = Builders<MeetingHistory>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<MeetingHistory>.Empty;

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
                filter = Builders<MeetingHistory>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<MeetingHistory>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<MeetingHistory>.Filter.Gt(Parameters.Column, val);
            }

            var option = new FindOptions<MeetingHistory, BsonDocument>
            {
                Projection = project
            };

            using (var cursor = await db.GetCollection<MeetingHistory>("MeetingHistory").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;

                    foreach (BsonDocument m in batch)
                    {
                        var meet = new MeetingHistory();

                        if (m.Contains("ID"))
                            meet.ID = m["ID"].AsString;
                        if (m.Contains("Title"))
                            meet.Title = m["Title"].AsString;
                        if (m.Contains("StartDate"))
                            meet.StartDate = m["StartDate"].AsString;
                        if (m.Contains("FinishDate"))
                            meet.FinishDate = m["FinishDate"].AsString;
                        if (m.Contains("ParticipantNumber"))
                            meet.ParticipantNumber = m["ParticipantNumber"].AsInt32;
                        if (m.Contains("BranchID"))
                            meet.BranchID = m["BranchID"].AsString;
                        if (m.Contains("Salloon"))
                            meet.Salloon = m["Salloon"].AsString;
                        if (m.Contains("Moderator"))
                            meet.Moderator = m["Moderator"].AsString;

                        List.Add(meet);
                    }
                }
            }
            return List;
        }
        //var g = new Guid();
        //    var id = g.ToString();


        //db.GetCollection<MeetingHistory>("MeetingHistories").InsertOne(MeetingHistory);




        //public static void UpdateMeet()
        //{
        //    db.GetCollection<MeetingHistory>("MeetingHistories").UpdateOne(Builders<MeetingHistory>.Filter.Eq("_id", ObjectId.Parse("5b334f67e4df9ee194c8dcbc")),
        //        Builders<MeetingHistory>.Update
        //        .Set("MeetingTitle", "Staj")
        //        .Set("Moderator", "Beyza")
        //);
        //}
        public static async Task<UpdateMeetingHistoryOutput> UpdateMeetingHistory(UpdateMeetingHistoryInput Parameters)
        {
            UpdateMeetingHistoryOutput output = new UpdateMeetingHistoryOutput()
            {
                Type = 0,
                Message = "Kayıt güncelleme sırasında hata oluştu"
            };

            var filter = Builders<MeetingHistory>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            var update = Builders<MeetingHistory>.Update
                        .Set(x => x.Title, Parameters.Title)
                        .Set(x => x.FinishDate, Parameters.FinishDate)
                        .Set(x => x.StartDate, Parameters.StartDate)
                        .Set(x => x.Moderator, Parameters.Moderator)
                        .Set(x => x.BranchID, Parameters.BranchID)
                        .Set(x => x.Salloon, Parameters.Salloon)
                        .Set(x => x.ParticipantNumber, Parameters.ParticipantNumber);

            var projection = Builders<MeetingHistory>.Projection;
            var project = projection.Exclude("_id");

            var result = await db.GetCollection<MeetingHistory>("MeetingHistory").UpdateManyAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }
            return output;
        }
        public static async Task<DeleteMeetingHistoryOutput> Delete(DeleteMeetingHistoryInput Parameters)
        {
            DeleteMeetingHistoryOutput output = new DeleteMeetingHistoryOutput()

            {
                Type = 1,
                Message = "Kayıt Başarıyla Silindi..."
            };
            var filter = Builders<MeetingHistory>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            //var filter = Builders<MeetingHistory>.Filter.Eq(x => x.ID, "5");
            var result = await db.GetCollection<MeetingHistory>("MeetingHistory").DeleteManyAsync(filter);

            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu";
            }

            return output;

        }

      





    }



}


