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
    public class RENTsController : BaseController
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/RENTs
        public ActionResult Index()
        {
            var rENTs = db.RENTs.Include(r => r.PRODUCT).Include(r => r.USER);
            return View(rENTs.ToList());
        }

        // GET: Admin/RENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RENT rENT = db.RENTs.Find(id);
            if (rENT == null)
            {
                return HttpNotFound();
            }
            return View(rENT);
        }

        // GET: Admin/RENTs/Create
        public ActionResult Create()
        {
            ViewBag.IdProduct = new SelectList(db.PRODUCTs, "IdProduct", "NameProduct");
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName");
            return View();
        }

        // POST: Admin/RENTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRent,DateRent,DateBack,Price,Status,IdUser,IdProduct")] RENT rENT)
        {
            if (ModelState.IsValid)
            {
                db.RENTs.Add(rENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProduct = new SelectList(db.PRODUCTs, "IdProduct", "NameProduct", rENT.IdProduct);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", rENT.IdUser);
            return View(rENT);
        }

        // GET: Admin/RENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RENT rENT = db.RENTs.Find(id);
            if (rENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProduct = new SelectList(db.PRODUCTs, "IdProduct", "NameProduct", rENT.IdProduct);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", rENT.IdUser);
            return View(rENT);
        }

        // POST: Admin/RENTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRent,DateRent,DateBack,Price,Status,IdUser,IdProduct")] RENT rENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProduct = new SelectList(db.PRODUCTs, "IdProduct", "NameProduct", rENT.IdProduct);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", rENT.IdUser);
            return View(rENT);
        }

        // GET: Admin/RENTs/Delete/5
       
        public ActionResult Delete(int id)
        {
            RENT rENT = db.RENTs.Find(id);
            db.RENTs.Remove(rENT);
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
