using System;
using System.Collections.Generic;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Services
{
    public class QuestionComplexService : IService<Question>
    {
        private IService<User> _userService;
        private IService<Tag> _tagsService;
        private ISearchEngineExtendedService<Question> _searchEngineService;

        public QuestionComplexService(IService<User> userService, IService<Tag> tagsService, ISearchEngineExtendedService<Question> searchEngineService)
        {
            _searchEngineService = searchEngineService;
            _userService = userService;
            _tagsService = tagsService;
        }

        public void Add(Question entity)
        {
            _searchEngineService.Add(entity);
        }

        public void Delete(Question entity)
        {
            _searchEngineService.Delete(entity);
        }

        public IEnumerable<Question> Filter(Func<Question, bool> filter)
        {
            return _searchEngineService.Filter(filter);
        }

        public IEnumerable<Question> GetAll()
        {
            return _searchEngineService.GetAll();
        }

        public Question GetById(int id)
        {
            return _searchEngineService.GetById(id);
        }

        public void Update(Question entity)
        {
            _searchEngineService.Update(entity);
        }
    }
}
