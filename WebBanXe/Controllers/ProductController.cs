using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{
    public class ProductController : Controller
    {
        DBBanXeEntities db = new DBBanXeEntities();
        // GET: Product
        public ActionResult Index()
        {
                List<PRODUCT> listProduct = new List<PRODUCT>();
                listProduct = db.PRODUCTs.ToList();
            return View(listProduct);
        }

        public ActionResult Detail(int id)
        {
            PRODUCT product = new PRODUCT();
            product = db.PRODUCTs.Where(p => p.IdProduct == id).SingleOrDefault();
            return View(product);
        }

       
    }
}