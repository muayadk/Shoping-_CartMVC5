using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
namespace BabyStore.Controllers
{
    public class LanguageController : Controller
    {
        // GET: MultiLanguage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Change(string LanguageAbbrevation)
        {
            if(LanguageAbbrevation !=null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new  CultureInfo(LanguageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);

            return View("Index");
        }
    }
}