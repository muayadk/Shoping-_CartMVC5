using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.SqlServer; 
using System.ComponentModel.DataAnnotations;
using System;
using BabyStore.Views.Shared;
namespace BabyStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(SiteResource))]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "LastName", ResourceType = typeof(SiteResource))]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DateOfBirth", ResourceType = typeof(SiteResource))]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public bool StatusUser { get; set; }
        [DataType(DataType.Date)]
       //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateCreateUser { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
       public Nullable<DateTime> DateEditUser { get; set; }

       [Display(Name = "Address", ResourceType = typeof(SiteResource))] 
        public Address Address { get; set; }


        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
       
        }

        static ApplicationDbContext()
        {
   
            // Set the database initializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
       public static ApplicationDbContext Create()
        {
           
            return new ApplicationDbContext();
        }
        public DbSet<Product> Products { get; set; }
      public DbSet<Category> Categories { get; set; }
      public DbSet<ProductImage> ProductImages { get; set; }
      public DbSet<BasketLine> BasketLines { get; set; }
      public DbSet<Order> Orders { get; set; }
      public DbSet<OrderDetiels> OrderDetielss { get; set; }
      public DbSet<SubCategory> SubCategories { get; set; }

      public DbSet<Inventory> Inventories { get; set; }

     
    }
}