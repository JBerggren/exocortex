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
        private IFirestoreFactory Db;
        public FDBController(IFirestoreFactory session)
        {
            Db = session;
        }
    }
}

