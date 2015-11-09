using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Interfaces;
using Data;
using System.Web.Security;

namespace OnlineShopping.Web.Areas.User.Controllers
{
    [Authorize(Roles = "U")]
    public class ChangePasswordController : Controller
    {
        private readonly IUsers repository;

        public ChangePasswordController(IUsers userRepository)
        {
            repository = userRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePassword([Bind(Include = "NewPassword,Password")] Data.User user)
        {
            try
            {

                
               
                    string currentUserEmail = User.Identity.Name;
                    Data.User currentUser = repository.GetByEmail(currentUserEmail);
                    if (user.Password==currentUser.Password)
                    {
                        currentUser.Password = user.NewPassword;
                        repository.Update(currentUser);
                        TempData["Msg"] = "Updated Successfully";
                        return RedirectToAction("Index");
                    }
                    //return RedirectToAction("Index");
               
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
