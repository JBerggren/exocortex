using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ExoCortex.Web.Models;
using ExoCortex.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExoCortex.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecuteController : ControllerBase
    {
        public IInputStorage InputManager { get; }
        public ExecuteController(IInputStorage inputManager)
        {
            InputManager = inputManager;
        }



        [HttpGet("/count")]
        public async Task<int> Count()
        {
            return await InputManager.Count();
        }
    }
}