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
using DocumentFormat.OpenXml.Wordprocessing;

namespace ListSerialNumber.Controllers
{
    public class PhieuyeucauhieuchuanDoiController : AccountLoginController
    {
        private readonly PhieuyeucauhieuchuanDoiDAO phieuyeucauhieuchuandoiDAO = new PhieuyeucauhieuchuanDoiDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("TiepNhan")) 
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1; 
                var count = phieuyeucauhieuchuandoiDAO.GetCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber; 
                var list = phieuyeucauhieuchuandoiDAO.GetPagedList(Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }    
            
        }
        public ActionResult Trash(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("TiepNhan"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;
                var count = phieuyeucauhieuchuandoiDAO.GetCountTrash();
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                var list = phieuyeucauhieuchuandoiDAO.GetPagedListTrash(Name, pageSize, pageNumber);
                return View("Trash", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }

        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaTiepNhan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuandoiDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<PhieuyeucauhieuchuanDoi> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.PhieuyeucauhieuchuanDois.Find(listNumber.Id);
                dbContext.PhieuyeucauhieuchuanDois.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }  
        public ActionResult Status(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("TiepNhan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuandoiDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = (listNumber.Status == 1) ? 2 : 1;
                    phieuyeucauhieuchuandoiDAO.Update(listNumber);
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Đã duyệt";
                return RedirectToAction("TaoNhieu", "PhanCong", new { ids = string.Join(",", ids) });
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }    
            
        }  
    }
}
