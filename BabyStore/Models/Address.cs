using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabyStore.Views.Shared;
namespace BabyStore.Models
{
    public class Address
    {
        [Required]
        [Display(Name = "AddressLine1",ResourceType=typeof(SiteResource))]
        public string AddressLine1 { get; set; }
        [Display(Name = "AddressLine2", ResourceType = typeof(SiteResource))]
        public string AddressLine2 { get; set; }
        [Required]
        [Display (Name="Town",ResourceType=typeof(SiteResource))]
        public string Town { get; set; }
        [Required]
        [Display(Name="County",ResourceType=typeof(SiteResource))]
        public string County { get; set; }
        /*[Required]*/
        [Display(Name = "Postcode", ResourceType = typeof(SiteResource))]
        public string Postcode { get; set; } 
    }
}