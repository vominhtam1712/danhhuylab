using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; } 
        public string MaQuyen { get; set; } 
        public string NameRoles { get; set; } 
        public int? Nhom { get; set; }
        public int Status { get; set; }
        public virtual ICollection<CompanyRoles> CompanyRoles { get; set; }
    }
}
