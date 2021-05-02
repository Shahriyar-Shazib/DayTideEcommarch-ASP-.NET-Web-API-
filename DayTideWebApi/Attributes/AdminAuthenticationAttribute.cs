using DayTideWebApi.Models;
using DayTideWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DayTideWebApi.Attributes
{
    public class AdminAuthenticationAttribute : AuthorizationFilterAttribute
    {
        UserRepository usrRepo = new UserRepository();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string encorded = actionContext.Request.Headers.Authorization.Parameter;
                string decorded = Encoding.UTF8.GetString(Convert.FromBase64String(encorded));
                string[] splittedText = decorded.Split(new char[] { ':' });

                string Type = splittedText[0];
                string pass = splittedText[1];
              
                if (Type != "Admin"&& pass!="111")
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
                /*//string userid = splittedText[1];
                //string pass = splittedText[2];
                //User usr = usrRepo.GetUserById(userid);

                if (usr == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    if(usr.Password!=pass && usr.Type!=Type)
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
                */
            }

        }
    }
}