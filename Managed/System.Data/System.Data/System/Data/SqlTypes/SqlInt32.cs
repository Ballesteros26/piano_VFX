using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 32-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x020002C8 RID: 712
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt32 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x0600200E RID: 8206 RVA: 0x00099B63 File Offset: 0x00097D63
		private SqlInt32(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure using the supplied integer value.</summary>
		/// <param name="value">The integer to be converted. </param>
		// Token: 0x0600200F RID: 8207 RVA: 0x00099B73 File Offset: 0x00097D73
		public SqlInt32(int value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure is null.</summary>
		/// <returns>This property is true if <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> is null. Otherwise, false.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00099B83 File Offset: 0x00097D83
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. This property is read-only.</summary>
		/// <returns>An integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property contains <see cref="F:System.Data.SqlTypes.SqlInt32.Null" />. </exception>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x00099B8E File Offset: 0x00097D8E
		public int Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return this.m_value;
			}
		}

		/// <summary>Converts the supplied integer to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose Value property is equal to the integer parameter.</returns>
		/// <param name="x">An integer value. </param>
		// Token: 0x06002012 RID: 8210 RVA: 0x00099BA4 File Offset: 0x00097DA4
		public static implicit operator SqlInt32(int x)
		{
			return new SqlInt32(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to an integer.</summary>
		/// <returns>The converted integer value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002013 RID: 8211 RVA: 0x00099BAC File Offset: 0x00097DAC
		public static explicit operator int(SqlInt32 x)
		{
			return x.Value;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002014 RID: 8212 RVA: 0x00099BB5 File Offset: 0x00097DB5
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to be parsed. </param>
		// Token: 0x06002015 RID: 8213 RVA: 0x00099BD1 File Offset: 0x00097DD1
		public static SqlInt32 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt32.Null;
			}
			return new SqlInt32(int.Parse(s, null));
		}

		/// <summary>Negates the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002016 RID: 8214 RVA: 0x00099BF2 File Offset: 0x00097DF2
		public static SqlInt32 operator -(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(-x.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the one's complement operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002017 RID: 8215 RVA: 0x00099C0F File Offset: 0x00097E0F
		public static SqlInt32 operator ~(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(~x.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the sum of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002018 RID: 8216 RVA: 0x00099C2C File Offset: 0x00097E2C
		public static SqlInt32 operator +(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value + y.m_value;
			if (SqlInt32.SameSignInt(x.m_value, y.m_value) && !SqlInt32.SameSignInt(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002019 RID: 8217 RVA: 0x00099C94 File Offset: 0x00097E94
		public static SqlInt32 operator -(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			int num = x.m_value - y.m_value;
			if (!SqlInt32.SameSignInt(x.m_value, y.m_value) && SqlInt32.SameSignInt(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the product of the two parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600201A RID: 8218 RVA: 0x00099CFC File Offset: 0x00097EFC
		public static SqlInt32 operator *(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			long num = (long)x.m_value * (long)y.m_value;
			long num2 = num & SqlInt32.s_lBitNotIntMax;
			if (num2 != 0L && num2 != SqlInt32.s_lBitNotIntMax)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600201B RID: 8219 RVA: 0x00099D58 File Offset: 0x00097F58
		public static SqlInt32 operator /(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.s_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600201C RID: 8220 RVA: 0x00099DC4 File Offset: 0x00097FC4
		public static SqlInt32 operator %(SqlInt32 x, SqlInt32 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt32.Null;
			}
			if (y.m_value == 0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if ((long)x.m_value == SqlInt32.s_iIntMin && y.m_value == -1)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32(x.m_value % y.m_value);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt32" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600201D RID: 8221 RVA: 0x00099E30 File Offset: 0x00098030
		public static SqlInt32 operator &(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value & y.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Computes the bitwise OR of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600201E RID: 8222 RVA: 0x00099E5C File Offset: 0x0009805C
		public static SqlInt32 operator |(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value | y.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600201F RID: 8223 RVA: 0x00099E88 File Offset: 0x00098088
		public static SqlInt32 operator ^(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt32(x.m_value ^ y.m_value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06002020 RID: 8224 RVA: 0x00099EB4 File Offset: 0x000980B4
		public static explicit operator SqlInt32(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.ByteValue);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> property to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06002021 RID: 8225 RVA: 0x00099ED1 File Offset: 0x000980D1
		public static implicit operator SqlInt32(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06002022 RID: 8226 RVA: 0x00099EEE File Offset: 0x000980EE
		public static implicit operator SqlInt32(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32((int)x.Value);
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the SqlInt64 parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002023 RID: 8227 RVA: 0x00099F0C File Offset: 0x0009810C
		public static explicit operator SqlInt32(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			long value = x.Value;
			if (value > 2147483647L || value < -2147483648L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06002024 RID: 8228 RVA: 0x00099F54 File Offset: 0x00098154
		public static explicit operator SqlInt32(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			float value = x.Value;
			if (value > 2.1474836E+09f || value < -2.1474836E+09f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06002025 RID: 8229 RVA: 0x00099F9C File Offset: 0x0009819C
		public static explicit operator SqlInt32(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			double value = x.Value;
			if (value > 2147483647.0 || value < -2147483648.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt32((int)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06002026 RID: 8230 RVA: 0x00099FEA File Offset: 0x000981EA
		public static explicit operator SqlInt32(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(x.ToInt32());
			}
			return SqlInt32.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06002027 RID: 8231 RVA: 0x0009A008 File Offset: 0x00098208
		public static explicit operator SqlInt32(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt32.Null;
			}
			x.AdjustScale((int)(-(int)x.Scale), true);
			long num = (long)((ulong)x._data1);
			if (!x.IsPositive)
			{
				num = -num;
			}
			if (x._bLen > 1 || num > 2147483647L || num < -2147483648L)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			return new SqlInt32((int)num);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> object to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object. </param>
		// Token: 0x06002028 RID: 8232 RVA: 0x0009A075 File Offset: 0x00098275
		public static explicit operator SqlInt32(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt32(int.Parse(x.Value, null));
			}
			return SqlInt32.Null;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0009A098 File Offset: 0x00098298
		private static bool SameSignInt(int x, int y)
		{
			return ((long)(x ^ y) & (long)((ulong)int.MinValue)) == 0L;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600202A RID: 8234 RVA: 0x0009A0A9 File Offset: 0x000982A9
		public static SqlBoolean operator ==(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performa a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600202B RID: 8235 RVA: 0x0009A0D6 File Offset: 0x000982D6
		public static SqlBoolean operator !=(SqlInt32 x, SqlInt32 y)
		{
			return !(x == y);
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600202C RID: 8236 RVA: 0x0009A0E4 File Offset: 0x000982E4
		public static SqlBoolean operator <(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600202D RID: 8237 RVA: 0x0009A111 File Offset: 0x00098311
		public static SqlBoolean operator >(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600202E RID: 8238 RVA: 0x0009A13E File Offset: 0x0009833E
		public static SqlBoolean operator <=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600202F RID: 8239 RVA: 0x0009A16E File Offset: 0x0009836E
		public static SqlBoolean operator >=(SqlInt32 x, SqlInt32 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the one's complement operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002030 RID: 8240 RVA: 0x0009A19E File Offset: 0x0009839E
		public static SqlInt32 OnesComplement(SqlInt32 x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the sum of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002031 RID: 8241 RVA: 0x0009A1A6 File Offset: 0x000983A6
		public static SqlInt32 Add(SqlInt32 x, SqlInt32 y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002032 RID: 8242 RVA: 0x0009A1AF File Offset: 0x000983AF
		public static SqlInt32 Subtract(SqlInt32 x, SqlInt32 y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the product of the two parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002033 RID: 8243 RVA: 0x0009A1B8 File Offset: 0x000983B8
		public static SqlInt32 Multiply(SqlInt32 x, SqlInt32 y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter from the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002034 RID: 8244 RVA: 0x0009A1C1 File Offset: 0x000983C1
		public static SqlInt32 Divide(SqlInt32 x, SqlInt32 y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002035 RID: 8245 RVA: 0x0009A1CA File Offset: 0x000983CA
		public static SqlInt32 Mod(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlInt32" /> values and returns the remainder.</summary>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> value.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> value.</param>
		// Token: 0x06002036 RID: 8246 RVA: 0x0009A1CA File Offset: 0x000983CA
		public static SqlInt32 Modulus(SqlInt32 x, SqlInt32 y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt32" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002037 RID: 8247 RVA: 0x0009A1D3 File Offset: 0x000983D3
		public static SqlInt32 BitwiseAnd(SqlInt32 x, SqlInt32 y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002038 RID: 8248 RVA: 0x0009A1DC File Offset: 0x000983DC
		public static SqlInt32 BitwiseOr(SqlInt32 x, SqlInt32 y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure that contains the results of the bitwise XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06002039 RID: 8249 RVA: 0x0009A1E5 File Offset: 0x000983E5
		public static SqlInt32 Xor(SqlInt32 x, SqlInt32 y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlInt32 will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600203A RID: 8250 RVA: 0x0009A1EE File Offset: 0x000983EE
		public static SqlBoolean Equals(SqlInt32 x, SqlInt32 y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600203B RID: 8251 RVA: 0x0009A1F7 File Offset: 0x000983F7
		public static SqlBoolean NotEquals(SqlInt32 x, SqlInt32 y)
		{
			return x != y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600203C RID: 8252 RVA: 0x0009A200 File Offset: 0x00098400
		public static SqlBoolean LessThan(SqlInt32 x, SqlInt32 y)
		{
			return x < y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600203D RID: 8253 RVA: 0x0009A209 File Offset: 0x00098409
		public static SqlBoolean GreaterThan(SqlInt32 x, SqlInt32 y)
		{
			return x > y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600203E RID: 8254 RVA: 0x0009A212 File Offset: 0x00098412
		public static SqlBoolean LessThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x <= y;
		}

		/// <summary>Compares the two <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x0600203F RID: 8255 RVA: 0x0009A21B File Offset: 0x0009841B
		public static SqlBoolean GreaterThanOrEqual(SqlInt32 x, SqlInt32 y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06002040 RID: 8256 RVA: 0x0009A224 File Offset: 0x00098424
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. If the value of the SqlInt32 is less than 0 or greater than 255, an <see cref="T:System.OverflowException" /> occurs.</returns>
		// Token: 0x06002041 RID: 8257 RVA: 0x0009A231 File Offset: 0x00098431
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002042 RID: 8258 RVA: 0x0009A23E File Offset: 0x0009843E
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002043 RID: 8259 RVA: 0x0009A24B File Offset: 0x0009844B
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002044 RID: 8260 RVA: 0x0009A258 File Offset: 0x00098458
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002045 RID: 8261 RVA: 0x0009A265 File Offset: 0x00098465
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002046 RID: 8262 RVA: 0x0009A272 File Offset: 0x00098472
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x06002047 RID: 8263 RVA: 0x0009A27F File Offset: 0x0009847F
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> structure equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002048 RID: 8264 RVA: 0x0009A28C File Offset: 0x0009848C
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt32" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002049 RID: 8265 RVA: 0x0009A29C File Offset: 0x0009849C
		public int CompareTo(object value)
		{
			if (value is SqlInt32)
			{
				SqlInt32 sqlInt = (SqlInt32)value;
				return this.CompareTo(sqlInt);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt32));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt32" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> to be compared.</param>
		// Token: 0x0600204A RID: 8266 RVA: 0x0009A2D8 File Offset: 0x000984D8
		public int CompareTo(SqlInt32 value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt32" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x0600204B RID: 8267 RVA: 0x0009A330 File Offset: 0x00098530
		public override bool Equals(object value)
		{
			if (!(value is SqlInt32))
			{
				return false;
			}
			SqlInt32 sqlInt = (SqlInt32)value;
			if (sqlInt.IsNull || this.IsNull)
			{
				return sqlInt.IsNull && this.IsNull;
			}
			return (this == sqlInt).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600204C RID: 8268 RVA: 0x0009A388 File Offset: 0x00098588
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
		// Token: 0x0600204D RID: 8269 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x0600204E RID: 8270 RVA: 0x0009A3B0 File Offset: 0x000985B0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt32(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x0600204F RID: 8271 RVA: 0x0009A400 File Offset: 0x00098600
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
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06002050 RID: 8272 RVA: 0x0009A436 File Offset: 0x00098636
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("int", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400163F RID: 5695
		private bool m_fNotNull;

		// Token: 0x04001640 RID: 5696
		private int m_value;

		// Token: 0x04001641 RID: 5697
		private static readonly long s_iIntMin = -2147483648L;

		// Token: 0x04001642 RID: 5698
		private static readonly long s_lBitNotIntMax = -2147483648L;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> class.</summary>
		// Token: 0x04001643 RID: 5699
		public static readonly SqlInt32 Null = new SqlInt32(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure.</summary>
		// Token: 0x04001644 RID: 5700
		public static readonly SqlInt32 Zero = new SqlInt32(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		// Token: 0x04001645 RID: 5701
		public static readonly SqlInt32 MinValue = new SqlInt32(int.MinValue);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		// Token: 0x04001646 RID: 5702
		public static readonly SqlInt32 MaxValue = new SqlInt32(int.MaxValue);
	}
}
