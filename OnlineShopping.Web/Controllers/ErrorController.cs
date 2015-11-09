using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShopping.Web.Models;

namespace OnlineShopping.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(Exception error)
        {
            Response.StatusCode = 500;
            var m = error.Message;
            if (error.InnerException != null) m += " | " + error.InnerException.Message;
            ViewBag.Message = m;
            //if (Request.IsAjaxRequest())
            //{
            //    if (error is EAgreementException)
            //        return View("Expectedp");
            //    return View("Errorp");
            //}

            if (error is EAgreementException)
                return View("Expected", new ErrorDisplay { Message = error.Message });
            return View("Error", new ErrorDisplay { Message = error.Message });

        }
        public ActionResult HttpError404(Exception error)
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}