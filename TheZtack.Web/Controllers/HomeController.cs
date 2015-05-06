using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheZtack.Database;
using TheZtack.SearchEngine;

namespace TheZtack.Controllers
{
    public class HomeController : Controller
    {
        private SearchEngineCore _searchEngineCore;
        private DatabaseContext _databaseContext;

        public HomeController(DatabaseContext databaseContext, SearchEngineCore searchEngineCore)
        {
            _searchEngineCore = searchEngineCore;
            _databaseContext = databaseContext;
        }

        public ActionResult Index()
        {
            _searchEngineCore.AddSomethingToIndex();

            var allQuestions = _searchEngineCore.GetAllQuestions();

            return View(allQuestions);
        }
    }
}