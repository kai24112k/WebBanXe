using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class InfoController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            DBBanXeEntities db = new DBBanXeEntities();
            var info = db.USERs.Find(Session["UserAdmin"]);
            return View(info);
        }
    }
}