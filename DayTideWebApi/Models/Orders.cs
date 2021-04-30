using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    [NotMapped]
    public class Orders
    {
        public OrderRequest Order { get; set; }
        public CartBackup Cart { get; set; }
    }
}