using Model.Dao;
using Model.EF;
using OnlineShopSystem.Areas.Admin.Controllers;
using OnlineShopSystem.Common;
using OnlineShopSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShopSystem.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session["cartSession"];
            var list = new List<CartItem>();
            if (cart != null)
            {
               
                list = (List<CartItem>)cart;
            }
            
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteItem(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new {
                status = true
                });
        }
        public JsonResult IncreaseQuanlity(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession]; 
            foreach(var item in sessionCart)
            {
             if(item.Product.ID == id)
                {
                    item.Quanlity += 1;
                }   
            }
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DescendingQuanlity(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            var temp = new CartItem();
            foreach (var item in sessionCart)
            {
                if (item.Product.ID == id)
                {
                    item.Quanlity -= 1;
                    if(item.Quanlity == 0)
                    {
                        temp = item;
                    }
                }
            }
            sessionCart.Remove(temp);
            Session[CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem != null){
                    item.Quanlity = jsonItem.Quanlity;
                }
            }
            return Json(new
            {
                status = true
            });
        }
        

        [ChildActionOnly]
        public ActionResult InCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            var SumItem = 0;
            decimal total = 0;
            if (cart!= null)
            {
                list = (List<CartItem>)cart;
                foreach(var item in list) {
                    SumItem += item.Quanlity;
                    total += (decimal)item.Quanlity * (item.Product.Price.GetValueOrDefault(0));
                }
            }
            ViewBag.SumOfItem = SumItem;
            ViewBag.Total = total;
            return PartialView(list);
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session["cartSession"];
            var list = new List<CartItem>();
            if (cart != null)
            {

                list = (List<CartItem>)cart;
            }

            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string phone, string address, string email)
        {
            try
            {
                var order = new Order()
                {
                    CreateDate = DateTime.Now,
                    ShipName = shipName,
                    ShipEmail = email,
                    ShipAddress = address,
                    ShipMobile = phone
                };
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                var detailDao = new OrderDetailDao();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderID = id,
                        ProductID = item.Product.ID,
                        Price = item.Product.Price,
                        Quanlity = item.Quanlity
                    };
                    detailDao.Insert(orderDetail);
                }
            }
            catch
            {
                throw;
            }
            
            return Redirect("/hoan-thanh");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult AddItem(long productID, int quan)
        {
            var cart = Session[CommonConstants.CartSession];
            var iplProduct = new ProductDao();
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x=>x.Product.ID == productID))
                {
                    foreach(var i in list)
                    {
                        if(i.Product.ID == productID)
                        {
                            i.Quanlity += quan;
                        }
                    }
                }
                else
                {
                    var item = new CartItem()
                    {
                        Product = iplProduct.FindbyId(productID),
                        Quanlity = quan
                    };
                    list.Add(item);
                }
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                var item = new CartItem()
                {
                    Product = iplProduct.FindbyId(productID),
                    Quanlity = quan
                };
                var list = new List<CartItem>();
                list.Add(item);
                Session[CommonConstants.CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}