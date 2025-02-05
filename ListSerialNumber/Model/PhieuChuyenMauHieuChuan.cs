using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhieuChuyenMauHieuChuans")]
    public class PhieuChuyenMauHieuChuan
    {

        [Key]
        public int Id { get; set; } 
        public int Id_PhieuNhanThietBi { get; set; }
        public string MaSoMau { get; set; } 
        public DateTime NgayNhan { get; set; }
        [Required]
        [StringLength(255)]
        public string NguoiNhan { get; set; }
        public string ThongSoHieuChuan { get; set; }
        public string NguoiDuocPhanCong { get; set; }
        public string PhuongPhamHieuChuan { get; set; } 
        public DateTime? NgayTraBaoCao { get; set; } 
        public DateTime NgayTaoPhieu { get; set; }
        public string NguoiTao { get; set; }
        public int? Status { get; set; }
        public virtual PhieuNhanThietBi PhieuNhanThietBi { get; set; } 
    }
}
