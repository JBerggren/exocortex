using System;
using System.Threading.Tasks;
using ExoCortex.Web.Framework.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ExoCortex.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecuteController : ControllerBase
    {
        public IInputStorage InputManager { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public ExecuteController(IInputStorage inputManager, IWebHostEnvironment  webHostEnvironment)
        {
            InputManager = inputManager;
            WebHostEnvironment = webHostEnvironment;
        }



        // [HttpGet("")]
        // public async Task<object> Execute()
        // {
        //     try
        //     {
        //         // if(string.IsNullOrEmpty("agent")){
        //         //     return BadRequest("No agent specified");
        //         // }
        //         return WebHostEnvironment.WebRootPath;
        //        /* var fileContent = System.IO.File.ReadAllText("")
        //         var api = new ScriptAPI() { InputStorage = InputManager };
        //         var value = await CSharpScript.EvaluateAsync(script, globals: api);
        //         return value;*/
        //     }
        //     catch (Exception ex)
        //     {
        //         return "Could not execute agent:" + ex.ToString();
        //     }

        // }
    }

    public class ScriptAPI
    {
        public IInputStorage InputStorage;
    }
}