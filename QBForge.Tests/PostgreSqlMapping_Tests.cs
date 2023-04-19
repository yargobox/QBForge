using FluentAssertions;
using NpgsqlTypes;
using QBForge.Provider.Configuration;
using QBForge.Provider.Configuration.PostgreSql;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

#pragma warning disable CA1822 // Mark members as static

namespace QBForge.Tests
{
	public class PostgreSqlMapping_Tests
	{
		[Theory, InlineData, InlineData]
		public void GetMappingInfo_Recognizes_WellKnownTypes()
		{
			var mappingInfo = PostgreSqlConfig.Provider.GetMappingInfo<DocumentClass>();

			mappingInfo.Should().NotBeNull().And.AllSatisfy(x =>
			{
				x.Value.DeclaringType.Should().BeSameAs(typeof(DocumentClass));
				x.Value.MappedName.Should().BeEquivalentTo(x.Value.Name);
				x.Value.IsMappedNameExpected.Should().Be(false);
			});

			mappingInfo.Should().ContainKey(nameof(DocumentClass.BoolProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BoolPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.CharProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.CharPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ByteProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BytePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.SbyteProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.SbytePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ShortProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ShortPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UshortProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UshortPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UintProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UintPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.LongProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.LongPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UlongProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UlongPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.FloatProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.FloatPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DoubleProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DoublePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DecimalProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DecimalPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.GuidProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.GuidPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimeProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimeOffsetProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimeOffsetPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeSpanProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeSpanPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPtrProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPtrPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UIntPtrProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UIntPtrPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#if NET6_0_OR_GREATER
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateOnlyProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateOnlyPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeOnlyProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeOnlyPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#endif

			// Ref types
			//

			mappingInfo.Should().ContainKey(nameof(DocumentClass.StringProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.StringPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ObjectProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ObjectPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.RegexProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.RegexPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ArrayProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ArrayPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BitArrayProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BitArrayPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);

			mappingInfo.Should().ContainKey(nameof(DocumentClass.BoolPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BoolPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.CharPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.CharPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BytePropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BytePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.SbytePropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.SbytePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ShortPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ShortPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UshortPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UshortPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UintPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UintPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.LongPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.LongPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UlongPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UlongPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.FloatPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.FloatPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DoublePropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DoublePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DecimalPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DecimalPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.GuidPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.GuidPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimePropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimeOffsetPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateTimeOffsetPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeSpanPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeSpanPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPtrPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IntPtrPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UIntPtrPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.UIntPtrPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#if NET6_0_OR_GREATER
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateOnlyPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DateOnlyPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeOnlyPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.TimeOnlyPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#endif

			mappingInfo.Should().ContainKey(nameof(DocumentClass.OtherDocumentClassProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Document);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.OtherDocumentClassPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.DocumentCollection);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.OtherDocumentClassPropList)).WhoseValue.MapAs.Should().Be(MapMemberAs.DocumentCollection);

			mappingInfo.Should().ContainKey(nameof(DocumentClass.DocumentStructProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Document);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DocumentStructPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Document);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DocumentStructPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.DocumentCollection);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DocumentStructPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.DocumentCollection);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.DocumentStructPropNullableEnumerable)).WhoseValue.MapAs.Should().Be(MapMemberAs.DocumentCollection);


			// PostgreSql
			//

			mappingInfo.Should().ContainKey(nameof(DocumentClass.DictionaryProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IDictionaryProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IPAddressProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.IPAddressPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ArraySegmentByteProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ArraySegmentBytePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ArraySegmentBytePropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ArraySegmentBytePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BigIntegerProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BigIntegerPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BigIntegerPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.BigIntegerPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ValueTupleIPAddressIntProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ValueTupleIPAddressIntPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ValueTupleIPAddressIntPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.ValueTupleIPAddressIntPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);

			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTsQueryProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTsQueryPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTsVectorProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTsVectorPropArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);

			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlBoxProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlBoxPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlBoxPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlBoxPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlCircleProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlCirclePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlCirclePropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlCirclePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlIntervalProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlIntervalPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlIntervalPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlIntervalPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLineProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLinePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLinePropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLinePropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLogSequenceNumberProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLogSequenceNumberPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLogSequenceNumberPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLogSequenceNumberPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLSegProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLSegPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLSegPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlLSegPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPathProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPathPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPathPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPathPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPointProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPointPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPointPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPointPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPolygonProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPolygonPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPolygonPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlPolygonPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTidProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTidPropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTidPropArraay)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlTidPropNullableArray)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlRangeByteProp)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(DocumentClass.NpgsqlRangeBytePropNullable)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);

