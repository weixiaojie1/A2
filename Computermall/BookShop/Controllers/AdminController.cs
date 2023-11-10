using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using BookShop.Models;

namespace BookShop.Controllers
{
   
    public class AdminController : Controller
    {
        // GET: Admin
        ChangDBEntities6 db = new ChangDBEntities6();
        //首页
        [AdminAuthentication]
        public ActionResult Index(int page = 1)
        {
            int size = 10;
            int count = 0;
            count = (int)Math.Ceiling(db.Products.Count() * 1.0 / size);
            ViewBag.PageIndex = page;
            ViewBag.PageCount = count;
            var list = db.Products.OrderBy(x => x.ProductID).Skip((page - 1) * size).Take(size).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult Cha(string pname = "")
        {
            if (pname==null)
            {
                return Content("<script>alert('The product does not exist!');location.href='/Admin/Index'</script>");
            }
            var list = db.Products.Where(p => p.Title.Contains(pname)).ToList();
            return View("Index", list);
        }
        //商品上架
        [AdminAuthentication]
        public ActionResult Add()
        {
            List<Category> data = db.Categories.Where(c => c.CateName != "default collation" && c.CateName != "Hot sales" && c.CateName != "price" && c.CateName != "discount").ToList();
            ViewBag.Data = data;
            return View(new Product());
        }
        //下拉框
        //商品接受表单数据
        [HttpPost]
        [AdminAuthentication]
        public ActionResult Add(Product product )
        {
            List<Category> data = db.Categories.Where(c => c.CateName != "default collation" && c.CateName != "Hot sales" && c.CateName != "price" && c.CateName != "discount").ToList();
            ViewBag.Data = data;
            if (ModelState.IsValid)
            {
                string savePath = Server.MapPath("~/image/");
                string Url = "";
                string Name = "";
                //判断是否有文件上传
                if (Request.Files.Count > 0)
                {
                    //判断是否存在这个文件夹
                    if (!System.IO.Directory.Exists(savePath))
                    {
                        //如果没有这个文件夹，就创建一个
                        System.IO.Directory.CreateDirectory(savePath);
                    }
                    HttpPostedFileBase f = Request.Files["Photo"];
                    Name = f.FileName;
                    Url = "/image/" + Name + "";
                    savePath = savePath + Name;
                    f.SaveAs(savePath);
                }
                product.Photo = Name;
                product.Title = product.Content;
                db.Products.Add(product);
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [AdminAuthentication]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "CateName", product.CateID);
            return View(product);
        }

        [HttpPost]
        [AdminAuthentication]
        public ActionResult Edit([Bind(Include = "ProductID,Title,CateID,MarketPrice,Price,Content,PostTime,Stock,Photo")] Product product)
        {
           
            if (ModelState.IsValid)
            {
                string savePath = Server.MapPath("~/image/");
                string Url = "";
                string Name = "";
                if (Request.Files.Count > 0)
                {
                   
                    if (!System.IO.Directory.Exists(savePath))
                    {
                  
                        System.IO.Directory.CreateDirectory(savePath);
                    }
                    HttpPostedFileBase f = Request.Files["PhotoUrl"];
                    Name = f.FileName;
                    if (Name.Length>0)
                    {
                        Url = "/image/" + Name + "";
                        savePath = savePath + Name;
                        f.SaveAs(savePath);
                        product.Photo = Name;
                    }
                    
                }
                
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "CateName", product.CateID);
            return View(product);
        }

        [HttpPost]
        [AdminAuthentication]
        public ActionResult Detail(Product product)
        {
            if (ModelState.IsValid)
            {             
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();            
                return RedirectToAction("Index");
          }
            return View(product);
        }

        //商品删除
        [AdminAuthentication]
        public ActionResult Del(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        //管理员登录
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string UserName, string UserPwd)
        {
            if (ModelState.IsValid)
            {
                var user = db.AdminUsers.SingleOrDefault(u => u.UserName.Trim() == UserName.Trim() && u.Pwd.Trim() == UserPwd.Trim());
                if (user != null)
                {
                    MyAuthentication.SetCookie(user.UserName, user.SuID.ToString(), "admin");
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Account or password error!");
                return View();
            }
            return View();
        }
       // 退出
        public ActionResult Logi()
        {
            MyAuthentication.LogOut();
            return RedirectToAction("Index");
        }

        //订单列表
        [AdminAuthentication]
        public ActionResult Ding(int? id,int? states)
        {
            List<Order>list;
            if(states == null)
            {
                list = db.Orders.Include(o => o.Delivery).Include(o => User)
                    .OrderByDescending(o => o.Orderdate).ToList();
            }
            else
            {
                list = db.Orders.Where(o => o.States == states).Include(o => o.Delivery)
                    .Include(o => o.User).OrderByDescending(o => o.Orderdate).ToList();
            }
            int pageIndex = id ?? 1;
            PagedList<Order> mPage = list.AsQueryable().ToPagedList(pageIndex, 5);
            return View(mPage);
        }

        //订单详情
        [AdminAuthentication]
        public ActionResult Details(int? id)
        {
            Order order = db.Orders.Find(id);
            return View(order);
        }

        /// <summary>
        /// 发货
        /// </summary>
        [AdminAuthentication]
        public ActionResult Send(int id)
        {
            Order o = db.Orders.Find(id);
            o.States = 2;
            db.SaveChanges();
            return Redirect("/Admin/Ding");
        }
    }
}