using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yak.Database;
using Yak.Services.Interfaces;
using System.Data.Entity;
using Yak.DTO;
using System.Linq.Expressions;

namespace Yak.Services
{
    public class QuestionService : IService<Question>
    {
        private DatabaseContext _databaseContext;

        public QuestionService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Question GetById(int id)
        {
            return new Question(_databaseContext.Questions.Find(id));
        }

        public IEnumerable<Question> GetAll()
        {
            var questions = new List<Question>();

            foreach (var question in _databaseContext.Questions.ToList())
            {
                questions.Add(new Question(question));
            }

            return questions;
        }

        public IEnumerable<Question> Filter(Func<Question, bool> filter)
        {
            return GetAll().Where(filter);
        }

        public void Add(Question entity)
        {
            var dbEntity = new Database.Entities.Question()
            {
                Title = entity.Title,
                Content = entity.Content,
                CreateDate = entity.CreateDate,
                LastModificationDate = entity.LastModificationDate,
                Author = _databaseContext.Users.Find(entity.Author.Id)
            };

            _databaseContext.Questions.Add(dbEntity);
            _databaseContext.SaveChanges();
        }

        public void Delete(Question entity)
        {
            var dbEntity = _databaseContext.Questions.Find(entity.Id);

            _databaseContext.Questions.Remove(dbEntity);
            _databaseContext.SaveChanges();
        }

        public void Update(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
