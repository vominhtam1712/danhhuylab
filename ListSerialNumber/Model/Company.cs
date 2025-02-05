using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("Companys")]
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } 
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; }
        public int Status { get; set; }
        public string Img { get; set; }
        public string SoCCCD { get; set; }
        public string QueQuan { get; set; }
        public DateTime? NamSinh { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgayVaoLam { get; set; } 
        public string BacDaoTao { get; set; }
        public string ChuyenNganh { get; set; }
        public string ChucVu { get; set; }
        public virtual ICollection<PhanCong> PhanCongs { get; set; }
        public virtual ICollection<CompanyRoles> CompanyRoles { get; set; }
        public virtual ICollection<Ngaynghi> Ngaynghis { get; set; }

    }
}
