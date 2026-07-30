using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 16-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x020002C7 RID: 711
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt16 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001FCB RID: 8139 RVA: 0x000992AB File Offset: 0x000974AB
		private SqlInt16(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure using the supplied short integer parameter.</summary>
		/// <param name="value">A short integer. </param>
		// Token: 0x06001FCC RID: 8140 RVA: 0x000992BB File Offset: 0x000974BB
		public SqlInt16(short value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false. For more information, see Handling Null Values (ADO.NET).</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x000992CB File Offset: 0x000974CB
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. This property is read-only.</summary>
		/// <returns>A short integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000992D6 File Offset: 0x000974D6
		public short Value
		{
			get
			{
				if (this.m_fNotNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the supplied short integer to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure with the same value as the specified short integer.</returns>
		/// <param name="x">A short integer value. </param>
		// Token: 0x06001FCF RID: 8143 RVA: 0x000992EC File Offset: 0x000974EC
		public static implicit operator SqlInt16(short x)
		{
			return new SqlInt16(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to a short integer.</summary>
		/// <returns>A short integer whose value is the Value of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000992F4 File Offset: 0x000974F4
		public static explicit operator short(SqlInt16 x)
		{
			return x.Value;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> object representing the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001FD1 RID: 8145 RVA: 0x000992FD File Offset: 0x000974FD
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 16-bit signed integer equivalent.</summary>
		/// <returns>A 16-bit signed integer equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06001FD2 RID: 8146 RVA: 0x00099319 File Offset: 0x00097519
		public static SqlInt16 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt16.Null;
			}
			return new SqlInt16(short.Parse(s, null));
		}

		/// <summary>The unary minus operator negates the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD3 RID: 8147 RVA: 0x0009933A File Offset: 0x0009753A
		public static SqlInt16 operator -(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(-x.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>The ~ operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the complement of the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD4 RID: 8148 RVA: 0x00099358 File Offset: 0x00097558
		public static SqlInt16 operator ~(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(~x.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD5 RID: 8149 RVA: 0x00099378 File Offset: 0x00097578
		public static SqlInt16 operator +(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((((num >> 15) ^ (num >> 16)) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD6 RID: 8150 RVA: 0x000993CC File Offset: 0x000975CC
		public static SqlInt16 operator -(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((((num >> 15) ^ (num >> 16)) & 1) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the product of the two parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD7 RID: 8151 RVA: 0x00099420 File Offset: 0x00097620
		public static SqlInt16 operator *(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			int num2 = num & SqlInt16.s_MASKI2;
			if (num2 != 0 && num2 != SqlInt16.s_MASKI2)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD8 RID: 8152 RVA: 0x00099478 File Offset: 0x00097678
		public static SqlInt16 operator /(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by its second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FD9 RID: 8153 RVA: 0x000994E4 File Offset: 0x000976E4
		public static SqlInt16 operator %(SqlInt16 x, SqlInt16 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt16.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -32768 && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16(x.m_value % y.m_value);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise AND.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FDA RID: 8154 RVA: 0x00099550 File Offset: 0x00097750
		public static SqlInt16 operator &(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16(x.m_value & y.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise OR.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FDB RID: 8155 RVA: 0x0009957D File Offset: 0x0009777D
		public static SqlInt16 operator |(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16((short)((ushort)x.m_value | (ushort)y.m_value));
			}
			return SqlInt16.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise XOR.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FDC RID: 8156 RVA: 0x000995AC File Offset: 0x000977AC
		public static SqlInt16 operator ^(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt16(x.m_value ^ y.m_value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06001FDD RID: 8157 RVA: 0x000995D9 File Offset: 0x000977D9
		public static explicit operator SqlInt16(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.ByteValue);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001FDE RID: 8158 RVA: 0x000995F6 File Offset: 0x000977F6
		public static implicit operator SqlInt16(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16((short)x.Value);
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06001FDF RID: 8159 RVA: 0x00099614 File Offset: 0x00097814
		public static explicit operator SqlInt16(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			int value = x.Value;
			if (value > 32767 || value < -32768)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06001FE0 RID: 8160 RVA: 0x0009965C File Offset: 0x0009785C
		public static explicit operator SqlInt16(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			long value = x.Value;
			if (value > 32767L || value < -32768L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06001FE1 RID: 8161 RVA: 0x000996A4 File Offset: 0x000978A4
		public static explicit operator SqlInt16(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			float value = x.Value;
			if (value < -32768f || value > 32767f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001FE2 RID: 8162 RVA: 0x000996EC File Offset: 0x000978EC
		public static explicit operator SqlInt16(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt16.Null;
			}
			double value = x.Value;
			if (value < -32768.0 || value > 32767.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt16((short)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06001FE3 RID: 8163 RVA: 0x0009973A File Offset: 0x0009793A
		public static explicit operator SqlInt16(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(checked((short)x.ToInt32()));
			}
			return SqlInt16.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001FE4 RID: 8164 RVA: 0x00099758 File Offset: 0x00097958
		public static explicit operator SqlInt16(SqlDecimal x)
		{
			return (SqlInt16)((SqlInt32)x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> object to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property is equal to the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> object parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object. </param>
		// Token: 0x06001FE5 RID: 8165 RVA: 0x00099765 File Offset: 0x00097965
		public static explicit operator SqlInt16(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt16(short.Parse(x.Value, null));
			}
			return SqlInt16.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FE6 RID: 8166 RVA: 0x00099788 File Offset: 0x00097988
		public static SqlBoolean operator ==(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FE7 RID: 8167 RVA: 0x000997B5 File Offset: 0x000979B5
		public static SqlBoolean operator !=(SqlInt16 x, SqlInt16 y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FE8 RID: 8168 RVA: 0x000997C3 File Offset: 0x000979C3
		public static SqlBoolean operator <(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FE9 RID: 8169 RVA: 0x000997F0 File Offset: 0x000979F0
		public static SqlBoolean operator >(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FEA RID: 8170 RVA: 0x0009981D File Offset: 0x00097A1D
		public static SqlBoolean operator <=(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FEB RID: 8171 RVA: 0x0009984D File Offset: 0x00097A4D
		public static SqlBoolean operator >=(SqlInt16 x, SqlInt16 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>The ~ operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the complement of the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FEC RID: 8172 RVA: 0x0009987D File Offset: 0x00097A7D
		public static SqlInt16 OnesComplement(SqlInt16 x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FED RID: 8173 RVA: 0x00099885 File Offset: 0x00097A85
		public static SqlInt16 Add(SqlInt16 x, SqlInt16 y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FEE RID: 8174 RVA: 0x0009988E File Offset: 0x00097A8E
		public static SqlInt16 Subtract(SqlInt16 x, SqlInt16 y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the product of the two parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FEF RID: 8175 RVA: 0x00099897 File Offset: 0x00097A97
		public static SqlInt16 Multiply(SqlInt16 x, SqlInt16 y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF0 RID: 8176 RVA: 0x000998A0 File Offset: 0x00097AA0
		public static SqlInt16 Divide(SqlInt16 x, SqlInt16 y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlInt16" /> operand by its second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF1 RID: 8177 RVA: 0x000998A9 File Offset: 0x00097AA9
		public static SqlInt16 Mod(SqlInt16 x, SqlInt16 y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlInt16" /> values and returns the remainder.</summary>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> value.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> value.</param>
		// Token: 0x06001FF2 RID: 8178 RVA: 0x000998A9 File Offset: 0x00097AA9
		public static SqlInt16 Modulus(SqlInt16 x, SqlInt16 y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise AND.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF3 RID: 8179 RVA: 0x000998B2 File Offset: 0x00097AB2
		public static SqlInt16 BitwiseAnd(SqlInt16 x, SqlInt16 y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt16" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property contains the results of the bitwise OR.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF4 RID: 8180 RVA: 0x000998BB File Offset: 0x00097ABB
		public static SqlInt16 BitwiseOr(SqlInt16 x, SqlInt16 y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure that contains the results of the XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x000998C4 File Offset: 0x00097AC4
		public static SqlInt16 Xor(SqlInt16 x, SqlInt16 y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlInt16 will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x000998CD File Offset: 0x00097ACD
		public static SqlBoolean Equals(SqlInt16 x, SqlInt16 y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x000998D6 File Offset: 0x00097AD6
		public static SqlBoolean NotEquals(SqlInt16 x, SqlInt16 y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x000998DF File Offset: 0x00097ADF
		public static SqlBoolean LessThan(SqlInt16 x, SqlInt16 y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlInt16" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x000998E8 File Offset: 0x00097AE8
		public static SqlBoolean GreaterThan(SqlInt16 x, SqlInt16 y)
		{
			return x > y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FFA RID: 8186 RVA: 0x000998F1 File Offset: 0x00097AF1
		public static SqlBoolean LessThanOrEqual(SqlInt16 x, SqlInt16 y)
		{
			return x <= y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlInt16" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001FFB RID: 8187 RVA: 0x000998FA File Offset: 0x00097AFA
		public static SqlBoolean GreaterThanOrEqual(SqlInt16 x, SqlInt16 y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06001FFC RID: 8188 RVA: 0x00099903 File Offset: 0x00097B03
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. If the value of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> is less than 0 or greater than 255, an <see cref="T:System.OverflowException" /> occurs.</returns>
		// Token: 0x06001FFD RID: 8189 RVA: 0x00099910 File Offset: 0x00097B10
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06001FFE RID: 8190 RVA: 0x0009991D File Offset: 0x00097B1D
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06001FFF RID: 8191 RVA: 0x0009992A File Offset: 0x00097B2A
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06002000 RID: 8192 RVA: 0x00099937 File Offset: 0x00097B37
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06002001 RID: 8193 RVA: 0x00099944 File Offset: 0x00097B44
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06002002 RID: 8194 RVA: 0x00099951 File Offset: 0x00097B51
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose Value equals the value of this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</returns>
		// Token: 0x06002003 RID: 8195 RVA: 0x0009995E File Offset: 0x00097B5E
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representing the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of this instance of <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002004 RID: 8196 RVA: 0x0009996B File Offset: 0x00097B6B
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt16" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002005 RID: 8197 RVA: 0x00099978 File Offset: 0x00097B78
		public int CompareTo(object value)
		{
			if (value is SqlInt16)
			{
				SqlInt16 sqlInt = (SqlInt16)value;
				return this.CompareTo(sqlInt);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt16));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt16" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> to be compared.</param>
		// Token: 0x06002006 RID: 8198 RVA: 0x000999B4 File Offset: 0x00097BB4
		public int CompareTo(SqlInt16 value)
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
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the specified object to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt16" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06002007 RID: 8199 RVA: 0x00099A0C File Offset: 0x00097C0C
		public override bool Equals(object value)
		{
			if (!(value is SqlInt16))
			{
				return false;
			}
			SqlInt16 sqlInt = (SqlInt16)value;
			if (sqlInt.IsNull || this.IsNull)
			{
				return sqlInt.IsNull && this.IsNull;
			}
			return (this == sqlInt).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002008 RID: 8200 RVA: 0x00099A64 File Offset: 0x00097C64
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XML schema consumed by .NET Framework.</returns>
		// Token: 0x06002009 RID: 8201 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code. </summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x0600200A RID: 8202 RVA: 0x00099A8C File Offset: 0x00097C8C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt16(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x0600200B RID: 8203 RVA: 0x00099ADC File Offset: 0x00097CDC
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.m_value));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A <see cref="T:System.String" /> value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x0600200C RID: 8204 RVA: 0x00099B12 File Offset: 0x00097D12
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("short", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001638 RID: 5688
		private bool m_fNotNull;

		// Token: 0x04001639 RID: 5689
		private short m_value;

		// Token: 0x0400163A RID: 5690
		private static readonly int s_MASKI2 = -32768;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</summary>
		// Token: 0x0400163B RID: 5691
		public static readonly SqlInt16 Null = new SqlInt16(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure.</summary>
		// Token: 0x0400163C RID: 5692
		public static readonly SqlInt16 Zero = new SqlInt16(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		// Token: 0x0400163D RID: 5693
		public static readonly SqlInt16 MinValue = new SqlInt16(short.MinValue);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		// Token: 0x0400163E RID: 5694
		public static readonly SqlInt16 MaxValue = new SqlInt16(short.MaxValue);
	}
}
