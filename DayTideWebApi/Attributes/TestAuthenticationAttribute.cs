using DayTideWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DayTideWebApi.Attributes
{

    public class TestAuthenticationAttribute : AuthorizationFilterAttribute
    {
        DayTideAPIContext context = new DayTideAPIContext();
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

                string userName = splittedText[0];
                string password = splittedText[1];
                string type = "Customer";

                bool TestLogin(string uname, string pass)
                {
                    User data = context.Users.Where(x => x.UserId == uname).FirstOrDefault();
                    if (data != null && data.Password == pass && data.Type == type)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }




                if (TestLogin(userName, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                }

            }
        }
    }
}