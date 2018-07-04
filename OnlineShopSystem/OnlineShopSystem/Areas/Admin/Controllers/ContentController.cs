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
    public class ContentController : AdminBaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString = null, int page = 1, int pageSize=2)
        {
            var dao = new ContentDao();
            var list = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(list);
        }
        [HttpGet]
        public ActionResult Detail(long id)
        {
            var dao = new ContentDao();
            var model = dao.FindByID(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        

        public void SetViewBag(long? seletedID = null)
        {
            var CateDao = new CategoryDao();
            ViewBag.CategoryId = new SelectList(CateDao.ListAll(), "ID", "Name", seletedID);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create( Content content)
        {
            if (ModelState.IsValid)
            {
                var _userLogin = new UserLogin();
                _userLogin = (UserLogin)HttpContext.Session["USER_SESSION"];
                var dao = new ContentDao();
                var res = dao.Insert(content, _userLogin.ID);
                if (res)
                {
                    SetViewBag();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Them moi that bai");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui long kiem tra nhap lieu");
            }
            SetViewBag();
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(long ID)
        {
            var dao = new ContentDao();
            var content = dao.FindByID(ID);
            SetViewBag(content.ID);
            return View(content);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Content content)
        {
            var _userLogin = new UserLogin();
            _userLogin = (UserLogin)HttpContext.Session["USER_SESSION"];
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                var res = dao.Update(content, _userLogin.ID);
                if (res)
                {
                    SetViewBag(content.CategoryID);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật that bai");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui long kiem tra nhap lieu");
            }
            SetViewBag();
            return View("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}