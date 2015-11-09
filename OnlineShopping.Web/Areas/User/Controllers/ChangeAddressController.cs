using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using OnlineShopping.Domain.Interfaces;
using System.Web.Security;

namespace OnlineShopping.Web.Areas.User.Controllers
{
     [Authorize(Roles = "U")]
    public class ChangeAddressController : Controller
    {
        private readonly IShippingAddress repository;

        public ChangeAddressController(IShippingAddress shippingAddressRepository)
        {
            repository = shippingAddressRepository;
        }
        public ActionResult Index()
        {
            UserAddress userAddress = repository.GetAddressByUserId(Convert.ToInt32(Session["UserId"]));
            return View(userAddress);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAddress([Bind(Include = "Address,City,State,Country,ZipCode,Mobile")] Data.UserAddress userAddress)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    userAddress.FKUserId =Convert.ToInt32(Session["UserId"]);
                    UserAddress currentAddress = repository.GetAddressByUserId(userAddress.FKUserId);
                    //currentAddress = userAddress;
                    currentAddress.Address = userAddress.Address;
                    currentAddress.City = userAddress.City;
                    currentAddress.State = userAddress.State;
                    currentAddress.Country = userAddress.Country;
                    currentAddress.ZipCode = userAddress.ZipCode;
                    currentAddress.Mobile = currentAddress.Mobile;
                    //userAddress.PKShippingAddressId = userAddress.PKShippingAddressId;
                    repository.Update(currentAddress);
                    TempData["Msg"] = "Update Successfully";
                    return RedirectToAction("Index");
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