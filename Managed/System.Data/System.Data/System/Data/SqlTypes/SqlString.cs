using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a variable-length stream of characters to be stored in or retrieved from the database. <see cref="T:System.Data.SqlTypes.SqlString" /> has a different underlying data structure from its corresponding .NET Framework <see cref="T:System.String" /> data type.</summary>
	// Token: 0x020002CD RID: 717
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlString : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002112 RID: 8466 RVA: 0x0009BE69 File Offset: 0x0009A069
		private SqlString(bool fNull)
		{
			this.m_value = null;
			this.m_cmpInfo = null;
			this.m_lcid = 0;
			this.m_flag = SqlCompareOptions.None;
			this.m_fNotNull = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> class.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		/// <param name="index">The starting index within the array. </param>
		/// <param name="count">The number of characters from index to copy. </param>
		/// <param name="fUnicode">true if Unicode encoded. Otherwise, false. </param>
		// Token: 0x06002113 RID: 8467 RVA: 0x0009BE90 File Offset: 0x0009A090
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count, bool fUnicode)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				this.m_cmpInfo = null;
				return;
			}
			this.m_fNotNull = true;
			this.m_cmpInfo = null;
			if (fUnicode)
			{
				this.m_value = SqlString.s_unicodeEncoding.GetString(data, index, count);
				return;
			}
			Encoding encoding = Encoding.GetEncoding(new CultureInfo(this.m_lcid).TextInfo.ANSICodePage);
			this.m_value = encoding.GetString(data, index, count);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> class.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		/// <param name="fUnicode">true if Unicode encoded. Otherwise, false. </param>
		// Token: 0x06002114 RID: 8468 RVA: 0x0009BF1E File Offset: 0x0009A11E
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, bool fUnicode)
		{
			this = new SqlString(lcid, compareOptions, data, 0, data.Length, fUnicode);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> class.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		/// <param name="index">The starting index within the array. </param>
		/// <param name="count">The number of characters from index to copy. </param>
		// Token: 0x06002115 RID: 8469 RVA: 0x0009BF2F File Offset: 0x0009A12F
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count)
		{
			this = new SqlString(lcid, compareOptions, data, index, count, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified locale id, compare options, and data.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		// Token: 0x06002116 RID: 8470 RVA: 0x0009BF3F File Offset: 0x0009A13F
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data)
		{
			this = new SqlString(lcid, compareOptions, data, 0, data.Length, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string, locale id, and compare option values.</summary>
		/// <param name="data">The string to store. </param>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		// Token: 0x06002117 RID: 8471 RVA: 0x0009BF4F File Offset: 0x0009A14F
		public SqlString(string data, int lcid, SqlCompareOptions compareOptions)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			this.m_cmpInfo = null;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				return;
			}
			this.m_fNotNull = true;
			this.m_value = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string and locale id values.</summary>
		/// <param name="data">The string to store. </param>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		// Token: 0x06002118 RID: 8472 RVA: 0x0009BF8C File Offset: 0x0009A18C
		public SqlString(string data, int lcid)
		{
			this = new SqlString(data, lcid, SqlString.s_iDefaultFlag);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string.</summary>
		/// <param name="data">The string to store. </param>
		// Token: 0x06002119 RID: 8473 RVA: 0x0009BF9B File Offset: 0x0009A19B
		public SqlString(string data)
		{
			this = new SqlString(data, CultureInfo.CurrentCulture.LCID, SqlString.s_iDefaultFlag);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0009BFB4 File Offset: 0x0009A1B4
		private SqlString(int lcid, SqlCompareOptions compareOptions, string data, CompareInfo cmpInfo)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				this.m_cmpInfo = null;
				return;
			}
			this.m_value = data;
			this.m_cmpInfo = cmpInfo;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlString" /> structure is null.</summary>
		/// <returns>true if <see cref="P:System.Data.SqlTypes.SqlString.Value" /> is <see cref="F:System.Data.SqlTypes.SqlString.Null" />. Otherwise, false.</returns>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0009C004 File Offset: 0x0009A204
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the string that is stored in this <see cref="T:System.Data.SqlTypes.SqlString" /> structure. This property is read-only.</summary>
		/// <returns>The string that is stored.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the string is <see cref="F:System.Data.SqlTypes.SqlString.Null" />. </exception>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0009C00F File Offset: 0x0009A20F
		public string Value
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Specifies the geographical locale and language for the <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>The locale id for the string stored in the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property.</returns>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x0009C025 File Offset: 0x0009A225
		public int LCID
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_lcid;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> structure that represents information about the culture of this <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> structure that describes information about the culture of this SqlString structure including the names of the culture, the writing system, and the calendar used, and also access to culture-specific objects that provide methods for common operations, such as formatting dates and sorting strings.</returns>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x0009C03B File Offset: 0x0009A23B
		public CultureInfo CultureInfo
		{
			get
			{
				if (!this.IsNull)
				{
					return CultureInfo.GetCultureInfo(this.m_lcid);
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0009C056 File Offset: 0x0009A256
		private void SetCompareInfo()
		{
			if (this.m_cmpInfo == null)
			{
				this.m_cmpInfo = CultureInfo.GetCultureInfo(this.m_lcid).CompareInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> object that defines how string comparisons should be performed for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>A CompareInfo object that defines string comparison for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x0009C076 File Offset: 0x0009A276
		public CompareInfo CompareInfo
		{
			get
			{
				if (!this.IsNull)
				{
					this.SetCompareInfo();
					return this.m_cmpInfo;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>A combination of one or more of the <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> enumeration values that represent the way in which this <see cref="T:System.Data.SqlTypes.SqlString" /> should be compared to other <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A value specifying how this <see cref="T:System.Data.SqlTypes.SqlString" /> should be compared to other <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x0009C092 File Offset: 0x0009A292
		public SqlCompareOptions SqlCompareOptions
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_flag;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the <see cref="T:System.String" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the value of the specified String.</returns>
		/// <param name="x">The <see cref="T:System.String" /> to be converted. </param>
		// Token: 0x06002122 RID: 8482 RVA: 0x0009C0A8 File Offset: 0x0009A2A8
		public static implicit operator SqlString(string x)
		{
			return new SqlString(x);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> to a <see cref="T:System.String" /></summary>
		/// <returns>A String, whose contents are the same as the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> to be converted. </param>
		// Token: 0x06002123 RID: 8483 RVA: 0x0009C0B0 File Offset: 0x0009A2B0
		public static explicit operator string(SqlString x)
		{
			return x.Value;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> object to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> with the same value as this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		// Token: 0x06002124 RID: 8484 RVA: 0x0009C0B9 File Offset: 0x0009A2B9
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value;
			}
			return SQLResource.NullString;
		}

		/// <summary>Gets an array of bytes, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in Unicode format.</summary>
		/// <returns>An byte array, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in Unicode format.</returns>
		// Token: 0x06002125 RID: 8485 RVA: 0x0009C0CF File Offset: 0x0009A2CF
		public byte[] GetUnicodeBytes()
		{
			if (this.IsNull)
			{
				return null;
			}
			return SqlString.s_unicodeEncoding.GetBytes(this.m_value);
		}

		/// <summary>Gets an array of bytes, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in ANSI format.</summary>
		/// <returns>An byte array, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in ANSI format.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002126 RID: 8486 RVA: 0x0009C0EB File Offset: 0x0009A2EB
		public byte[] GetNonUnicodeBytes()
		{
			if (this.IsNull)
			{
				return null;
			}
			return Encoding.GetEncoding(new CultureInfo(this.m_lcid).TextInfo.ANSICodePage).GetBytes(this.m_value);
		}

		/// <summary>Concatenates the two specified <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the newly concatenated value representing the contents of the two <see cref="T:System.Data.SqlTypes.SqlString" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x06002127 RID: 8487 RVA: 0x0009C11C File Offset: 0x0009A31C
		public static SqlString operator +(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlString.Null;
			}
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.ConcatDiffCollationMessage);
			}
			return new SqlString(x.m_lcid, x.m_flag, x.m_value + y.m_value, (x.m_cmpInfo == null) ? y.m_cmpInfo : x.m_cmpInfo);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0009C1A0 File Offset: 0x0009A3A0
		private static int StringCompare(SqlString x, SqlString y)
		{
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.CompareDiffCollationMessage);
			}
			x.SetCompareInfo();
			y.SetCompareInfo();
			int num;
			if ((x.m_flag & SqlCompareOptions.BinarySort) != SqlCompareOptions.None)
			{
				num = SqlString.CompareBinary(x, y);
			}
			else if ((x.m_flag & SqlCompareOptions.BinarySort2) != SqlCompareOptions.None)
			{
				num = SqlString.CompareBinary2(x, y);
			}
			else
			{
				string value = x.m_value;
				string value2 = y.m_value;
				int i = value.Length;
				int num2 = value2.Length;
				while (i > 0)
				{
					if (value[i - 1] != ' ')
					{
						break;
					}
					i--;
				}
				while (num2 > 0 && value2[num2 - 1] == ' ')
				{
					num2--;
				}
				CompareOptions compareOptions = SqlString.CompareOptionsFromSqlCompareOptions(x.m_flag);
				num = x.m_cmpInfo.Compare(x.m_value, 0, i, y.m_value, 0, num2, compareOptions);
			}
			return num;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0009C294 File Offset: 0x0009A494
		private static SqlBoolean Compare(SqlString x, SqlString y, EComparison ecExpectedResult)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			int num = SqlString.StringCompare(x, y);
			bool flag;
			switch (ecExpectedResult)
			{
			case EComparison.LT:
				flag = num < 0;
				break;
			case EComparison.LE:
				flag = num <= 0;
				break;
			case EComparison.EQ:
				flag = num == 0;
				break;
			case EComparison.GE:
				flag = num >= 0;
				break;
			case EComparison.GT:
				flag = num > 0;
				break;
			default:
				return SqlBoolean.Null;
			}
			return new SqlBoolean(flag);
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x0600212A RID: 8490 RVA: 0x0009C314 File Offset: 0x0009A514
		public static explicit operator SqlString(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString());
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x0600212B RID: 8491 RVA: 0x0009C344 File Offset: 0x0009A544
		public static explicit operator SqlString(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x0600212C RID: 8492 RVA: 0x0009C378 File Offset: 0x0009A578
		public static explicit operator SqlString(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The SqlInt32 structure to be converted. </param>
		// Token: 0x0600212D RID: 8493 RVA: 0x0009C3AC File Offset: 0x0009A5AC
		public static explicit operator SqlString(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x0600212E RID: 8494 RVA: 0x0009C3E0 File Offset: 0x0009A5E0
		public static explicit operator SqlString(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted. </param>
		// Token: 0x0600212F RID: 8495 RVA: 0x0009C414 File Offset: 0x0009A614
		public static explicit operator SqlString(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x06002130 RID: 8496 RVA: 0x0009C448 File Offset: 0x0009A648
		public static explicit operator SqlString(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the SqlDecimal parameter.</returns>
		/// <param name="x">The SqlDecimal structure to be converted. </param>
		// Token: 0x06002131 RID: 8497 RVA: 0x0009C479 File Offset: 0x0009A679
		public static explicit operator SqlString(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x06002132 RID: 8498 RVA: 0x0009C49C File Offset: 0x0009A69C
		public static explicit operator SqlString(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to be converted. </param>
		// Token: 0x06002133 RID: 8499 RVA: 0x0009C4BF File Offset: 0x0009A6BF
		public static explicit operator SqlString(SqlDateTime x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> whose value is the string representation of the specified <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to be converted. </param>
		// Token: 0x06002134 RID: 8500 RVA: 0x0009C4E2 File Offset: 0x0009A6E2
		public static explicit operator SqlString(SqlGuid x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		/// <summary>Creates a copy of this <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object in which all property values are the same as the original.</returns>
		// Token: 0x06002135 RID: 8501 RVA: 0x0009C505 File Offset: 0x0009A705
		public SqlString Clone()
		{
			if (this.IsNull)
			{
				return new SqlString(true);
			}
			return new SqlString(this.m_value, this.m_lcid, this.m_flag);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002136 RID: 8502 RVA: 0x0009C52D File Offset: 0x0009A72D
		public static SqlBoolean operator ==(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.EQ);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002137 RID: 8503 RVA: 0x0009C537 File Offset: 0x0009A737
		public static SqlBoolean operator !=(SqlString x, SqlString y)
		{
			return !(x == y);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002138 RID: 8504 RVA: 0x0009C545 File Offset: 0x0009A745
		public static SqlBoolean operator <(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.LT);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002139 RID: 8505 RVA: 0x0009C54F File Offset: 0x0009A74F
		public static SqlBoolean operator >(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.GT);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600213A RID: 8506 RVA: 0x0009C559 File Offset: 0x0009A759
		public static SqlBoolean operator <=(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.LE);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600213B RID: 8507 RVA: 0x0009C563 File Offset: 0x0009A763
		public static SqlBoolean operator >=(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.GE);
		}

		/// <summary>Concatenates the two specified <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the newly concatenated value representing the contents of the two <see cref="T:System.Data.SqlTypes.SqlString" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x0600213C RID: 8508 RVA: 0x0009C56D File Offset: 0x0009A76D
		public static SqlString Concat(SqlString x, SqlString y)
		{
			return x + y;
		}

		/// <summary>Concatenates two specified <see cref="T:System.Data.SqlTypes.SqlString" /> values to create a new <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that is the concatenated value of <paramref name="x" /> and <paramref name="y" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x0600213D RID: 8509 RVA: 0x0009C56D File Offset: 0x0009A76D
		public static SqlString Add(SqlString x, SqlString y)
		{
			return x + y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlString will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600213E RID: 8510 RVA: 0x0009C576 File Offset: 0x0009A776
		public static SqlBoolean Equals(SqlString x, SqlString y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600213F RID: 8511 RVA: 0x0009C57F File Offset: 0x0009A77F
		public static SqlBoolean NotEquals(SqlString x, SqlString y)
		{
			return x != y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002140 RID: 8512 RVA: 0x0009C588 File Offset: 0x0009A788
		public static SqlBoolean LessThan(SqlString x, SqlString y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002141 RID: 8513 RVA: 0x0009C591 File Offset: 0x0009A791
		public static SqlBoolean GreaterThan(SqlString x, SqlString y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002142 RID: 8514 RVA: 0x0009C59A File Offset: 0x0009A79A
		public static SqlBoolean LessThanOrEqual(SqlString x, SqlString y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002143 RID: 8515 RVA: 0x0009C5A3 File Offset: 0x0009A7A3
		public static SqlBoolean GreaterThanOrEqual(SqlString x, SqlString y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002144 RID: 8516 RVA: 0x0009C5AC File Offset: 0x0009A7AC
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A new SqlByte structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> equals the number represented by this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002145 RID: 8517 RVA: 0x0009C5B9 File Offset: 0x0009A7B9
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>A new SqlDateTime structure that contains the date value represented by this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002146 RID: 8518 RVA: 0x0009C5C6 File Offset: 0x0009A7C6
		public SqlDateTime ToSqlDateTime()
		{
			return (SqlDateTime)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002147 RID: 8519 RVA: 0x0009C5D3 File Offset: 0x0009A7D3
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />. </summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />. </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002148 RID: 8520 RVA: 0x0009C5E0 File Offset: 0x0009A7E0
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />. </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002149 RID: 8521 RVA: 0x0009C5ED File Offset: 0x0009A7ED
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600214A RID: 8522 RVA: 0x0009C5FA File Offset: 0x0009A7FA
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600214B RID: 8523 RVA: 0x0009C607 File Offset: 0x0009A807
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> that contains the value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		// Token: 0x0600214C RID: 8524 RVA: 0x0009C614 File Offset: 0x0009A814
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />..</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600214D RID: 8525 RVA: 0x0009C621 File Offset: 0x0009A821
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is the Guid represented by this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600214E RID: 8526 RVA: 0x0009C62E File Offset: 0x0009A82E
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0009C63B File Offset: 0x0009A83B
		private static void ValidateSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			if ((compareOptions & SqlString.s_iValidSqlCompareOptionMask) != compareOptions)
			{
				throw new ArgumentOutOfRangeException("compareOptions");
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareOptions" /> enumeration equilvalent of the specified <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> value.</summary>
		/// <returns>A CompareOptions value that corresponds to the SqlCompareOptions for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <param name="compareOptions">A <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> value that describes the comparison options for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002150 RID: 8528 RVA: 0x0009C654 File Offset: 0x0009A854
		public static CompareOptions CompareOptionsFromSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			CompareOptions compareOptions2 = CompareOptions.None;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			if ((compareOptions & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) != SqlCompareOptions.None)
			{
				throw ADP.ArgumentOutOfRange("compareOptions");
			}
			if ((compareOptions & SqlCompareOptions.IgnoreCase) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreCase;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreNonSpace) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreNonSpace;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreKanaType) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreKanaType;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreWidth) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreWidth;
			}
			return compareOptions2;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009C6A4 File Offset: 0x0009A8A4
		private bool FBinarySort()
		{
			return !this.IsNull && (this.m_flag & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) > SqlCompareOptions.None;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0009C6C0 File Offset: 0x0009A8C0
		private static int CompareBinary(SqlString x, SqlString y)
		{
			byte[] bytes = SqlString.s_unicodeEncoding.GetBytes(x.m_value);
			byte[] bytes2 = SqlString.s_unicodeEncoding.GetBytes(y.m_value);
			int num = bytes.Length;
			int num2 = bytes2.Length;
			int num3 = ((num < num2) ? num : num2);
			int i;
			for (i = 0; i < num3; i++)
			{
				if (bytes[i] < bytes2[i])
				{
					return -1;
				}
				if (bytes[i] > bytes2[i])
				{
					return 1;
				}
			}
			i = num3;
			int num4 = 32;
			if (num < num2)
			{
				while (i < num2)
				{
					int num5 = (int)bytes2[i + 1] << (int)(8 + bytes2[i]);
					if (num5 != num4)
					{
						if (num4 <= num5)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i += 2;
					}
				}
			}
			else
			{
				while (i < num)
				{
					int num5 = (int)bytes[i + 1] << (int)(8 + bytes[i]);
					if (num5 != num4)
					{
						if (num5 <= num4)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i += 2;
					}
				}
			}
			return 0;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0009C798 File Offset: 0x0009A998
		private static int CompareBinary2(SqlString x, SqlString y)
		{
			string value = x.m_value;
			string value2 = y.m_value;
			int length = value.Length;
			int length2 = value2.Length;
			int num = ((length < length2) ? length : length2);
			for (int i = 0; i < num; i++)
			{
				if (value[i] < value2[i])
				{
					return -1;
				}
				if (value[i] > value2[i])
				{
					return 1;
				}
			}
			char c = ' ';
			if (length < length2)
			{
				int i = num;
				while (i < length2)
				{
					if (value2[i] != c)
					{
						if (c <= value2[i])
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				int i = num;
				while (i < length)
				{
					if (value[i] != c)
					{
						if (value[i] <= c)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
					}
				}
			}
			return 0;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlString" /> object to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002154 RID: 8532 RVA: 0x0009C86C File Offset: 0x0009AA6C
		public int CompareTo(object value)
		{
			if (value is SqlString)
			{
				SqlString sqlString = (SqlString)value;
				return this.CompareTo(sqlString);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlString));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlString" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlString" /> to be compared.</param>
		// Token: 0x06002155 RID: 8533 RVA: 0x0009C8A8 File Offset: 0x0009AAA8
		public int CompareTo(SqlString value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				int num = SqlString.StringCompare(this, value);
				if (num < 0)
				{
					return -1;
				}
				if (num > 0)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>Equals will return true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlString" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002156 RID: 8534 RVA: 0x0009C8F0 File Offset: 0x0009AAF0
		public override bool Equals(object value)
		{
			if (!(value is SqlString))
			{
				return false;
			}
			SqlString sqlString = (SqlString)value;
			if (sqlString.IsNull || this.IsNull)
			{
				return sqlString.IsNull && this.IsNull;
			}
			return (this == sqlString).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002157 RID: 8535 RVA: 0x0009C948 File Offset: 0x0009AB48
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			byte[] array;
			if (this.FBinarySort())
			{
				array = SqlString.s_unicodeEncoding.GetBytes(this.m_value.TrimEnd(Array.Empty<char>()));
			}
			else
			{
				CompareInfo compareInfo;
				CompareOptions compareOptions;
				try
				{
					this.SetCompareInfo();
					compareInfo = this.m_cmpInfo;
					compareOptions = SqlString.CompareOptionsFromSqlCompareOptions(this.m_flag);
				}
				catch (ArgumentException)
				{
					compareInfo = CultureInfo.InvariantCulture.CompareInfo;
					compareOptions = CompareOptions.None;
				}
				array = compareInfo.GetSortKey(this.m_value.TrimEnd(Array.Empty<char>()), compareOptions).KeyData;
			}
			return SqlBinary.HashByteArray(array, array.Length);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06002158 RID: 8536 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x06002159 RID: 8537 RVA: 0x0009C9E8 File Offset: 0x0009ABE8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = reader.ReadElementString();
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x0600215A RID: 8538 RVA: 0x0009CA33 File Offset: 0x0009AC33
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.m_value);
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x0600215B RID: 8539 RVA: 0x00094B5B File Offset: 0x00092D5B
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001668 RID: 5736
		private string m_value;

		// Token: 0x04001669 RID: 5737
		private CompareInfo m_cmpInfo;

		// Token: 0x0400166A RID: 5738
		private int m_lcid;

		// Token: 0x0400166B RID: 5739
		private SqlCompareOptions m_flag;

		// Token: 0x0400166C RID: 5740
		private bool m_fNotNull;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		// Token: 0x0400166D RID: 5741
		public static readonly SqlString Null = new SqlString(true);

		// Token: 0x0400166E RID: 5742
		internal static readonly UnicodeEncoding s_unicodeEncoding = new UnicodeEncoding();

		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons should ignore case.</summary>
		// Token: 0x0400166F RID: 5743
		public static readonly int IgnoreCase = 1;

		/// <summary>Specifies that the string comparison must ignore the character width. </summary>
		// Token: 0x04001670 RID: 5744
		public static readonly int IgnoreWidth = 16;

		/// <summary>Specifies that the string comparison must ignore non-space combining characters, such as diacritics. </summary>
		// Token: 0x04001671 RID: 5745
		public static readonly int IgnoreNonSpace = 2;

		/// <summary>Specifies that the string comparison must ignore the Kana type. </summary>
		// Token: 0x04001672 RID: 5746
		public static readonly int IgnoreKanaType = 8;

		/// <summary>Specifies that sorts should be based on a characters numeric value instead of its alphabetical value.</summary>
		// Token: 0x04001673 RID: 5747
		public static readonly int BinarySort = 32768;

		/// <summary>Specifies that sorts should be based on a character's numeric value instead of its alphabetical value.</summary>
		// Token: 0x04001674 RID: 5748
		public static readonly int BinarySort2 = 16384;

		// Token: 0x04001675 RID: 5749
		private static readonly SqlCompareOptions s_iDefaultFlag = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04001676 RID: 5750
		private static readonly CompareOptions s_iValidCompareOptionMask = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04001677 RID: 5751
		internal static readonly SqlCompareOptions s_iValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x04001678 RID: 5752
		internal static readonly int s_lcidUSEnglish = 1033;

		// Token: 0x04001679 RID: 5753
		private static readonly int s_lcidBinary = 33280;
	}
}
