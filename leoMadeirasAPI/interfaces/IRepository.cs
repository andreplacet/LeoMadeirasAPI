using System.Threading.Tasks;
using leoMadeirasAPI.Models;

namespace leoMadeirasAPI.interfaces
{
    public interface IRepository
    {
        public Task<dynamic> GetUser(User user);
    }
}