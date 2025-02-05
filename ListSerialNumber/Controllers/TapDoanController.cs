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
    public class TapDoanController : AccountLoginController
    {
        private readonly TapDoanDAO tapdoanDAO = new TapDoanDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("TapDoan"))
            {
                const int pageSize = 10;
                int pageNumber = page ?? 1;
                var count = tapdoanDAO.getCountIndex();
                ViewBag.Count = count;
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                var list = tapdoanDAO.getList(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("ThemTapDoan"))
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
        public ActionResult Create(TapDoan tapdoan)
        {
            if (!ModelState.IsValid)
            { 
                return View(tapdoan);
            }
             
            tapdoan.Status = 1;
            tapdoanDAO.Insert(tapdoan);

            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "TapDoan");
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaTapDoan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = tapdoanDAO.GetRowsByIds(idList);

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
        public ActionResult Edit(List<TapDoan> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.TapDoans
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KhachHang in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KhachHang.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Tentapdoan = KhachHang.Tentapdoan;
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
        }
        public ActionResult ChinhSua(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaTapDoan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = tapdoanDAO.GetRowsByIds(idList);

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
        public ActionResult ChinhSua(List<TapDoan> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.TapDoans
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KhachHang in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KhachHang.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Tentapdoan = KhachHang.Tentapdoan;
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
              
            if (HasAccessAd() || HasAccessUser("XoaTapDoan"))
            {
                var idList = ParseIds(ids);
                  
                var listNumbers = tapdoanDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("Không tìm thấy dữ liệu cho các ID đã cung cấp");
                }

                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<TapDoan> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Lỗi trong quá trình xóa.";
                return RedirectToAction("Index", "Home");
            }
            var tapDoanNamesWithKhachHang = tapdoanDAO.GetTapDoanNamesWithKhachHang(ids);
            if (tapdoanDAO.CheckExists(ids))
            {
                string message = " Tập đoàn ' "
                       + string.Join(" , ", tapDoanNamesWithKhachHang) + " ' đang có khách hàng không thể xóa! ";
                TempData["Message"] = message;
                return RedirectToAction("Index");
            }
             
            foreach (var listNumber in ids)
            {
                var oldList = dbContext.TapDoans.Find(listNumber.Id);
                if (oldList != null)
                {
                    dbContext.TapDoans.Remove(oldList);
                }
            }

            dbContext.SaveChanges();

            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }

    }
}
