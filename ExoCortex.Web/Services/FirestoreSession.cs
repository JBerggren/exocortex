using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace ExoCortex.Web.Services{
    public interface IFirestoreFactory
    {
        Task<FirestoreDb> Get();
    }

    public class FirestoreFactory : IFirestoreFactory
    {
        private FirestoreDb Db;
        private string ProjectName;
        public FirestoreFactory(IConfiguration conf){
            ProjectName = conf.GetValue<string>("FIRESTORE_GOOGLE_PROJECT");
            if(string.IsNullOrEmpty(ProjectName)) throw new System.Exception("Missing env variable 'FIRESTORE_GOOGLE_PROJECT'");
        }
        
        public async Task<FirestoreDb> Get()
        {
            if(Db == null){
                Db = await FirestoreDb.CreateAsync(ProjectName);
            }
            return Db;
        }
    }
}