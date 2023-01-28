using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Contracts.Utility.Permission
{
    [Serializable]
    public class RoleClaimsDto
    {
        /// <summary>
        /// Permissions
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Permissions.Items.Delete
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Create, View, Edit, Delete
        /// </summary>
        public string DisplayName { get; set; } //View
        public bool IsGranted { get; set; } //true/false
        public int Level { get; set; } //0,1,2,3...

        /// <summary>
        /// Items, Users
        /// </summary>
        public string ParentName { get; set; } //Items,Users

    }
}
