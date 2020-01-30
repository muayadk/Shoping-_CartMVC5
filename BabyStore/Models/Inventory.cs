using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyStore.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public string InventoryName { get; set; }

        public string InventoryLocation { get; set; }
      

        public virtual List<Product> Products { get; set; }

    }
}