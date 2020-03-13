using System;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExoCortex.Web.Services;

namespace ExoCortex.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FDBController : ControllerBase
    {
        private IFirestoreSession Db;
        public FDBController(IFirestoreSession session)
        {
            Db = session;
        }

        [HttpGet("list")]
        public async Task<List<string>> List(string project)
        {
            var session = await Db.GetSession();
            QuerySnapshot snapshot = await session.CollectionGroup("users").GetSnapshotAsync();
            var results = new List<string>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                results.Add(document.Id);
            }
            return results;
        }
    }
}

