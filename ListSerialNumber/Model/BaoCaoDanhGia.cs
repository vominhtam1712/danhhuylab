using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("BaoCaoDanhGias")]
    public class BaoCaoDanhGia
    {
        [Key]
        public int Id { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public string ThietBi_HoaChatHieuChuan { get; set; }
        public DateTime NgayHieuChuan { get; set; }
        public string KetQuaHC { get; set; }
        public string KetLuan { get; set; }
        public DateTime NgayHieuChuanKeTiep { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public int Status { get; set; }
        public virtual DanhMucThietBi DanhMucThietBi { get; set; }
    }
}
