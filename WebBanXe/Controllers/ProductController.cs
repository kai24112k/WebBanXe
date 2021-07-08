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
        public ActionResult Index(int? idBrand, int? idType, int? idStatus)
        {
            List<PRODUCT> listproduct = new List<PRODUCT>();
            if (idBrand != null || idType != null || idStatus != null)
            {
                listproduct = db.PRODUCTs.Where(p => p.IdBrand == idBrand || p.IdType == idType || p.Status == idStatus).ToList();
            }
            else
            {
                listproduct = db.PRODUCTs.ToList();
            }
            return View(listproduct);

        }


        public ActionResult Detail(int id)
        {
            PRODUCT product = new PRODUCT();
            product = db.PRODUCTs.Where(p => p.IdProduct == id).SingleOrDefault();
            var request = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.Url = request;
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

        public ActionResult StatusCar()
        {
            return View();
        }
    }
}