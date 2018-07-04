using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using OnlineShopSystem.Areas.Admin.Models;
using OnlineShopSystem.Common;

namespace OnlineShopSystem.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin { UserName = user.UserName, ID = user.ID };

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }else if (result == 0){
                    ModelState.AddModelError("", "Tai Khoan khong dung");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tai khoan dang bi khoa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mat khau");
                }
                else
                {
                    ModelState.AddModelError("", "Dang nhap khong thanh cong");
                }
            }
            return View("Index");
        }
    }
}