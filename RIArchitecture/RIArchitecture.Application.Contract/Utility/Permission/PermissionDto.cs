using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Permission
{
    public class PermissionDto
    {
        public string RoleId { get; set; }
        public IList<RoleClaimsDto> RoleClaims { get; set; }
    }
}
