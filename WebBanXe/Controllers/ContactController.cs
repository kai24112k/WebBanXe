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
        [HttpPost]
        public ActionResult ContactPage(FormCollection contactForm)
        {
            CONTACT contact = new CONTACT();


           
            var title = contactForm["title"];
            var email = contactForm["email"];
            var content = contactForm["content"];
           
            var detail = contactForm["noidung"];
            contact.Title = title;
            contact.Email = email;
            contact.Content = content;
           
            db.CONTACTs.Add(contact);
            db.SaveChanges();
            ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";
            return View();
        }

        public ActionResult ContactPage()
        {
            return View();
           
        }
    }
}