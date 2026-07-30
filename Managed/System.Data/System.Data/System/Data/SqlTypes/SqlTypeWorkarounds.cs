using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;

namespace System.Data.SqlTypes
{
	// Token: 0x020002D8 RID: 728
	internal static class SqlTypeWorkarounds
	{
		// Token: 0x060021A1 RID: 8609 RVA: 0x0009D538 File Offset: 0x0009B738
		internal static XmlReader SqlXmlCreateSqlXmlReader(Stream stream, bool closeInput = false, bool async = false)
		{
			XmlReaderSettings xmlReaderSettings = (closeInput ? (async ? SqlTypeWorkarounds.s_defaultXmlReaderSettingsAsyncCloseInput : SqlTypeWorkarounds.s_defaultXmlReaderSettingsCloseInput) : SqlTypeWorkarounds.s_defaultXmlReaderSettings);
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0009D568 File Offset: 0x0009B768
		internal static DateTime SqlDateTimeToDateTime(int daypart, int timepart)
		{
			if (daypart < -53690 || daypart > 2958463 || timepart < 0 || timepart > 25919999)
			{
				throw new OverflowException(SQLResource.DateTimeOverflowMessage);
			}
			long ticks = new DateTime(1900, 1, 1).Ticks;
			long num = (long)daypart * 864000000000L;
			long num2 = (long)((double)timepart / 0.3 + 0.5) * 10000L;
			return new DateTime(ticks + num + num2);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0009D5E8 File Offset: 0x0009B7E8
		internal static SqlMoney SqlMoneyCtor(long value, int ignored)
		{
			SqlTypeWorkarounds.SqlMoneyCaster sqlMoneyCaster = default(SqlTypeWorkarounds.SqlMoneyCaster);
			sqlMoneyCaster.Fake._fNotNull = true;
			sqlMoneyCaster.Fake._value = value;
			return sqlMoneyCaster.Real;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0009D620 File Offset: 0x0009B820
		internal static long SqlMoneyToSqlInternalRepresentation(SqlMoney money)
		{
			SqlTypeWorkarounds.SqlMoneyCaster sqlMoneyCaster = default(SqlTypeWorkarounds.SqlMoneyCaster);
			sqlMoneyCaster.Real = money;
			if (money.IsNull)
			{
				throw new SqlNullValueException();
			}
			return sqlMoneyCaster.Fake._value;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0009D658 File Offset: 0x0009B858
		internal static void SqlDecimalExtractData(SqlDecimal d, out uint data1, out uint data2, out uint data3, out uint data4)
		{
			SqlTypeWorkarounds.SqlDecimalCaster sqlDecimalCaster = new SqlTypeWorkarounds.SqlDecimalCaster
			{
				Real = d
			};
			data1 = sqlDecimalCaster.Fake._data1;
			data2 = sqlDecimalCaster.Fake._data2;
			data3 = sqlDecimalCaster.Fake._data3;
			data4 = sqlDecimalCaster.Fake._data4;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0009D6AC File Offset: 0x0009B8AC
		internal static SqlBinary SqlBinaryCtor(byte[] value, bool ignored)
		{
			SqlTypeWorkarounds.SqlBinaryCaster sqlBinaryCaster = default(SqlTypeWorkarounds.SqlBinaryCaster);
			sqlBinaryCaster.Fake._value = value;
			return sqlBinaryCaster.Real;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0009D6D4 File Offset: 0x0009B8D4
		internal static SqlGuid SqlGuidCtor(byte[] value, bool ignored)
		{
			SqlTypeWorkarounds.SqlGuidCaster sqlGuidCaster = default(SqlTypeWorkarounds.SqlGuidCaster);
			sqlGuidCaster.Fake._value = value;
			return sqlGuidCaster.Real;
		}

		// Token: 0x04001690 RID: 5776
		private static readonly XmlReaderSettings s_defaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x04001691 RID: 5777
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04001692 RID: 5778
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsAsyncCloseInput = new XmlReaderSettings
		{
			Async = true,
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04001693 RID: 5779
		internal const SqlCompareOptions SqlStringValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x020002D9 RID: 729
		private struct SqlMoneyLookalike
		{
			// Token: 0x04001694 RID: 5780
			internal bool _fNotNull;

			// Token: 0x04001695 RID: 5781
			internal long _value;
		}

		// Token: 0x020002DA RID: 730
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlMoneyCaster
		{
			// Token: 0x04001696 RID: 5782
			[FieldOffset(0)]
			internal SqlMoney Real;

			// Token: 0x04001697 RID: 5783
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlMoneyLookalike Fake;
		}

		// Token: 0x020002DB RID: 731
		private struct SqlDecimalLookalike
		{
			// Token: 0x04001698 RID: 5784
			internal byte _bStatus;

			// Token: 0x04001699 RID: 5785
			internal byte _bLen;

			// Token: 0x0400169A RID: 5786
			internal byte _bPrec;

			// Token: 0x0400169B RID: 5787
			internal byte _bScale;

			// Token: 0x0400169C RID: 5788
			internal uint _data1;

			// Token: 0x0400169D RID: 5789
			internal uint _data2;

			// Token: 0x0400169E RID: 5790
			internal uint _data3;

			// Token: 0x0400169F RID: 5791
			internal uint _data4;
		}

		// Token: 0x020002DC RID: 732
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlDecimalCaster
		{
			// Token: 0x040016A0 RID: 5792
			[FieldOffset(0)]
			internal SqlDecimal Real;

			// Token: 0x040016A1 RID: 5793
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlDecimalLookalike Fake;
		}

		// Token: 0x020002DD RID: 733
		private struct SqlBinaryLookalike
		{
			// Token: 0x040016A2 RID: 5794
			internal byte[] _value;
		}

		// Token: 0x020002DE RID: 734
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlBinaryCaster
		{
			// Token: 0x040016A3 RID: 5795
			[FieldOffset(0)]
			internal SqlBinary Real;

			// Token: 0x040016A4 RID: 5796
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlBinaryLookalike Fake;
		}

		// Token: 0x020002DF RID: 735
		private struct SqlGuidLookalike
		{
			// Token: 0x040016A5 RID: 5797
			internal byte[] _value;
		}

		// Token: 0x020002E0 RID: 736
		[StructLayout(LayoutKind.Explicit)]
		private struct SqlGuidCaster
		{
			// Token: 0x040016A6 RID: 5798
			[FieldOffset(0)]
			internal SqlGuid Real;

			// Token: 0x040016A7 RID: 5799
			[FieldOffset(0)]
			internal SqlTypeWorkarounds.SqlGuidLookalike Fake;
		}
	}
}
