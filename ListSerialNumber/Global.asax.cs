using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ListSerialNumber
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
        protected void Session_Start()
        {
            Session["UserName"] = "";
            Session["CompanyName"] = "";
            Session["Name"] = "";
            Session["Lever"] = "";

        }
        protected void Application_Error()
        { 
            Exception exception = Server.GetLastError();
             
            if (exception is HttpException httpException && httpException.GetHttpCode() == 404)
            { 
                Response.Clear();
                Server.ClearError();
                Response.Redirect("~/Error/404");
            }
        }
    }
}
