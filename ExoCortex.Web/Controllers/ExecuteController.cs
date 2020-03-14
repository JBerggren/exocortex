using System;
using ExoCortex.Web.Framework.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;
using ExoCortex.Web.Agents.Interface;

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
        public async Task<IActionResult> ExecuteAgent(string agent)
        {
            try
            {
                if (string.IsNullOrEmpty(agent))
                {
                    return BadRequest("No agent specified");
                }
                var scriptContent = System.IO.File.ReadAllText(Path.Combine(WebHostEnvironment.ContentRootPath, "Agents/" + agent + ".txt"));
                var api = new ScriptAPI(InputManager);
                var value = await CSharpScript.EvaluateAsync<string>(scriptContent, globals: api);
                switch (api.ReturnValue.ValueType)
                {
                    case ScriptReturnValue.ValueTypeEnum.Value:
                        return Content(api.ReturnValue.Value);
                    case ScriptReturnValue.ValueTypeEnum.Url:
                        return Redirect(api.ReturnValue.Value);
                    default:
                        return Content(api.ReturnValue.Value);
                }
            }
            catch (Exception ex)
            {
                return Problem("Could not execute agent:" + ex.ToString());
            }

        }
    }
}