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
    public class CATEGORY_BLOGController : Controller
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/CATEGORY_BLOG
        public ActionResult Index()
        {
            return View(db.CATEGORY_BLOG.ToList());
        }

        // GET: Admin/CATEGORY_BLOG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY_BLOG cATEGORY_BLOG = db.CATEGORY_BLOG.Find(id);
            if (cATEGORY_BLOG == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY_BLOG);
        }

        // GET: Admin/CATEGORY_BLOG/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CATEGORY_BLOG/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCate,NameCate")] CATEGORY_BLOG cATEGORY_BLOG)
        {
            if (ModelState.IsValid)
            {
                db.CATEGORY_BLOG.Add(cATEGORY_BLOG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATEGORY_BLOG);
        }

        // GET: Admin/CATEGORY_BLOG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY_BLOG cATEGORY_BLOG = db.CATEGORY_BLOG.Find(id);
            if (cATEGORY_BLOG == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY_BLOG);
        }

        // POST: Admin/CATEGORY_BLOG/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCate,NameCate")] CATEGORY_BLOG cATEGORY_BLOG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cATEGORY_BLOG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATEGORY_BLOG);
        }

        // GET: Admin/CATEGORY_BLOG/Delete/5       
        public ActionResult Delete(int id)
        {
            CATEGORY_BLOG cATEGORY_BLOG = db.CATEGORY_BLOG.Find(id);
            db.CATEGORY_BLOG.Remove(cATEGORY_BLOG);
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
