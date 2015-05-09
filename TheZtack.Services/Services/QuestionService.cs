using System;
using System.Collections.Generic;
using System.Linq;
using TheZtack.Data.Interfaces;
using TheZtack.Database;
using TheZtack.Database.Entities;

namespace TheZtack.Data.Services
{
    public class QuestionService : IService<Question>
    {
        private readonly DatabaseContext _databaseContext;

        public QuestionService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<Question> GetAll()
        {
            return _databaseContext.Questions.ToList();
        }

        public Question GetById(int id)
        {
            return _databaseContext.Questions.Find(id);
        }

        public IEnumerable<Question> Filter(Func<Question, bool> predicate)
        {
            return _databaseContext.Questions.Where(predicate);
        }

        public int Add(Question entity)
        {
            _databaseContext.Questions.Add(entity);
            _databaseContext.SaveChanges();

            return entity.Id;
        }

        public void Delete(Question entity)
        {
            _databaseContext.Questions.Remove(entity);
            _databaseContext.SaveChanges();
        }

        public void Update(Question entity)
        {
            var existingEntity = GetById(entity.Id);

            existingEntity.Content = entity.Content;
            existingEntity.LastModificationDate = entity.LastModificationDate;
            existingEntity.RankPoint = entity.RankPoint;
            existingEntity.Title = entity.Title;
            existingEntity.UserId = entity.UserId;

            _databaseContext.SaveChanges();
        }
    }
}
