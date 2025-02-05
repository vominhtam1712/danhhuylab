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
    public class KhuVucController : AccountLoginController
    {
        private readonly KhuVucDAO khuvucDAO = new KhuVucDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("KhuVuc"))
            {
                const int pageSize = 10;
                int pageNumber = page ?? 1;
                var count = khuvucDAO.getCountIndex();
                ViewBag.Count = count;
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                var list = khuvucDAO.getList(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("ThemKhuVuc"))
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
        public ActionResult Create(KhuVuc tapdoan)
        {
            if (!ModelState.IsValid)
            {
                return View(tapdoan);
            }

            tapdoan.Status = 1;
            khuvucDAO.Insert(tapdoan);

            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "KhuVuc");
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaKhuVuc"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khuvucDAO.GetRowsByIds(idList);

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
        public ActionResult Edit(List<KhuVuc> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.KhuVucs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KhachHang in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KhachHang.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Tenkhuvuc = KhachHang.Tenkhuvuc;
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
            if (HasAccessAd() || HasAccessUser("SuaKhuVuc"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khuvucDAO.GetRowsByIds(idList);

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
        public ActionResult ChinhSua(List<KhuVuc> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.KhuVucs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KhachHang in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KhachHang.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Tenkhuvuc = KhachHang.Tenkhuvuc;
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
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaKhuVuc"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khuvucDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
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
        public ActionResult Delete(List<KhuVuc> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Lỗ trong quá trình xóa.";
                return RedirectToAction("Index", "Home");
            }
            var khuvucNamesWithKhachHang = khuvucDAO.GetKhuVucNamesWithKhachHang(ids);
            if (khuvucDAO.CheckExists(ids))
            {
                string message = " Khu vực ' "
                       + string.Join(" , ", khuvucNamesWithKhachHang) + " ' đang có khách hàng không thể xóa! ";
                TempData["Message"] = message;
                return RedirectToAction("Index");
            }
            foreach (var listNumber in ids)
            {
                var oldList = dbContext.KhuVucs.Find(listNumber.Id);
                dbContext.KhuVucs.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
