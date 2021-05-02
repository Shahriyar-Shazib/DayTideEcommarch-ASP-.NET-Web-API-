namespace DayTideWebApi.Migrations
{
    using DayTideWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DayTideWebApi.Models.DayTideAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DayTideWebApi.Models.DayTideAPIContext";
        }

        protected override void Seed(DayTideWebApi.Models.DayTideAPIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            List<User> users = new List<User>()
            {
                new User(){UserId= "shah-1",Password="111",Type="Admin",Status="valid"},
                 new User(){UserId= "shah-12",Password="222",Type="Admin",Status="valid"},
                 new User(){UserId= "a-1",Password="111",Type="Moderator",Status="valid"},
                 new User(){UserId= "a-12",Password="222",Type="Moderator",Status="valid"},
                 new User(){UserId= "j-1",Password="111",Type="Customer",Status="valid"},
                 new User(){UserId= "j-12",Password="222",Type="Customer",Status="valid"},
                 new User(){UserId= "p-1",Password="111",Type="Delivery Man",Status="valid"},
                 new User(){UserId= "p-12",Password="222",Type="Delivery Man",Status="valid"},
                 new User(){UserId= "arefin101",Password="1234",Type="Moderator",Status="valid"},
                 new User(){UserId= "joy101",Password="1",Type="customer",Status="valid"},
                 new User(){UserId= "joy102",Password="1",Type="customer",Status="valid"},
                 new User(){UserId= "pantho101",Password="1",Type="DeliveryMan",Status="Processing"},
                 new User(){UserId= "pantho102",Password="1",Type="DeliveryMan",Status="Processing"},
            };
            if (!context.Users.Any())
            {
                foreach (var user in users)
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            List<Admin> admins = new List<Admin>()
            {
                new Admin(){AdminId= "shah-1",Name="Shahriyar",Email="shahriyar@gmail.com",Phone="01956424568",Address="gazipur",Salary=800000,Picture=""},
                 new Admin(){AdminId= "shah-12",Name="shazib",Email="shazib@gmail.com",Phone="01956424568",Address="Dhaka",Salary=500000,Picture=""},
                
            };
            if (!context.Admins.Any())
            {
                foreach (var admin in admins)
                {
                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
            }
            List<Customer> customers = new List<Customer>()
            {
                new Customer(){CustomerId= "j-1",Name="joy",Email="joy@gmail.com",Phone="01956424568",Address="cumilla",Picture=""},
                new Customer(){CustomerId= "j-12",Name="deb",Email="deb@gmail.com",Phone="01956424568",Address="chandpur",Picture=""},
                new Customer(){CustomerId= "joy101",Name="joy1",Email="joy1@gmail.com",Phone="01956424568",Address="Dhaka",Picture="me.jpg"},
                new Customer(){CustomerId= "joy102",Name="joy2",Email="joy2@gmail.com",Phone="01956424568",Address="Chittagong",Picture="me.jpg"},

            };
            if (!context.Customers.Any())
            {
                foreach (var cus in customers)
                {
                    context.Customers.Add(cus);
                    context.SaveChanges();
                }
            }
            List<Moderator> moderators = new List<Moderator>()
            {
                new Moderator(){ModeratorId= "a-1",Name="arifin",Email="arifin@gmail.com",Phone="01956424568",Address="chittagong",Salary=10000,Picture=""},
                new Moderator(){ModeratorId= "a-12",Name="samsul",Email="samsul@gmail.com",Phone="01956424568",Address="mymensing",Salary=5000,Picture=""},
                new Moderator(){ModeratorId= "arefin101",Name="Arefin Khan",Email="arefink910@gmail.com",Phone="01829747029",Address="Tejgaon Dhaka-1208",Salary=50000,Picture="me.jpg"},

            };
            if (!context.Moderators.Any())
            {
                foreach (var mod in moderators)
                {
                    context.Moderators.Add(mod);
                    context.SaveChanges();
                }
            }
            List<DeliveryMan> deleveryMen = new List<DeliveryMan>()
            {
                new DeliveryMan(){DelManId= "p-1",Name="pantho",Email="pantho@gmail.com",Phone="01956424568",Address="chittagong",Salary=10000,Complete_Task=0,Picture=""},
                new DeliveryMan(){DelManId= "p-12",Name="hafiz",Email="hafiz@gmail.com",Phone="01956424568",Address="mymensing",Salary=5000,Complete_Task=0,Picture=""},
                new DeliveryMan(){DelManId= "pantho101",Name="pantho1",Email="pantho1@gmail.com",Phone="01956424568",Address="Dhaka",Salary=10000,Complete_Task=0,Picture=""},
                new DeliveryMan(){DelManId= "pantho102",Name="pantho2",Email="pantho2@gmail.com",Phone="01956424568",Address="Chittagong",Salary=10000,Complete_Task=0,Picture=""},

            };
            if (!context.DeliveryMen.Any())
            {
                foreach (var del in deleveryMen)
                {
                    context.DeliveryMen.Add(del);
                    context.SaveChanges();
                }
            }
            List<Notice> notices = new List<Notice>()
            {
                new Notice(){Id=0,Massage="Hello",Send_by="shah-1",Send_For="shah-12",Status="Unread"},
                 new Notice(){Id=1,Massage="Hi",Send_by="shah-12",Send_For="p-12",Status="Unread"}

            };
            if (!context.Notices.Any())
            {
                foreach (var notice in notices)
                {
                    context.Notices.Add(notice);
                    context.SaveChanges();
                }
            }
            List<Application> applications = new List<Application>()
            {
                new Application(){Id=0,ApplicationType="Leave",Massage="need 3 days off",SentBy="a-1",Status="Processing",Accepted_RejectedBy=null},
                 new Application(){Id=3,ApplicationType="Argent",Massage="need to meet ",SentBy="a-12",Status="Processing",Accepted_RejectedBy=null}

            };
            if (!context.Applications.Any())
            {
                foreach (var app in applications)
                {
                    context.Applications.Add(app);
                    context.SaveChanges();
                }
            }
            List<Category> categories = new List<Category>()
            {
                new Category(){CategoryName = "Food"},
                new Category(){CategoryName = "Cloth"},
                new Category(){CategoryName = "Electronics"},
            };
            if (!context.Categories.Any())
            {
                foreach (var cat in categories)
                {
                    context.Categories.Add(cat);
                    context.SaveChanges();
                }
            }
            List<Product> products = new List<Product>()
            {
                new Product(){ ProductId=1, ProductName="Pant", Picture="me.jpg", Buying_Price=1200, CategoryId=2, Description="Black", Quantity=120, Selling_Price=1500},
               new Product(){ ProductId=2, ProductName="Rice", Picture="me.jpg", Buying_Price=70, CategoryId=1, Description="White Rice", Quantity=14, Selling_Price=80},
            };
            if (!context.Products.Any())
            {
                foreach (var Pro in products)
                {
                    context.Products.Add(Pro);
                    context.SaveChanges();
                }
            }
            List<OrderRequest> orderRequests = new List<OrderRequest>()
            {
                new OrderRequest(){Address="Dhaka", Amount=100, CustomerId="joy101", Date="12/12/12", District="Dhaka", OrderId=1, Payment_Type="Cash"},
                new OrderRequest(){Address="Chittagong", Amount=100, CustomerId="joy101", Date="12/12/12", District="Chittagong", OrderId=2, Payment_Type="Cash"},
            };
            if (!context.OrderRequests.Any())
            {
                foreach (var ord in orderRequests)
                {
                    context.OrderRequests.Add(ord);
                    context.SaveChanges();
                }
            }
            List<CartBackup> cartBackups = new List<CartBackup>()
            {
                new CartBackup(){ OrderId=1, CustomerId="joy101", Price=100, ProductId=1, Quantiry=200 },
                new CartBackup(){ OrderId=2, CustomerId="joy102", Price=100, ProductId=2, Quantiry=300 },
            };
            if (!context.CartBackups.Any())
            {
                foreach (var cart in cartBackups)
                {
                    context.CartBackups.Add(cart);
                    context.SaveChanges();
                }
            }
        }
    }
}
