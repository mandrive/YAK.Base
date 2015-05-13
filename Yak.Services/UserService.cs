using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class UserService : IService<User>
    {
        private DatabaseContext _databaseContext;

        public UserService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public User GetById(int id)
        {
            return new User(_databaseContext.Users.Find(id));
        }

        public IEnumerable<User> GetAll()
        {
            var users = new List<User>();

            foreach (var user in _databaseContext.Users.ToList())
            {
                users.Add(new User(user));
            }

            return users;
        }

        public IEnumerable<User> Filter(Func<User, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
