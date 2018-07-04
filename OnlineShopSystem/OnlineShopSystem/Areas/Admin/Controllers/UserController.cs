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
    public class UserController : AdminBaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        public ActionResult ViewProfile(int id)
        {
            var dao = new UserDao();
            var model = dao.ViewDetail(id);
            return View(model);
        }
        public ActionResult ViewProfileLogin()
        {
            var userLogin = new UserLogin();
            userLogin = (UserLogin)HttpContext.Session["USER_SESSION"];
            var model = new UserDao().GetById(userLogin.UserName);
            return View(model);
        }
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });

        }
        public ActionResult Edit(int id)
        {
            var dao = new UserDao();
            var user = dao.ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var res = dao.Update(user);
                if (res)
                {
                    SetAlert("Cap nhat nguoi dung thanh cong.", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cap nhat that bai", "error");
                    ModelState.AddModelError("", "Cap nhat thong tin that bai");
                }
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                var dao = new UserDao();
                var IsExit = dao.GetById(user.UserName);
                if (IsExit == null)
                {
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        user.Password = Encryptor.MD5Hash(user.Password);
                    }
                    
                    long res = dao.Insert(user);
                    if (res > 0)
                    {
                        SetAlert("Them moi nguoi dung thanh cong.", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Them moi khong thanh cong");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tai khoan da ton tai");
                }
                
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            var res = dao.Delete(id);
            if (res)
            {
                SetAlert("Xoa nguoi dung thanh cong.", "success");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Xoa khong thanh cong.");
            }
            return View();
        }

    }
}