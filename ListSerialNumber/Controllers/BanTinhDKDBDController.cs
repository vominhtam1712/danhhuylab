using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ListClass.Dao;
using ListClass.Model;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class BanTinhDKDBDController : AccountLoginController
    {
        private readonly BanTinhDKDBD_DAO banTinhDKDBD_DAO = new BanTinhDKDBD_DAO();
        private readonly BienBanHieuChuanDAO bbhcDAO = new BienBanHieuChuanDAO();
        private readonly Join_BanTinhDKDBD_PhanCong_DAO join_bt = new Join_BanTinhDKDBD_PhanCong_DAO();
        private readonly Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD_DAO join = new Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD_DAO();
        private readonly Join_Bienbanhieuchuan_DKDBD_DAO join_bbhc_dkdbd_DAO = new Join_Bienbanhieuchuan_DKDBD_DAO();
        private readonly Join_STD_DUT_Bienbanhieuchuan_DAO join_STD_DUT_BIENBAN = new Join_STD_DUT_Bienbanhieuchuan_DAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Index(int? id)
        { 
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThucHien"))
            {
                var Id_bienbanhieuchuan = id;
                ViewBag.Id_bienbanhieuchuan = Id_bienbanhieuchuan;
                var list = join_bt.GetListJoin(id);
                return View("Index", list);
            } 
            else 
            { 
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        } 
        public ActionResult XoaDKDBD(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = banTinhDKDBD_DAO.GetRowsByIds(idList);
                var id_bienban = listNumbers.FirstOrDefault()?.Id_BienBanHieuChuan;
                var list_idbb = bbhcDAO.getListIdbienban(id_bienban);
                var id_pc = list_idbb.FirstOrDefault()?.Id_PhanCong;
                ViewBag.id = id_pc;
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa ";
                return RedirectToReferrer(); 
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaDKDBD(List<BanTinhDKDBD> ids)
        {
            foreach (BanTinhDKDBD BanTinhDKDBD in ids)
            {
                var oldlist = dbContext.BanTinhDKDBDs.Find(BanTinhDKDBD.Id);
                dbContext.BanTinhDKDBDs.Remove(oldlist);

            }
            var id_bb = ids.FirstOrDefault()?.Id_BienBanHieuChuan; 
            var list_idbb = bbhcDAO.getListIdbienban(id_bb);
            var id = list_idbb.FirstOrDefault()?.Id_PhanCong;
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        }
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
                    var data = join_bbhc_dkdbd_DAO.GetListJoin(id);

                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    { 
                        new DataColumn("STT", typeof(int)),
                        new DataColumn("Mã biên bản", typeof(string)),
                        new DataColumn("U_a", typeof(float)),
                        new DataColumn("U_d", typeof(float)),
                        new DataColumn("U_cer", typeof(float)),
                        new DataColumn("U_od_std", typeof(float)),
                        new DataColumn("U_od", typeof(float)),
                        new DataColumn("U_dd", typeof(float)),
                        new DataColumn("U_c", typeof(float)),
                        new DataColumn("K", typeof(float)),
                        new DataColumn("U_morong", typeof(float)),
                        new DataColumn("Saiso", typeof(float)),
                        new DataColumn("DoDKDB", typeof(float))
                    });

                    int rowIndex = 1;

                    foreach (var product in data)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.MaBienBan,
                            product.U_a,
                            product.U_d,
                            product.U_cer,
                            product.U_od_std,
                            product.U_od,
                            product.U_dd,
                            product.U_c,
                            product.K,
                            product.U_morong,
                            product.Saiso,
                            product.DoDKDB
                            );
                    }

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("BantinhDKDBDs");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BantinhDKDBDss.xlsx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Lỗi không có dữ liệu";
                    return RedirectToReferrer();
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền Export Excel";
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
            if (HasAccessAd() || HasAccessUser("InPdfLuuInHoSo"))
            {
                var list = join.GetListJoin(id);
                float slope = join_STD_DUT_BIENBAN.CalculateSlope(id);
                float? trungBinhCong_STD = join_STD_DUT_BIENBAN.TrungBinhCong_STD(id);
                float? trungBinhCong_DUT = join_STD_DUT_BIENBAN.TrungBinhCong_DUT(id);

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
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPDF ";
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
                        Options ={
                                        PdfPageSize = PdfPageSize.A4,
                                        PdfPageOrientation = PdfPageOrientation.Portrait,
                                        MarginLeft = 50,
                                        MarginRight = 50,
                                        MarginTop = 20,
                                        MarginBottom = 50
                                    }
                    };

                    var list = join.GetListJoin(ids);
                    if (list == null || !list.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    float slope = join_STD_DUT_BIENBAN.CalculateSlope(ids);
                    float? trungBinhCong_STD = join_STD_DUT_BIENBAN.TrungBinhCong_STD(ids);
                    float? trungBinhCong_DUT = join_STD_DUT_BIENBAN.TrungBinhCong_DUT(ids);

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

                    var htmlPdf = base.RenderViewToString("~/Views/BanTinhDKDBD/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "BanTinhDKDBD.pdf";

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
                TempData["Message"] = "Bạn không có quyền InPDF ";
                return RedirectToReferrer(); 
            }     
        } 
    }
}
