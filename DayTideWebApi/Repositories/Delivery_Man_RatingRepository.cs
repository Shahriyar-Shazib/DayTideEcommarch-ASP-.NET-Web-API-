using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class Delivery_Man_RatingRepository:Repository<Delivery_Man_RatingRepository>
    {
        public List<Delivery_Man_Rating> GetDeleveryMenRatingById(string id)
        {
            return this.context.Delivery_Man_Ratings.Where(x => x.DelManID==id).ToList();
        }
    }
}