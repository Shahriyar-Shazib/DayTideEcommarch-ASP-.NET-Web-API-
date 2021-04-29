using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DayTideWebApi.Migrations;

namespace DayTideWebApi.Models
{
    public class DayTideAPIContext:DbContext
    {
        public DayTideAPIContext():base("name=DayTideAPIConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DayTideAPIContext,Configuration>());
        }
        public DbSet<User> Users { set; get; }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<Application> Applications { set; get; }
         public DbSet<Cart> Carts { set; get; }
        public DbSet<CartBackup> CartBackups { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Customer> Customers { set; get; }
         public DbSet<Delivery_Man_Rating> Delivery_Man_Ratings { set; get; }
        public DbSet<DeliveryMan> DeliveryMen { set; get; }
        public DbSet<Moderator> Moderators { set; get; }
        public DbSet<Notice> Notices { set; get; }
        public DbSet<Order_Detail> Order_Details { set; get; }
        public DbSet<OrderRequest> OrderRequests { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Product_Rating> Product_Ratings { set; get; }
       
    }
}