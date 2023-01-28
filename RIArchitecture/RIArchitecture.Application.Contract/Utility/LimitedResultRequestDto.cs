using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        public LimitedResultRequestDto()
        {

        }

        //
        // Summary:
        //     Default value: 10.
        public static int DefaultMaxResultCount { get; set; }
        //
        // Summary:
        //     Maximum possible value of the Volo.Abp.Application.Dtos.LimitedResultRequestDto.MaxResultCount.
        //     Default value: 1,000.
        public static int MaxMaxResultCount { get; set; }
        //
        // Summary:
        //     Maximum result count should be returned. This is generally used to limit result
        //     count on paging.
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; }
    }
}
