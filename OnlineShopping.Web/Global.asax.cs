﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineShopping.Web.Controllers;

namespace OnlineShopping.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    // Log the exception.
        //    Response.Clear();

        //    var httpException = exception as HttpException;

        //    var routeData = new RouteData();
        //    routeData.Values.Add("controller", "Error");

        //    if (httpException == null)
        //    {
        //        routeData.Values.Add("action", "Index");
        //    }
        //    else //It's an Http Exception, Let's handle it.
        //    {
        //        switch (httpException.GetHttpCode())
        //        {
        //            case 404:
        //                // Page not found.
        //                routeData.Values.Add("action", "HttpError404");
        //                break;
        //            case 505:
        //                // Server error.
        //                routeData.Values.Add("action", "HttpError505");
        //                break;

        //            // Here you can handle Views to other error codes.
        //            // I choose a General error template  
        //            default:
        //                routeData.Values.Add("action", "Index");
        //                break;
        //        }
        //    }

        //    // Pass exception details to the target error View.
        //    routeData.Values.Add("error", exception);

        //    // Clear the error on server.
        //    Server.ClearError();

        //    // Call target Controller and pass the routeData.
        //    IController errorController = new ErrorController();
        //    errorController.Execute(new RequestContext(
        //         new HttpContextWrapper(Context), routeData));
        //}

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            // Log the exception.
            Response.Clear();

            var httpException = exception as HttpException;

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        Response.Redirect("~/Error/HttpError404");
                        break;
                    case 505:
                        // Server error.
                        Response.Redirect("~/Error/HttpError404");
                        break;

                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        Response.Redirect("/Error/HttpError404");
                        break;
                }

            }

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                 new HttpContextWrapper(Context), routeData));
        }
    }
}
