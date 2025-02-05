using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("KeHoachHieuChuans")]
    public class KeHoachHieuChuan
    {
        [Key]
        public int Id { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public int Id_KeHoachHieuChuan_Form { get; set; }
        public string Jan_Plan {  get; set; }
        public string Jan_Act{ get; set; }

        public string Feb_Plan { get; set; }
        public string Feb_Act { get; set; }

        public string March_Plan { get; set; }
        public string March_Act { get; set; }

        public string April_Plan { get; set; }
        public string April_Act { get; set; }

        public string May_Plan { get; set; }
        public string May_Act { get; set; }

        public string June_Plan { get; set; }
        public string June_Act { get; set; }

        public string July_Plan { get; set; }
        public string July_Act { get; set; }

        public string Aug_Plan { get; set; }
        public string Aug_Act { get; set; }

        public string Sep_Plan { get; set; }
        public string Sep_Act { get; set; }

        public string Oct_Plan { get; set; }
        public string Oct_Act { get; set; }

        public string Nov_Plan { get; set; }
        public string Nov_Act { get; set; }

        public string Dec_Plan { get; set; }
        public string Dec_Act { get; set; }

        public string NguoiTao { get; set; }
        [Display(Name = "Date edit")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayTao { get; set; }
        public int Status { get; set; }
        public virtual DanhMucThietBi DanhMucThietBi { get; set; }
        public virtual KeHoachHieuChuan_Form KeHoachHieuChuan_Form { get; set; }

    }
}
