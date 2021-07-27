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
       
        public ActionResult ContactPage()
        {
            return View();

        }

        [Route("lien-he")]
        [HttpPost]
        public ActionResult ContactPage(FormCollection contactForm)
        {
            CONTACT contact = new CONTACT();
            var title = contactForm["title"];
            var email = contactForm["email"];
            var content = contactForm["content"];
            contact.Title = title;
            contact.Email = email;
            contact.Content = content;
            var user = new USER();
            if (Session["userID"]!= null)
            {
                contact.IdUser = int.Parse(Session["userID"].ToString());
                user = db.USERs.Where(x => x.IdUser == contact.IdUser).SingleOrDefault();
                if (user != null)
                {
                    contact.Email = user.Email;
                    contact.IdUser = user.IdUser;
                }
            }
            else
            {
                contact.IdUser = null;
            }
            
            contact.Status = false;
            db.CONTACTs.Add(contact);
            db.SaveChanges();

            ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";

            return View(contact);
        }
       
    }
}