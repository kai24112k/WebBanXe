﻿using System;
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
    public class CUSTOMERsController : Controller
    {
        private static DBBanXeEntities db = new DBBanXeEntities();
        public List<USER_ROLE> listRole = db.USER_ROLE.Where(u => u.IdRole ==3).ToList();

        // GET: Admin/CUSTOMERs
        public ActionResult Index()
        {
            var uSERs = db.USERs.Include(u => u.USER_ROLE).Where(u => u.IdRole==3);
            return View(uSERs.ToList());
        }

        // GET: Admin/CUSTOMERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: Admin/CUSTOMERs/Create
        public ActionResult Create()
        {

            ViewBag.IdRole = new SelectList(listRole, "IdRole", "RoleName");
            return View();
        }

        // POST: Admin/CUSTOMERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUser,IdRole,FullName,Email,Username,Password,Address,Phone,DayCreate")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                var user = db.USERs.Where(p => p.FullName.ToLower() == uSER.FullName.ToLower() || p.Email.ToLower() == uSER.Email.ToLower()
   || p.Phone == uSER.Phone).FirstOrDefault();
                if (user != null)
                {
                    ViewBag.IdRole = new SelectList(listRole, "IdRole", "RoleName", uSER.IdRole);
                    ViewBag.Error = "Người dùng đã tồn tại";
                    return View(uSER);
                }
                db.USERs.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRole = new SelectList(listRole, "IdRole", "RoleName", uSER.IdRole);
            return View(uSER);
        }

        // GET: Admin/CUSTOMERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRole = new SelectList(listRole, "IdRole", "RoleName", uSER.IdRole);
            return View(uSER);
        }

        // POST: Admin/CUSTOMERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUser,IdRole,FullName,Email,Username,Password,Address,Phone,DayCreate")] USER uSER)
        {
            if (ModelState.IsValid)
            {

                var user = db.USERs.Where(p => (p.FullName.ToLower() == uSER.FullName.ToLower() || p.Email.ToLower() == uSER.Email.ToLower()
               || p.Phone == uSER.Phone) && p.IdUser != uSER.IdUser).SingleOrDefault();
                if (user != null)
                {
                    ViewBag.IdRole = new SelectList(listRole, "IdRole", "RoleName", uSER.IdRole);
                    ViewBag.Error = "Người dùng đã tồn tại";
                    return View(uSER);
                }
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRole = new SelectList(listRole, "IdRole", "RoleName", uSER.IdRole);
            return View(uSER);
        }

        // GET: Admin/CUSTOMERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Admin/CUSTOMERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USERs.Find(id);
            db.USERs.Remove(uSER);
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
