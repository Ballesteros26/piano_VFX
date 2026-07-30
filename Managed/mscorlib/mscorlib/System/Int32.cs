using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a 32-bit signed integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000190 RID: 400
	[ComVisible(true)]
	[Serializable]
	public struct Int32 : IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not an <see cref="T:System.Int32" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010D6 RID: 4310 RVA: 0x00047710 File Offset: 0x00045910
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is int))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Int32."));
			}
			int num = (int)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified 32-bit signed integer and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010D7 RID: 4311 RVA: 0x00047750 File Offset: 0x00045950
		public int CompareTo(int value)
		{
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

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Int32" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010D8 RID: 4312 RVA: 0x00047761 File Offset: 0x00045961
		public override bool Equals(object obj)
		{
			return obj is int && this == (int)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Int32" /> value. </summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Int32" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010D9 RID: 4313 RVA: 0x00047777 File Offset: 0x00045977
		public bool Equals(int obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010DA RID: 4314 RVA: 0x0004777E File Offset: 0x0004597E
		public override int GetHashCode()
		{
			return this;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a negative sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010DB RID: 4315 RVA: 0x00047782 File Offset: 0x00045982
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatInt32(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid or not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010DC RID: 4316 RVA: 0x00047791 File Offset: 0x00045991
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatInt32(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010DD RID: 4317 RVA: 0x000477A0 File Offset: 0x000459A0
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid or not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010DE RID: 4318 RVA: 0x000477B0 File Offset: 0x000459B0
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010DF RID: 4319 RVA: 0x000477C0 File Offset: 0x000459C0
		public static int Parse(string s)
		{
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010E0 RID: 4320 RVA: 0x000477CE File Offset: 0x000459CE
		public static int Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010E1 RID: 4321 RVA: 0x000477E2 File Offset: 0x000459E2
		public static int Parse(string s, IFormatProvider provider)
		{
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 32-bit signed integer equivalent.</summary>
		/// <returns>A 32-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010E2 RID: 4322 RVA: 0x000477F1 File Offset: 0x000459F1
		public static int Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 32-bit signed integer equivalent. A return value indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="result">When this method returns, contains the 32-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010E3 RID: 4323 RVA: 0x00047806 File Offset: 0x00045A06
		public static bool TryParse(string s, out int result)
		{
			return Number.TryParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 32-bit signed integer equivalent. A return value indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. The string is interpreted using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 32-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010E4 RID: 4324 RVA: 0x00047815 File Offset: 0x00045A15
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Int32" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Int32" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010E5 RID: 4325 RVA: 0x0004782B File Offset: 0x00045A2B
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010E6 RID: 4326 RVA: 0x0004782F File Offset: 0x00045A2F
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010E7 RID: 4327 RVA: 0x00047838 File Offset: 0x00045A38
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010E8 RID: 4328 RVA: 0x00047841 File Offset: 0x00045A41
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010E9 RID: 4329 RVA: 0x0004784A File Offset: 0x00045A4A
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010EA RID: 4330 RVA: 0x00047853 File Offset: 0x00045A53
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010EB RID: 4331 RVA: 0x0004785C File Offset: 0x00045A5C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010EC RID: 4332 RVA: 0x0004777E File Offset: 0x0004597E
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010ED RID: 4333 RVA: 0x00047865 File Offset: 0x00045A65
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010EE RID: 4334 RVA: 0x0004786E File Offset: 0x00045A6E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010EF RID: 4335 RVA: 0x00047877 File Offset: 0x00045A77
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010F0 RID: 4336 RVA: 0x00047880 File Offset: 0x00045A80
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010F1 RID: 4337 RVA: 0x00047889 File Offset: 0x00045A89
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010F2 RID: 4338 RVA: 0x00047892 File Offset: 0x00045A92
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060010F3 RID: 4339 RVA: 0x0004789B File Offset: 0x00045A9B
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Int32", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Int32" /> value.</param>
		/// <param name="provider">An object that provides information about the format of the returned value.</param>
		// Token: 0x060010F4 RID: 4340 RVA: 0x000478C2 File Offset: 0x00045AC2
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000A2B RID: 2603
		internal int m_value;

		/// <summary>Represents the largest possible value of an <see cref="T:System.Int32" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000A2C RID: 2604
		public const int MaxValue = 2147483647;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Int32" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000A2D RID: 2605
		public const int MinValue = -2147483648;
	}
}
