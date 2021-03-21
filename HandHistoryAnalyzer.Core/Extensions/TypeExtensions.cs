using System;
using System.Collections.Generic;
using System.Linq;

namespace HandHistoryAnalyzer.Core.Extensions
{
    /// <summary>
    /// Extensions for `Type`.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets generic interfaces which the type `type` implements.
        /// </summary>
        public static IEnumerable<Type> GetGenericInterfaces(this Type type, Type genericInterfaceType)
        {
            return type.GetInterfaces().Where(@interface =>
                @interface.IsGenericType
                && @interface.GetGenericTypeDefinition() == genericInterfaceType
            );
        }
    }
}
