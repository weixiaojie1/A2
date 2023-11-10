using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using BookShop.Models;
namespace BookShop.Controllers
{
    [UsersAuthentication]
    public class ShopController : Controller
    {
        ChangDBEntities6 db = new ChangDBEntities6();

        //我的订单页面
        public ActionResult Orders(int? id, int? states)
        {
            List<Order> list;
            int uid = MyAuthentication.GetUserID();
            //判断状态是否为空
            if (states == null)
            {
                //如果为空， 降序显示该用户所有的订单
                list = db.Orders
                    .Where(t => t.UserID == uid)
                    .Include(t => t.Delivery)
                    .OrderByDescending(t => t.OrdersID).ToList();
            }
            else
            {
                //如果不为空，降序显示相对应的订单状态的订单
                list = db.Orders
                    .Where(t => t.States == states && t.UserID == uid)
                    .Include(t => t.Delivery)
                    .OrderByDescending(t => t.OrdersID).ToList();

            }
            //分页编号
            int pageIndex = id ?? 1;
            //分页大小
            PagedList<Order> mPage = list.AsQueryable().ToPagedList(pageIndex, 5);
            return View(mPage);
        }

        public ActionResult Confirm(int id)
        {
            Order order = db.Orders.Find(id);
            order.DeliveryDate = DateTime.Now;
            order.States = 3;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        public ActionResult Index(int? id)
        {
            //session，转换为list，使用model传给视图
            List<ShopCart> carts = Session["cart"] as List<ShopCart>;
            if (carts == null)
                carts = new List<ShopCart>();

            ViewBag.SelTotal = carts.Count(x => x.IsCheck);
            ViewBag.Total = SumTotal();
            return View(carts);
        }

        //计算总金额
        public decimal SumTotal()
        {
            List<ShopCart> carts = Session["cart"] as List<ShopCart>;
            if (carts == null)
                carts = new List<ShopCart>();

            return carts.Where(x => x.IsCheck).Sum(x => x.Number * x.Product.Price);
        }

        public ActionResult addCart(int id, int num = 1)
        {
            //按id获取图书信息
            Product product = db.Products.Find(id);
            if (product.Stock < num)
            {
                return Content("<script>alert('Insufficient inventory');window.location.href = '/Car/Details/" + id + "';</script>");
            }

            //从session中取出购物车信息
            if (Session["cart"] != null)
            {
                //session存在
                List<ShopCart> carts = (List<ShopCart>)Session["cart"];
                //判断图书在原来的sesssave的集合中是否存在
                ShopCart cart = carts.SingleOrDefault(u => u.Product.ProductID == id);
                if (cart != null)
                {
                    //图书在购物车中存在，更新数量和小计价格
                    cart.Number = cart.Number + num;
                    cart.TotalPrice = Convert.ToDouble(cart.Number * cart.Product.Price);
                }
                else
                {
                    //图书在购物车中不存在，new一个新的cart，添加属性值并添加到carts集合
                    cart = new ShopCart();
                    cart.Number = num;
                    cart.TotalPrice = Convert.ToDouble(product.Price);
                    cart.Product = product;
                    cart.IsCheck = true;
                    carts.Add(cart);
                }
                //save到sesson
                Session["cart"] = carts;
            }
            else
            {
                //seess不存在，new一个新的cart，添加属性值并添加到carts集合
                List<ShopCart> carts = new List<ShopCart>();
                ShopCart cart = new ShopCart();
                cart.Number = num;
                cart.TotalPrice = Convert.ToDouble(product.Price);
                cart.Product = product;
                carts.Add(cart);
                Session["cart"] = carts;
            }
            return RedirectToAction("index");
        }

        public ActionResult Remove(int id)
        {
            if (Session["cart"] != null)
            {
                List<ShopCart> carts = (List<ShopCart>)Session["cart"];
                //判断图书在原来的sesssave的集合中是否存在
                ShopCart cart = carts.SingleOrDefault(u => u.Product.ProductID == id);
                if (cart != null)
                {
                    //图书在购物车中存在，从购物车中删除
                    carts.Remove(cart);
                }
                //save到sesson
                Session["cart"] = carts;
            }

            return RedirectToAction("Index");

        }

        //购物车商品加1
        public ActionResult Add(int id)
        {
            if (Session["cart"] != null)
            {
                List<ShopCart> carts = (List<ShopCart>)Session["cart"];
                //判断商品在原来的sesssave的集合中是否存在
                ShopCart cart = carts.SingleOrDefault(u => u.Product.ProductID == id);

                Product product = db.Products.Find(id);
                if (product.Stock < cart.Number + 1)
                {
                    return Content("<script>alert('Insufficient inventory');window.location.href = '/Shop/Index;</script>");
                }

                if (cart != null)
                {
                    //图书在购物车中存在，数量加1
                    cart.Number++;
                    cart.TotalPrice = Convert.ToDouble(cart.Number * cart.Product.Price);
                }
                //save到sesson
                Session["cart"] = carts;
            }

            return Redirect("/Shop/Index");
        }

        //购物车商品减一
        public ActionResult Reduce(int id)
        {
            if (Session["cart"] != null)
            {
                List<ShopCart> carts = (List<ShopCart>)Session["cart"];
                //判断商品在原来的sesssave的集合中是否存在
                ShopCart cart = carts.SingleOrDefault(u => u.Product.ProductID == id);
                if (cart != null && cart.Number > 1)
                {
                    //商品在购物车中存在，数量加1
                    cart.Number--;
                    cart.TotalPrice = Convert.ToDouble(cart.Number * cart.Product.Price);
                }
                //save到sesson
                Session["cart"] = carts;
            }

            return Redirect("/Shop/Index");
        }

        //购物车商品选中与取消
        public ActionResult Check(int id, bool isCheck)
        {
            if (Session["cart"] != null)
            {
                List<ShopCart> carts = (List<ShopCart>)Session["cart"];
                ShopCart cart = carts.SingleOrDefault(u => u.Product.ProductID == id);
                if (cart != null)
                {
                    cart.IsCheck = isCheck;
                }
                //save到sesson
                Session["cart"] = carts;
            }

            return Redirect("/Shop/Index");
        }
    }
}