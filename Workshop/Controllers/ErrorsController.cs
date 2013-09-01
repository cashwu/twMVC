using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workshop.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Index(Exception exception = null)
        {
            Response.StatusCode = 200;
            return View();
        }


        [PreventDirectAccess]
        public ActionResult PageNotFound(string error, Exception exception)
        {
            ViewData["Description"] = "404 Error, Page not Found!";
            Response.StatusCode = 404;
            return View();
        }

        private class PreventDirectAccessAttribute : FilterAttribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationContext filterContext)
            {
                object value = filterContext.RouteData.Values["from_Application_Error_Event"];
                if (!(value is bool && (bool)value))
                {
                    filterContext.Result = new ViewResult { ViewName = "Index" };
                }
            }
        }

    }
}
