using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyStore.Models
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public List<Product> Product { get; set; }
    }
}