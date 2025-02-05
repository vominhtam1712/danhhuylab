using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("BienBanHieuChuans")]
    public class BienBanHieuChuan
    {
        [Key]
        public int Id { get; set; }
         
        public int Id_PhanCong { get; set; }

        public string MaBienBan { get; set; }
        public float? NhietDo { get; set; }
        public float? DoAm { get; set; }
        public string TenHieuChuan { get; set; }
        public int? SN { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThucHien { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime NgayTao { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime Cal_Date { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime Due_Day { get; set; }
        public string NguoiTao { get; set; }
        public int? Status { get; set; }
        public virtual PhanCong PhanCong { get; set; }
        public virtual ICollection<STD_DUT> STD_DUTs { get; set; }
        public virtual ICollection<Giaychungnhan> Giaychungnhans { get; set; }
        

    }
}
