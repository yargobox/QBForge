using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QBForge.Extensions.Reflection
{
	internal static class ReflectionExtensions
	{
		public static IEnumerable<Type> GetEnumerableItemTypes(this Type type)
		{
			if (type.IsGenericType && type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				return new Type[] { type.GetGenericArguments()[0] };
			}
			else
			{
				return type
					.GetInterfaces()
					.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
					.Select(i => i.GetGenericArguments()[0]);
			}
		}

		public static Type GetPropertyOrFieldType(this MemberInfo memberInfo)
		{
			return memberInfo is PropertyInfo pi
				? pi.PropertyType
				: memberInfo is FieldInfo fi
					? fi.FieldType
					: memberInfo is null
						? throw new ArgumentNullException(nameof(memberInfo))
						: throw new ArgumentException($"\"{nameof(PropertyInfo)}\" or \"{nameof(FieldInfo)}\" types are expected, not \"{memberInfo.GetType().Name}\"", nameof(memberInfo));
		}
	}
}