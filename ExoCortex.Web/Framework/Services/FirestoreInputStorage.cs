using System.Collections.Generic;
using System.Threading.Tasks;
using ExoCortex.Web.Models;
using ExoCortex.Web.Models.Response;
using Google.Cloud.Firestore;

namespace ExoCortex.Web.Framework.Services
{

    public class FirestoreInputStorage : IInputStorage
    {
        private IFirestoreFactory Db;
        public FirestoreInputStorage(IFirestoreFactory factory)
        {
            Db = factory;
        }

        public async Task<long> Count()
        {
            var collection = await GetCollection();
            return (await collection.GetSnapshotAsync()).Count;
        }

        public async Task<InputQueryResult> GetLatest(string type, int limit)
        {
            var collection = await GetCollection();
            var query = collection.OrderByDescending("Time").Limit(limit);
            if (!string.IsNullOrEmpty(type))
            {
                query = query.WhereEqualTo("Type", type);
            }
            var results = await query.GetSnapshotAsync();
            var items = new List<InputItem>();

            foreach (var item in results)
            {
                items.Add(item.ConvertTo<InputItem>());
            }
            return new InputQueryResult(items);
        }

        public async Task<string> Save(InputItem item)
        {
            var collection = await GetCollection();
            if (item.Id != null)
            {
                var lookupDoc = collection.Document(item.Id);
                var oldDocument = await lookupDoc.GetSnapshotAsync();
                if (!oldDocument.Exists)
                {
                    throw new System.Exception("Trying to fetch unknown inputitem with id:" + item.Id);
                }
                var saveResult = await oldDocument.Reference.SetAsync(item);
                return oldDocument.Id;
            }
            var addedDocReference = await collection.AddAsync(item);
            return addedDocReference.Id;
        }

        private async Task<CollectionReference> GetCollection()
        {
            var session = await Db.Get();
            return session.Collection("input");
        }
    }
}