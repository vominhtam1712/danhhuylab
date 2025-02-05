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
    public class BulbSpectrumController : AccountLoginController
    {
        private readonly BulbSpectrumDAO bulbSpectrumDAO = new BulbSpectrumDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("BulbSpectrum"))
            {
                int pageNumber = page ?? 1;
                int pageSize = 10;
                var list = bulbSpectrumDAO.getList(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("ThemBulbSpectrum"))
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
        public ActionResult Create(BulbSpectrum bulbSpectrum)
        {
            if (!ModelState.IsValid)
            {
                return View(bulbSpectrum);
            }

            if (bulbSpectrumDAO.CheckExists(bulbSpectrum.Bulb_Spectrum))
            {
                TempData["Message"] = "Bulb Spectrum đã tồn tại";
                return RedirectToAction("Index");
            }
            bulbSpectrum.status = 1;
            bulbSpectrumDAO.Insert(bulbSpectrum);
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
            if (HasAccessAd() || HasAccessUser("SuaBulbSpectrum"))
            {
                var idList = ParseIds(ids);
                var bulbSpectrum = bulbSpectrumDAO.GetRowsByIds(idList);

                if (bulbSpectrum == null || !bulbSpectrum.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(bulbSpectrum);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sửa";
                return RedirectToReferrer();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<BulbSpectrum> bulbSpectrums)
        {
            if (bulbSpectrums == null || !bulbSpectrums.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = bulbSpectrums.Select(b => b.Id).ToList();

                var existingRecords = dbContext.BulbSpectrums
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var bulbSpectrum in bulbSpectrums)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == bulbSpectrum.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Bulb_Spectrum = bulbSpectrum.Bulb_Spectrum;
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
            if (HasAccessAd() || HasAccessUser("XoaBulbSpectrum"))
            {
                var idList = ParseIds(ids);
                var bulbSpectrums = bulbSpectrumDAO.GetRowsByIds(idList);

                if (bulbSpectrums == null || !bulbSpectrums.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }

                return View(bulbSpectrums);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<BulbSpectrum> bulbSpectrums)
        {
            if (bulbSpectrums == null || !bulbSpectrums.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index");
            }
            var id = bulbSpectrums.FirstOrDefault().Id;
            if(bulbSpectrumDAO.Check1(id) || bulbSpectrumDAO.Check2(id))
            {
                TempData["Message"] = "Không thể xóa bulbSpectrums này .";
                return RedirectToAction("Index");
            } 
            var productIds = bulbSpectrums.Select(x => x.Id).ToList();
            var listNumbers = dbContext.BulbSpectrums.Where(x => productIds.Contains(x.Id)).ToList();
            foreach (var oldList in listNumbers)
            {
                dbContext.BulbSpectrums.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
