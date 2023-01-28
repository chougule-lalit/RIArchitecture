using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Application.Contracts.Utility.Otp;
using RIArchitecture.Core.Entities;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Utility
{
    public class OtpDetailAppService : RIArchitectureAppService, IOtpDetailAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OtpDetailAppService> _logger;

        public OtpDetailAppService(IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<OtpDetailAppService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OtpDetailDto> CreateAsync(string phoneNumber)
        {
            //Insert new OTP
            var otpData = new OtpDetail
            {
                Otp = GenerateOtp(),
                CreationTime = DateTime.UtcNow,
                PhoneNumber = phoneNumber
            };

            var createdOtp = _unitOfWork.Repository<OtpDetail>().Insert(otpData);
            await _unitOfWork.Complete();
            var returnData = ObjectMapper.Map<OtpDetail, OtpDetailDto>(createdOtp);

            var sendOtpResult = await SendOtpAsync(returnData);

            if (sendOtpResult != "Sent.")
                _logger.LogError($"OTP not sent to {phoneNumber} API Output : {sendOtpResult}");

            return returnData;
        }

        public async Task<OtpVerificationOutputDto> VerifyOtp(OtpVerificationDto input)
        {
            var output = new OtpVerificationOutputDto
            {
                IsVerified = false,
                Message = "Otp Not Verified"

            };

            var otpDetail = await _unitOfWork.Repository<OtpDetail>().GetAll().Where(x => x.PhoneNumber == input.PhoneNumber
                                        && x.Otp == input.Otp)
                                    .OrderByDescending(x => x.CreationTime)
                                    .FirstOrDefaultAsync();
            if (otpDetail == null)
            {
                return output;
            }
            else
            {
                var seconds = (DateTime.UtcNow - otpDetail.CreationTime).TotalSeconds;
                if (seconds <= 60)
                {
                    output.IsVerified = true;
                    output.Message = "Otp Verified";
                    return output;
                }
                else
                {
                    output.Message = "Otp Expired";
                    return output;
                }
            }
        }

        private async Task<string> SendOtpAsync(OtpDetailDto input)
        {
            string finalResponse = string.Empty;
            var client = new HttpClient();
            var smsText = $"Login OTP for Sapney Mason Application is {input.Otp}";
            //Payload
            var payload = new Dictionary<string, string>();
            payload.Add("username", _configuration["SMS:UserName"]);
            payload.Add("password", _configuration["SMS:Password"]);
            payload.Add("to", input.PhoneNumber);
            payload.Add("from", _configuration["SMS:From"]);
            payload.Add("text", smsText);

            var apiURl = _configuration["SMS:SendSMSURL"];
            Uri u = new Uri(apiURl);

            var accessToken = await GenerateTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            var response = client.PostAsync(u, new FormUrlEncodedContent(payload)).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                finalResponse = jsonString.Result.ToString();
            }
            else
            {
                _logger.LogError($"SMS API failed to send SMS with code : {response.StatusCode} and message : {response.ReasonPhrase}");
            }

            return finalResponse;
        }

        private static int GenerateOtp()
        {
            Random generator = new Random();
            String otp = generator.Next(0, 1000000).ToString("D6");
            return Convert.ToInt32(otp);
        }

        private async Task<AccessTokenResponseDto> GenerateTokenAsync()
        {

            var tokenResponse = new AccessTokenResponseDto();
            var client = new HttpClient();

            //Basic Authentication           
            var authString = $"{_configuration["SMS:UserName"]}:{_configuration["SMS:Password"]}";
            var base64EncodedAuthString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authString));

            //Http header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthString);

            //Payload
            var payload = new Dictionary<string, string>();
            payload.Add("action", "generate");

            var apiURl = $@"{_configuration["SMS:TokenURL"]}?action=generate";
            Uri u = new Uri(apiURl);
            var response = client.PostAsync(u, null).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponseDto>(jsonString.Result.ToString());
            }
            else
            {
                _logger.LogError($"Token generation for SMS API failed with code : {response.StatusCode} and message : {response.ReasonPhrase}");
            }

            return tokenResponse;
        }
    }
}
