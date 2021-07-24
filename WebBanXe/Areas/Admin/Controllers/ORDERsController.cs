using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class ORDERsController : BaseController
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/ORDERs
        public ActionResult Index()
        {
            var oRDERs = db.ORDERs.Include(o => o.DISCOUNT).Include(o => o.USER);
            return View(oRDERs.ToList());
        }

        // GET: Admin/ORDERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERs.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
