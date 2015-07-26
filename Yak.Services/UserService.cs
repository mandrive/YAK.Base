using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class UserService : IUserValidationService
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
            try
            {
                var dbEntity = new Database.Entities.User
                {
                    Username = entity.Username,
                    Email = entity.Email,
                    Password = string.IsNullOrEmpty(entity.Password) ? null : GetPasswordHash(entity.Password)
                };

                _databaseContext.Users.Add(dbEntity);
                _databaseContext.SaveChanges();

                entity.Id = dbEntity.Id;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private string GetPasswordHash(string password)
        {
            string hashString;

            using (SHA512 shaM = new SHA512Managed())
            {
                hashString = BitConverter.ToString(shaM.ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", string.Empty).ToLower();
            }

            return hashString;
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Validate(string username, string password)
        {
            var user = _databaseContext.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                using (SHA512 shaM = new SHA512Managed())
                {
                    var hash = BitConverter.ToString(shaM.ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", string.Empty).ToLower();
                    var userPasswordHash = user.Password.ToLower();

                    return hash.SequenceEqual(userPasswordHash);
                }
            }

            return false;
        }
    }
}
