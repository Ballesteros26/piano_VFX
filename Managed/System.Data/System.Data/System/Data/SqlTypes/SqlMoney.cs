using System;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of currency unit to be stored in or retrieved from a database.</summary>
	// Token: 0x020002CA RID: 714
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlMoney : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002096 RID: 8342 RVA: 0x0009AEBB File Offset: 0x000990BB
		private SqlMoney(bool fNull)
		{
			this._fNotNull = false;
			this._value = 0L;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0009AECC File Offset: 0x000990CC
		internal SqlMoney(long value, int ignored)
		{
			this._value = value;
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with the specified integer value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x06002098 RID: 8344 RVA: 0x0009AEDC File Offset: 0x000990DC
		public SqlMoney(int value)
		{
			this._value = (long)value * SqlMoney.s_lTickBase;
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with the specified long integer value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x06002099 RID: 8345 RVA: 0x0009AEF3 File Offset: 0x000990F3
		public SqlMoney(long value)
		{
			if (value < SqlMoney.s_minLong || value > SqlMoney.s_maxLong)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this._value = value * SqlMoney.s_lTickBase;
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with the specified <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x0600209A RID: 8346 RVA: 0x0009AF24 File Offset: 0x00099124
		public SqlMoney(decimal value)
		{
			SqlDecimal sqlDecimal = new SqlDecimal(value);
			sqlDecimal.AdjustScale(SqlMoney.s_iMoneyScale - (int)sqlDecimal.Scale, true);
			if (sqlDecimal._data3 != 0U || sqlDecimal._data4 != 0U)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			bool isPositive = sqlDecimal.IsPositive;
			ulong num = (ulong)sqlDecimal._data1 + ((ulong)sqlDecimal._data2 << 32);
			if ((isPositive && num > 9223372036854775807UL) || (!isPositive && num > 9223372036854775808UL))
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			this._value = (long)(isPositive ? num : (-(long)num));
			this._fNotNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class with specified double value.</summary>
		/// <param name="value">The monetary value to initialize. </param>
		// Token: 0x0600209B RID: 8347 RVA: 0x0009AFC2 File Offset: 0x000991C2
		public SqlMoney(double value)
		{
			this = new SqlMoney(new decimal(value));
		}

		/// <summary>Returns a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x0009AFD0 File Offset: 0x000991D0
		public bool IsNull
		{
			get
			{
				return !this._fNotNull;
			}
		}

		/// <summary>Gets the monetary value of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. This property is read-only.</summary>
		/// <returns>The monetary value of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is set to null. </exception>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0009AFDB File Offset: 0x000991DB
		public decimal Value
		{
			get
			{
				if (this._fNotNull)
				{
					return this.ToDecimal();
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Converts the Value of this instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> as a <see cref="T:System.Decimal" /> structure.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		// Token: 0x0600209E RID: 8350 RVA: 0x0009AFF4 File Offset: 0x000991F4
		public decimal ToDecimal()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			bool flag = false;
			long num = this._value;
			if (this._value < 0L)
			{
				flag = true;
				num = -this._value;
			}
			return new decimal((int)num, (int)(num >> 32), 0, flag, (byte)SqlMoney.s_iMoneyScale);
		}

		/// <summary>Converts the Value of this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to an <see cref="T:System.Int64" />.</summary>
		/// <returns>A 64-bit integer whose value equals the integer part of this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		// Token: 0x0600209F RID: 8351 RVA: 0x0009B040 File Offset: 0x00099240
		public long ToInt64()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			long num = this._value / (SqlMoney.s_lTickBase / 10L);
			bool flag = num >= 0L;
			long num2 = num % 10L;
			num /= 10L;
			if (num2 >= 5L)
			{
				if (flag)
				{
					num += 1L;
				}
				else
				{
					num -= 1L;
				}
			}
			return num;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0009B096 File Offset: 0x00099296
		internal long ToSqlInternalRepresentation()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			return this._value;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to an <see cref="T:System.Int32" />.</summary>
		/// <returns>A 32-bit integer whose value equals the integer part of this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		// Token: 0x060020A1 RID: 8353 RVA: 0x0009B0AC File Offset: 0x000992AC
		public int ToInt32()
		{
			return checked((int)this.ToInt64());
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to a <see cref="T:System.Double" />.</summary>
		/// <returns>A double with a value equal to this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure.</returns>
		// Token: 0x060020A2 RID: 8354 RVA: 0x0009B0B5 File Offset: 0x000992B5
		public double ToDouble()
		{
			return decimal.ToDouble(this.ToDecimal());
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> equals the value of the <see cref="T:System.Decimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Decimal" /> value to be converted. </param>
		// Token: 0x060020A3 RID: 8355 RVA: 0x0009B0C2 File Offset: 0x000992C2
		public static implicit operator SqlMoney(decimal x)
		{
			return new SqlMoney(x);
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x060020A4 RID: 8356 RVA: 0x0009B0CA File Offset: 0x000992CA
		public static explicit operator SqlMoney(double x)
		{
			return new SqlMoney(x);
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Int64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property is equal to the value of the <see cref="T:System.Int64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Int64" /> structure to be converted. </param>
		// Token: 0x060020A5 RID: 8357 RVA: 0x0009B0D2 File Offset: 0x000992D2
		public static implicit operator SqlMoney(long x)
		{
			return new SqlMoney(new decimal(x));
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Decimal" />.</summary>
		/// <returns>A new <see cref="T:System.Decimal" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020A6 RID: 8358 RVA: 0x0009B0DF File Offset: 0x000992DF
		public static explicit operator decimal(SqlMoney x)
		{
			return x.Value;
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> to string.</summary>
		/// <returns>A string whose value is the string representation of the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060020A7 RID: 8359 RVA: 0x0009B0E8 File Offset: 0x000992E8
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			return this.ToDecimal().ToString("#0.00##", null);
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its <see cref="T:System.Data.SqlTypes.SqlMoney" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x060020A8 RID: 8360 RVA: 0x0009B118 File Offset: 0x00099318
		public static SqlMoney Parse(string s)
		{
			SqlMoney @null;
			decimal num;
			if (s == SQLResource.NullString)
			{
				@null = SqlMoney.Null;
			}
			else if (decimal.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol, NumberFormatInfo.InvariantInfo, out num))
			{
				@null = new SqlMoney(num);
			}
			else
			{
				@null = new SqlMoney(decimal.Parse(s, NumberStyles.Currency, NumberFormatInfo.CurrentInfo));
			}
			return @null;
		}

		/// <summary>The unary minus operator negates the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the results of the negation.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be negated. </param>
		// Token: 0x060020A9 RID: 8361 RVA: 0x0009B170 File Offset: 0x00099370
		public static SqlMoney operator -(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlMoney.Null;
			}
			if (x._value == SqlMoney.s_minLong)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return new SqlMoney(-x._value, 0);
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> stucture whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020AA RID: 8362 RVA: 0x0009B1A8 File Offset: 0x000993A8
		public static SqlMoney operator +(SqlMoney x, SqlMoney y)
		{
			SqlMoney sqlMoney;
			try
			{
				sqlMoney = ((x.IsNull || y.IsNull) ? SqlMoney.Null : new SqlMoney(checked(x._value + y._value), 0));
			}
			catch (OverflowException)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return sqlMoney;
		}

		/// <summary>The subtraction operator subtracts the second <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure that contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020AB RID: 8363 RVA: 0x0009B204 File Offset: 0x00099404
		public static SqlMoney operator -(SqlMoney x, SqlMoney y)
		{
			SqlMoney sqlMoney;
			try
			{
				sqlMoney = ((x.IsNull || y.IsNull) ? SqlMoney.Null : new SqlMoney(checked(x._value - y._value), 0));
			}
			catch (OverflowException)
			{
				throw new OverflowException(SQLResource.ArithOverflowMessage);
			}
			return sqlMoney;
		}

		/// <summary>The multiplicaion operator calculates the product of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020AC RID: 8364 RVA: 0x0009B260 File Offset: 0x00099460
		public static SqlMoney operator *(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Multiply(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		/// <summary>The division operator divides the first <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020AD RID: 8365 RVA: 0x0009B292 File Offset: 0x00099492
		public static SqlMoney operator /(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlMoney(decimal.Divide(x.ToDecimal(), y.ToDecimal()));
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x060020AE RID: 8366 RVA: 0x0009B2C4 File Offset: 0x000994C4
		public static explicit operator SqlMoney(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.ByteValue);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x060020AF RID: 8367 RVA: 0x0009B2E1 File Offset: 0x000994E1
		public static implicit operator SqlMoney(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x060020B0 RID: 8368 RVA: 0x0009B2FE File Offset: 0x000994FE
		public static implicit operator SqlMoney(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((int)x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x060020B1 RID: 8369 RVA: 0x0009B31B File Offset: 0x0009951B
		public static implicit operator SqlMoney(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This implicit operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x060020B2 RID: 8370 RVA: 0x0009B338 File Offset: 0x00099538
		public static implicit operator SqlMoney(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted. </param>
		// Token: 0x060020B3 RID: 8371 RVA: 0x0009B355 File Offset: 0x00099555
		public static explicit operator SqlMoney(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney((double)x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x060020B4 RID: 8372 RVA: 0x0009B373 File Offset: 0x00099573
		public static explicit operator SqlMoney(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This operator converts the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x060020B5 RID: 8373 RVA: 0x0009B390 File Offset: 0x00099590
		public static explicit operator SqlMoney(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(x.Value);
			}
			return SqlMoney.Null;
		}

		/// <summary>This operator converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> object to be converted. </param>
		// Token: 0x060020B6 RID: 8374 RVA: 0x0009B3AD File Offset: 0x000995AD
		public static explicit operator SqlMoney(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlMoney(decimal.Parse(x.Value, NumberStyles.Currency, null));
			}
			return SqlMoney.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020B7 RID: 8375 RVA: 0x0009B3D5 File Offset: 0x000995D5
		public static SqlBoolean operator ==(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value == y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020B8 RID: 8376 RVA: 0x0009B402 File Offset: 0x00099602
		public static SqlBoolean operator !=(SqlMoney x, SqlMoney y)
		{
			return !(x == y);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020B9 RID: 8377 RVA: 0x0009B410 File Offset: 0x00099610
		public static SqlBoolean operator <(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value < y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020BA RID: 8378 RVA: 0x0009B43D File Offset: 0x0009963D
		public static SqlBoolean operator >(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value > y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020BB RID: 8379 RVA: 0x0009B46A File Offset: 0x0009966A
		public static SqlBoolean operator <=(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value <= y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020BC RID: 8380 RVA: 0x0009B49A File Offset: 0x0009969A
		public static SqlBoolean operator >=(SqlMoney x, SqlMoney y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(x._value >= y._value);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> stucture whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the sum of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020BD RID: 8381 RVA: 0x0009B4CA File Offset: 0x000996CA
		public static SqlMoney Add(SqlMoney x, SqlMoney y)
		{
			return x + y;
		}

		/// <summary>The subtraction operator subtracts the second <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure that contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020BE RID: 8382 RVA: 0x0009B4D3 File Offset: 0x000996D3
		public static SqlMoney Subtract(SqlMoney x, SqlMoney y)
		{
			return x - y;
		}

		/// <summary>The multiplicaion operator calculates the product of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020BF RID: 8383 RVA: 0x0009B4DC File Offset: 0x000996DC
		public static SqlMoney Multiply(SqlMoney x, SqlMoney y)
		{
			return x * y;
		}

		/// <summary>The division operator divides the first <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C0 RID: 8384 RVA: 0x0009B4E5 File Offset: 0x000996E5
		public static SqlMoney Divide(SqlMoney x, SqlMoney y)
		{
			return x / y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlMoney will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C1 RID: 8385 RVA: 0x0009B4EE File Offset: 0x000996EE
		public static SqlBoolean Equals(SqlMoney x, SqlMoney y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C2 RID: 8386 RVA: 0x0009B4F7 File Offset: 0x000996F7
		public static SqlBoolean NotEquals(SqlMoney x, SqlMoney y)
		{
			return x != y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C3 RID: 8387 RVA: 0x0009B500 File Offset: 0x00099700
		public static SqlBoolean LessThan(SqlMoney x, SqlMoney y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C4 RID: 8388 RVA: 0x0009B509 File Offset: 0x00099709
		public static SqlBoolean GreaterThan(SqlMoney x, SqlMoney y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C5 RID: 8389 RVA: 0x0009B512 File Offset: 0x00099712
		public static SqlBoolean LessThanOrEqual(SqlMoney x, SqlMoney y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure. </param>
		// Token: 0x060020C6 RID: 8390 RVA: 0x0009B51B File Offset: 0x0009971B
		public static SqlBoolean GreaterThanOrEqual(SqlMoney x, SqlMoney y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. If the value of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure is zero, the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value will be <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />.</returns>
		// Token: 0x060020C7 RID: 8391 RVA: 0x0009B524 File Offset: 0x00099724
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />. </returns>
		// Token: 0x060020C8 RID: 8392 RVA: 0x0009B531 File Offset: 0x00099731
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060020C9 RID: 8393 RVA: 0x0009B53E File Offset: 0x0009973E
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060020CA RID: 8394 RVA: 0x0009B54B File Offset: 0x0009974B
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060020CB RID: 8395 RVA: 0x0009B558 File Offset: 0x00099758
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060020CC RID: 8396 RVA: 0x0009B565 File Offset: 0x00099765
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060020CD RID: 8397 RVA: 0x0009B572 File Offset: 0x00099772
		public SqlDecimal ToSqlDecimal()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> equal to the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060020CE RID: 8398 RVA: 0x0009B57F File Offset: 0x0009977F
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure whose value is a string representing the value of this <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060020CF RID: 8399 RVA: 0x0009B58C File Offset: 0x0009978C
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlMoney" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060020D0 RID: 8400 RVA: 0x0009B59C File Offset: 0x0009979C
		public int CompareTo(object value)
		{
			if (value is SqlMoney)
			{
				SqlMoney sqlMoney = (SqlMoney)value;
				return this.CompareTo(sqlMoney);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlMoney));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlMoney" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlMoney" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> to be compared.</param>
		// Token: 0x060020D1 RID: 8401 RVA: 0x0009B5D8 File Offset: 0x000997D8
		public int CompareTo(SqlMoney value)
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

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> object.</summary>
		/// <returns>Equals will return true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlMoney" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x060020D2 RID: 8402 RVA: 0x0009B630 File Offset: 0x00099830
		public override bool Equals(object value)
		{
			if (!(value is SqlMoney))
			{
				return false;
			}
			SqlMoney sqlMoney = (SqlMoney)value;
			if (sqlMoney.IsNull || this.IsNull)
			{
				return sqlMoney.IsNull && this.IsNull;
			}
			return (this == sqlMoney).Value;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060020D3 RID: 8403 RVA: 0x0009B685 File Offset: 0x00099885
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this._value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x060020D4 RID: 8404 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x060020D5 RID: 8405 RVA: 0x0009B69C File Offset: 0x0009989C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this._fNotNull = false;
				return;
			}
			SqlMoney sqlMoney = new SqlMoney(XmlConvert.ToDecimal(reader.ReadElementString()));
			this._fNotNull = sqlMoney._fNotNull;
			this._value = sqlMoney._value;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x060020D6 RID: 8406 RVA: 0x0009B6FE File Offset: 0x000998FE
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(this.ToDecimal()));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x060020D7 RID: 8407 RVA: 0x0009843F File Offset: 0x0009663F
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x0400164F RID: 5711
		private bool _fNotNull;

		// Token: 0x04001650 RID: 5712
		private long _value;

		// Token: 0x04001651 RID: 5713
		internal static readonly int s_iMoneyScale = 4;

		// Token: 0x04001652 RID: 5714
		private static readonly long s_lTickBase = 10000L;

		// Token: 0x04001653 RID: 5715
		private static readonly double s_dTickBase = (double)SqlMoney.s_lTickBase;

		// Token: 0x04001654 RID: 5716
		private static readonly long s_minLong = long.MinValue / SqlMoney.s_lTickBase;

		// Token: 0x04001655 RID: 5717
		private static readonly long s_maxLong = long.MaxValue / SqlMoney.s_lTickBase;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x04001656 RID: 5718
		public static readonly SqlMoney Null = new SqlMoney(true);

		/// <summary>Represents the zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x04001657 RID: 5719
		public static readonly SqlMoney Zero = new SqlMoney(0);

		/// <summary>Represents the minimum value that can be assigned to <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x04001658 RID: 5720
		public static readonly SqlMoney MinValue = new SqlMoney(long.MinValue, 0);

		/// <summary>Represents the maximum value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> class.</summary>
		// Token: 0x04001659 RID: 5721
		public static readonly SqlMoney MaxValue = new SqlMoney(long.MaxValue, 0);
	}
}
