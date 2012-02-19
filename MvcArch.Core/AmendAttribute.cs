using System;
using System.Collections.Generic;
using Afterthought;
using MvcArch.Core.Log;

namespace MvcArch.Core
{
    /// <summary>
    /// Assembly-level amendment provider attribute indicating to Afterthought that
    /// all types in any amended assembly with the <see cref="Log.TracelogAttribute"/> applied
    /// should be amended/logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AmendAttribute : Attribute, IAmendmentAttribute
    {
        IEnumerable<ITypeAmendment> IAmendmentAttribute.GetAmendments(Type target)
        {
            if (target.GetCustomAttributes(typeof(TracelogAttribute), true).Length > 0)
                yield return (ITypeAmendment)typeof(LogAmender<>).MakeGenericType(target).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
        }
    }
}