using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Microsoft.AspNet.Identity.Owin;

using PagedList;

using BabyStore.Models;
using System.Threading.Tasks;

namespace BabyStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _UserManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _UserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            set
            {
                 _UserManager=value;
            }
        }

        // GET: Orders
        public ActionResult Index(string searchorder,string startDate,string endDate,string orderSortOrder,string filter_value , int? page)
        {
            var order = db.Orders.OrderBy(o=> o.DateOrder).Include(o => o.OrderDetielss);

            if (!User.IsInRole("Admin"))
            {
                 // return View(db.Orders.ToList());

                order = order.Where(o=>o.UserID==User.Identity.Name);
            }
                 /* else
                  {
                   return View(db.Orders.Where(o => o.UserID == User.Identity.Name));
                  }*/


            if (searchorder != null)
            {
                page = 1;
            }

            else
            {
                searchorder = filter_value;
            }

            ViewBag.FilterValue = searchorder;

            if (!String.IsNullOrEmpty(searchorder))
            {
                order = order.Where(o => o.OrderID.ToString().Equals(searchorder)
                  || o.UserID.Contains(searchorder) ||o.DeliveryName.Contains(searchorder)
                  || o.DeliveryAddress.AddressLine1.Contains(searchorder) 
                  || o.DeliveryAddress.AddressLine2.Contains(searchorder)
                  || o.DeliveryAddress.Town.Contains(searchorder)
                  || o.DeliveryAddress.County.Contains(searchorder) 
                  || o.DeliveryAddress.Postcode.Contains(searchorder)
                  || o.TotalPrice.ToString().Equals(searchorder)
                  || o.OrderDetielss.Any(od => od.ProductName.Contains(searchorder)));

            }

            DateTime PrasedStartDate;
            if(DateTime.TryParse(startDate , out PrasedStartDate))
            {
                order = order.Where(o => o.DateOrder >= PrasedStartDate);
            }

            DateTime PrasedEndDate;
            if(DateTime.TryParse(endDate , out PrasedEndDate))
            {
                order = order.Where(o => o.DateOrder <= PrasedEndDate);
            }
            ViewBag.CurrentOrderSearch = searchorder;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.DateSort = String.IsNullOrEmpty(orderSortOrder) ? "date" : "";
            ViewBag.UserSort = orderSortOrder == "user" ? "user_desc" : "user";
            ViewBag.PriceSort = orderSortOrder == "price" ? "price_desc" : "price";


            switch(orderSortOrder)
            {
                case "user":
                    {
                        order = order.OrderBy(o => o.UserID);
                        break;
                    }

                case "user_desc":
                    {
                        order = order.OrderByDescending(o => o.UserID);
                        break;
                    }

                case "price" :
                    {
                        order = order.OrderBy(o => o.TotalPrice);
                        break;
                    }

                case "price_desc" :
                    {
                        order = order.OrderByDescending(o => o.TotalPrice );
                        break;
                    }

                case "date":
                    {
                        order = order.OrderBy(o => o.DateOrder);
                        break;
                    }

                default :
                    {
                        order = order.OrderByDescending(o => o.DateOrder);
                        break;
                    }

            }


            int pageSize = 4;
            int currentPage = (page ?? 1);

            return View(order.ToPagedList(currentPage ,pageSize));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ///Order order = db.Orders.Find(id);
            
            Order order = db.Orders.Include(o => o.OrderDetielss).Where(o => o.OrderID == id).SingleOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.UserID == User.Identity.Name || User.IsInRole("Admin"))
            {
                return View(order);
            }

            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Orders/Review
        public async Task<ActionResult> Review ()
        {

            Basket basket = Basket.GetBasket();
            Order order = new Order();

            order.UserID = User.Identity.Name;
            ApplicationUser user = await UserManager.FindByNameAsync(order.UserID);
            order.DeliveryName = user.FirstName + "  " + user.LastName;
            order.DeliveryAddress = user.Address;
            order.OrderDetielss =new List<OrderDetiels>();
            foreach (var baskline in basket.GetBasketlines())
            {
                OrderDetiels line = new OrderDetiels
                {
                    Product = baskline.Product,
                    ProductID = baskline.ProductID,
                    ProductName = baskline.Product.Name,
                    Quantity = baskline.Quantity,
                    UnitPrice = baskline.Product.Price
                };

             
                order.OrderDetielss.Add(line);
            }

            order.TotalPrice = basket.GetTotalCost();
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,UserID,DeliveryName,DeliveryAddress,TotalPrice,DateOrder")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DateOrder = DateTime.Now;
                db.Orders.Add(order);

                db.SaveChanges();

                // add orderdetails to database after creat order

                Basket basket = Basket.GetBasket();
                order.TotalPrice = basket.CreateOrderDetails(order.OrderID);
                db.SaveChanges();

                return RedirectToAction("Details", new { id =order.OrderID});
            }

            return RedirectToAction("Review");
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,UserID,DeliveryName,DeliveryAddress,TotalPrice,DateOrder")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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
