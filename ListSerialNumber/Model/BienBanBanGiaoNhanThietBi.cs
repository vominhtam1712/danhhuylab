using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("BienBanBanGiaoNhanThietBis")]
    public class BienBanBanGiaoNhanThietBi
    {
        [Key]
        public int Id { get; set; }
        public string SoYeuCau { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThucHien { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ngaytao { get; set; }
        public string Nguoitao { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<PhieuNhanThietBi> PhieuNhanThietBis { get; set; }
    }
}
