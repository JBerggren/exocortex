using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ExoCortex.Web.Models;
using ExoCortex.Web.Services;
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
        public ExecuteController(IInputStorage inputManager)
        {
            InputManager = inputManager;
        }



        [HttpGet("/")]
        public async Task<object> Execute()
        {
            var script = @"
            
            ";
            return await CSharpScript.EvaluateAsync("Directory.GetCurrentDirectory()", ScriptOptions.Default.WithImports("System.IO"));
        }
    }
}