using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyStore.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int NumberOfitem { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:c}")]

      public decimal TotatlCost { get; set; }
    }
}