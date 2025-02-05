using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK
    {
        //PhieuyeucauhieuchuanBackUp
        public int Id_PhieuHieuChuan { get; set; }
        public int Id_phieunhanthietbi { get; set; }
        public int Id_phieutrathietbi { get; set; }
        public string Diachihieuchuan { get; set; }
        public string MaPhieu { get; set; }
        public string Tencognty { get; set; }
        public string Diachicongty { get; set; }
        public string Masothue { get; set; }
        public int? Soluong { get; set; }
        public string Dichvuyeucau { get; set; }
        public string Createby { get; set; }
        public string UpdateBy { get; set; }
        public string Danhhuy { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao_PYC { get; set; }
        public int? Status_PhieuHieuChuan { get; set; }

        //ListNumberBackUp

        public int Id_Listnumber { get; set; }
        public string Tenthietbi { get; set; }
        public string Img { get; set; }
        public string Hang { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ngaytao_SanPham { get; set; }
        public string Nguoitao { get; set; }
        public int? Status_Listnumber { get; set; }

        //PhieuNhanThietBi

        public int Id_PhieuNhanThietBi { get; set; }
        public string SoYeuCau { get; set; }
        public string SoNhanDang { get; set; }
        public string NguoiThucHien { get; set; }
        public string HienTrang { get; set; }
        public string GhiChu { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ngaytao_PhieuNhanTB { get; set; }
        public int? Status { get; set; }

        //BienBanBanGiaoNhanThietBiBackUp

        public int Id_BienBan { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThucHien { get; set; }
        public string KhachHang { get; set; }
        public string DiaChi { get; set; }
        public string LienHe { get; set; }

        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Ngaytao_BienBan { get; set; }

    }
}
