using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using OnlineShopping.Domain.Interfaces;
using OnlineShopping.Domain.Repositoies;
using OnlineShopping.Web.Models;

namespace OnlineShopping.Web.Areas.Admin.Controllers
{
 [Authorize(Roles = "A")]
    public class ManageProductController : Controller
 {
      private readonly IProduct productRepo;
      private readonly ICategory categoryRepo;

      public ManageProductController(IProduct productRepository,ICategory categoryRepository)
        {
            productRepo = productRepository;
            categoryRepo= categoryRepository;
         
        }
    
     public ActionResult Index()
     {

         ViewBag.Category = new SelectList(categoryRepo.GetAllCategory().ToList(), "PKCategoryId", "CategoryName");
         
        
            return View();
        }

  

     public ActionResult GetProductByCategoryId(int id)
     {
         return PartialView("~/Areas/Admin/Views/Shared/_ManageProductList.cshtml", productRepo.GetAllProductsByFKCatId(id));
     }

 }
   
}