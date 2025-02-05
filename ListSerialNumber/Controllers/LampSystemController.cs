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
    public class LampSystemController : AccountLoginController
    {
        private readonly LampSystemsDAO lampsystemsDAO = new LampSystemsDAO(); 
        private readonly ListDBContext dbContext = new ListDBContext();
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("LamSystems"))
            { 
                int pageNumber = page ?? 1;
                int pageSize = 10;
                var list = lampsystemsDAO.getList(Name, pageSize, pageNumber); 
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemLamSystems"))
            { 
                return View();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LampSystem lampSystem)
        {
            if (!ModelState.IsValid)
            {
                return View(lampSystem);
            }

            if (lampsystemsDAO.CheckExists(lampSystem.Lamp_Systems))
            {
                TempData["Message"] = "Đèn UV đã tồn tại";
                return RedirectToAction("Index");
            }
            lampSystem.status = 1;
            lampsystemsDAO.Insert(lampSystem);
            TempData["Message"] = "Thêm thành công"; 
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaLamSystems"))
            {
                var idList = ParseIds(ids);
                var lampSystems = lampsystemsDAO.GetRowsByIds(idList);

                if (lampSystems == null || !lampSystems.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                } 
                return View(lampSystems);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sửa";
                return RedirectToReferrer();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<LampSystem> lampSystems)
        {
            if (lampSystems == null || !lampSystems.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = lampSystems.Select(b => b.Id).ToList();

                var existingRecords = dbContext.LampSystems
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var lampSystem in lampSystems)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == lampSystem.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Lamp_Systems = lampSystem.Lamp_Systems; 
                        existingRecord.status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                } 
                dbContext.SaveChanges();
                TempData["Message"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaLamSystems"))
            {
                var idList = ParseIds(ids);
                var lampSystems = lampsystemsDAO.GetRowsByIds(idList);

                if (lampSystems == null || !lampSystems.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }

                return View(lampSystems);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<LampSystem> lampSystems)
        {
            if (lampSystems == null || !lampSystems.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index");
            } 
            var id = lampSystems.FirstOrDefault().Id;
            if (lampsystemsDAO.Check1(id) || lampsystemsDAO.Check2(id))
            {
                TempData["Message"] = "Không thể xóa lampSystems này .";
                return RedirectToAction("Index");
            }
            var productIds = lampSystems.Select(x => x.Id).ToList();
            var listNumbers = dbContext.LampSystems.Where(x => productIds.Contains(x.Id)).ToList(); 
            foreach (var oldList in listNumbers)
            {  
                dbContext.LampSystems.Remove(oldList);
            } 
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
