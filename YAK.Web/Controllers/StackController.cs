using System.Web.Mvc;
using WebGrease.Css.Extensions;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Web.BaseUtils;

namespace Yak.Web.Controllers
{
    public class StackController : BaseController
    {
        private readonly IService<Question> _questionService;
        private readonly IService<Answer> _answerService;
        private readonly IService<Vote> _voteService;
        private readonly IService<Comment> _commentService;

        public StackController(
            IService<Question> questionService,
            IService<Answer> answerService,
            IService<Comment> commentService,
            IService<Vote> voteService)
        {
            _questionService = questionService;
            _answerService = answerService;
            _voteService = voteService;
            _commentService = commentService;
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