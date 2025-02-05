using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_Phieutrathietbi_phieunhanthietbi_bienban
    {
        public int Id_PhieuTra {  get; set; }   
        public int Id_PhieuTra_Form {  get; set; }   
        public int Id_GiayCN { get; set; }
        public int Id_bienbannhan{ get; set; }
        public string MaGCN { get; set; }
        public string SoYeuCau {  get; set; }
        public string MaPhieuTra {  get; set; }
        public string KhachHang {  get; set; }
        public string DiaChi {  get; set; }
        public string LienHe {  get; set; }
        public string TenThietBi {  get; set; } 
        public string Img {  get; set; } 
        public string Hang {  get; set; }
        public string Model {  get; set; }
        public string SerialNumber {  get; set; }
        public string SoNhanDang { get; set; }
        public string PhuongThucGiaoTra { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThucHien { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string TrangThaiThietBi { get; set; }
        public string NguoiTao { get; set; }
        public string GhiChu {  get; set; }
        public int Status {  get; set; }

    }
}
