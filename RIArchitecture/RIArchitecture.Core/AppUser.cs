using Microsoft.AspNetCore.Identity;
using RIArchitecture.Core.RIArchitectureCoreBase;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Core
{
    public class AppUser : IdentityUser<Guid>, ISoftDelete, IFullAuditedEntity
    {
        public virtual string Name { get; set; }
        public virtual Guid? DeletorId { get; set; }
        public virtual DateTime? DeletedDateTime { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual Guid? LastModifierId { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual Guid? CreatorId { get; set; }
        public virtual string Source { get; set; }
    }
}
