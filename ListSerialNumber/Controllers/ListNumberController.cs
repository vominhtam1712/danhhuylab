using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Geom;
using ListClass.Dao;
using ListClass.Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace ListSerialNumber.Controllers
{
    public class ListNumberController : AccountLoginController
    {
        private readonly ListNumberDAO listnumberDAO = new ListNumberDAO();
        private readonly Join_KhachHang_Listnumber_DAO join_ = new Join_KhachHang_Listnumber_DAO();
        private readonly KhachHangDAO khDAO = new KhachHangDAO(); 
        private readonly ListDBContext dbContext = new ListDBContext(); 
        [HttpPost]
        public async Task<ActionResult> UploadImage(int listnumberId, HttpPostedFileBase imgFile)
        {
            if (HasAccessAd() || HasAccessUser("SuaHangHoa"))
            {
                if (imgFile != null && imgFile.ContentLength > 0)
                {
                    if (imgFile.ContentType.StartsWith("image"))
                    {
                        var company = await dbContext.ListNumbers.FindAsync(listnumberId);
                        if (company == null)
                        {
                            return Json(new { success = false, message = "Công ty không tồn tại." });
                        }

                        var fileName = System.IO.Path.GetFileName(imgFile.FileName);
                        var filePath = System.IO.Path.Combine(Server.MapPath("~/Public/Products"), fileName);

                        imgFile.SaveAs(filePath);

                        company.Image = fileName;
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
                return Json(new { success = false, message = "Bạn không thể sửa ảnh!" });
            }    
        }
        public ActionResult Index(string Name, int? page)
        { 
            if (HasAccessAd() || HasAccessUser("HangHoa"))
            {
                if (ViewBag.Count == null)
                {
                    var count = listnumberDAO.GetCountIndex();
                    ViewBag.Count = count;
                }

                int pageNumber = page ?? 1;
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 160;
                var list = join_.getlistjoin(Name, pageSize, pageNumber);

                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        } 
        public ActionResult Copy(string ids)
        {
            if (HasAccessAd() || HasAccessUser("CopyHangHoa"))
            {
                var idList = ids.Split(',').Select(int.Parse).ToList();

                foreach (var id in idList)
                {
                    var itemToCopy = dbContext.ListNumbers.Find(id);
                    if (itemToCopy != null)
                    {
                        var newItem = new ListNumber
                        {
                            Id_KhachHang = itemToCopy.Id_KhachHang,
                            Tenthietbi = itemToCopy.Tenthietbi,
                            Hang = itemToCopy.Hang,
                            Model = itemToCopy.Model,
                            SerialNumber = itemToCopy.SerialNumber,
                            PhamViDo = itemToCopy.PhamViDo,
                            DoPhanGiai = itemToCopy.DoPhanGiai,
                            NguoiTao = Session["Name"]?.ToString(),
                            GhiChu = itemToCopy.GhiChu,
                            NgaytaoSanPham = itemToCopy.NgaytaoSanPham,
                            Status = 1,
                        };
                        listnumberDAO.Insert(newItem);
                    }
                }
                return Json(new { success = true, message = "Copy thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền copy!" });
            }  
            
        }
        public JsonResult GetCustomerInfo(int id)
        {
            var khachHang = dbContext.KhachHangs.Find(id);  
            if (khachHang == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet); 
            }

            var result = new
            {
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
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemHangHoa"))
            {
                ViewBag.KhachHang = new SelectList(khDAO.getList("Index"), "Id", "TenKH");
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
        [ValidateInput(false)]
        public ActionResult Create(ListNumber listNumber, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(listNumber);
            } 

            if (listnumberDAO.CheckSerialNumberExists(listNumber.SerialNumber))
            {
                TempData["Message"] = "Sản phẩm đã tồn tại";
                return RedirectToAction("Index");
            }
            if (IsImageValid(file))
            {
                SaveUploadedFile(listNumber, file);
            }
            listNumber.NgaytaoSanPham = DateTime.Now;
            listNumber.Status = 1; 
            listNumber.NguoiTao = Session["Name"]?.ToString();
            listnumberDAO.Insert(listNumber);
            TempData["Message"] = "Thêm thành công";
            ViewBag.KhachHang = new SelectList(khDAO.getList("Index"), "Id", "TenKH");
            return RedirectToAction("Index");
        }
        public ActionResult EditNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaHangHoa"))
            {
                var idList = ParseIds(ids);
                var listNumbers = listnumberDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.KhachHang = new SelectList(khDAO.getList("Index"), "Id", "TenKH");
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
        [ValidateInput(false)]
        public ActionResult EditNhieu(List<ListNumber> listnumbers)
        {
            if (listnumbers == null || !listnumbers.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = listnumbers.Select(b => b.Id).ToList();

                var existingRecords = dbContext.ListNumbers
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var listNumber in listnumbers)
                { 
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == listNumber.Id); 
                    if (existingRecord != null)
                    {
                        existingRecord.Id_KhachHang = listNumber.Id_KhachHang;
                        existingRecord.Tenthietbi = listNumber.Tenthietbi;
                        existingRecord.PhamViDo = listNumber.PhamViDo;
                        existingRecord.DoPhanGiai = listNumber.DoPhanGiai;
                        existingRecord.Hang = listNumber.Hang;
                        existingRecord.Model = listNumber.Model;
                        existingRecord.SerialNumber = listNumber.SerialNumber;
                        existingRecord.GhiChu = listNumber.GhiChu;
                        existingRecord.NgaytaoSanPham = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"]?.ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                ViewBag.KhachHang = new SelectList(khDAO.getList("Index"), "Id", "TenKH");
                dbContext.SaveChanges();
                TempData["Message"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index");
            }
        } 
        public ActionResult Delete(string ids)
        { 
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaHangHoa"))
            {
                var idList = ParseIds(ids);
                var listNumbers = listnumberDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<ListNumber> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }
             
            var productIds = ids.Select(x => x.Id).ToList();
            var listNumbers = dbContext.ListNumbers.Where(x => productIds.Contains(x.Id)).ToList();

            foreach (var oldList in listNumbers)
            { 
                DeleteProductImage(oldList.Image);
                 
                dbContext.ListNumbers.Remove(oldList);
            }

            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcel()
        {
            if (HasAccessAd() || HasAccessUser("ExcelHangHoa"))
            {
                try
                {
                    var data = listnumberDAO.getListAll();

                    DataTable dt = CreateDataTable(data);

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Sanphams");
                        worksheet.Cell(1, 1).InsertTable(dt);
                        AddImagesToWorksheet(worksheet, data, dt.Columns.Count);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sanphams.xlsx");
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("HangHoa"))
            {
                var data = dbContext.ListNumbers
                .Include(ln => ln.KhachHang)
                .Include(ln => ln.PhieuNhanThietBis.  

                Select(pntb => pntb.Phieuyeucauhieuchuans.
                Select(pychc => pychc.PhanCongs.
                Select(pc => pc.BienBanHieuChuans.
                Select(bbhc => bbhc.Giaychungnhans.
                Select(gcn => gcn.Phieutrathietbis))))))

                .Include(ln => ln.PhieuNhanThietBis.

                Select(pntb => pntb.Phieuyeucauhieuchuans.
                Select(pychc => pychc.PhanCongs.
                Select(pc => pc.BienBanHieuChuans.
                Select(bbhc => bbhc.Giaychungnhans.
                Select(gcn => gcn.BaoCaoSuaChuas))))))

                .FirstOrDefault(ln => ln.Id == id);
                if (data == null)
                {
                    return HttpNotFound();
                }
                return View(data);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            } 
        }   
        public ActionResult ExportToExcelOne(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelHangHoa"))
            {
                var data = listnumberDAO.getListOne(id);
                if (data == null || !data.Any())
                {
                    TempData["Message"] = "Không có dữ liệu để xuất";
                    return RedirectToAction("Index", "Home");
                }
                DataTable dt = CreateDataTable(data);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sanphams");
                    worksheet.Cell(1, 1).InsertTable(dt);
                    AddImagesToWorksheet(worksheet, data, dt.Columns.Count);

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sanphams.xlsx");
                    }
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPdf";
                return RedirectToReferrer();
            }    
            
        } 
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (HasAccessAd() || HasAccessUser("ThemHangHoa"))
            {
                if (file != null && file.ContentLength > 0)
                {
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        List<string> existingSerialNumbers = new List<string>();
                        List<ListNumber> newListNumbers = new List<ListNumber>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var Tenthietbis = worksheet.Cells[row, 1].Text.Trim();
                            var Hangs = worksheet.Cells[row, 2].Text.Trim();
                            var Models = worksheet.Cells[row, 3].Text.Trim();
                            var SerialNumbers = worksheet.Cells[row, 4].Text.Trim();
                            if (listnumberDAO.CheckSerialNumberExists(SerialNumbers))
                            {
                                existingSerialNumbers.Add(SerialNumbers);
                            }
                            else
                            {
                                var listNumber = new ListNumber
                                {
                                    Tenthietbi = Tenthietbis,
                                    Hang = Hangs,
                                    Model = Models,
                                    SerialNumber = SerialNumbers,
                                    NgaytaoSanPham = DateTime.Now,
                                    Status = 1,
                                    Image = "",
                                };

                                newListNumbers.Add(listNumber);
                            }
                        }

                        foreach (var listNumber in newListNumbers)
                        {
                            listnumberDAO.Insert(listNumber);
                        }

                        if (existingSerialNumbers.Any())
                        {
                            TempData["Message"] = $"Các sản phẩm sau đã tồn tại: {string.Join(", ", existingSerialNumbers)}";
                        }
                        else
                        {
                            TempData["Message"] = "Tải lên thành công";
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            } 
        }  
    }
}
