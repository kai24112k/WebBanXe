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
     
        private List<BLOG> GetBlog (int count)
        {
            return db.BLOGs.OrderByDescending(a => a.DateCreate).Take(count).ToList();
        }
        [Route("tap-chi-xe")]
        public ActionResult BlogHome(int? IdCate/*, int? page*/)
        {
            //int pageSize = 3;
            //int pageNum = (page ?? 1);
            //var NewBlog = GetBlog(5);
            //return View(NewBlog.ToPagedList(pageNum, pageSize));

            List<BLOG> listBlog = new List<BLOG>();
            listBlog = db.BLOGs.ToList();

      
            if (IdCate != null)
            {
                listBlog = db.BLOGs.Where(p => p.IdCate == IdCate).ToList();
            }
            else
            {
                listBlog = db.BLOGs.ToList();
            }
            return View(listBlog);
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
    }
}