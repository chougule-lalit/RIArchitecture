using RIArchitecture.Core.RIArchitectureCoreBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Core.Entities
{
    public class OtpDetail : Entity<Guid>
    {
        public virtual string PhoneNumber { get; set; }
        public virtual int Otp { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }
}
