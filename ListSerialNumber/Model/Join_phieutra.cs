using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_phieutra
    {
        public int Id_Number { get; set; }
        public int Id_BienBan { get; set; }
        public int Id_PhieuNhan { get; set; }
        public int Id_PhieuTra { get; set; }
        public string Tenthietbi { get; set; }
        public string Img { get; set; }
        public string Hang { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; } 
        public DateTime NgaytaoSanPham { get; set; }
        public string Nguoitao { get; set; }
        public string SoYeuCau { get; set; }
        public string SoNhanDang { get; set; }
        public string NguoiThucHien { get; set; }
        public string HienTrang { get; set; }
        public string GhiChu { get; set; }
         
        public DateTime Ngaytao { get; set; } 
        public DateTime NgayThucHien { get; set; }
        public string KhachHang { get; set; }
        public string DiaChi { get; set; }
        public string LienHe { get; set; }
        public int? Status { get; set; }
    }
}
