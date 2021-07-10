using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        DBBanXeEntities db = new DBBanXeEntities();
        // GET: Admin/Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            var userName = frm["userName"];
            var password = frm["password"];

            if (String.IsNullOrEmpty(userName))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";

            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                USER user = db.USERs.SingleOrDefault(u => u.Username == userName && u.Password == password && u.IdRole == 2 );
                if (user != null)
                {
                    Session["userID"] = user.IdUser;
                    Session["fullName"] = user.FullName;
                    return RedirectToAction("Index", "PRODUCTS");
                }
                else

                    ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Login", "Auth");

        }
    }
}