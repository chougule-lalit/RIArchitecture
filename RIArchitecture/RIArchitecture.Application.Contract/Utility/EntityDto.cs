using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility
{
    public abstract class EntityDto<TKey> : IEntityDto<TKey>
    {
        public virtual TKey Id { get; protected set; }

        protected EntityDto()
        {

        }

        protected EntityDto(TKey id)
        {
            Id = id;
        }
    }
}
