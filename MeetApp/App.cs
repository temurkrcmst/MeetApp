using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MeetApp.Token;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace MeetApp
{
    /// <summary>
    /// Uygulamaya özel genel değişkenler burada saklanır.
    /// Buraya yeni birşey eklenecek ise özellikle THREAD-SAFE olmasına dikkat etmek gerekli !!!
    /// </summary>
    public static class App
    {
        private static CultureInfo _culture;

        private static MongoClient _mongoClient;

        public static MongoClient MongoClient
        {
            get { return App._mongoClient; }
        }

        private static IMongoDatabase _mtDatabase;
        private static IMongoCollection<BsonDocument> _mtUsers;
        private static JsonWriterSettings _writerSettings;

        public static JsonWriterSettings JsonWriterSettings
        {
            get { return App._writerSettings; }
        }

        public static void Init()
        {
            // TODO: Burada DefaultCulture TÜRKÇE olarak ayarlanıyor. Belki bu ayarlanabilir olmalı.
            _culture = new CultureInfo("tr-TR");

            // Default Mongo Connection
            _mongoClient = new MongoClient(ConfigurationManager.AppSettings["database.url"]);

            _mtDatabase = _mongoClient.GetDatabase(ConfigurationManager.AppSettings["database.GM.name"]);

            // GM Users
            _mtUsers = _mtDatabase.GetCollection<BsonDocument>("users");

            _writerSettings = JsonWriterSettings.Defaults.Clone();
            _writerSettings.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            _writerSettings.Indent = false;
            _writerSettings.MaxSerializationDepth = 100;
            _writerSettings.NewLineChars = "";
            _writerSettings.OutputMode = JsonOutputMode.Strict;
        }

        /// <summary>
        /// Bu uygulama için tanımlanmı CultureInfo değeridir.
        /// </summary>
        public static CultureInfo AppCultureInfo { get { return _culture; } }

        /// <summary>
        /// Geomarket veritabanı
        /// </summary>
        public static IMongoDatabase MeetAppDatabase { get { return _mtDatabase; } }

        /// <summary>
        /// Uygulamanın kullanıcılar tablosu
        /// </summary>
        public static IMongoCollection<BsonDocument> MeetAppUsers { get { return _mtUsers; } }

        /// <summary>
        /// Aktif kullanıcının veritabanını verir.
        /// </summary>
        public static IMongoDatabase GetCurrentUsersDatabase
        {
            get
            {
                // Aktif kullanıcıyı al
                var user = HttpContext.Current.User as UserPrincipal;

                if (user == null)
                    throw new InvalidOperationException("There is no user found in the current context");

                // Kullanıcının veritabanından MAPCATALOG
                return user.DB;
            }
        }

        public static UserPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User as UserPrincipal;
            }
        }
    }
}