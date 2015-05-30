using System.Web.Mvc;
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

            return View(questions);
        }
    }
}