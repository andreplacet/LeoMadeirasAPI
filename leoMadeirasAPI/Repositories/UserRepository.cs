using System;
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
        public async Task<User> GetUser(string username, string password)
        {
            try
            {   // validação feita aqui, somente pra nao permitir passar valores invalidos
                // o correto seria uma função apenas pra criar o usuario, mas como estou usando um banco em memoria
                // fiz desse jeito
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)
                                        || username == "string" || password == "string")
                {
                    return null;

                }
                _dbContext.Add(new User(username, password));
                await _dbContext.SaveChangesAsync();

                var validUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
                return validUser;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}