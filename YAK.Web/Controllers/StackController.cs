using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Yak.Database;
using Yak.Database.Entities;

namespace Yak.Web.Controllers
{
    public class StackController : Controller
    {
        private DatabaseContext _databaseContext;

        public StackController(DatabaseContext context)
        {
            _databaseContext = context;
        }

        public ActionResult Index()
        {
            var questions = _databaseContext.Questions.Include("Author");

            return View(questions);
        }
    }
}