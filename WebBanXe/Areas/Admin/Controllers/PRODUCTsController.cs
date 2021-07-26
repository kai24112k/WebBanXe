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
    public class PRODUCTsController : BaseController
    {
        private DBBanXeEntities db = new DBBanXeEntities();

        // GET: Admin/PRODUCTs
        public ActionResult Index()
        {
            ViewBag.BRAND = db.BRANDs.ToList();
            var pRODUCTs = db.PRODUCTs.Include(p => p.BRAND).Include(p => p.TYPECAR);
            foreach(var item in pRODUCTs)
            {
                if (item.Description.Length > 100)
                {
                    item.Description.Substring(0, 100);
                }
            }
            return View(pRODUCTs.ToList());
        }

        // GET: Admin/PRODUCTs/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand");
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PRODUCT pRODUCT, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var product = db.PRODUCTs.Where(p => p.NameProduct.ToLower() == pRODUCT.NameProduct.ToLower()).SingleOrDefault();
               if (product!=null || fileUpload == null)
                {
                    ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
                    ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
                    ViewBag.Error = "Sản phẩm đã tồn tại";
                    return View(pRODUCT);
                }
                db.PRODUCTs.Add(pRODUCT);
                if (fileUpload != null)
                {
                    var extension = Path.GetExtension(fileUpload.FileName);
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(pRODUCT.NameProduct.ToLower()) + "-anh-bia" + extension);
                    var path = Path.Combine(Server.MapPath("~/Public/img/products/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG_PRODUCT();
                    img.AltImg = fileName;
                    img.LinkImg = "/Public/img/products/" + fileName;
                    img.IdProduct = pRODUCT.IdProduct;
                    db.IMG_PRODUCT.Add(img);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
            return View(pRODUCT);


        }

        // GET: Admin/PRODUCTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PRODUCT pRODUCT, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                var product = db.PRODUCTs.Where(p => p.NameProduct.ToLower() == pRODUCT.NameProduct.ToLower() && pRODUCT.IdProduct != p.IdProduct).SingleOrDefault();
                if (product != null || fileUpload == null)
                {
                    ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
                    ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
                    ViewBag.Error = "Sản phẩm đã tồn tại và vui lòng nhập đầy đủ thông tin";
                    return View(pRODUCT);
                }
                if (fileUpload != null)
                {                 
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(pRODUCT.NameProduct.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/products/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG_PRODUCT();
                    img.AltImg = fileName;
                    img.LinkImg = "/Public/img/products/" + fileName;
                    img.IdProduct = pRODUCT.IdProduct;
                    var imgold = db.IMG_PRODUCT.Where(x => x.LinkImg == img.LinkImg).SingleOrDefault();
                    db.IMG_PRODUCT.Remove(imgold);
                    db.IMG_PRODUCT.Add(img);
                }
               
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBrand = new SelectList(db.BRANDs, "IdBrand", "NameBrand", pRODUCT.IdBrand);
            ViewBag.IdType = new SelectList(db.TYPECARs, "IdType", "NameType", pRODUCT.IdType);
            return View(pRODUCT);
        }
        // POST: Admin/PRODUCTs/Delete/5
        public ActionResult Delete(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
            var listImg = db.IMG_PRODUCT.Where(p => p.IdProduct == id).ToList();
            foreach (var item in listImg)
            {
                db.IMG_PRODUCT.Remove(item);
            }
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
