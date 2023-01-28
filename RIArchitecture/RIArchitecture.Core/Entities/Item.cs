using RIArchitecture.Core.RIArchitectureCoreBase;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using System;

namespace RIArchitecture.Core.Entities
{
    public class Item : Entity<int>, IFullAuditedEntity, ISoftDelete
    {
        public virtual string Name { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual Guid? LastModifierId { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual Guid? CreatorId { get; set; }
        public virtual Guid? DeletorId { get; set; }
        public virtual DateTime? DeletedDateTime { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string Source { get; set; }
    }
}