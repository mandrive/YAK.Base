using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Yak.Services.Interfaces;
using Yak.DTO;

namespace Yak.Web.Controllers
{
    public class StackController : Controller
    {
        private IService<Question> _questionService;

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