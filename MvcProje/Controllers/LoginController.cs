using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProje.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        AdminManager adm = new AdminManager(new EfAdminDal());
        WriterLoginManager wlm = new WriterLoginManager(new EfWriterDal());

        [HttpGet] 
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Index(Admin admin)
        {
            var adminUser = adm.GetList().FirstOrDefault(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword == admin.AdminPassword);
            
            if (adminUser != null)
            {
                FormsAuthentication.SetAuthCookie(adminUser.AdminUserName, false);
                Session["AdminUserName"] = adminUser.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                TempData["errorMessage"] = "Hatalı Giriş";
                return RedirectToAction("Index");
            }
        }

        [HttpGet] 
        public ActionResult WriterLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterLogin(Writer writer)
        {
            //var writerUser = wm.GetList().FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);

            var writerUser = wlm.GetWriter(writer.WriterMail,writer.WriterPassword);

            if (writerUser != null)
            {
                FormsAuthentication.SetAuthCookie(writerUser.WriterMail, false);
                Session["WriterMail"] = writerUser.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                TempData["errorMessage"] = "Hatalı Giriş";
                return RedirectToAction("WriterLogin");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}