using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MeetApp.Models;

namespace MeetApp.Actions
{


    public class BranchManager
    {
        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "Meeting";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);


        public static async Task<AddBranchOutput> Add(Branch Branch)
        {
            try
            {
                if (Branch.Name == "")
                {
                    return new AddBranchOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Hata Gerçekleşti.. " };

                }
                Branch.ID = Guid.NewGuid().ToString();
                await db.GetCollection<Branch>("Branches").InsertOneAsync(Branch);
            }
            catch (Exception ex)
            {
                return new AddBranchOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Hata Gerçekleşti.. Hata : " + ex.Message };
            }

            return new AddBranchOutput() { Type = 1, Message = "Kayıt Başarıyla Eklendi.." };

        }

        public static async Task<List<Branch>> GetBranch(GetBranchInput Parameters)
        {
            var List = new List<Branch>();
            // Branch salon = new Branch();
            var projection = Builders<Branch>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<Branch>.Empty;

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
                filter = Builders<Branch>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<Branch>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<Branch>.Filter.Gt(Parameters.Column, val);
            }


            var option = new FindOptions<Branch, BsonDocument> { Projection = project };
            using (var cursor = await db.GetCollection<Branch>("Branches").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument s in batch)
                    {
                        var branch = new Branch();
                        if (s.Contains("ID"))
                        {
                            branch.ID = s["ID"].AsString;
                        }

                        if (s.Contains("Name"))
                        {
                            branch.Name = s["Name"].AsString;
                        }

                        if (s.Contains("Address"))
                        {
                            branch.Address = s["Address"].AsString;
                        }

                        if (s.Contains("Phone"))
                        {
                            branch.Phone = s["Phone"].AsString;
                        }

                        List.Add(branch);
                    }

                }

            }
            return List;
        }


        public static async Task<UpdateBranchOutput> Update(UpdateBranchInput Parameters)
        {
            UpdateBranchOutput output = new UpdateBranchOutput()
            {
                Type = 0,
                Message = "Hata Oluştu"
            };

            var filter = Builders<Branch>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            var update = Builders<Branch>.Update
                .Set(x => x.Name, Parameters.Name)
                .Set(x => x.Address, Parameters.Address)
                .Set(x => x.Phone, Parameters.Phone);


            var projection = Builders<Branch>.Projection;
            var project = projection.Exclude("_id");

            //var result = await db.GetCollection<Branch>("Branches").UpdateOne(filter, update);
            var result = await db.GetCollection<Branch>("Branches").UpdateManyAsync(filter, update);
            // var branches = db.GetCollection<Branch>("Branches").Find("{}").Project(project).ToList();

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = "Kayıt başarıyla güncellendi";
            }

            return output;
        }


        public static async Task<DeleteBranchOutput> Delete(DeleteBranchInput Parameters)
        {
            DeleteBranchOutput output = new DeleteBranchOutput()
            {
                Type = 1,
                Message = "Kayıt başarıyla silindi.."
            };

            var filter = Builders<Branch>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            var result = await db.GetCollection<Branch>("Branches").DeleteManyAsync(filter);


            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu..";
            }
            else
            {
                var res = await DeleteUserOrSalloon(Parameters.FilterVal);

                if (res == 1)
                {
                    output.Message += " | Bilgi : Şubeye tanımlı kullanıcılar silindi..";

                }
                else if (res == 2)
                {
                    output.Message += " | Bilgi : Şubeye tanımlı kullanıcılar ve salonlar silindi..";
                }

            }

            return output;

        }
        public static async Task<int> DeleteUserOrSalloon(string id)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("BranchID", id);
            var filter2 = Builders<BsonDocument>.Filter.Eq("Branch", id);

            var result = await db.GetCollection<BsonDocument>("Users").DeleteManyAsync(filter);
            var result2 = await db.GetCollection<BsonDocument>("Salloons").DeleteManyAsync(filter2);

            if (result.DeletedCount > 0)
            {
                if (result2.DeletedCount > 0)
                {
                    return 2;
                }
                return 1;
            }

            return 0;

        }




        /*  public GetAllBranch()
          {
              var List = new List<Branch>();
              // Branch salon = new Branch();
              var projection = Builders<Branch>.Projection;
              var project = projection.Exclude("_id");
              var filter = FilterDefinition<Branch>.Empty;
          }*/





    }
}