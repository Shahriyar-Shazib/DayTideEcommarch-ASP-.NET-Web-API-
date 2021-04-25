using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayTideWebApi.Repositories
{
    public class UserRepository : Repository<User>
    {
        public List<User> GetUserSignUpReq()
        {
            return this.context.Users.Where(x => x.Status == "Processing").ToList();
        }
        public List<User> GetBlockedUser()
        {
            return this.context.Users.Where(x => x.Status == "invalid").ToList();
        }
    }
}