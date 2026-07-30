using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a 64-bit signed integer to be stored in or retrieved from a database.</summary>
	// Token: 0x020002C9 RID: 713
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlInt64 : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002052 RID: 8274 RVA: 0x0009A49F File Offset: 0x0009869F
		private SqlInt64(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0L;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure using the supplied long integer.</summary>
		/// <param name="value">A long integer. </param>
		// Token: 0x06002053 RID: 8275 RVA: 0x0009A4B0 File Offset: 0x000986B0
		public SqlInt64(long value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0009A4C0 File Offset: 0x000986C0
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. This property is read-only.</summary>
		/// <returns>A long integer representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</returns>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x0009A4CB File Offset: 0x000986CB
		public long Value
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

		/// <summary>Converts the long parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> equals the value of the long parameter.</returns>
		/// <param name="x">A long integer value. </param>
		// Token: 0x06002056 RID: 8278 RVA: 0x0009A4E1 File Offset: 0x000986E1
		public static implicit operator SqlInt64(long x)
		{
			return new SqlInt64(x);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to long.</summary>
		/// <returns>A new long value equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002057 RID: 8279 RVA: 0x0009A4E9 File Offset: 0x000986E9
		public static explicit operator long(SqlInt64 x)
		{
			return x.Value;
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002058 RID: 8280 RVA: 0x0009A4F2 File Offset: 0x000986F2
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 64-bit signed integer equivalent.</summary>
		/// <returns>A 64-bit signed integer equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to be parsed. </param>
		// Token: 0x06002059 RID: 8281 RVA: 0x0009A50E File Offset: 0x0009870E
		public static SqlInt64 Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlInt64.Null;
			}
			return new SqlInt64(long.Parse(s, null));
		}

		/// <summary>The unary minus operator negates the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the negated <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600205A RID: 8282 RVA: 0x0009A52F File Offset: 0x0009872F
		public static SqlInt64 operator -(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(-x.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the ones complement of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600205B RID: 8283 RVA: 0x0009A54C File Offset: 0x0009874C
		public static SqlInt64 operator ~(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(~x.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600205C RID: 8284 RVA: 0x0009A56C File Offset: 0x0009876C
		public static SqlInt64 operator +(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value + y.m_value;
			if (SqlInt64.SameSignLong(x.m_value, y.m_value) && !SqlInt64.SameSignLong(x.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the results of the subtraction operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600205D RID: 8285 RVA: 0x0009A5D4 File Offset: 0x000987D4
		public static SqlInt64 operator -(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			long num = x.m_value - y.m_value;
			if (!SqlInt64.SameSignLong(x.m_value, y.m_value) && SqlInt64.SameSignLong(y.m_value, num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600205E RID: 8286 RVA: 0x0009A63C File Offset: 0x0009883C
		public static SqlInt64 operator *(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			bool flag = false;
			long num = x.m_value;
			long num2 = y.m_value;
			long num3 = 0L;
			if (num < 0L)
			{
				flag = true;
				num = -num;
			}
			if (num2 < 0L)
			{
				flag = !flag;
				num2 = -num2;
			}
			long num4 = num & SqlInt64.s_lLowIntMask;
			long num5 = (num >> 32) & SqlInt64.s_lLowIntMask;
			long num6 = num2 & SqlInt64.s_lLowIntMask;
			long num7 = (num2 >> 32) & SqlInt64.s_lLowIntMask;
			if (num5 != 0L && num7 != 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			long num8 = num4 * num6;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (num5 != 0L)
			{
				num3 = num5 * num6;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			else if (num7 != 0L)
			{
				num3 = num4 * num7;
				if (num3 < 0L || num3 > 9223372036854775807L)
				{
					throw new OverflowException(SQLResource.ArithOverflowMessage);
				}
			}
			num8 += num3 << 32;
			if (num8 < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (flag)
			{
				num8 = -num8;
			}
			return new SqlInt64(num8);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the results of the division operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600205F RID: 8287 RVA: 0x0009A758 File Offset: 0x00098958
		public static SqlInt64 operator /(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value / y.m_value);
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002060 RID: 8288 RVA: 0x0009A7C8 File Offset: 0x000989C8
		public static SqlInt64 operator %(SqlInt64 x, SqlInt64 y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlInt64.Null;
			}
			if (y.m_value == 0L)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			if (x.m_value == -9223372036854775808L && y.m_value == -1L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64(x.m_value % y.m_value);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002061 RID: 8289 RVA: 0x0009A838 File Offset: 0x00098A38
		public static SqlInt64 operator &(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value & y.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002062 RID: 8290 RVA: 0x0009A864 File Offset: 0x00098A64
		public static SqlInt64 operator |(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value | y.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002063 RID: 8291 RVA: 0x0009A890 File Offset: 0x00098A90
		public static SqlInt64 operator ^(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlInt64(x.m_value ^ y.m_value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x06002064 RID: 8292 RVA: 0x0009A8BC File Offset: 0x00098ABC
		public static explicit operator SqlInt64(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.ByteValue));
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x06002065 RID: 8293 RVA: 0x0009A8DA File Offset: 0x00098ADA
		public static implicit operator SqlInt64(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)((ulong)x.Value));
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06002066 RID: 8294 RVA: 0x0009A8F8 File Offset: 0x00098AF8
		public static implicit operator SqlInt64(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x06002067 RID: 8295 RVA: 0x0009A916 File Offset: 0x00098B16
		public static implicit operator SqlInt64(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64((long)x.Value);
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the integer part of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted. </param>
		// Token: 0x06002068 RID: 8296 RVA: 0x0009A934 File Offset: 0x00098B34
		public static explicit operator SqlInt64(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			float value = x.Value;
			if (value > 9.223372E+18f || value < -9.223372E+18f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64((long)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x06002069 RID: 8297 RVA: 0x0009A97C File Offset: 0x00098B7C
		public static explicit operator SqlInt64(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			double value = x.Value;
			if (value > 9.223372036854776E+18 || value < -9.223372036854776E+18)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlInt64((long)value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x0600206A RID: 8298 RVA: 0x0009A9CA File Offset: 0x00098BCA
		public static explicit operator SqlInt64(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(x.ToInt64());
			}
			return SqlInt64.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the integer part of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x0600206B RID: 8299 RVA: 0x0009A9E8 File Offset: 0x00098BE8
		public static explicit operator SqlInt64(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlInt64.Null;
			}
			SqlDecimal sqlDecimal = x;
			sqlDecimal.AdjustScale((int)(-(int)sqlDecimal._bScale), false);
			if (sqlDecimal._bLen > 2)
			{
				throw new OverflowException(SQLResource.ConversionOverflowMessage);
			}
			long num2;
			if (sqlDecimal._bLen == 2)
			{
				ulong num = SqlDecimal.DWL(sqlDecimal._data1, sqlDecimal._data2);
				if (num > SqlDecimal.s_llMax && (sqlDecimal.IsPositive || num != 1UL + SqlDecimal.s_llMax))
				{
					throw new OverflowException(SQLResource.ConversionOverflowMessage);
				}
				num2 = (long)num;
			}
			else
			{
				num2 = (long)((ulong)sqlDecimal._data1);
			}
			if (!sqlDecimal.IsPositive)
			{
				num2 = -num2;
			}
			return new SqlInt64(num2);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> object to be converted. </param>
		// Token: 0x0600206C RID: 8300 RVA: 0x0009AA89 File Offset: 0x00098C89
		public static explicit operator SqlInt64(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlInt64(long.Parse(x.Value, null));
			}
			return SqlInt64.Null;
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0009AAAC File Offset: 0x00098CAC
		private static bool SameSignLong(long x, long y)
		{
			return ((x ^ y) & long.MinValue) == 0L;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600206E RID: 8302 RVA: 0x0009AABF File Offset: 0x00098CBF
		public static SqlBoolean operator ==(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on the two SqlInt64 parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600206F RID: 8303 RVA: 0x0009AAEC File Offset: 0x00098CEC
		public static SqlBoolean operator !=(SqlInt64 x, SqlInt64 y)
		{
			return !(x == y);
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002070 RID: 8304 RVA: 0x0009AAFA File Offset: 0x00098CFA
		public static SqlBoolean operator <(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002071 RID: 8305 RVA: 0x0009AB27 File Offset: 0x00098D27
		public static SqlBoolean operator >(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002072 RID: 8306 RVA: 0x0009AB54 File Offset: 0x00098D54
		public static SqlBoolean operator <=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002073 RID: 8307 RVA: 0x0009AB84 File Offset: 0x00098D84
		public static SqlBoolean operator >=(SqlInt64 x, SqlInt64 y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operand.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the ones complement of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002074 RID: 8308 RVA: 0x0009ABB4 File Offset: 0x00098DB4
		public static SqlInt64 OnesComplement(SqlInt64 x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the sum of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002075 RID: 8309 RVA: 0x0009ABBC File Offset: 0x00098DBC
		public static SqlInt64 Add(SqlInt64 x, SqlInt64 y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property equals the results of the subtraction operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002076 RID: 8310 RVA: 0x0009ABC5 File Offset: 0x00098DC5
		public static SqlInt64 Subtract(SqlInt64 x, SqlInt64 y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is equal to the product of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002077 RID: 8311 RVA: 0x0009ABCE File Offset: 0x00098DCE
		public static SqlInt64 Multiply(SqlInt64 x, SqlInt64 y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the results of the division operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002078 RID: 8312 RVA: 0x0009ABD7 File Offset: 0x00098DD7
		public static SqlInt64 Divide(SqlInt64 x, SqlInt64 y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing the first <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002079 RID: 8313 RVA: 0x0009ABE0 File Offset: 0x00098DE0
		public static SqlInt64 Mod(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlInt64" /> values and returns the remainder.</summary>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> value.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> value.</param>
		// Token: 0x0600207A RID: 8314 RVA: 0x0009ABE0 File Offset: 0x00098DE0
		public static SqlInt64 Modulus(SqlInt64 x, SqlInt64 y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600207B RID: 8315 RVA: 0x0009ABE9 File Offset: 0x00098DE9
		public static SqlInt64 BitwiseAnd(SqlInt64 x, SqlInt64 y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlInt64" /> operands.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600207C RID: 8316 RVA: 0x0009ABF2 File Offset: 0x00098DF2
		public static SqlInt64 BitwiseOr(SqlInt64 x, SqlInt64 y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure that contains the results of the bitwise XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600207D RID: 8317 RVA: 0x0009ABFB File Offset: 0x00098DFB
		public static SqlInt64 Xor(SqlInt64 x, SqlInt64 y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlInt64 will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600207E RID: 8318 RVA: 0x0009AC04 File Offset: 0x00098E04
		public static SqlBoolean Equals(SqlInt64 x, SqlInt64 y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison on the two SqlInt64 parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x0600207F RID: 8319 RVA: 0x0009AC0D File Offset: 0x00098E0D
		public static SqlBoolean NotEquals(SqlInt64 x, SqlInt64 y)
		{
			return x != y;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002080 RID: 8320 RVA: 0x0009AC16 File Offset: 0x00098E16
		public static SqlBoolean LessThan(SqlInt64 x, SqlInt64 y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002081 RID: 8321 RVA: 0x0009AC1F File Offset: 0x00098E1F
		public static SqlBoolean GreaterThan(SqlInt64 x, SqlInt64 y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison on the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002082 RID: 8322 RVA: 0x0009AC28 File Offset: 0x00098E28
		public static SqlBoolean LessThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06002083 RID: 8323 RVA: 0x0009AC31 File Offset: 0x00098E31
		public static SqlBoolean GreaterThanOrEqual(SqlInt64 x, SqlInt64 y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06002084 RID: 8324 RVA: 0x0009AC3A File Offset: 0x00098E3A
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </returns>
		// Token: 0x06002085 RID: 8325 RVA: 0x0009AC47 File Offset: 0x00098E47
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002086 RID: 8326 RVA: 0x0009AC54 File Offset: 0x00098E54
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002087 RID: 8327 RVA: 0x0009AC61 File Offset: 0x00098E61
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002088 RID: 8328 RVA: 0x0009AC6E File Offset: 0x00098E6E
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x06002089 RID: 8329 RVA: 0x0009AC7B File Offset: 0x00098E7B
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x0600208A RID: 8330 RVA: 0x0009AC88 File Offset: 0x00098E88
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x0600208B RID: 8331 RVA: 0x0009AC95 File Offset: 0x00098E95
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representing the value of this <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600208C RID: 8332 RVA: 0x0009ACA2 File Offset: 0x00098EA2
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt64" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600208D RID: 8333 RVA: 0x0009ACB0 File Offset: 0x00098EB0
		public int CompareTo(object value)
		{
			if (value is SqlInt64)
			{
				SqlInt64 sqlInt = (SqlInt64)value;
				return this.CompareTo(sqlInt);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlInt64));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlInt64" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> to be compared.</param>
		// Token: 0x0600208E RID: 8334 RVA: 0x0009ACEC File Offset: 0x00098EEC
		public int CompareTo(SqlInt64 value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlInt64" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x0600208F RID: 8335 RVA: 0x0009AD44 File Offset: 0x00098F44
		public override bool Equals(object value)
		{
			if (!(value is SqlInt64))
			{
				return false;
			}
			SqlInt64 sqlInt = (SqlInt64)value;
			if (sqlInt.IsNull || this.IsNull)
			{
				return sqlInt.IsNull && this.IsNull;
			}
			return (this == sqlInt).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002090 RID: 8336 RVA: 0x0009AD9C File Offset: 0x00098F9C
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
		// Token: 0x06002091 RID: 8337 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x06002092 RID: 8338 RVA: 0x0009ADC4 File Offset: 0x00098FC4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToInt64(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06002093 RID: 8339 RVA: 0x0009AE14 File Offset: 0x00099014
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
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06002094 RID: 8340 RVA: 0x0009AE4A File Offset: 0x0009904A
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001647 RID: 5703
		private bool m_fNotNull;

		// Token: 0x04001648 RID: 5704
		private long m_value;

		// Token: 0x04001649 RID: 5705
		private static readonly long s_lLowIntMask = (long)((ulong)(-1));

		// Token: 0x0400164A RID: 5706
		private static readonly long s_lHighIntMask = -4294967296L;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x0400164B RID: 5707
		public static readonly SqlInt64 Null = new SqlInt64(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x0400164C RID: 5708
		public static readonly SqlInt64 Zero = new SqlInt64(0L);

		/// <summary>A constant representing the smallest possible value for a <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x0400164D RID: 5709
		public static readonly SqlInt64 MinValue = new SqlInt64(long.MinValue);

		/// <summary>A constant representing the largest possible value for a <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure.</summary>
		// Token: 0x0400164E RID: 5710
		public static readonly SqlInt64 MaxValue = new SqlInt64(long.MaxValue);
	}
}
