using System;
using System.Linq;
using System.Web.Mvc;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Web.BaseUtils;
using Yak.Web.Models;

namespace Yak.Web.Controllers
{
    public class AnswerController : BaseController
    {
        private readonly IService<Answer> _answerService;
        private readonly IService<Question> _questionService;
        private readonly IService<Vote> _voteService;

        public AnswerController(
            IService<Answer> answerService,
            IService<Question> questionService,
            IService<Vote> voteService)
        {
            _answerService = answerService;
            _questionService = questionService;
            _voteService = voteService;
        }

        public ActionResult PostAnswer(AnswerViewModel answerViewModel)
        {
            _answerService.Add(new Answer
            {
                Author = User.DatabaseUser,
                Content = answerViewModel.Answer.Content,
                CreateDate = DateTime.UtcNow,
                LastModificationDate = DateTime.UtcNow,
                QuestionId = answerViewModel.QuestionId
            });
            
            return RedirectToAction("View", new { controller = "Question", id = answerViewModel.QuestionId });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var answer = _answerService.GetById(id);

            return View("AnswerForm", new AnswerForm(answer));
        }

        [HttpPost]
        public ActionResult Edit(AnswerForm answerForm)
        {
            var dto = answerForm.ToDto();
            var originalAnswer = _answerService.GetById(answerForm.Id);

            originalAnswer.Content = answerForm.Content;
            originalAnswer.LastModificationDate = DateTime.UtcNow;

            _answerService.Update(originalAnswer);

            return RedirectToAction("View", new { id = dto.QuestionId, controller = "Question" });
        }

        public JsonResult VoteUp(int answerId)
        {
            var newVote = new Vote
            {
                PointValue = true,
                User = User.DatabaseUser
            };

            _voteService.Add(newVote);

            var answer = _answerService.GetById(answerId);
            answer.Votes.Add(newVote);

            var voteDown = answer.Votes.SingleOrDefault(v => v.User.Id == User.DatabaseUser.Id && !v.PointValue);

            if (voteDown != null)
            {
                answer.Votes.Remove(voteDown);
                _voteService.Delete(voteDown);
            }

            var allVotesUp = answer.Votes.Count(v => v.PointValue);
            var allVotesDown = answer.Votes.Count(v => !v.PointValue);

            answer.RankPoint = allVotesUp - allVotesDown;

            _answerService.Update(answer);

            return Json(new { rankPoint = answer.RankPoint });
        }

        public JsonResult VoteDown(int answerId)
        {
            var newVote = new Vote
            {
                PointValue = false,
                User = User.DatabaseUser
            };

            _voteService.Add(newVote);

            var answer = _answerService.GetById(answerId);
            answer.Votes.Add(newVote);

            var voteUp = answer.Votes.SingleOrDefault(v => v.User.Id == User.DatabaseUser.Id && v.PointValue);

            if (voteUp != null)
            {
                answer.Votes.Remove(voteUp);
                _voteService.Delete(voteUp);
            }

            var allVotesUp = answer.Votes.Count(v => v.PointValue);
            var allVotesDown = answer.Votes.Count(v => !v.PointValue);

            answer.RankPoint = allVotesUp - allVotesDown;

            _answerService.Update(answer);

            return Json(new { rankPoint = answer.RankPoint });
        }

        public JsonResult MarkAnswer(int answerId, bool isCorrect)
        {
            var answer = _answerService.GetById(answerId);

            if (answer.Author.Id != User.DatabaseUser.Id)
            {
                throw new Exception("Unauthorized user tries to accept others user answer!");
            }

            answer.IsCorrect = isCorrect;

            _answerService.Update(answer);

            return Json(new { Result = true });
        }
    }
}