using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Otp
{
    public interface IOtpDetailAppService : IRIArchitectureAppService
    {
        Task<OtpDetailDto> CreateAsync(string phoneNumber);
        Task<OtpVerificationOutputDto> VerifyOtp(OtpVerificationDto input);

    }
}
