using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{   
    public class RentController : Controller
    {
        DBBanXeEntities db = new DBBanXeEntities();

        // GET: Product
        public ActionResult Index()
        {
            //var ngaynhan = Session["Ngaynhan"];
            //var ngatra = Session["Ngaytra"];
            ViewBag.BRAND = db.BRANDs.ToList();
            return View();

        }

        [Route("thue-xe")]
        public ActionResult RentProduct(int? idBrand, int? idType, int? page)
        {
            //int pageSize = 6;
            //int pageNum = (page ?? 1);
            //var xemoi = 
            List<RENT> listrent = new List<RENT>();
            ViewBag.BRAND = db.BRANDs.ToList();
            if (idBrand != null || idType != null)
            {
                listrent = db.RENTs.Where(p => p.PRODUCT.IdBrand == idBrand || p.PRODUCT.IdType == idType).ToList();
            }
            else
            {
                listrent = db.RENTs.ToList();
            }
            return View(listrent);
        }

        [Route("thue-xe/{url}")]
        public ActionResult Detail(string url)
        {
            int id = PRODUCT.GetIDFromURL(url) ?? 0;
            PRODUCT product = new PRODUCT();
            product = db.PRODUCTs.Where(p => p.IdProduct == id).SingleOrDefault();
            if (product is null) return RedirectToAction("Index", "Home");

            var request = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.Url = request;
            ViewBag.Title = product.NameProduct;

            var imgDB = product.IMG_PRODUCT.FirstOrDefault();
            if (imgDB != null) ViewBag.MetaIMG = imgDB.LinkImg;

            return View(product);
        }

        public ActionResult Brand()
        {
            List<BRAND> listbrand = new List<BRAND>();
            listbrand = db.BRANDs.ToList();
            return View(listbrand);
        }
        public ActionResult TypeCar()
        {
            List<TYPECAR> listtypecar = new List<TYPECAR>();
            listtypecar = db.TYPECARs.ToList();
            return View(listtypecar);
        }
    }
}