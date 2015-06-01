using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Services.Utils;
using Yak.Web.Models;

namespace Yak.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ISearchEngineExtendedService<Question> _questionService;
        private readonly IService<User> _userService;
        private readonly IndexRebuilder _indexRebuilder;

        public QuestionController(ISearchEngineExtendedService<Question> questionService, IService<User> userService, IndexRebuilder indexRebuilder)
        {
            _questionService = questionService;
            _userService = userService;
            _indexRebuilder = indexRebuilder;
        }

        public ActionResult View(int id)
        {
            var question = _questionService.GetById(id);
            question.Content = new MarkdownDeep.Markdown().Transform(question.Content);
            return View(question);
        }

        public ActionResult FilterQuestions(string query)
        {
            var filteredQuestions = _questionService.GetFromIndex(query);
            var jsonResponse = JsonConvert.SerializeObject(filteredQuestions.Select(n => new
            {
                id = n.Id,
                value = n.Title,
                content = n.Content
            }),
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(QuestionForm questionForm)
        {
            var dto = questionForm.ToDto();
            _questionService.Add(dto);

            return RedirectToAction("View", new { id = dto.Id });
        }

        public ActionResult RebuildIndexes()
        {
            _indexRebuilder.RebuildQuestionsIndex();

            return RedirectToAction("Index", "Stack");
        }
    }
}
