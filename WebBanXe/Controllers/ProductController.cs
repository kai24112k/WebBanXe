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
        public ActionResult Status(int? id)
        {
            List<PRODUCT> listProduct = new List<PRODUCT>();
            if (id != null)
            {
                listProduct = db.PRODUCTs.Where(p=>p.Status == id).ToList();
            }
            else
            {
                listProduct = db.PRODUCTs.ToList();
            }
            return View(listProduct);
        }
        public ActionResult Price(int? id)
        {
            List<PRODUCT> listProduct = new List<PRODUCT>();
            if (id != null)
            {
                listProduct = new List<PRODUCT>();
                listProduct = db.PRODUCTs.Where(p =>p.Price <= id).ToList();
            }
            else
            {
                listProduct = db.PRODUCTs.ToList();
            }
            return View(listProduct);
        }
        public ActionResult LineCar(String id)
        {
            List<PRODUCT> listProduct = new List<PRODUCT>();
            if (id != null)
            {
                listProduct = new List<PRODUCT>();
                listProduct = db.PRODUCTs.Where(p => p.NameProduct.Contains(id)).ToList();
            }
            else
            {
                listProduct = db.PRODUCTs.ToList();
            }
            return View(listProduct);
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

    }
}