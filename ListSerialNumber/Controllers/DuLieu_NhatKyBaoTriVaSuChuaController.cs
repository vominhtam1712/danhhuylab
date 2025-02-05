using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListClass.Model;
using ListClass.Dao;

namespace ListSerialNumber.Controllers
{
    public class DuLieu_NhatKyBaoTriVaSuChuaController : AccountLoginController
    {
        private readonly NhatKyBaoTriVaSuChuaDAO nhatKyBaoTriVaSuChuaDAO = new NhatKyBaoTriVaSuChuaDAO();
        private readonly DuLieu_NhatKyBaoTriVaSuChua_DAO dulieuDAO = new DuLieu_NhatKyBaoTriVaSuChua_DAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaNhatKy"))
            {
                var idList = ParseIds(ids);
                var listNumbers = dulieuDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<DuLieu_NhatKyBaoTriVaSuChua> ids)
        {
            var id = ids.FirstOrDefault()?.Id_NhatKyBaoTriVaSuChua;
            foreach (DuLieu_NhatKyBaoTriVaSuChua Ids in ids)
            {
                var oldlist = dbContext.DuLieu_NhatKyBaoTriVaSuChua.Find(Ids.Id);
                dbContext.DuLieu_NhatKyBaoTriVaSuChua.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "NhatKyBaoTriVaSuChua", new { id });
        }
        public ActionResult Create(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemNhatKy"))
            {
                var idList = ParseIds(ids);
                var listNumbers = nhatKyBaoTriVaSuChuaDAO.GetRowsByIds(idList);
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }     
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<DuLieu_NhatKyBaoTriVaSuChua> ids)
        {
            var Id = ids.First().Id_NhatKyBaoTriVaSuChua;
            foreach (var id in ids)
            {
                var oldlist = new DuLieu_NhatKyBaoTriVaSuChua
                {
                    Id_NhatKyBaoTriVaSuChua = id.Id_NhatKyBaoTriVaSuChua,
                    Ngay = id.Ngay,
                    MoTaSuCo = id.MoTaSuCo,
                    HanhDongKhacPhuc = id.HanhDongKhacPhuc,
                    KetQua = id.KetQua,
                    NguoiSuaChua = id.NguoiSuaChua,
                    NguoiKiemTra = id.NguoiKiemTra,
                    Status = 1
                };
                dulieuDAO.Insert(oldlist);
            }
            TempData["Message"] = "Tạo thành công";
            return RedirectToAction("Details", "NhatKyBaoTriVaSuChua", new { id = Id });
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaNhatKy"))
            {
                var idList = ParseIds(ids);
                var listNumbers = dulieuDAO.GetRowsByIds(idList);
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
        public ActionResult Edit(List<DuLieu_NhatKyBaoTriVaSuChua> ids)
        {
            var Id = ids.First().Id_NhatKyBaoTriVaSuChua;
            foreach (DuLieu_NhatKyBaoTriVaSuChua id in ids)
            {
                DuLieu_NhatKyBaoTriVaSuChua oldlist = dbContext.DuLieu_NhatKyBaoTriVaSuChua.Find(id.Id);
                oldlist.Id_NhatKyBaoTriVaSuChua = id.Id_NhatKyBaoTriVaSuChua;
                oldlist.Ngay = id.Ngay;
                oldlist.MoTaSuCo = id.MoTaSuCo;
                oldlist.HanhDongKhacPhuc = id.HanhDongKhacPhuc;
                oldlist.KetQua = id.KetQua;
                oldlist.NguoiSuaChua = id.NguoiSuaChua;
                oldlist.NguoiKiemTra = id.NguoiKiemTra; 
                oldlist.Status = 1;
                dbContext.Entry(oldlist).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            TempData["Message"] = "Chỉnh sửa thành công"; 
            return RedirectToAction("Details", "NhatKyBaoTriVaSuChua", new { id=Id });
        } 
    }
}
