using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rector_blog.Filters
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // если пользователь не принадлежит роли admin, то он перенаправляется на Home/About
            bool auth = filterContext.HttpContext.User.IsInRole("Admin");
            bool isAuthorized = filterContext.HttpContext.User.Identity.IsAuthenticated;
            if (isAuthorized)
            {
                if (!auth)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary {
                { "controller", "Home" }, { "action", "Index" }
                    });
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary {
                { "controller", "Account" }, { "action", "Login" }
                    });
            }
        }
            
        
    }

}
