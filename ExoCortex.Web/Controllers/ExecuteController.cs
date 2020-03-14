using System;
using ExoCortex.Web.Framework.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;

namespace ExoCortex.Web.Controllers
{
    [ApiController]
    [Route("execute")]
    public class ExecuteController : ControllerBase
    {
        public IInputStorage InputManager { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public ExecuteController(IInputStorage inputManager, IWebHostEnvironment webHostEnvironment)
        {
            InputManager = inputManager;
            WebHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<string> ExecuteAgent(string agent)
        {
            try
            {
                if(string.IsNullOrEmpty(agent)){
                    return "No agent specified";
                }
                var scriptContent =  System.IO.File.ReadAllText(Path.Combine(WebHostEnvironment.ContentRootPath,"Agents/" + agent + ".txt"));
                var api = new ScriptAPI() { InputStorage = InputManager };
                var value = await CSharpScript.EvaluateAsync<string>(scriptContent, globals: api);
                return value;
            }
            catch (Exception ex)
            {
                return "Could not execute agent:" + ex.ToString();
            }

        }
    }

    public class ScriptAPI
    {
        public IInputStorage InputStorage;
    }
}