using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Yak.DTO;
using Yak.Services.Interfaces;
using Yak.Web.Models;

namespace Yak.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserValidationService _userService;

        public LoginController(IUserValidationService userService)
        {
            _userService = userService;
        }

        // GET: Login
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Stack");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel loginModel)
        {
            var isUserValid = _userService.Validate(loginModel.Username, loginModel.Password);

            if (isUserValid)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Username, true);
                return RedirectToAction("Index", "Stack");
            }

            ModelState.AddModelError(string.Empty, "Username or password not valid");
            return View(loginModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}
