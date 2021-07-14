using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class PRODUCTsController : Controller
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/PRODUCTs
        public ActionResult Index()
        {
           
            var pRODUCTs = db.PRODUCTs.Include(p => p.BRAND).Include(p => p.TYPECAR);
            return View(pRODUCTs.ToList());
        }

        // GET: Admin/PRODUCTs/Details/5
        public ActionResult Details(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand");
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdProduct,NameProduct,Price,Description,Status,IdBrand,IdType")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTs.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
            return View(pRODUCT);
        
     
        }

        // GET: Admin/PRODUCTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IdProduct,NameProduct,Price,Description,Status,IdBrand,IdType")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Delete/5
        public ActionResult Delete(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
            db.SaveChanges();
            return RedirectToAction("Index");
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
