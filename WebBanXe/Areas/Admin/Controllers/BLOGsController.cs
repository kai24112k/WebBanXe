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
            if (Session["userID"] != null)
            {
                
            }
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
      
        public ActionResult Create(BLOG bLOG, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                
                bLOG.DateCreate = DateTime.Now;
                bLOG.IdUser = int.Parse( Session["userID"].ToString());
                db.BLOGs.Add(bLOG);
                if (fileUpload != null)
                {
                    
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(bLOG.Title.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/blogs/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG_BLOG();
                    img.AltImg = fileName;
                    img.LinkImg = "/Public/img/blogs/"+ fileName;
                    img.IdBlog = bLOG.IdBlog;                   
                    db.IMG_BLOG.Add(img);
                }
                var blog = db.BLOGs.Where(p => p.Title.ToLower() == bLOG.Title.ToLower()).SingleOrDefault();
                if (blog != null)
                {
                    ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate", bLOG.IdCate);
                    ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", bLOG.IdUser);
                    ViewBag.Error = "Tiêu đề đã tồn tại";
                    return View(bLOG);
                }
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
        public ActionResult Edit (BLOG bLOG, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {

                bLOG.DateCreate = DateTime.Now;
                bLOG.IdUser = int.Parse(Session["userID"].ToString());

                if (fileUpload != null)
                {             
                    if (!fileUpload.ContentType.Contains("image")) throw new Exception("File hình không hợp lệ");
                    if (fileUpload.ContentLength > 3 * 1024 * 1024) throw new Exception("Hình ảnh vượt quá 3Mb");
                    var fileName = Path.GetFileName(RemoveVietnamese.convertToSlug(bLOG.Title.ToLower()) + "-anh-bia.png");
                    var path = Path.Combine(Server.MapPath("~/Public/img/blogs/"), fileName);
                    try
                    {
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                    catch
                    {

                    }
                    fileUpload.SaveAs(path);
                    var img = new IMG_BLOG();
                    img.AltImg = fileName;
                    img.LinkImg = "/Public/img/blogs/" + fileName;
                    img.IdBlog = bLOG.IdBlog;
                    var imgageold = db.IMG_BLOG.Where(x => x.LinkImg == img.LinkImg).SingleOrDefault();
                    if(imgageold != null)
                    {
                        db.IMG_BLOG.Remove(imgageold);
                    }
                
                    db.IMG_BLOG.Add(img);
                   
                 
                var blog = db.BLOGs.Where(p => p.Title.ToLower() == bLOG.Title.ToLower()).SingleOrDefault();
                if (blog != null)
                {
                    ViewBag.IdCate = new SelectList(db.CATEGORY_BLOG, "IdCate", "NameCate", bLOG.IdCate);
                    ViewBag.IdUser = new SelectList(db.USERs, "IdUser", "FullName", bLOG.IdUser);
                    ViewBag.Error = "Tiêu đề đã tồn tại";
                    return View(bLOG);
                }
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
            var listImg = db.IMG_BLOG.Where(p => p.IdBlog == id).ToList();
            foreach (var item in listImg)
            {
                db.IMG_BLOG.Remove(item);
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
