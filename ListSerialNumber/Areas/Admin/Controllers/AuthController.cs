using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListClass.Model;
using ListSerialNumber.Library;

namespace ListSerialNumber.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        ListDBContext db = new ListDBContext();
        Md5 md5 = new Md5();

        // GET: Admin/Auth
        public ActionResult Login()
        {
            if (!Session["username"].Equals(""))
            {
                return RedirectToAction("Index", "Dashboard");
            }    
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection filed)
        {
            string strerror = "";
            string username = filed["username"];
            string password = filed["password"];

            string hashedPassword = md5.ComputeMd5Hash(password);

            User rowuer = db.Users.Where(m =>m.UserName == username && m.Lever=="admin").FirstOrDefault();
            if (rowuer == null)
            {
                strerror = "Sai tài khoản hoặc mật khẩu";
            }
            else
            {
                if(rowuer.Password.Equals(hashedPassword))
                {
                    Session["UserName"] = rowuer.UserName;
                    Session["Name"] = rowuer.Name;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    strerror = "Sai tài khoản hoặc mật khẩu";
                }    
            }
            ViewBag.Error ="<span class='text-danger'>"+ strerror+"</span>";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserName"] = "";
            Session["CompanyName"] = "";
            Session["Name"] = "";
            return RedirectToAction("Login", "Auth");
        }
    }
}