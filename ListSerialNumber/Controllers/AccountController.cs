using ListClass.Model;
using ListSerialNumber.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class AccountController : Controller
    {
        private readonly ListDBContext db = new ListDBContext();
        private readonly Md5 md5 = new Md5();

        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Session["username"] as string))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection filed)
        {
            string username = filed["username"];
            string password = filed["password"];
            string hashedPassword = md5.ComputeMd5Hash(password);
             
            var company = db.Companys
                .Where(m => m.UserName == username && m.Password == hashedPassword && m.Status == 1)
                .FirstOrDefault();

            if (company != null)
            { 
                SetSessionValues(company);  
                return RedirectToAction("Index", "Home");
            }
             
            var user = db.Users.SingleOrDefault(m => m.UserName == username && m.Password == hashedPassword && m.Lever == "admin");
            if (user != null)
            { 
                SetSessionValues(user); 
                return RedirectToAction("Index", "Home");
            }
             
            ViewBag.Error = "<span class='text-danger'>Sai tài khoản hoặc mật khẩu</span>";
            return View();
        } 
        public void SetSessionValues(Company company)
        {
            Session["CompanyId"] = company.Id;
            Session["UserName"] = company.UserName;
            Session["Password"] = company.Password;
            Session["Name"] = company.Name; 
        }

        public void SetSessionValues(User user)
        {
            Session["UserId"] = user.Id;
            Session["UserName"] = user.UserName;
            Session["Password"] = user.Password;
            Session["Name"] = user.Name; 
            Session["Lever"] = user.Lever; 
        }

        public ActionResult LogoutAccount()
        {
            Session.Clear();
            return RedirectToAction("Index", "Account");
        }
    }
}