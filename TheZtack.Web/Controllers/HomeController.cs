using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheZtack.Database;
using TheZtack.Database.Entities;
using TheZtack.SearchEngine;
using TheZtack.Services.Interfaces;

namespace TheZtack.Controllers
{
    public class HomeController : Controller
    {
        private IService<Question> _questionService;

        public HomeController(IService<Question> questionService)
        {
            _questionService = questionService;
        }

        public ActionResult Index()
        {
            var questions = _questionService.GetAll();

            return View(questions);
        }
    }
}