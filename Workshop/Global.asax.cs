using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Clutch.Diagnostics.EntityFramework;
using NLog;
using Workshop.Controllers;

namespace Workshop
{
    // 注意: 如需啟用 IIS6 或 IIS7 傳統模式的說明，
    // 請造訪 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            DbTracing.Enable(
                new GenericDbTracingListener()
                    .OnFinished(c => logger.Trace("-- Command finished - time: {0}{1}{2}", c.Duration, Environment.NewLine, c.Command.ToTraceString()))
                    .OnFailed(c => logger.Trace("-- Command failed - time: {0}{1}{2}", c.Duration, Environment.NewLine, c.Command.ToTraceString()))
            );
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    // 發生未處理錯誤時執行的程式碼

        //    var app = (MvcApplication)sender;
        //    var ex = app.Server.GetLastError();

        //    var context = app.Context;
        //    context.Response.Clear();
        //    context.ClearError();

        //    var httpException = ex as HttpException;
        //    if (httpException == null)
        //    {
        //        httpException = new HttpException(null, ex);
        //    }

        //    var routeData = new RouteData();

        //    routeData.Values["controller"] = "Errors";
        //    routeData.Values["action"] = "Index";

        //    routeData.Values["exception"] = ex;
        //    routeData.Values["from_Application_Error_Event"] = true;

        //    if (httpException != null)
        //    {
        //        switch (httpException.GetHttpCode())
        //        {
        //            case 404:
        //                routeData.Values["action"] = "PageNotFound";
        //                break;
        //            default:
        //                routeData.Values["action"] = "Index";
        //                break;
        //        }
        //    }

        //    // Pass exception details to the target error View.
        //    routeData.Values.Add("error", ex.Message);

        //    // Avoid IIS7 getting in the middle
        //    context.Response.TrySkipIisCustomErrors = true;
        //    IController controller = new ErrorsController();
        //    controller.Execute(new RequestContext(new HttpContextWrapper(context), routeData));
        //}


        void ErrorLog_Filtering(object sender, Elmah.ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpRequestValidationException)
            {
                e.Dismiss();
            }

            var httpException = e.Exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                e.Dismiss();
            }
        }

        public void ErrorMail_Filtering(object sender, Elmah.ExceptionFilterEventArgs e)
        {
            var httpException = e.Exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                e.Dismiss();
            }
        }

        void ErrorMail_Mailing(object sender, Elmah.ErrorMailEventArgs e)
        {
            string machineName = "Server-Localhost";

            string systemName = "Workshop";

            //subject="$MachineName$ ($SystemName$) at $ErrorTime$ : {0}"
            string elmahSubject = e.Mail.Subject;

            string emailSubject = string.Format("ELMAH 錯誤記錄 => {0}",
                elmahSubject
                    .Replace("$MachineName$", machineName)
                    .Replace("$SystemName$", systemName)
                    .Replace("$ErrorTime$", DateTime.Now.ToString("yyyy/MM/dd HH:mm"))
            );

            e.Mail.Subject = emailSubject;
        }

    }
}