using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Webdiyer.WebControls.Mvc;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class ShouController : Controller
    {
        ChangDBEntities6 db = new ChangDBEntities6();

        public ActionResult Kefu()
        {
            return View();
        }
        public ActionResult Index(int? id)
        {

            List<Category> cates = db.Categories.Where(c => c.ParentID == 1).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(4).ToList();

            List<Category> cate = db.Categories.Where(c => c.ParentID == 2).ToList();
            int[] cateid = new int[cate.Count];
            for (int i = 0; i < cateid.Length; i++)
            {
                cateid[i] = cate[i].CateID;
            }
            var product2 = from p in db.Products where (cateid).Contains(p.CateID) select p;
            ViewBag.Product2 = product2.Take(4).ToList();
 
            List<Category> nan = db.Categories.Where(c => c.ParentID == 3).ToList();
            int[] nanid = new int[nan.Count];
            for(int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product3 = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product3 = product3.Take(4).ToList();
            Product product = db.Products.Find(id);
            return View(product);
        }
        public ActionResult Category(int CateID=1)
        {

            List<Category> nan = db.Categories.Where(c => c.CateID == CateID).ToList();
            int[] nanid = new int[nan.Count];
            for (int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product = product.ToList();
            ViewBag.CateID = CateID;
            return View();
        }

        public ActionResult Category2()
        {
   
            List<Category> nan = db.Categories.Where(c => c.ParentID == 2).ToList();
            int[] nanid = new int[nan.Count];
            for (int i = 0; i < nanid.Length; i++)
            {
                nanid[i] = nan[i].CateID;
            }
            var product3 = from p in db.Products where (nanid).Contains(p.CateID) select p;
            ViewBag.Product3 = product3.Take(5).ToList();
            return View();
        }
      
        public ActionResult Category3()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 3).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }
      
        public ActionResult Category4()
        {
           
            List<Category> cates = db.Categories.Where(c => c.ParentID == 4).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }
       
        public ActionResult Category5()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 5).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }
     
        public ActionResult Category6()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 6).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }

        public ActionResult Category7()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 7).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }

        public ActionResult Category8()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 8).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }

        public ActionResult Category9()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 9).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }
        public ActionResult Category13()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 13).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }
        public ActionResult Category14()
        {
            List<Category> cates = db.Categories.Where(c => c.ParentID == 14).ToList();
            int[] cateids = new int[cates.Count];
            for (int i = 0; i < cateids.Length; i++)
            {
                cateids[i] = cates[i].CateID;
            }
            var product1 = from p in db.Products where (cateids).Contains(p.CateID) select p;
            ViewBag.Product1 = product1.Take(5).ToList();
            return View();
        }
        public ActionResult Youhui()
        {
            //获取经典著作
            var news = db.News.ToList();
            return View(news);
        }
   

        public ActionResult Search(string pname)
        {
            ViewBag.search = pname;
            //模糊查询
            var products = from product in db.Products where product.Title.Contains(pname) select product;
            return View(products.ToList());
        }
        //登录
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string pwd)
        {
            var user = db.Users.SingleOrDefault(u => u.UserName.Trim() == name.Trim() && u.Pwd.Trim() == pwd.Trim());
            if (user != null)
            {
                MyAuthentication.SetCookie(user.Nick, user.UserID.ToString(), "user");
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Account or password error!");
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
                    if (db.Users.Any(x =>x.UserName == user.UserName))
                    {
                        TempData["msg"] = "The username has been registered!";
                        return View();
                    }

                    if (ModelState.IsValid)
                    {
                        db.Users.Add(user);
                        if (db.SaveChanges() > 0)
                        {
                            return Content("<script>alert('Registration successful!');location.href='/shou/login'</script>");
                        }
                    }
                    return View("Reg");
                }
            }
            catch (Exception)
            {
                TempData["msg"] = "login has failed";
            }
            return View();
        }
        //查看个人信息
        

        public ActionResult Ren(string Nick)
        {
            if (!MyAuthentication.IsLogin() || MyAuthentication.GetRights() != "user") {
                //HttpContext.Response.Redirect(", true);
                return Redirect("~/Shou/Login");
            }
            else
            {
                User user = db.Users.FirstOrDefault(t => t.Nick == Nick) as User;
                return View(user);
            }
            
                    
            
        }

    }
}
