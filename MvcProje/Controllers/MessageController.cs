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
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        public ActionResult Inbox()
        {
            var messageList = mm.GetListInbox("admin@gmail.com");
            return View(messageList);
        }

        public ActionResult Sendbox()
        {
            var messageList = mm.GetListSendBox("admin@gmail.com");
            return View(messageList);
        }

        public ActionResult GetMessageDetails(int id)
        {
            var contactValues = mm.GetByID(id);
            return View(contactValues);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            message.MassageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            mm.MessageAddBL(message);
            return RedirectToAction("Sendbox");
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            return View(values);
        }
    }
}