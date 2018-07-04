
using BotDetect.Web.UI.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.Username))
                {
                    ModelState.AddModelError("", "ユーザIDが存在しています。");
                }else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "メールアドレスが存在しています。");
                }
                else
                {
                    var user = new User()
                    {
                        UserName= model.Username,
                        Name = model.Name,
                        Password = model.Password,
                        Phone = model.Phone,
                        Mail = model.Email,
                        Address = model.Address,
                        CreateDate = DateTime.Now,
                        Status = true
                    };
                    var res = dao.Insert(user);
                    if (res > 0)
                    {
                        ViewBag.Success = "登録が完了しました！";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "登録が失敗しました。");
                    }
                }
            }
            return View(model);
        }
        
    }
}