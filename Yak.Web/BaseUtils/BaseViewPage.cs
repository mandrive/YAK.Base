using System.Web.Mvc;
using Yak.Web.Models;

namespace Yak.Web.BaseUtils
{
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }

        public virtual CustomPrincipal CustomUser
        {
            get { return base.User as CustomPrincipal; }
        }
    }
}