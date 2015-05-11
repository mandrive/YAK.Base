using System.Web.Mvc;
using YAK.Base.Data.Interfaces;
using YAK.Base.Database.Entities;

namespace YAK.Base.Web.Controllers
{
    public class StackController : Controller
    {
        private readonly IService<Question> _questionService;

        public StackController(IService<Question> questionService)
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