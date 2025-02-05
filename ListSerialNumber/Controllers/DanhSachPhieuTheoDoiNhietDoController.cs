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
    public class DanhSachPhieuTheoDoiNhietDoController : AccountLoginController
    {
        private readonly DanhSachPhieuTheoDoiNhietDoDAO danhsachphieutheodoinhietdoDAO = new DanhSachPhieuTheoDoiNhietDoDAO();
        private readonly PhieuTheoDoiNhietDoDAO phieutheodoinhietdoDAO = new PhieuTheoDoiNhietDoDAO();
        private readonly join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo_DAO ph = new join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo_DAO();
        private readonly Join_dsptdnd_phancong_DAO join = new Join_dsptdnd_phancong_DAO();
        private readonly CompanyDAO companyDAO = new CompanyDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("NhietDoPhongLab"))
            {
                var ID = phieutheodoinhietdoDAO.getListId(id);
                var recordId = ID.FirstOrDefault()?.Id;
                ViewBag.Id = recordId;
                var list = join.GetListJoin(id);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }     
        }
        public ActionResult TaoDanhSach(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemNhietDoPhongLab")) 
            {
                var idList = ParseIds(ids);
                var listNumbers = phieutheodoinhietdoDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.Nhanvien = new SelectList(companyDAO.getList("Index"), "Name", "Name");
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
        public ActionResult TaoDanhSach(List<DanhSachPhieuTheoDoiNhietDo> DanhSachPhieuTheoDoiNhietDos)
        {
            if (DanhSachPhieuTheoDoiNhietDos == null || !DanhSachPhieuTheoDoiNhietDos.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            } 
            try
            {

                foreach (var DanhSachPhieuTheoDoiNhietDo in DanhSachPhieuTheoDoiNhietDos)
                {

                    var oldlist = new DanhSachPhieuTheoDoiNhietDo
                    {
                        ID_PhieuTheoDoiNhietDo = DanhSachPhieuTheoDoiNhietDo.ID_PhieuTheoDoiNhietDo,
                        Ngay_theo_doi = DanhSachPhieuTheoDoiNhietDo.Ngay_theo_doi,
                        NhietDo_Sang = DanhSachPhieuTheoDoiNhietDo.NhietDo_Sang,
                        DoAm_Sang = DanhSachPhieuTheoDoiNhietDo.DoAm_Sang,
                        NhietDo_Chieu = DanhSachPhieuTheoDoiNhietDo.NhietDo_Chieu,
                        DoAm_Chieu = DanhSachPhieuTheoDoiNhietDo.DoAm_Chieu,
                        KetLuan = DanhSachPhieuTheoDoiNhietDo.KetLuan,
                        NguoiKiemTra = DanhSachPhieuTheoDoiNhietDo.NguoiKiemTra,
                        Ngay = DateTime.Now,
                        status = 1
                    };
                    danhsachphieutheodoinhietdoDAO.Insert(oldlist);
                }
                var Id = DanhSachPhieuTheoDoiNhietDos.First().ID_PhieuTheoDoiNhietDo;
                TempData["Message"] = "Tạo thành công";
                ViewBag.Nhanvien = new SelectList(companyDAO.getList("Index"), "Name", "Name");
                return RedirectToAction("Index", "DanhSachPhieuTheoDoiNhietDo", new { id = Id });
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Đã xảy ra lỗi: {ex.Message}";
            }
            return RedirectToAction("ThucHien", "PhanCong");
        }
        public ActionResult ChinhSua(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaNhietDoPhongLab"))
            {
                var idList = ParseIds(ids);
                var listNumbers = danhsachphieutheodoinhietdoDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.Nhanvien = new SelectList(companyDAO.getList("Index"), "Name", "Name");
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
        public ActionResult ChinhSua(List<DanhSachPhieuTheoDoiNhietDo> DanhSachPhieuTheoDoiNhietDos)
        {
            if (DanhSachPhieuTheoDoiNhietDos == null || !DanhSachPhieuTheoDoiNhietDos.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = DanhSachPhieuTheoDoiNhietDos.Select(b => b.ID).ToList();

                var existingRecords = dbContext.DanhSachPhieuTheoDoiNhietDos
                    .Where(b => idsToUpdate.Contains(b.ID))
                    .ToList();

                foreach (var DanhSachPhieuTheoDoiNhietDo in DanhSachPhieuTheoDoiNhietDos)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.ID == DanhSachPhieuTheoDoiNhietDo.ID);
                    if (existingRecord != null)
                    {
                        existingRecord.ID_PhieuTheoDoiNhietDo = DanhSachPhieuTheoDoiNhietDo.ID_PhieuTheoDoiNhietDo;
                        existingRecord.Ngay_theo_doi = DanhSachPhieuTheoDoiNhietDo.Ngay_theo_doi;
                        existingRecord.NhietDo_Sang = DanhSachPhieuTheoDoiNhietDo.NhietDo_Sang;
                        existingRecord.DoAm_Sang = DanhSachPhieuTheoDoiNhietDo.DoAm_Sang;
                        existingRecord.NhietDo_Chieu = DanhSachPhieuTheoDoiNhietDo.NhietDo_Chieu;
                        existingRecord.DoAm_Chieu = DanhSachPhieuTheoDoiNhietDo.DoAm_Chieu;
                        existingRecord.KetLuan = DanhSachPhieuTheoDoiNhietDo.KetLuan;
                        existingRecord.NguoiKiemTra = DanhSachPhieuTheoDoiNhietDo.NguoiKiemTra; 
                        existingRecord.Ngay = DateTime.Now;
                        existingRecord.status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                var id = DanhSachPhieuTheoDoiNhietDos.First().ID_PhieuTheoDoiNhietDo;
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.Nhanvien = new SelectList(companyDAO.getList("Index"), "Name", "Name");
                return RedirectToAction("Index", "DanhSachPhieuTheoDoiNhietDo", new { Id = id });
            }
        }
        public ActionResult ExportToExcel(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelNhietDoPhongLab"))
            {
                try
                {
                    var data = danhsachphieutheodoinhietdoDAO.getList(id);
                     
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {

                            new DataColumn("Ngày", typeof(int)),
                            new DataColumn("Nhiệt độ ghi nhận lúc:7 giờ 30(°C)", typeof(decimal)),
                            new DataColumn("Độ ẩm ghi nhận lúc:7 giờ 30(%)", typeof(decimal)),
                            new DataColumn("Nhiệt độ ghi nhận lúc:17 giờ 30(°C)", typeof(decimal)),
                            new DataColumn("Độ ẩm ghi nhận lúc:17 giờ 30(%)", typeof(decimal)),
                            new DataColumn("Kết luận", typeof(string)),
                            new DataColumn("Người kiểm tra", typeof(string)),
                    });
                     
                    foreach (var product in data)
                    {
                        dt.Rows.Add(
                            product.Ngay_theo_doi,
                            product.NhietDo_Sang,
                            product.NhietDo_Chieu,
                            product.DoAm_Sang,
                            product.DoAm_Chieu,
                            product.KetLuan,
                            product.NguoiKiemTra
                            );
                    }
                     
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("danhsachphieutheodoinhietdo");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "danhsachphieutheodoinhietdoDAO.xlsx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Bạn không thể truy cập";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền Export Excel";
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
            if (HasAccessAd() || HasAccessUser("XoaNhietDoPhongLab"))
            {
                var idList = ParseIds(ids);
                var listNumbers = danhsachphieutheodoinhietdoDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<DanhSachPhieuTheoDoiNhietDo> ids)
        {
            var id = ids.FirstOrDefault()?.ID_PhieuTheoDoiNhietDo;
            foreach (DanhSachPhieuTheoDoiNhietDo listNumber in ids)
            {
                var oldlist = dbContext.DanhSachPhieuTheoDoiNhietDos.Find(listNumber.ID);
                dbContext.DanhSachPhieuTheoDoiNhietDos.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index", "DanhSachPhieuTheoDoiNhietDo", new { id });
        }
        public ActionResult InPDF(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfNhietDoPhongLab"))
            {
                var list = ph.GetListJoin(ids);
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPDF";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ExportPdf(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfNhietDoPhongLab"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                    {
                        PdfPageSize = PdfPageSize.A4,
                        PdfPageOrientation = PdfPageOrientation.Portrait,
                        MarginLeft = 10,
                        MarginRight = 10,
                        MarginTop = 20,
                        MarginBottom =20
                    }
                    };
                     
                    var list = ph.GetListJoin(ids);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                     
                    var htmlPdf = base.RenderPartialTostring("~/Views/DanhSachPhieuTheoDoiNhietDo/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");
                     
                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);
                     
                    string fileName = "Phieutheodoinhietdodoam.pdf";
                     
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
