using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class AccessTokenResponseDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }
    }
}
