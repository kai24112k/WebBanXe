using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBanXe.Model;

namespace WebBanXe.Helpers.Security
{
    public class Authen
    {
        public enum Role
        {
            Customer = 3,
            Admin = 1
        }

        public class Auth : AuthorizeAttribute
        {
            private readonly int[] allowRole;

            public Auth()
            {
                allowRole = new int[] { (int)Role.Admin, (int)Role.Customer };
            }

            public Auth(params int[] role)
            {
                allowRole = role;
            }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                // Authen
                if (httpContext.Session["userID"] is null) return false;

                // Authorize
                int id = int.Parse(httpContext.Session["userID"].ToString());
                using (DBBanXeEntities db = new DBBanXeEntities())
                {
                    var userDB = db.USERs.Find(id);
                    if (userDB is null) return false;
                    if (!allowRole.Any(x => x == userDB.IdRole)) return false;
                }
                return true;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Customer", returnURL = HttpContext.Current.Request.Url.AbsoluteUri }));
            }
        }
    }
}