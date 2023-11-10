using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;
namespace BookShop.Controllers
{
    [AdminAuthentication]
    public class UserController : Controller
    {
        ChangDBEntities6 db = new ChangDBEntities6();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //用户信息
        public ActionResult user()
        {
            var user = db.Users.ToList();
            return View(user);
        }
        //用户详情
        public ActionResult List(int? id)
        {
            var list = db.Users.Find(id);
            return View(list);
        }
        //删除
        public ActionResult Del(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("user");
        }
        //注册新用户
        public ActionResult zhuc()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult zhuc(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("user");
                }
            }
            return View("zhuc");
        }
        //修改用户

        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", user.UserID);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "UserID,UserName,Pwd,Email,Nick")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("user");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", user.UserID);
            return View(user);
        }
    }
}