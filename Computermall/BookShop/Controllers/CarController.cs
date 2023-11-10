using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;

namespace BookShop.Controllers
{
    
    public class CarController : Controller
    {
        ChangDBEntities6 db = new ChangDBEntities6();
        // GET: Car
        //商品详情，购买
        
        public ActionResult Details(int? id)
        {

            Product product = db.Products.Find(id);

            //统计总销售d
            List<OrdersDetail> details = db.OrdersDetails.Where(d => d.ProductID == id).ToList();
            int totalSale = 0;
            foreach (OrdersDetail item in details)
            {
                totalSale += item.Quantity;
            }
            ViewBag.TotalSale = totalSale;

            return View(product);
        }
        public ActionResult Cha(int? id)
        {

            var gd = db.News.FirstOrDefault(y => y.NewsID == id);
            return View(gd);
        }


        //填地址下单页面
        public ActionResult Order()
        {
            if (Session["cart"] == null)
            {
                return Content("<script>alert('1');location.href='/shou/index'</script>");
            }
            List<ShopCart> carts = (List<ShopCart>)Session["cart"];
            if (carts.Count(x => x.IsCheck) == 0)
            {
                return Content("<script>alert('2');location.href='/shou/index'</script>");
            }

            int uid = MyAuthentication.GetUserID();

            //身份票据，存在一个用户
            User user = db.Users.Include("Delivery").First(t => t.UserID == uid);
            if (user.Delivery == null)
            {
                user.Delivery = new Delivery();
            }

     
            ViewBag.Cart = carts.Where(x => x.IsCheck).ToList();
            ViewBag.SelTotal = carts.Count(x => x.IsCheck);
            ViewBag.Total = SumTotal();
            return View(user.Delivery);
        }

        //计算总金额
        public decimal SumTotal()
        {
            List<ShopCart> carts = Session["cart"] as List<ShopCart>;
            if (carts == null)
                carts = new List<ShopCart>();

            return carts.Where(x => x.IsCheck).Sum(x => x.Number * x.Product.Price);
        }

        [HttpPost]
        public ActionResult Order(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                if(delivery.DeliveryID > 0)
                {
                    db.Entry<Delivery>(delivery).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Deliveries.Add(delivery);
                }
                int uid = MyAuthentication.GetUserID();
                User user = db.Users.Include("Delivery").First(t => t.UserID == uid);
                user.Delivery = delivery;

                db.SaveChanges();


                //订单信息
                Order order = new Order()
                {
                    Orderdate = DateTime.Now,
                    UserID = uid,
                    DeliveryID = delivery.DeliveryID,
                    States = 1,
                    Total = 0
                };

                List<ShopCart> carts = (List<ShopCart>)Session["cart"];
                var selectedCarts = carts.Where(x => x.IsCheck).ToList();
                foreach (var cart in selectedCarts)
                {
                    //判断库存
                    var product = db.Products
                        .FirstOrDefault(x => x.ProductID == cart.Product.ProductID);
                    if (product.Stock < cart.Number)
                    {
                        return Content("<script>alert('" + product.Title + "Insufficient inventory!');</script>");
                    }

                    OrdersDetail detail = new OrdersDetail() 
                    {
                        OrdersID = order.OrdersID,
                        ProductID = cart.Product.ProductID,
                        Quantity = cart.Number,
                        States = order.States
                    };
                    //db.OrdersDetails.Add(detail);
                    order.OrdersDetails.Add(detail);
                    order.Total = order.Total + cart.Number * cart.Product.Price;
                    product.Stock = product.Stock - cart.Number;
                }
                db.Orders.Add(order);

                if (db.SaveChanges() > 0)
                {
                    selectedCarts.ForEach(x => carts.Remove(x));
                    Session["cart"] = selectedCarts;

                    return Content("<script>alert('Successfully submitted the order!');location.href='/shou/index'</script>");
                }

            }

            return Content("<script>alert('Failed to place the order!');</script>");
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}