using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Interfaces;

namespace OnlineShopping.Web.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        private readonly IProduct repository;

        public HomeController(IProduct productRepository)
        {
            repository = productRepository;
        }
        public ActionResult Index()
        {
            //return PartialView("_Product", repository.GetAllProduct());
            return View();
        }
    }
}