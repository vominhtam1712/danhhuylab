using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListClass.Dao;
using ListClass.Model;

namespace ListSerialNumber.Controllers
{
    public class RoleController : AccountLoginController
    {
        RoleDAO roleDAO = new RoleDAO();
        public ActionResult Index(int? page)
        {
            if (HasAccessAd())
            {
                int pageSize = 10;
                int pageNumber = page ?? 1;
                var list = roleDAO.getList(pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Create()
        {
            if (HasAccessAd())
            { 
                return View("Create");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.Status = 1;
                roleDAO.Insert(role);
                TempData["Message"] = "Tạo thành công";
                return RedirectToAction("Index");

            } 
            return View(role);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (HasAccessAd())
            {
                Role role = roleDAO.getRow(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(role);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            role.Status = 1;
            roleDAO.Update(role);
            TempData["Message"] = "Cập nhật thành công";
            return RedirectToAction("Index");

        }
    }
}
