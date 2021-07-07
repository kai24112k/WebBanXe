using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        DBBanXeEntities db = new DBBanXeEntities();

        public ActionResult BlogHome()
        {
            List<BLOG> listBlog = new List<BLOG>();
            listBlog = db.BLOGs.ToList();
            return View(listBlog);
        }
        // GET: Product
        public ActionResult CategoryBlog()
        {
            List<CATEGORY_BLOG> category_Blog = new List<CATEGORY_BLOG>();
            category_Blog = db.CATEGORY_BLOG.ToList();
            return View(category_Blog);
        }

        public ActionResult Detail(int id)
        {
            BLOG blog = new BLOG();
            blog = db.BLOGs.Where(p => p.IdBlog == id).SingleOrDefault();
            return View(blog);
        }
    }
}