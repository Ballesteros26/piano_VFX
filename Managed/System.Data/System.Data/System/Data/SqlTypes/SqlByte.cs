using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an 8-bit unsigned integer, in the range of 0 through 255, to be stored in or retrieved from a database. </summary>
	// Token: 0x020002BD RID: 701
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlByte : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x00093072 File Offset: 0x00091272
		private SqlByte(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure using the specified byte value.</summary>
		/// <param name="value">A byte value to be stored in the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E2C RID: 7724 RVA: 0x00093082 File Offset: 0x00091282
		public SqlByte(byte value)
		{
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x00093092 File Offset: 0x00091292
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. This property is read-only </summary>
		/// <returns>The value of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0009309D File Offset: 0x0009129D
		public byte Value
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

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the supplied parameter.</returns>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlByte" />. </param>
		// Token: 0x06001E2F RID: 7727 RVA: 0x000930B3 File Offset: 0x000912B3
		public static implicit operator SqlByte(byte x)
		{
			return new SqlByte(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to a byte.</summary>
		/// <returns>A byte whose value equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted to a byte. </param>
		// Token: 0x06001E30 RID: 7728 RVA: 0x000930BB File Offset: 0x000912BB
		public static explicit operator byte(SqlByte x)
		{
			return x.Value;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" />. If the Value is null, the String will be a null string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001E31 RID: 7729 RVA: 0x000930C4 File Offset: 0x000912C4
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its 8-bit unsigned integer equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure that contains the 8-bit number represented by the String parameter.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06001E32 RID: 7730 RVA: 0x000930E0 File Offset: 0x000912E0
		public static SqlByte Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlByte.Null;
			}
			return new SqlByte(byte.Parse(s, null));
		}

		/// <summary>The ones complement operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the ones complement of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E33 RID: 7731 RVA: 0x00093101 File Offset: 0x00091301
		public static SqlByte operator ~(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(~x.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the sum of the two operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E34 RID: 7732 RVA: 0x00093120 File Offset: 0x00091320
		public static SqlByte operator +(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value + y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</summary>
		/// <returns>The results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E35 RID: 7733 RVA: 0x00093170 File Offset: 0x00091370
		public static SqlByte operator -(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value - y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E36 RID: 7734 RVA: 0x000931C0 File Offset: 0x000913C0
		public static SqlByte operator *(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			int num = (int)(x.m_value * y.m_value);
			if ((num & SqlByte.s_iBitNotByteMax) != 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlByte((byte)num);
		}

		/// <summary>Divides its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E37 RID: 7735 RVA: 0x0009320E File Offset: 0x0009140E
		public static SqlByte operator /(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value / y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E38 RID: 7736 RVA: 0x0009324E File Offset: 0x0009144E
		public static SqlByte operator %(SqlByte x, SqlByte y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlByte.Null;
			}
			if (y.m_value != 0)
			{
				return new SqlByte(x.m_value % y.m_value);
			}
			throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>The results of the bitwise AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E39 RID: 7737 RVA: 0x0009328E File Offset: 0x0009148E
		public static SqlByte operator &(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value & y.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>The results of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E3A RID: 7738 RVA: 0x000932BB File Offset: 0x000914BB
		public static SqlByte operator |(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value | y.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>The results of the bitwise XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E3B RID: 7739 RVA: 0x000932E8 File Offset: 0x000914E8
		public static SqlByte operator ^(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlByte(x.m_value ^ y.m_value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to be converted to a <see cref="T:System.Data.SqlTypes.SqlByte" />. </param>
		// Token: 0x06001E3C RID: 7740 RVA: 0x00093315 File Offset: 0x00091515
		public static explicit operator SqlByte(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(x.ByteValue);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">A SqlMoney structure. </param>
		// Token: 0x06001E3D RID: 7741 RVA: 0x00093332 File Offset: 0x00091532
		public static explicit operator SqlByte(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(checked((byte)x.ToInt32()));
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001E3E RID: 7742 RVA: 0x00093350 File Offset: 0x00091550
		public static explicit operator SqlByte(SqlInt16 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255 || x.Value < 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06001E3F RID: 7743 RVA: 0x000933AC File Offset: 0x000915AC
		public static explicit operator SqlByte(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255 || x.Value < 0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the SqlInt64 parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06001E40 RID: 7744 RVA: 0x00093408 File Offset: 0x00091608
		public static explicit operator SqlByte(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255L || x.Value < 0L)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06001E41 RID: 7745 RVA: 0x00093468 File Offset: 0x00091668
		public static explicit operator SqlByte(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255f || x.Value < 0f)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001E42 RID: 7746 RVA: 0x000934C8 File Offset: 0x000916C8
		public static explicit operator SqlByte(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlByte.Null;
			}
			if (x.Value > 255.0 || x.Value < 0.0)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			if (!x.IsNull)
			{
				return new SqlByte((byte)x.Value);
			}
			return SqlByte.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001E43 RID: 7747 RVA: 0x0009352F File Offset: 0x0009172F
		public static explicit operator SqlByte(SqlDecimal x)
		{
			return (SqlByte)((SqlInt32)x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property is equal to the numeric value represented by the <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="x">An instance of the SqlString class. </param>
		// Token: 0x06001E44 RID: 7748 RVA: 0x0009353C File Offset: 0x0009173C
		public static explicit operator SqlByte(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlByte(byte.Parse(x.Value, null));
			}
			return SqlByte.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E45 RID: 7749 RVA: 0x0009355F File Offset: 0x0009175F
		public static SqlBoolean operator ==(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E46 RID: 7750 RVA: 0x0009358C File Offset: 0x0009178C
		public static SqlBoolean operator !=(SqlByte x, SqlByte y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E47 RID: 7751 RVA: 0x0009359A File Offset: 0x0009179A
		public static SqlBoolean operator <(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E48 RID: 7752 RVA: 0x000935C7 File Offset: 0x000917C7
		public static SqlBoolean operator >(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E49 RID: 7753 RVA: 0x000935F4 File Offset: 0x000917F4
		public static SqlBoolean operator <=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the SqlBoolean will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E4A RID: 7754 RVA: 0x00093624 File Offset: 0x00091824
		public static SqlBoolean operator >=(SqlByte x, SqlByte y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>The ones complement operator performs a bitwise one's complement operation on its <see cref="T:System.Data.SqlTypes.SqlByte" /> operand.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the ones complement of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E4B RID: 7755 RVA: 0x00093654 File Offset: 0x00091854
		public static SqlByte OnesComplement(SqlByte x)
		{
			return ~x;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value property contains the results of the addition.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E4C RID: 7756 RVA: 0x0009365C File Offset: 0x0009185C
		public static SqlByte Add(SqlByte x, SqlByte y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</summary>
		/// <returns>The results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlByte" /> operand from the first.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E4D RID: 7757 RVA: 0x00093665 File Offset: 0x00091865
		public static SqlByte Subtract(SqlByte x, SqlByte y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E4E RID: 7758 RVA: 0x0009366E File Offset: 0x0009186E
		public static SqlByte Multiply(SqlByte x, SqlByte y)
		{
			return x * y;
		}

		/// <summary>Divides its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E4F RID: 7759 RVA: 0x00093677 File Offset: 0x00091877
		public static SqlByte Divide(SqlByte x, SqlByte y)
		{
			return x / y;
		}

		/// <summary>Computes the remainder after dividing its first <see cref="T:System.Data.SqlTypes.SqlByte" /> operand by its second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> contains the remainder.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E50 RID: 7760 RVA: 0x00093680 File Offset: 0x00091880
		public static SqlByte Mod(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		/// <summary>Divides two <see cref="T:System.Data.SqlTypes.SqlByte" /> values and returns the remainder.</summary>
		/// <returns>The remainder left after division is performed on <paramref name="x" /> and <paramref name="y" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" />.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" />.</param>
		// Token: 0x06001E51 RID: 7761 RVA: 0x00093680 File Offset: 0x00091880
		public static SqlByte Modulus(SqlByte x, SqlByte y)
		{
			return x % y;
		}

		/// <summary>Computes the bitwise AND of its <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>The results of the bitwise AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E52 RID: 7762 RVA: 0x00093689 File Offset: 0x00091889
		public static SqlByte BitwiseAnd(SqlByte x, SqlByte y)
		{
			return x & y;
		}

		/// <summary>Computes the bitwise OR of its two <see cref="T:System.Data.SqlTypes.SqlByte" /> operands.</summary>
		/// <returns>The results of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E53 RID: 7763 RVA: 0x00093692 File Offset: 0x00091892
		public static SqlByte BitwiseOr(SqlByte x, SqlByte y)
		{
			return x | y;
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>The results of the XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E54 RID: 7764 RVA: 0x0009369B File Offset: 0x0009189B
		public static SqlByte Xor(SqlByte x, SqlByte y)
		{
			return x ^ y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlByte will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E55 RID: 7765 RVA: 0x000936A4 File Offset: 0x000918A4
		public static SqlBoolean Equals(SqlByte x, SqlByte y)
		{
			return x == y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E56 RID: 7766 RVA: 0x000936AD File Offset: 0x000918AD
		public static SqlBoolean NotEquals(SqlByte x, SqlByte y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E57 RID: 7767 RVA: 0x000936B6 File Offset: 0x000918B6
		public static SqlBoolean LessThan(SqlByte x, SqlByte y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E58 RID: 7768 RVA: 0x000936BF File Offset: 0x000918BF
		public static SqlBoolean GreaterThan(SqlByte x, SqlByte y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlByte" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E59 RID: 7769 RVA: 0x000936C8 File Offset: 0x000918C8
		public static SqlBoolean LessThanOrEqual(SqlByte x, SqlByte y)
		{
			return x <= y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlByte" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure. </param>
		// Token: 0x06001E5A RID: 7770 RVA: 0x000936D1 File Offset: 0x000918D1
		public static SqlBoolean GreaterThanOrEqual(SqlByte x, SqlByte y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06001E5B RID: 7771 RVA: 0x000936DA File Offset: 0x000918DA
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A SqlDouble structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x06001E5C RID: 7772 RVA: 0x000936E7 File Offset: 0x000918E7
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A SqlInt16 structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x06001E5D RID: 7773 RVA: 0x000936F4 File Offset: 0x000918F4
		public SqlInt16 ToSqlInt16()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A SqlInt32 structure with the same value as this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x06001E5E RID: 7774 RVA: 0x00093701 File Offset: 0x00091901
		public SqlInt32 ToSqlInt32()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A SqlInt64 structure who <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x06001E5F RID: 7775 RVA: 0x0009370E File Offset: 0x0009190E
		public SqlInt64 ToSqlInt64()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A SqlMoney structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x06001E60 RID: 7776 RVA: 0x0009371B File Offset: 0x0009191B
		public SqlMoney ToSqlMoney()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A SqlDecimal structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x06001E61 RID: 7777 RVA: 0x00093728 File Offset: 0x00091928
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A SqlSingle structure that has the same <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> as this <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</returns>
		// Token: 0x06001E62 RID: 7778 RVA: 0x00093735 File Offset: 0x00091935
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A SqlString that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's <see cref="P:System.Data.SqlTypes.SqlByte.Value" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001E63 RID: 7779 RVA: 0x00093742 File Offset: 0x00091942
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001E64 RID: 7780 RVA: 0x00093750 File Offset: 0x00091950
		public int CompareTo(object value)
		{
			if (value is SqlByte)
			{
				SqlByte sqlByte = (SqlByte)value;
				return this.CompareTo(sqlByte);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlByte));
		}

		/// <summary>Compares this instance to the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlByte" /> object to be compared.</param>
		// Token: 0x06001E65 RID: 7781 RVA: 0x0009378C File Offset: 0x0009198C
		public int CompareTo(SqlByte value)
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

		/// <summary>Compares the supplied <see cref="T:System.Object" /> parameter to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlByte" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		// Token: 0x06001E66 RID: 7782 RVA: 0x000937E4 File Offset: 0x000919E4
		public override bool Equals(object value)
		{
			if (!(value is SqlByte))
			{
				return false;
			}
			SqlByte sqlByte = (SqlByte)value;
			if (sqlByte.IsNull || this.IsNull)
			{
				return sqlByte.IsNull && this.IsNull;
			}
			return (this == sqlByte).Value;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E67 RID: 7783 RVA: 0x0009383C File Offset: 0x00091A3C
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
		// Token: 0x06001E68 RID: 7784 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06001E69 RID: 7785 RVA: 0x00093864 File Offset: 0x00091A64
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToByte(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06001E6A RID: 7786 RVA: 0x000938B4 File Offset: 0x00091AB4
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
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001E6B RID: 7787 RVA: 0x000938EA File Offset: 0x00091AEA
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("unsignedByte", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x040015B3 RID: 5555
		private bool m_fNotNull;

		// Token: 0x040015B4 RID: 5556
		private byte m_value;

		// Token: 0x040015B5 RID: 5557
		private static readonly int s_iBitNotByteMax = -256;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</summary>
		// Token: 0x040015B6 RID: 5558
		public static readonly SqlByte Null = new SqlByte(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure.</summary>
		// Token: 0x040015B7 RID: 5559
		public static readonly SqlByte Zero = new SqlByte(0);

		/// <summary>A constant representing the smallest possible value of a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		// Token: 0x040015B8 RID: 5560
		public static readonly SqlByte MinValue = new SqlByte(0);

		/// <summary>A constant representing the largest possible value of a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		// Token: 0x040015B9 RID: 5561
		public static readonly SqlByte MaxValue = new SqlByte(byte.MaxValue);
	}
}
