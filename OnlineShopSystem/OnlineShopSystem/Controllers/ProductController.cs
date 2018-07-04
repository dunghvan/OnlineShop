using Model.Dao;
using Model.EF;
using OnlineShopSystem.Areas.Admin.Controllers;
using OnlineShopSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopSystem.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 16, string searchString=null)
        {
            
            SetViewBag();
            var iplProduct = new ProductDao();
            ViewBag.listCategory = new ProductCategoryDao().ListAll("jp");
            var list = iplProduct.ListAllPaging(page, pageSize, searchString);
          
            return View(list);
        }
        public ActionResult Search(string keyword, int page= 1, int pageSize = 4)
        {
            SetViewBag();
            var iplProduct = new ProductDao();
            var SearchRS = iplProduct.GetByName(keyword, page, pageSize);
            return RedirectToAction("Index", new { searList = SearchRS });
        
        }
       public JsonResult ListName(string q)
        {
            var jdata = new ProductDao().ListName(q);
            return Json(new
            {

                data = jdata,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        public void SetViewBag()
        {
            var lang = HttpContext.Session["CurrentCulture"].ToString();
            var CateDao = new ProductCategoryDao();
            ViewBag.CategoryList = new SelectList(CateDao.ListAll(lang), "ID", "Name");
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var lang = HttpContext.Session["CurrentCulture"].ToString();
            var model = new ProductCategoryDao().ListAll(lang);
            return PartialView(model);
        }

        public ActionResult Category(long id)
        {
            var list = new ProductDao().ListByCategoryID(id, 1,4);

            return View(list);
        }

        public void SetViewBag(long? selectedID = null) {
            var cate = new CategoryDao();
            ViewBag.CategoryId = new SelectList(cate.ListAll(), "ID", "Name", selectedID);
        }

        public ActionResult ViewProductDetail(long productID) {
            var iplProduct = new ProductDao();
            iplProduct.ViewCount( productID);
            var product = iplProduct.ViewDetail(productID);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProList = new ProductDao().ListRelatedProduct(productID);
            
            
            return View(product);
        }
    }
}