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

        private List<PRODUCT> GetNewProduct(int count)
        {
            return db.PRODUCTs.OrderByDescending(a => a.IdProduct).Take(count).ToList();
        }
        private List<BLOG> GetBlog(int count)
        {
            return db.BLOGs.OrderByDescending(a => a.IdBlog).Take(count).ToList();
        }
        public ActionResult Index()
        {
            //List<PRODUCT> listProduct = new List<PRODUCT>();
            //listProduct = db.PRODUCTs.ToList();
            var newproduct = GetNewProduct(3);
           
            ViewBag.BRAND = db.BRANDs.ToList();
            ViewBag.BLOG = db.BLOGs.ToList();
            var NewBlog = GetBlog(4);
            return View(newproduct);

        }
        

    }
}