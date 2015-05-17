using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Yak.DTO;
using Yak.Services;
using Yak.Services.Interfaces;
using Yak.Services.Utils;

namespace Yak.Web.Controllers
{
    public class QuestionController : Controller
    {
        private ISearchEngineExtendedService<Question> _questionService;
        private IService<User> _userService;
        private IndexRebuilder _indexRebuilder;

        public QuestionController(ISearchEngineExtendedService<Question> questionService, IService<User> userService, IndexRebuilder indexRebuilder)
        {
            _questionService = questionService;
            _userService = userService;
            _indexRebuilder = indexRebuilder;
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

            return RedirectToAction("View", new { id = question.Id });
        }

        public ActionResult RebuildIndexes()
        {
            _indexRebuilder.RebuildQuestionsIndex();

            return RedirectToAction("Index", "Stack");
        }
    }
}
