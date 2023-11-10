using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        ChangDBEntities6 db = new ChangDBEntities6();
        public ActionResult Index()
        {
            return View();
        }
        //分类查询
        //默认排序
        public ActionResult News()
        {
            List<Category> nan = db.Categories.Where(c => c.ParentID == 10).ToList();
            int[] nanid = new int[nan.Count];
            for (int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product3 = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product3 = product3.Take(5).ToList();
            return View();
        }
        //热销
        public ActionResult Xiao()
        {
           
            List<Category> nan = db.Categories.Where(c => c.ParentID == 11).ToList();
            int[] nanid = new int[nan.Count];
            for (int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product3 = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product3 = product3.Take(5).ToList();
            return View();
        }
        //价格
        public ActionResult Price()
        {
            
            List<Category> nan = db.Categories.Where(c => c.ParentID == 12).ToList();
            int[] nanid = new int[nan.Count];
            for (int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product3 = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product3 = product3.Take(5).ToList();
            return View();
        }
        //折扣
        public ActionResult Zhe()
        {
            
            List<Category> nan = db.Categories.Where(c => c.ParentID == 13).ToList();
            int[] nanid = new int[nan.Count];
            for (int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product3 = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product3 = product3.Take(5).ToList();
            return View();
        }

        /// <summary>
        /// 退出
        /// </summary>
        public ActionResult Logout()
        {
            MyAuthentication.LogOut();
            return Redirect("/Shou/Index");
        }
        public ActionResult Menu()
        {
            List<Category> data = db.Categories.Where(c => c.CateName != "default collation" && c.CateName != "Hot sales" && c.CateName != "price" && c.CateName != "discount").ToList();
            ViewBag.Data = data;
            return View(data);
        }
    }
}