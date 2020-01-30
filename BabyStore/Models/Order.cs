using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabyStore.Views.Shared;

namespace BabyStore.Models
{
    public class Order
    {
        [Display(Name = "OrderId", ResourceType = typeof(SiteResource))]

        public int OrderID{ get; set; }

        [Display(Name = "UserID",ResourceType=typeof(SiteResource))]
        public string UserID { get; set; }

        [Display(Name = "DeliveryName",ResourceType=typeof(SiteResource))]

        public string DeliveryName { get; set; }

        [Display(Name = "DeliveryAddress",ResourceType=typeof(SiteResource))]
        public Address DeliveryAddress { get; set; }

        [Display(Name = "TotalPrice",ResourceType=typeof(SiteResource))]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString= "{0:c}")]
        public decimal TotalPrice { get; set; }

      
        [Display(Name = "TimeOrder",ResourceType=typeof(SiteResource))]

        public DateTime DateOrder { get; set; }

        public List<OrderDetiels> OrderDetielss { get; set; }

    }
}