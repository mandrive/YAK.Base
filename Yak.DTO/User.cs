using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = Yak.Database.Entities;

namespace Yak.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public User() { }

        public User(Entities.User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
        }
    }
}
