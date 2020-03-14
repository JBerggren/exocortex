using ExoCortex.Web.Framework.Services;

namespace ExoCortex.Web.Agents.Interface
{
    public class ScriptAPI
    {
        public ScriptAPI(IInputStorage inputStorage){
            InputStorage = inputStorage;
            ReturnValue = new ScriptReturnValue();
        }
        public IInputStorage InputStorage {get;set;}
        public ScriptReturnValue ReturnValue {get;set;}
    }
}