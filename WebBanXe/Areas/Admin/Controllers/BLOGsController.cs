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
    public class BLOGsController : Controller
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/BLOGs
        public ActionResult Index()
        {
            var bLOGs = db.BLOGs.Include(b => b.CATEGORY_BLOG).Include(b => b.USER);
            return View(bLOGs.ToList());
        }

        // GET: Admin/BLOGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOGs.Find(id);
            if (bLOG == null)
            {
                return HttpNotFound();
            }
            return View(bLOG);
        }

        // GET: Admin/BLOGs/Create
        public ActionResult Create()
        {
            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate");
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdBlog,IdCate,IdUser,Title,Content,DateCreate")] BLOG bLOG)
        {
            if (ModelState.IsValid)
            {
                db.BLOGs.Add(bLOG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate", bLOG.IdCate);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", bLOG.IdUser);
            return View(bLOG);
        }

     
        [ValidateInput(false)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLOG bLOG = db.BLOGs.Find(id);
            if (bLOG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate", bLOG.IdCate);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", bLOG.IdUser);
            return View(bLOG);
        }

        // POST: Admin/BLOGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]  
        [ValidateInput(false)]
        public ActionResult Edit (BLOG bLOG)
        {
            if (ModelState.IsValid)
            {
                bLOG.IdUser = int.Parse(Session["userID"].ToString());
                bLOG.DateCreate = DateTime.Now;
                db.Entry(bLOG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate", bLOG.IdCate);
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", bLOG.IdUser);
            return View(bLOG);
        }


        // POST: Admin/BLOGs/Delete/5

        public ActionResult Delete(int id)
        {
            BLOG bLOG = db.BLOGs.Find(id);
            db.BLOGs.Remove(bLOG);
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
