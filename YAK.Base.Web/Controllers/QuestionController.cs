using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult FilterQuestions(string query)
        {
            var filteredQuestions = _databaseContext.Questions.Where(q => q.Title.Contains(query)).ToList().ToArray();

            return Json(JsonConvert.SerializeObject(filteredQuestions.Select(n => new
            {
                value = n.Title
            }), Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }), JsonRequestBehavior.AllowGet);
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
