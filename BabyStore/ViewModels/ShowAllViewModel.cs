using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyStore.Models;
namespace BabyStore.ViewModels
{
    public class ShowAllViewModel
    {

        public Product Product { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }

       public IEnumerable<SubCategory> SubCategories{ get; set; }

    }

}