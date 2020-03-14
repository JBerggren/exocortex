using System.Collections.Generic;

namespace ExoCortex.Web.Models{
    public class InputQueryResult{
        public IList<InputItem> Items {get;set;}

        public InputQueryResult(IList<InputItem> items){
            Items = items;
        }
    }
}