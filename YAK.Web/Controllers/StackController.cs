using System.Web.Mvc;
using MarkdownSharp;
using WebGrease.Css.Extensions;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Web.Controllers
{
    public class StackController : Controller
    {
        private readonly ISearchEngineExtendedService<Question> _questionService;

        public StackController(ISearchEngineExtendedService<Question> questionService)
        {
            _questionService = questionService;
        }

        public ActionResult Index()
        {
            var questions = _questionService.GetAll();

            questions.ForEach(q => q.Content = new Markdown().Transform(q.Content));

            return View(questions);
        }
    }
}