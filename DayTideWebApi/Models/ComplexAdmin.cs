using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class ComplexAdmin
    {
        public Admin admin { get; set; }
        public User user { get; set; }
       
    }
}