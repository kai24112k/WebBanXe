using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebBanXe.Helpers.Name;
using WebBanXe.Model;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class TYPECARsController : BaseController
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/TYPECARs
        public ActionResult Index()
        {
            return View(db.TYPECARs.ToList());
        }

        // GET: Admin/TYPECARs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPECAR tYPECAR = db.TYPECARs.Find(id);
            if (tYPECAR == null)
            {
                return HttpNotFound();
            }
            return View(tYPECAR);
        }

        // GET: Admin/TYPECARs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TYPECARs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdType,NameType,ImgType")] TYPECAR tYPECAR)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TYPECARs.Add(tYPECAR);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tYPECAR);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Create(TYPECAR tYPECAR, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            { 
                 var typecar = db.TYPECARs.Where(p => p.NameType.ToLower() == tYPECAR.NameType.ToLower()).SingleOrDefault();
                if (typecar != null)
                {
                    ViewBag.Error = "Loại xe đã tồn tại";
                    return View(tYPECAR);
                }

                if (fileUpload != null)
                {
                   
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(tYPECAR.NameType.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/typecars/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    tYPECAR.ImgType = "/Public/img/typecars/" + fileName;
                    db.TYPECARs.Add(tYPECAR);
                }
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }         
            return View(tYPECAR);
        }

        // GET: Admin/TYPECARs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TYPECAR tYPECAR = db.TYPECARs.Find(id);
            if (tYPECAR == null)
            {
                return HttpNotFound();
            }
            return View(tYPECAR);
        }

        // POST: Admin/TYPECARs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(TYPECAR tYPECAR, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var typecar = db.TYPECARs.Where(p => p.NameType.ToLower() == tYPECAR.NameType.ToLower() && p.IdType != tYPECAR.IdType).SingleOrDefault();
                if (typecar != null)
                {
                    ViewBag.Error = "Loại xe đã tồn tại";
                    return View(tYPECAR);
                }
                if (fileUpload != null)
                {
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(tYPECAR.NameType.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/typecars/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    tYPECAR.ImgType = "/Public/img/typecars/" + fileName;
                    UpdateModel(tYPECAR);
                    db.SaveChanges();
                    db.Entry(tYPECAR).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                return View(tYPECAR);
            
        }

        // GET: Admin/TYPECARs/Delete/5
       
        public ActionResult Delete(int id)
        {
            TYPECAR tYPECAR = db.TYPECARs.Find(id);
            db.TYPECARs.Remove(tYPECAR);
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
