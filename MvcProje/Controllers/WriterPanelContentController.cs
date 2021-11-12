using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        // GET: WriterPanelContent
        public ActionResult MyContent(string p)
        {
            Context c = new Context();
            p = (string)Session["WriterMail"];
            var writerIDInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var contentValues = cm.GetListByWriter(writerIDInfo);
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.data = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content content)
        {
            string p = (string)Session["WriterMail"];
            var writerIDInfo = wm.GetList().Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();

            content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            content.WriterID = writerIDInfo;
            content.ContentStatus = true;
            cm.ContentAddBL(content);
            return RedirectToAction("MyContent");
        }

        public ActionResult ToDoList()
        {
            return View();
        }
    }
}