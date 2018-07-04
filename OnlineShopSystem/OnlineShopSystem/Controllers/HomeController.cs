using Model.Dao;
using OnlineShopSystem.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Controllers
{
    public class HomeController : BaseController
    {
       
        // GET: Home
        public ActionResult Index()
        {
            var iplProduct = new ProductDao();
            ViewBag.Slides = new SlideDao().ListAll();
            ViewBag.newProduct = iplProduct.ListNewProduct(4);
            ViewBag.FeatureProducts = iplProduct.ListFeatureProduct(4);
            ViewBag.HotProducts = iplProduct.ListHotProduct(4);
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult BigMenu()
        {
            string lang = HttpContext.Session["CurrentCulture"].ToString();
            var iplMenu = new MenuDao();
            var model = iplMenu.ListByGroup(1, lang);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            string lang = HttpContext.Session["CurrentCulture"].ToString();
            var list = new MenuDao().ListByGroupId(1, lang);
            return PartialView(list);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            string lang = HttpContext.Session["CurrentCulture"].ToString();
            var model = new MenuDao().ListByGroupId(2, lang);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var lang = HttpContext.Session["CurrentCulture"].ToString();
            var model = new FooterDao().GetFooter(lang);
            return PartialView(model);
        }
    }
}