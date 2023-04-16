using System;

namespace QBForge.Provider.Configuration.PostgreSql
{
	public class PostgreSqlMapping : DocumentMapping
	{
		protected override bool IsKnownNonDocumentType(Type type)
		{
			//typeof(Dictionary<string, string>),
			//typeof(IDictionary<string, string>),

			//typeof(System.Net.IPAddress),

			//typeof(NpgsqlTsQuery),
			//typeof(NpgsqlTsVector),

			//typeof(ArraySegment<byte>),
			//typeof(ArraySegment<byte>?),
			//typeof(System.Numerics.BigInteger),
			//typeof(System.Numerics.BigInteger?),
			//typeof(ValueTuple<System.Net.IPAddress, int>), // instead typeof(NpgsqlInet)
			//typeof(ValueTuple<System.Net.IPAddress, int>?),

			//typeof(NpgsqlBox),
			//typeof(NpgsqlBox?),
			//typeof(NpgsqlCircle),
			//typeof(NpgsqlCircle?),
			//typeof(NpgsqlInterval),
			//typeof(NpgsqlInterval?),
			//typeof(NpgsqlLine),
			//typeof(NpgsqlLine?),
			//typeof(NpgsqlLogSequenceNumber),
			//typeof(NpgsqlLogSequenceNumber?),
			//typeof(NpgsqlLSeg),
			//typeof(NpgsqlLSeg?),
			//typeof(NpgsqlPath),
			//typeof(NpgsqlPath?),
			//typeof(NpgsqlPoint),
			//typeof(NpgsqlPoint?),
			//typeof(NpgsqlPolygon),
			//typeof(NpgsqlPolygon?),
			////typeof(NpgsqlRange<>),
			//typeof(NpgsqlTid),
			//typeof(NpgsqlTid?)

			return base.IsKnownNonDocumentType(type);
		}
	}
}