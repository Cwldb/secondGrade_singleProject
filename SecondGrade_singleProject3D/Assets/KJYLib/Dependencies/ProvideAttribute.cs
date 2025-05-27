using System;

namespace KJYLib.Dependencies
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ProvideAttribute : Attribute
    {
    }
}