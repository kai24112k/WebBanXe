using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;
using WebBanXe.Service;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class CONTACTsController : Controller
    {
        private DBBanXeEntities db = new DBBanXeEntities();
        EmailService emailService = new EmailService();

        // GET: Admin/CONTACTs
        public ActionResult Index()
        {
            var cONTACTs = db.CONTACTs.Include(c => c.USER);
            return View(cONTACTs.ToList());
        }

        // GET: Admin/CONTACTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACT cONTACT = db.CONTACTs.Find(id);
            if (cONTACT == null)

            {
                return HttpNotFound();
            }
            cONTACT.Status = true;
            db.SaveChanges();
            return View(cONTACT);
        }

        // GET: Admin/CONTACTs/Create
      
        // GET: Admin/CONTACTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACT cONTACT = db.CONTACTs.Find(id);
            if (cONTACT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", cONTACT.IdUser);
            return View(cONTACT);
        }

        // POST: Admin/CONTACTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdContact,IdUser,Title,Email,Content")] CONTACT cONTACT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTACT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", cONTACT.IdUser);
            return View(cONTACT);
        }

        // GET: Admin/CONTACTs/Delete/5
       
        public ActionResult Delete(int id)
        {
            CONTACT cONTACT = db.CONTACTs.Find(id);
            db.CONTACTs.Remove(cONTACT);
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
        [HttpGet]
        public ActionResult Feedback(int id)
        {
            var contact = db.CONTACTs.Find(id);
            return View(contact);
        }

        [HttpPost]
        public ActionResult Feedback(FormCollection frm)
        {
            string Address = frm["address"];
            string Title = frm["title"];
            string Message = frm["message"];
            emailService.sendEmail(Address, Title, Message);      
            return RedirectToAction("index", "CONTACTS"); 

        }
    }
}
