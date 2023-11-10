using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookShop
{
    public class UsersAuthentication:AuthorizeAttribute 
    {
    
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!MyAuthentication.IsLogin() || MyAuthentication.GetRights() != "user")
                HttpContext.Current.Response.Redirect("~/Shou/Login", true);
        }
    }
}