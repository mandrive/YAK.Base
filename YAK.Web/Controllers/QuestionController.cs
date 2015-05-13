using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Yak.DTO;
using Yak.Services;
using Yak.Services.Interfaces;

namespace Yak.Web.Controllers
{
    public class QuestionController : Controller
    {
        private IService<Question> _questionService;
        private IService<User> _userService;

        public QuestionController(IService<Question> questionService, IService<User> userService)
        {
            _questionService = questionService;
            _userService = userService;
        }

        public ActionResult View(int id)
        {
            return View(_questionService.GetById(id));
        }

        public ActionResult FilterQuestions(string query)
        {
            var filteredQuestions = _questionService.Filter(q => q.Title.Contains(query));
            return Json(
                JsonConvert.SerializeObject(filteredQuestions.Select(n => new
                {
                    value = n.Title
                }),
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Question question)
        {
            question.Author = _userService.GetById(1);
            question.CreateDate = DateTime.Now;
            question.LastModificationDate = DateTime.Now;
            _questionService.Add(question);

            return Redirect(string.Format(@"View\{0}", question.Id));
        }
    }
}
