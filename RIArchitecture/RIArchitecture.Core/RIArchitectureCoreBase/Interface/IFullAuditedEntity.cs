using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Core.RIArchitectureCoreBase.Interface
{
    public interface IFullAuditedEntity
    {
        DateTime? LastModificationTime { get; set; }
        Guid? LastModifierId { get; set; }
        DateTime? CreationTime { get; set; }
        Guid? CreatorId { get; set; }
        string Source { get; set; }

    }
}
