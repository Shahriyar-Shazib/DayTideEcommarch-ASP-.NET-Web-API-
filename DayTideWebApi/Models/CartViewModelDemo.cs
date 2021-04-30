using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    [NotMapped]
    public class CartViewModelDemo
    {
        
        public int Id { get; set; }
        
        public string CustomerId { get; set; }
        
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        public double Price_unit_ { get; set; }


        public string ProductName { get; set; }
        public string Picture { get; set; }



    }
}