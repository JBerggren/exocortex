namespace ExoCortex.Web.Agents.Interface
{
    public class ScriptReturnValue
    {
        public void Content(string content){
            Value = content;
            ValueType = ValueTypeEnum.Value;
        }
        public void Url(string url){
            Value = url;
            ValueType = ValueTypeEnum.Url;
        }
        
        public string Value { get; set; }
        public ValueTypeEnum ValueType { get; set; }
        public enum ValueTypeEnum
        {
            Value,
            Url
        }
    }
}