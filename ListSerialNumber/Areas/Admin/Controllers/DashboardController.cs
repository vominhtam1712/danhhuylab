using ListSerialNumber.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Areas.Admin.Controllers
{
    public class DashboardController : LoginController
    { 
        public ActionResult Index()
        {
            return View();
        }
    }
}