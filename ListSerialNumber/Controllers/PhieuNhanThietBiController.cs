using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using ListClass.Dao;
using ListClass.Model;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class PhieuNhanThietBiController : AccountLoginController
    {
        private readonly PhieuNhanThietBiDAO phieunhanthietbiDAO = new PhieuNhanThietBiDAO();
        private readonly ListNumberDAO listNumberDAO = new ListNumberDAO(); 
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public JsonResult GetCustomerInfo(int id)
        {
            var listnumber = dbContext.ListNumbers.Find(id);
            if (listnumber == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var khachHang = dbContext.KhachHangs.FirstOrDefault(m => m.Id == listnumber.Id_KhachHang);
            if (khachHang == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var result = new
            {
                SerialNumber = listnumber.SerialNumber,
                Tenthietbi = listnumber.Tenthietbi,
                Model = listnumber.Model,
                MaKH = khachHang.MaKH,
                TenKH = khachHang.TenKH,
                DiaChi = khachHang.DiaChi,
                Email = khachHang.Email,
                SDT = khachHang.SDT,
                LienHe = khachHang.LienHe,
                Ghichu = khachHang.Ghichu,
                NhomNganh = khachHang.NhomNganh
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("NhanThietBi"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;
                var count = phieunhanthietbiDAO.getCountIndex();
                ViewBag.Count = count;
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                var list = phieunhanthietbiDAO.getlistIndex(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("ThemNhanThietBi"))
            { 
                ViewBag.SerialNumberList = new SelectList(listNumberDAO.getList("Index"), "Id", "SerialNumber");
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
        public ActionResult Create(PhieuNhanThietBi PhieuNhanThietBi)
        {
            if (!ModelState.IsValid)
            {
                return View(PhieuNhanThietBi);
            }
            PhieuNhanThietBi.SoYeuCau = phieunhanthietbiDAO.Layphieutudong();
            PhieuNhanThietBi.Ngaytao = DateTime.Now;
            PhieuNhanThietBi.Status = 1;
            PhieuNhanThietBi.Nguoitao = Session["Name"]?.ToString();
            phieunhanthietbiDAO.Insert(PhieuNhanThietBi);
            TempData["Message"] = "Thêm thành công";
            ViewBag.SerialNumberList = new SelectList(listNumberDAO.getList("Index"), "Id", "SerialNumber");
            return RedirectToAction("Index");
        } 
        public ActionResult XoaNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaNhanThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieunhanthietbiDAO.GetRowsByIds(idList); 
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
        public ActionResult XoaNhieu(List<PhieuNhanThietBi> ids)
        {
            foreach (PhieuNhanThietBi listNumber in ids)
            {
                var oldlist = dbContext.PhieuNhanThietBis.Find(listNumber.Id);
                dbContext.PhieuNhanThietBis.Remove(oldlist);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công"; 
            return RedirectToAction("Index");
        }
        public ActionResult EditNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaNhanThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieunhanthietbiDAO.GetRowsByIds(idList); 
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.SerialNumberList = new SelectList(listNumberDAO.getList("Index"), "Id", "SerialNumber");
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
        public ActionResult EditNhieu(List<PhieuNhanThietBi> ids)
        { 
            foreach (PhieuNhanThietBi id in ids)
            {
                PhieuNhanThietBi oldlist = dbContext.PhieuNhanThietBis.Find(id.Id); 
                oldlist.Id_ListNumber = id.Id_ListNumber;
                oldlist.SoYeuCau = id.SoYeuCau;
                oldlist.SoNhanDang = id.SoNhanDang;
                oldlist.NguoiThucHien = id.NguoiThucHien;
                oldlist.HienTrang = id.HienTrang;
                oldlist.GhiChu = id.GhiChu;
                oldlist.NgayThucHien = id.NgayThucHien;
                oldlist.Ngaytao = DateTime.Now;
                oldlist.Nguoitao = Session["Name"].ToString();
                oldlist.Status = 1;
                dbContext.Entry(oldlist).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            TempData["Message"] = "Chỉnh sửa thành công";
            ViewBag.SerialNumberList = new SelectList(listNumberDAO.getList("Index"), "Id", "SerialNumber");
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (HasAccessAd() || HasAccessUser("NhanThietBi"))
            {
                var list = phieunhanthietbiDAO.getlistDetails(id);
                return View("Details", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult chitiet(int? id)
        {
            if (HasAccessAd() || HasAccessUser("NhanThietBi"))
            {
                var list = phieunhanthietbiDAO.getlistDetails(id);
                return View("chitiet", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult InPDF(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfNhanThietBi"))
            {
                var list = phieunhanthietbiDAO.getlistDetails(id);
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }

        } 
        public ActionResult ExportPdf(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfNhanThietBi"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                            {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Landscape,
                                MarginLeft = 80,
                                MarginRight = 80,
                                MarginTop = 30,
                                MarginBottom = 30
                            }
                    };

                    var list = phieunhanthietbiDAO.getlistDetails(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/PhieuNhanThietBi/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "PhieuNhanThietBi.pdf";

                    using (var stream = new MemoryStream())
                    {
                        doc.Save(stream);
                        doc.Close();

                        stream.Position = 0;

                        return File(stream.ToArray(), "application/pdf", fileName);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
    }
}
