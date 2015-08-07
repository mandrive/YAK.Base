using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Services.Utils;
using Yak.Web.Models;
using Yak.Web.Utils;
using Yak.Web.BaseUtils;

namespace Yak.Web.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly ISearchEngineExtendedService<Question> _questionSearchService;
        private readonly IService<Question> _questionService;
        private readonly IService<User> _userService;
        private readonly IService<Vote> _voteService;
        private readonly IndexRebuilder _indexRebuilder;

        public QuestionController(ISearchEngineExtendedService<Question> questionSearchService, IService<Question> questionService, IService<User> userService, IService<Vote> voteService, IndexRebuilder indexRebuilder)
        {
            _questionSearchService = questionSearchService;
            _questionService = questionService;
            _userService = userService;
            _indexRebuilder = indexRebuilder;
            _voteService = voteService;
        }

        public ActionResult View(int id)
        {
            var question = _questionService.GetById(id);
            var questionViewModel = new QuestionViewModel(question);

            return View(questionViewModel);
        }

        public ActionResult ViewQuestionContent(int id)
        {
            var question = _questionService.GetById(id);
            var questionViewModel = new QuestionViewModel(question);

            return PartialView(questionViewModel);
        }

        public ActionResult FilterQuestions(string query)
        {
            var filteredQuestions = _questionSearchService.GetFromIndex(query);
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

        public JsonResult VoteUp(int questionId)
        {
            var newVote = new Vote
            {
                PointValue = true,
                User = User.DatabaseUser
            };

            _voteService.Add(newVote);

            var question = _questionService.GetById(questionId);
            question.Votes.Add(newVote);

            var voteDown = question.Votes.SingleOrDefault(v => v.User.Id == User.DatabaseUser.Id && !v.PointValue);

            if (voteDown != null)
            {
                question.Votes.Remove(voteDown);
                _voteService.Delete(voteDown);
            }

            var allVotesUp = question.Votes.Count(v => v.PointValue);
            var allVotesDown = question.Votes.Count(v => !v.PointValue);

            question.RankPoint = allVotesUp - allVotesDown;

            _questionService.Update(question);

            return Json(new { rankPoint = question.RankPoint });
        }

        public JsonResult VoteDown(int questionId)
        {
            var newVote = new Vote
            {
                PointValue = false,
                User = User.DatabaseUser
            };

            _voteService.Add(newVote);

            var question = _questionService.GetById(questionId);
            question.Votes.Add(newVote);

            var voteUp = question.Votes.SingleOrDefault(v => v.User.Id == User.DatabaseUser.Id && v.PointValue);

            if (voteUp != null)
            {
                question.Votes.Remove(voteUp);
                _voteService.Delete(voteUp);
            }

            var allVotesUp = question.Votes.Count(v => v.PointValue);
            var allVotesDown = question.Votes.Count(v => !v.PointValue);

            question.RankPoint = allVotesUp - allVotesDown;

            _questionService.Update(question);

            return Json(new { rankPoint = question.RankPoint });
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("QuestionForm");
        }

        [HttpPost]
        public ActionResult New(QuestionForm questionForm)
        {
            var dto = questionForm.ToDto();
            dto.Author = User.DatabaseUser;
            _questionService.Add(dto);

            return RedirectToAction("View", new { id = dto.Id });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var question = _questionService.GetById(id);

            return View("QuestionForm", new QuestionForm(question));
        }

        [HttpPost]
        public ActionResult Edit(QuestionForm questionForm)
        {
            var dto = questionForm.ToDto();
            _questionService.Update(dto);

            return RedirectToAction("View", new { id = dto.Id });
        }

        public ActionResult RebuildIndexes()
        {
            _indexRebuilder.RebuildQuestionsIndex();

            return RedirectToAction("Index", "Stack");
        }
    }
}
