using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Core.RIArchitectureCoreBase
{
    public abstract class FullAuditedEntity<TKey> : Entity<TKey>
    {
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual Guid? LastModifierId { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual Guid? CreatorId { get; set; }

        protected FullAuditedEntity()
        {

        }

        protected FullAuditedEntity(TKey id)
        : base(id)
        {

        }
    }
}
