using System.Threading.Tasks;
using ExoCortex.Web.Models;
using ExoCortex.Web.Models.Response;

namespace ExoCortex.Web.Framework.Services
{
    public interface IInputStorage
    {
        Task<string> Save(InputItem item);
        Task<long> Count();
        Task<InputQueryResult> GetLatest(string type,int limit);
    }
}