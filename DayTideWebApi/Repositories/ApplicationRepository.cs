using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class ApplicationRepository : Repository<Application>
    {
        public Application GetApplicationById(int id)
        {
            return this.context.Applications.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}