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

namespace BabyStore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        [AllowAnonymous]
        public ActionResult Index(string category, string search, string sortBy, int? page)
        {
            ProductIndexViewModel viewModel = new ProductIndexViewModel();
            try
            {
                //instantiate a new view model 
                

                //Select Products
                var products = db.Products.Include(p => p.Category).Include(c=>c.SubCategory);
                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Where(p => p.Category.Name == category);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    //perform the search and save the search string to the ModelView
                    products = products.Where(p => p.Name.Contains(search) ||
                        p.Description.Contains(search) ||
                        p.Category.Name.Contains(search));
                    //filter by category
                    //ViewBag.Search = search;
                    viewModel.Search = search;
                }

                //group search results into categories and count how many items in each category 
                viewModel.CatsWithCount = from matchingProducts in products
                                          where
                                          matchingProducts.CategoryID != null
                                          group matchingProducts by
                                          matchingProducts.Category.Name into
                                          catGroup
                                          select new CategoryWithCount()
                                          {
                                              CategoryName = catGroup.Key,
                                              ProductCount = catGroup.Count()
                                          };
                if (!String.IsNullOrEmpty(category))
                {

                    products = products.Where(p => p.Category.Name == category);
                    viewModel.Category = category;
                }

                //sort  by price
                switch (sortBy)
                {
                    case "price_lawst":
                        products = products.OrderBy(p => p.Price);
                        break;

                    case "price_highlest":
                        products = products.OrderByDescending(p => p.Price);
                        break;

                    default:
                        products = products.OrderBy(p => p.Name);
                        break;
                }


                int currentPage = (page ?? 1);
                viewModel.Products = products.ToPagedList(currentPage, Constants.PageItems);
                viewModel.sortBy = sortBy;

                viewModel.sorts = new Dictionary<string, string>
            {
            {"price low to hight","price_lawst"},
            {"price hight to low ","price_highlest"}
            };

            }
            catch { }
            return View(viewModel);


            /*var categories = products.OrderBy(p => p.Category.Name).Select(p => p.Category.Name).Distinct();
            if (!String.IsNullOrEmpty(category))
               {
               products = products.Where(p => p.Category.Name == category);
               }

            ViewBag.Category =new SelectList(categories);
            return View(products.ToList());*/
          }
        

           

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
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
            return View(product);
        }

      

        // GET: Products/Create
        public ActionResult Create()
        {
         
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            //List<SelectListItem> SubCategory = new List<SelectListItem>() {
               // new SelectListItem() { Value="0", Text="-- Select Sub Category--" },
          //};
            //ViewBag.SubCategoryID = SubCategory;
           
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName");

         
            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "InventoryName");
            return View();
        }

     

 /*[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LoadSubCategories(string catId)

        {
         db.Configuration.ProxyCreationEnabled = false;
         var SubCatgoryID = GetAllSubCategories(Convert.ToInt32(catId));

         return Json(SubCatgoryID, JsonRequestBehavior.AllowGet);
    }


        
  [NonAction]
    public IEnumerable<SelectListItem> GetAllSubCategories(int selectedCatId)
        {
      //generate empty list
      var selectList = new List<SelectListItem>();

      var subcategories = from sbcat in db.SubCategories

                          where sbcat.CategoryID == selectedCatId
                          select sbcat;
      foreach (var subcategory in subcategories)
      
      {
           //add elements in dropdown
           selectList.Add(new SelectListItem
           {
                Value = subcategory.SubCategoryID.ToString(),
                Text = subcategory.SubCategoryName.ToString()
            });
          
       }
      return selectList;

  }*/

   
        /*public JsonResult GetSubCategoryById(int? CategoryID)
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> SubCategory = db.SubCategories.Where(c => c.Category.ID == CategoryID).ToList();

           // ViewBag.SubCategoryID = new SelectList(db.SubCategories.Where(c=>c.CategoryID==CategoryID), "SubCategoryID", "SubCategoryName");
            return Json(SubCategory, JsonRequestBehavior.AllowGet);

        }*/
        

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase upload)
        {
           
                if (ModelState.IsValid)
                {
                    if (upload != null)
                    {
                        string path = Path.Combine(Server.MapPath("/Images"),upload.FileName);
                        upload.SaveAs(path);
                        product.productImg = upload.FileName;

                    }
                               
                    product.UserID =User.Identity.Name;
                    product.UserInsertDate = DateTime.Now;
                   
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);

                ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName",product.SubCategoryID);
                ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "InventoryName", product.InventoryID);

           
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName",product.SubCategoryID);
            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "InventoryName",product.InventoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product, HttpPostedFileBase upload)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    if (upload != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), upload.FileName);
                        upload.SaveAs(path);
                        product.productImg = upload.FileName;
                    }

                   
                    product.UserID = User.Identity.Name;
                    product.UserUpdateDate = DateTime.Now;
                    //product.UserUpdateDate = u.UserUpdateDate;

                   // product.UserInsertDate = product.UserInsertDate;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch { }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            ViewBag.SubCategoryID = new SelectList(db.SubCategories, "SubCategoryID", "SubCategoryName",product.SubCategoryID);
            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "InventoryName",product.InventoryID);
            return View();
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Product product = db.Products.Find(id);

            db.Products.Remove(product);

            /*   عند حذف المنتج من جدول المنتوجات وحتى مايصير اعتراض واخلي رقم المنتج في جدول تفاصبل الطلبات ب فارغ */

            var orderditeles = db.OrderDetielss.Where(od =>od.ProductID==id);
            foreach (var item in orderditeles)
            {
                item.ProductID = null;
            }

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
