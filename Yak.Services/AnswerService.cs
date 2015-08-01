using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class AnswerService : IService<Answer>
    {
        private DatabaseContext _databaseContext;

        public AnswerService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Answer entity)
        {
            var dbEntity = new Database.Entities.Answer
            {
                Id = entity.Id,
                Content = entity.Content,
                CreateDate = entity.CreateDate,
                LastModificationDate = entity.CreateDate,
                RankPoint = entity.RankPoint,
                IsCorrect = entity.IsCorrect,
                Author = _databaseContext.Users.Find(entity.Author.Id),
                Comments = entity.Comments.Select(c => _databaseContext.Comments.Find(c.Id)).ToList(),
                Votes = entity.Votes.Select(v=> _databaseContext.Votes.Find(v.Id)).ToList()
            };

            _databaseContext.Answers.Add(dbEntity);
            _databaseContext.SaveChanges();

            entity.Id = dbEntity.Id;
        }

        public void Delete(Answer entity)
        {
            var answer = _databaseContext.Answers.Find(entity.Id);
            _databaseContext.Answers.Remove(answer);
        }

        public IEnumerable<Answer> Filter(Func<Answer, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public IEnumerable<Answer> GetAll()
        {
            return _databaseContext.Answers.Select(t => new Answer(t));
        }

        public Answer GetById(int id)
        {
            var dbAnswer = _databaseContext.Answers.Find(id);
            if (dbAnswer == null) return null;

            return new Answer(dbAnswer);
        }

        public void Update(Answer entity)
        {
            throw new NotImplementedException();
        }
    }
}
