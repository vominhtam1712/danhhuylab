using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListClass.Dao;
using ListClass.Model;

namespace ListSerialNumber.Controllers
{
    public class GiayCNSanPhamController : AccountLoginController
    {
        private readonly GiayCNSanPhamDAO GiayCNSanPhamDAO = new GiayCNSanPhamDAO();
        private readonly ListNumberDAO lnbDAO = new ListNumberDAO();
        private readonly ListDBContext db = new ListDBContext();
        public ActionResult Index(int id)
        {   
            if (HasAccessAd() || HasAccessUser("GCNHangHoa"))
            { 
                var data = GiayCNSanPhamDAO.getList(id);  
                ViewBag.id = id;
                return View(data);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();  
            }
        }
        public ActionResult Upload(int id)
        { 
            if (HasAccessAd() || HasAccessUser("ThemGCNHangHoa"))
            {
                var listNumbers = lnbDAO.GetById(id);
                ViewBag.id = id;
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
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int id)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file.ContentLength > 2039296000)
                    {
                        TempData["Message"] = "File tải lên vượt quá kích thước cho phép (1,9 GB).";
                        return RedirectToReferrer();
                    }
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Public/GiayCNSanPham/"), fileName);

                        if (GiayCNSanPhamDAO.CheckTonTai(fileName))
                        {
                            TempData["Message"] = "File đã tồn tại không thể tải lên!";
                            return RedirectToReferrer();
                        }
                        file.SaveAs(filePath);
                        var giaycnsp = new GiayCNSanPham
                        {
                            FileName = fileName,
                            FilePath = "/Public/GiayCNSanPham/" + fileName,
                            Id_ListNumber = id
                        };
                        TempData["Message"] = "Tải file lên thành công!";
                        db.GiayCNSanPhams.Add(giaycnsp);
                    }
                }
                db.SaveChanges();
                return RedirectToReferrer();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }

            if (HasAccessAd() || HasAccessUser("XoaGCNHangHoa"))
            {
                var fileRecord = db.GiayCNSanPhams.Find(ids);
                if (fileRecord != null)
                {
                    var filePath = Server.MapPath(fileRecord.FilePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    db.GiayCNSanPhams.Remove(fileRecord);
                    db.SaveChanges();

                    TempData["Message"] = "File đã được xóa thành công!";
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy tệp tin.";
                }
                return RedirectToReferrer();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToReferrer();
            }
        }
    }
}
