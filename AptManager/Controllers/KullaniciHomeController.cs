using AptManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AptManager.Controllers
{
    public class KullaniciHomeController : Controller
    {
        //
        // GET: /KullaniciHome/



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["KullaniciAdi"] == null)
            {
                filterContext.Result = Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Odemeislemleri()
        {   
            return View();
        }
        public ActionResult Onnayyok()
        {
            return View();
        }
        public ActionResult BilgileriGuncelle()
        {
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Harita()
        {
            return View();
        }
        AptYonetimEntities db = new AptYonetimEntities();
        public ActionResult MailGonder(String a)
        {
            string konu = "Girişiniz Tarafımızda Onaylanmıştır Giriş Yapabilirsiniz";
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("aksu2612@gmail.com");
            ePosta.To.Add(a);
            ePosta.Subject = "Onay Bilgilendirme";
            ePosta.Body = konu;
            ePosta.Attachments.Add(new Attachment(@"C:\hosts.txt"));
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("aksu2612@gmail.com", "4762452578");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(ePosta);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
