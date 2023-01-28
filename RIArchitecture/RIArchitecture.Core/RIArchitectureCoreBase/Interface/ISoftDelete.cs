using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Core.RIArchitectureCoreBase.Interface
{
    public interface ISoftDelete
    {
        Guid? DeletorId { get; set; }
        DateTime? DeletedDateTime { get; set; }
        bool IsDeleted { get; set; }
    }
}
