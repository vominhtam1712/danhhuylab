using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_STD_DUT
    {
        public int ID_STD { get; set; }
        public int Id_BienBanHieuChuan { get; set; }
        public float? MucKiemTra_STD { get; set; }
        public float? Lan1_STD { get; set; }
        public float? Lan2_STD { get; set; }
        public float? Lan3_STD { get; set; }
        public float? Lan4_STD { get; set; }
        public float? Lan5_STD { get; set; }
        public float? Max_STD { get; set; }
        public float? Min_STD { get; set; }
        public float? TrungBinh_STD { get; set; }
        public float? DoOnDinh_STD { get; set; }
        public float? U_STD { get; set; }
        public int ID_DUT { get; set; }
        public float? MucKiemTra_DUT { get; set; }
        public float? Lan1_DUT { get; set; }
        public float? Lan2_DUT { get; set; }
        public float? Lan3_DUT { get; set; }
        public float? Lan4_DUT { get; set; }
        public float? Lan5_DUT { get; set; }
        public float? Max_DUT { get; set; }
        public float? Min_DUT { get; set; }
        public float? TrungBinh_DUT { get; set; }
        public float? DoOnDinh_DUT { get; set; }
        public float? U_DUT { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThucHien { get; set; }

    }
}
