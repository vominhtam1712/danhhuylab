using ListClass.Dao;
using ListClass.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class GiaTriTruocHieuChinhController : AccountLoginController
    {
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly GiaTriTruocHieuChinhDAO giatritruochieuchinhDAO = new GiaTriTruocHieuChinhDAO();
        private readonly NgayTaoTechnicialBuldTruocHieuChinhDAO ngaytaotruoc = new NgayTaoTechnicialBuldTruocHieuChinhDAO();
        private readonly BulbSpectrumDAO bulbspectrumDAO = new BulbSpectrumDAO();
        private readonly LampSystemsDAO lampsystemsDAO = new LampSystemsDAO();
        private readonly ListDBContext dbContext = new ListDBContext(); 
        public ActionResult Them1kenh(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
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
        public ActionResult Them1kenh(List<GiaTriTruocHieuChinh> ids)
        {
            var Id = ids.FirstOrDefault()?.Id_PhanCong;
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Danh sách không hợp lệ.";
                return RedirectToAction("Index", "PhieuTra_form");
            }
            
            foreach (var idss in ids)
            {  
                var oldlist = new GiaTriTruocHieuChinh
                {
                    Id_PhanCong = idss.Id_PhanCong,
                    BuocSong = idss.BuocSong,
                    Measurand_1 = "Irradiance",
                    Reference_1 = idss.Reference_1,
                    Instrument_1 = idss.Instrument_1,
                    Deviation_1 = ((idss.Instrument_1 - idss.Reference_1) / idss.Reference_1) * 100,
                    Measurand_2 = "Energy Density",
                    Reference_2 = idss.Reference_2,
                    Instrument_2 = idss.Instrument_2,
                    Deviation_2 = ((idss.Instrument_2 - idss.Reference_2) / idss.Reference_2) * 100,
                    NgayTao = DateTime.Now,
                    NguoiTao = Session["Name"].ToString(),
                    Status = 1
                };
                giatritruochieuchinhDAO.Insert(oldlist);
            }
            TempData["Message"] = "Tạo thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult ImportExcel(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
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
        public ActionResult Upload(List<GiaTriTruocHieuChinh> ids, HttpPostedFileBase file)
        {
            if (HasAccessAd() || HasAccessUser("ThemThucHien"))
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
                        foreach (var stdDut in ids)
                        {

                            List<GiaTriTruocHieuChinh> newListNumbers = new List<GiaTriTruocHieuChinh>();
                            for (int row = 2; row <= rowCount; row++)
                            {
                                try
                                {
                                    var buocsong = worksheet.Cells[row, 1].Text.Trim();
                                    float reference_1 = ParseFloat(worksheet.Cells[row, 2].Text.Trim());
                                    float instrument_1 = ParseFloat(worksheet.Cells[row, 3].Text.Trim());
                                    float reference_2 = ParseFloat(worksheet.Cells[row, 4].Text.Trim());
                                    float instrument_2 = ParseFloat(worksheet.Cells[row, 5].Text.Trim());
                                    float deviation_1 = ((instrument_1 - reference_1) / reference_1) * 100;
                                    float deviation_2 = ((instrument_2 - reference_2) / reference_2) * 100;
                                    var measurand_1 = "Irradiance";
                                    var measurand_2 = "Energy Density";
                                    DateTime ngaytao = DateTime.Now;
                                    var std_dut = new GiaTriTruocHieuChinh
                                    {
                                        Id_PhanCong = stdDut.Id_PhanCong,
                                        BuocSong = buocsong,
                                        Measurand_1 = measurand_1,
                                        Reference_1 = reference_1,
                                        Instrument_1 = instrument_1,
                                        Deviation_1 = deviation_1,
                                        Measurand_2 = measurand_2,
                                        Reference_2 = reference_2,
                                        Instrument_2 = instrument_2,
                                        Deviation_2 = deviation_2,
                                        NgayTao = ngaytao,
                                        NguoiTao = creatorName,
                                        Status = 1
                                    };
                                    newListNumbers.Add(std_dut);
                                    giatritruochieuchinhDAO.Insert(std_dut);
                                }
                                catch (Exception ex)
                                {
                                    TempData["Message"] = "Tải lên thành công";
                                    var Ids = ids.FirstOrDefault()?.Id_PhanCong;
                                    return RedirectToAction("Details", "BienBanHieuChuan", new { Id = Ids });
                                }
                            }
                        }
                    }
                }
                TempData["Message"] = "Tải lên thành công";
                var Id = ids.FirstOrDefault()?.Id_PhanCong;
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm ";
                return RedirectToReferrer();
            }    
        }
        private float ParseFloat(string input)
        {
            if (float.TryParse(input, out float result))
            {
                return result;
            }
            return 0f;
        }
        public ActionResult Them4kenh(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
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
        public ActionResult Them4kenh(List<GiaTriTruocHieuChinh> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            }

            var Id = ids.First().Id_PhanCong; 
            var firstIdss = ids.First();
            
            for (int j = 0; j < 4; j++)
            {
                float reference1 = float.TryParse(Request.Form[$"ids[{j}].Reference_1"], out float ref1) ? ref1 : 0;
                float instrument1 = float.TryParse(Request.Form[$"ids[{j}].Instrument_1"], out float instr1) ? instr1 : 0;
                float reference2 = float.TryParse(Request.Form[$"ids[{j}].Reference_2"], out float ref2) ? ref2 : 0;
                float instrument2 = float.TryParse(Request.Form[$"ids[{j}].Instrument_2"], out float instr2) ? instr2 : 0; 
                var oldlist = new GiaTriTruocHieuChinh
                {
                    Id_PhanCong = firstIdss.Id_PhanCong, 
                    BuocSong = new[] { "UV-A", "UV-B", "UV-C", "UV-V" }[j],
                    Measurand_1 = "Irradiance",
                    Reference_1 = reference1,
                    Instrument_1 = instrument1,
                    Deviation_1 = ((instrument1 - reference1) / reference1) * 100,
                    Measurand_2 = "Energy Density",
                    Reference_2 = reference2,
                    Instrument_2 = instrument2,
                    Deviation_2 = ((instrument2 - reference2) / reference2) * 100,
                    NgayTao = DateTime.Now,
                    NguoiTao = Session["Name"].ToString(),
                    Status = 1
                };

                giatritruochieuchinhDAO.Insert(oldlist);
            }
            TempData["Message"] = "Tạo thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = giatritruochieuchinhDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sưa ";
                return RedirectToReferrer();
            }    
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<GiaTriTruocHieuChinh> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.GiaTriTruocHieuChinhs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();
                var Id = ids.First().Id;
                foreach (var idss in ids)
                {
                    if (idss.Instrument_1 < idss.Reference_1 || idss.Instrument_2 < idss.Reference_2)
                    {
                        TempData["Message"] = "Instrument phải lớn hơn Reference";
                        return RedirectToAction("Edit", "GiaTriTruocHieuChinh", new { ids = Id });
                    }
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == idss.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_PhanCong = idss.Id_PhanCong;
                        existingRecord.BuocSong = idss.BuocSong;
                        existingRecord.Measurand_1 = "Irradiance";
                        existingRecord.Reference_1 = idss.Reference_1;
                        existingRecord.Instrument_1 = idss.Instrument_1;
                        existingRecord.Deviation_1 = ((idss.Instrument_1 - idss.Reference_1)/ idss.Reference_1)*100;
                        existingRecord.Measurand_2 = "Energy Density";
                        existingRecord.Reference_2 = idss.Reference_2;
                        existingRecord.Instrument_2 = idss.Instrument_2;
                        existingRecord.Deviation_2 = ((idss.Instrument_2 - idss.Reference_2) / idss.Reference_2) * 100;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                var id = ids.First().Id_PhanCong;
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id = id });
            }
        }
        public ActionResult EditNgay(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = ngaytaotruoc.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.BulbSpectrum = new SelectList(bulbspectrumDAO.getList("Index"), "Id", "Bulb_Spectrum");
                ViewBag.LampSystems = new SelectList(lampsystemsDAO.getList("Index"), "Id", "Lamp_Systems");
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sửa ";
                return RedirectToReferrer();
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNgay(List<NgayTaoTechnicialBuldTruocHieuChinh> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.NgayTaoTechnicialBuldTruocHieuChinhs
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var idss in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == idss.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_PhanCong = idss.Id_PhanCong;
                        existingRecord.NgayTao = idss.NgayTao; 
                        existingRecord.Technicial = idss.Technicial;
                        existingRecord.Id_LampSystems = idss.Id_LampSystems; 
                        existingRecord.Id_BulbSpectrum = idss.Id_BulbSpectrum; 
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                var id = ids.First().Id_PhanCong;
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.BulbSpectrum = new SelectList(bulbspectrumDAO.getList("Index"), "Id", "Bulb_Spectrum");
                ViewBag.LampSystems = new SelectList(lampsystemsDAO.getList("Index"), "Id", "Lamp_Systems");
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id = id });
            }
        }
        public ActionResult ThemNgay(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.BulbSpectrum = new SelectList(bulbspectrumDAO.getList("Index"), "Id", "Bulb_Spectrum");
                ViewBag.LampSystems = new SelectList(lampsystemsDAO.getList("Index"), "Id", "Lamp_Systems");
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
        public ActionResult ThemNgay(List<NgayTaoTechnicialBuldTruocHieuChinh> ngaytaotruochieuchinhs)
        {
            var Id = ngaytaotruochieuchinhs.FirstOrDefault()?.Id_PhanCong;
            if (ngaytaotruochieuchinhs == null || !ngaytaotruochieuchinhs.Any())
            {
                TempData["Message"] = "Danh sách không hợp lệ.";
                return RedirectToReferrer();
            }

            foreach (var ngaytaotruochieuchinh in ngaytaotruochieuchinhs)
            {
                if (ngaytaotruoc.CheckSerialNumberExists(ngaytaotruochieuchinh.Id_PhanCong))
                {
                    TempData["Message"] = "Chỉ có thể tạo ngày 1 lần";
                    return RedirectToAction("Details", "BienBanHieuChuan", new { Id }); 
                }
                var oldlist = new NgayTaoTechnicialBuldTruocHieuChinh
                {
                    Id_PhanCong = ngaytaotruochieuchinh.Id_PhanCong,
                    Id_BulbSpectrum = ngaytaotruochieuchinh.Id_BulbSpectrum,
                    Id_LampSystems = ngaytaotruochieuchinh.Id_LampSystems,
                    NgayTao= ngaytaotruochieuchinh.NgayTao,
                    Technicial = ngaytaotruochieuchinh.Technicial, 
                    NguoiTao = Session["Name"].ToString(),
                    Status = 1
                };
                ngaytaotruoc.Insert(oldlist);
            }
            ViewBag.BulbSpectrum = new SelectList(bulbspectrumDAO.getList("Index"), "Id", "Bulb_Spectrum");
            ViewBag.LampSystems = new SelectList(lampsystemsDAO.getList("Index"), "Id", "Lamp_Systems");
            TempData["Message"] = "Tạo thành công"; 
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        } 
        public ActionResult XoaNgay(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = ngaytaotruoc.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sửa ";
                return RedirectToReferrer();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaNgay(List<NgayTaoTechnicialBuldTruocHieuChinh> ids)
        {
            var id = ids.FirstOrDefault()?.Id_PhanCong;
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có gì để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.Find(listNumber.Id);
                dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.Remove(oldList);
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
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
                var listNumbers = giatritruochieuchinhDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<GiaTriTruocHieuChinh> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có bản ghi nào để xóa.";
                return RedirectToAction("Index");
            }

            var id = ids.FirstOrDefault()?.Id_PhanCong;

            foreach (var chuanSuDung in ids)
            {
                var oldlist = dbContext.GiaTriTruocHieuChinhs.Find(chuanSuDung.Id);
                if (oldlist != null)
                {
                    dbContext.GiaTriTruocHieuChinhs.Remove(oldlist);
                }
            }
            dbContext.SaveChanges();

            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        } 
    }
}