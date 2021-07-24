using PagedList;
using PagedList.Mvc;
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

        private List<BLOG> GetBlog(List<BLOG> blogs)
        {
            return blogs.OrderByDescending(a => a.IdBlog).ToList();
        }
        [Route("tap-chi-xe")]
        public ActionResult BlogHome(int? IdCate, int? page, int? IdUser)
        {
                int pageSize = 4;
                int pageNum = (page ?? 1);
            var NewBlog = new List<BLOG>();

            List<BLOG> listBlog = new List<BLOG>();
            listBlog = db.BLOGs.ToList();
            ViewBag.FULLNAME = db.USERs.ToList();
            if (IdCate != null || IdUser != null )
            {
                NewBlog = GetBlog(db.BLOGs.Where(p => p.IdCate == IdCate || p.IdUser == IdUser).ToList());
              
            }
            else
            {
                NewBlog = GetBlog(db.BLOGs.ToList());
            }
            return View(NewBlog.ToPagedList(pageNum, pageSize));
        }
       

        // GET: Product
        public ActionResult CategoryBlog()
        {
            List<CATEGORY_BLOG> category_Blog = new List<CATEGORY_BLOG>();
            category_Blog = db.CATEGORY_BLOG.ToList();
            return View(category_Blog);
        }
        [Route("tap-chi-xe/{id}")]
        public ActionResult Detail(int id)
        {
            BLOG blog = new BLOG();
            blog = db.BLOGs.Where(p => p.IdBlog == id).SingleOrDefault();
            
            return View(blog);
        }
        public ActionResult Author(int id)
        {
            List<BLOG> author_Blog = new List<BLOG>();
            author_Blog = db.BLOGs.Where(a => a.IdUser == id).ToList();
            return View(author_Blog);
        }
    }
}