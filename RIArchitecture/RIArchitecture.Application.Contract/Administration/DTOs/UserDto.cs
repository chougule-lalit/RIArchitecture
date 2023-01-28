using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Administration.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
