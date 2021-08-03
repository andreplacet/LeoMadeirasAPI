using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using leoMadeirasAPI.data;
using leoMadeirasAPI.interfaces;
using leoMadeirasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace leoMadeirasAPI.Repositories
{
    public class UserRepository : IRepository
    {
        private readonly leoDbContext _dbContext;

        public UserRepository(leoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<dynamic> GetUser(User user)
        {
            try
            {
                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();

                var validUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password);
                return validUser;
            }
            catch (Exception)
            {
                return null;
            }
            //var users = new List<User>();
            //users.Add(new User { Id = 1, Username = "andre", Password = "a@jkwm!q1paCm8c" });
            //users.Add(new User { Id = 2, Username = "luiz", Password = "123andre" });
            //return users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() || x.Password.ToLower() == x.Password.ToLower());
        }
    }
}