﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ListClass.Model;
using Newtonsoft.Json;

namespace ListSerialNumber.Controllers
{
    public class HomeController : AccountLoginController
    { 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Khongtimthay()
        {
            return View();
        } 
    }
}