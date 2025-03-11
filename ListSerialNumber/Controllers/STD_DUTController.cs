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
using OfficeOpenXml;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class STD_DUTController : AccountLoginController
    {
        private readonly STD_DUT_DAO std_dut_DAO = new STD_DUT_DAO();
        private readonly BienBanHieuChuanDAO bienbanhieuchuanDAO = new BienBanHieuChuanDAO();
        private readonly Join_STD_DUT_PhanCong_DAO join = new Join_STD_DUT_PhanCong_DAO();
        private readonly Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DAO abc = new Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DAO();
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        private readonly Join_STD_DUT_Bienbanhieuchuan_DAO sp = new Join_STD_DUT_Bienbanhieuchuan_DAO();
        public ActionResult ExportToExcel(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelLuuInHoSo"))
            {
                try
                {
                    var data = abc.GetListJoin(id);

                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("STT", typeof(int)),
                        new DataColumn("MucKiemTra_STD", typeof(float)),
                        new DataColumn("Lan1_STD", typeof(float)),
                        new DataColumn("Lan2_STD", typeof(float)),
                        new DataColumn("Lan3_STD", typeof(float)),
                        new DataColumn("Lan4_STD", typeof(float)),
                        new DataColumn("Lan5_STD", typeof(float)),
                        new DataColumn("MucKiemTra_DUT", typeof(float)),
                        new DataColumn("Lan1_DUT", typeof(float)),
                        new DataColumn("Lan2_DUT", typeof(float)),
                        new DataColumn("Lan3_DUT", typeof(float)),
                        new DataColumn("Lan4_DUT", typeof(float)),
                        new DataColumn("Lan5_DUT", typeof(float)),
                        new DataColumn("Id_BienBanHieuChuan", typeof(int)),
                        new DataColumn("NhietDo", typeof(float)),
                        new DataColumn("DoAm", typeof(float)),
                        new DataColumn("TenHieuChuan", typeof(string)),
                        new DataColumn("SN", typeof(int)),
                        new DataColumn("NgayThucHien", typeof(DateTime)),
                        new DataColumn("Tenthietbi", typeof(string)),
                        new DataColumn("Hang", typeof(string)),
                        new DataColumn("Model", typeof(string)),
                        new DataColumn("SerialNumber", typeof(string)),
                    });

                    int rowIndex = 1;

                    foreach (var product in data)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.MucKiemTra_STD,
                            product.Lan1_STD,
                            product.Lan2_STD,
                            product.Lan3_STD,
                            product.Lan4_STD,
                            product.Lan5_STD,
                            product.MucKiemTra_DUT,
                            product.Lan1_DUT,
                            product.Lan2_DUT,
                            product.Lan3_DUT,
                            product.Lan4_DUT,
                            product.Lan5_DUT,
                            product.Id_BienBanHieuChuan,
                            product.NhietDo,
                            product.DoAm,
                            product.TenHieuChuan,
                            product.SN,
                            product.NgayThucHien,
                            product.Tenthietbi,
                            product.Hang,
                            product.Model,
                            product.SerialNumber);
                    }

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("BienBanHieuChuans");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BienBanHieuChuans.xlsx");
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
                TempData["Message"] = "Bạn không có quyền ExportExcel";
                return RedirectToReferrer();
            }

        } 
        public ActionResult InPDF_DoDongDeu(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo")) 
            {
                try
                {
                    var data = sp.GetListJoin(id);
                    if (data == null || !data.Any())
                    {
                        TempData["Message"] = "Chưa có thông tin";
                        return RedirectToAction("ThucHien", "PhanCong");
                    }
                    float slope = sp.CalculateSlope(id);
                    float? trungBinhCong_STD = sp.TrungBinhCong_STD(id);
                    float? trungBinhCong_DUT = sp.TrungBinhCong_DUT(id);

                    if (!trungBinhCong_STD.HasValue || !trungBinhCong_DUT.HasValue)
                    {
                        TempData["Message"] = "Dữ liệu tính toán không hợp lệ";
                        return RedirectToAction("ThucHien", "PhanCong");
                    }
                    float intercept = trungBinhCong_STD.Value - (slope * trungBinhCong_DUT.Value);
                    if (trungBinhCong_STD.Value == 0)
                    {
                        TempData["Message"] = "Giá trị trung bình không hợp lệ để tính toán độ đồng đều.";
                        return RedirectToAction("ThucHien", "PhanCong");
                    }
                    float Dodongdeu = ((slope - intercept) / trungBinhCong_STD.Value) * 100;
                    ViewBag.Slope = slope.ToString("F3");
                    ViewBag.Intercept = intercept.ToString("F3");
                    ViewBag.DoDongDeu = Dodongdeu.ToString("F3");

                    return View(data);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Đã xảy ra lỗi khi xử lý yêu cầu";
                    return RedirectToAction("ThucHien", "PhanCong");
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPdf";
                return RedirectToReferrer();
            }

        }
        public ActionResult ExportPdf_DoDongDeu(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo"))
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
                            MarginTop = 20,
                            MarginBottom = 80
                       }
                    };

                    var data = sp.GetListJoin(ids);
                    if (data == null || !data.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    float slope = sp.CalculateSlope(ids);
                    float? trungBinhCong_STD = sp.TrungBinhCong_STD(ids);
                    float? trungBinhCong_DUT = sp.TrungBinhCong_DUT(ids);

                    if (!trungBinhCong_STD.HasValue || !trungBinhCong_DUT.HasValue)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Dữ liệu tính toán không hợp lệ");
                    }

                    float intercept = trungBinhCong_STD.Value - (slope * trungBinhCong_DUT.Value);

                    if (trungBinhCong_STD.Value == 0)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Giá trị trung bình không hợp lệ để tính toán độ đồng đều.");
                    }

                    float Dodongdeu = ((slope - intercept) / trungBinhCong_STD.Value) * 100;

                    ViewBag.Slope = slope.ToString("F3");
                    ViewBag.Intercept = intercept.ToString("F3");
                    ViewBag.DoDongDeu = Dodongdeu.ToString("F3");

                    var htmlPdf = base.RenderViewToString("~/Views/STD_DUT/InPDF_DoDongDeu.cshtml", data, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "BangKiemTraDoDongDeuCuaNguonUV.pdf";

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
        public ActionResult InPDF_DoOnDinh(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo"))
            {
                var list = sp.GetListJoin(id);
                return View("InPDF_DoOnDinh", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPdf";
                return RedirectToReferrer();
            }

        }
        public ActionResult ExportPdf_DoOnDinh(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                           {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Landscape,
                                MarginLeft = 50,
                                MarginRight = 50,
                                MarginTop = 20,
                                MarginBottom =20
                           }
                    };

                    var list = sp.GetListJoin(ids);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderPartialTostring("~/Views/STD_DUT/InPDF_DoOnDinh.cshtml", list, ControllerContext);
                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");
                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);
                    string fileName = "doondinh.pdf";
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
        public ActionResult InPDF(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo"))
            {
                var list = abc.GetListJoin(ids);
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPdf";
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
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo"))
            {
                try
                { 
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                           {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Portrait,
                                MarginLeft = 50,
                                MarginRight = 50,
                                MarginTop = 20,
                                MarginBottom =50
                           }
                    };

                    var list = abc.GetListJoin(ids);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/STD_DUT/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Bienbanhieuchuan.pdf";

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
        public ActionResult PerformAction(List<int> selectedIds, string action)
        {
            if (selectedIds == null || !selectedIds.Any() || string.IsNullOrEmpty(action))
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            switch (action)
            {
                case "Create":
                    return RedirectToAction("Tao_TB_STD", "TB_STD", new { ids = string.Join(",", selectedIds) });
                case "Edit":
                    return RedirectToAction("ChinhSua", new { ids = string.Join(",", selectedIds) });

                case "Delete":
                    return RedirectToAction("Delete", new { ids = string.Join(",", selectedIds) });

                default:
                    TempData["Error"] = "Invalid action";
                    return RedirectToAction("Index");
            }
        }
        public ActionResult Tao_STD_DUT(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien")) 
            {
                var idList = ParseIds(ids);
                var listNumbers = bienbanhieuchuanDAO.GetRowsByIds(idList);

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
        public ActionResult Tao_STD_DUT(List<STD_DUT> STD_DUTs)
        {
            if (STD_DUTs == null)
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            }

            try
            {
                var creatorName = Session["Name"]?.ToString();
                if (creatorName == null)
                {
                    TempData["Message"] = "Tên người tạo không hợp lệ.";
                    return RedirectToAction("ThucHien", "PhanCong");
                }
                foreach (var std in STD_DUTs)
                {
                    var sqrt3 = (float)Math.Sqrt(3);

                    var values_std = new[] { std.Lan1_STD, std.Lan2_STD, std.Lan3_STD, std.Lan4_STD, std.Lan5_STD };
                    var maxLanValue_std = values_std.Max();
                    var minLanValue_std = values_std.Min();
                    var trungBinh_std = values_std.Average();
                    var doOnDinh_std = (maxLanValue_std - minLanValue_std) / 2;
                    var u_std = (doOnDinh_std / (sqrt3 * trungBinh_std)) * 100;

                    var values_dut = new[] { std.Lan1_DUT, std.Lan2_DUT, std.Lan3_DUT, std.Lan4_DUT, std.Lan5_DUT };
                    var maxLanValue_dut = values_dut.Max();
                    var minLanValue_dut = values_dut.Min();
                    var trungBinh_dut = values_dut.Average();
                    var doOnDinh_dut = (maxLanValue_dut - minLanValue_dut) / 2;
                    var u_dut = (doOnDinh_dut / (sqrt3 * trungBinh_dut)) * 100;

                    var oldlist = new STD_DUT
                    {
                        Id_BienBanHieuChuan = std.Id_BienBanHieuChuan,
                        MucKiemTra_STD = std.MucKiemTra_STD,
                        Lan1_STD = std.Lan1_STD,
                        Lan2_STD = std.Lan2_STD,
                        Lan3_STD = std.Lan3_STD,
                        Lan4_STD = std.Lan4_STD,
                        Lan5_STD = std.Lan5_STD,
                        Max_STD = maxLanValue_std,
                        Min_STD = minLanValue_std,
                        TrungBinh_STD = trungBinh_std,
                        DoOnDinh_STD = doOnDinh_std,
                        U_STD = u_std,
                        STD_Cer_STD = std.STD_Cer_STD,
                        STD_Spec_STD = std.STD_Cer_STD,
                        MucKiemTra_DUT = std.MucKiemTra_STD,
                        Lan1_DUT = std.Lan1_DUT,
                        Lan2_DUT = std.Lan2_DUT,
                        Lan3_DUT = std.Lan3_DUT,
                        Lan4_DUT = std.Lan4_DUT,
                        Lan5_DUT = std.Lan5_DUT,
                        Max_DUT = maxLanValue_dut,
                        Min_DUT = minLanValue_dut,
                        TrungBinh_DUT = trungBinh_dut,
                        DoOnDinh_DUT = doOnDinh_dut,
                        U_DUT = u_dut,
                        DoPhanGiai_DUT = std.DoPhanGiai_DUT,
                        NguoiTao = creatorName,
                        Status = 1
                    };
                    std_dut_DAO.Insert(oldlist);
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Đã xảy ra lỗi: {ex.Message}";
            }
            TempData["Message"] = "Tạo thành công";
            var idsss = STD_DUTs.FirstOrDefault()?.Id_BienBanHieuChuan;
            var idss = bienbanhieuchuanDAO.getListIdbienban(idsss);
            var idPhanCong = idss.FirstOrDefault()?.Id_PhanCong;
            var phanCongList = phancongDAO.getListIdphancong(idPhanCong);
            var id = phanCongList?.FirstOrDefault()?.Id;
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        }
        public ActionResult ImportExcel(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = bienbanhieuchuanDAO.GetRowsByIds(idList);

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
        public ActionResult Upload(List<STD_DUT> STD_DUTs, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var creatorName = Session["Name"]?.ToString();
                    if (creatorName == null)
                    {
                        TempData["Message"] = "Tên người tạo không hợp lệ.";
                        return RedirectToAction("ThucHien", "PhanCong");
                    } 
                    foreach (var stdDut in STD_DUTs)
                    { 
                        List<STD_DUT> newListNumbers = new List<STD_DUT>();
                        for (int row = 2; row <= rowCount; row++)
                        { 
                            float muckiemtra = ParseFloat(worksheet.Cells[row, 1].Text.Trim());
                            float lan1_std = ParseFloat(worksheet.Cells[row, 2].Text.Trim());
                            float lan2_std = ParseFloat(worksheet.Cells[row, 3].Text.Trim());
                            float lan3_std = ParseFloat(worksheet.Cells[row, 4].Text.Trim());
                            float lan4_std = ParseFloat(worksheet.Cells[row, 5].Text.Trim());
                            float lan5_std = ParseFloat(worksheet.Cells[row, 6].Text.Trim());
                            float lan1_dut = ParseFloat(worksheet.Cells[row, 7].Text.Trim());
                            float lan2_dut = ParseFloat(worksheet.Cells[row, 8].Text.Trim());
                            float lan3_dut = ParseFloat(worksheet.Cells[row, 9].Text.Trim());
                            float lan4_dut = ParseFloat(worksheet.Cells[row, 10].Text.Trim());
                            float lan5_dut = ParseFloat(worksheet.Cells[row, 11].Text.Trim());

                            var sqrt3 = (float)Math.Sqrt(3);
                             
                            var values_std = new[] { lan1_std, lan2_std, lan3_std, lan4_std, lan5_std };
                            var maxLanValue_std = values_std.Max();
                            var minLanValue_std = values_std.Min();
                            var trungBinh_std = values_std.Average();
                            var doOnDinh_std = (maxLanValue_std - minLanValue_std) / 2;
                            var u_std = (doOnDinh_std / (sqrt3 * trungBinh_std)) * 100;

                            var values_dut = new[] { lan1_dut, lan2_dut, lan3_dut, lan4_dut, lan5_dut };
                            var maxLanValue_dut = values_dut.Max();
                            var minLanValue_dut = values_dut.Min();
                            var trungBinh_dut = values_dut.Average();
                            var doOnDinh_dut = (maxLanValue_dut - minLanValue_dut) / 2;
                            var u_dut = (doOnDinh_dut / (sqrt3 * trungBinh_dut)) * 100;
                             
                            var std_dut = new STD_DUT
                            {
                                Id_BienBanHieuChuan = stdDut.Id_BienBanHieuChuan,
                                MucKiemTra_STD = muckiemtra,
                                Lan1_STD = lan1_std,
                                Lan2_STD = lan2_std,
                                Lan3_STD = lan3_std,
                                Lan4_STD = lan4_std,
                                Lan5_STD = lan5_std,
                                Max_STD = maxLanValue_std,
                                Min_STD = minLanValue_std,
                                TrungBinh_STD = trungBinh_std,
                                DoOnDinh_STD = doOnDinh_std,
                                U_STD = u_std,
                                STD_Cer_STD = stdDut.STD_Cer_STD,
                                STD_Spec_STD = stdDut.STD_Cer_STD,
                                MucKiemTra_DUT = muckiemtra,
                                Lan1_DUT = lan1_dut,
                                Lan2_DUT = lan2_dut,
                                Lan3_DUT = lan3_dut,
                                Lan4_DUT = lan4_dut,
                                Lan5_DUT = lan5_dut,
                                Max_DUT = maxLanValue_dut,
                                Min_DUT = minLanValue_dut,
                                TrungBinh_DUT = trungBinh_dut,
                                DoOnDinh_DUT = doOnDinh_dut,
                                U_DUT = u_dut,
                                DoPhanGiai_DUT = stdDut.DoPhanGiai_DUT,
                                NguoiTao = creatorName,
                                Status = 1
                            }; 
                            newListNumbers.Add(std_dut); 
                            std_dut_DAO.Insert(std_dut); 
                        }
                    }
                }
            } 
            TempData["Message"] = "Tải lên thành công";
            var idsss = STD_DUTs.FirstOrDefault()?.Id_BienBanHieuChuan;
            var idss = bienbanhieuchuanDAO.getListIdbienban(idsss);
            var idPhanCong = idss.FirstOrDefault()?.Id_PhanCong;
            var phanCongList = phancongDAO.getListIdphancong(idPhanCong);
            var id = phanCongList?.FirstOrDefault()?.Id;
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        }
        private float ParseFloat(string input)
        {
            if (float.TryParse(input, out float result))
            {
                return result;
            }
            return 0f; 
        }
        public ActionResult ChinhSua(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();	
            }
            if (HasAccessAd() || HasAccessUser("SuaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = std_dut_DAO.GetRowsByIds(idList);
                var id = listNumbers.FirstOrDefault()?.Id_BienBanHieuChuan;
                var idss = bienbanhieuchuanDAO.getListIdbienban(id);
                var idPhanCong = idss.FirstOrDefault()?.Id_PhanCong;
                ViewBag.id = idPhanCong;

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
        public ActionResult ChinhSua(List<STD_DUT> STD_DUTs)
        {
            if (STD_DUTs == null || !STD_DUTs.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = STD_DUTs.Select(b => b.ID).ToList();

                var existingRecords = dbContext.STD_DUTs
                    .Where(b => idsToUpdate.Contains(b.ID))
                    .ToList();
                var creatorName = Session["Name"]?.ToString();
                if (creatorName == null)
                {
                    TempData["Message"] = "Tên người tạo không hợp lệ.";
                    return RedirectToAction("ThucHien", "PhanCong");
                }
                foreach (var std in STD_DUTs)
                {
                    var sqrt3 = (float)Math.Sqrt(3);

                    var values_std = new[] { std.Lan1_STD, std.Lan2_STD, std.Lan3_STD, std.Lan4_STD, std.Lan5_STD };
                    var maxLanValue_std = values_std.Max();
                    var minLanValue_std = values_std.Min();
                    var trungBinh_std = values_std.Average();
                    var doOnDinh_std = (maxLanValue_std - minLanValue_std) / 2;
                    var u_std = (doOnDinh_std / (sqrt3 * trungBinh_std)) * 100;

                    var values_dut = new[] { std.Lan1_DUT, std.Lan2_DUT, std.Lan3_DUT, std.Lan4_DUT, std.Lan5_DUT };
                    var maxLanValue_dut = values_dut.Max();
                    var minLanValue_dut = values_dut.Min();
                    var trungBinh_dut = values_dut.Average();
                    var doOnDinh_dut = (maxLanValue_dut - minLanValue_dut) / 2;
                    var u_dut = (doOnDinh_dut / (sqrt3 * trungBinh_dut)) * 100;
                    var oldlist = existingRecords.FirstOrDefault(b => b.ID == std.ID);
                    if (oldlist != null)
                    {
                        oldlist.Id_BienBanHieuChuan = std.Id_BienBanHieuChuan;
                        oldlist.MucKiemTra_STD = std.MucKiemTra_STD;
                        oldlist.Lan1_STD = std.Lan1_STD;
                        oldlist.Lan2_STD = std.Lan2_STD;
                        oldlist.Lan3_STD = std.Lan3_STD;
                        oldlist.Lan4_STD = std.Lan4_STD;
                        oldlist.Lan5_STD = std.Lan5_STD;
                        oldlist.Max_STD = maxLanValue_std;
                        oldlist.Min_STD = minLanValue_std;
                        oldlist.TrungBinh_STD = trungBinh_std;
                        oldlist.DoOnDinh_STD = doOnDinh_std;
                        oldlist.U_STD = u_std;
                        oldlist.STD_Cer_STD = std.STD_Cer_STD;
                        oldlist.STD_Spec_STD = std.STD_Cer_STD;
                        oldlist.MucKiemTra_DUT = std.MucKiemTra_STD;
                        oldlist.Lan1_DUT = std.Lan1_DUT;
                        oldlist.Lan2_DUT = std.Lan2_DUT;
                        oldlist.Lan3_DUT = std.Lan3_DUT;
                        oldlist.Lan4_DUT = std.Lan4_DUT;
                        oldlist.Lan5_DUT = std.Lan5_DUT;
                        oldlist.Max_DUT = maxLanValue_dut;
                        oldlist.Min_DUT = minLanValue_dut;
                        oldlist.TrungBinh_DUT = trungBinh_dut;
                        oldlist.DoOnDinh_DUT = doOnDinh_dut;
                        oldlist.U_DUT = u_dut;
                        oldlist.DoPhanGiai_DUT = std.DoPhanGiai_DUT;
                        oldlist.NguoiTao = creatorName;
                        oldlist.Status = 1;
                        dbContext.Entry(oldlist).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                var idsss = STD_DUTs.FirstOrDefault()?.Id_BienBanHieuChuan;
                var idss = bienbanhieuchuanDAO.getListIdbienban(idsss);
                var idPhanCong = idss.FirstOrDefault()?.Id_PhanCong;
                var phanCongList = phancongDAO.getListIdphancong(idPhanCong);
                var id = phanCongList?.FirstOrDefault()?.Id;
                return RedirectToAction("Details", "BienBanHieuChuan", new { id });
            }
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = std_dut_DAO.GetRowsByIds(idList); 
                var id = listNumbers.FirstOrDefault()?.Id_BienBanHieuChuan;
                var idss = bienbanhieuchuanDAO.getListIdbienban(id);
                var idPhanCong = idss.FirstOrDefault()?.Id_PhanCong; 
                ViewBag.id = idPhanCong;
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
        public ActionResult Delete(List<STD_DUT> STD_DUTs)
        {
            var idsss = STD_DUTs.FirstOrDefault()?.Id_BienBanHieuChuan;
            var idss = bienbanhieuchuanDAO.getListIdbienban(idsss);
            var idPhanCong = idss.FirstOrDefault()?.Id_PhanCong;
            var phanCongList = phancongDAO.getListIdphancong(idPhanCong);
            var id = phanCongList?.FirstOrDefault()?.Id;
            foreach (STD_DUT STD_DUT in STD_DUTs)
            {
                var oldlist = dbContext.STD_DUTs.Find(STD_DUT.ID);
                dbContext.STD_DUTs.Remove(oldlist);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        } 
    }
}
