using System.Web.Mvc;
using TheZtack.Data.Interfaces;
using TheZtack.Database.Entities;

namespace TheZtack.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Question> _questionService;

        public HomeController(IService<Question> questionService)
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