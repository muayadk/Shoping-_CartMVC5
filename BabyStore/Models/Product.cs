using System.ComponentModel.DataAnnotations;

using BabyStore.Views.Shared;
using System;
using System.Collections.Generic;
using System.Web.WebPages.Html;
namespace BabyStore.Models
    
{
    public class Product
    {
       
        public int ID { get; set; }

        [Required(ErrorMessage = "The product name cannot be blank")]
     
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a product namebetween 3 and 50 characters in length")] 
        //[RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a product name made up of letters and numbers only")]
        [Display(Name="Name",ResourceType= typeof(SiteResource))]
        public string Name { get; set; }



        [Required(ErrorMessage = "The product description can not be blank")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Please enter a productdescription between 10 and 200 characters in length")]
        //[RegularExpression(@"^[,;a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a productdescription made up of letters and numbers only")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(SiteResource))]
        public string Description { get; set; }


        [Required(ErrorMessage = "The price cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a price between 0.10 and 10000.00")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "The price must be a number up to two decimal places")]
        [Display(Name = "BuyPrice")]
        public decimal BuyPrice { get; set; }

        [Required(ErrorMessage = "The price cannot be blank")]
        [Range(0.10, 10000, ErrorMessage = "Please enter a price between 0.10 and 10000.00")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "The price must be a number up to two decimal places")]
        [Display(Name = "Price", ResourceType = typeof(SiteResource))]
        public decimal Price { get; set; }
       


           [Display(Name= "productImg", ResourceType = typeof(SiteResource))]
        public string productImg { get; set; }



        [Display(Name = "Category", ResourceType = typeof(SiteResource))]
         public int? CategoryID { get; set; }



        public int? SubCategoryID { get; set; }
        public int? InventoryID { get; set; }

        // country product
         [Display(Name= "CountryProuduct", ResourceType = typeof(SiteResource))]
         public string CountryProuduct { get; set; }
        
        
        //  تاريخ الانتاج
       [Display(Name = "DateProduct", ResourceType = typeof(SiteResource))]
      // [DataType(DataType.Date)]
        [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode = true)]
        public DateTime  DateProduct { get; set; }

        // تاريخ الانتهاء
        [Display(Name = "DateExpire", ResourceType = typeof(SiteResource))]
        //[DataType(DataType.Date)]
       [DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:d}", ApplyFormatInEditMode = true)]
     
        public DateTime DateExpire { get; set; }


        // المستخدم الذي ادخل البيانات
      [Display(Name = "UserID", ResourceType = typeof(SiteResource))]
      public string UserID { get; set; }

        // تاريخ ادخال البيانات
        [Display(Name = "UserInsertDate", ResourceType = typeof(SiteResource))]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
      public Nullable<DateTime> UserInsertDate { get; set; }

       //تاريخ تحديث البيانات
        [Display(Name = "UserUpdateDate", ResourceType = typeof(SiteResource))]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
      public Nullable<DateTime> UserUpdateDate { get; set; }

         [Display(Name = "Quantity", ResourceType = typeof(SiteResource))]


        public decimal  Quantity { get; set; }

        public virtual Category Category { get; set; }

        //public IEnumerable<SelectListItem> Categories { get; set; }
        public virtual Inventory Inventory { get; set; }

        public virtual SubCategory SubCategory { get; set; }
      
        

    }
}