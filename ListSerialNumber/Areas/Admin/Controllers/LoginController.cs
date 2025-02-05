using ListSerialNumber.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public LoginController()
        {
            if (System.Web.HttpContext.Current.Session["UserName"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/admin/login");
            }
        }
    }
}