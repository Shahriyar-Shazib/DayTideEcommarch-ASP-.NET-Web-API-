using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class DeliveryManRepository : Repository<DeliveryMan>
    {
        public List<DeliveryMan> GetDeleveryMenByAdd(string add)
        {
            return this.context.DeliveryMen.Where(x => x.Address.Contains(add) ).ToList();
        }
    }
}