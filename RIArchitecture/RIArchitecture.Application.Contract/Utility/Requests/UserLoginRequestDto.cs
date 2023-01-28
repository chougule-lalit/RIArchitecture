using System.ComponentModel.DataAnnotations;

namespace RIArchitecture.Application.Contracts.Utility.Requests
{
    public class UserLoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}