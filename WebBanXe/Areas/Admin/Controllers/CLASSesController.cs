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
    public class CLASSesController : Controller
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/CLASSes
        public ActionResult Index()
        {
            return View(db.CLASSes.ToList());
        }

        // GET: Admin/CLASSes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS cLASS = db.CLASSes.Find(id);
            if (cLASS == null)
            {
                return HttpNotFound();
            }
            return View(cLASS);
        }

        // GET: Admin/CLASSes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CLASSes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdClass,NameClass")] CLASS cLASS)
        {
            if (ModelState.IsValid)
            {
                db.CLASSes.Add(cLASS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cLASS);
        }

        // GET: Admin/CLASSes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLASS cLASS = db.CLASSes.Find(id);
            if (cLASS == null)
            {
                return HttpNotFound();
            }
            return View(cLASS);
        }

        // POST: Admin/CLASSes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClass,NameClass")] CLASS cLASS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLASS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cLASS);
        }

        // GET: Admin/CLASSes/Delete/5
       
        public ActionResult Delete(int id)
        {
            CLASS cLASS = db.CLASSes.Find(id);
            db.CLASSes.Remove(cLASS);
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
