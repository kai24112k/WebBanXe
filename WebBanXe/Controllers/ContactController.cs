using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{
    public class ContactController : Controller
    {
        DBBanXeEntities db = new DBBanXeEntities();
        // GET: Contact
        [Route("lien-he")]
        [HttpPost]
        public ActionResult ContactPage(USER user,FormCollection contactForm)
        {
            CONTACT contact = new CONTACT();
            var title = contactForm["title"];
            var email = contactForm["email"];
            var content = contactForm["content"];
            contact.Title = title;
            contact.Email = email;
            contact.Content = content;
            contact.Status = false;
            if (user != null)
            {
                contact.Email = user.Email;
                contact.IdUser = user.IdUser;
            }
            db.CONTACTs.Add(contact);
            db.SaveChanges();
            ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";
            return View(user);
        }

        [Route("lien-he")]
        public ActionResult ContactPage()
        {
            ViewBag.Title = "Trang liên hệ";
            if (Session["userID"] != null)
            {
                USER user = new USER();
                user = db.USERs.Find(Session["userID"]);
                return View(user);
            }
            return View();
           
        }
    }
}