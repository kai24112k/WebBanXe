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
        public ActionResult PBrand(int? id)
        {
            List<BRAND> listbrand = new List<BRAND>();
            if (id != null)
            {
                listbrand = db.BRANDs.Where(p => p.IdBrand == id).ToList();
            }
            else
            {
                listbrand = db.BRANDs.ToList();
            }
            return View(listbrand);
        }
        public ActionResult PTypeCar(int? id)
        {
            List<TYPECAR> listtypecar = new List<TYPECAR>();
            if (id != null)
            {
                listtypecar = db.TYPECARs.Where(p => p.IdType == id).ToList();
            }
            else
            {
                listtypecar = db.TYPECARs.ToList();
            }
            return View(listtypecar);
        }

    }
}