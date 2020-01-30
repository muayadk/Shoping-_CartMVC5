using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabyStore.Views.Shared;

namespace BabyStore.Models
{
    public class BasketLine
    {
        public int ID { get; set; }
        
        //UserID==basketID
        public string BasketID { get; set; }

        public int ProductID { get; set; }

        [Range(0, 50, ErrorMessage = "Please enter a quantity between 0 and 50")]
        [Display(Name="Quantity",ResourceType=typeof(SiteResource))]
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Product Product { get; set; }


    }
}