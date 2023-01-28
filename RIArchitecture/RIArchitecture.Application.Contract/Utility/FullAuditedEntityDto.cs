using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public abstract class FullAuditedEntityDto<TKey> : EntityDto<TKey>
    {
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public string Source { get; set; }
        public Guid? DeletorId { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
