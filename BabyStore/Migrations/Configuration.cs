
namespace BabyStore.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using System.Collections.Generic;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class  Configuration : DbMigrationsConfiguration<BabyStore.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false ;
            ContextKey = "BabyStore.Models.ApplicationDbContext";
        }

        protected override void Seed(BabyStore.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            
             var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

             const string name = "moayadabdo0@gmail.com"; 
            const string password = "Muayad123+-"; 
            const string roleName = "Admin"; 
  
        //Create Role Admin if it does not exist 
        var role = roleManager.FindByName(roleName); 
     if (role == null) 
      { 
      
         role = new IdentityRole(roleName); 
         var roleresult = roleManager.Create(role); 
     
     } 
   
        var user = userManager.FindByName(name); 

     
            if (user == null) 
    
            { 
     
                user = new ApplicationUser 
       
                { 
        
                    UserName = name, 
        
                    Email = name,
                    FirstName = "Muayad",
                    LastName = "Esmail",
                    DateOfBirth = new DateTime(2015, 1, 1),
                   StatusUser =true,
                   DateCreateUser=DateTime.Now,
                    Address = new Address
                    {
                        AddressLine1 = "1 Admin Street",
                        Town = "Town",
                        County = "County",
                        Postcode = "PostCode"
                    }
        
                }; 
     
        
         
                var result = userManager.Create(user, password); 
                result = userManager.SetLockoutEnabled(user.Id, false); 
       } 

        // Add user admin to Role Admin if not already added 
        var rolesForUser = userManager.GetRoles(user.Id); 
        if (!rolesForUser.Contains(role.Name)) 

        { 
            var result = userManager.AddToRole(user.Id, role.Name);
        }

        //Create users role
        const string userRoleName = "Users";
        role = roleManager.FindByName(userRoleName);
        if (role == null)
        {
            role = new IdentityRole(userRoleName);
            var roleresult = roleManager.Create(role);
        }

        


        
    
           var categories = new List<Category>
            {
              new Category { Name = "Computer" },
              new Category { Name = "Play and Toys" },
              new Category { Name = "Feeding" },
              new Category { Name = "Medicine" },
              new Category { Name= "Travel" },
             new Category { Name= "Sleeping" }
            };
            categories.ForEach(c => context.Categories.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();
            var products = new List<Product>
            {
               
                new Product { Name = "TOSHIBA", Description="tow giga bayte",Price=2.99M,CountryProuduct="YEMEN", DateProduct=System.DateTime.Now,DateExpire=System.DateTime.Today,CategoryID=categories.Single( c => c.Name =="Computer").ID },
                 new Product { Name = "DELL", Description="Four giga bayte",Price=2.99M,CountryProuduct="YEMEN", DateProduct=System.DateTime.Now,DateExpire=System.DateTime.Today,CategoryID=categories.Single( c => c.Name =="Computer").ID }

        
            };
            products.ForEach(c => context.Products.AddOrUpdate(p => p.Name, c));
            context.SaveChanges();

            var orders = new List<Order>
            {

                 new Order { DeliveryAddress =new Address {AddressLine1="20 Street", AddressLine2="sity max hail" , Town="Sana'a", County ="Yemen" ,Postcode="Postcode"}, TotalPrice=200.00M, UserID="moayadabdo0@gmail.com", DateOrder=new DateTime (2019,4,4), DeliveryName="Admin"},
                 new Order { DeliveryAddress =new Address {AddressLine1="shomila", AddressLine2="Street Soghatra" , Town="Sana'a", County ="Yemen" ,Postcode="Postcode"}, TotalPrice=400.00M, UserID="User1@gmail.com", DateOrder=new DateTime (2019,5,4), DeliveryName="user"}
            };
            //var orderdetiels = new List<OrderDetiels>
            //{
                
            //    //new OrderDetiels { OrderID=1 , ProductID=products.Single(c=>c.Name =="Sleep Suit").ID, ProductName="Sleep Suit", Quantity=1 ,UnitPrice=products.Single(c=> c.Name=="Sleep Suit").Price },
            //     new OrderDetiels { OrderID=1, ProductID=products.Single(c=>c.Name =="DELL").ID, ProductName="DELL", Quantity=1 ,UnitPrice=products.Single(c=> c.Name=="DELL").Price }
            //};

            //orderdetiels.ForEach(c => context.OrderDetielss.AddOrUpdate(od => od.OrderID, c));
            //context.SaveChanges();

            //orders.ForEach(c=> context.Orders.AddOrUpdate(o=>o.DateOrder, c));
            //context.SaveChanges();


            var subcatogery = new List<SubCategory>
            {
               new SubCategory{SubCategoryID=1,CategoryID=categories.Single(s=>s.Name=="Computer").ID, SubCategoryName="TOSHIBA", Category=new Category{ID=1 ,Name="Computer"}}
            };
            subcatogery.ForEach(s => context.SubCategories.AddOrUpdate(su => su.SubCategoryID));
            context.SaveChanges();

            var inventory = new List<Inventory> 
            { 
                new Inventory{InventoryID =1 ,InventoryName="A",InventoryLocation="Taixz" }
             };

            inventory.ForEach(iv => context.Inventories.AddOrUpdate(i => i.InventoryID));
            context.SaveChanges();
          
        }
    }

 }

