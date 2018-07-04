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
    public class ProductController : AdminBaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 10, string searchString = null)
        {
            var model = new ProductDao().ListAllPaging(page,pageSize,searchString);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult ViewDetail(long id)
        {
            
            var product = new ProductDao().ViewDetails(id);
           
            return View(product);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product pro)
        {
            if (ModelState.IsValid)
            {
                var _userLogin = new UserLogin();
                _userLogin = (UserLogin)HttpContext.Session["USER_SESSION"];
                var dao = new ProductDao();
                pro.CreateDate = DateTime.Now;
                pro.Language = CommonConstants.lang;
                var res = dao.Insert(pro, _userLogin.ID);
                if (res)
                {
                    SetViewBag();
                    SetAlert("新しい商品をが完了しました！", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "登録が失敗しました。");
                }
            }
            else
            {
                ModelState.AddModelError("", "各フィールドに正しく入力してください");
                SetViewBag();
                return View();
            }
            SetViewBag();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var model = new ProductDao().FindbyId(id);
            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var _userLogin = new UserLogin();
                _userLogin = (UserLogin)HttpContext.Session["USER_SESSION"];
                var res = new ProductDao().Update(product, _userLogin.ID);
                if (res)
                {
                    SetAlert("編集が完了しました！", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetViewBag();
                    SetAlert("編集が失敗しました！", "error");
                    ModelState.AddModelError("", "編集が失敗しました");
                }
            }
            SetViewBag();
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var res = new ProductDao().Delete(id);
            if (res)
            {
                SetAlert("Delete success", "success");
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Delete Fail!!");
            }
            return View();
        }
        public void SetViewBag(long? selectedID = null)
        {
            var lang = HttpContext.Session["CurrentCulture"].ToString();
            var CateDao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(CateDao.ListAll(lang), "ID", "Name");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}