using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptManager.Models;
using System.Web.Security;

namespace AptMenager.Controllers
{
    public class LoginController : Controller
    {
        AptYonetimEntities db = new AptYonetimEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetBlok(int id){
            var liste = db.Blok.Where(w=>w.SiteId==id).ToList();
             return Json(liste, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSite()
        {
            var liste = db.Site.ToList();
            return Json(liste, JsonRequestBehavior.AllowGet);
        }
         
        [HttpPost]
        public ActionResult LoginKontrol(string kullaniciAdi, string sifre)
        {
            var listeeee = db.Kullanici.Where(e => e.Onay == false).ToList();
            Session["onaybekleyen"] = listeeee.Count;

        
        

            var list1 = db.Admin.Where(w => w.AdminKullaniciadi == kullaniciAdi && w.AdminParola == sifre ).ToList();
            if (list1.Count == 1) 
            {
                Session["KullaniciAdi"] = list1[0].Ad;
                return RedirectToAction("Index","Admin");
            }
            else
            {
                var list2 = db.Kullanici.Where(q => q.Email == kullaniciAdi && q.Sifre == sifre).ToList();
                if (list2.Count == 1)
                {
                    if (list2[0].Onay == true)
                    {
                        Session["KullaniciAdi"] = list2[0].Adi;
                        return RedirectToAction("Index", "KullaniciHome");
                    }
                    else
                    {
                        return RedirectToAction("Onnayyok", "KullaniciHome");
                    }
                }
                else
                {
                    Response.Write("<script>alert('asdasd','asdasd')</script>");
                
                    return RedirectToAction("Index", "Login");
                }
              
            }
           
     }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login"); // RedirectToAction("ActionResult", "Controller"); // 
        }

    }
}
