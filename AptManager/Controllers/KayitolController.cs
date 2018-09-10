using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptManager.Models;


namespace AptManager.Controllers
{
    public class KayitolController : Controller
    {
        //
        // GET: /Kayitol/
        AptYonetimEntities db = new AptYonetimEntities();

        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(Kullanici form)
        {
           
                    form.KullaniciTipi=2;
                    db.Kullanici.Add(form);
                    db.SaveChanges();
                    return View();
         
      
        }
    }
}
