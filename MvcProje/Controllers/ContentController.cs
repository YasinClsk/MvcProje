using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllContent(string p)
        {
            if(!string.IsNullOrEmpty(p))
            {
                var values = cm.GetListByContentValue(p);
                return View(values.ToList());
            }
            return View(cm.GetList());
            //var values = cm.GetList();

        }

        public ActionResult ContentByHeading(int id)
        {
            var contentValues = cm.GetListByID(id);
            return View(contentValues);
        }
    }
}