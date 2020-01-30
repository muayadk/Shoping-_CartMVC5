using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabyStore.Views.Shared;

namespace BabyStore.Models
{
    public class OrderDetiels
    {
        public int OrderDetielsID { get; set; }
        public int OrderID{ get; set; }

        public int? ProductID { get; set; }
        [Display(Name = "Quantity", ResourceType = typeof(SiteResource))]
        public int Quantity { get; set; }

        public string ProductName { get; set; }

        [Display(Name = "UnitPrice", ResourceType = typeof(SiteResource))]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString="{0:c}")]
        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }


    }
}