using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public CommentController(IService<Comment> commentService, IService<Answer> answerService)
        {
            _commentService = commentService;
            _answerService = answerService;
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
    }
}