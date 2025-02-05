using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("CompanyRoles")]
    public class CompanyRoles
    {
        [Key]
        public int Id { get; set; } 
        public int? Id_Company { get; set; } 
        public int? Id_Role { get; set; }
        public string MaQuyen { get; set; }
        public virtual Role Role { get; set; }
        public virtual Company Company { get; set; }
    }
}
