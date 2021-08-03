using leoMadeirasAPI.Models;

namespace leoMadeirasAPI.interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}