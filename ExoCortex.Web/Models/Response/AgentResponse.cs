namespace ExoCortex.Web.Models.Response
{
    public class AgentResponse {
        public string[] Agents {get;set;}

        public AgentResponse(string[] agents){
            Agents = agents;
        }
    }
}