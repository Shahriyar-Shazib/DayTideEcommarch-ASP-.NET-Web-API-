using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    [NotMapped]
    public class OrderDetailsGetViewModel
    {
        public string CustomerId { get; set; }
        public int OrderId { get; set; }
    }
}