using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_KhachHang_Listnumber
    {
        public int Id_Listnumber {  get; set; }
        public int Id_KhachHang {  get; set; }
        public string Tenthietbi { get; set; }
        public string Img { get; set; }
        public string Hang { get; set; }
        public string Model { get; set; }
        public string PhamViDo { get; set; }
        public string DoPhanGiai { get; set; }
        public string Serial { get; set; } 
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string GhiChu { get; set; }
        public string LienHe { get; set; }
        public string NhomNganh { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayHCLai { get; set; }
        public int? Status { get; set; }

    }
}
