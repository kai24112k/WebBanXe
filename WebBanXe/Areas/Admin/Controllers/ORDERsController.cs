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
    public class ORDERsController : Controller
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

        // GET: Admin/ORDERs/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscount = new SelectList(db.DISCOUNTs, "IdDiscount", "CodeDiscount");
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName");
            return View();
        }

        // POST: Admin/ORDERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrder,IdUser,IdDiscount,TotalMoney,ReduceMoney,FinalMoney,DayCreate,Status")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.ORDERs.Add(oRDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscount = new SelectList(db.DISCOUNTs, "IdDiscount", "CodeDiscount", oRDER.IdDiscount);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", oRDER.IdUser);
            return View(oRDER);
        }

        // GET: Admin/ORDERs/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdDiscount = new SelectList(db.DISCOUNTs, "IdDiscount", "CodeDiscount", oRDER.IdDiscount);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", oRDER.IdUser);
            return View(oRDER);
        }

        // POST: Admin/ORDERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrder,IdUser,IdDiscount,TotalMoney,ReduceMoney,FinalMoney,DayCreate,Status")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscount = new SelectList(db.DISCOUNTs, "IdDiscount", "CodeDiscount", oRDER.IdDiscount);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", oRDER.IdUser);
            return View(oRDER);
        }

        // GET: Admin/ORDERs/Delete/5
        
        public ActionResult Delete(int id)
        {
            ORDER oRDER = db.ORDERs.Find(id);
            db.ORDERs.Remove(oRDER);
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
