using System;

namespace libloaderapi.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class DevelClientRestrictedAttribute : Attribute
    {
        public bool RestrictToIpAddress { get; set; } = true;
    }
}
