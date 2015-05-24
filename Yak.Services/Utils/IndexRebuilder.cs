using System.Collections.Generic;
using Yak.Database;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;

namespace Yak.Services.Utils
{
    public class IndexRebuilder
    {
        private DatabaseContext _databaseContext;
        private ISearchEngineService<Question> _questionSearchEngineService;

        public IndexRebuilder(DatabaseContext databaseContext, ISearchEngineService<Question> questionSearchEngineService)
        {
            _databaseContext = databaseContext;
            _questionSearchEngineService = questionSearchEngineService;
        }

        public void RebuildQuestionsIndex()
        {
            var questions = new List<Question>();

            foreach (var question in _databaseContext.Questions.Include("Author"))
            {
                var dtoQuestion = new Question(question);
                _questionSearchEngineService.AddToIndex(dtoQuestion);
            }
        }
    }
}
