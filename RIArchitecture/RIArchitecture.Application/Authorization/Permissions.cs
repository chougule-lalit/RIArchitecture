using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Authorization
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }

        public static List<Type> GetAllPermissionTypes()
        {
            return new List<Type>
                    {
                        typeof(Items),
                        typeof(Users)
                    };
        }

        public static class Items
        {
            public const string View = "Permissions.Items.View";
            public const string Create = "Permissions.Items.Create";
            public const string Edit = "Permissions.Items.Edit";
            public const string Delete = "Permissions.Items.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string PermissionChange = "Permissions.Roles.PermissionChange";
        }
    }
}
