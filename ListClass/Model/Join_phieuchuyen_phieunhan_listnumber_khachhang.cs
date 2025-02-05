using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_phieuchuyen_phieunhan_listnumber_khachhang
    {
        public int Id_phieuchuyen { get; set; }
        public string Ma_phieuchuyen { get; set; }
        public string SoNhanDang { get; set; } 
        public string TenKH { get; set; }
        public string SoSN { get; set; }
        public string Img { get; set; }
        public string NguoiNhan { get; set; }
        public string NguoiDuocPC { get; set; }
        public string PhuocPhapHC { get; set; }
        public string ThongSoHC { get; set; }
        public string NguoiTao { get; set; } 
        public DateTime? NgayTra { get; set; }
        public DateTime NgayNhan { get; set; }
    }
}
