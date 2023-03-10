using System.ComponentModel.DataAnnotations;

namespace RIArchitecture.Application.Contracts.Utility.Requests
{
    public class TokenRequestDto
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}