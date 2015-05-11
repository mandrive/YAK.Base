using System;
using System.Web.Mvc;
using YAK.Base.Data.Interfaces;
using YAK.Base.Database;
using YAK.Base.Database.Entities;

namespace YAK.Base.Web.Controllers
{
    public class QuestionController : Controller
    {
        private DatabaseContext _databaseContext;

        public QuestionController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ActionResult View(int id)
        {
            return View(_databaseContext.Questions.Find(id));
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Question question)
        {
            question.Author = _databaseContext.Users.Find(1);
            question.CreateDate = DateTime.Now;
            question.LastModificationDate = DateTime.Now;
            _databaseContext.Questions.Add(question);
            _databaseContext.SaveChanges();

            return Redirect(string.Format(@"View\{0}", question.Id));
        }
    }
}
