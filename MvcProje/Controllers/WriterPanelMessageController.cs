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

namespace MvcProje.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());

        public ActionResult Inbox(int ? p)
        {
            var messageList = mm.GetListInbox(Session["WriterMail"].ToString()).ToPagedList(p ?? 1,10);
            return View(messageList);
        }

        public ActionResult Sendbox(int ? p)
        {
            var messageList = mm.GetListSendBox(Session["WriterMail"].ToString()).ToPagedList(p ?? 1,10);
            return View(messageList);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
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

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            message.SenderMail = Session["WriterMail"].ToString();
            message.MassageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            mm.MessageAddBL(message);
            return RedirectToAction("Sendbox");
        }
    }
}