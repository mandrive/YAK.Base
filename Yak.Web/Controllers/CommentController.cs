using System.Linq;
using System.Web.Mvc;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Web.BaseUtils;
using Yak.Web.Models;

namespace Yak.Web.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IService<Comment> _commentService;
        private readonly IService<Answer> _answerService;
        private readonly IService<Vote> _voteService;

        public CommentController(
            IService<Comment> commentService,
            IService<Answer> answerService,
            IService<Vote> voteService)
        {
            _commentService = commentService;
            _answerService = answerService;
            _voteService = voteService;
        }

        [HttpPost]
        public ActionResult AddComment(CommentForm commentForm)
        {
            var dto = commentForm.ToDto();

            dto.Author = User.DatabaseUser;

            _commentService.Add(dto);

            var questionId = dto.QuestionId.HasValue ? dto.QuestionId.Value : _answerService.GetById(commentForm.AnswerId.Value).QuestionId;

            return RedirectToAction("View", new { id = questionId, controller = "Question" });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var comment = _commentService.GetById(id);

            return View("CommentForm", new CommentForm(comment));
        }

        [HttpPost]
        public ActionResult Edit(CommentForm commentForm)
        {
            var dto = commentForm.ToDto();

            _commentService.Update(dto);

            var questionId = dto.QuestionId.HasValue ? dto.QuestionId.Value : _answerService.GetById(commentForm.AnswerId.Value).QuestionId;

            return RedirectToAction("View", new { id = questionId, controller = "Question" });
        }

        public JsonResult VoteUp(int commentId)
        {
            var newVote = new Vote
            {
                PointValue = true,
                User = User.DatabaseUser
            };

            _voteService.Add(newVote);

            var comment = _commentService.GetById(commentId);
            comment.Votes.Add(newVote);

            var voteDown = comment.Votes.SingleOrDefault(v => v.User.Id == User.DatabaseUser.Id && !v.PointValue);

            if (voteDown != null)
            {
                comment.Votes.Remove(voteDown);
                _voteService.Delete(voteDown);
            }

            var allVotesUp = comment.Votes.Count(v => v.PointValue);
            var allVotesDown = comment.Votes.Count(v => !v.PointValue);

            comment.RankPoint = allVotesUp - allVotesDown;

            _commentService.Update(comment);

            return Json(new { rankPoint = comment.RankPoint });
        }

        public JsonResult VoteDown(int commentId)
        {
            var newVote = new Vote
            {
                PointValue = false,
                User = User.DatabaseUser
            };

            _voteService.Add(newVote);

            var comment = _commentService.GetById(commentId);
            comment.Votes.Add(newVote);

            var voteUp = comment.Votes.SingleOrDefault(v => v.User.Id == User.DatabaseUser.Id && v.PointValue);

            if (voteUp != null)
            {
                comment.Votes.Remove(voteUp);
                _voteService.Delete(voteUp);
            }

            var allVotesUp = comment.Votes.Count(v => v.PointValue);
            var allVotesDown = comment.Votes.Count(v => !v.PointValue);

            comment.RankPoint = allVotesUp - allVotesDown;

            _commentService.Update(comment);

            return Json(new { rankPoint = comment.RankPoint });
        }
    }
}