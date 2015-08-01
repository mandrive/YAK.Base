using System.Web.Mvc;
using Yak.Web.Models;

namespace Yak.Web.BaseUtils
{
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}