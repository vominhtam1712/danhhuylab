using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_PhanCong_NhanVien
    {
        public int Id { get; set; }
        public string MaPC { get; set; }
        public int? Id_pychc { get; set; }
        public int? Id_NV { get; set; }
        public string SoNhanDang { get; set; }
        public string Name { get; set; }
        public string TenThietBi { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string TenCty_XuatHD { get; set; }
        public string TenCty { get; set; }
        public string MaPhieu { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public string GhiChu { get; set; }
        public int? Status { get; set; }
    }
}
