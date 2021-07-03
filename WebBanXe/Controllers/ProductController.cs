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
            //if (brand != null)
            //{
            //    List<PRODUCT> listProduct = new List<PRODUCT>();
            //    listProduct = db.PRODUCTs.Where(p => p.BRAND.IdBrand == brand).ToList();
            //    ViewBag.listProduct = listProduct; // lấy sản phẩm
            //    ViewBag.title_product = db.BRANDs.Where(p => p.IdBrand == brand).FirstOrDefault(); //lấy tiêu đề
            //}
            //else
            //{
                List<PRODUCT> listProduct = new List<PRODUCT>();
                listProduct = db.PRODUCTs.ToList();
                ViewBag.listProduct = listProduct;// lấy sản phẩm
                ViewBag.title_product = "Tất cả sản phẩm"; // lấy tiêu đề
            //}
            return View();
        }
        public ActionResult ProductDetail()
        {
            return View();
        }
    }
}