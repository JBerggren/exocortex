using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ExoCortex.Web.Models;
using ExoCortex.Web.Framework.Services;
using Microsoft.AspNetCore.Mvc;
using ExoCortex.Web.Models.Response;

namespace ExoCortex.Web.Controllers
{
    [ApiController]
    [Route("input")]
    public class InputController : ControllerBase
    {
        public InputController(IInputStorage inputManager){
            InputManager = inputManager;
        }

        public IInputStorage InputManager { get; }

        [HttpGet("count")]
        public async Task<int> Count(){
            return await InputManager.Count();
        }

        [HttpPost]
        public async Task<IActionResult> Store([FromBody] JsonElement param){
            try{
                var type = param.GetProperty("type").GetString();
                var time = DateTime.Now;
                if(param.TryGetProperty("time", out var timeElement)){
                    time = timeElement.GetDateTime();
                }
                var dataProperties = param.GetProperty("data").EnumerateObject();
                var data = new Dictionary<string,string>();
                while(dataProperties.MoveNext()){
                    data.Add(dataProperties.Current.Name,dataProperties.Current.Value.GetRawText());
                }
                var item = new InputItem(type,data,time);
                return Content(await InputManager.Save(item));
            }catch(System.Exception ex){
                return Problem(ex.ToString());
            }
        }

        [HttpGet]
        public async Task<InputQueryResult> Get(string type = "", int limit =1){
            return await InputManager.GetLatest(type,limit);
        }
        
    }
}