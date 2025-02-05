using ListClass.Dao;
using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class KeHoachHieuChuan_FormController : AccountLoginController
    {
        private readonly KeHoachHieuChuan_Form_DAO khhcform_DAO = new KeHoachHieuChuan_Form_DAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("KeHoacHieuChuan"))
            {
                int pageSize = 10;
                int pageNumber = page ?? 1; 
                var count = khhcform_DAO.GetCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;

                var list = khhcform_DAO.getListIndex(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("ThemKeHoacHieuChuan"))
            {
                return View("Create");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }    
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KeHoachHieuChuan_Form listNumber)
        {
            if (ModelState.IsValid)
            {
                listNumber.MaPhieu = khhcform_DAO.Layphieutudong();
                listNumber.NgayTao = DateTime.Now;
                listNumber.Status = 1;
                listNumber.NguoiTao = Session["Name"].ToString();
                khhcform_DAO.Insert(listNumber);
                TempData["Message"] = "Tạo thành công";
                return RedirectToAction("Index", "KeHoachHieuChuan_Form");
            }
            return View(listNumber);
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaKeHoacHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khhcform_DAO.GetRowsByIds(idList);
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
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
        public ActionResult Delete(List<KeHoachHieuChuan_Form> ids)
        {
            foreach (KeHoachHieuChuan_Form khhc in ids)
            {
                var oldlist = dbContext.KeHoachHieuChuan_Forms.Find(khhc.Id);
                dbContext.KeHoachHieuChuan_Forms.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        } 
    }
}