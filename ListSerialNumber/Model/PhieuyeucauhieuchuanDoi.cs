using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhieuyeucauhieuchuanDois")]
    public class PhieuyeucauhieuchuanDoi
    {
        [Key]
        public int Id { get; set; }
        public string MaPhieu { get; set; }
        public int Id_phieuyeucauhieuchuan { get; set; }
        public int Id_phieunhanthietbi { get; set; }
        public string Diachihieuchuan { get; set; }
        public string Tencognty { get; set; }
        public string Diachicongty { get; set; }
        public string Masothue { get; set; }
        public int? Soluong { get; set; }
        public string Dichvuyeucau { get; set; }
        public string Createby { get; set; }
        public string UpdateBy { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string Danhhuy { get; set; }
        public int? Status { get; set; }
        public virtual Phieuyeucauhieuchuan Phieuyeucauhieuchuan { get; set; }

    }
}
