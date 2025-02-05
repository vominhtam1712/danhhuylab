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
    public class KeHoachHieuChuanController : AccountLoginController
    {
        private readonly KeHoachHieuChuanDAO khhcDAO = new KeHoachHieuChuanDAO();
        private readonly KeHoachHieuChuan_Form_DAO khhcfrom_DAO = new KeHoachHieuChuan_Form_DAO();
        private readonly Join_KeHoachHieuChuanKiemTra_ThietBi_DAO join_DAO = new Join_KeHoachHieuChuanKiemTra_ThietBi_DAO();
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
        public ActionResult ExportToExcel(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelKeHoachHieuChuan")) 
            {
                try
                {
                    var list = join_DAO.getlistPdf(id);
                     
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    { 
                        new DataColumn("STT", typeof(int)),
                        new DataColumn("TenThietBi", typeof(string)),
                        new DataColumn("MaThietBi", typeof(string)),
                        new DataColumn("Jan_Plan", typeof(string)),
                        new DataColumn("Jan_Act", typeof(string)),
                        new DataColumn("Feb_Plan", typeof(string)),
                        new DataColumn("Feb_Act", typeof(string)),
                        new DataColumn("March_Plan", typeof(string)),
                        new DataColumn("March_Act", typeof(string)),
                        new DataColumn("April_Plan", typeof(string)),
                        new DataColumn("April_Act", typeof(string)),
                        new DataColumn("May_Plan", typeof(string)),
                        new DataColumn("May_Act", typeof(string)),
                        new DataColumn("June_Plan", typeof(string)),
                        new DataColumn("June_Act", typeof(string)),
                        new DataColumn("July_Plan", typeof(string)),
                        new DataColumn("July_Act", typeof(string)),
                        new DataColumn("Aug_Plan", typeof(string)),
                        new DataColumn("Aug_Act", typeof(string)),
                        new DataColumn("Sep_Plan", typeof(string)),
                        new DataColumn("Sep_Act", typeof(string)),
                        new DataColumn("Oct_Plan", typeof(string)),
                        new DataColumn("Oct_Act", typeof(string)),
                        new DataColumn("Nov_Plan", typeof(string)),
                        new DataColumn("Nov_Act", typeof(string)),
                        new DataColumn("Dec_Plan", typeof(string)),
                        new DataColumn("Dec_Act", typeof(string)),
                    });

                    int rowIndex = 1;

                    foreach (var product in list)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.TenThietBi,
                            product.MaThietBi,
                            product.Jan_Plan,
                            product.Jan_Act,
                            product.Feb_Plan,
                            product.Feb_Act,
                            product.March_Plan,
                            product.March_Act,
                            product.April_Plan,
                            product.April_Act,
                            product.May_Plan,
                            product.May_Act,
                            product.June_Plan,
                            product.June_Act,
                            product.July_Plan,
                            product.July_Act,
                            product.Aug_Plan,
                            product.Aug_Act,
                            product.Sep_Plan,
                            product.Sep_Act,
                            product.Oct_Plan,
                            product.Oct_Act,
                            product.Nov_Plan,
                            product.Nov_Act,
                            product.Dec_Plan,
                            product.Dec_Act
                            );
                    }
                     
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("danhsachkehoachieuchuans");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "danhsachkehoachieuchuans.xlsx");
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
        public ActionResult InPDF(int? id)
        {
            if (HasAccessAd() || HasAccessUser("InPdfKeHoachHieuChuan"))
            {
                var list = join_DAO.getlistPdf(id);
                return View("InPDF", list);
            } 
            else
            {
                TempData["Message"] = "Bạn không có quyền InPdf";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ExportToPDF(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfKeHoachHieuChuan"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                   {
                        PdfPageSize = PdfPageSize.A4,
                        PdfPageOrientation = PdfPageOrientation.Landscape,
                        MarginLeft = 20,
                        MarginRight = 20,
                        MarginTop = 20,
                        MarginBottom =20
                   }
                    };
                     
                    var list = join_DAO.getlistPdf(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/KeHoachHieuChuan/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "quytrinhquanlythietbi.pdf";

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
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaKeHoachHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = join_DAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }

                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToAction("Index", "Home");
            }     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<KeHoachHieuChuan> ids)
        {
            var id = ids.FirstOrDefault()?.Id_KeHoachHieuChuan_Form;
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.KeHoachHieuChuans.Find(listNumber.Id);
                dbContext.KeHoachHieuChuans.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index", new { id });
        }
        public ActionResult DelTrash(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaKeHoachHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khhcDAO.GetRowsByIds(idList);
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = listNumber.Status == 1 ? 2 : 1;
                    khhcDAO.Update(listNumber);
                }
                TempData["Message"] = "Đã xóa vào thùng rác";
                return RedirectToReferrer();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ReTrash(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaKeHoachHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khhcDAO.GetRowsByIds(idList);
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }

                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = listNumber.Status == 2 ? 1 : 2;
                    khhcDAO.Update(listNumber);
                }

                TempData["Message"] = "Đã khôi phục";
                return RedirectToReferrer();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền khôi phục";
                return RedirectToReferrer();
            }    
            
        }
        public ActionResult Trash(int? id, string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("KeHoachHieuChuan"))
            {
                int pageSize = 10;
                int pageNumber = page ?? 1; 
                var ID = khhcfrom_DAO.getListId(id);
                var recordId = ID.FirstOrDefault()?.Id;
                ViewBag.Id = recordId; 
                var list = join_DAO.getlistTrash(id, Name, pageSize, pageNumber);
                return View("Trash", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }     
        }
        public ActionResult Index(int? id,string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("KeHoachHieuChuan"))
            {
                int pageSize = 10;
                int pageNumber = page ?? 1; 
                var ID = khhcfrom_DAO.getListId(id);
                var recordId = ID.FirstOrDefault()?.Id;
                ViewBag.Id = recordId; 
                var ngaytao = khhcfrom_DAO.getListId(id);
                var ngaytaopdf = ngaytao.FirstOrDefault()?.NgayTao;
                ViewBag.ngaytaopdf = ngaytaopdf.Value.ToString("yyyy"); 
                var list = join_DAO.getlistIndex(id, Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }     
        } 
        public ActionResult Create(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemKeHoachHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khhcfrom_DAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm ";
                return RedirectToReferrer(); 
            }     

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<KeHoachHieuChuan> ids)
        {
            var Id = ids.First().Id_KeHoachHieuChuan_Form; 
            foreach (var KeHoachHieuChuans in ids)
            {
                if (khhcDAO.CheckSerialNumberExists(KeHoachHieuChuans.Id_DanhMucThietBi, KeHoachHieuChuans.Id_KeHoachHieuChuan_Form))
                {
                    TempData["Message"] = "Thiết bị đã tồn tại";
                    return RedirectToAction("Index", "KeHoachHieuChuan", new { Id });
                }
                var oldlist = new KeHoachHieuChuan
                {
                    Id_DanhMucThietBi = KeHoachHieuChuans.Id_DanhMucThietBi,
                    Id_KeHoachHieuChuan_Form = KeHoachHieuChuans.Id_KeHoachHieuChuan_Form,
                    Jan_Plan = KeHoachHieuChuans.Jan_Plan   ,
                    Jan_Act = KeHoachHieuChuans.Jan_Act,
                    Feb_Plan = KeHoachHieuChuans.Feb_Plan,
                    Feb_Act = KeHoachHieuChuans.Feb_Act,
                    March_Plan = KeHoachHieuChuans.March_Plan   ,
                    March_Act = KeHoachHieuChuans.March_Act,
                    April_Plan = KeHoachHieuChuans.April_Plan,
                    April_Act = KeHoachHieuChuans.April_Act,
                    May_Plan = KeHoachHieuChuans.May_Plan,
                    May_Act = KeHoachHieuChuans.May_Act,
                    June_Plan = KeHoachHieuChuans.June_Plan,
                    June_Act = KeHoachHieuChuans.June_Act,
                    July_Plan = KeHoachHieuChuans.July_Plan,
                    July_Act = KeHoachHieuChuans.July_Act,
                    Aug_Plan = KeHoachHieuChuans.Aug_Plan,
                    Aug_Act = KeHoachHieuChuans.Aug_Act,
                    Sep_Plan = KeHoachHieuChuans.Sep_Plan,
                    Sep_Act = KeHoachHieuChuans.Sep_Act,
                    Oct_Plan = KeHoachHieuChuans.Oct_Plan,
                    Oct_Act = KeHoachHieuChuans.Oct_Act,
                    Nov_Plan = KeHoachHieuChuans.Nov_Plan,
                    Nov_Act = KeHoachHieuChuans.Nov_Act,
                    Dec_Plan = KeHoachHieuChuans.Dec_Plan,
                    Dec_Act = KeHoachHieuChuans.Dec_Act,
                    NgayTao = DateTime.Now,
                    NguoiTao = Session["Name"].ToString(),
                    Status = 1
                };
                khhcDAO.Insert(oldlist);
            } 
            TempData["Message"] = "Tạo thành công";
            ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
            return RedirectToAction("Index", "KeHoachHieuChuan", new { Id });
        }
        public ActionResult Details(int? id)
        {
            if (HasAccessAd() || HasAccessUser("KeHoachHieuChuan"))
            {
                var list = join_DAO.getlistId(id); 
                var recordId = list.FirstOrDefault()?.Id_KeHoachHieuChuan_form;
                ViewBag.Id = recordId; 
                return View("Details", list);
            } 
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
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
            if (HasAccessAd() || HasAccessUser("SuaKeHoachHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = join_DAO.GetRowsByIds(idList);

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
        public ActionResult Edit(List<KeHoachHieuChuan> ids)
        {
            var id = ids.FirstOrDefault()?.Id;
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.KeHoachHieuChuans
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KeHoachHieuChuan in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KeHoachHieuChuan.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_DanhMucThietBi = KeHoachHieuChuan.Id_DanhMucThietBi;
                        existingRecord.Jan_Plan = KeHoachHieuChuan.Jan_Plan;
                        existingRecord.Jan_Act = KeHoachHieuChuan.Jan_Act;
                        existingRecord.Feb_Plan = KeHoachHieuChuan.Feb_Plan;
                        existingRecord.Feb_Act = KeHoachHieuChuan.Feb_Act;
                        existingRecord.March_Plan = KeHoachHieuChuan.March_Plan;
                        existingRecord.March_Act = KeHoachHieuChuan.March_Act;
                        existingRecord.April_Plan = KeHoachHieuChuan.April_Plan;
                        existingRecord.April_Act = KeHoachHieuChuan.April_Act;
                        existingRecord.May_Plan = KeHoachHieuChuan.May_Plan;
                        existingRecord.May_Act = KeHoachHieuChuan.May_Act;
                        existingRecord.June_Plan = KeHoachHieuChuan.June_Plan;
                        existingRecord.June_Act = KeHoachHieuChuan.June_Act;
                        existingRecord.July_Plan = KeHoachHieuChuan.July_Plan;
                        existingRecord.July_Act = KeHoachHieuChuan.July_Act;
                        existingRecord.Aug_Plan = KeHoachHieuChuan.Aug_Plan;
                        existingRecord.Aug_Act = KeHoachHieuChuan.Aug_Act;
                        existingRecord.Sep_Plan = KeHoachHieuChuan.Sep_Plan;
                        existingRecord.Sep_Act = KeHoachHieuChuan.Sep_Act;
                        existingRecord.Oct_Plan = KeHoachHieuChuan.Oct_Plan;
                        existingRecord.Oct_Act = KeHoachHieuChuan.Oct_Act;
                        existingRecord.Nov_Plan = KeHoachHieuChuan.Nov_Plan;
                        existingRecord.Nov_Act = KeHoachHieuChuan.Nov_Act;
                        existingRecord.Dec_Plan = KeHoachHieuChuan.Dec_Plan;
                        existingRecord.Dec_Act = KeHoachHieuChuan.Dec_Act;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.ThietBi = new SelectList(dmtbDAO.getList("Index"), "Id", "MaThietBi");
                return RedirectToAction("Details", "KeHoachHieuChuan", new { id });
            }
        } 
    }
}
