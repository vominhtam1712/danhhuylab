using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Geom;
using ListClass.Dao;
using ListClass.Model;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class DanhMucThietBiController : AccountLoginController
    {
        private readonly DanhMucThietBiDAO danhmucthietbiDAO = new DanhMucThietBiDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("DanhSachThietBi")) 
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;

                var count = danhmucthietbiDAO.getCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.Count = count;

                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;

                var list = danhmucthietbiDAO.getList(Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        }

        public ActionResult InPDF()
        {
            if (HasAccessAd() || HasAccessUser("InPdfDanhSachThietBi"))
            {
                var list = danhmucthietbiDAO.getList();
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        }
        public ActionResult ExportToPDF()
        {
            if (HasAccessAd() || HasAccessUser("InPdfDanhSachThietBi"))
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
                                MarginBottom = 30
                            }
                    };
                    var list = danhmucthietbiDAO.getList();
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/DanhMucThietBi/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "DanhMucThietBi.pdf";

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
                TempData["Message"] = "Bạn không có quyền Inpdf ";
                return RedirectToReferrer();
            }     
        }
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemDanhSachThietBi"))
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
        public ActionResult Create(DanhMucThietBi danhmucthietbi, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(danhmucthietbi);
            }

            if (danhmucthietbiDAO.CheckSerialNumberExists(danhmucthietbi.Serial))
            {
                TempData["Message"] = "Sản phẩm đã tồn tại";
                return RedirectToAction("Create");
            }

            if (file != null && file.ContentLength > 0 && IsValidImage(file))
            {
                string fileName = SaveFile(danhmucthietbi.Serial, file);
                danhmucthietbi.Img = fileName;
            }
            danhmucthietbi.NgayTao = DateTime.Now;
            danhmucthietbi.Status = 1;
            danhmucthietbi.NguoiTao = Session["Name"]?.ToString();
            danhmucthietbiDAO.Insert(danhmucthietbi);
            TempData["Message"] = "Thêm thành công";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> UploadProfileImage(int companyId, HttpPostedFileBase imgFile)
        {
            if (HasAccessAd() || HasAccessUser("ThemDanhSachThietBi"))
            {
                if (imgFile != null && imgFile.ContentLength > 0)
                {
                    if (imgFile.ContentType.StartsWith("image"))
                    {
                        var company = await dbContext.DanhMucThietBis.FindAsync(companyId);
                        if (company == null)
                        {
                            return Json(new { success = false, message = "thiết bị không tồn tại." });
                        }

                        var fileName = System.IO.Path.GetFileName(imgFile.FileName);
                        var filePath = System.IO.Path.Combine(Server.MapPath("~/Public/Products"), fileName);

                        imgFile.SaveAs(filePath);

                        company.Img = fileName;
                        dbContext.Entry(company).State = EntityState.Modified;
                        await dbContext.SaveChangesAsync();

                        return Json(new { success = true, message = "Cập nhật ảnh thành công!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Vui lòng chọn một file ảnh hợp lệ." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Không có file ảnh được chọn." });
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            } 
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaDanhSachThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = danhmucthietbiDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
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
        public ActionResult Edit(List<DanhMucThietBi> listnumbers)
        {
            if (listnumbers == null || !listnumbers.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = listnumbers.Select(b => b.Id).ToList();

                var existingRecords = dbContext.DanhMucThietBis
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var listNumber in listnumbers)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == listNumber.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Serial = listNumber.Serial;
                        existingRecord.TenThietBi = listNumber.TenThietBi;
                        existingRecord.MaThietBi = listNumber.MaThietBi;
                        existingRecord.HangSX = listNumber.HangSX;
                        existingRecord.NgayDuaVaoSuDung = listNumber.NgayDuaVaoSuDung;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"]?.ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaDanhSachThietBi"))
            {
                var idList = ParseIds(ids);
                var listNumbers = danhmucthietbiDAO.GetRowsByIds(idList);
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
        public ActionResult Delete(List<DanhMucThietBi> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.DanhMucThietBis.Find(listNumber.Id);
                if (oldList != null)
                { 
                    if (!string.IsNullOrEmpty(oldList.Img))
                    {
                        var oldImagePath = System.IO.Path.Combine(Server.MapPath("~/Public/Thietbis"), oldList.Img);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    dbContext.DanhMucThietBis.Remove(oldList);
                }
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index", "DanhMucThietBi");
        }
        public ActionResult ExportToExcel()
        {
            if (HasAccessAd() || HasAccessUser("ExcelDanhSachThietBi"))
            {
                try
                {
                    var list = danhmucthietbiDAO.getList();
                     
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {

                new DataColumn("STT", typeof(int)),
                new DataColumn("TenThietBi", typeof(string)),
                new DataColumn("MaThietBi", typeof(string)),
                new DataColumn("HangSX", typeof(string)),
                new DataColumn("NgayDuaVaoSuDung", typeof(string))
                    });

                    int rowIndex = 1;

                    foreach (var product in list)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.TenThietBi,
                            product.MaThietBi,
                            product.HangSX,
                            product.NgayDuaVaoSuDung
                            );
                    } 
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("danhsachthietbis");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "danhsachthietbis.xlsx");
                        }
                    }
                }
                catch (Exception ex)
                { 
                    return new HttpStatusCodeResult(500, "Internal server error: " + ex.Message);
                }
            } 
            else
            {
                TempData["Message"] = "Bạn không có quyền Export Excel";
                return RedirectToReferrer();
            }     
        } 
    }
}
