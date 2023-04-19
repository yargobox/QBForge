using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QBForge.Provider.Configuration.PostgreSql
{
	public class PostgreSqlMapping : DocumentMapping
	{
		private Type? _anyNpgSqlType;

		public PostgreSqlMapping()
		{
			KnownNonDocumentTypes = new HashSet<Type>(KnownNonDocumentTypes)
			{
				typeof(Dictionary<string, string>),
				typeof(IDictionary<string, string>),
				typeof(System.Net.IPAddress),
				typeof(System.Net.IPAddress[]),
				typeof(ArraySegment<byte>),
				typeof(ArraySegment<byte>?),
				typeof(ArraySegment<byte>[]),
				typeof(ArraySegment<byte>?[]),
				typeof(System.Numerics.BigInteger),
				typeof(System.Numerics.BigInteger?),
				typeof(System.Numerics.BigInteger[]),
				typeof(System.Numerics.BigInteger?[]),
				typeof(ValueTuple<System.Net.IPAddress, int>),
				typeof(ValueTuple<System.Net.IPAddress, int>?),
				typeof(ValueTuple<System.Net.IPAddress, int>[]),
				typeof(ValueTuple<System.Net.IPAddress, int>?[])
			};
		}

		protected override bool IsKnownNonDocumentType(Type type)
		{
			if (base.IsKnownNonDocumentType(type))
			{
				return true;
			}

			if (!IsKnownNonDocumentPostgreSqlTypesAdded())
			{
				var fullName = type.FullName;

				if (fullName != null
					&& (fullName.StartsWith("NpgsqlTypes.Npgsql", StringComparison.Ordinal)
						|| fullName.StartsWith("System.Nullable`1[[NpgsqlTypes.Npgsql", StringComparison.Ordinal))
					&& type.Assembly.GetName().Name == "Npgsql")
				{
					AddKnownNonDocumentPostgreSqlTypes(type.Assembly);

					return KnownNonDocumentTypes.Contains(type);
				}
			}

			return false;
		}

		protected override MapMemberAs GetDefaultMemberMappingType(MemberInfo memberInfo, Type type, Type trueType, Type[] itemTypes)
		{
			if (IsNpgsqlRange(trueType))
			{
				return MapMemberAs.Element;
			}

			foreach (var itemType in itemTypes)
			{
				if (IsNpgsqlRange(itemType))
				{
					return MapMemberAs.Element;
				}
			}

			return itemTypes.Length > 0 ? MapMemberAs.DocumentCollection : MapMemberAs.Document;

			static bool IsNpgsqlRange(Type typeToTest)
			{
				var fullName = typeToTest.FullName;

				return fullName != null
					&& (fullName.StartsWith("NpgsqlTypes.NpgsqlRange`1[[", StringComparison.Ordinal)
						|| fullName.StartsWith("System.Nullable`1[[NpgsqlTypes.NpgsqlRange`1[[", StringComparison.Ordinal));
			}
		}

		private bool IsKnownNonDocumentPostgreSqlTypesAdded()
		{
			return _anyNpgSqlType != null && KnownNonDocumentTypes.Contains(_anyNpgSqlType);
		}

		private void AddKnownNonDocumentPostgreSqlTypes(Assembly assembly)
		{
			var npgSqlTypes = new List<Type?>();

			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlTsQuery", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlTsVector", false));

			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlBox", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlCircle", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlInterval", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlLine", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlLogSequenceNumber", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlLSeg", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlPath", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlPoint", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlPolygon", false));
			//npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlRange<>", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlTid", false));
			npgSqlTypes.Add((Type?)assembly.GetType("NpgsqlTypes.NpgsqlInet", false));

			var anyNpgSqlType = npgSqlTypes.First(x => x != null);
			var result = new HashSet<Type>(KnownNonDocumentTypes);

			Type? t, nt;
			for (int i = 0, j = npgSqlTypes.Count; i < j; i++)
			{
				t = npgSqlTypes[i]; if (t == null) continue;

				result.Add(t);
				result.Add(t.MakeArrayType());

				if (t.IsValueType)
				{
					nt = typeof(Nullable<>).MakeGenericType(t);

					result.Add(nt);
					result.Add(nt.MakeArrayType());
				}
			}

			KnownNonDocumentTypes = result;
			_anyNpgSqlType = anyNpgSqlType;
		}
	}
}