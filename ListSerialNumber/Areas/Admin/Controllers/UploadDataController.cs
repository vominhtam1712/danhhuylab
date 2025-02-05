//using ListClass.Model;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc; 
//namespace ListSerialNumber.Areas.Admin.Controllers
//{
//    public class UploadDataController : LoginController
//    {

//        private readonly ListDBContext _context;

//        public UploadDataController(ListDBContext context)
//        {
//            _context = context;
//        }
//        public UploadDataController() : this(new ListDBContext())
//        {
//        }
//        public ActionResult Index()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Upload(HttpPostedFileBase file)
//        {
//            if (file != null && file.ContentLength > 0)
//            {
//                using (var reader = new StreamReader(file.InputStream, Encoding.UTF8))
//                {
//                    var sql = reader.ReadToEnd().Trim();

//                    if (string.IsNullOrWhiteSpace(sql))
//                    {
//                        ViewBag.Message = "The uploaded file is empty or contains only white spaces.";
//                        return View("Index");
//                    }

//                    try
//                    {
//                        ExecuteSql(sql);
//                        TempData["Message"] = "Upload successful!";
//                    }
//                    catch (Exception ex)
//                    {
//                        TempData["Message"] = $"Error processing the file: {ex.Message}";
//                    }
//                }
//            }
//            else
//            {
//                TempData["Message"] = "No file selected.";
//            }

//            return RedirectToAction("Index", "DownloadData");
//        }
//        private void ExecuteSql(string sql)
//        {
//            var commands = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

//            using (var transaction = _context.Database.BeginTransaction())
//            {
//                try
//                {
//                    foreach (var command in commands)
//                    { 
                         
//                        _context.Database.ExecuteSqlCommand(command);
//                    }
//                    transaction.Commit();
//                }
//                catch (Exception ex)
//                {
//                    transaction.Rollback();
                     
//                    TempData["Message"] = $"An error occurred while processing SQL commands: {ex.Message} \n{ex.StackTrace}";
//                    throw;  
//                }
//            }
//        } 

//    }
//}