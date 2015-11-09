using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;

using OnlineShopping.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineShopping.Web.Areas.Admin.Controllers
{
     [AllowAnonymous]
    public class ManageUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ManageUser

        public ManageUserController()
        {
            
        }
        public ActionResult Index()
        {
            string userId = User.Identity.Name;
           
            return View();
        }

    }
}