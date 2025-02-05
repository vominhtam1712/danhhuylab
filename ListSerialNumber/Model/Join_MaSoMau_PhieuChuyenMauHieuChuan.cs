using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_MaSoMau_PhieuChuyenMauHieuChuan
    {
        public int Id_MSM { get; set; }
        public string MaSoMau { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTaoPhieu_MSM { get; set; }
        public int Id_PCM { get; set; }
        public string SoNhanDang { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhan { get; set; }
        public string NguoiNhan { get; set; }
        public string ThongSoHieuChuan { get; set; }
        public string NguoiDuocPhanCong { get; set; }
        public string PhuongPhamHieuChuan { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayTraBaoCao { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTaoPhieu_PCM { get; set; }
        public string NguoiTao { get; set; }
        public int? Status { get; set; }
    }
}
