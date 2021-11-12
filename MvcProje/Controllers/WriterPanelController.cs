using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace MvcProje.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator validationRules = new WriterValidator();

        public ActionResult WriterProfile()
        {
            string p = (string)Session["WriterMail"];
            var writerIdInfo = wm.GetList().Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writerValue = wm.GetByID(writerIdInfo);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading","WriterPanel");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
            var writerIdInfo = wm.GetList().Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var values = hm.GetListByWriter(writerIdInfo);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();

            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            string writerMailInfo = (string)Session["WriterMail"];
            var writerIdInfo = wm.GetList().Where(x => x.WriterMail == writerMailInfo).Select(y => y.WriterID).FirstOrDefault();
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.HeadingStatus = true;
            heading.WriterID = writerIdInfo;
            hm.HeadingAddBL(heading);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem 
                                                  { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();

            ViewBag.vlc = valueCategory;

            var headingValue = hm.GetByID(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            hm.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }


        public ActionResult DeleteHeading(int id)
        {
            var headingValue = hm.GetByID(id);
            if (headingValue.HeadingStatus)
                headingValue.HeadingStatus = false;
            else
                headingValue.HeadingStatus = true;

            hm.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int? p)
        {
            var headings = hm.GetList().ToPagedList(p ?? 1,10);
            return View(headings);
        }
    }
}