
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyStore.Models
{
    public class Basket
    {
        private string BasketID { get; set; }
        private const string BasketSessionKey = "BasketID";
        private ApplicationDbContext db = new ApplicationDbContext();

        private string GetBasketID()
        {

            if (HttpContext.Current.Session[BasketSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[BasketSessionKey] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid tempBasketID = Guid.NewGuid();
                    HttpContext.Current.Session[BasketSessionKey] = tempBasketID.ToString();
                }
            }

            return HttpContext.Current.Session[BasketSessionKey].ToString();
        }

        public static Basket GetBasket()
        {
            Basket basket = new Basket();
            basket.BasketID = basket.GetBasketID();
            return basket;
        }

        public void AddToBasket(int productID, int quality)
        {
            var basketline = db.BasketLines.FirstOrDefault(b => b.BasketID == BasketID && b.ProductID==productID);
            //var products = db.Products.FirstOrDefault(b => b.ID== productID);
            if (basketline == null)
            {
                basketline = new BasketLine
                {
                    ProductID = productID,
                    BasketID = BasketID,
                    //Quantity = (int)products.Quantity-quality,
                    Quantity=quality,
                    DateCreated = DateTime.Now

                };
                db.BasketLines.Add(basketline);

            }
            else
            {
                basketline.Quantity += quality;
            }

            db.SaveChanges();
        }


        public void RemoveLine(int productID)
        {
            var basketline = db.BasketLines.FirstOrDefault(b => b.BasketID == BasketID && b.ProductID == productID);
            if (basketline != null)
            {
                db.BasketLines.Remove(basketline);
            }

            db.SaveChanges();
        }


        public void UpdateBasket(List<BasketLine> lines)
        {
            foreach (var line in lines)
            {
            
               var basketline = db.BasketLines.FirstOrDefault(b => b.BasketID == BasketID && b.ProductID == line.ProductID);
               if (basketline != null)
               {
                   if (line.Quantity == 0)
                   {
                       RemoveLine(line.ProductID);
                   }
                   else
                   {
                       basketline.Quantity = line.Quantity;
                   }
               }
            }
            db.SaveChanges();

        }

        // function to empty basketline
        public void EmptyBasket()
        {
            var basketlines = db.BasketLines.Where(p => p.BasketID == BasketID);
            foreach (var basketline in basketlines)
            {
                db.BasketLines.Remove(basketline);
            }
            db.SaveChanges();
        }

        //return all the BasketLines for the current BasketID
        public List<BasketLine> GetBasketlines()
        {
            return db.BasketLines.Where(p => p.BasketID == BasketID).ToList();
        }

        //methods to the Basketclass to calculate the total cost of the basket 
        public decimal GetTotalCost() 
        {
            decimal basketTotal = decimal.Zero;
            if (GetBasketlines().Count() > 0)
            {
                basketTotal = db.BasketLines.Where(p => p.BasketID == BasketID).Sum(p => p.Product.Price * p.Quantity);
            }
            return basketTotal;
        }

        //and to get the total number of products in the basket.
        public int GetNumberOfitem()
        {
            int NumberOfitem =0;
            if (GetBasketlines().Count() > 0)
            {
                NumberOfitem = db.BasketLines.Where(p => p.BasketID == BasketID).Sum(p => p.Quantity);
            }
            return NumberOfitem;
        }

        // 

        public decimal CreateOrderDetails(int orderId)
        {
            decimal orderTotal = 0;

            var basketlines = GetBasketlines();

            foreach(var item in basketlines)
            {
                OrderDetiels orderdetiels = new OrderDetiels
                {
                    Product =item.Product,
                    ProductID=item.ProductID,
                    Quantity=item.Quantity,
                    ProductName=item.Product.Name,
                    UnitPrice=item.Product.Price,
                    OrderID=orderId
                };

                orderTotal +=(item.Quantity * item.Product.Price);
                db.OrderDetielss.Add(orderdetiels);
            }


            db.SaveChanges();
            EmptyBasket();
            return orderTotal;

        }



        public void MigrateBasket(string userName)
                     
        {
            //find the current basket and store it in memory using ToList()
            var basket = db.BasketLines.Where(b => b.BasketID == BasketID).ToList();

            //find if the user already has a basket or not and store it in memory using //ToList()
            var usersBasket = db.BasketLines.Where(b => b.BasketID == userName).ToList();

            //if the user has a basket then add the current items to it
            if (usersBasket != null)
            {

                //set the basketID to the username
                string prevID = BasketID;
                BasketID = userName;
                //add the lines in anonymous basket to the user's basket
                
                foreach (var line in basket)
                {
                    AddToBasket(line.ProductID, line.Quantity);
                }
                //delete the lines in the anonymous basket from the database
                BasketID = prevID;
                EmptyBasket();
            }
            else
            {
                //if the user does not have a basket then just migrate this one
                foreach (var basketLine in basket)
                {
                    basketLine.BasketID = userName;
                }
                db.SaveChanges();
            }
            HttpContext.Current.Session[BasketSessionKey] = userName;
        }
    } 
}