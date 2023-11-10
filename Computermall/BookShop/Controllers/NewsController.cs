using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;

namespace BookShop.Controllers
{
    public class NewsController : Controller
    {
        private ChangDBEntities6 db = new ChangDBEntities6();

        // GET: News
        public ActionResult Index(int page=1)
        {
            int size = 3;
            int count = 0;
            count = (int)Math.Ceiling(db.News.Count() * 1.0 / size);
            ViewBag.PageIndex = page;
            ViewBag.PageCount = count;
            var list = db.News.OrderBy(x => x.NewsID).Skip((page - 1) * size).Take(size).ToList();
            return View(list);
        }

        // GET: News/Details/5
        [HttpPost]
        [AdminAuthentication]
        public ActionResult Details(News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
        
        //GET: News/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(News news, string Title, string NTypes, string Content,string PhotoUrl,int States)
        {
            if (ModelState.IsValid)
            {
                //if (Request.Files.Count > 0)
                //{
                //    HttpPostedFileBase f = Request.Files["PhotoUrl"];
                //   f.SaveAs("image" + f.FileName);
                    
                //}
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
                    HttpPostedFileBase f = Request.Files["PhotoUrl"];
                    if (f.FileName.Length>0)
                    {
                        Name = f.FileName;
                        Url = "/image/" + Name + "";
                        savePath = savePath + Name;
                        f.SaveAs(savePath);
                    }       
                }
                news.PhotoUrl = Name;
                news.Title = Title;
                news.Content = Content;
                news.NTypes = NTypes;
                news.PushTime = DateTime.Now;
                news.States = States;
                db.News.Add(news);
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        //// POST: News/Create
        //// 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        //// 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "NewsID,Title,NTypes,Content,PhotoUrl,PushTime,States")] News news)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.News.Add(news);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(news);
        //}


        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsID,Title,NTypes,Content,PhotoUrl,PushTime,States")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
        public ActionResult Delete(int? id)
        {

            var del = db.News.FirstOrDefault(x => x.NewsID == id);
            if (del == null)
            {
                return Content("<script>alert('dont have');window.location.href='/News/Index'</script>");
            }
            else
            {
                db.News.Remove(del);
                if (db.SaveChanges() > 0)
                {
                    return Content("<script>alert('Successfully deleted!');window.location.href='/News/Index'</script>");
                }
                else
                {
                    return Content("<script>alert('Delete failed!');window.location.href='/News/Index'</script>");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
