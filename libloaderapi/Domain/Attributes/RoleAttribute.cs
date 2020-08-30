using System;
using Microsoft.AspNetCore.Authorization;

namespace libloaderapi.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RoleAttribute : AuthorizeAttribute
    {
        public RoleAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
