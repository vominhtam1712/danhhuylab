using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_BaoCaoSuaChua
    {
        public int Id_baocaosuachua { get; set; }
        public int Id_giaychungnhan { get; set; }

        public string MaGCN { get; set; }
        public string MaBaoCao { get; set; }
        public string Tenthietbi { get; set; }
        public string SoNhanDang { get; set; }
        public string Model { get; set; }
        public string TenKH { get; set; }
        public string Hang { get; set; }

        public string SN { get; set; }

        public DateTime cal_date { get; set; }
        public DateTime Dul_date { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]

        public string CacHangSuDung { get; set; }

        public string NguoiTao { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]

        public DateTime NgayTao { get; set; }
        public int Status { get; set; }
    }
}
