using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BookShop.Models;

namespace BookShop
{
    public class MyAuthentication
    {
        ///<summary>
        ///获取登录凭据
        ///</summary>
        public static HttpCookie AuthCookie()
        {
            return HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
        }
        
        public static void SetCookie(string UserName, string UserID, string Rights)
        {
            String UserData = UserID + "#" + Rights;
            //数据放入ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now,
                DateTime.Now.AddMinutes(60), false, UserData);
            string enyTicket = FormsAuthentication.Encrypt(ticket);//加密
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, enyTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        ///<summary>
        ///判断用户是否登录
        ///</summary>
        ///<returns>Ture,Fales</returns>
        public static bool IsLogin()
        {
            return AuthCookie() != null;
        }
        ///<summary>
        ///注销登录
        ///</summary>
        public static void LogOut()
        {
            FormsAuthentication.SignOut();
        }
        ///<summary>
        ///获取凭据中的用户名
        ///</summary>
        ///<returns>用户名</returns>
        public static string GetUserName()
        {
            if (IsLogin())
            {
                //解密
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(AuthCookie().Value);
                return authTicket.Name;
            }
            else return "";
        }
        ///<summary>
        ///获取凭据中的用户ID
        ///</summary>
        ///<returns>用户</returns>
        public static int GetUserID()
        {
            if (IsLogin())
            {
                //解密
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(AuthCookie().Value);
                string[] UserData = authTicket.UserData.Split('#');//拆分用户数据
                if (UserData.Length > 0) return int.Parse(UserData[0]);
                else return 0;
            }
            else return 0;
        }
        ///<summary>
        ///获取凭据中的用户权限
        ///</summary>
        ///<returns>用户权限</returns>
        public static string GetRights()
        {
            if (IsLogin())
            {
                //解密
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(AuthCookie().Value);
                string[] UserData = authTicket.UserData.Split('#');//拆分用户数据
                if (UserData.Length > 1) return UserData[1].ToString();
                else return "";
            }
            else return "";
        }
        ///<summary>
        ///获取登录用户ID获取会员实体类
        ///</summary>
        ///<returns>用户ID</returns>
        public static User GetUser()
        {
            int uid = GetUserID();
            ChangDBEntities6  db = new ChangDBEntities6();
            return db.Users.Find(uid);
        }
    }
}