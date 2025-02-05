using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhieuTheoDoiNhietDos")]
    public class PhieuTheoDoiNhietDo
    {
        [Key]
        public int Id { get; set; }
        public string MaPhieuTheoDoiNhietDo { get; set; }
        public string MSNhietKe_Model_No {  get; set; }
        public string Phong {  get; set; }
        public DateTime Thang_Nam { get; set; }
        public float? NhietDo_Dau { get; set; }
        public float? NhietDo_Sau { get; set; }
        public float? DoAm {  get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public int status {  get; set; }
        public virtual ICollection<DanhSachPhieuTheoDoiNhietDo> DanhSachPhieuTheoDoiNhietDos { get; set; }
    }
}
