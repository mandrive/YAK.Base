using Yak.Database;
using Yak.DTO;
using Yak.SearchEngine.Interfaces;

namespace Yak.Services.Utils
{
    public class IndexRebuilder
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISearchEngineService<Question> _questionSearchEngineService;

        public IndexRebuilder(DatabaseContext databaseContext, ISearchEngineService<Question> questionSearchEngineService)
        {
            _databaseContext = databaseContext;
            _questionSearchEngineService = questionSearchEngineService;
        }

        public void RebuildQuestionsIndex()
        {
            foreach (var question in _databaseContext.Questions.Include("Author").Include("Tags").Include("Answers").Include("Comments").Include("Votes"))
            {
                var dtoQuestion = new Question(question);
                _questionSearchEngineService.AddToIndex(dtoQuestion);
            }
        }
    }
}
