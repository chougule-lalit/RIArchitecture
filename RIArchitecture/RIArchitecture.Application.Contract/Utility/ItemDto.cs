using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public class ItemDto : FullAuditedEntityDto<int>
    {
        public string Name { get; set; }
    }
}
