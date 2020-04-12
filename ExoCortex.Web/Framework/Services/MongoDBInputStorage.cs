using ExoCortex.Web.Models;
using ExoCortex.Web.Models.Response;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ExoCortex.Web.Framework.Services
{
    public class MongoDBInputStorage : IInputStorage
    {
        private MongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IMongoCollection<InputItem> Collection { get; set; }

        public MongoDBInputStorage(IConfiguration config)
        {
            var connectionString = config["MongoCS"];
            var database = config["MongoDB"];
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(database))
            {
                throw new ArgumentException("MongoCS or MongoDB config values missing");
            }
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(database);
            Collection = Database.GetCollection<InputItem>("InputItems");
        }
        public async Task<long> Count()
        {
            return await Collection.CountDocumentsAsync(x => true);
        }

        public async Task<InputQueryResult> GetLatest(string type, int limit)
        {
            var query = await Collection
                .Find(x => string.IsNullOrEmpty(type) || x.Type == type)
                .SortByDescending(x => x.Time)
                .Limit(limit)
                .ToListAsync();
            return new InputQueryResult(query);
        }

        public async Task<string> Save(InputItem item)
        {
            await Collection.InsertOneAsync(item);
            return item.Id;
        }
    }
}