#if NET6_0_OR_GREATER
			mappingInfo.Should().HaveCount(164);
#else
			mappingInfo.Should().HaveCount(156);
#endif
		}



		[Fact]
		public void GetMappingInfo_Includes_PublicWritablePropsByDefault()
		{
			var mappingInfo = PostgreSqlConfig.Provider.GetMappingInfo(typeof(OtherDocumentClass));

			mappingInfo.Should().NotBeNull().And.AllSatisfy(x =>
			{
				x.Value.DeclaringType.Should().BeSameAs(typeof(OtherDocumentClass));
				x.Value.MappedName.Should().BeEquivalentTo(x.Value.Name);
				x.Value.IsMappedNameExpected.Should().Be(false);
			});

			mappingInfo.Should().ContainKey(nameof(OtherDocumentClass.PropPublicGetPublicSet)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(OtherDocumentClass.PropPublicSet)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(OtherDocumentClass.PropProtectedGetPublicSet)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#if NET6_0_OR_GREATER
			mappingInfo.Should().ContainKey(nameof(OtherDocumentClass.PropPublicInit)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(OtherDocumentClass.PropPublicGetPublicInit)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(OtherDocumentClass.PropProtectedGetPublicInit)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#endif

#if NET6_0_OR_GREATER
			mappingInfo.Should().HaveCount(6);
#else
			mappingInfo.Should().HaveCount(3);
#endif
		}



		[Fact]
		public void GetMappingInfo_Includes_NonPublicWritablePropsWithCollumnAttr()
		{
			var mappingInfo = PostgreSqlConfig.Provider.GetMappingInfo(typeof(InversedDocumentClass));

			mappingInfo.Should().NotBeNull().And.AllSatisfy(x => x.Value.DeclaringType.Should().BeSameAs(typeof(InversedDocumentClass)));

			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicSet)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicGet)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey("PropPrivateGetPrivateSet").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey("PropPrivateGet").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey("PropPrivateSet").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicGetProtectedSet)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PublicField)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey("PrivateField").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PublicReadonlyField)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey("ProtectedReadonlyField").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PublicStaticField));
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PublicStaticReadonlyField));
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PublicConstField));
			mappingInfo.Should().NotContainKey("ProtectedStaticField");
			mappingInfo.Should().NotContainKey("ProtectedStaticReadonlyField");
			mappingInfo.Should().NotContainKey("ProtectedConstField");
#if NET6_0_OR_GREATER
			mappingInfo.Should().ContainKey("PropPrivateInit").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey("PropPrivateGetPrivateInit").WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicGetProtectedInit)).WhoseValue.MapAs.Should().Be(MapMemberAs.Element);
#endif
		}



		[Fact]
		public void GetMappingInfo_NotIncludes_PublicWritablePropsWithNotMappedAttr()
		{
			var mappingInfo = PostgreSqlConfig.Provider.GetMappingInfo(typeof(InversedDocumentClass));

			mappingInfo.Should().NotBeNull();

			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PropPublicGetPublicSet));
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PropProtectedGetPublicSet));
#if NET6_0_OR_GREATER
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PropPublicInit));
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PropPublicGetPublicInit));
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PropProtectedGetPublicInit));
			mappingInfo.Should().NotContainKey(nameof(InversedDocumentClass.PropPublicInit));
#endif
		}



		[Fact]
		public void GetMappingInfo_Obtains_MappedNamesWithColumnAttr()
		{
			var mappingInfo = PostgreSqlConfig.Provider.GetMappingInfo(typeof(InversedDocumentClass));

			mappingInfo.Should().NotBeNull();

			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicSet)).WhoseValue.MappedName.Should().BeEquivalentTo("prop_public_set");
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicGet)).WhoseValue.MappedName.Should().BeEquivalentTo("prop_public_get");
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PublicField)).WhoseValue.MappedName.Should().BeEquivalentTo("public_field");
#if NET6_0_OR_GREATER
			mappingInfo.Should().ContainKey(nameof(InversedDocumentClass.PropPublicGetProtectedInit)).WhoseValue.MappedName.Should().BeEquivalentTo("prop_public_get_protected_init");
