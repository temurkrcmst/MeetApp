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

    public class UserManager
    {
        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "Meeting";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);



        public static async Task<AddUserOutput> AddUser(User User)
        {
          

            try
            {
                if (User.Name == "")
                {
                    return new AddUserOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Hata Gerçekleşti.. " };

                }

                User.ID = Guid.NewGuid().ToString();
                await db.GetCollection<User>("Users").InsertOneAsync(User);  // Users = tablonun ismi, async olan metodlarda, await kullanmalıyız ne zaman ekleneceğini bilmiyoruz
            }
            catch (Exception ex)
            {
                return new AddUserOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Hata Gerçekleşti.. Hata : " + ex.Message };
                
            }
            return new AddUserOutput() { Type = 1, Message = "Kayıt Başarıyla Eklendi..." };
        }
        
        public static async Task<List<User>> GetUser(GetUserInput Parameters)
        {
            var List = new List<User>();
            var projection = Builders<User>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<User>.Empty;

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
                filter = Builders<User>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<User>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<User>.Filter.Gt(Parameters.Column, val);
            }

            var option = new FindOptions<User, BsonDocument>
            {
                Projection = project
            };



            //var option = new FindOptions<User, BsonDocument> { Projection = project };


            using (var cursor = await db.GetCollection<User>("Users").FindAsync(filter, option))
            {
                while(await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach(BsonDocument s in batch)
                    {
                        var user = new User();

                        if (s.Contains("ID"))
                            user.ID = s["ID"].AsString;

                        if (s.Contains("Name"))
                            user.Name = s["Name"].AsString;

                        if (s.Contains("Surname"))
                            user.Surname = s["Surname"].AsString;

                        if (s.Contains("Degree"))
                            user.Degree = s["Degree"].AsString;

                        if (s.Contains("Email"))
                            user.Email = s["Email"].AsString;

                        if (s.Contains("Phone"))
                            user.Phone = s["Phone"].AsString;

                        if (s.Contains("Password"))
                            user.Password = s["Password"].AsString;

                        if (s.Contains("ConfirmPassword"))
                            user.ConfirmPassword = s["ConfirmPassword"].AsString;

                        if (s.Contains("Authority"))
                            user.Authority = s["Authority"].AsString;

                        if (s.Contains("State"))
                            user.State = s["State"].AsString;

                        if (s.Contains("BranchID"))
                            user.BranchID = s["BranchID"].AsString;

                        List.Add(user);    
                        
                    }
                }
            }
            
            return List;

        }

        public static async Task<UpdateUserOutput> UpdateUser(UpdateUserInput Parameters)
        {
            UpdateUserOutput output = new UpdateUserOutput()
            {
                Type = 0,
                Message = "Kayıt güncellemede hata oluştu"
            };

            var filter = Builders<User>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);


            var update = Builders<User>.Update
                .Set(x => x.Name, Parameters.Name)
                .Set(x => x.Surname, Parameters.Surname)
                .Set(x => x.Degree, Parameters.Degree)
                .Set(x => x.Email, Parameters.Email)
                .Set(x => x.Phone, Parameters.Phone)
                .Set(x => x.Password, Parameters.Password)
                .Set(x => x.ConfirmPassword, Parameters.ConfirmPassword)
                .Set(x => x.Authority, Parameters.Authority)
                .Set(x => x.State, Parameters.State)
                .Set(x => x.BranchID, Parameters.BranchID); ;

            var projection = Builders<User>.Projection;    
            var project = projection.Exclude("_id");

            var result = await db.GetCollection<User>("Users").UpdateManyAsync(filter, update);

            if (result.ModifiedCount > 0)  
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }
            return output;
        }
        
        public static async Task<DeleteUserOutput> DeleteUser(DeleteUserInput Parameters)
        {
            DeleteUserOutput output = new DeleteUserOutput()
            {
                Type = 1,
                Message = "Kayıt başarıyla silindi..."
            };


            var filter = Builders<User>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var result = await db.GetCollection<User>("Users").DeleteManyAsync(filter);

            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu";
            }

            return output;

        }
    }
}

