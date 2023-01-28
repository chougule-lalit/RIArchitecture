using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Otp
{
    public class OtpVerificationOutputDto
    {
        public bool IsVerified { get; set; }
        public string Message { get; set; }
    }
}
