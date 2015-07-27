using System.Web.Mvc;
using WebGrease.Css.Extensions;
using Yak.DTO;
using Yak.Services.Interfaces;

namespace Yak.Web.Controllers
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
            var markdown = new MarkdownDeep.Markdown();
            questions.ForEach(q => q.Content = markdown.Transform(q.Content));

            return View(questions);
        }
    }
}