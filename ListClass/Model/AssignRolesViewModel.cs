using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class AssignRolesViewModel
    {
        public int CompanyId { get; set; }
        public List<RoleCheckboxViewModel> Roles { get; set; }
    }
}
