using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class Order_DetailRepository : Repository<Order_Detail>
    {
        public List<Order_Detail> GetOrderDetailByUsertId(string id)
        {
            return this.context.Order_Details.Where(x => x.CustomerId == id).ToList();
        }
        public Order_Detail GetOrderDetailByOrderId(int id)
        {
            return context.Order_Details.Find(id);
        }

    }
}