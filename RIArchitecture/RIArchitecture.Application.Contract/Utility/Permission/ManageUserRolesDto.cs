using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Permission
{
    public class ManageUserRolesDto
    {
        public string UserId { get; set; }
        public IList<UserRolesDto> UserRoles { get; set; }
    }
}
