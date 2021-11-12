using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProje.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        WriterLoginManager wlm = new WriterLoginManager(new EfWriterDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        
        public ActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Writer writer)
        {
            var writerUser = wlm.GetWriter(writer.WriterMail, writer.WriterPassword);

            if (writerUser != null)
            {
                FormsAuthentication.SetAuthCookie(writerUser.WriterMail, false);
                Session["WriterMail"] = writerUser.WriterMail;
                TempData["Writer"] = writerUser.WriterName +  " " + writerUser.WriterSurName;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                TempData["errorMessage"] = "Hatalı Giriş";
                return RedirectToAction("WriterLogin");
            }
        }
    }
}