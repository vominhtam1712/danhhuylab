using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("GiaTriTruocHieuChinhs")]
    public class GiaTriTruocHieuChinh
    {
        [Key]
        public int Id { get; set; }
        public int Id_PhanCong { get; set; }
        public string BuocSong { get; set; }
        public string Measurand_1 { get; set; }
        public float Reference_1 { get; set; }
        public float Instrument_1 { get; set; }
        public float Deviation_1 { get; set; }
        public string Measurand_2 { get; set; }
        public float Reference_2 { get; set; }
        public float Instrument_2 { get; set; }
        public float Deviation_2 { get; set; } 
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public int Status { get; set; }
        public virtual PhanCong PhanCong { get; set; }

    }
}
