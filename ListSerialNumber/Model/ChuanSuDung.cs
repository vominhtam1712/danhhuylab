using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("ChuanSuDungs")]
    public class ChuanSuDung
    {
        [Key]
        public int Id { get; set; }
        public int Id_PhanCong { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int Serial { get; set; }
        public int Cetificate { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Cal_Date { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Cal_Due { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; } 
        public string NguoiTao { get; set; }
        public int Status { get; set; }
        public virtual PhanCong PhanCong { get; set; }
    }
}
