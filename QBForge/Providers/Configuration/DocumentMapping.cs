using QBForge.Extensions.Linq.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QBForge.Providers.Configuration
{
	public enum MapMemberAs
	{
		None = 0,
		Element = 1,
		Document = 2,
		DocumentCollection = 6
	}

	public class MemberMappingInfo
	{
		public MemberMappingInfo(string name, string mappedName, Type memberType, Type declaringType, MapMemberAs mapAs, Action<object, object?>? setter)
		{
			Name = name;
			MappedName = mappedName;
			MemberType = memberType;
			DeclaringType = declaringType;
			MapAs = mapAs;
			Setter = setter;
		}

		public string Name { get; }
		public string MappedName { get; }
		public Type MemberType { get; }
		public Type DeclaringType { get; }
		public MapMemberAs MapAs { get; }
		public Action<object, object?>? Setter { get; }
	}

	public class DocumentMapping
	{
		protected virtual IEnumerable<MemberInfo> GetAllMembers(Type documentType)
		{
			return documentType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanWrite && x.CanRead);
		}

		protected virtual bool IsKnownNonDocumentType(Type type)
		{
			return type.IsEnum || Static._standardTypes.Contains(type) || IsDateTimeOnlyOrOriginatedType(type);
		}

		protected virtual bool IsKnownDocumentType(Type type)
		{
			return false;
		}

		protected virtual bool IsDocumentType(Type type)
		{
			if (type.FullName?.StartsWith("System.ValueTuple`", StringComparison.Ordinal) == true) return false;

			return true;
		}

		protected virtual string GetMemberMappedName(MemberInfo memberInfo)
		{
			return memberInfo.Name;
		}

		protected virtual MapMemberAs GetMemberMappingType(MemberInfo memberInfo)
		{
			var type = memberInfo is PropertyInfo pi
				? pi.PropertyType
				: memberInfo is FieldInfo fi
					? fi.FieldType
					: throw new InvalidOperationException();

			var mapAs = IsKnownNonDocumentType(type)
				? MapMemberAs.Element
				: IsKnownDocumentType(type)
					? MapMemberAs.Document
					: MapMemberAs.None;

			if (mapAs == MapMemberAs.None)
			{
				Type openType;
				foreach (var i in type.GetInterfaces())
				{
					if (i.IsGenericType)
					{
						openType = i.GetGenericTypeDefinition();
						if (openType == typeof(IEnumerable<>))
						{
							var itemType = openType.GetGenericArguments()[0];

							if (IsKnownNonDocumentType(itemType))
							{
								mapAs = MapMemberAs.Element;
								break;
							}
							else if (IsKnownDocumentType(itemType))
							{
								mapAs = MapMemberAs.DocumentCollection;
								break;
							}
						}
						else if (openType == typeof(Nullable<>))
						{
							var itemType = openType.GetGenericArguments()[0];

							if (IsKnownNonDocumentType(itemType))
							{
								mapAs = MapMemberAs.Element;
								break;
							}
							else if (IsKnownDocumentType(itemType))
							{
								mapAs = MapMemberAs.Document;
								break;
							}
						}
					}
				}
			}

			if (mapAs == MapMemberAs.None)
			{
				mapAs = IsDocumentType(type) ? MapMemberAs.Document : MapMemberAs.Element;
			}

			return mapAs;
		}

		public List<MemberMappingInfo> GetDocumentMappingInfo(Type documentType)
		{
			List<MemberMappingInfo> list = new();

			foreach (var memberInfo in GetAllMembers(documentType))
			{
				var type = memberInfo is PropertyInfo pi
					? pi.PropertyType
					: memberInfo is FieldInfo fi
						? fi.FieldType
						: throw new InvalidOperationException();

				var mappingType = GetMemberMappingType(memberInfo);
				if (mappingType == MapMemberAs.None)
				{
					continue;
				}

				var mappedName = GetMemberMappedName(memberInfo);

				list.Add(new MemberMappingInfo(
					memberInfo.Name,
					mappedName,
					type,
					memberInfo.DeclaringType,
					mappingType,
					mappingType == MapMemberAs.Document ? memberInfo.MakeCommonSetter()?.Compile() : null
				));
			}

			return list;
		}

		private static bool IsDateTimeOnlyOrOriginatedType(Type type)
		{
			var fullName = type.FullName;
			if (fullName?.StartsWith("System.", StringComparison.Ordinal) == true)
			{
				if (fullName?.StartsWith("System.Nullable`", StringComparison.Ordinal) == true)
				{
					Type? underType;

					if (type.IsArray)
					{
						var elemType = type.GetElementType();
						if (elemType == null) return false;

						underType = Nullable.GetUnderlyingType(elemType);
					}
					else
					{
						underType = Nullable.GetUnderlyingType(type);
					}

					if (underType == null) return false;

					fullName = underType.FullName;
				}

				if (fullName == "System.DateOnly" || fullName == "System.TimeOnly" || fullName == "System.DateOnly[]" || fullName == "System.TimeOnly[]")
				{
					return true;
				}
			}

			return false;
		}

		private static class Static
		{
			public static readonly HashSet<Type> _standardTypes = new()
			{
				// Value types
				//

				typeof(bool),
				typeof(bool?),
				typeof(char),
				typeof(char?),
				typeof(byte),
				typeof(byte?),
				typeof(sbyte),
				typeof(sbyte?),
				typeof(short),
				typeof(short?),
				typeof(ushort),
				typeof(ushort?),
				typeof(int),
				typeof(int?),
				typeof(uint),
				typeof(uint?),
				typeof(long),
				typeof(long?),
				typeof(ulong),
				typeof(ulong?),
				typeof(float),
				typeof(float?),
				typeof(double),
				typeof(double?),
				typeof(decimal),
				typeof(decimal?),
				typeof(nint),
				typeof(nint?),
				typeof(nuint),
				typeof(nuint?),
				typeof(Guid),
				typeof(Guid?),
				//typeof(DateOnly),
				//typeof(DateOnly?),
				typeof(DateTime),
				typeof(DateTime?),
				typeof(DateTimeOffset),
				typeof(DateTimeOffset?),
				//typeof(TimeOnly),
				//typeof(TimeOnly?),
				typeof(TimeSpan),
				typeof(TimeSpan?),
				typeof(IntPtr),
				typeof(IntPtr?),
				typeof(UIntPtr),
				typeof(UIntPtr?),

				// Ref types
				//

				typeof(string),
				typeof(string[]),
				typeof(object),
				typeof(object[]),
				typeof(Regex),
				typeof(Regex[]),
				typeof(Array),
				typeof(Array[]),
				typeof(BitArray),
				typeof(BitArray[]),

				typeof(bool[]),
				typeof(bool?[]),
				typeof(char[]),
				typeof(char?[]),
				typeof(byte[]),
				typeof(byte?[]),
				typeof(sbyte[]),
				typeof(sbyte?[]),
				typeof(short[]),
				typeof(short?[]),
				typeof(ushort[]),
				typeof(ushort?[]),
				typeof(int[]),
				typeof(int?[]),
				typeof(uint[]),
				typeof(uint?[]),
				typeof(long[]),
				typeof(long?[]),
				typeof(ulong[]),
				typeof(ulong?[]),
				typeof(float[]),
				typeof(float?[]),
				typeof(double[]),
				typeof(double?[]),
				typeof(decimal[]),
				typeof(decimal?[]),
				typeof(nint[]),
				typeof(nint?[]),
				typeof(nuint[]),
				typeof(nuint?[]),
				typeof(Guid[]),
				typeof(Guid?[]),
				//typeof(DateOnly[]),
				//typeof(DateOnly?[]),
				typeof(DateTime[]),
				typeof(DateTime?[]),
				typeof(DateTimeOffset[]),
				typeof(DateTimeOffset?[]),
				//typeof(TimeOnly[]),
				//typeof(TimeOnly?[]),
				typeof(TimeSpan[]),
				typeof(TimeSpan?[]),
				typeof(IntPtr[]),
				typeof(IntPtr?[]),
				typeof(UIntPtr[]),
				typeof(UIntPtr?[])
			};
		}
	}
}