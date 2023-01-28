using System;
using System.Collections.Generic;

namespace RIArchitecture.Application.Contracts.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }

        public Guid UserId { get; set; }

        public List<Guid> RoleIds { get; set; }
    }
}