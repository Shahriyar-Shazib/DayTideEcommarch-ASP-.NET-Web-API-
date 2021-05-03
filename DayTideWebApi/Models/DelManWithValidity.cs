using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    [NotMapped]
    public class DelManWithValidity
    {
        public DeliveryMan deliveryMan { get; set; }
        public User user { get; set; }
    }
}