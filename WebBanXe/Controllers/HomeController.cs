using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DBBanXeEntities db = new DBBanXeEntities();
        // GET: Product
        public ActionResult Index()
        {
            List<PRODUCT> listProductHome = new List<PRODUCT>();
            listProductHome = db.PRODUCTs.ToList();
            return View(listProductHome);
        }
    }
}