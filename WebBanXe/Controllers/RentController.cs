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
        public ActionResult Prent(int? idBrand, int? idType, int? idStatus, int? page)
        {
            List<PRODUCT> listproduct = new List<PRODUCT>();
            ViewBag.BRAND = db.BRANDs.ToList();
            if (idBrand != null || idType != null || idStatus != null)
            {
                listproduct = db.PRODUCTs.Where(p => p.IdBrand == idBrand || p.IdType == idType || p.Status == idStatus).ToList();
            }
            else
            {
                listproduct = db.PRODUCTs.Where(s=>s.Status == 0).ToList();
            }
            return View(listproduct);

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