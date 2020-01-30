using System;
using BabyStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BabyStore.ViewModels
{
     public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }


    }

     public class EditUserViewModel
     {
         public string Id { get; set; }

         [Required(AllowEmptyStrings = false)]
         [Display(Name = "Email")]
         [EmailAddress]
         public string Email { get; set; }

         [Required]
         [Display(Name = "First Name")]
         [StringLength(50)]
         public string FirstName { get; set; }

         [Required]
         [Display(Name = "Last Name")]
         [StringLength(50)]
         public string LastName { get; set; }

         [Required]
         [DataType(DataType.Date)]
         [Display(Name = "Date of birth")]
         [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime DateOfBirth { get; set; }

         public bool StatusUser { get; set; }

         //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
          [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",ApplyFormatInEditMode = true)]
         public Nullable<DateTime> DateEditUser { get; set; }

         public Address Address { get; set; }
         public IEnumerable <SelectListItem> RolesList { get; set; }

     }

}