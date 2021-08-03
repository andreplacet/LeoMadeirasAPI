using System.Threading.Tasks;
using leoMadeirasAPI.Models;

namespace leoMadeirasAPI.interfaces
{
    public interface IRepository
    {
        public Task<User> GetUser(string username, string password);
    }
}