using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Helpers.Name;
using WebBanXe.Model;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class BRANDsController : BaseController
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/BRANDs
        public ActionResult Index()
        {
            return View(db.BRANDs.ToList());
        }

        // GET: Admin/BRANDs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRANDs.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        // GET: Admin/BRANDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BRANDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BRAND bRAND , HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var brand = db.BRANDs.Where(p => p.NameBrand.ToLower() == bRAND.NameBrand.ToLower()).SingleOrDefault();
                if (brand != null)
                {
                    ViewBag.Error = "Hãng xe đã tồn tại";
                    return View(bRAND);
                }
                if (fileUpload != null)
                {
                    var extension = Path.GetExtension(fileUpload.FileName);
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(bRAND.NameBrand.ToLower()) + "-anh-bia" + extension);
                    var path = Path.Combine(Server.MapPath("~/Public/img/brands/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    bRAND.ImgBrand = "/Public/img/brands/" + fileName;

                    db.BRANDs.Add(bRAND);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bRAND);
        }

        // GET: Admin/BRANDs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BRAND bRAND = db.BRANDs.Find(id);
            if (bRAND == null)
            {
                return HttpNotFound();
            }
            return View(bRAND);
        }

        // POST: Admin/BRANDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(BRAND bRAND, HttpPostedFileBase fileUpload)
        {   
            if (ModelState.IsValid)
            {
                var brand = db.BRANDs.Where(p => p.NameBrand.ToLower() == bRAND.NameBrand.ToLower() && p.IdBrand != bRAND.IdBrand).SingleOrDefault();
                if (brand != null)
                {
                    ViewBag.Error = "Hãng xe đã tồn tại";
                    return View(bRAND);
                }
                if (fileUpload != null)
                {
               
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(bRAND.NameBrand.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/brands/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                   
                    bRAND.ImgBrand = "/Public/img/brands/" + fileName;
                    UpdateModel(bRAND);
                  
                    db.SaveChanges();
                }
                db.Entry(bRAND).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bRAND);
        }

        
        public ActionResult Delete(int id)
        {
            BRAND bRAND = db.BRANDs.Find(id);
            db.BRANDs.Remove(bRAND);
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
