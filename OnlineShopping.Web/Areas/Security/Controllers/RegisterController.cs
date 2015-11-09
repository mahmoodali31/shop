using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using OnlineShopping.Domain.Interfaces;
using OnlineShopping.Domain.Repositoies;
using System.Web.Security;
using OnlineShopping.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace OnlineShopping.Web.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
      
        // GET: Security/Register
        private readonly IUsers userRepo;
        private readonly IShippingAddress sAddresRepo;

        public RegisterController(IUsers userRepository, IShippingAddress shippingAddressRepository)
        {
            userRepo = userRepository;
            sAddresRepo = shippingAddressRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EmailId,Password,Address,City,State,Country,ZipCode,Mobile")] RegisterViewModel viewModel)
        {
            try
            {
                
              
                if (ModelState.IsValid)
                {
                  


                    Data.User user = new Data.User();
                    Data.UserAddress userAddress = new UserAddress();

                  //  ShippingAddressRepository shReop = new ShippingAddressRepository();
                    
                 
                     user.FirstName = viewModel.FirstName;
                     user.LastName = viewModel.LastName;
                     user.EmailId = viewModel.EmailId;
                     user.Password = viewModel.Password;
                     user.Role = "U";
                     user.UserName = viewModel.FirstName + " " + viewModel.LastName;

                     if (!userRepo.VerifyEmail(viewModel.EmailId))
                     {
                         userRepo.Create(user);
                         int userId = userRepo.GetByEmail(viewModel.EmailId).PKUserId;
                         userAddress.FKUserId = userId;
                         userAddress.Address = viewModel.Address;
                         userAddress.City = viewModel.City;
                         userAddress.State = viewModel.State;
                         userAddress.Country = viewModel.Country;
                         userAddress.ZipCode = viewModel.ZipCode;
                         userAddress.Mobile = viewModel.Mobile;
                         sAddresRepo.Create(userAddress);

                         TempData["Msg"] = "Created Successfully";

                         return RedirectToAction("Index");
                     }
                     else
                     {
                         TempData["Msg"] = "Email already exits";

                         return RedirectToAction("Index");
                     }
                    
                    
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Create Failed : " + e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}