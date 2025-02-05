using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ListClass.Dao;
using ListClass.Model;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class NhatKyBaoTriVaSuChuaController : AccountLoginController
    {
        private readonly NhatKyBaoTriVaSuChuaDAO nhatKyBaoTriVaSuChuaDAO = new NhatKyBaoTriVaSuChuaDAO();
        private readonly DanhMucThietBiDAO dmtbDAO = new DanhMucThietBiDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
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
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("NhatKy")) 
            {
                int pageSize = 10;
                int pageNumber = page ?? 1; 
                var count = nhatKyBaoTriVaSuChuaDAO.getCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber; 
                var list = nhatKyBaoTriVaSuChuaDAO.getlistIndex(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("ThemNhatKy"))
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
        public ActionResult Create(NhatKyBaoTriVaSuChua danhmucthietbi)
        {
            if (!ModelState.IsValid)
            {
                return View(danhmucthietbi);
            }
            danhmucthietbi.NgayTao = DateTime.Now;
            danhmucthietbi.Status = 1;
            danhmucthietbi.NguoiTao = Session["Name"]?.ToString();
            nhatKyBaoTriVaSuChuaDAO.Insert(danhmucthietbi);
            TempData["Message"] = "Thêm thành công";
            ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
            return RedirectToAction("Index");
        } 
        public ActionResult Details(int? id)
        {
            if (HasAccessAd() || HasAccessUser("NhatKy"))
            {
                var ID = nhatKyBaoTriVaSuChuaDAO.getListId(id);
                var recordId = ID.FirstOrDefault()?.Id;
                var TenThietBi = ID.FirstOrDefault()?.DanhMucThietBi.TenThietBi;
                var HangSX = ID.FirstOrDefault()?.DanhMucThietBi.HangSX;
                var Serial = ID.FirstOrDefault()?.DanhMucThietBi.Serial;
                var Img = ID.FirstOrDefault()?.DanhMucThietBi.Img;
                var NgayLapDat = ID.FirstOrDefault()?.NgayLapDat;
                var NhatKyBaoTri = ID.FirstOrDefault()?.NhatKyBaoTri;
                ViewBag.Id = recordId;
                ViewBag.TenThietBi = TenThietBi;
                ViewBag.HangSX = HangSX;
                ViewBag.Serial = Serial;
                ViewBag.NgayLapDat = NgayLapDat.Value.ToString("dd/MM/yyyy");
                ViewBag.NhatKyBaoTri = NhatKyBaoTri;
                ViewBag.Img = Img; 
                var list = nhatKyBaoTriVaSuChuaDAO.getlistId(id);
                return View("Details", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }    
            
        }
        public ActionResult InPdf(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfNhatKy"))
            {
                var list = nhatKyBaoTriVaSuChuaDAO.getlistInPdf(id);
                return View("InPdf", list);
            }  
            else
            {
                TempData["Message"] = "Bạn không có quyền InPdf";
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
            if (HasAccessAd() || HasAccessUser("InPdfNhatKy"))
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
                        MarginBottom =50
                   }
                    };

                    var list = nhatKyBaoTriVaSuChuaDAO.getlistInPdf(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderPartialTostring("~/Views/NhatKyBaoTriVaSuChua/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "baotrivasuachuathietbis.pdf";

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
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaNhatKy"))
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
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToReferrer();
            }    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<NhatKyBaoTriVaSuChua> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.NhatKyBaoTriVaSuChuas.Find(listNumber.Id); 
                    dbContext.NhatKyBaoTriVaSuChuas.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("EditNhatKy"))
            {
                var idList = ParseIds(ids);
                var listNumbers = nhatKyBaoTriVaSuChuaDAO.GetRowsByIds(idList);
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
        public ActionResult Edit(List<NhatKyBaoTriVaSuChua> ids)
        { 
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.NhatKyBaoTriVaSuChuas
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KeHoachHieuChuan in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KeHoachHieuChuan.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_DanhMucThietBi = KeHoachHieuChuan.Id_DanhMucThietBi;
                        existingRecord.NgayLapDat = KeHoachHieuChuan.NgayLapDat;
                        existingRecord.NhatKyBaoTri = KeHoachHieuChuan.NhatKyBaoTri;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
                return RedirectToAction("Index", "NhatKyBaoTriVaSuChua");
            }
        }
    }
}
