using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{
    public class CustomerController : Controller
    {
        DBBanXeEntities db = new DBBanXeEntities();
        // GET: Customer
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
                USER user = db.USERs.SingleOrDefault(u => u.Username == userName && u.Password == password && u.IdRole == 3);
                if (user != null)
                {
                    Session["userID"] = user.IdUser;
                    Session["fullName"] = user.FullName;
                    ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Đăng nhập thành công</div>";
                    return RedirectToAction("Index", "Home");
                }
                else
            
                    ViewBag.ThongBao = "Đăng nhập không thành công";
            }
            return View();

        }
        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection collection)
        {
            USER user = new USER();
            user.FullName = collection["HoTen"];
            user.Email = collection["Email"];
            user.Username = collection["TaiKhoan"];
            user.Password = collection["MatKhau"];
            user.Address = collection["DiaChi"];
            user.Phone = collection["Sdt"];
            user.DayCreate = DateTime.Now;
           
            if (String.IsNullOrEmpty(user.FullName))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            else if (String.IsNullOrEmpty(user.Email))
            {
                ViewData["Loi2"] = "Phải nhập email";
            }
            else if (String.IsNullOrEmpty(user.Username))
            {
                ViewData["Loi3"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                ViewData["Loi4"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(user.Address))
            {
                ViewData["Loi5"] = "Địa chỉ không được để trống";
            }
            else if (String.IsNullOrEmpty(user.Phone))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại";
            }
            
            else
            {
                if (collection["XacNhanMatKhau"] == collection["MatKhau"])
                {
                    user.IdRole = 3;
                    db.USERs.Add(user);
                    db.SaveChanges();
                    ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";
                    return RedirectToAction("Login", "Customer");

                }
                else
                {
                    ViewData["Loi4"] = " Vui lòng xác nhận lại mật khẩu";
                }

                return this.SignUp();
            }
            return this.View();
        }

       
    }
}