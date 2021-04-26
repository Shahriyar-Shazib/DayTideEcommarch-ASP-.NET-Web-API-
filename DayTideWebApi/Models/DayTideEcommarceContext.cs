using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DayTideWebApi.Migrations;

namespace DayTideWebApi.Models
{
    public class DayTideEcommarceContext:DbContext
    {
        public DayTideEcommarceContext():base("name=DayTideEcommarceConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DayTideEcommarceContext,Configuration>());
        }
        public DbSet<User> Users { set; get; }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<Application> Applications { set; get; }
         public DbSet<Cart> Carts { set; get; }
        public DbSet<CartBackup> CartBackups { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Customer> Customers { set; get; }
         public DbSet<Delevary_Man_Rating> Delevary_Man_Ratings { set; get; }
        public DbSet<DeleveryMan> DeleveryMen { set; get; }
        public DbSet<Moderator> Moderators { set; get; }
        public DbSet<Notice> Notices { set; get; }
        public DbSet<Order_Detail> Order_Details { set; get; }
        public DbSet<OrderRequest> OrderRequests { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Product_Rating> Product_Ratings { set; get; }
       
    }
}