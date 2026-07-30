using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an integer value that is either 1 or 0 to be stored in or retrieved from a database.</summary>
	// Token: 0x020002BC RID: 700
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBoolean : INullable, IComparable, IXmlSerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the supplied Boolean value.</summary>
		/// <param name="value">The value for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure; either true or false. </param>
		// Token: 0x06001DED RID: 7661 RVA: 0x00092955 File Offset: 0x00090B55
		public SqlBoolean(bool value)
		{
			this.m_value = (value ? 2 : 1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the specified integer value.</summary>
		/// <param name="value">The integer whose value is to be used for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001DEE RID: 7662 RVA: 0x00092964 File Offset: 0x00090B64
		public SqlBoolean(int value)
		{
			this = new SqlBoolean(value, false);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0009296E File Offset: 0x00090B6E
		private SqlBoolean(int value, bool fNull)
		{
			if (fNull)
			{
				this.m_value = 0;
				return;
			}
			this.m_value = ((value != 0) ? 2 : 1);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null; otherwise false.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00092988 File Offset: 0x00090B88
		public bool IsNull
		{
			get
			{
				return this.m_value == 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value. This property is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />; otherwise false.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is set to null. </exception>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00092994 File Offset: 0x00090B94
		public bool Value
		{
			get
			{
				byte value = this.m_value;
				if (value == 1)
				{
					return false;
				}
				if (value == 2)
				{
					return true;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />.</summary>
		/// <returns>true if Value is True; otherwise, false.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000929B9 File Offset: 0x00090BB9
		public bool IsTrue
		{
			get
			{
				return this.m_value == 2;
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</summary>
		/// <returns>true if Value is False; otherwise, false.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000929C4 File Offset: 0x00090BC4
		public bool IsFalse
		{
			get
			{
				return this.m_value == 1;
			}
		}

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value that contains 0 or 1.</returns>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06001DF4 RID: 7668 RVA: 0x000929CF File Offset: 0x00090BCF
		public static implicit operator SqlBoolean(bool x)
		{
			return new SqlBoolean(x);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to a Boolean.</summary>
		/// <returns>A Boolean set to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to convert. </param>
		// Token: 0x06001DF5 RID: 7669 RVA: 0x000929D7 File Offset: 0x00090BD7
		public static explicit operator bool(SqlBoolean x)
		{
			return x.Value;
		}

		/// <summary>Performs a NOT operation on a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> with the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /><see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if argument was true, <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" /> if argument was null, and <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> on which the NOT operation will be performed. </param>
		// Token: 0x06001DF6 RID: 7670 RVA: 0x000929E0 File Offset: 0x00090BE0
		public static SqlBoolean operator !(SqlBoolean x)
		{
			byte value = x.m_value;
			if (value == 1)
			{
				return SqlBoolean.True;
			}
			if (value == 2)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		/// <summary>The true operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is true.</summary>
		/// <returns>Returns true if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is true, false otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested. </param>
		// Token: 0x06001DF7 RID: 7671 RVA: 0x00092A0D File Offset: 0x00090C0D
		public static bool operator true(SqlBoolean x)
		{
			return x.IsTrue;
		}

		/// <summary>The false operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is false.</summary>
		/// <returns>Returns true if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is false, false otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested. </param>
		// Token: 0x06001DF8 RID: 7672 RVA: 0x00092A16 File Offset: 0x00090C16
		public static bool operator false(SqlBoolean x)
		{
			return x.IsFalse;
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The result of the logical AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001DF9 RID: 7673 RVA: 0x00092A1F File Offset: 0x00090C1F
		public static SqlBoolean operator &(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 1 || y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			if (x.m_value == 2 && y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Computes the bitwise OR of its operands.</summary>
		/// <returns>The results of the logical OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001DFA RID: 7674 RVA: 0x00092A56 File Offset: 0x00090C56
		public static SqlBoolean operator |(SqlBoolean x, SqlBoolean y)
		{
			if (x.m_value == 2 || y.m_value == 2)
			{
				return SqlBoolean.True;
			}
			if (x.m_value == 1 && y.m_value == 1)
			{
				return SqlBoolean.False;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure as a byte.</summary>
		/// <returns>A byte representing the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00092A8D File Offset: 0x00090C8D
		public byte ByteValue
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				if (this.m_value != 2)
				{
					return 0;
				}
				return 1;
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a string.</summary>
		/// <returns>A string that contains the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />. If the value is null, the string will contain "null".</returns>
		// Token: 0x06001DFC RID: 7676 RVA: 0x00092AAC File Offset: 0x00090CAC
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.Value.ToString();
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> representation of a logical value to its <see cref="T:System.Data.SqlTypes.SqlBoolean" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure that contains the parsed value.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to be converted. </param>
		// Token: 0x06001DFD RID: 7677 RVA: 0x00092AD8 File Offset: 0x00090CD8
		public static SqlBoolean Parse(string s)
		{
			if (s == null)
			{
				return new SqlBoolean(bool.Parse(s));
			}
			if (s == SQLResource.NullString)
			{
				return SqlBoolean.Null;
			}
			s = s.TrimStart(Array.Empty<char>());
			char c = s[0];
			if (char.IsNumber(c) || '-' == c || '+' == c)
			{
				return new SqlBoolean(int.Parse(s, null));
			}
			return new SqlBoolean(bool.Parse(s));
		}

		/// <summary>Performs a one's complement operation on the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The one's complement of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001DFE RID: 7678 RVA: 0x00092B46 File Offset: 0x00090D46
		public static SqlBoolean operator ~(SqlBoolean x)
		{
			return !x;
		}

		/// <summary>Performs a bitwise exclusive-OR (XOR) operation on the supplied parameters.</summary>
		/// <returns>The result of the logical XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001DFF RID: 7679 RVA: 0x00092B4E File Offset: 0x00090D4E
		public static SqlBoolean operator ^(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value != y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E00 RID: 7680 RVA: 0x00092B7E File Offset: 0x00090D7E
		public static explicit operator SqlBoolean(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value > 0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E01 RID: 7681 RVA: 0x00092B9E File Offset: 0x00090D9E
		public static explicit operator SqlBoolean(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E02 RID: 7682 RVA: 0x00092BBE File Offset: 0x00090DBE
		public static explicit operator SqlBoolean(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E03 RID: 7683 RVA: 0x00092BDE File Offset: 0x00090DDE
		public static explicit operator SqlBoolean(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0L);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E04 RID: 7684 RVA: 0x00092BFF File Offset: 0x00090DFF
		public static explicit operator SqlBoolean(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E05 RID: 7685 RVA: 0x00092C2A File Offset: 0x00090E2A
		public static explicit operator SqlBoolean(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean((double)x.Value != 0.0);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E06 RID: 7686 RVA: 0x00092C56 File Offset: 0x00090E56
		public static explicit operator SqlBoolean(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return x != SqlMoney.Zero;
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E07 RID: 7687 RVA: 0x00092C72 File Offset: 0x00090E72
		public static explicit operator SqlBoolean(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlBoolean(x._data1 != 0U || x._data2 != 0U || x._data3 != 0U || x._data4 > 0U);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E08 RID: 7688 RVA: 0x00092CAC File Offset: 0x00090EAC
		public static explicit operator SqlBoolean(SqlString x)
		{
			if (!x.IsNull)
			{
				return SqlBoolean.Parse(x.Value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06001E09 RID: 7689 RVA: 0x00092CC9 File Offset: 0x00090EC9
		public static SqlBoolean operator ==(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06001E0A RID: 7690 RVA: 0x00092CF6 File Offset: 0x00090EF6
		public static SqlBoolean operator !=(SqlBoolean x, SqlBoolean y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E0B RID: 7691 RVA: 0x00092D04 File Offset: 0x00090F04
		public static SqlBoolean operator <(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than the second instance; otherwise, false. </returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object. </param>
		// Token: 0x06001E0C RID: 7692 RVA: 0x00092D31 File Offset: 0x00090F31
		public static SqlBoolean operator >(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than or equal to the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E0D RID: 7693 RVA: 0x00092D5E File Offset: 0x00090F5E
		public static SqlBoolean operator <=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than or equal to the second instance; otherwise, false. </returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E0E RID: 7694 RVA: 0x00092D8E File Offset: 0x00090F8E
		public static SqlBoolean operator >=(SqlBoolean x, SqlBoolean y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a one's complement operation on the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The one's complement of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E0F RID: 7695 RVA: 0x00092DBE File Offset: 0x00090FBE
		public static SqlBoolean OnesComplement(SqlBoolean x)
		{
			return ~x;
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The result of the logical AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E10 RID: 7696 RVA: 0x00092DC6 File Offset: 0x00090FC6
		public static SqlBoolean And(SqlBoolean x, SqlBoolean y)
		{
			return x & y;
		}

		/// <summary>Performs a bitwise OR operation on the two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose Value is the result of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E11 RID: 7697 RVA: 0x00092DCF File Offset: 0x00090FCF
		public static SqlBoolean Or(SqlBoolean x, SqlBoolean y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>The result of the logical XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E12 RID: 7698 RVA: 0x00092DD8 File Offset: 0x00090FD8
		public static SqlBoolean Xor(SqlBoolean x, SqlBoolean y)
		{
			return x ^ y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E13 RID: 7699 RVA: 0x00092DE1 File Offset: 0x00090FE1
		public static SqlBoolean Equals(SqlBoolean x, SqlBoolean y)
		{
			return x == y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001E14 RID: 7700 RVA: 0x00092DEA File Offset: 0x00090FEA
		public static SqlBoolean NotEquals(SqlBoolean x, SqlBoolean y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than the second instance; otherwise false. </returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E15 RID: 7701 RVA: 0x00092DF3 File Offset: 0x00090FF3
		public static SqlBoolean GreaterThan(SqlBoolean x, SqlBoolean y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E16 RID: 7702 RVA: 0x00092DFC File Offset: 0x00090FFC
		public static SqlBoolean LessThan(SqlBoolean x, SqlBoolean y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than or equal to the second instance; otherwise false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E17 RID: 7703 RVA: 0x00092E05 File Offset: 0x00091005
		public static SqlBoolean GreaterThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x >= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than or equal to the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06001E18 RID: 7704 RVA: 0x00092E0E File Offset: 0x0009100E
		public static SqlBoolean LessThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x <= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's value is 0.</returns>
		// Token: 0x06001E19 RID: 7705 RVA: 0x00092E17 File Offset: 0x00091017
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value is 0.</returns>
		// Token: 0x06001E1A RID: 7706 RVA: 0x00092E24 File Offset: 0x00091024
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new SqlInt16 structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then the new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure's value is 1. Otherwise, the new SqlInt16 structure's value is 0.</returns>
		// Token: 0x06001E1B RID: 7707 RVA: 0x00092E31 File Offset: 0x00091031
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new SqlInt32 structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure's value is 1. Otherwise, the new SqlInt32 structure's value is 0.</returns>
		// Token: 0x06001E1C RID: 7708 RVA: 0x00092E3E File Offset: 0x0009103E
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new SqlInt64 structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlInt64" />  structure's value is 1. Otherwise, the new SqlInt64 structure's value is 0.</returns>
		// Token: 0x06001E1D RID: 7709 RVA: 0x00092E4B File Offset: 0x0009104B
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is 1. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals false, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is 0. If <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value is neither 1 nor 0, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is <see cref="F:System.Data.SqlTypes.SqlMoney.Null" />.</returns>
		// Token: 0x06001E1E RID: 7710 RVA: 0x00092E58 File Offset: 0x00091058
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value is 0.</returns>
		// Token: 0x06001E1F RID: 7711 RVA: 0x00092E65 File Offset: 0x00091065
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose value is 1 or 0.If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's value is 1; otherwise the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's value is 0.</returns>
		// Token: 0x06001E20 RID: 7712 RVA: 0x00092E72 File Offset: 0x00091072
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then <see cref="T:System.Data.SqlTypes.SqlString" /> structure's value is 1. Otherwise the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure's value is 0.</returns>
		// Token: 0x06001E21 RID: 7713 RVA: 0x00092E7F File Offset: 0x0009107F
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and value.Value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">An object to compare, or a null reference (Nothing in Visual Basic). </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001E22 RID: 7714 RVA: 0x00092E8C File Offset: 0x0009108C
		public int CompareTo(object value)
		{
			if (value is SqlBoolean)
			{
				SqlBoolean sqlBoolean = (SqlBoolean)value;
				return this.CompareTo(sqlBoolean);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlBoolean));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and value.Value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /><see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to compare, or a null reference (Nothing in Visual Basic).  </param>
		// Token: 0x06001E23 RID: 7715 RVA: 0x00092EC8 File Offset: 0x000910C8
		public int CompareTo(SqlBoolean value)
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
				if (this.ByteValue < value.ByteValue)
				{
					return -1;
				}
				if (this.ByteValue > value.ByteValue)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> and the two are equal; otherwise, false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06001E24 RID: 7716 RVA: 0x00092F18 File Offset: 0x00091118
		public override bool Equals(object value)
		{
			if (!(value is SqlBoolean))
			{
				return false;
			}
			SqlBoolean sqlBoolean = (SqlBoolean)value;
			if (sqlBoolean.IsNull || this.IsNull)
			{
				return sqlBoolean.IsNull && this.IsNull;
			}
			return (this == sqlBoolean).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E25 RID: 7717 RVA: 0x00092F70 File Offset: 0x00091170
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06001E26 RID: 7718 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06001E27 RID: 7719 RVA: 0x00092F98 File Offset: 0x00091198
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = 0;
				return;
			}
			this.m_value = (XmlConvert.ToBoolean(reader.ReadElementString()) ? 2 : 1);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06001E28 RID: 7720 RVA: 0x00092FE7 File Offset: 0x000911E7
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString((this.m_value == 2) ? "true" : "false");
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001E29 RID: 7721 RVA: 0x00093027 File Offset: 0x00091227
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040015AA RID: 5546
		private byte m_value;

		// Token: 0x040015AB RID: 5547
		private const byte x_Null = 0;

		// Token: 0x040015AC RID: 5548
		private const byte x_False = 1;

		// Token: 0x040015AD RID: 5549
		private const byte x_True = 2;

		/// <summary>Represents a true value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040015AE RID: 5550
		public static readonly SqlBoolean True = new SqlBoolean(true);

		/// <summary>Represents a false value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040015AF RID: 5551
		public static readonly SqlBoolean False = new SqlBoolean(false);

		/// <summary>Represents <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040015B0 RID: 5552
		public static readonly SqlBoolean Null = new SqlBoolean(0, true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040015B1 RID: 5553
		public static readonly SqlBoolean Zero = new SqlBoolean(0);

		/// <summary>Represents a one value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040015B2 RID: 5554
		public static readonly SqlBoolean One = new SqlBoolean(1);
	}
}
