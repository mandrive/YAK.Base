using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class QuestionService : ISearchEngineExtendedService<Question>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISearchEngineService<Question> _questionSearchEngineService;

        public QuestionService(DatabaseContext databaseContext, ISearchEngineService<Question> questionSearchEngineService)
        {
            _databaseContext = databaseContext;
            _questionSearchEngineService = questionSearchEngineService;
        }

        public Question GetById(int id)
        {
            return new Question(_databaseContext.Questions.Find(id));
        }

        public IEnumerable<Question> GetAll()
        {
            return _questionSearchEngineService.GetAll().ToList();
        }

        public IEnumerable<Question> Filter(Func<Question, bool> filter)
        {
            return _questionSearchEngineService.GetAll().Where(filter);
        }

        public void Add(Question dto)
        {
            var dbEntity = new Database.Entities.Question()
            {
                Title = dto.Title,
                Content = dto.Content,
                CreateDate = dto.CreateDate,
                LastModificationDate = dto.LastModificationDate,
                Author = _databaseContext.Users.Single(u => u.Username == dto.Author)
            };

            _databaseContext.Questions.Add(dbEntity);
            _databaseContext.SaveChanges();

            dto.Id = dbEntity.Id;

            _questionSearchEngineService.AddToIndex(dto);
        }

        public void Delete(Question dto)
        {
            var dbEntity = _databaseContext.Questions.Find(dto.Id);

            _databaseContext.Questions.Remove(dbEntity);
            _databaseContext.SaveChanges();
        }

        public void Update(Question entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetFromIndex(string query)
        {
            return _questionSearchEngineService.GetFiltered(query);
        }
    }
}
