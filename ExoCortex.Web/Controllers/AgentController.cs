using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ExoCortex.Web.Models.Request;
using ExoCortex.Web.Models.Response;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ExoCortex.Web.Controllers
{
    [ApiController]
    [Route("agent")]
    public class AgentController : ControllerBase
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public AgentController(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetAgents()
        {
            try
            {
                var folder = Path.GetFullPath(Path.Combine(WebHostEnvironment.ContentRootPath, "Agents"));
                var files = Directory.GetFiles(folder,"*.txt");
                return Ok(new AgentResponse(files.Select(x=> Path.GetFileNameWithoutExtension(x)).ToArray()));
            }
            catch (Exception ex)
            {
                return Problem("Could not get agent. " + ex.Message);
            }
        }

        [HttpGet("{agent}")]
        public IActionResult GetAgent(string agent)
        {
            try
            {
                var scriptContent = System.IO.File.ReadAllText(GetAgentPath(agent));
                return Content(scriptContent);
            }
            catch (Exception ex)
            {
                return Problem("Could not get agent. " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateAgent(AgentRequest request)
        {
            try
            {
                if(string.IsNullOrEmpty(request.Agent) || string.IsNullOrEmpty(request.Content)){
                    return Problem("Missing agent or content argument!");
                }
                var path = GetAgentPath(request.Agent);
                System.IO.File.WriteAllText(path,request.Content);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem("Could not get agent. " + ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteAgent(string agent){
            try{
             var path = GetAgentPath(agent);
             System.IO.File.Delete(path);
             return Ok();
            }
            catch (Exception ex)
            {
                return Problem("Could not delete agent. " + ex.Message);
            }
        }

        private string GetAgentPath(string agent)
        {
            var agentPath = Path.GetFullPath(Path.Combine(WebHostEnvironment.ContentRootPath, "Agents/" + agent + ".txt"));
            if (!agentPath.StartsWith(WebHostEnvironment.ContentRootPath))
            {
                throw new Exception("Security issue! Path outside of web root! Agentname:" + agent);
            }
            return agentPath;
        }
    }
}