using Model.Dao;
using OnlineShopSystem.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Contact
        public ActionResult Index()
        {
            var iplContact = new ContactDao();
            var model = iplContact.GetToShow();
            return View(model);
        }
    }
}