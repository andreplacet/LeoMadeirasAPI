using System;

namespace leoMadeirasAPI.Models
{
    public class User
    {
        public User()
        {

        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? ExpireTokenDate { get; set; }
    }

}