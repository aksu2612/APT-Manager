using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptManager.Models;
using System.Data;
using System.Net.Mail;
using System.Net;
namespace AptManager.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult denemedatalist()
        {
            return View();
        }
        public ActionResult Create(Blok form)
        {
            if (form.Id!=null&&form.SiteId!=null&&form.BlokAdi!=null){
            db.Blok.Add(form);
            db.SaveChanges();
        }
            RedirectToAction("/Admin/Site");
            return View();
        }
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        AptYonetimEntities db = new AptYonetimEntities();
        public ActionResult SiteKayit(Site form)
        {
            db.Site.Add(form);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
          [HttpPost]
          public ActionResult asdasd(Site data)
        {
            if (ModelState.IsValid)
            {  
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return Json("");
            }
            return View(data);
        }
      
          public ActionResult GetSiteById(int siteid)
          {
              var result = db.Site.Where(w => w.Id == siteid).FirstOrDefault();
              return Json(result, JsonRequestBehavior.AllowGet);
          }
        public ActionResult Sitesil(int siteid)
        {
            var sil = db.Site.Where(w => w.Id == siteid).FirstOrDefault();
            db.Site.Remove(sil);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public List<Kullanici> Getonaysizkullanic() {
            var list = db.Kullanici.Where(e => e.Onay == false).ToList();
            Session["onaybekleyen"] = list.Count;
            Session["Onaylist"] = list;
            return list;
        
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["KullaniciAdi"] == null)
            {
                filterContext.Result = Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult Site()
        {
            return View();
        }
        public ActionResult Harita()
        {
            return View();
        }
        public ActionResult BilgileriGuncelle()
        {
            return View();
        }
        public ActionResult KisiBildirim()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetKisibildirim()
        {
            var liste = db.Kullanici.Where(q => q.Onay == false).ToList();
            Session["onaybekleyen"] = liste.Count;
            return Json(liste, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKullaniciSite(int a)
        {
            var list = db.Site.Where(w => w.Id == a).FirstOrDefault();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBlokBYID(int id)
        {
            var result = db.Kullanici.Where(w => w.Id == id).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



         [HttpPost]
        public ActionResult KisiUpdate(int id)
        {
            var a = db.Kullanici.Where(w => w.Id == id).FirstOrDefault();
            a.Onay = true;
            db.SaveChanges();
            string Email = a.Email;
            string konu = "Girişiniz Tarafımızda Onaylanmıştır Giriş Yapabilirsiniz";
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("aksu2612@gmail.com");
            ePosta.To.Add(a.Email);
            ePosta.Subject = "Onay Bilgilendirme";
            ePosta.Body = konu;
            ePosta.Attachments.Add(new Attachment(@"C:\hosts.txt"));
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("aksu2612@gmail.com", "4762452578");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(ePosta);
            return Json(Email, JsonRequestBehavior.AllowGet);
        }
         public ActionResult GetkisiEposta(int id)
         {
             var a = db.Kullanici.Where(r => r.Id == id).FirstOrDefault();
             var b = a.Email;
             return Json(b, JsonRequestBehavior.AllowGet);
         }
    }
}
