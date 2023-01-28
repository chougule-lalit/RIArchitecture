using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public interface IListResult<T>
    {
        //
        // Summary:
        //     List of items.
        IReadOnlyList<T> Items { get; set; }
    }
}
