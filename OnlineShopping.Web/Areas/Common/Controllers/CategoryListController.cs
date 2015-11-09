using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Interfaces;

namespace OnlineShopping.Web.Areas.Common.Controllers
{
    [AllowAnonymous]
    public class CategoryListController : Controller
    {
        private readonly ICategory repository;

          public CategoryListController(ICategory categoryRepository)
        {
            repository = categoryRepository;
        }
        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            return PartialView("_Category",repository.GetAllCategory());
        }
    }
}