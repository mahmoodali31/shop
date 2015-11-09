using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Domain.Interfaces;
using Data;
using System.Web.Security;

namespace OnlineShopping.Web.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUsers repository;

        public LoginController(IUsers userRepository)
        {
            repository = userRepository;
           
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        public ActionResult SingIn(Data.User user)
        {
          
            try
            {

                if (repository.ValidateUser(user.EmailId, user.Password))
                {
                  

                    FormsAuthentication.SetAuthCookie(user.EmailId, false);
                   
                    Data.User currentUser = repository.GetByEmail(user.EmailId);
                    Session["UserId"] = currentUser.PKUserId;
                    Session["UserName"] = currentUser.FirstName;
                    return RedirectToAction("Index", "Home", new { area = "Common" });
                }
                else
                {
                    
                    TempData["Msg"] = "Login Failed";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception E1)
            {
                
              TempData["Msg"] = "Login Failed " + E1.Message;
               //return RedirectToAction("Index");
                return View("Error", new HandleErrorInfo(E1,"Login","SingIn"));

            }
           
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home", new { area = "Common" });
        }
    }
}