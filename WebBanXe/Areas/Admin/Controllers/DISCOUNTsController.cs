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
    public class DISCOUNTsController : BaseController
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/DISCOUNTs
        public ActionResult Index()
        {
            return View(db.DISCOUNTs.ToList());
        }

        // GET: Admin/DISCOUNTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISCOUNT dISCOUNT = db.DISCOUNTs.Find(id);
            if (dISCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(dISCOUNT);
        }

        // GET: Admin/DISCOUNTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DISCOUNTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDiscount,CodeDiscount,Value,Status")] DISCOUNT dISCOUNT)
        {
            if (ModelState.IsValid)
            {
                var discount = db.DISCOUNTs.Where(p => p.CodeDiscount.ToLower() == dISCOUNT.CodeDiscount.ToLower() || p.Value == dISCOUNT.Value).SingleOrDefault();
                if (discount != null)
                {
                    ViewBag.Error = "Khuyến mãi đã tồn tại";
                    return View(dISCOUNT);
                }
                db.DISCOUNTs.Add(dISCOUNT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dISCOUNT);
        }

        // GET: Admin/DISCOUNTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DISCOUNT dISCOUNT = db.DISCOUNTs.Find(id);
            if (dISCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(dISCOUNT);
        }

        // POST: Admin/DISCOUNTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDiscount,CodeDiscount,Value,Status")] DISCOUNT dISCOUNT)
        {
            if (ModelState.IsValid)
            {
                var discount = db.DISCOUNTs.Where(p => p.CodeDiscount.ToLower() == dISCOUNT.CodeDiscount.ToLower() || p.Value == dISCOUNT.Value).SingleOrDefault();
                if (discount != null)
                {
                    ViewBag.Error = "Khuyến mãi đã tồn tại";
                    return View(dISCOUNT);
                }
                db.Entry(dISCOUNT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dISCOUNT);
        }

        // GET: Admin/DISCOUNTs/Delete/5
       
        public ActionResult Delete(int id)
        {
            DISCOUNT dISCOUNT = db.DISCOUNTs.Find(id);
            db.DISCOUNTs.Remove(dISCOUNT);
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
