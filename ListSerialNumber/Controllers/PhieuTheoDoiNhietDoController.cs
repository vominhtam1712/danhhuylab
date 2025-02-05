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
    public class PhieuTheoDoiNhietDoController : AccountLoginController
    {
        private readonly PhieuTheoDoiNhietDoDAO phieutheodoinhietdoDAO = new PhieuTheoDoiNhietDoDAO();
        private readonly DanhSachPhieuTheoDoiNhietDoDAO danhsachDAO = new DanhSachPhieuTheoDoiNhietDoDAO();
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo_DAO ph = new join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo_DAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public ActionResult Details(string Name, int? thang, int? nam, int? page)
        {
            if (HasAccessAd() || HasAccessUser("NhietDoPhongLab"))
            {
                int pageSize = string.IsNullOrEmpty(Name) && !thang.HasValue && !nam.HasValue ? 10 : 150;

                int pageNumber = page ?? 1;

                var list = phieutheodoinhietdoDAO.getList(Name, thang, nam, pageSize, pageNumber);
                 
                ViewBag.thang = dbContext.PhieuTheoDoiNhietDos.Select(kv => kv.Thang_Nam.Month).Distinct().ToList();
                ViewBag.nam = dbContext.PhieuTheoDoiNhietDos.Select(td => td.Thang_Nam.Year).Distinct().ToList();

                return View("Details", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }

        public ActionResult TaoPhieuTheoDoiNhietDo()
        {
            if (HasAccessAd() || HasAccessUser("ThemNhietDoPhongLab"))
            {
                return View("TaoPhieuTheoDoiNhietDo");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }     
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoPhieuTheoDoiNhietDo(PhieuTheoDoiNhietDo PhieuTheoDoiNhietDo)
        {
            if (PhieuTheoDoiNhietDo == null)
            {
                return RedirectToAction("Index", "PhieuTheoDoiNhietDo");
            }
            var errorMessage = ValidatePhieu(PhieuTheoDoiNhietDo);
            if (errorMessage != null)
            {
                TempData["Message"] = "Một vài thông tin không hợp lệ";
                return RedirectToAction("TaoPhieuTheoDoiNhietDo", "PhieuTheoDoiNhietDo");
            }
            PhieuTheoDoiNhietDo.MaPhieuTheoDoiNhietDo = phieutheodoinhietdoDAO.Layphieutudong();
            PhieuTheoDoiNhietDo.NgayTao = DateTime.Now;
            PhieuTheoDoiNhietDo.status = 1;
            PhieuTheoDoiNhietDo.NguoiTao = Session["Name"]?.ToString();
            phieutheodoinhietdoDAO.Insert(PhieuTheoDoiNhietDo);
            TempData["Message"] = "Tạo thành công";
            return RedirectToAction("Details");
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
                var listNumbers = phieutheodoinhietdoDAO.GetRowsByIds(idList);

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
        public ActionResult ChinhSua(List<PhieuTheoDoiNhietDo> PhieuTheoDoiNhietDos)
        {
            if (PhieuTheoDoiNhietDos == null || !PhieuTheoDoiNhietDos.Any())
            {
                return RedirectWithMessage("Danh sách phân công không hợp lệ.");
            }

            try
            {
                foreach (var phieu in PhieuTheoDoiNhietDos)
                {
                    var errorMessage = ValidatePhieu(phieu);
                    if (errorMessage != null)
                    {
                        return RedirectWithMessage(errorMessage);
                    }
                }

                using (var dbContext = new ListDBContext())
                {
                    var idsToUpdate = PhieuTheoDoiNhietDos.Select(b => b.Id).ToList();
                    var existingRecords = dbContext.PhieuTheoDoiNhietDos.Where(b => idsToUpdate.Contains(b.Id)).ToList();

                    foreach (var phieu in PhieuTheoDoiNhietDos)
                    {
                        var existingRecord = existingRecords.FirstOrDefault(b => b.Id == phieu.Id);
                        if (existingRecord != null)
                        {
                            UpdatePhieu(existingRecord, phieu);
                        }
                    }
                    dbContext.SaveChanges();
                }
                TempData["Message"] = "Câp nhật thành công";
                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                return RedirectWithMessage($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        public ActionResult Xoa(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaNhietDoPhongLab"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phieutheodoinhietdoDAO.GetRowsByIds(idList);

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
        public ActionResult Xoa(List<PhieuTheoDoiNhietDo> PhieuTheoDoiNhietDos)
        { 
            foreach (PhieuTheoDoiNhietDo PhieuTheoDoiNhietDo in PhieuTheoDoiNhietDos)
            {
                var oldlist = dbContext.PhieuTheoDoiNhietDos.Find(PhieuTheoDoiNhietDo.Id);
                dbContext.PhieuTheoDoiNhietDos.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details");
        }

        public ActionResult ExportToExcel(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelNhietDoPhongLab"))
            {
                try
                {
                    var data = phieutheodoinhietdoDAO.getList(ids);
                     
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {

                            new DataColumn("Ngày", typeof(int)),
                            new DataColumn("Mã phiếu", typeof(string)),
                            new DataColumn("Mã số nhiệt kế/Model No", typeof(string)),
                            new DataColumn("Phòng", typeof(string)),
                            new DataColumn("Ngày tháng năm", typeof(DateTime)),
                            new DataColumn("Khoảng chấp nhận: Nhiệt độ (°C)_1", typeof(decimal)),
                            new DataColumn("Khoảng chấp nhận: Nhiệt độ (°C)_2", typeof(decimal)),
                            new DataColumn("Độ ẩm (%)", typeof(decimal)),
                            new DataColumn("Ngày tạo", typeof(DateTime)),
                            new DataColumn("Người tạo", typeof(string)),
                    });

                    int rowIndex = 1;

                    foreach (var product in data)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.MaPhieuTheoDoiNhietDo,
                            product.MSNhietKe_Model_No,
                            product.Phong,
                            product.Thang_Nam,
                            product.NhietDo_Dau,
                            product.NhietDo_Sau,
                            product.DoAm,
                            product.NgayTao,
                            product.NguoiTao
                            );
                    }
                     
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("phieutheodoinhietdo");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "phieutheodoinhietdo.xlsx");
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
        public ActionResult Copy(string ids)
        {
            if (HasAccessAd() || HasAccessUser("CopyNhietDoPhongLab"))
            {
                var idList = ids.Split(',').Select(int.Parse).ToList();

                foreach (var id in idList)
                {
                    var itemToCopy = dbContext.PhieuTheoDoiNhietDos.Find(id);
                    if (itemToCopy != null)
                    {
                        var newItem = new PhieuTheoDoiNhietDo
                        {
                            MaPhieuTheoDoiNhietDo = phieutheodoinhietdoDAO.Layphieutudong(),
                            MSNhietKe_Model_No = itemToCopy.MSNhietKe_Model_No,
                            Phong = itemToCopy.Phong,
                            Thang_Nam = itemToCopy.Thang_Nam,
                            NhietDo_Dau = itemToCopy.NhietDo_Dau,
                            NhietDo_Sau = itemToCopy.NhietDo_Sau,
                            DoAm = itemToCopy.DoAm,
                            NgayTao = DateTime.Now,
                            status = 1,
                            NguoiTao = Session["Name"]?.ToString()
                        };
                        phieutheodoinhietdoDAO.Insert(newItem);

                        var danhsachs = dbContext.DanhSachPhieuTheoDoiNhietDos.Where(m => m.ID_PhieuTheoDoiNhietDo == id).ToList();
                        foreach (var danhsach in danhsachs)
                        {
                            var newDanhsach = new DanhSachPhieuTheoDoiNhietDo
                            {
                                ID_PhieuTheoDoiNhietDo = newItem.Id,
                                Ngay_theo_doi = danhsach.Ngay_theo_doi,
                                NhietDo_Sang = danhsach.NhietDo_Sang,
                                DoAm_Sang = danhsach.DoAm_Sang,
                                NhietDo_Chieu = danhsach.NhietDo_Chieu,
                                DoAm_Chieu = danhsach.DoAm_Chieu,
                                KetLuan = danhsach.KetLuan,
                                NguoiKiemTra = Session["Name"]?.ToString(),
                                Ngay = DateTime.Now
                            };
                            danhsachDAO.Insert(newDanhsach);
                        }
                    }
                }
                return Json(new { success = true, message = "Copy thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền copy!" });
            }    

               
        } 
    }
}
