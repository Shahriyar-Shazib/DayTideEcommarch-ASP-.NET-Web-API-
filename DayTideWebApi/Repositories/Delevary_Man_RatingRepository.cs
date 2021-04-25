using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class Delevary_Man_RatingRepository:Repository<Delevary_Man_RatingRepository>
    {
        public List<Delevary_Man_Rating> GetDeleveryMenRatingById(string id)
        {
            return this.context.Delevary_Man_Ratings.Where(x => x.DelManID==id).ToList();
        }
    }
}