using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200039C RID: 924
	internal class SmiMetaData
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000BFEC9 File Offset: 0x000BE0C9
		internal static SmiMetaData DefaultNVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultNVarChar_NoCollation.MaxLength, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);
			}
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000BFF08 File Offset: 0x000BE108
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions)
			: this(dbType, maxLength, precision, scale, localeId, compareOptions, false, null, null)
		{
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000BFF28 File Offset: 0x000BE128
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
		{
			this.SetDefaultsForType(dbType);
			switch (dbType)
			{
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this._maxLength = maxLength;
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this._maxLength = maxLength;
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Decimal:
				this._precision = precision;
				this._scale = scale;
				this._maxLength = (long)((ulong)SmiMetaData.s_maxLenFromPrecision[(int)(precision - 1)]);
				break;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Udt:
				throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
			case SqlDbType.Structured:
				if (fieldTypes != null)
				{
					this._fieldMetaData = new ReadOnlyCollection<SmiExtendedMetaData>(fieldTypes);
				}
				this._isMultiValued = isMultiValued;
				this._maxLength = (long)this._fieldMetaData.Count;
				break;
			case SqlDbType.Time:
				this._scale = scale;
				this._maxLength = (long)(5 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTime2:
				this._scale = scale;
				this._maxLength = (long)(8 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTimeOffset:
				this._scale = scale;
				this._maxLength = (long)(10 - SmiMetaData.s_maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			}
			if (extendedProperties != null)
			{
				extendedProperties.SetReadOnly();
				this._extendedProperties = extendedProperties;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000C00E0 File Offset: 0x000BE2E0
		internal SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000C00E8 File Offset: 0x000BE2E8
		internal long LocaleId
		{
			get
			{
				return this._localeId;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000C00F0 File Offset: 0x000BE2F0
		internal long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x000C00F8 File Offset: 0x000BE2F8
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x000C0100 File Offset: 0x000BE300
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x000C0108 File Offset: 0x000BE308
		internal SqlDbType SqlDbType
		{
			get
			{
				return this._databaseType;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000C0110 File Offset: 0x000BE310
		internal bool IsMultiValued
		{
			get
			{
				return this._isMultiValued;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000C0118 File Offset: 0x000BE318
		internal IList<SmiExtendedMetaData> FieldMetaData
		{
			get
			{
				return this._fieldMetaData;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000C0120 File Offset: 0x000BE320
		internal SmiMetaDataPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000C0128 File Offset: 0x000BE328
		internal static bool IsSupportedDbType(SqlDbType dbType)
		{
			return (SqlDbType.BigInt <= dbType && SqlDbType.Xml >= dbType) || (SqlDbType.Udt <= dbType && SqlDbType.DateTimeOffset >= dbType);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000C0144 File Offset: 0x000BE344
		internal static SmiMetaData GetDefaultForType(SqlDbType dbType)
		{
			return SmiMetaData.s_defaultValues[(int)dbType];
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000C0150 File Offset: 0x000BE350
		private SmiMetaData(SqlDbType sqlDbType, long maxLength, byte precision, byte scale, SqlCompareOptions compareOptions)
		{
			this._databaseType = sqlDbType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._compareOptions = compareOptions;
			this._localeId = 0L;
			this._isMultiValued = false;
			this._fieldMetaData = SmiMetaData.s_emptyFieldList;
			this._extendedProperties = SmiMetaDataPropertyCollection.EmptyInstance;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000C01B0 File Offset: 0x000BE3B0
		private void SetDefaultsForType(SqlDbType dbType)
		{
			SmiMetaData defaultForType = SmiMetaData.GetDefaultForType(dbType);
			this._databaseType = dbType;
			this._maxLength = defaultForType.MaxLength;
			this._precision = defaultForType.Precision;
			this._scale = defaultForType.Scale;
			this._localeId = defaultForType.LocaleId;
			this._compareOptions = defaultForType.CompareOptions;
			this._isMultiValued = defaultForType._isMultiValued;
			this._fieldMetaData = defaultForType._fieldMetaData;
			this._extendedProperties = defaultForType._extendedProperties;
		}

		// Token: 0x04001A60 RID: 6752
		private SqlDbType _databaseType;

		// Token: 0x04001A61 RID: 6753
		private long _maxLength;

		// Token: 0x04001A62 RID: 6754
		private byte _precision;

		// Token: 0x04001A63 RID: 6755
		private byte _scale;

		// Token: 0x04001A64 RID: 6756
		private long _localeId;

		// Token: 0x04001A65 RID: 6757
		private SqlCompareOptions _compareOptions;

		// Token: 0x04001A66 RID: 6758
		private bool _isMultiValued;

		// Token: 0x04001A67 RID: 6759
		private IList<SmiExtendedMetaData> _fieldMetaData;

		// Token: 0x04001A68 RID: 6760
		private SmiMetaDataPropertyCollection _extendedProperties;

		// Token: 0x04001A69 RID: 6761
		internal const long UnlimitedMaxLengthIndicator = -1L;

		// Token: 0x04001A6A RID: 6762
		internal const long MaxUnicodeCharacters = 4000L;

		// Token: 0x04001A6B RID: 6763
		internal const long MaxANSICharacters = 8000L;

		// Token: 0x04001A6C RID: 6764
		internal const long MaxBinaryLength = 8000L;

		// Token: 0x04001A6D RID: 6765
		internal const int MinPrecision = 1;

		// Token: 0x04001A6E RID: 6766
		internal const int MinScale = 0;

		// Token: 0x04001A6F RID: 6767
		internal const int MaxTimeScale = 7;

		// Token: 0x04001A70 RID: 6768
		internal static readonly DateTime MaxSmallDateTime = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04001A71 RID: 6769
		internal static readonly DateTime MinSmallDateTime = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04001A72 RID: 6770
		internal static readonly SqlMoney MaxSmallMoney = new SqlMoney(214748.3647m);

		// Token: 0x04001A73 RID: 6771
		internal static readonly SqlMoney MinSmallMoney = new SqlMoney(-214748.3648m);

		// Token: 0x04001A74 RID: 6772
		internal const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04001A75 RID: 6773
		internal const long MaxNameLength = 128L;

		// Token: 0x04001A76 RID: 6774
		private static readonly IList<SmiExtendedMetaData> s_emptyFieldList = new SmiExtendedMetaData[0];

		// Token: 0x04001A77 RID: 6775
		private static byte[] s_maxLenFromPrecision = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
			13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
			17, 17, 17, 17, 17, 17, 17, 17
		};

		// Token: 0x04001A78 RID: 6776
		private static byte[] s_maxVarTimeLenOffsetFromScale = new byte[] { 2, 2, 2, 1, 1, 0, 0, 0 };

		// Token: 0x04001A79 RID: 6777
		internal static readonly SmiMetaData DefaultBigInt = new SmiMetaData(SqlDbType.BigInt, 8L, 19, 0, SqlCompareOptions.None);

		// Token: 0x04001A7A RID: 6778
		internal static readonly SmiMetaData DefaultBinary = new SmiMetaData(SqlDbType.Binary, 1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A7B RID: 6779
		internal static readonly SmiMetaData DefaultBit = new SmiMetaData(SqlDbType.Bit, 1L, 1, 0, SqlCompareOptions.None);

		// Token: 0x04001A7C RID: 6780
		internal static readonly SmiMetaData DefaultChar_NoCollation = new SmiMetaData(SqlDbType.Char, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A7D RID: 6781
		internal static readonly SmiMetaData DefaultDateTime = new SmiMetaData(SqlDbType.DateTime, 8L, 23, 3, SqlCompareOptions.None);

		// Token: 0x04001A7E RID: 6782
		internal static readonly SmiMetaData DefaultDecimal = new SmiMetaData(SqlDbType.Decimal, 9L, 18, 0, SqlCompareOptions.None);

		// Token: 0x04001A7F RID: 6783
		internal static readonly SmiMetaData DefaultFloat = new SmiMetaData(SqlDbType.Float, 8L, 53, 0, SqlCompareOptions.None);

		// Token: 0x04001A80 RID: 6784
		internal static readonly SmiMetaData DefaultImage = new SmiMetaData(SqlDbType.Image, -1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A81 RID: 6785
		internal static readonly SmiMetaData DefaultInt = new SmiMetaData(SqlDbType.Int, 4L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04001A82 RID: 6786
		internal static readonly SmiMetaData DefaultMoney = new SmiMetaData(SqlDbType.Money, 8L, 19, 4, SqlCompareOptions.None);

		// Token: 0x04001A83 RID: 6787
		internal static readonly SmiMetaData DefaultNChar_NoCollation = new SmiMetaData(SqlDbType.NChar, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A84 RID: 6788
		internal static readonly SmiMetaData DefaultNText_NoCollation = new SmiMetaData(SqlDbType.NText, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A85 RID: 6789
		internal static readonly SmiMetaData DefaultNVarChar_NoCollation = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A86 RID: 6790
		internal static readonly SmiMetaData DefaultReal = new SmiMetaData(SqlDbType.Real, 4L, 24, 0, SqlCompareOptions.None);

		// Token: 0x04001A87 RID: 6791
		internal static readonly SmiMetaData DefaultUniqueIdentifier = new SmiMetaData(SqlDbType.UniqueIdentifier, 16L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A88 RID: 6792
		internal static readonly SmiMetaData DefaultSmallDateTime = new SmiMetaData(SqlDbType.SmallDateTime, 4L, 16, 0, SqlCompareOptions.None);

		// Token: 0x04001A89 RID: 6793
		internal static readonly SmiMetaData DefaultSmallInt = new SmiMetaData(SqlDbType.SmallInt, 2L, 5, 0, SqlCompareOptions.None);

		// Token: 0x04001A8A RID: 6794
		internal static readonly SmiMetaData DefaultSmallMoney = new SmiMetaData(SqlDbType.SmallMoney, 4L, 10, 4, SqlCompareOptions.None);

		// Token: 0x04001A8B RID: 6795
		internal static readonly SmiMetaData DefaultText_NoCollation = new SmiMetaData(SqlDbType.Text, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A8C RID: 6796
		internal static readonly SmiMetaData DefaultTimestamp = new SmiMetaData(SqlDbType.Timestamp, 8L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A8D RID: 6797
		internal static readonly SmiMetaData DefaultTinyInt = new SmiMetaData(SqlDbType.TinyInt, 1L, 3, 0, SqlCompareOptions.None);

		// Token: 0x04001A8E RID: 6798
		internal static readonly SmiMetaData DefaultVarBinary = new SmiMetaData(SqlDbType.VarBinary, 8000L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A8F RID: 6799
		internal static readonly SmiMetaData DefaultVarChar_NoCollation = new SmiMetaData(SqlDbType.VarChar, 8000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A90 RID: 6800
		internal static readonly SmiMetaData DefaultVariant = new SmiMetaData(SqlDbType.Variant, 8016L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A91 RID: 6801
		internal static readonly SmiMetaData DefaultXml = new SmiMetaData(SqlDbType.Xml, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04001A92 RID: 6802
		internal static readonly SmiMetaData DefaultStructured = new SmiMetaData(SqlDbType.Structured, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04001A93 RID: 6803
		internal static readonly SmiMetaData DefaultDate = new SmiMetaData(SqlDbType.Date, 3L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04001A94 RID: 6804
		internal static readonly SmiMetaData DefaultTime = new SmiMetaData(SqlDbType.Time, 5L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04001A95 RID: 6805
		internal static readonly SmiMetaData DefaultDateTime2 = new SmiMetaData(SqlDbType.DateTime2, 8L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04001A96 RID: 6806
		internal static readonly SmiMetaData DefaultDateTimeOffset = new SmiMetaData(SqlDbType.DateTimeOffset, 10L, 0, 7, SqlCompareOptions.None);

		// Token: 0x04001A97 RID: 6807
		private static SmiMetaData[] s_defaultValues = new SmiMetaData[]
		{
			SmiMetaData.DefaultBigInt,
			SmiMetaData.DefaultBinary,
			SmiMetaData.DefaultBit,
			SmiMetaData.DefaultChar_NoCollation,
			SmiMetaData.DefaultDateTime,
			SmiMetaData.DefaultDecimal,
			SmiMetaData.DefaultFloat,
			SmiMetaData.DefaultImage,
			SmiMetaData.DefaultInt,
			SmiMetaData.DefaultMoney,
			SmiMetaData.DefaultNChar_NoCollation,
			SmiMetaData.DefaultNText_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultReal,
			SmiMetaData.DefaultUniqueIdentifier,
			SmiMetaData.DefaultSmallDateTime,
			SmiMetaData.DefaultSmallInt,
			SmiMetaData.DefaultSmallMoney,
			SmiMetaData.DefaultText_NoCollation,
			SmiMetaData.DefaultTimestamp,
			SmiMetaData.DefaultTinyInt,
			SmiMetaData.DefaultVarBinary,
			SmiMetaData.DefaultVarChar_NoCollation,
			SmiMetaData.DefaultVariant,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultXml,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			null,
			SmiMetaData.DefaultStructured,
			SmiMetaData.DefaultDate,
			SmiMetaData.DefaultTime,
			SmiMetaData.DefaultDateTime2,
			SmiMetaData.DefaultDateTimeOffset
		};
	}
}
