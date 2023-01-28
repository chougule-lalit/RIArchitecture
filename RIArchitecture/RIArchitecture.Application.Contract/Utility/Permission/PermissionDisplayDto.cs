using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Permission
{
    public class PermissionDisplayDto
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public string DisplayName { get; set; }
    }

    public class PermissionGrantDto
    {
        public string Name { get; set; }
        public bool IsGranted { get; set; }
    }
}
