using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("DanhSachPhieuTheoDoiNhietDos")]
    public class DanhSachPhieuTheoDoiNhietDo
    {
        [Key]
        public int ID { get; set; }
        public int ID_PhieuTheoDoiNhietDo { get; set; }
        public int Ngay_theo_doi {  get; set; }
        public float? NhietDo_Sang {  get; set; }
        public float? DoAm_Sang {  get; set; }    
        public float? NhietDo_Chieu { get; set; }
        public float? DoAm_Chieu { get; set; }
        public string KetLuan { get; set; }
        public string NguoiKiemTra { get; set; }
        public DateTime Ngay { get; set; }
        public int status {  get; set; }
        public virtual PhieuTheoDoiNhietDo PhieuTheoDoiNhietDos { get; set; }
    }
}
