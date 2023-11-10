using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;
namespace BookShop.Controllers
{
    public class ZzhuCController : Controller
    {
        ChangDBEntities6 db = new ChangDBEntities6();
        // GET: ZzhuC
        public ActionResult Index()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name, string pwd)
        {
            var list = db.AdminUsers.SingleOrDefault(u => u.UserName.Trim() == name.Trim() && u.Pwd.Trim() == pwd.Trim());
            if (list != null)
            {
                return View("~/Views/Shou/index");
            }
            return View();
        }
        //注册
        public ActionResult Reg()
        {
            return View(new User());
        }
        //接收注册页面的数据
        [HttpPost]
        public ActionResult Reg(User user)
        {
            try
            {

                using (ChangDBEntities6 db = new ChangDBEntities6())
                {
                    if (ModelState.IsValid)
                    {
                        db.Users.Add(user);
                        if (db.SaveChanges() > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    return View("Reg");
                }
            }
            catch (Exception)
            {
                TempData["msg"] = "注册失败";
            }
            return View();
        }
    }
}