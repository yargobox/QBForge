using QBForge.Extensions.Linq.Expressions;
using QBForge.Extensions.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QBForge.Provider.Configuration
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
		public MemberMappingInfo(string name, string mappedName, bool isMappedNameExpected, Type memberType, Type declaringType, MapMemberAs mapAs, Action<object, object?>? setter)
		{
			Name = name;
			MappedName = mappedName;
			IsMappedNameExpected = isMappedNameExpected;
			MemberType = memberType;
			DeclaringType = declaringType;
			MapAs = mapAs;
			Setter = setter;
		}

		public string Name { get; }
		public string MappedName { get; }
		public bool IsMappedNameExpected { get; }
		public Type MemberType { get; }
		public Type DeclaringType { get; }
		public MapMemberAs MapAs { get; }
		public Action<object, object?>? Setter { get; }
	}

	public class DocumentMapping
	{
		protected virtual HashSet<Type> KnownDocumentTypes { get; set; }
		protected virtual HashSet<Type> KnownNonDocumentTypes { get; set; }

		public DocumentMapping()
		{
			KnownDocumentTypes = new HashSet<Type>();
			KnownNonDocumentTypes = new HashSet<Type>()
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
				typeof(Guid),
				typeof(Guid?),
				typeof(DateTime),
				typeof(DateTime?),
				typeof(DateTimeOffset),
				typeof(DateTimeOffset?),
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
				typeof(Guid[]),
				typeof(Guid?[]),
				typeof(DateTime[]),
				typeof(DateTime?[]),
				typeof(DateTimeOffset[]),
				typeof(DateTimeOffset?[]),
				typeof(TimeSpan[]),
				typeof(TimeSpan?[]),
				typeof(IntPtr[]),
				typeof(IntPtr?[]),
				typeof(UIntPtr[]),
				typeof(UIntPtr?[])
			};

			// DateOnly and TimeOnly support
			//

			Type? tn, t = (Type?)typeof(DateTime).Assembly.GetType("System.DateOnly", false);
			if (t != null)
			{
				KnownNonDocumentTypes.Add(t);
				KnownNonDocumentTypes.Add(t.MakeArrayType());
				KnownNonDocumentTypes.Add(tn = typeof(Nullable<>).MakeGenericType(t));
				KnownNonDocumentTypes.Add(tn.MakeArrayType());

				t = (Type)typeof(DateTime).Assembly.GetType("System.TimeOnly", true)!;
				KnownNonDocumentTypes.Add(t);
				KnownNonDocumentTypes.Add(t.MakeArrayType());
				KnownNonDocumentTypes.Add(tn = typeof(Nullable<>).MakeGenericType(t));
				KnownNonDocumentTypes.Add(tn.MakeArrayType());
			}
		}

		protected virtual IEnumerable<MemberInfo> GetAllMembers(Type documentType)
		{
			var members = documentType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				.Where(x => !x.IsDefined(typeof(NotMappedAttribute)) && (x.GetSetMethod(true)?.IsPublic == true || x.IsDefined(typeof(ColumnAttribute))))
				.Cast<MemberInfo>()
				.Union
				(
					documentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
					.Where(x => x.IsDefined(typeof(ColumnAttribute)))
				)
				.Select(x => (member: x, order: x.GetCustomAttribute<ColumnAttribute>()?.Order ?? 0))
				.ToArray();

			foreach (var m in members.Where(x => x.order <= 0))
			{
				yield return m.member;
			}

			foreach (var m in members.Where(x => x.order > 0).OrderBy(x => x.order))
			{
				yield return m.member;
			}
		}

		protected virtual bool IsKnownNonDocumentType(Type type)
		{
			return type.IsEnum || KnownNonDocumentTypes.Contains(type);
		}

		protected virtual bool IsKnownDocumentType(Type type)
		{
			return KnownDocumentTypes.Contains(type);
		}

		protected virtual MapMemberAs GetMemberMappingType(MemberInfo memberInfo)
		{
			var type = memberInfo.GetPropertyOrFieldType();
			MapMemberAs mapAs = MapMemberAs.None;

			if (IsKnownNonDocumentType(type))
			{
				mapAs = MapMemberAs.Element;
			}
			else if (IsKnownDocumentType(type))
			{
				mapAs = MapMemberAs.Document;
			}
			else
			{
				var trueType = Nullable.GetUnderlyingType(type) ?? type;
				var itemTypes = trueType.GetEnumerableItemTypes().ToArray();

				foreach (var itemType in itemTypes)
				{
					if (IsKnownNonDocumentType(itemType))
					{
						mapAs = MapMemberAs.Element; break;
					}
					else if (IsKnownDocumentType(itemType))
					{
						mapAs = MapMemberAs.DocumentCollection; break;
					}
				}

				if (mapAs == MapMemberAs.None)
				{
					mapAs = GetDefaultMemberMappingType(memberInfo, type, trueType, itemTypes);
				}
			}

			return mapAs;
		}

		protected virtual MapMemberAs GetDefaultMemberMappingType(MemberInfo memberInfo, Type type, Type trueType, Type[] itemTypes)
		{
			return itemTypes.Length > 0 ? MapMemberAs.DocumentCollection : MapMemberAs.Document;
		}

		protected virtual string GetMemberMappedName(MemberInfo memberInfo, out bool isMappedNameExpected)
		{
			isMappedNameExpected = false;

			var mappedName = memberInfo.GetCustomAttribute<ColumnAttribute>()?.Name;

			return string.IsNullOrEmpty(mappedName) ? memberInfo.Name : mappedName!;
		}

		public virtual List<MemberMappingInfo> GetDocumentMappingInfo(Type documentType)
		{
			var list = new List<MemberMappingInfo>();

			foreach (var memberInfo in GetAllMembers(documentType))
			{
				var type = memberInfo.GetPropertyOrFieldType();

				var mappingType = GetMemberMappingType(memberInfo);
				if (mappingType == MapMemberAs.None)
				{
					continue;
				}

				var mappedName = GetMemberMappedName(memberInfo, out var isMappedNameExpected);

				list.Add(new MemberMappingInfo(
					memberInfo.Name,
					mappedName,
					isMappedNameExpected,
					type,
					memberInfo.DeclaringType,
					mappingType,
					mappingType.HasFlag(MapMemberAs.Document) ? memberInfo.MakeCommonSetter()?.Compile() : null
				));
			}

			return list;
		}
	}
}