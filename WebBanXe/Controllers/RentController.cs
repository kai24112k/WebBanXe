using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanXe.Controllers
{   
    public class RentController : Controller
    {
        [Route("thue-xe")]
        // GET: Rent
        public ActionResult Index()
        {
            return View();
        }
    }
}