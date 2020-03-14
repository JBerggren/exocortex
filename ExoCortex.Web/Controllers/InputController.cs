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
    public class InputController : ControllerBase
    {
        public InputController(IInputManager inputManager){
            InputManager = inputManager;
        }

        public IInputManager InputManager { get; }

        [HttpGet]
        public async Task<int> Count(){
            return await InputManager.Count();
        }

        [HttpPost]
        public async Task<string> Store([FromBody] JsonElement param){
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
                return await InputManager.Save(item);
            }catch(System.Exception ex){
                return ex.ToString();
            }
        }
        
    }
}