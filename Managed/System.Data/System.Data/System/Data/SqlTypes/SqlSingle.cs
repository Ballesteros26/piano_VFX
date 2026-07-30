using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a floating point number within the range of -3.40E +38 through 3.40E +38 to be stored in or retrieved from a database.</summary>
	// Token: 0x020002CB RID: 715
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlSingle : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x060020D9 RID: 8409 RVA: 0x0009B7C3 File Offset: 0x000999C3
		private SqlSingle(bool fNull)
		{
			this._fNotNull = false;
			this._value = 0f;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		/// <param name="value">A floating point number which will be used as the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020DA RID: 8410 RVA: 0x0009B7D7 File Offset: 0x000999D7
		public SqlSingle(float value)
		{
			if (!float.IsFinite(value))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this._fNotNull = true;
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure using the supplied double parameter.</summary>
		/// <param name="value">A double value which will be used as the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020DB RID: 8411 RVA: 0x0009B7FA File Offset: 0x000999FA
		public SqlSingle(double value)
		{
			this = new SqlSingle((float)value);
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x0009B804 File Offset: 0x00099A04
		public bool IsNull
		{
			get
			{
				return !this._fNotNull;
			}
		}

		/// <summary>Gets the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. This property is read-only.</summary>
		/// <returns>A floating point value in the range -3.40E+38 through 3.40E+38.</returns>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0009B80F File Offset: 0x00099A0F
		public float Value
		{
			get
			{
				if (this._fNotNull)
				{
					return this._value;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the specified floating point value to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the value of the specified float.</returns>
		/// <param name="x">The float value to be converted to <see cref="T:System.Data.SqlTypes.SqlSingle" />. </param>
		// Token: 0x060020DE RID: 8414 RVA: 0x0009B825 File Offset: 0x00099A25
		public static implicit operator SqlSingle(float x)
		{
			return new SqlSingle(x);
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to float.</summary>
		/// <returns>A float that contains the value of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> value to be converted to float. </param>
		// Token: 0x060020DF RID: 8415 RVA: 0x0009B82D File Offset: 0x00099A2D
		public static explicit operator float(SqlSingle x)
		{
			return x.Value;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A String object representing the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060020E0 RID: 8416 RVA: 0x0009B836 File Offset: 0x00099A36
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this._value.ToString(null);
			}
			return SQLResource.NullString;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> to a <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to be parsed. </param>
		// Token: 0x060020E1 RID: 8417 RVA: 0x0009B852 File Offset: 0x00099A52
		public static SqlSingle Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlSingle.Null;
			}
			return new SqlSingle(float.Parse(s, CultureInfo.InvariantCulture));
		}

		/// <summary>Negates the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the negated value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020E2 RID: 8418 RVA: 0x0009B877 File Offset: 0x00099A77
		public static SqlSingle operator -(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(-x._value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020E3 RID: 8419 RVA: 0x0009B894 File Offset: 0x00099A94
		public static SqlSingle operator +(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x._value + y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020E4 RID: 8420 RVA: 0x0009B8D3 File Offset: 0x00099AD3
		public static SqlSingle operator -(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x._value - y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>Computes the product of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020E5 RID: 8421 RVA: 0x0009B912 File Offset: 0x00099B12
		public static SqlSingle operator *(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			float num = x._value * y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure by the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020E6 RID: 8422 RVA: 0x0009B954 File Offset: 0x00099B54
		public static SqlSingle operator /(SqlSingle x, SqlSingle y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlSingle.Null;
			}
			if (y._value == 0f)
			{
				throw new DivideByZeroException(SQLResource.DivideByZeroMessage);
			}
			float num = x._value / y._value;
			if (float.IsInfinity(num))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlSingle(num);
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x060020E7 RID: 8423 RVA: 0x0009B9B6 File Offset: 0x00099BB6
		public static explicit operator SqlSingle(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.ByteValue);
			}
			return SqlSingle.Null;
		}

		/// <summary>This implicit operator converts the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> to be converted. </param>
		// Token: 0x060020E8 RID: 8424 RVA: 0x0009B9D4 File Offset: 0x00099BD4
		public static implicit operator SqlSingle(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x060020E9 RID: 8425 RVA: 0x0009B9F2 File Offset: 0x00099BF2
		public static implicit operator SqlSingle(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x060020EA RID: 8426 RVA: 0x0009BA10 File Offset: 0x00099C10
		public static implicit operator SqlSingle(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x060020EB RID: 8427 RVA: 0x0009BA2E File Offset: 0x00099C2E
		public static implicit operator SqlSingle(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle((float)x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x060020EC RID: 8428 RVA: 0x0009BA4C File Offset: 0x00099C4C
		public static implicit operator SqlSingle(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x060020ED RID: 8429 RVA: 0x0009BA69 File Offset: 0x00099C69
		public static implicit operator SqlSingle(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.ToDouble());
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to be converted. </param>
		// Token: 0x060020EE RID: 8430 RVA: 0x0009BA86 File Offset: 0x00099C86
		public static explicit operator SqlSingle(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlSingle(x.Value);
			}
			return SqlSingle.Null;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is equal to the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> object to be converted. </param>
		// Token: 0x060020EF RID: 8431 RVA: 0x0009BAA3 File Offset: 0x00099CA3
		public static explicit operator SqlSingle(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlSingle.Null;
			}
			return SqlSingle.Parse(x.Value);
		}

		/// <summary>Performs a logical comparison of the two SqlSingle parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F0 RID: 8432 RVA: 0x0009BAC0 File Offset: 0x00099CC0
		public static SqlBoolean operator ==(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value == y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F1 RID: 8433 RVA: 0x0009BAED File Offset: 0x00099CED
		public static SqlBoolean operator !=(SqlSingle x, SqlSingle y)
		{
			return !(x == y);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F2 RID: 8434 RVA: 0x0009BAFB File Offset: 0x00099CFB
		public static SqlBoolean operator <(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value < y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F3 RID: 8435 RVA: 0x0009BB28 File Offset: 0x00099D28
		public static SqlBoolean operator >(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value > y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F4 RID: 8436 RVA: 0x0009BB55 File Offset: 0x00099D55
		public static SqlBoolean operator <=(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value <= y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures to determine whether the first is greater than or equl to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F5 RID: 8437 RVA: 0x0009BB85 File Offset: 0x00099D85
		public static SqlBoolean operator >=(SqlSingle x, SqlSingle y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value >= y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Computes the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the sum of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F6 RID: 8438 RVA: 0x0009BBB5 File Offset: 0x00099DB5
		public static SqlSingle Add(SqlSingle x, SqlSingle y)
		{
			return x + y;
		}

		/// <summary>Subtracts the second <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure from the first.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F7 RID: 8439 RVA: 0x0009BBBE File Offset: 0x00099DBE
		public static SqlSingle Subtract(SqlSingle x, SqlSingle y)
		{
			return x - y;
		}

		/// <summary>Computes the product of the two specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure that contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F8 RID: 8440 RVA: 0x0009BBC7 File Offset: 0x00099DC7
		public static SqlSingle Multiply(SqlSingle x, SqlSingle y)
		{
			return x * y;
		}

		/// <summary>Divides the first <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure by the second.</summary>
		/// <returns>A SqlInt64 structure that contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020F9 RID: 8441 RVA: 0x0009BBD0 File Offset: 0x00099DD0
		public static SqlSingle Divide(SqlSingle x, SqlSingle y)
		{
			return x / y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlSingle will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020FA RID: 8442 RVA: 0x0009BBD9 File Offset: 0x00099DD9
		public static SqlBoolean Equals(SqlSingle x, SqlSingle y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020FB RID: 8443 RVA: 0x0009BBE2 File Offset: 0x00099DE2
		public static SqlBoolean NotEquals(SqlSingle x, SqlSingle y)
		{
			return x != y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020FC RID: 8444 RVA: 0x0009BBEB File Offset: 0x00099DEB
		public static SqlBoolean LessThan(SqlSingle x, SqlSingle y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020FD RID: 8445 RVA: 0x0009BBF4 File Offset: 0x00099DF4
		public static SqlBoolean GreaterThan(SqlSingle x, SqlSingle y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020FE RID: 8446 RVA: 0x0009BBFD File Offset: 0x00099DFD
		public static SqlBoolean LessThanOrEqual(SqlSingle x, SqlSingle y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlSingle" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. </param>
		// Token: 0x060020FF RID: 8447 RVA: 0x0009BC06 File Offset: 0x00099E06
		public static SqlBoolean GreaterThanOrEqual(SqlSingle x, SqlSingle y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06002100 RID: 8448 RVA: 0x0009BC0F File Offset: 0x00099E0F
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure. If the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's Value is true, the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's Value will be 1. Otherwise, the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's Value will be 0.</returns>
		// Token: 0x06002101 RID: 8449 RVA: 0x0009BC1C File Offset: 0x00099E1C
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new SqlDouble equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06002102 RID: 8450 RVA: 0x0009BC29 File Offset: 0x00099E29
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new SqlInt16 equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06002103 RID: 8451 RVA: 0x0009BC36 File Offset: 0x00099E36
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06002104 RID: 8452 RVA: 0x0009BC43 File Offset: 0x00099E43
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06002105 RID: 8453 RVA: 0x0009BC50 File Offset: 0x00099E50
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06002106 RID: 8454 RVA: 0x0009BC5D File Offset: 0x00099E5D
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new SqlDecimal equal to the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x06002107 RID: 8455 RVA: 0x0009BC6A File Offset: 0x00099E6A
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representing the value of this <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002108 RID: 8456 RVA: 0x0009BC77 File Offset: 0x00099E77
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlSingle" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002109 RID: 8457 RVA: 0x0009BC84 File Offset: 0x00099E84
		public int CompareTo(object value)
		{
			if (value is SqlSingle)
			{
				SqlSingle sqlSingle = (SqlSingle)value;
				return this.CompareTo(sqlSingle);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlSingle));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlSingle" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> to be compared.</param>
		// Token: 0x0600210A RID: 8458 RVA: 0x0009BCC0 File Offset: 0x00099EC0
		public int CompareTo(SqlSingle value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> object.</summary>
		/// <returns>true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlSingle" /> and the two are equal. Otherwise, false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x0600210B RID: 8459 RVA: 0x0009BD18 File Offset: 0x00099F18
		public override bool Equals(object value)
		{
			if (!(value is SqlSingle))
			{
				return false;
			}
			SqlSingle sqlSingle = (SqlSingle)value;
			if (sqlSingle.IsNull || this.IsNull)
			{
				return sqlSingle.IsNull && this.IsNull;
			}
			return (this == sqlSingle).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600210C RID: 8460 RVA: 0x0009BD70 File Offset: 0x00099F70
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
		// Token: 0x0600210D RID: 8461 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x0600210E RID: 8462 RVA: 0x0009BD98 File Offset: 0x00099F98
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._fNotNull = false;
				return;
			}
			this._value = XmlConvert.ToSingle(reader.ReadElementString());
			this._fNotNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x0600210F RID: 8463 RVA: 0x0009BDE8 File Offset: 0x00099FE8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this._value));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06002110 RID: 8464 RVA: 0x0009BE1E File Offset: 0x0009A01E
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("float", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400165A RID: 5722
		private bool _fNotNull;

		// Token: 0x0400165B RID: 5723
		private float _value;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure.</summary>
		// Token: 0x0400165C RID: 5724
		public static readonly SqlSingle Null = new SqlSingle(true);

		/// <summary>Represents the zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> class.</summary>
		// Token: 0x0400165D RID: 5725
		public static readonly SqlSingle Zero = new SqlSingle(0f);

		/// <summary>Represents the minimum value that can be assigned to <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> class.</summary>
		// Token: 0x0400165E RID: 5726
		public static readonly SqlSingle MinValue = new SqlSingle(float.MinValue);

		/// <summary>Represents the maximum value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> class.</summary>
		// Token: 0x0400165F RID: 5727
		public static readonly SqlSingle MaxValue = new SqlSingle(float.MaxValue);
	}
}
