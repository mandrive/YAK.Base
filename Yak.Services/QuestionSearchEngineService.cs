using System;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class QuestionSearchEngineService : ISearchEngineExtendedService<Question>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISearchEngineService<Question> _questionSearchEngineService;

        public QuestionSearchEngineService(DatabaseContext databaseContext, ISearchEngineService<Question> questionSearchEngineService)
        {
            _databaseContext = databaseContext;
            _questionSearchEngineService = questionSearchEngineService;
        }

        public Question GetById(int id)
        {
            return _questionSearchEngineService.GetById(id);
        }

        public IEnumerable<Question> GetAll()
        {
            return _questionSearchEngineService.GetAll();
        }

        public IEnumerable<Question> Filter(Func<Question, bool> filter)
        {
            return _questionSearchEngineService.GetAll().Where(filter);
        }

        public void Add(Question dto)
        {
            _questionSearchEngineService.AddToIndex(dto);
        }

        public void Delete(Question dto)
        {
            throw new NotImplementedException();
        }

        public void Update(Question dto)
        {
            _questionSearchEngineService.AddToIndex(dto);
        }

        public IEnumerable<Question> GetFromIndex(string query)
        {
            return _questionSearchEngineService.GetFiltered(query);
        }
    }
}
