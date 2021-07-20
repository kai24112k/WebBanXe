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
       
        public ActionResult ContactPage()
        {
            return View();

        }
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
            if(Session["userID"]!= null)
            {
                contact.IdUser = int.Parse(Session["userID"].ToString());
            }
            else
            {
                Session["userID"] = null;
            }
            
            contact.Status = false;
            db.CONTACTs.Add(contact);
            db.SaveChanges();

            ViewBag.Notice = "<div class='alert alert-success text-center text-dark' role='alert'>Gửi liên hệ thành công</div>";

            return View(contact);
        }
       
    }
}