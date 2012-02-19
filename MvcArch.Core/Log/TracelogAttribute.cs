using System;

namespace MvcArch.Core.Log
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TracelogAttribute : Attribute { }
}