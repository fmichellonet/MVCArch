using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace MvcArch.Core.Log
{
    /// <summary>
    /// Simple logger class. 
    /// </summary>
    public class Logger<T>
    {
        /// <summary>
        /// Simple logging
        /// </summary>
        public static void LogMethodBefore(T instance, string name, object[] ps)
        {
            Log(instance, name, ps, "Before");
        }

        /// <summary>
        /// Simple logging
        /// </summary>
        public static void LogMethodAfter(T instance, string name, object[] ps)
        {
            Log(instance, name, ps, "After");
        }

        public static TK LogGetPropertyAfter<TK>(T instance, string propertyName, TK returnValue)
        {
            Log(instance, propertyName, new object[] {}, "After");
            return returnValue;
        }

        public static void LogGetPropertyBefore(T instance, string propertyName)
        {
            Log(instance, propertyName, new object[] {}, "Before");
        }

        public static TK LogSetPropertyBefore<TK>(T instance, string propertyName, TK oldValue, TK value)
        {
            Log(instance, propertyName, new object[] {value}, "Before");
            return value;
        }

        public static void LogSetPropertyAfter<TK>(T instance, string propertyName, TK oldValue, TK value, TK newValue)
        {
            Log(instance, propertyName, new object[] {value}, "After");
        }

        /// <summary>
        /// The implementation of writeline
        /// </summary>
        private static void Log(object instance, string name, IEnumerable<object> parameters, string prefix)
        {
            StringBuilder strBuilder = new StringBuilder();

            //get function name and class name
            strBuilder.Append(String.Format("{0} {1}.{2}(", prefix, instance != null ? instance.GetType().FullName : "static", name));
            //add parameters
            foreach (var p in parameters)
                strBuilder.Append(p != null ? p.ToString() : "NULL");
            strBuilder.Append(")");

            LogManager.GetCurrentClassLogger().Debug(strBuilder.ToString());
        }
    }
}