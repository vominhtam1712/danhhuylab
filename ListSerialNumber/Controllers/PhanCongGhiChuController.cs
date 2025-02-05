using ListClass.Dao;
using ListClass.Model;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class PhanCongGhiChuController : AccountLoginController
    {
        private readonly PhanCongGhiChuDAO pcghichuDAO = new PhanCongGhiChuDAO();
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult ThemGhiChu(string id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien")) 
            {
                var idList = ParseIds(id);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

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
        public ActionResult ThemGhiChu(List<PhanCongGhiChu> ids)
        {
            var Id = ids.First().Id_PhanCong;
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
            }
            var userName = Session["Name"]?.ToString();
            if (string.IsNullOrEmpty(userName))
            {
                TempData["Message"] = "Thông tin người tạo không hợp lệ.";
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
            }
            if(pcghichuDAO.CheckExists(Id))
            {
                TempData["Message"] = "Chỉ được tạo ghi chú 1 lần!, hãy vào chỉnh sửa!.";
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
            }    
            foreach (var phancong in ids)
            {
                var oldlist = new PhanCongGhiChu
                { 
                    Id_PhanCong = phancong.Id_PhanCong,
                    GhiChu = phancong.GhiChu,
                    NgayTao = DateTime.Now,
                    NguoiTao = userName, 
                };
                pcghichuDAO.Insert(oldlist);
            }
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult Chinhsua(string id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaThucHien"))
            {
                var idList = ParseIds(id);
                var listNumbers = pcghichuDAO.GetRowsByIds(idList);

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
        public ActionResult Chinhsua(List<PhanCongGhiChu> ids)
        {
            var Id = ids.First().Id_PhanCong;
            foreach (var phancong in ids)
            {
                PhanCongGhiChu oldlist = dbContext.PhanCongGhiChus.Find(phancong.Id);
                oldlist.Id_PhanCong = phancong.Id_PhanCong;
                oldlist.GhiChu = phancong.GhiChu; 
                oldlist.NgayTao = DateTime.Now;
                oldlist.NguoiTao = Session["Name"]?.ToString(); 
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công"; 
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult Xoa(string id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(id);
                var listNumbers = pcghichuDAO.GetRowsByIds(idList);

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
        public ActionResult Xoa(List<PhanCongGhiChu> ids)
        {
            var Id = ids.First().Id_PhanCong;
            foreach (PhanCongGhiChu listNumber in ids)
            {
                var oldlist = dbContext.PhanCongGhiChus.Find(listNumber.Id);
                dbContext.PhanCongGhiChus.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult InPDF(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfThucHien"))
            {
                var list = pcghichuDAO.getlistJoin(id);
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
            if (HasAccessAd() || HasAccessUser("InPdfThucHien"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                            {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Portrait,
                                MarginLeft = 40,
                                MarginRight = 40,
                                MarginTop = 40,
                                MarginBottom = 40
                            }
                    };

                    var list = pcghichuDAO.getlistJoin(id);
                    if (list == null || !list.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderViewToString("~/Views/PhanCongGhiChu/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Ghichuquatrinhhieuchuanthietbi.pdf";

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
                TempData["Message"] = "Bạn không có quyền InPDF";
                return RedirectToReferrer();
            }
        } 
    }
}