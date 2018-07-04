using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
        {
            ViewBag.searchString = searchString;
            var iplCategory = new CategoryDao();
            var list = iplCategory.ListAll(searchString, page, pageSize);
            return View(list);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var iplCategory = new CategoryDao();
            var model = iplCategory.FindByID(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            if (ModelState.IsValid)
            {
                var iplCategory = new CategoryDao();
                var res = iplCategory.Update(cate);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat khong thanh cong!");
                }
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var iplCategory = new CategoryDao();
                var res = iplCategory.Insert(category);
                if (res>0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Them moi khong thanh cong");
                }

            }
            return View();
            
        }
    }
}