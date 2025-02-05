using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD
    {
        //[Table("STDs")]
        public int ID_STD { get; set; }
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
        public float? STD_Cer_STD { get; set; }
        public float? STD_Spec_STD { get; set; }
        //[Table("DUTs")]
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
        public float? DoPhanGiai { get; set; }

        //[Table("BienBanHieuChuans")]
        public int Id_BienBanHieuChuan { get; set; }
        public float? NhietDo { get; set; }
        public float? DoAm { get; set; }
        public string TenHieuChuan { get; set; }
        public int? SN { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThucHien { get; set; }
        //[Table("PhanCongs")]
        public int Id_PhanCong { get; set; }

        //[Table("PhieuYeuCauHieuChuanDois")]
        public int? Id_pychc { get; set; }

        //[Table("Phieunhanthietbis")]
        public int Id_phieunhanthietbi { get; set; }
        //[Table("BienBanBanGiaoNhanThietBis")]
        public string SoYeuCau { get; set; }
        public string KhachHang { get; set; }
        //[Table("listnumbers")]
        public string Tenthietbi { get; set; }
        public string Hang { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public int Id_ListNumber { get; set; }
        //[Table("BanTinhDKDBDs")]
        public int Id_dkdbd { get; set; }
        public float? U_a { get; set; }
        public float? U_d { get; set; }
        public float? U_cer { get; set; }
        public float? U_od_std { get; set; }
        public float? U_od { get; set; }
        public float? U_dd { get; set; }
        public float? U_c { get; set; }
        public float? K { get; set; }
        public float? U_morong { get; set; }
        public float? Saiso { get; set; }
        public float? DoDKDB { get; set; }
        //TB STD
        public int Id_tbstd { get; set; }
        public float? TB_STD { get; set; }

    }
}
