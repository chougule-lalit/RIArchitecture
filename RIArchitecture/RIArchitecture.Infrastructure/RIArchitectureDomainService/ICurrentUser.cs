using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Infrastructure.RIArchitectureDomainService
{
    public interface ICurrentUser
    {
        Guid Id { get; }
    }
}
