using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using ListClass.Dao;
using ListClass.Model;

namespace ListSerialNumber.Controllers
{
    public class KhachHangController : AccountLoginController
    {
        private readonly KhachHangDAO khDAO = new KhachHangDAO();
        private readonly TapDoanDAO tapdoanDAO = new TapDoanDAO();
        private readonly KhuVucDAO khuvucDAO = new KhuVucDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public ActionResult Index(string Name, string Tenkhuvuc, string Tentapdoan, int? page)
        {
            if (HasAccessAd() || HasAccessUser("KhachHang"))
            {
                int pageSize = string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Tenkhuvuc) && string.IsNullOrEmpty(Tentapdoan) ? 10 : 160;
                int pageNumber = page ?? 1;

                var count = khDAO.getCountIndex();
                ViewBag.Count = count;

                int totalPages = (int)Math.Ceiling((double)count / pageSize);
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;
                 
                var list = khDAO.getList(Name, Tenkhuvuc, Tentapdoan, pageSize, pageNumber);
                 
                ViewBag.TenKhuVucs = dbContext.KhuVucs.Select(kv => kv.Tenkhuvuc).ToList();
                ViewBag.TenTapDoans = dbContext.TapDoans.Select(td => td.Tentapdoan).ToList();
                 
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }

        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemKhachHang"))
            {
                ViewBag.TapDoan = new SelectList(tapdoanDAO.getList("Index"), "Id", "Tentapdoan");
                ViewBag.KhuVuc = new SelectList(khuvucDAO.getList("Index"), "Id", "Tenkhuvuc");
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
        public ActionResult Create(KhachHang khachhang)
        {
            if (!ModelState.IsValid)
            {
                return View(khachhang);
            }

            if (khDAO.CheckExists(khachhang.TenKH))
            {
                TempData["Message"] = "Khách hàng đã tồn tại";
                return RedirectToAction("Index");
            } 
            khachhang.MaKH = khDAO.Layphieutudong();
            khachhang.NgayTao = DateTime.Now;
            khachhang.Status = 1;
            khachhang.NguoiTao = Session["Name"]?.ToString();
            khDAO.Insert(khachhang); 
            TempData["Message"] = "Thêm thành công";
            ViewBag.TapDoan = new SelectList(tapdoanDAO.getList("Index"), "Id", "Tentapdoan");
            ViewBag.KhuVuc = new SelectList(khuvucDAO.getList("Index"), "Id", "Tenkhuvuc");
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaKhachHang"))
            { 
                var idList = ParseIds(ids);
                var listNumbers = khDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.TapDoan = new SelectList(tapdoanDAO.getList("Index"), "Id", "Tentapdoan");
                ViewBag.KhuVuc = new SelectList(khuvucDAO.getList("Index"), "Id", "Tenkhuvuc");
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
        public ActionResult Edit(List<KhachHang> KhachHangs)
        {
            if (KhachHangs == null || !KhachHangs.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = KhachHangs.Select(b => b.Id).ToList();

                var existingRecords = dbContext.KhachHangs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KhachHang in KhachHangs)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KhachHang.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.MaKH = KhachHang.MaKH;
                        existingRecord.TenKH = KhachHang.TenKH;
                        existingRecord.DiaChi = KhachHang.DiaChi;
                        existingRecord.Email = KhachHang.Email;
                        existingRecord.SDT = KhachHang.SDT;
                        existingRecord.LienHe = KhachHang.LienHe;
                        existingRecord.Ghichu = KhachHang.Ghichu;
                        existingRecord.NhomNganh = KhachHang.NhomNganh;
                        existingRecord.Id_TapDoan = KhachHang.Id_TapDoan;
                        existingRecord.Id_KhuVuc = KhachHang.Id_KhuVuc;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.TapDoan = new SelectList(tapdoanDAO.getList("Index"), "Id", "Tentapdoan");
                ViewBag.KhuVuc = new SelectList(khuvucDAO.getList("Index"), "Id", "Tenkhuvuc");
                return RedirectToAction("Index", "KhachHang");
            }
        }
        public ActionResult ChinhSua(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaKhachHang"))
            { 
                var listNumbers = khDAO.getListId(id); 
                ViewBag.TapDoan = new SelectList(tapdoanDAO.getList("Index"), "Id", "Tentapdoan");
                ViewBag.KhuVuc = new SelectList(khuvucDAO.getList("Index"), "Id", "Tenkhuvuc");
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
        public ActionResult ChinhSua(List<KhachHang> KhachHangs)
        {
            if (KhachHangs == null || !KhachHangs.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = KhachHangs.Select(b => b.Id).ToList();

                var existingRecords = dbContext.KhachHangs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var KhachHang in KhachHangs)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == KhachHang.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.MaKH = KhachHang.MaKH;
                        existingRecord.TenKH = KhachHang.TenKH;
                        existingRecord.DiaChi = KhachHang.DiaChi;
                        existingRecord.Email = KhachHang.Email;
                        existingRecord.SDT = KhachHang.SDT;
                        existingRecord.LienHe = KhachHang.LienHe;
                        existingRecord.Ghichu = KhachHang.Ghichu;
                        existingRecord.NhomNganh = KhachHang.NhomNganh;
                        existingRecord.Id_TapDoan = KhachHang.Id_TapDoan;
                        existingRecord.Id_KhuVuc = KhachHang.Id_KhuVuc;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.TapDoan = new SelectList(tapdoanDAO.getList("Index"), "Id", "Tentapdoan");
                ViewBag.KhuVuc = new SelectList(khuvucDAO.getList("Index"), "Id", "Tenkhuvuc");
                return RedirectToAction("Index", "KhachHang");
            }
        } 
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaKhachHang"))
            {
                var idList = ParseIds(ids);
                var listNumbers = khDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<KhachHang> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.KhachHangs.Find(listNumber.Id);
                dbContext.KhachHangs.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcelOne(int? id)
        {
            if (HasAccessAd() || HasAccessUser("ExcelKhachHang"))
            {
                try
                {
                    var data = khDAO.getListOne(id);

                    DataTable dt = CreateDataTable(data);

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("KhachHangs");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KhachHangs.xlsx");
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
        public ActionResult ExportToExcel()
        {
            if (HasAccessAd() || HasAccessUser("ExcelKhachHang"))
            {
                try
                {
                    var data = khDAO.getListAll();

                    DataTable dt = CreateDataTable(data);

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("KhachHangs");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "KhachHangs.xlsx");
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
        private DataTable CreateDataTable(IEnumerable<KhachHang> data)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("STT", typeof(int)),
                new DataColumn("Mã KH", typeof(string)),
                new DataColumn("Tên KH", typeof(string)),
                new DataColumn("Địa chỉ", typeof(string)),
                new DataColumn("Email", typeof(string)),
                new DataColumn("SDT", typeof(string)),
                new DataColumn("Liên hệ", typeof(string)),
                new DataColumn("Nhóm ngành", typeof(string)),
                new DataColumn("Ghi chú", typeof(string)),
            });

            int rowIndex = 1;
            foreach (var product in data)
            {
                dt.Rows.Add(
                    rowIndex++,
                    product.MaKH,
                    product.TenKH,
                    product.DiaChi,
                    product.Email,
                    product.SDT,
                    product.LienHe,
                    product.NhomNganh,
                    product.Ghichu);
            }

            return dt;
        } 
    }
}
