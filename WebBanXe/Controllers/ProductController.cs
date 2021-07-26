using PagedList;
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

        [Route("san-pham")]
        // GET: Product
        private List<PRODUCT> GetProduct(List<PRODUCT> products)
        {
            return products.OrderByDescending(a => a.IdProduct).ToList();
        }

        public ActionResult Index(int? idBrand, int? idType, int? idStatus, int? page)
        {
            int pageSize = 3;
            int pageNum = (page ?? 1);
            var NewProduct = new List<PRODUCT>();
            List<PRODUCT> listproduct = new List<PRODUCT>();
            ViewBag.BRAND = db.BRANDs.ToList();
            if (idBrand != null || idType != null || idStatus != null)
            {
                NewProduct = GetProduct(db.PRODUCTs.Where(p => p.IdBrand == idBrand || p.IdType == idType || p.Status == idStatus).ToList());
            }
            else
            {
                NewProduct = GetProduct(db.PRODUCTs.ToList());
            }
            return View(NewProduct.ToPagedList(pageNum, pageSize));

        }

        [Route("san-pham/{url}")]
        public ActionResult Detail(string url)
        {
            int id = PRODUCT.GetIDFromURL(url) ?? 0;
            PRODUCT product = new PRODUCT();
            product = db.PRODUCTs.Where(p => p.IdProduct == id).SingleOrDefault();
            if (product is null) return RedirectToAction("Index", "Home");

            var request = HttpContext.Request.Url.AbsoluteUri;
            ViewBag.Url = request;
            ViewBag.Title = product.NameProduct;
            ViewBag.Same = db.PRODUCTs.Where(x => x.IdType == product.IdType || x.IdBrand == product.IdBrand).ToList();
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

        public ActionResult StatusCar()
        {
            return View();
        }
    }
}