
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BabyStore.Views.Shared;
using System;
namespace BabyStore.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "The category name cannot be blank")]
        [StringLength(50, MinimumLength = 3,ErrorMessage="Please enter a category name between 3 and 50 characters in length")]
       // [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$",ErrorMessage="lease enter a categoryname beginning with a capital letter and made up of letters and spaces only")]
        // display Category Name for distinict between Name Category and Product Name
        [Display(Name = "الصنف")]
        public string Name { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(SiteResource))]
        public string UserID{ get; set; }


         [Display(Name = "UserInsertDate", ResourceType = typeof(SiteResource))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> UserInsertDate{ get; set; }

         [Display(Name = "UserUpdateDate", ResourceType = typeof(SiteResource))]
        [DataType(DataType.Date),DisplayFormat(DataFormatString=@"{0:dd\/MM\/yyyy HH: mm}",ApplyFormatInEditMode=true)]
   
        public Nullable<DateTime> UserUpdateDate { get; set; }


        public virtual ICollection<Product> Products { get; set; }


        public virtual List<SubCategory> SubCategories { get; set; }
    }
}