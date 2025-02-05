using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Geom;
using ListClass.Dao;
using ListClass.Model;

namespace ListSerialNumber.Controllers
{
    public class PhanCongController : AccountLoginController
    {
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        private readonly CompanyDAO companyDAO = new CompanyDAO();
        private readonly Join_PhanCong_NhanVien_DAO joinphancongnhanvienDAO = new Join_PhanCong_NhanVien_DAO();
        private readonly PhieuyeucauhieuchuanDoiDAO phieuyeucauhieuchuandoiDAO = new PhieuyeucauhieuchuanDoiDAO();
        public ActionResult TaoNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemPhanCong"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuandoiDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.UserList = new SelectList(companyDAO.getList("Index"), "Id", "Name");
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
        public ActionResult TaoNhieu(List<PhanCong> listnumbers)
        {
            if (listnumbers == null || !listnumbers.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("Index");
            }
            var userName = Session["Name"]?.ToString();
            if (string.IsNullOrEmpty(userName))
            {
                TempData["Message"] = "Thông tin người tạo không hợp lệ.";
                return RedirectToAction("Index");
            }

            foreach (var phancong in listnumbers)
            {
                if (phancongDAO.CheckSerialNumberExists(phancong.Id_pychc))
                {
                    TempData["Message"] = "Phiếu đã phân công";
                    return RedirectToAction("Trash", "PhieuyeucauhieuchuanDoi");
                }
                else
                {
                    var oldlist = new PhanCong
                    {
                        MaPC = phancongDAO.Layphieutudong(),
                        Id_pychc = phancong.Id_pychc,
                        Id_NV = phancong.Id_NV,
                        NgayTao = DateTime.Now,
                        NguoiTao = userName,
                        Status = 1
                    };
                    phancongDAO.Insert(oldlist);
                }
            }
            TempData["Message"] = "Successful";
            ViewBag.UserList = new SelectList(companyDAO.getList("Index"), "Id", "Name");
            return RedirectToAction("Index", "PhanCong");
        }
        public ActionResult Index(string Name ,int? page)
        {
            if (HasAccessAd() || HasAccessUser("PhanCong"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1; 
                var count = joinphancongnhanvienDAO.GetCountIndex1();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber; 
                var list = joinphancongnhanvienDAO.getlistIndex(Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
            
        }
        public ActionResult Trash(string Name,int? page)
        {
            if (HasAccessAd() || HasAccessUser("PhanCong"))
            {
                int pageSize = 10;
                int pageNumber = page ?? 1; 
                var count = joinphancongnhanvienDAO.GetCountTrash();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber; 
                var list = joinphancongnhanvienDAO.getlistTrash(Name, pageSize, pageNumber);
                return View("Trash", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }    
            
        }
        private int GetCurrentCompanyId()
        {
            if (Session["CompanyId"] != null && int.TryParse(Session["CompanyId"].ToString(), out int companyId))
            {
                return companyId;
            }

            throw new Exception("CompanyId is missing or invalid.");
        }

        public ActionResult Thuchien(string Name, int? page)
        {
            int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
            int pageNumber = page ?? 1;
             
            if (HasAccessAd() || HasAccessUser("ThucHien"))
            {
                int currentCompanyId = 0;

                if (!HasAccessAd())  
                {
                    try
                    {
                        currentCompanyId = GetCurrentCompanyId();
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                IEnumerable<Join_PhanCong_NhanVien> list;
                try
                {
                    list = HasAccessAd()  
                        ? joinphancongnhanvienDAO.getlistjoin(Name, pageSize, pageNumber)
                        : joinphancongnhanvienDAO.getlistjoin(Name, pageSize, pageNumber, currentCompanyId);
                }
                catch (Exception)
                {
                    TempData["Message"] = "Bạn không thể truy cập";
                    return RedirectToAction("Index", "Home");
                }
                return View("Thuchien", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToAction("Index", "Home");
            }
        } 
        public ActionResult EditNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaPhanCong"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.UserList = new SelectList(companyDAO.getList("Index"), "Id", "Name");
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
        public ActionResult EditNhieu(List<PhanCong> phancongs)
        {
            foreach (var phancong in phancongs)
            {
                PhanCong oldlist = dbContext.PhanCongs.Find(phancong.Id);
                oldlist.MaPC = phancong.MaPC;
                oldlist.Id_pychc = phancong.Id_pychc;
                oldlist.Id_NV = phancong.Id_NV;
                oldlist.NgayTao = DateTime.Now;
                oldlist.NguoiTao = Session["Name"]?.ToString();
                oldlist.Status = 1;
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công";
            ViewBag.UserList = new SelectList(companyDAO.getList("Index"), "Id", "Name");
            return RedirectToAction("Index", "PhanCong");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            PhanCong phancong = phancongDAO.getRow(id);
            if (phancong == null)
            {
                TempData["Message"] = "Không tồn tại";
                return RedirectToAction("ThucHien", "PhanCong");
            }
            if (phancong.Status == 2)
            {
                TempData["Message"] = "Không thể sửa đổi";
                return RedirectToAction("ThucHien", "PhanCong");
            }
            else
            {
                phancong.Status = (phancong.Status == 1) ? 2 : 1;
                phancongDAO.Update(phancong);
                TempData["Message"] = "Đã hoàn thành";
                return RedirectToAction("ThucHien", "PhanCong");
            }
        }
        public ActionResult Huy(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            PhanCong phancong = phancongDAO.getRow(id);
            if (phancong == null)
            {
                TempData["Message"] = "Không tồn tại";
                return RedirectToAction("ThucHien", "PhanCong");
            } 
            else
            {
                phancong.Status = (phancong.Status == 1) ? 3 : 1;
                phancongDAO.Update(phancong);
                TempData["Message"] = "Đã hủy";
                return RedirectToAction("ThucHien", "PhanCong");
            }
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaPhanCong"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList); 
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.UserList = new SelectList(companyDAO.getList("Index"), "Id", "Name");
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToReferrer();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<PhanCong> ids)
        {
            foreach (PhanCong listNumber in ids)
            {
                var oldlist = dbContext.PhanCongs.Find(listNumber.Id);
                dbContext.PhanCongs.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        } 
    }
}
