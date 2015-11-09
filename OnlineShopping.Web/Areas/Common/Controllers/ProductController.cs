using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Interfaces;
using OnlineShopping.Domain.Repositoies;
using Data;
//using OnlineShopping.Web.Areas.Common.Models;
using System.Configuration;

namespace OnlineShopping.Web.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
       private readonly IProduct repository;

      
     //  HttpCookie cartCookie;
    
       
       IList<Product> products = new List<Product>();
       IList<string> productId = new List<string>();
       public ProductController(IProduct productRepository)
        {
            repository = productRepository;
        }
        public ActionResult Index()
        {
          
            return View();
        }
        [OutputCache(Duration = 10)]
        public ActionResult ProductByCategoryId(int FKCatId)
        {
            //return View();
            //return View(repository.GetAllProductsByFKCatId(FKCatId));
            return PartialView("_ProductByCategoryId", repository.GetAllProductsByFKCatId(FKCatId));
        }
       
        //public void CheckCookie()
        //{
           
        //    cartCookie = Request.Cookies["cookie"];
        //    if (cartCookie == null)
        //    {
        //        cartCookie = new HttpCookie("cookie");
        //    }
        //    //cartCookie.Expires = DateTime.MaxValue;
        //    int count = cartCookie.Values.AllKeys.Count();
        //}
        //public void NoOfItem()
        //{
        //    foreach (string item in cartCookie.Values)
        //    {
        //        productId.Add(cartCookie.Values[item]);
        //    }
        //}

        public string AddItemToCart()
        {
            string ProId = Request.QueryString["productId"];

            if (Session["ProId"] == null)
            {
              
                Session["ProId"] = new List<string>();
            }
            ((List<string>)Session["ProId"]).Add(ProId);
           
            return NoOfItemInTheCart();
                    
        }


        public string NoOfItemInTheCart()
        {
            if (Session["ProId"] == null)
            {
                return "(" + 0 + ")";
            }
            else
            {
                var c = ((List<string>)Session["ProId"]).Count();
                return "(" + c + ")";
            }
        }

        public List<Product> FindProduct()
        {
            productId = (IList<string>)Session["ProId"];
            foreach (var newitem in productId)
            {
                int newid = Convert.ToInt32(newitem);

                products.Add(repository.GetById(newid));
            }
            return products.ToList();
        }
       
        public ActionResult RemoveItemFromCart(int itemId)
        {
          
            if (Session["ProId"] != null)
            {
                productId = (IList<string>)Session["ProId"];
                foreach (var item in productId)
                {
                    int id = Convert.ToInt32(item);
                    if (id == itemId)
                    {
                        productId.Remove(item);
                        Session["ProId"] = productId;
                        
                       
                        return PartialView("_ViewCartItem", FindProduct());
                      
                       
                    }
                    
                    
                   
                }
             
                
            }

            return RedirectToAction("Index");
        }

        public ActionResult ViewCart()
        {
      

            if (Session["ProId"]!=null)
            {
                


                return View(FindProduct());
                   
            }
           return RedirectToAction("Index");
        }
  

        
            
       
    }
}