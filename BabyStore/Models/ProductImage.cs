

using System.ComponentModel.DataAnnotations;
namespace BabyStore.Models
{
    public class ProductImage
    {
        public int ID { get; set; }
        [Display(Name="الملف")]
       public string FileName {get; set;}
    }
}