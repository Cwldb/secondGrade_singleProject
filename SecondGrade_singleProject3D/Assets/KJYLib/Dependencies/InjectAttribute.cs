using System;

namespace KJYLib.Dependencies
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    { }
}