using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("Giaychungnhans")]
    public class Giaychungnhan
    {
        public int Id { get; set; }
        public int Id_BienBanHieuChuan { get; set; }
        public string MaGCN {  get; set; }
        public string Type {  get; set; }
        public string NguoiTao {  get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime NgayTao {  get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime NgayHieuChuan { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime NgayHieuChuanLai { get; set; }
        public int status { get; set; }
        public virtual BienBanHieuChuan BienBanHieuChuan { get; set; }
        public virtual ICollection<Phieutrathietbi> Phieutrathietbis { get; set; }
        public virtual ICollection<BaoCaoSuaChua> BaoCaoSuaChuas { get; set; }
    }
}
