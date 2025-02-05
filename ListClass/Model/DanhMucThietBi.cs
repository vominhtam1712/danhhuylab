using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("DanhMucThietBis")]
    public class DanhMucThietBi
    {
        [Key]
        public int Id { get; set; }
        public string TenThietBi {  get; set; }
        public string Serial {  get; set; }
        public string MaThietBi { get; set; }
        public string Img { get; set; }
        public string HangSX {  get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayDuaVaoSuDung {  get; set; }
        public string NguoiTao {  get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao {  get; set; }
        public int? Status {  get; set; }
        public virtual ICollection<KeHoachHieuChuan> KeHoachHieuChuans { get; set; }
        public virtual ICollection<BaoCaoDanhGia> BaoCaoDanhGias { get; set; }
        public virtual ICollection<NhatKyBaoTriVaSuChua> NhatKyBaoTriVaSuChuas { get; set; }

    }
}
