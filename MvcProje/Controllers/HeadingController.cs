using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var headingValues = hm.GetList();
            return View(headingValues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList() select new SelectListItem 
            { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();

            List<SelectListItem> valueWriter = (from x in wm.GetList() select new SelectListItem 
            { Text = x.WriterName+" "+x.WriterSurName, Value = x.WriterID.ToString() }).ToList();

            ViewBag.vlw = valueWriter;
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAddBL(heading);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()select 
            new SelectListItem{ Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();

            ViewBag.vlc = valueCategory;

            var headingValue = hm.GetByID(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            hm.HeadingUpdate(heading);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = hm.GetByID(id);
            if (headingValue.HeadingStatus)
                headingValue.HeadingStatus = false;
            else
                headingValue.HeadingStatus = true;

            hm.HeadingDelete(headingValue);
            return RedirectToAction("Index");
        }
    }
}