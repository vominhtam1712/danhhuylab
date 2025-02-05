using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Commons.Utils;
using iText.Kernel.Geom;
using iText.Layout.Element;
using iText.Svg.Processors;
using ListClass.Dao;
using ListClass.Model;
using ListSerialNumber.Areas.Admin.Controllers;
using Rotativa;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class PhieuyeucauhieuchuanController : AccountLoginController
    {
        private readonly PhieuyeucauhieuchuanDAO phieuyeucauhieuchuanDAO = new PhieuyeucauhieuchuanDAO();
        private readonly PhieuNhanThietBiDAO phieunhanthietbiDAO = new PhieuNhanThietBiDAO();
        private readonly Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK_DAO join = new Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK_DAO();
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
            if (HasAccessAd() || HasAccessUser("YeuCauHieuChuan"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1; 
                var count = join.GetCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber; 
                var list = join.getlistjoin(Name, pageSize, pageNumber);
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
            if (HasAccessAd() || HasAccessUser("InPdfYeuCauHieuChuan"))
            {
                var list = join.getlistjoin(id);
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }    
           
        }  
        public ActionResult Details(int? id)
        {
            if (HasAccessAd() || HasAccessUser("YeuCauHieuChuan"))
            {
                var phieuHieuChuanJoin = join.getlistjoin(id);
                return View("Details", phieuHieuChuanJoin);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }     
        }
        public ActionResult chitiet(int? id)
        {
            if (HasAccessAd() || HasAccessUser("YeuCauHieuChuan"))
            {
                var phieuHieuChuanJoin = join.getlistjoin(id);
                return View("chitiet", phieuHieuChuanJoin);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemYeuCauHieuChuan"))
            {
                ViewBag.SoYeuCauList = new SelectList(phieunhanthietbiDAO.getList("Index"), "Id", "SoNhanDang");
                return View();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }    
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Phieuyeucauhieuchuan phieuyeucauhieuchuan)
        {
            if (ModelState.IsValid)
            {
                phieuyeucauhieuchuan.MaPhieu = phieuyeucauhieuchuanDAO.Layphieutudong();
                phieuyeucauhieuchuan.Createby = Session["Name"].ToString();
                phieuyeucauhieuchuan.NgayTao = DateTime.Now;
                phieuyeucauhieuchuan.Status = 1;
                phieuyeucauhieuchuanDAO.Insert(phieuyeucauhieuchuan);
                TempData["Message"] = "Tạo thành công";
                return RedirectToAction("Index", "Phieuyeucauhieuchuan");
            }
            ViewBag.SoYeuCauList = new SelectList(phieunhanthietbiDAO.getList("Index"), "Id", "SoNhanDang");
            return View(phieuyeucauhieuchuan);
        }
        public ActionResult EditNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaYeuCauHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuanDAO.GetRowsByIds(idList);

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
        public ActionResult EditNhieu(List<Phieuyeucauhieuchuan> Phieuyeucauhieuchuans)
        {
            foreach (var phieuyeucauhieuchuan in Phieuyeucauhieuchuans)
            {
                Phieuyeucauhieuchuan oldlist = dbContext.Phieuyeucauhieuchuans.Find(phieuyeucauhieuchuan.Id);
                oldlist.MaPhieu = phieuyeucauhieuchuan.MaPhieu;
                oldlist.Id_phieunhanthietbi = phieuyeucauhieuchuan.Id_phieunhanthietbi;
                oldlist.Diachihieuchuan = phieuyeucauhieuchuan.Diachihieuchuan;
                oldlist.Tencognty = phieuyeucauhieuchuan.Tencognty;
                oldlist.Diachicongty = phieuyeucauhieuchuan.Diachicongty;
                oldlist.Masothue = phieuyeucauhieuchuan.Masothue;
                oldlist.Soluong = phieuyeucauhieuchuan.Soluong;
                oldlist.Dichvuyeucau = phieuyeucauhieuchuan.Dichvuyeucau;
                oldlist.NgayTao = DateTime.Now;
                oldlist.Createby = Session["Name"].ToString();
                oldlist.Danhhuy = phieuyeucauhieuchuan.Danhhuy;
                oldlist.UpdateBy = Session["Name"].ToString();
                oldlist.Status = 1;
                dbContext.Entry(oldlist).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            TempData["Message"] = "Chỉnh sửa thành công";
            return RedirectToAction("Index", "Phieuyeucauhieuchuan");
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaYeuCauHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuanDAO.GetRowsByIds(idList); 
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
        public ActionResult Delete(List<Phieuyeucauhieuchuan> ids)
        {
            foreach (Phieuyeucauhieuchuan phieuyeucauhieuchuan in ids)
            {
                var oldlist = dbContext.Phieuyeucauhieuchuans.Find(phieuyeucauhieuchuan.Id);
                dbContext.Phieuyeucauhieuchuans.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult DelTrash(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaYeuCauHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuanDAO.GetRowsByIds(idList); 
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = (listNumber.Status == 1) ? 2 : 1;
                    phieuyeucauhieuchuanDAO.Update(listNumber);
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Đã xóa vào thùng rác";
                return RedirectToAction("Index", "Phieuyeucauhieuchuan");
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
            if (HasAccessAd() || HasAccessUser("XoaYeuCauHieuChuan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieuyeucauhieuchuanDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                foreach (var listNumber in listNumbers)
                {
                    listNumber.Status = (listNumber.Status == 2) ? 1 : 2;
                    phieuyeucauhieuchuanDAO.Update(listNumber);
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Đã khôi phục";
                return RedirectToAction("Trash", "Phieuyeucauhieuchuan");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền khôi phục";
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
            if (HasAccessAd() || HasAccessUser("InPdfYeuCauHieuChuan"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                            {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Portrait,
                                MarginLeft = 30,
                                MarginRight = 30,
                                MarginTop = 10,
                                MarginBottom = 10
                            }
                    };

                    var list = join.getlistjoin(id);
                    if (list == null)
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }

                    var htmlPdf = base.RenderPartialTostring("~/Views/Phieuyeucauhieuchuan/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"~/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Phieuyecauhieuchuan.pdf";

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
        public ActionResult ExportToExcel()
        {
            if (HasAccessAd() || HasAccessUser("ExcelYeuCauHieuChuan"))
            {
                try
                {
                    var data = join.getlistjoin();
                     
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {
                            new DataColumn("STT", typeof(int)),
                            new DataColumn("SoNhanDang", typeof(string)),
                            new DataColumn("NgayThucHien", typeof(DateTime)),
                            new DataColumn("KhachHang", typeof(string)),
                            new DataColumn("DiaChi", typeof(string)),
                            new DataColumn("Diachihieuchuan", typeof(string)),
                            new DataColumn("LienHe", typeof(string)),
                            new DataColumn("Tencognty", typeof(string)),
                            new DataColumn("Diachicongty", typeof(string)),
                            new DataColumn("Masothue", typeof(string)),
                            new DataColumn("Model", typeof(string)),
                            new DataColumn("SerialNumber", typeof(string)),
                            new DataColumn("Soluong", typeof(string)),
                            new DataColumn("Dichvuyeucau", typeof(string)),
                            new DataColumn("Danhhuy", typeof(string))
                    });

                    int rowIndex = 1;

                    foreach (var product in data)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.SoNhanDang,
                            product.NgayThucHien,
                            product.KhachHang,
                            product.DiaChi,
                            product.Diachihieuchuan,
                            product.LienHe,
                            product.Tencognty,
                            product.Diachicongty,
                            product.Masothue,
                            product.Model,
                            product.SerialNumber,
                            product.Soluong,
                            product.Dichvuyeucau,
                            product.Danhhuy);
                    }
                     
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Phieuyeucauhieuchuans");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Phieuyeucauhieuchuans.xlsx");
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
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }     
        } 
    }
}
