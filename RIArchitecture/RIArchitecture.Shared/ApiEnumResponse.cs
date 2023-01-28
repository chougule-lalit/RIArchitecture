using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Shared
{
    public enum ApiEnumResponse
    {
        [Description("Data Found")]
        DataFound = 0,

        [Description("Data Not Found")]
        DataNotFound = 1
    }
}
