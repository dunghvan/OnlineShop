using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pageSize = 10, string searchString = null)
        {
            var model = new OrderDao();
            var list = model.ListAllPaging(page, pageSize, searchString);
            return View(list);
        }
        public ActionResult OrderCheck(long id)
        {
            var res = new OrderDao().Check(id);
            if (res)
            {
                SetAlert("確認出来ました。", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("確認できない。", "warning");
                return RedirectToAction("Index");
            }
        }
    }
}