using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhanCongs")]
    public class PhanCong
    {
        [Key]
        public int Id { get; set; }

        public string MaPC { get; set; }
        public int? Id_pychc { get; set; } 
        public int? Id_NV { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public int? Status { get; set; }
        public virtual Company Company { get; set; }
        public virtual Phieuyeucauhieuchuan Phieuyeucauhieuchuan { get; set; } 
        public virtual ICollection<BienBanHieuChuan> BienBanHieuChuans { get; set; }
        public virtual ICollection<ChuanSuDung> ChuanSuDungs { get; set; }
        public virtual ICollection<KetQuaHieuChinh> KetQuaHieuChinhs { get; set; }
        public virtual ICollection<GiaTriTruocHieuChinh> GiaTriTruocHieuChinhs { get; set; }
        public virtual ICollection<NgayTaoTechnicialBuldTruocHieuChinh> NgayTaoTechnicialBuldTruocHieuChinhs { get; set; }
        public virtual ICollection<NgayTaoTechnicialBuldKetQuaHieuChinh> NgayTaoTechnicialBuldKetQuaHieuChinhs { get; set; }
        public virtual ICollection<PhanCongGhiChu> PhanCongGhiChus { get; set; }
    }
}
