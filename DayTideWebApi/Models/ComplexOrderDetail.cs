using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class ComplexOrderDetail
    {
        public Order_Detail order_Detail { get; set; }
        public Customer customer { get; set; }
        public DeliveryMan deliveryMan { get; set; }
    }
}