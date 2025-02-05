using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("MaSoPhieuChuyenMaus")]
    public class MaSoPhieuChuyenMau
    {

        [Key]
        public int Id { get; set; }
        public string MaSoMau { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTaoPhieu { get; set; }
        public string NguoiTao { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<PhieuChuyenMauHieuChuan> PhieuChuyenMauHieuChuans { get; set; }
    }
}
