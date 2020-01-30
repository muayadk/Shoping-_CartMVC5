using BabyStore.Models;
using BabyStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyStore.Controllers
{
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult Index()
        {
            Basket basket = Basket.GetBasket();
            BasketViewModel viewModel = new BasketViewModel
            {
                BasketLines = basket.GetBasketlines(),
                TotalCost = basket.GetTotalCost()
            };

            return View(viewModel);
        }

        //calling the GetNumberOfItemsand GetTotalCostmethods from the Basketinstance
        public PartialViewResult Summary()
        {
            Basket basket = Basket.GetBasket();
            BasketSummaryViewModel viewmodel= new BasketSummaryViewModel
            {
                NumberOfitem =basket.GetNumberOfitem(),
             TotatlCost= basket.GetTotalCost(),
            };
            return PartialView(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToBasket(int id, int quantity)
        {
            Basket basket = Basket.GetBasket();
            basket.AddToBasket(id, quantity);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBasket(BasketViewModel viewModel)
        {
            Basket basket = Basket.GetBasket();
            basket.UpdateBasket(viewModel.BasketLines);
            return RedirectToAction("Index");
        }

       [HttpGet]
        public ActionResult RemoveLine(int id)
       
       {
           Basket basket = Basket.GetBasket();
           basket.RemoveLine(id);
           return RedirectToAction("Index");

       }


    }
}