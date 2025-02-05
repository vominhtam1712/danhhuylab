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
    public class BaoCaoDanhGiaController : AccountLoginController
    {
        private readonly BaoCaoDanhGiaDAO bcdgDAO = new BaoCaoDanhGiaDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        private readonly DanhMucThietBiDAO dmtbDAO = new DanhMucThietBiDAO();
        public JsonResult GetCustomerInfo(int id)
        {
            var dmtb = dbContext.DanhMucThietBis.Find(id);
            if (dmtb == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var result = new
            {
                Serial = dmtb.Serial,
                MaThietBi = dmtb.MaThietBi,
                TenThietBi = dmtb.TenThietBi,
                HangSX = dmtb.HangSX,
                NgayDuaVaoSuDung = dmtb.NgayDuaVaoSuDung,
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemBaoCaoDanhGia")) 
            { 
                ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
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
        public ActionResult Create(BaoCaoDanhGia danhmucthietbi)
        {
            if (!ModelState.IsValid)
            {
                return View(danhmucthietbi);
            }  
            danhmucthietbi.NgayTao = DateTime.Now;
            danhmucthietbi.Status = 1;
            danhmucthietbi.NguoiTao = Session["Name"]?.ToString();
            bcdgDAO.Insert(danhmucthietbi);
            TempData["Message"] = "Thêm thành công";
            ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaBaoCaoDanhGia"))
            { 
                var idList = ParseIds(ids);
                var listNumbers = bcdgDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
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
        public ActionResult Edit(List<BaoCaoDanhGia> ids)
        {
            var Id = ids.First().Id;
            foreach (BaoCaoDanhGia BaoCaoDanhGias in ids)
            {
                BaoCaoDanhGia oldlist = dbContext.BaoCaoDanhGias.Find(BaoCaoDanhGias.Id);
                oldlist.Id_DanhMucThietBi = BaoCaoDanhGias.Id_DanhMucThietBi;
                oldlist.ThietBi_HoaChatHieuChuan = BaoCaoDanhGias.ThietBi_HoaChatHieuChuan;
                oldlist.NgayHieuChuan = BaoCaoDanhGias.NgayHieuChuan;
                oldlist.NgayHieuChuanKeTiep = BaoCaoDanhGias.NgayHieuChuanKeTiep;
                oldlist.KetQuaHC = BaoCaoDanhGias.KetQuaHC;
                oldlist.KetLuan = BaoCaoDanhGias.KetLuan;
                oldlist.NgayTao = DateTime.Now;
                oldlist.NguoiTao = Session["Name"].ToString();
                oldlist.Status = 1;
                dbContext.Entry(oldlist).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            TempData["Message"] = "Chỉnh sửa thành công";
            ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
            return RedirectToAction("Index", "BaoCaoDanhGia", new { Id });
        }
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("BaoCaoDanhGia"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;

                var count = bcdgDAO.getCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                ViewBag.Count = count;

                var list = bcdgDAO.getlistIndex(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("BaoCaoDanhGia"))
            { 
                var list = bcdgDAO.getlistDetails(id);
                return View("Details", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Delete(string ids)
        { 
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaBaoCaoDanhGia"))
            { 
                var idList = ParseIds(ids);
                var listNumbers = bcdgDAO.GetRowsByIds(idList);
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
        public ActionResult Delete(List<BaoCaoDanhGia> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.BaoCaoDanhGias.Find(listNumber.Id); 
                dbContext.BaoCaoDanhGias.Remove(oldList); 
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult InPDF(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfBaoCaoDanhGia"))
            {
                var list = bcdgDAO.getlistDetails(id);
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
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
            if (HasAccessAd() || HasAccessUser("InPdfBaoCaoDanhGia"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                   {
                        PdfPageSize = PdfPageSize.A4,
                        PdfPageOrientation = PdfPageOrientation.Landscape,
                        MarginLeft = 70,
                        MarginRight = 70,
                        MarginTop = 30,
                        MarginBottom =40
                   }
                    };

                    var list = bcdgDAO.getlistDetails(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/BaoCaoDanhGia/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "baocaodanhgiathietbis.pdf";

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
                TempData["Message"] = "Bạn không có quyền InPdf";
                return RedirectToReferrer();
            }    
        }
    }
}