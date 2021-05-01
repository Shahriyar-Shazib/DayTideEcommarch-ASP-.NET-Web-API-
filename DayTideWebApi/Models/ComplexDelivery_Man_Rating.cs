using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Models
{
    public class ComplexDelivery_Man_Rating
    {
        public DeliveryMan deliveryMan { get; set; }
        public Delivery_Man_Rating delivery_Man_Rating { get; set; }
    }
}