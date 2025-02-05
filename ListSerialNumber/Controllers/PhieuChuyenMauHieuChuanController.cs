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
using ClosedXML.Excel;
using System.IO;
using SelectPdf;
namespace ListSerialNumber.Controllers
{
    public class PhieuChuyenMauHieuChuanController : AccountLoginController
    {
        private readonly PhieuChuyenMauHieuChuanDAO phieuchuyenmauhieuchuanDAO = new PhieuChuyenMauHieuChuanDAO(); 
        private readonly PhieuNhanThietBiDAO phieunhanthietbiDAO = new PhieuNhanThietBiDAO(); 
        private readonly ListDBContext dbContext = new ListDBContext();
        public JsonResult GetCustomerInfo(int id)
        {
            var phieunhan = dbContext.PhieuNhanThietBis.Find(id);
            var listnumber = dbContext.ListNumbers.FirstOrDefault(m => m.Id == phieunhan.Id_ListNumber);
            var khachHang = dbContext.KhachHangs.FirstOrDefault(m => m.Id == listnumber.Id_KhachHang);
            if (phieunhan == null || listnumber == null || khachHang == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            var result = new
            {
                SerialNumber = listnumber.SerialNumber,
                Tenthietbi = listnumber.Tenthietbi,
                Hang = listnumber.Hang,
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
            if (HasAccessAd() || HasAccessUser("ChuyenThietBi"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;
                var count = phieuchuyenmauhieuchuanDAO.getCountIndex();
                ViewBag.Count = count;
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                var list = phieuchuyenmauhieuchuanDAO.getlistIndex(Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Details(int? id)
        {
            if (HasAccessAd() || HasAccessUser("ChuyenThietBi"))
            { 
                var list = phieuchuyenmauhieuchuanDAO.getlistDetails(id);
                return View("Details", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult chitiet(int? id)
        {
            if (HasAccessAd() || HasAccessUser("ChuyenThietBi"))
            {
                var list = phieuchuyenmauhieuchuanDAO.getlistDetails(id);
                return View("chitiet", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemChuyenThietBi"))
            { 
                ViewBag.SoYeuCauList = new SelectList(phieunhanthietbiDAO.getList("Index"), "Id", "SoNhanDang");
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
        public ActionResult Create(PhieuChuyenMauHieuChuan PhieuChuyenMauHieuChuans)
        {
            if (!ModelState.IsValid)
            {
                return View(PhieuChuyenMauHieuChuans);
            }
            PhieuChuyenMauHieuChuans.MaSoMau = phieuchuyenmauhieuchuanDAO.Layphieutudong();
            PhieuChuyenMauHieuChuans.NgayTaoPhieu = DateTime.Now;
            PhieuChuyenMauHieuChuans.Status = 1;
            PhieuChuyenMauHieuChuans.NguoiTao = Session["Name"]?.ToString();
            phieuchuyenmauhieuchuanDAO.Insert(PhieuChuyenMauHieuChuans);
            TempData["Message"] = "Thêm thành công";
            ViewBag.SoYeuCauList = new SelectList(phieunhanthietbiDAO.getList("Index"), "Id", "SoNhanDang");
            return RedirectToAction("Index");
        } 
        public ActionResult XoaNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaChuyenThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuchuyenmauhieuchuanDAO.GetRowsByIds(idList);
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
        public ActionResult XoaNhieu(List<PhieuChuyenMauHieuChuan> ids)
        {
            foreach (PhieuChuyenMauHieuChuan listNumber in ids)
            {
                var oldlist = dbContext.PhieuChuyenMauHieuChuans.Find(listNumber.Id);
                dbContext.PhieuChuyenMauHieuChuans.Remove(oldlist);
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
            if (HasAccessAd() || HasAccessUser("SuaChuyenThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuchuyenmauhieuchuanDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.SoYeuCauList = new SelectList(phieunhanthietbiDAO.getList("Index"), "Id", "SoNhanDang");
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
        public ActionResult EditNhieu(List<PhieuChuyenMauHieuChuan> ids)
        {
            foreach (PhieuChuyenMauHieuChuan id in ids)
            {
                PhieuChuyenMauHieuChuan oldlist = dbContext.PhieuChuyenMauHieuChuans.Find(id.Id);
                oldlist.Id_PhieuNhanThietBi = id.Id_PhieuNhanThietBi;
                oldlist.MaSoMau = id.MaSoMau;
                oldlist.NguoiDuocPhanCong = id.NguoiDuocPhanCong;
                oldlist.NguoiNhan = id.NguoiNhan;
                oldlist.PhuongPhamHieuChuan = id.PhuongPhamHieuChuan;
                oldlist.NgayNhan = id.NgayNhan;
                oldlist.ThongSoHieuChuan = id.ThongSoHieuChuan;
                oldlist.NgayTraBaoCao = id.NgayTraBaoCao;
                oldlist.NgayTaoPhieu = DateTime.Now;
                oldlist.NguoiTao = Session["Name"].ToString();
                oldlist.Status = 1;
                dbContext.Entry(oldlist).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            TempData["Message"] = "Chỉnh sửa thành công";
            ViewBag.SoYeuCauList = new SelectList(phieunhanthietbiDAO.getList("Index"), "Id", "SoNhanDang");
            return RedirectToAction("Index");
        }
        public ActionResult InPDF(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfChuyenThietBi"))
            {
                var list = phieuchuyenmauhieuchuanDAO.getlistDetails(id);
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
            if (HasAccessAd() || HasAccessUser("InPdfChuyenThietBi"))
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

                    var list = phieuchuyenmauhieuchuanDAO.getlistDetails(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/PhieuChuyenMauHieuChuan/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "phieuchuyenmauhieuchuan.pdf";

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
