using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class ComplexCustomer
    {
        public Customer customer { get; set; }
        public User user { get; set; }
     
    }
}