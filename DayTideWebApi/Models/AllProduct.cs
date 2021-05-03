using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class AllProduct
    {
        public Category category { get; set; }
        public Product product { get; set; }
    }
}