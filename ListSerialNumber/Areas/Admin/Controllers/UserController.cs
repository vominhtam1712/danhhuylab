using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ListClass.Dao;
using ListClass.Model;
using OfficeOpenXml;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using ListSerialNumber.Library;

namespace ListSerialNumber.Areas.Admin.Controllers
{
    public class UserController : LoginController
    {
        UserDAO userDAO = new UserDAO();
        Md5 md5Hasher = new Md5();

        public ActionResult Index(string Name, int? Search, int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;
            var list = userDAO.getList(Name, Search, pageSize, pageNumber);
            return View("Index", list);

        }
            
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
         
        public ActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = md5Hasher.ComputeMd5Hash(user.Password);
                user.Lever = "admin";
                userDAO.Insert(user);
                TempData["Message"] = "Tạo thành công";
                return RedirectToAction("Create", "User");

            }

            return View(user);
        }
        protected List<int> ParseIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                throw new ArgumentException("No IDs provided");
            }

            var idList = ids.Split(',')
                            .Select(id => int.TryParse(id, out var result) ? result : (int?)null)
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList();

            if (!idList.Any())
            {
                throw new ArgumentException("Invalid IDs");
            }

            return idList;
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            var idList = ParseIds(ids);
            var listNumbers = userDAO.GetRowsByIds(idList);

            if (listNumbers == null || !listNumbers.Any())
            {
                return HttpNotFound("No data found for the provided IDs");
            } 
            return View(listNumbers);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<User> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.Users
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var id in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == id.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.UserName = id.UserName; 
                        existingRecord.Name = id.Name; 
                        existingRecord.Email = id.Email; 
                        existingRecord.Phone = id.Phone; 
                        existingRecord.Lever = "admin"; 
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index");
            }
        }
        public ActionResult EditMK(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            var idList = ParseIds(ids);
            var listNumbers = userDAO.GetRowsByIds(idList);

            if (listNumbers == null || !listNumbers.Any())
            {
                return HttpNotFound("No data found for the provided IDs");
            }
            return View(listNumbers);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMK(List<User> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.Users
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var id in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == id.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Password = md5Hasher.ComputeMd5Hash(id.Password);
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            User user = userDAO.getRow(id);
            userDAO.Delete(user);
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        protected ActionResult RedirectToReferrer()
        {
            var currentUrl = Request.UrlReferrer?.ToString();
            if (!string.IsNullOrEmpty(currentUrl))
            {
                return Redirect(currentUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
