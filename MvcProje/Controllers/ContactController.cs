using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        public ActionResult Index()
        {
            var contactValues = cm.GetList();
            return View(contactValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = cm.GetByID(id);
            return View(contactValues);
        }

        public PartialViewResult MessageListMenu()
        {
            var messageCounts = mm.MessageCount(Session["WriterMail"].ToString());
            ViewBag.receiverMessageCount = messageCounts[0];
            ViewBag.senderMessageCount = messageCounts[1];
            return PartialView();
        }
    }
}