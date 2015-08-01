using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class VoteService : IService<Vote>
    {
        private DatabaseContext _databaseContext;

        public VoteService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Vote entity)
        {
            var dbEntity = new Database.Entities.Vote
            {
                PointValue = entity.PointValue,
                User = _databaseContext.Users.Find(entity.User.Id)
            };

            _databaseContext.Votes.Add(dbEntity);
            _databaseContext.SaveChanges();

            entity.Id = dbEntity.Id;
        }

        public void Delete(Vote entity)
        {
            var vote = _databaseContext.Votes.Find(entity.Id);
            _databaseContext.Votes.Remove(vote);
        }

        public IEnumerable<Vote> Filter(Func<Vote, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public IEnumerable<Vote> GetAll()
        {
            return _databaseContext.Votes.Select(t => new Vote(t));
        }

        public Vote GetById(int id)
        {
            var dbVote = _databaseContext.Votes.Find(id);
            if (dbVote == null) return null;

            return new Vote(dbVote);
        }

        public void Update(Vote entity)
        {
            throw new NotImplementedException();
        }
    }
}
