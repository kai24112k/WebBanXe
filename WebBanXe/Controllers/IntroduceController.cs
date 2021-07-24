using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanXe.Controllers
{
    public class IntroduceController : Controller
    {

        // GET: Introduce
        [Route("gioi-thieu")]
        public ActionResult Index()
        {
            ViewBag.Title = "Giới thiệu";
            return View();
        }
    }
}