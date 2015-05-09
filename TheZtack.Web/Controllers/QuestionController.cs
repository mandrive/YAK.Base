using System;
using System.Web.Mvc;
using TheZtack.Data.Interfaces;
using TheZtack.Database.Entities;

namespace TheZtack.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IService<Question> _questionService;

        public QuestionController(IService<Question> questionService)
        {
            _questionService = questionService;
        }

        public ActionResult View(int id)
        {
            return View(_questionService.GetById(id));
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Question question)
        {
            var id =_questionService.Add(question);

            return Redirect(string.Format(@"View\{0}", id));
        }
    }
}
