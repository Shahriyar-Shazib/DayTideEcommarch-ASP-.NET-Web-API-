using DayTideWebApi.Models;
using DayTideWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DayTideWebApi.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        UserRepository userRepo = new UserRepository();
        [Route("login"), HttpGet]
        public IHttpActionResult login(string id,string pass)
        {
            User usr = userRepo.GetUserById(id);
            if (usr == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                if (usr.Password == pass)
                {
                    if (usr.Status == "valid")
                    {
                        return Ok(usr);
                    }
                    return StatusCode(HttpStatusCode.Forbidden);
                }

                return StatusCode(HttpStatusCode.NoContent);

            }

        }
    }
}