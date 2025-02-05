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
    public class QuyTrinhISOController : AccountLoginController
    {
        private readonly ListDBContext db = new ListDBContext();
        private readonly QuyTrinhISODAO quytrinhISO = new QuyTrinhISODAO();

        // GET: ThietBi
        public ActionResult Index()
        {
            if (HasAccessAd() || HasAccessUser("QuyTrinhISO")) 
            {
                var model = new QuyTrinhISOInfo
                {
                    QuyTrinhISOs = db.QuyTrinhISOs.ToList(),   
                    QuyTrinhISO02 = db.QuyTrinhISO02s.ToList()   
                };
                return View(model);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }    
            
        }
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemQuyTrinhISO"))
            { 
                return View("Create");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuyTrinhISO QuyTrinhISO)
        {
            if (!ModelState.IsValid)
            {
                return View(QuyTrinhISO);
            }
            QuyTrinhISO.NgayTao = DateTime.Now;
            QuyTrinhISO.Status = 1;
            quytrinhISO.Insert(QuyTrinhISO);
            TempData["Message"] = "Thêm thành công"; 
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd())
            {
                var listNumbers = quytrinhISO.GetById(id);
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
        public ActionResult Edit(QuyTrinhISO QuyTrinhISO)
        {
            QuyTrinhISO.NgayTao = DateTime.Now;
            QuyTrinhISO.Status = 1;
            quytrinhISO.Update(QuyTrinhISO);
            TempData["Message"] = "Cập nhật thành công";
            return RedirectToAction("Index");

        }
        public ActionResult EditFile(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd())
            {
                var listNumbers = quytrinhISO.GetById2(id);
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
        public ActionResult EditFile(IEnumerable<HttpPostedFileBase> files, int id, int id_quytrinh)
        { 
            var fileRecord = db.QuyTrinhISO02s.Find(id);
            if (fileRecord != null)
            { 
                var filePath = Server.MapPath(fileRecord.FilePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                db.QuyTrinhISO02s.Remove(fileRecord);
                db.SaveChanges();
            }
             
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file.ContentLength > 2039296000)
                    {
                        TempData["Message"] = "File tải lên vượt quá kích thước cho phép (1,9 GB).";
                        return RedirectToAction("Index");
                    }
                    if (file != null && file.ContentLength > 0)
                    { 
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Public/ISO17025/"), fileName);
                         
                        file.SaveAs(filePath);
                         
                        var thietBi2 = new QuyTrinhISO02
                        {
                            FileName = fileName,
                            FilePath = "/Public/ISO17025/" + fileName,
                            id_quytrinh = id_quytrinh,
                        };

                        db.QuyTrinhISO02s.Add(thietBi2);
                    }
                }

                TempData["Message"] = "Cập nhật file thành công!";
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            return View();
        }
        public ActionResult Upload(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd())
            {
                var listNumbers = quytrinhISO.GetById(id);
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
                        return RedirectToAction("Index");
                    }   
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Public/ISO17025/"), fileName);

                        if (quytrinhISO.CheckTonTai(fileName))
                        {
                            TempData["Message"] = "File đã tồn tại không thể tải lên!";
                            return RedirectToAction("Index");
                        }
                        file.SaveAs(filePath); 
                        var thietBi2 = new QuyTrinhISO02
                        {
                            FileName = fileName,
                            FilePath = "/Public/ISO17025/" + fileName,
                            id_quytrinh = id
                        };
                        TempData["Message"] = "Tải file lên thành công!";
                        db.QuyTrinhISO02s.Add(thietBi2);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd())
            {
                if(quytrinhISO.CheckDelete(id))
                {
                    TempData["Message"] = "Hãy xóa hết dữ liệu trước khi xóa quy trình này!";
                    return RedirectToAction("Index");
                }    
                var thietBi = db.QuyTrinhISOs.Find(id);
                db.QuyTrinhISOs.Remove(thietBi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToReferrer();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToAction("Index");
            }
            if (HasAccessAd())
            {
                var fileRecord = db.QuyTrinhISO02s.Find(id); 
                if (fileRecord != null)
                { 
                    var filePath = Server.MapPath(fileRecord.FilePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                     
                    db.QuyTrinhISO02s.Remove(fileRecord);
                    db.SaveChanges();  

                    TempData["Message"] = "File đã được xóa thành công!";
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy tệp tin.";
                }

                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToAction("Index");
            }
        }

    }
}
