using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Otp
{
    public class OtpVerificationDto
    {
        public string PhoneNumber { get; set; }
        public int Otp { get; set; }
    }
}
