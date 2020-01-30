using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Globalization;

namespace BabyStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          // Database.SetInitializer<DbContext>(null);
          // Database.SetInitializer<DbContext>(new DropCreateDatabaseIfModelChanges<DbContext>());

           ModelBinders.Binders.Add(typeof(DateTime), new CustomDateModelBinder());
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
          
            HttpCookie cookie= HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }

            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            }
        }


        public class CustomDateModelBinder : DefaultModelBinder
        {
            public override object BindModel
            (ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                if (!string.IsNullOrEmpty(displayFormat) && value != null)
                {
                    DateTime date;
                    displayFormat = displayFormat.Replace
                    ("{0:", string.Empty).Replace("}", string.Empty);
                    if (DateTime.TryParseExact(value.AttemptedValue, displayFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        return date;
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            string.Format("{0} is an invalid date format", value.AttemptedValue)
                        );
                       }
                  }

                return base.BindModel(controllerContext, bindingContext);
            }
        }
        
    }
}
