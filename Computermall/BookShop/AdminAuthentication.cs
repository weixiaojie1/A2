using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookShop
{
    public class AdminAuthentication:AuthorizeAttribute
    {
       
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!MyAuthentication.IsLogin() || MyAuthentication.GetRights() != "admin")
                HttpContext.Current.Response.Redirect("~/Admin/Login", true);
        }
    }
}