using Model.Dao;
using Model.EF;
using OnlineShopSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Areas.Admin.Controllers
{
    public class SlideController : AdminBaseController
    {
        // GET: Admin/SLide
        public ActionResult Index(string searchString = null, int page = 1, int pageSize = 3)
        {
            var model = new SlideDao();
            var list = model.ListAllPaging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Slide model)
        {
            if (ModelState.IsValid)
            {
                var slide = new SlideDao();
                model.CreateDate = DateTime.Now;
                model.CreateBy = ((UserLogin)HttpContext.Session["USER_SESSION"]).ID;
                slide.Insert(model);
                SetAlert("Them moi thanh cong ", "success");
                return RedirectToAction("Index", "Slide");
            }
            else
            {
                SetAlert("Them moi that bai", "error");
                ModelState.AddModelError("", "Them moi khong thanh cong.");
            }
            return View();
            
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var iplSlide = new SlideDao();
            var res = iplSlide.Delete(id);
            if (res)
            {
                SetAlert("Xoa thanh cong ", "success");
                return RedirectToAction("Index", "Slide");
            }
            else
            {
                SetAlert("Xoa khong thanh cong", "error");
                return  RedirectToAction("Index", "Slide");
            }
        }
    }
}