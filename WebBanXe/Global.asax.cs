using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanXe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            Session["UserAdmin"] = "";
        }
            protected void Application_BeginRequest(object sender, EventArgs e)
            {
                //You don't want to redirect on posts, or images/css/js
                bool isGet = HttpContext.Current.Request.RequestType.ToLowerInvariant().Contains("get");
                if (isGet && !HttpContext.Current.Request.Url.AbsolutePath.Contains("."))
                {
                    //You don't want to modify URL encoded chars (ex.: %C3%8D that's code to Í accented I) to lowercase, than you need do decode the URL
                    string urlLowercase = Request.Url.Scheme + "://" + HttpUtility.UrlDecode(HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.AbsolutePath);
                    //You want to consider accented chars in uppercase check
                    if (Regex.IsMatch(urlLowercase, @"[A-Z]") || Regex.IsMatch(urlLowercase, @"[ÀÈÌÒÙÁÉÍÓÚÂÊÎÔÛÃÕÄËÏÖÜÝÑ]"))
                    {
                        //You don't want to change casing on query strings
                        urlLowercase = urlLowercase.ToLower() + HttpContext.Current.Request.Url.Query;

                        Response.Clear();
                        Response.Status = "301 Moved Permanently";
                        Response.AddHeader("Location", urlLowercase);
                        Response.End();
                    }
                }

            }
        }
    }
