using System.Collections.Generic;
using System.Linq;
using leoMadeirasAPI.Models;

namespace leoMadeirasAPI.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "andre", Password = "a@jkwm!q1paCm8c" });
            users.Add(new User { Id = 2, Username = "luiz", Password = "" });
            return users.Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault();
        }
    }
}