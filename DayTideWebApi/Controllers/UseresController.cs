using DayTideWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DayTideWebApi.Controllers
{
    
    public class UseresController : ApiController
    {
        UserRepository Usrrepo = new UserRepository();
        public IHttpActionResult GetUsers()
        {
            return Ok(Usrrepo.GetAll());
        }
    }
}
