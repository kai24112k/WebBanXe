using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;
using System.Dynamic;

namespace WebBanXe.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        DBBanXeEntities db = new DBBanXeEntities();
       
        private List<BLOG> GetBlog (int count)
        {
            return db.BLOGs.OrderByDescending(a => a.DateCreate).Take(count).ToList();
        }
        public ActionResult BlogHome(int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var NewBlog = GetBlog(5);

            //List<BLOG> listBlog = new List<BLOG>();
            //listBlog = db.BLOGs.ToList();
            return View(NewBlog.ToPagedList(pageNum, pageSize));
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