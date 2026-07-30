using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a floating-point number within the range of -1.79E +308 through 1.79E +308 to be stored in or retrieved from a database.</summary>
	// Token: 0x020002C5 RID: 709
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDouble : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x000986D7 File Offset: 0x000968D7
		private SqlDouble(bool fNull)
		{
			this.m_fNotNull = false;
			this.m_value = 0.0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure using the supplied double parameter to set the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> property.</summary>
		/// <param name="value">A double whose value will be used for the new <see cref="T:System.Data.SqlTypes.SqlDouble" />. </param>
		// Token: 0x06001F6D RID: 8045 RVA: 0x000986EF File Offset: 0x000968EF
		public SqlDouble(double value)
		{
			if (!double.IsFinite(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this.m_value = value;
			this.m_fNotNull = true;
		}

		/// <summary>Returns a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlDouble" /> instance is null.</summary>
		/// <returns>true if <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is null. Otherwise, false.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x00098712 File Offset: 0x00096912
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. This property is read-only.</summary>
		/// <returns>The value of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0009871D File Offset: 0x0009691D
		public double Value
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

		/// <summary>Converts the supplied double value to a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> with the same value as the specified double parameter.</returns>
		/// <param name="x">The double value to convert. </param>
		// Token: 0x06001F70 RID: 8048 RVA: 0x00098733 File Offset: 0x00096933
		public static implicit operator SqlDouble(double x)
		{
			return new SqlDouble(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to double.</summary>
		/// <returns>A double equivalent to the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F71 RID: 8049 RVA: 0x0009873B File Offset: 0x0009693B
		public static explicit operator double(SqlDouble x)
		{
			return x.Value;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to a string.</summary>
		/// <returns>A string representing the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F72 RID: 8050 RVA: 0x00098744 File Offset: 0x00096944
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its double-precision floating point number equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> that contains the value represented by the String.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06001F73 RID: 8051 RVA: 0x00098760 File Offset: 0x00096960
		public static SqlDouble Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlDouble.Null;
			}
			return new SqlDouble(double.Parse(s, CultureInfo.InvariantCulture));
		}

		/// <summary>Returns the negated value of the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F74 RID: 8052 RVA: 0x00098785 File Offset: 0x00096985
		public static SqlDouble operator -(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(-x.m_value);
			}
			return SqlDouble.Null;
		}

		/// <summary>The addition operator computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</summary>
		/// <returns>The sum of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F75 RID: 8053 RVA: 0x000987A2 File Offset: 0x000969A2
		public static SqlDouble operator +(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			double num = x.m_value + y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		/// <summary>The subtraction operator the second <see cref="T:System.Data.SqlTypes.SqlDouble" /> operand from the first.</summary>
		/// <returns>The results of the subtraction operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F76 RID: 8054 RVA: 0x000987E1 File Offset: 0x000969E1
		public static SqlDouble operator -(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			double num = x.m_value - y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</summary>
		/// <returns>The product of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F77 RID: 8055 RVA: 0x00098820 File Offset: 0x00096A20
		public static SqlDouble operator *(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			double num = x.m_value * y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		/// <summary>The division operator divides the first <see cref="T:System.Data.SqlTypes.SqlDouble" /> operand by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure that contains the results of the division operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F78 RID: 8056 RVA: 0x00098860 File Offset: 0x00096A60
		public static SqlDouble operator /(SqlDouble x, SqlDouble y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDouble.Null;
			}
			if (y.m_value == 0.0)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			double num = x.m_value / y.m_value;
			if (double.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlDouble(num);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is either 0 or 1, depending on the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to be converted. </param>
		// Token: 0x06001F79 RID: 8057 RVA: 0x000988C6 File Offset: 0x00096AC6
		public static explicit operator SqlDouble(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.ByteValue);
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlByte" /> is <see cref="F:System.Data.SqlTypes.SqlByte.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F7A RID: 8058 RVA: 0x000988E4 File Offset: 0x00096AE4
		public static implicit operator SqlDouble(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlInt16" /> is <see cref="F:System.Data.SqlTypes.SqlInt16.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure. </param>
		// Token: 0x06001F7B RID: 8059 RVA: 0x00098902 File Offset: 0x00096B02
		public static implicit operator SqlDouble(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlInt32" /> is <see cref="F:System.Data.SqlTypes.SqlInt32.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure. </param>
		// Token: 0x06001F7C RID: 8060 RVA: 0x00098920 File Offset: 0x00096B20
		public static implicit operator SqlDouble(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlInt64" /> is <see cref="F:System.Data.SqlTypes.SqlInt64.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure. </param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x0009893E File Offset: 0x00096B3E
		public static implicit operator SqlDouble(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlSingle" /> is <see cref="F:System.Data.SqlTypes.SqlSingle.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x06001F7E RID: 8062 RVA: 0x0009895C File Offset: 0x00096B5C
		public static implicit operator SqlDouble(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble((double)x.Value);
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlMoney" /> is <see cref="F:System.Data.SqlTypes.SqlMoney.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x06001F7F RID: 8063 RVA: 0x0009897A File Offset: 0x00096B7A
		public static implicit operator SqlDouble(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(x.ToDouble());
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is <see cref="F:System.Data.SqlTypes.SqlDecimal.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06001F80 RID: 8064 RVA: 0x00098997 File Offset: 0x00096B97
		public static implicit operator SqlDouble(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlDouble(x.ToDouble());
			}
			return SqlDouble.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> whose <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is equal to the value of the number represented by the <see cref="T:System.Data.SqlTypes.SqlString" />. If the <see cref="T:System.Data.SqlTypes.SqlString" /> is <see cref="F:System.Data.SqlTypes.SqlString.Null" />, the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure will be <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object. </param>
		// Token: 0x06001F81 RID: 8065 RVA: 0x000989B4 File Offset: 0x00096BB4
		public static explicit operator SqlDouble(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlDouble.Null;
			}
			return SqlDouble.Parse(x.Value);
		}

		/// <summary>Performs a logical comparison on two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F82 RID: 8066 RVA: 0x000989D1 File Offset: 0x00096BD1
		public static SqlBoolean operator ==(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value == y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F83 RID: 8067 RVA: 0x000989FE File Offset: 0x00096BFE
		public static SqlBoolean operator !=(SqlDouble x, SqlDouble y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F84 RID: 8068 RVA: 0x00098A0C File Offset: 0x00096C0C
		public static SqlBoolean operator <(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value < y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F85 RID: 8069 RVA: 0x00098A39 File Offset: 0x00096C39
		public static SqlBoolean operator >(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value > y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F86 RID: 8070 RVA: 0x00098A66 File Offset: 0x00096C66
		public static SqlBoolean operator <=(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value <= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F87 RID: 8071 RVA: 0x00098A96 File Offset: 0x00096C96
		public static SqlBoolean operator >=(SqlDouble x, SqlDouble y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x.m_value >= y.m_value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>The addition operator computes the sum of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</summary>
		/// <returns>The sum of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F88 RID: 8072 RVA: 0x00098AC6 File Offset: 0x00096CC6
		public static SqlDouble Add(SqlDouble x, SqlDouble y)
		{
			return x + y;
		}

		/// <summary>The subtraction operator the second <see cref="T:System.Data.SqlTypes.SqlDouble" /> operand from the first.</summary>
		/// <returns>The results of the subtraction operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F89 RID: 8073 RVA: 0x00098ACF File Offset: 0x00096CCF
		public static SqlDouble Subtract(SqlDouble x, SqlDouble y)
		{
			return x - y;
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</summary>
		/// <returns>The product of the two <see cref="T:System.Data.SqlTypes.SqlDouble" /> operands.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F8A RID: 8074 RVA: 0x00098AD8 File Offset: 0x00096CD8
		public static SqlDouble Multiply(SqlDouble x, SqlDouble y)
		{
			return x * y;
		}

		/// <summary>The division operator divides the first <see cref="T:System.Data.SqlTypes.SqlDouble" /> operand by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure that contains the results of the division operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F8B RID: 8075 RVA: 0x00098AE1 File Offset: 0x00096CE1
		public static SqlDouble Divide(SqlDouble x, SqlDouble y)
		{
			return x / y;
		}

		/// <summary>Performs a logical comparison on two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F8C RID: 8076 RVA: 0x00098AEA File Offset: 0x00096CEA
		public static SqlBoolean Equals(SqlDouble x, SqlDouble y)
		{
			return x == y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether they are notequal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F8D RID: 8077 RVA: 0x00098AF3 File Offset: 0x00096CF3
		public static SqlBoolean NotEquals(SqlDouble x, SqlDouble y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F8E RID: 8078 RVA: 0x00098AFC File Offset: 0x00096CFC
		public static SqlBoolean LessThan(SqlDouble x, SqlDouble y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F8F RID: 8079 RVA: 0x00098B05 File Offset: 0x00096D05
		public static SqlBoolean GreaterThan(SqlDouble x, SqlDouble y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F90 RID: 8080 RVA: 0x00098B0E File Offset: 0x00096D0E
		public static SqlBoolean LessThanOrEqual(SqlDouble x, SqlDouble y)
		{
			return x <= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDouble" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDouble" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure. </param>
		// Token: 0x06001F91 RID: 8081 RVA: 0x00098B17 File Offset: 0x00096D17
		public static SqlBoolean GreaterThanOrEqual(SqlDouble x, SqlDouble y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A SqlBoolean structure whose <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> is non-zero, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the <see cref="T:System.Data.SqlTypes.SqlDouble" /> is zero and <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" /> if the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure is <see cref="F:System.Data.SqlTypes.SqlDouble.Null" />.</returns>
		// Token: 0x06001F92 RID: 8082 RVA: 0x00098B20 File Offset: 0x00096D20
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A SqlByte structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</returns>
		// Token: 0x06001F93 RID: 8083 RVA: 0x00098B2D File Offset: 0x00096D2D
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure whose Value equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value.</returns>
		// Token: 0x06001F94 RID: 8084 RVA: 0x00098B3A File Offset: 0x00096D3A
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure whose Value equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value.</returns>
		// Token: 0x06001F95 RID: 8085 RVA: 0x00098B47 File Offset: 0x00096D47
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure whose Value equals the integer part of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value.</returns>
		// Token: 0x06001F96 RID: 8086 RVA: 0x00098B54 File Offset: 0x00096D54
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new SqlMoney structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> is equal to the value of this <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		// Token: 0x06001F97 RID: 8087 RVA: 0x00098B61 File Offset: 0x00096D61
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new SqlDecimal structure whose converted value equals the rounded value of this SqlDouble.</returns>
		// Token: 0x06001F98 RID: 8088 RVA: 0x00098B6E File Offset: 0x00096D6E
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new SqlSingle structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		// Token: 0x06001F99 RID: 8089 RVA: 0x00098B7B File Offset: 0x00096D7B
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A SqlString representing the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001F9A RID: 8090 RVA: 0x00098B88 File Offset: 0x00096D88
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDouble" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to compare. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001F9B RID: 8091 RVA: 0x00098B98 File Offset: 0x00096D98
		public int CompareTo(object value)
		{
			if (value is SqlDouble)
			{
				SqlDouble sqlDouble = (SqlDouble)value;
				return this.CompareTo(sqlDouble);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlDouble));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDouble" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> to be compared. </param>
		// Token: 0x06001F9C RID: 8092 RVA: 0x00098BD4 File Offset: 0x00096DD4
		public int CompareTo(SqlDouble value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> object.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06001F9D RID: 8093 RVA: 0x00098C2C File Offset: 0x00096E2C
		public override bool Equals(object value)
		{
			if (!(value is SqlDouble))
			{
				return false;
			}
			SqlDouble sqlDouble = (SqlDouble)value;
			if (sqlDouble.IsNull || this.IsNull)
			{
				return sqlDouble.IsNull && this.IsNull;
			}
			return (this == sqlDouble).Value;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Data.SqlTypes.SqlDouble" /> structre.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001F9E RID: 8094 RVA: 0x00098C84 File Offset: 0x00096E84
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
		// Token: 0x06001F9F RID: 8095 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x00098CAC File Offset: 0x00096EAC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = XmlConvert.ToDouble(reader.ReadElementString());
			this.m_fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x00098CFC File Offset: 0x00096EFC
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
		// Token: 0x06001FA2 RID: 8098 RVA: 0x00098D32 File Offset: 0x00096F32
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400162E RID: 5678
		private bool m_fNotNull;

		// Token: 0x0400162F RID: 5679
		private double m_value;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</summary>
		// Token: 0x04001630 RID: 5680
		public static readonly SqlDouble Null = new SqlDouble(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</summary>
		// Token: 0x04001631 RID: 5681
		public static readonly SqlDouble Zero = new SqlDouble(0.0);

		/// <summary>A constant representing the minimum possible value of <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		// Token: 0x04001632 RID: 5682
		public static readonly SqlDouble MinValue = new SqlDouble(double.MinValue);

		/// <summary>A constant representing the maximum value for a <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure.</summary>
		// Token: 0x04001633 RID: 5683
		public static readonly SqlDouble MaxValue = new SqlDouble(double.MaxValue);
	}
}
