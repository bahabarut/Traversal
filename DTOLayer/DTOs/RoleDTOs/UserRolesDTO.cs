using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.RoleDTOs
{
    public class UserRolesDTO
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public bool roleExist { get; set; }
    }
}
