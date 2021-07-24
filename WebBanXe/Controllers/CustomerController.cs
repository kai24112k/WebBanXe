using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Helpers;
using WebBanXe.Helpers.Facebook;
using WebBanXe.Helpers.MD5;
using WebBanXe.Model;
using static WebBanXe.Helpers.Security.Authen;

namespace WebBanXe.Controllers
{
    public class CustomerController : Controller
    {
        DBBanXeEntities db = new DBBanXeEntities();
        // GET: Customer
        [Route("dang-nhap")]
        public ActionResult Login()
        {
            Session.Remove("returnURL");
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"].ToString();
            return View();
        }

        [Route("dang-nhap")]
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            var userName = frm["userName"];
            var password = MD5Helper.MD5Hash(frm["password"]);

            if (String.IsNullOrEmpty(userName))
            {
                ViewBag.Error = "Phải nhập tên đăng nhập";

            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Phải nhập mật khẩu";
            }
            else
            {
                USER user = db.USERs.SingleOrDefault(u => u.Username == userName && u.Password == password);
                if (user != null)
                {
                    SaveSession(user);

                    if (!string.IsNullOrEmpty(Request.QueryString["returnURL"]))
                    {
                        return Redirect(Request.QueryString["returnURL"].ToString());
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Đăng nhập không thành công";
                }
            }
            return View();

        }

        /// <summary>
        /// Login bằng facebook
        /// </summary>
        /// <returns></returns>
        [Route("dang-nhap-facebook")]
        public ActionResult LoginFacebook(string returnURL)
        {
            var uriBuilder = new UriBuilder(Request.Url);
            uriBuilder.Query = null;
            uriBuilder.Fragment = null;
            uriBuilder.Path = Url.Action("FacebookCallback");
            Session["returnURL"] = returnURL;

            var facebookLoginURL = FacebookHelper.GetLinkLogin(uriBuilder.Uri.AbsoluteUri);
            return Redirect(facebookLoginURL);
        }

        [Route("dang-nhap-facebook-callback")]
        public ActionResult FacebookCallback(string code)
        {
            var uriBuilder = new UriBuilder(Request.Url);
            uriBuilder.Query = null;
            uriBuilder.Fragment = null;
            uriBuilder.Path = Url.Action("FacebookCallback");

            var facebookUser = FacebookHelper.GetFacebookCallback(uriBuilder.Uri.AbsoluteUri, code);
            if (facebookUser is null)
            {
                TempData["Error"] = "Đăng nhập facebook thất bại, vui lòng thử lại sau";
                return RedirectToAction("Login");
            }

            // Login facebook thành công
            // Tìm user đó tồn tại chưa
            using (DBBanXeEntities db = new DBBanXeEntities())
            {
                // Tìm theo email của fb trong db của mình
                var emailFacebook = facebookUser.Email.ToLower();
                var userDB = db.USERs
                    .Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.ToLower().Equals(emailFacebook) && x.IdRole == 3)
                    .SingleOrDefault();

                // Trường hợp user có
                if (userDB != null)
                {
                    SaveSession(userDB);
                }
                else
                {
                    // Ko tìm thấy
                    USER newUser = new USER();

                    newUser.FullName = facebookUser.Name;
                    newUser.Email = facebookUser.Email;
                    newUser.IdRole = 3;
                    newUser.Username = facebookUser.Email;
                    newUser.Password = string.Empty;
                    newUser.Phone = string.Empty;
                    newUser.Address = string.Empty;
                    newUser.DayCreate = DateTime.Now;
                    db.USERs.Add(newUser);
                    db.SaveChanges();

                    SaveSession(newUser);
                }

                if (Session["returnURL"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["returnURL"].ToString()))
                    {
                        return Redirect(Session["returnURL"].ToString());
                    }
                }

                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Save lại session khi login thành công
        /// </summary>
        /// <param name="data"></param>
        private void SaveSession(USER data)
        {
            Session["userID"] = data.IdUser;
            Session["fullName"] = data.FullName;
        }

        public ActionResult Logout()
        {
            Session["userID"] = null;
            Session["fullName"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("dang-ky")]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("dang-ky")]
        public ActionResult SignUp(FormCollection collection)
        {
            USER user = new USER();
            user.FullName = collection["HoTen"];
            user.Email = collection["Email"];
            user.Username = collection["TaiKhoan"];
            user.Password = MD5Helper.MD5Hash(collection["MatKhau"]);
            user.Address = collection["DiaChi"];
            user.Phone = collection["Sdt"];
            user.DayCreate = DateTime.Now;
            if (String.IsNullOrEmpty(user.Address))
            {
                user.Address = string.Empty;
            }
            if (String.IsNullOrEmpty(user.FullName))
            {
                ViewBag.Error = "Họ tên không được để trống";
            }
            else if (!ValidateHelper.IsValidEmail(user.Email))
            {
                ViewBag.Error = "Sai định đạng Email";
            }
            else if (!ValidateHelper.ValidateUsername(user.Username))
            {
                ViewBag.Error = "Tài khoản không được chứa ký tự đặc biệt";
            }
            else if (!ValidateHelper.ValidatePassword(user.Password))
            {
                ViewBag.Error = "Mật khẩu không được chứa ký tự đặc biệt";
            }
            else if (!ValidateHelper.IsPhone(user.Phone))
            {
                ViewBag.Error = "Vui lòng nhập số điện thoại";
            }
            else
            {
                if (collection["XacNhanMatKhau"] == collection["MatKhau"])
                {
                    using (DBBanXeEntities db = new DBBanXeEntities())
                    {
                        var userDB = db.USERs.Where(u => u.Email == user.Email || u.Username == user.Username || u.Phone == user.Phone).FirstOrDefault();
                        if (userDB != null)
                        {
                            ViewBag.Error = "Đã có tài khoản, vui lòng kiểm tra lại thông tin!";
                            return View(collection);
                        }
                        else
                        {
                            user.IdRole = 3;
                            db.USERs.Add(user);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Login", "Customer");

                }
                else
                {
                    ViewBag.Error = " Vui lòng xác nhận lại mật khẩu";
                }
                return View(collection);
            }
            return View(collection);
        }

        [Auth]
        public ActionResult Info()
        {
            var id = Session["userID"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRole = new SelectList(db.USER_ROLE, "IdRole", "RoleName", uSER.IdRole);
            return View(uSER);
        }

        [HttpPost]
        public ActionResult Info(USER user)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                if (user.Password != MD5Helper.MD5Hash(Request["oldpassword"]))
                {
                    ViewBag.Error = "Mật khẩu cũ không đúng!";
                    return View(user);
                }
            }
            if(Request["passwordnew"] != Request["passwordconfirm"])
            {
                ViewBag.Error = "Mật khẩu xác thực không đúng!";
                return View(user);
            }
            var password = MD5Helper.MD5Hash(Request["passwordnew"].ToString());
            user.Password = password;
            if (ModelState.IsValid)
            {
                user.IdRole = int.Parse(Session["userID"].ToString());
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [Auth]
        public ActionResult HistoryOrder()
        {
            var id = 0;
            if (Session["userID"] != null)
            {
                id = int.Parse(Session["userID"].ToString());
            }

            if(id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = new List<ORDER>();
            order  = db.ORDERs.Where(o => o.IdUser == id).ToList();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}