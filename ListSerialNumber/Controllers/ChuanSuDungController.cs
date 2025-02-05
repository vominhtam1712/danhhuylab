using ListClass.Dao;
using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class ChuanSuDungController : AccountLoginController
    {
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly ChuanSuDungDAO chuansudungDAO = new ChuanSuDungDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Create(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien")) 
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm ";
                return RedirectToReferrer();
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<ChuanSuDung> ids)
        {
            var Id = ids.First().Id_PhanCong;
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            }
            foreach (var idss in ids)
            {
                if (chuansudungDAO.CheckSerialNumberExists(idss.Id_PhanCong))
                {
                    TempData["Message"] = "Chỉ có thể tạo chuẩn sử dụng 1 lần";
                    return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
                }
                var oldlist = new ChuanSuDung
                {
                    Id_PhanCong = idss.Id_PhanCong,
                    Model = idss.Model,
                    Description = idss.Description,
                    Serial = idss.Serial,
                    Cetificate = idss.Cetificate,
                    Cal_Date = idss.Cal_Date,
                    Cal_Due = idss.Cal_Due,  
                    NgayTao = DateTime.Now,
                    NguoiTao = Session["Name"].ToString(),
                    Status = 1
                };
                chuansudungDAO.Insert(oldlist);
            }

            TempData["Message"] = "Tạo thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = chuansudungDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sửa";
                return RedirectToReferrer();
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<ChuanSuDung> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.ChuanSuDungs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var idss in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == idss.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_PhanCong = idss.Id_PhanCong;
                        existingRecord.Model = idss.Model;
                        existingRecord.Description = idss.Description;
                        existingRecord.Serial = idss.Serial;
                        existingRecord.Cetificate = idss.Cetificate;
                        existingRecord.Cal_Date = idss.Cal_Date;
                        existingRecord.Cal_Due = idss.Cal_Due;  
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                var id = ids.First().Id_PhanCong;
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id = id });
            }
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = chuansudungDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa ";
                return RedirectToReferrer();
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<ChuanSuDung> ids)
        {
            var id = ids.FirstOrDefault()?.Id_PhanCong;
            foreach (ChuanSuDung ChuanSuDung in ids)
            {
                var oldlist = dbContext.ChuanSuDungs.Find(ChuanSuDung.Id);
                dbContext.ChuanSuDungs.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        } 
    }
}