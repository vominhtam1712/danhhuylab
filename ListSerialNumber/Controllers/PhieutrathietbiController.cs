using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Wordprocessing;
using ListClass.Dao;
using ListClass.Model;
using PagedList;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class PhieutrathietbiController : AccountLoginController
    {
        private readonly PhieutrathietbiDAO phieutrathietbiDAO = new PhieutrathietbiDAO();
        private readonly GiaychungnhanDAO gcnDAO = new GiaychungnhanDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public JsonResult GetCustomerInfo(int id)
        {
            var gcn = dbContext.Giaychungnhans.Find(id);
            var bbhc = dbContext.BienBanHieuChuans.FirstOrDefault(m => m.Id == gcn.Id_BienBanHieuChuan);
            var pc = dbContext.PhanCongs.FirstOrDefault(m => m.Id == bbhc.Id_PhanCong);
            var pychc = dbContext.Phieuyeucauhieuchuans.FirstOrDefault(m => m.Id == pc.Id_pychc);
            var pntb = dbContext.PhieuNhanThietBis.FirstOrDefault(m => m.Id == pychc.Id_phieunhanthietbi);
            var lms = dbContext.ListNumbers.FirstOrDefault(m => m.Id == pntb.Id_ListNumber);
            var kh = dbContext.KhachHangs.FirstOrDefault(m => m.Id == lms.Id_KhachHang);
            if (gcn == null || bbhc == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var result = new
            {
                SerialNumber = lms.SerialNumber,
                Tenthietbi = lms.Tenthietbi,
                Hang = lms.Hang,
                Model = lms.Model,
                MaKH = kh.MaKH,
                TenKH = kh.TenKH,
                DiaChi = kh.DiaChi,
                NhomNganh = kh.NhomNganh
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("TraThietBi")) 
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;
                var count = phieutrathietbiDAO.getCountIndex();
                ViewBag.Count = count;
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                var list = phieutrathietbiDAO.getlistIndex(Name, pageSize, pageNumber);
                return View("Index", list);
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
            if (HasAccessAd() || HasAccessUser("InPdfTraThietBi"))
            {
                var list = phieutrathietbiDAO.getlistDetails(id);
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
            if (HasAccessAd() || HasAccessUser("InPdfTraThietBi"))
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

                    var list = phieutrathietbiDAO.getlistDetails(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderPartialTostring("~/Views/Phieutrathietbi/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Bienbanbangiaotrathietbi.pdf";

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
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemTraThietBi"))
            { 
                ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
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
        public ActionResult Create(Phieutrathietbi Phieutrathietbis)
        {
            if (!ModelState.IsValid)
            {
                return View(Phieutrathietbis);
            }
            Phieutrathietbis.MaPT = phieutrathietbiDAO.Layphieutudong();
            Phieutrathietbis.NgayTao = DateTime.Now;
            Phieutrathietbis.status = 1;
            Phieutrathietbis.NguoiTao = Session["Name"]?.ToString();
            phieutrathietbiDAO.Insert(Phieutrathietbis);
            TempData["Message"] = "Thêm thành công";
            ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
            return RedirectToAction("Index");
        } 
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaTraThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieutrathietbiDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
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
        public ActionResult Edit(List<Phieutrathietbi> Phieutrathietbis)
        { 
            foreach (Phieutrathietbi Phieutrathietbi in Phieutrathietbis)
            {
                Phieutrathietbi oldlist = dbContext.Phieutrathietbis.Find(Phieutrathietbi.Id);
                oldlist.Id_Giaychungnhan = Phieutrathietbi.Id_Giaychungnhan; 
                oldlist.MaPT = Phieutrathietbi.MaPT; 
                oldlist.PhuongThucGiaoTra = Phieutrathietbi.PhuongThucGiaoTra;
                oldlist.TrangThaiThietbi = Phieutrathietbi.TrangThaiThietbi;
                oldlist.NgayThucHien = Phieutrathietbi.NgayThucHien;
                oldlist.Ghichu = Phieutrathietbi.Ghichu;
                oldlist.NgayTao = DateTime.Now;
                oldlist.NguoiTao = Session["Name"].ToString();
                oldlist.status = 1;
                dbContext.SaveChanges();

            }
            TempData["Message"] = "Chỉnh sửa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (HasAccessAd() || HasAccessUser("TraThietBi"))
            {  
                var list = phieutrathietbiDAO.getlistDetails(id);
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
            if (HasAccessAd() || HasAccessUser("TraThietBi"))
            {
                var list = phieutrathietbiDAO.getlistDetails(id);
                return View("chitiet", list);
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
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToAction("Index", "Phieutrathietbi");
            }
            if (HasAccessAd() || HasAccessUser("XoaTraThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieutrathietbiDAO.GetRowsByIds(idList);
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
        public ActionResult Delete(List<Phieutrathietbi> ids)
        { 
            foreach (Phieutrathietbi listNumber in ids)
            {
                var oldlist = dbContext.Phieutrathietbis.Find(listNumber.Id);
                dbContext.Phieutrathietbis.Remove(oldlist);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}
