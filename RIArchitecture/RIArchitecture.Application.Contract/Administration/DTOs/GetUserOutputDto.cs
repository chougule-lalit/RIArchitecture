using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Administration.DTOs
{
    public class GetUserOutputDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<string> RoleList { get; set; }

        public string Roles { get; set; }
    }
}
