﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Helpers.MD5;
using WebBanXe.Model;
using static WebBanXe.Helpers.Security.Authen;

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
            var password = MD5Helper.MD5Hash(frm["password"]);

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
                USER user = db.USERs.SingleOrDefault(u => u.Username == userName && u.Password == password && u.IdRole != 3 );
                if (user != null)
                {
                 
                    Session["UserAdmin"] = user.IdUser;
                    Session["fullName"] = user.FullName;
                    Session["Role"] = user.USER_ROLE.RoleName;
                    return RedirectToAction("Index", "PRODUCTs");
                }
                else
                    ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu";
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["fullName"] = null;
            return RedirectToAction("Login", "Auth");

        }
     
    }
}