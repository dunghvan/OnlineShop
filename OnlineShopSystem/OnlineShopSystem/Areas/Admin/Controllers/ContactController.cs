using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Areas.Admin.Controllers
{
    public class ContactController : AdminBaseController
    {
        // GET: Admin/Contact
        public ActionResult Index()
        {
            var iplContac = new ContactDao();
            var model = iplContac.ListAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var iplContact = new ContactDao();
                    iplContact.Insert(model);
                    SetAlert("Them moi thanh cong", "success");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                SetAlert("Them moi that bai", "warning");
                return View();
            }
            return View();
        }
        public bool ChangeStatus(int id)
        {
            return new ContactDao().ChangeStatus(id);
        }
      
    }
}