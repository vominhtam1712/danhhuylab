using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_Giaychungnhan
    {
        //[Table("Giaychungnhans")]
        public int Id_GCN { get; set; }
        public string SoNhanDang { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime NgayHieuChuan { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime NgayHieuChuanLai { get; set; }
        public string Type { get; set; }

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
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime Cal_Date { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime Due_Day { get; set; }
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
        public string PhammVido_listnumber { get; set; }
        public string DoPhanGiai_listnumber { get; set; }
        public int? Id_ListNumber { get; set; }
        public string SerialNumber { get; set; }
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

        //[Table("ChuanSuDungs")]
        public int Id_ChuanSuDung { get; set; } 
        public string Model_ChuanSuDung { get; set; }
        public string Description_ChuanSuDung { get; set; }
        public int Serial_ChuanSuDung { get; set; }
        public int Cetificate_ChuanSuDung { get; set; } 
        public DateTime Cal_Date_ChuanSuDung { get; set; } 
        public DateTime Cal_Due_ChuanSuDung { get; set; }

        //[Table("NgayTaoTechnicialBuldTruocHieuChinhs")]
    
        public int Id_NgayTaoTechnicialBuldTruocHieuChinh { get; set; } 
        public string Technicial_NgayTaoTechnicialBuldTruocHieuChinh { get; set; }
        public string Bulb_Spectrum_NgayTaoTechnicialBuldTruocHieuChinh { get; set; } 
        public string LampSystem_NgayTaoTechnicialBuldTruocHieuChinh { get; set; } 
        public DateTime NgayTao_NgayTaoTechnicialBuldTruocHieuChinh { get; set; }

        // [Table("GiaTriTruocHieuChinhs")]
        public int Id_GiaTriTruocHieuChinh { get; set; } 
        public string BuocSong_GiaTriTruocHieuChinh { get; set; }
        public string Measurand_1_GiaTriTruocHieuChinh { get; set; }
        public float Reference_1_GiaTriTruocHieuChinh { get; set; }
        public float Instrument_1_GiaTriTruocHieuChinh { get; set; }
        public float Deviation_1_GiaTriTruocHieuChinh { get; set; }
        public string Measurand_2_GiaTriTruocHieuChinh { get; set; }
        public float Reference_2_GiaTriTruocHieuChinh { get; set; }
        public float Instrument_2_GiaTriTruocHieuChinh { get; set; }
        public float Deviation_2_GiaTriTruocHieuChinh { get; set; }

        // [Table("NgayTaoTechnicialBuldKetQuaHieuChinh")]

        public int Id_NgayTaoTechnicialBuldKetQuaHieuChinh { get; set; }
        public string Technicial_NgayTaoTechnicialBuldKetQuaHieuChinh { get; set; }
        public string Bulb_Spectrum_NgayTaoTechnicialBuldKetQuaHieuChinh { get; set; }
        public string LampSystem_NgayTaoTechnicialBuldKetQuaHieuChinh { get; set; }
        public DateTime NgayTao_NgayTaoTechnicialBuldKetQuaHieuChinh { get; set; }

        //[Table("KetQuaHieuChinhs")]
        public int Id_KetQuaHieuChinh { get; set; }
        public string BuocSong_KetQuaHieuChinh { get; set; }
        public string Measurand_1_KetQuaHieuChinh { get; set; }
        public float Reference_1_KetQuaHieuChinh { get; set; }
        public float Instrument_1_KetQuaHieuChinh { get; set; }
        public float Deviation_1_KetQuaHieuChinh { get; set; }
        public string Measurand_2_KetQuaHieuChinh { get; set; }
        public float Reference_2_KetQuaHieuChinh { get; set; }
        public float Instrument_2_KetQuaHieuChinh { get; set; }
        public float Deviation_2_KetQuaHieuChinh { get; set; }


    }
}