#endif
		}



		[Fact]
		public void GetMappedName_Obtains_MappedNamesWithColumnAttr()
		{
			PostgreSqlConfig.Provider.GetMappedName<InversedDocumentClass>(nameof(InversedDocumentClass.PropPublicSet)).Should().BeEquivalentTo("prop_public_set");
			PostgreSqlConfig.Provider.GetMappedName<InversedDocumentClass>(nameof(InversedDocumentClass.PropPublicGet)).Should().BeEquivalentTo("prop_public_get");
			PostgreSqlConfig.Provider.GetMappedName<InversedDocumentClass>(nameof(InversedDocumentClass.PublicField)).Should().BeEquivalentTo("public_field");
#if NET6_0_OR_GREATER
			PostgreSqlConfig.Provider.GetMappedName<InversedDocumentClass>(nameof(InversedDocumentClass.PropPublicGetProtectedInit)).Should().BeEquivalentTo("prop_public_get_protected_init");
#endif
		}



		[Fact]
		public void GetMappingInfo_Respects_OrderPropOfColumnAttr()
		{
			var mappingInfo = PostgreSqlConfig.Provider.GetMappingInfo(typeof(InversedDocumentClass));

			mappingInfo.Should().NotBeNull().And.HaveCountGreaterThanOrEqualTo(4);

			var keys = mappingInfo.Keys.ToArray();

			keys.Should().BeEquivalentTo(mappingInfo.Select(x => x.Key), options => options.WithStrictOrdering());
			keys.Should().BeEquivalentTo(mappingInfo.Select(x => x.Value.Name), options => options.WithStrictOrdering());
			keys.Should().BeEquivalentTo(mappingInfo.Values.Select(x => x.Name), options => options.WithStrictOrdering());

			keys[keys.Length - 1].Should().BeEquivalentTo(nameof(InversedDocumentClass.PropPublicSet));
			keys[keys.Length - 2].Should().BeEquivalentTo(nameof(InversedDocumentClass.PropPublicGet));
			keys[keys.Length - 3].Should().BeEquivalentTo("PrivateField");
#if NET6_0_OR_GREATER
			keys[keys.Length - 4].Should().BeEquivalentTo(nameof(InversedDocumentClass.PropPublicGetProtectedInit));
#endif
		}



		private class DocumentClass
		{
			public bool BoolProp { get; set; }
			public bool? BoolPropNullable { get; set; }
			public char CharProp { get; set; }
			public char? CharPropNullable { get; set; }
			public byte ByteProp { get; set; }
			public byte? BytePropNullable { get; set; }
			public sbyte SbyteProp { get; set; }
			public sbyte? SbytePropNullable { get; set; }
			public short ShortProp { get; set; }
			public short? ShortPropNullable { get; set; }
			public ushort UshortProp { get; set; }
			public ushort? UshortPropNullable { get; set; }
			public int IntProp { get; set; }
			public int? IntPropNullable { get; set; }
			public uint UintProp { get; set; }
			public uint? UintPropNullable { get; set; }
			public long LongProp { get; set; }
			public long? LongPropNullable { get; set; }
			public ulong UlongProp { get; set; }
			public ulong? UlongPropNullable { get; set; }
			public float FloatProp { get; set; }
			public float? FloatPropNullable { get; set; }
			public double DoubleProp { get; set; }
			public double? DoublePropNullable { get; set; }
			public decimal DecimalProp { get; set; }
			public decimal? DecimalPropNullable { get; set; }
			public Guid GuidProp { get; set; }
			public Guid? GuidPropNullable { get; set; }
			public DateTime DateTimeProp { get; set; }
			public DateTime? DateTimePropNullable { get; set; }
			public DateTimeOffset DateTimeOffsetProp { get; set; }
			public DateTimeOffset? DateTimeOffsetPropNullable { get; set; }
			public TimeSpan TimeSpanProp { get; set; }
			public TimeSpan? TimeSpanPropNullable { get; set; }
			public IntPtr IntPtrProp { get; set; }
			public IntPtr? IntPtrPropNullable { get; set; }
			public UIntPtr UIntPtrProp { get; set; }
			public UIntPtr? UIntPtrPropNullable { get; set; }
#if NET6_0_OR_GREATER
			public DateOnly DateOnlyProp { get; set; }
			public DateOnly? DateOnlyPropNullable { get; set; }
			public TimeOnly TimeOnlyProp { get; set; }
			public TimeOnly? TimeOnlyPropNullable { get; set; }
#endif

			// Ref types
			//

			public string StringProp { get; set; } = null!;
			public string[] StringPropArray { get; set; } = null!;
			public object ObjectProp { get; set; } = null!;
			public object[] ObjectPropArray { get; set; } = null!;
			public Regex RegexProp { get; set; } = null!;
			public Regex[] RegexPropArray { get; set; } = null!;
			public Array ArrayProp { get; set; } = null!;
			public Array[] ArrayPropArray { get; set; } = null!;
			public BitArray BitArrayProp { get; set; } = null!;
			public BitArray[] BitArrayPropArray { get; set; } = null!;

			public bool[] BoolPropArray { get; set; } = null!;
			public bool?[] BoolPropNullableArray { get; set; } = null!;
			public char[] CharPropArray { get; set; } = null!;
			public char?[] CharPropNullableArray { get; set; } = null!;
			public byte[] BytePropArray { get; set; } = null!;
			public byte?[] BytePropNullableArray { get; set; } = null!;
			public sbyte[] SbytePropArray { get; set; } = null!;
			public sbyte?[] SbytePropNullableArray { get; set; } = null!;
			public short[] ShortPropArray { get; set; } = null!;
			public short?[] ShortPropNullableArray { get; set; } = null!;
			public ushort[] UshortPropArray { get; set; } = null!;
			public ushort?[] UshortPropNullableArray { get; set; } = null!;
			public int[] IntPropArray { get; set; } = null!;
			public int?[] IntPropNullableArray { get; set; } = null!;
			public uint[] UintPropArray { get; set; } = null!;
			public uint?[] UintPropNullableArray { get; set; } = null!;
			public long[] LongPropArray { get; set; } = null!;
			public long?[] LongPropNullableArray { get; set; } = null!;
			public ulong[] UlongPropArray { get; set; } = null!;
			public ulong?[] UlongPropNullableArray { get; set; } = null!;
			public float[] FloatPropArray { get; set; } = null!;
			public float?[] FloatPropNullableArray { get; set; } = null!;
			public double[] DoublePropArray { get; set; } = null!;
			public double?[] DoublePropNullableArray { get; set; } = null!;
			public decimal[] DecimalPropArray { get; set; } = null!;
			public decimal?[] DecimalPropNullableArray { get; set; } = null!;
			public Guid[] GuidPropArray { get; set; } = null!;
			public Guid?[] GuidPropNullableArray { get; set; } = null!;
			public DateTime[] DateTimePropArray { get; set; } = null!;
			public DateTime?[] DateTimePropNullableArray { get; set; } = null!;
			public DateTimeOffset[] DateTimeOffsetPropArray { get; set; } = null!;
			public DateTimeOffset?[] DateTimeOffsetPropNullableArray { get; set; } = null!;
			public TimeSpan[] TimeSpanPropArray { get; set; } = null!;
			public TimeSpan?[] TimeSpanPropNullableArray { get; set; } = null!;
			public IntPtr[] IntPtrPropArray { get; set; } = null!;
			public IntPtr?[] IntPtrPropNullableArray { get; set; } = null!;
			public UIntPtr[] UIntPtrPropArray { get; set; } = null!;
			public UIntPtr?[] UIntPtrPropNullableArray { get; set; } = null!;
#if NET6_0_OR_GREATER
			public DateOnly[] DateOnlyPropArray { get; set; } = null!;
			public DateOnly?[] DateOnlyPropNullableArray { get; set; } = null!;
			public TimeOnly[] TimeOnlyPropArray { get; set; } = null!;
			public TimeOnly?[] TimeOnlyPropNullableArray { get; set; } = null!;
#endif

			public OtherDocumentClass OtherDocumentClassProp { get; set; } = null!;
			public OtherDocumentClass[] OtherDocumentClassPropArray { get; set; } = null!;
			public virtual List<OtherDocumentClass> OtherDocumentClassPropList { get; set; } = null!;

			public virtual DocumentStruct DocumentStructProp { get; set; }
			public DocumentStruct? DocumentStructPropNullable { get; set; }
			public DocumentStruct[] DocumentStructPropArray { get; set; } = null!;
			public DocumentStruct?[] DocumentStructPropNullableArray { get; set; } = null!;
			public IEnumerable<DocumentStruct?> DocumentStructPropNullableEnumerable { get; set; } = null!;


			// PostgreSql
			//

			public Dictionary<string, string> DictionaryProp { get; set; } = null!;
			public IDictionary<string, string> IDictionaryProp { get; set; } = null!;
			public System.Net.IPAddress IPAddressProp { get; set; } = null!;
			public System.Net.IPAddress[] IPAddressPropArray { get; set; } = null!;
			public ArraySegment<byte> ArraySegmentByteProp { get; set; }
			public ArraySegment<byte>? ArraySegmentBytePropNullable { get; set; }
			public ArraySegment<byte>[] ArraySegmentBytePropArray { get; set; } = null!;
			public ArraySegment<byte>?[] ArraySegmentBytePropNullableArray { get; set; } = null!;
			public System.Numerics.BigInteger BigIntegerProp { get; set; }
			public System.Numerics.BigInteger? BigIntegerPropNullable { get; set; }
			public System.Numerics.BigInteger[] BigIntegerPropArray { get; set; } = null!;
			public System.Numerics.BigInteger?[] BigIntegerPropNullableArray { get; set; } = null!;
			public ValueTuple<System.Net.IPAddress, int> ValueTupleIPAddressIntProp { get; set; }
			public ValueTuple<System.Net.IPAddress, int>? ValueTupleIPAddressIntPropNullable { get; set; }
			public ValueTuple<System.Net.IPAddress, int>[] ValueTupleIPAddressIntPropArray { get; set; } = null!;
			public ValueTuple<System.Net.IPAddress, int>?[] ValueTupleIPAddressIntPropNullableArray { get; set; } = null!;

			public NpgsqlTsQuery NpgsqlTsQueryProp { get; set; } = null!;
			public NpgsqlTsQuery[] NpgsqlTsQueryPropArraay { get; set; } = null!;
			public NpgsqlTsVector NpgsqlTsVectorProp { get; set; } = null!;
			public NpgsqlTsVector[] NpgsqlTsVectorPropArray { get; set; } = null!;

			public NpgsqlBox NpgsqlBoxProp { get; set; }
			public NpgsqlBox? NpgsqlBoxPropNullable { get; set; }
			public NpgsqlBox[] NpgsqlBoxPropArraay { get; set; } = null!;
			public NpgsqlBox?[] NpgsqlBoxPropNullableArray { get; set; } = null!;
			public NpgsqlCircle NpgsqlCircleProp { get; set; }
			public NpgsqlCircle? NpgsqlCirclePropNullable { get; set; }
			public NpgsqlCircle[] NpgsqlCirclePropArraay { get; set; } = null!;
			public NpgsqlCircle?[] NpgsqlCirclePropNullableArray { get; set; } = null!;
			public NpgsqlInterval NpgsqlIntervalProp { get; set; }
			public NpgsqlInterval? NpgsqlIntervalPropNullable { get; set; }
			public NpgsqlInterval[] NpgsqlIntervalPropArraay { get; set; } = null!;
			public NpgsqlInterval?[] NpgsqlIntervalPropNullableArray { get; set; } = null!;
			public NpgsqlLine NpgsqlLineProp { get; set; }
			public NpgsqlLine? NpgsqlLinePropNullable { get; set; }
			public NpgsqlLine[] NpgsqlLinePropArraay { get; set; } = null!;
			public NpgsqlLine?[] NpgsqlLinePropNullableArray { get; set; } = null!;
			public NpgsqlLogSequenceNumber NpgsqlLogSequenceNumberProp { get; set; }
			public NpgsqlLogSequenceNumber? NpgsqlLogSequenceNumberPropNullable { get; set; }
			public NpgsqlLogSequenceNumber[] NpgsqlLogSequenceNumberPropArraay { get; set; } = null!;
			public NpgsqlLogSequenceNumber?[] NpgsqlLogSequenceNumberPropNullableArray { get; set; } = null!;
			public NpgsqlLSeg NpgsqlLSegProp { get; set; }
			public NpgsqlLSeg? NpgsqlLSegPropNullable { get; set; }
			public NpgsqlLSeg[] NpgsqlLSegPropArraay { get; set; } = null!;
			public NpgsqlLSeg?[] NpgsqlLSegPropNullableArray { get; set; } = null!;
			public NpgsqlPath NpgsqlPathProp { get; set; }
			public NpgsqlPath? NpgsqlPathPropNullable { get; set; }
			public NpgsqlPath[] NpgsqlPathPropArraay { get; set; } = null!;
			public NpgsqlPath?[] NpgsqlPathPropNullableArray { get; set; } = null!;
			public NpgsqlPoint NpgsqlPointProp { get; set; }
			public NpgsqlPoint? NpgsqlPointPropNullable { get; set; }
			public NpgsqlPoint[] NpgsqlPointPropArraay { get; set; } = null!;
			public NpgsqlPoint?[] NpgsqlPointPropNullableArray { get; set; } = null!;
			public NpgsqlPolygon NpgsqlPolygonProp { get; set; }
			public NpgsqlPolygon? NpgsqlPolygonPropNullable { get; set; }
			public NpgsqlPolygon[] NpgsqlPolygonPropArraay { get; set; } = null!;
			public NpgsqlPolygon?[] NpgsqlPolygonPropNullableArray { get; set; } = null!;
			public NpgsqlTid NpgsqlTidProp { get; set; }
			public NpgsqlTid? NpgsqlTidPropNullable { get; set; }
			public NpgsqlTid[] NpgsqlTidPropArraay { get; set; } = null!;
			public NpgsqlTid?[] NpgsqlTidPropNullableArray { get; set; } = null!;
			public NpgsqlRange<byte> NpgsqlRangeByteProp { get; set; }
			public NpgsqlRange<byte>? NpgsqlRangeBytePropNullable { get; set; }

			/* public NpgsqlInet NpgsqlInetProp { get; set; } */
		}

		private class OtherDocumentClass
		{
			public int PropPublicGetPublicSet { get; set; }
			public int PropPublicSet { set { } }
			public int PropPublicGet { get; }
#if NET6_0_OR_GREATER
			public int PropPublicInit { init { } }
			public int PropPublicGetPublicInit { get; init; }
#endif

			private int PropPrivateGetPrivateSet { get; set; }
			private int PropPrivateGet { get; }
			private int PropPrivateSet { set { } }
#if NET6_0_OR_GREATER
			private int PropPrivateInit { init { } }
			private int PropPrivateGetPrivateInit { get; init; }
#endif

			public int PropPublicGetProtectedSet { get; protected set; }
			public int PropProtectedGetPublicSet { protected get; set; }
#if NET6_0_OR_GREATER
			public int PropProtectedGetPublicInit { protected get => 0; init { } }
			public int PropPublicGetProtectedInit { get => 0; protected init { } }
#endif

			public int PublicField;
			private int PrivateField;
			public readonly int PublicReadonlyField;
			protected readonly int ProtectedReadonlyField;
			public static int PublicStaticField;
			public static readonly int PublicStaticReadonlyField;
			public const int PublicConstField = 0;
			protected static int ProtectedStaticField;
			protected static readonly int ProtectedStaticReadonlyField;
			protected const int ProtectedConstField = 0;
		}

		private class InversedDocumentClass
		{
			[NotMapped] public int PropPublicGetPublicSet { get; set; }
			[Column("prop_public_set", Order = 4)] public int PropPublicSet { set { } }
			[Column("prop_public_get", Order = 3)] public int PropPublicGet { get; }
#if NET6_0_OR_GREATER
			[NotMapped] public int PropPublicInit { init { } }
			[NotMapped] public int PropPublicGetPublicInit { get; init; }
#endif

			[Column] private int PropPrivateGetPrivateSet { get; set; }
			[Column] private int PropPrivateGet { get; }
			[Column] private int PropPrivateSet { set { } }
#if NET6_0_OR_GREATER
			[Column] private int PropPrivateInit { init { } }
			[Column] private int PropPrivateGetPrivateInit { get; init; }
#endif

			[Column] public int PropPublicGetProtectedSet { get; protected set; }
			[NotMapped] public int PropProtectedGetPublicSet { protected get; set; }
#if NET6_0_OR_GREATER
			[NotMapped] public int PropProtectedGetPublicInit { protected get => 0; init { } }
			[Column("prop_public_get_protected_init", Order = 1)] public int PropPublicGetProtectedInit { get => 0; protected init { } }
#endif

			[Column("public_field")] public int PublicField;
			[Column(Order = 2)] private int PrivateField;
			[Column] public readonly int PublicReadonlyField;
			[Column] protected readonly int ProtectedReadonlyField;
			[Column] public static int PublicStaticField;
			[Column] public static readonly int PublicStaticReadonlyField;
			[Column] public const int PublicConstField = 0;
			[Column] protected static int ProtectedStaticField;
			[Column] protected static readonly int ProtectedStaticReadonlyField;
			[Column] protected const int ProtectedConstField = 0;
		}

		private struct DocumentStruct
		{

		}
	}
}

#pragma warning restore CA1822 // Mark members as static