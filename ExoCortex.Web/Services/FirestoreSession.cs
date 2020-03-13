using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace ExoCortex.Web.Services{
    public interface IFirestoreSession
    {
        Task<FirestoreDb> GetSession();
    }

    public class FirestoreSession : IFirestoreSession
    {
        private FirestoreDb Db;
        private string ProjectName;
        public FirestoreSession(IConfiguration conf){
            ProjectName = conf.GetValue<string>("FIRESTORE_GOOGLE_PROJECT");
            if(string.IsNullOrEmpty(ProjectName)) throw new System.Exception("Missing env variable 'FIRESTORE_GOOGLE_PROJECT'");
        }
        
        public async Task<FirestoreDb> GetSession()
        {
            if(Db == null){
                Db = await FirestoreDb.CreateAsync(ProjectName);
            }
            return Db;
        }
    }
}