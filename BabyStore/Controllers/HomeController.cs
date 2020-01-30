using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using BabyStore.Models;
using BabyStore.ViewModels;
using System.IO;
using PagedList;

using System.Threading.Tasks;
namespace BabyStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string category, string search)
        {


            /*var products = from pro in db.Products.Include(p => p.Category).OrderByDescending(p => p.Category)
                           where pro.ID != null
                           select pro; */

            var products = db.Products.Include(p => p.Category);
     
      
           // ViewBag.Cat = new SelectList(db.Categories.OrderBy(c => c.Name).Select(c => c.Name).Distinct(), "CategoryID", "CategoryName").ToList();
          // ViewBag.Category = new SelectList(db.Categories.OrderBy(c => c.Name).ToList());
           
            //View(db.Categories.OrderBy(c => c.Name).ToList());

            return View(products.ToList());


        }


        // category  
       // public ActionResult 


         public PartialViewResult BestSeller()
         {
              
             try
             {
                 var topsellers = (from topProducts in db.OrderDetielss
                                   where (topProducts.ProductID != null)
                                   group topProducts by topProducts.Product into topgroup
                                   select new BasetSellesrViewModel
                                   {
                                       Product = topgroup.Key,
                                       SalesCount = topgroup.Sum(o => o.Quantity),


                                   }).OrderByDescending(tg => tg.SalesCount).Take(8);
                 return PartialView( topsellers.ToList());
             }
             catch { }

             ViewBag.s = "hello";

             return PartialView();
         }

    
        public ActionResult aboutus()
        {
            ViewBag.us = "تأسست دي إكس إن من قبل الداتو الدكتور ليم سيو جين، وهو خرّيج من المعهد الهندي للتكنلوجيا المرموق. بدأ الداتو الدكتور ليم العمل في البحث عن فوائد الفطر لصحة الإنسان. فقد قاده إهتمامه العميق وجهوده المتواصلة التي لا نهاية لها ليستفيد من أقصى الإمكانيات لجانوديرما أو لينجزي، المعروف باسم ملك الأعشاب، لصحة الإنسان والثروة، مع تأسيس دي إكس إن في عام ١٩٩٣.";
            return View();
        }

    }
}