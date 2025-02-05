using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhieuTra_forms")]
    public class PhieuTra_form
    {
        [Key]
        public int Id { get; set; }
        public string MaPhieuTra { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Phieutrathietbi> Phieutrathietbis { get; set; }

    }
}
