using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class ListResultDto<T> : IListResult<T>
    {
        //
        // Summary:
        //     Creates a new Volo.Abp.Application.Dtos.ListResultDto`1 object.
        public ListResultDto() { }
        //
        // Summary:
        //     Creates a new Volo.Abp.Application.Dtos.ListResultDto`1 object.
        //
        // Parameters:
        //   items:
        //     List of items
        public ListResultDto(IReadOnlyList<T> items) { }

        public IReadOnlyList<T> Items { get; set; }
    }
}
