
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AptManager.Models;
using System.Web.Security;

namespace AptManager.Controllers
{
    public class HomeController : Controller
    {
        AptYonetimEntities db = new AptYonetimEntities();


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

            List<Kullanici> model = new List<Kullanici>();
            model = db.Kullanici.ToList();
            return View(model);
        }
         


    }
}
