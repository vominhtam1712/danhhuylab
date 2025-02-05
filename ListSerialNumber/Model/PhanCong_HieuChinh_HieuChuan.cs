using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class PhanCong_HieuChinh_HieuChuan
    {
        public PhanCong PhanCong { get; set; }   
        public List<ChuanSuDung> ChuanSuDungs { get; set; }
        public List<GiaTriTruocHieuChinh> GiaTriTruocHieuChinhs { get; set; }  
        public List<KetQuaHieuChinh> KetQuaHieuChinhs { get; set; }   
        public List<BienBanHieuChuan> BienBanHieuChuans { get; set; }
        public List<NgayTaoTechnicialBuldTruocHieuChinh> NgayTaoTechnicialBuldTruocHieuChinhs { get; set; }
        public List<NgayTaoTechnicialBuldKetQuaHieuChinh> NgayTaoTechnicialBuldKetQuaHieuChinhs { get; set; } 
        public List<Phieuyeucauhieuchuan> Phieuyeucauhieuchuans { get; set; } 
        public List<PhieuNhanThietBi> PhieuNhanThietBis { get; set; } 
        public List<ListNumber> ListNumbers { get; set; } 
        public List<KhachHang> KhachHangs { get; set; }  
        public List<STD_DUT> STD_DUTs { get; set; }  
        public List<PhanCongGhiChu> PhanCongGhiChus { get; set; }  
        public List<BanTinhDKDBD> BanTinhDKDBDs { get; set; }  
    }
}
