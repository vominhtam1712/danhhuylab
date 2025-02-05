using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Geom;
using ListClass.Dao;
using ListClass.Model;
using ListSerialNumber.Library;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
namespace ListSerialNumber.Controllers
{
    public class CompanysController : AccountLoginController
    {
        private readonly CompanyDAO companyDAO = new CompanyDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        private readonly ThanhVienDAO thanhvienDAO = new ThanhVienDAO(); 
        private readonly Md5 md5Hasher = new Md5(); 
        public ActionResult Index(string Name , int? page)
        {
            if (HasAccessAd() || HasAccessUser("TaiKhoan"))
            {
                int pageSize = 10;
                int pageNumber = page ?? 1;
                ViewBag.ListThanhVien = thanhvienDAO.getlist();
                var list = companyDAO.getList(Name , pageSize, pageNumber); 
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        }
        public ActionResult Trash(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("TaiKhoan"))
            {
                int pageSize = 10;
                int pageNumber = page ?? 1;
                var list = companyDAO.getListTrash(Name, pageSize, pageNumber);
                return View("Trash", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        }

        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemTaiKhoan"))
            {
                return View();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToAction("Khongtimthay", "Home");
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Create(Company company)
        {
            company.Password = md5Hasher.ComputeMd5Hash(company.Password);
            company.Status = 1;
            companyDAO.Insert(company);
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index", "Companys");
        } 
        public ActionResult Edit(int? id)
        {
            if (HasAccessAd() || HasAccessUser("PhanQuyen")) 
            { 
                var company = dbContext.Companys.FirstOrDefault(c => c.Id == id);
                if (company == null)
                {
                    TempData["Message"] = "Không có dữ liệu";
                    return RedirectToReferrer();
                }
             
                var roles = dbContext.Roles.ToList();
             
                var companyRoles = dbContext.CompanyRoles
                                            .Where(cr => cr.Id_Company == id)
                                            .Select(cr => cr.Id_Role)
                                            .ToList();
             
                var viewModel = new AssignRolesViewModel
                {
                    CompanyId = (int)id,
                    Roles = roles.Select(role => new RoleCheckboxViewModel
                    {
                        Id = role.Id,
                        NameRoles = role.NameRoles,
                        Nhom = role.Nhom,
                        IsSelected = companyRoles.Contains(role.Id)
                    }).ToList()
                };

                return View(viewModel);
                }
            else
            {
                TempData["Message"] = "Bạn không thể phân quyền";
                return RedirectToAction("Khongtimthay", "Home");
            }    

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, List<int> roles)
        {
            var company = dbContext.Companys.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                TempData["Message"] = "Không tìm thấy thành viên";
                return RedirectToAction("Index");
            }
             
            if (roles == null || roles.Count == 0)
            {
                var existingCompanyRoles = dbContext.CompanyRoles.Where(cr => cr.Id_Company == id).ToList();
                dbContext.CompanyRoles.RemoveRange(existingCompanyRoles);
                dbContext.SaveChanges();
                 
                TempData["Message"] = "Đã tắt quyền của thành viên";
                return RedirectToAction("Index");
            }
             
            var existingCompanyRolesToRemove = dbContext.CompanyRoles.Where(cr => cr.Id_Company == id).ToList();
            dbContext.CompanyRoles.RemoveRange(existingCompanyRolesToRemove);

            foreach (var roleId in roles)
            {
                var companyRole = new CompanyRoles
                {
                    Id_Company = id,
                    Id_Role = roleId,   
                    MaQuyen = dbContext.Roles.FirstOrDefault(r => r.Id == roleId)?.MaQuyen
                };
                dbContext.CompanyRoles.Add(companyRole);
            }

            dbContext.SaveChanges();

            TempData["Message"] = "Đã cập nhật quyền cho thành viên";
            return RedirectToAction("Index", "Companys");
        }
        public ActionResult DelTrash(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("VoHieuHoaTaiKhoan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = companyDAO.GetRowsByIds(idList);
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = listNumber.Status == 1 ? 2 : 1;
                    companyDAO.Update(listNumber);
                } 
                return Json(new { success = true, message = "Đã vô hiệu hóa tài khoản!" });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền xóa!" });
            }   
        }
        public ActionResult ReTrash(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("VoHieuHoaTaiKhoan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = companyDAO.GetRowsByIds(idList);
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }

                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = listNumber.Status == 2 ? 1 : 2;
                    companyDAO.Update(listNumber);
                }
                return Json(new { success = true, message = "Đã khôi phục!" }); 
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền khôi phục!" });
            } 
        }
        
    }
}