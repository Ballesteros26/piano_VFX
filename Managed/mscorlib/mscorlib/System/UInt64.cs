using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a 64-bit unsigned integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001E5 RID: 485
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public struct UInt64 : IComparable, IFormattable, IConvertible, IComparable<ulong>, IEquatable<ulong>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.UInt64" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600164D RID: 5709 RVA: 0x00058F04 File Offset: 0x00057104
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type UInt64."));
			}
			ulong num = (ulong)value;
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

		/// <summary>Compares this instance to a specified 64-bit unsigned integer and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An unsigned integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600164E RID: 5710 RVA: 0x00058F44 File Offset: 0x00057144
		public int CompareTo(ulong value)
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
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.UInt64" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600164F RID: 5711 RVA: 0x00058F55 File Offset: 0x00057155
		public override bool Equals(object obj)
		{
			return obj is ulong && this == (ulong)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.UInt64" /> value.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.UInt64" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001650 RID: 5712 RVA: 0x0004793B File Offset: 0x00045B3B
		public bool Equals(ulong obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001651 RID: 5713 RVA: 0x00058F6B File Offset: 0x0005716B
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a sequence of digits ranging from 0 to 9, without a sign or leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001652 RID: 5714 RVA: 0x00058F77 File Offset: 0x00057177
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001653 RID: 5715 RVA: 0x00058F86 File Offset: 0x00057186
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001654 RID: 5716 RVA: 0x00058F96 File Offset: 0x00057196
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance. </param>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001655 RID: 5717 RVA: 0x00058FA5 File Offset: 0x000571A5
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 64-bit unsigned integer equivalent.</summary>
		/// <returns>A 64-bit unsigned integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001656 RID: 5718 RVA: 0x00058FB5 File Offset: 0x000571B5
		[CLSCompliant(false)]
		public static ulong Parse(string s)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 64-bit unsigned integer equivalent.</summary>
		/// <returns>A 64-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter. </param>
		/// <param name="style">A bitwise combination of the enumeration values that specifies the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001657 RID: 5719 RVA: 0x00058FC3 File Offset: 0x000571C3
		[CLSCompliant(false)]
		public static ulong Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 64-bit unsigned integer equivalent.</summary>
		/// <returns>A 64-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in the correct style. </exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001658 RID: 5720 RVA: 0x00058FD7 File Offset: 0x000571D7
		[CLSCompliant(false)]
		public static ulong Parse(string s, IFormatProvider provider)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 64-bit unsigned integer equivalent.</summary>
		/// <returns>A 64-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001659 RID: 5721 RVA: 0x00058FE6 File Offset: 0x000571E6
		[CLSCompliant(false)]
		public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Tries to convert the string representation of a number to its 64-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that represents the number to convert. </param>
		/// <param name="result">When this method returns, contains the 64-bit unsigned integer value that is equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600165A RID: 5722 RVA: 0x00058FFB File Offset: 0x000571FB
		[CLSCompliant(false)]
		public static bool TryParse(string s, out ulong result)
		{
			return Number.TryParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its 64-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 64-bit unsigned integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600165B RID: 5723 RVA: 0x0005900A File Offset: 0x0005720A
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.UInt64" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.UInt64" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600165C RID: 5724 RVA: 0x00059020 File Offset: 0x00057220
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600165D RID: 5725 RVA: 0x00059024 File Offset: 0x00057224
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600165E RID: 5726 RVA: 0x0005902D File Offset: 0x0005722D
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600165F RID: 5727 RVA: 0x00059036 File Offset: 0x00057236
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001660 RID: 5728 RVA: 0x0005903F File Offset: 0x0005723F
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001661 RID: 5729 RVA: 0x00059048 File Offset: 0x00057248
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001662 RID: 5730 RVA: 0x00059051 File Offset: 0x00057251
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001663 RID: 5731 RVA: 0x0005905A File Offset: 0x0005725A
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001664 RID: 5732 RVA: 0x00059063 File Offset: 0x00057263
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001665 RID: 5733 RVA: 0x0005906C File Offset: 0x0005726C
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001666 RID: 5734 RVA: 0x00047A43 File Offset: 0x00045C43
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001667 RID: 5735 RVA: 0x00059075 File Offset: 0x00057275
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001668 RID: 5736 RVA: 0x0005907E File Offset: 0x0005727E
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001669 RID: 5737 RVA: 0x00059087 File Offset: 0x00057287
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600166A RID: 5738 RVA: 0x00059090 File Offset: 0x00057290
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "UInt64", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.UInt64" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		// Token: 0x0600166B RID: 5739 RVA: 0x000590B7 File Offset: 0x000572B7
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000BBF RID: 3007
		private ulong m_value;

		/// <summary>Represents the largest possible value of <see cref="T:System.UInt64" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BC0 RID: 3008
		public const ulong MaxValue = 18446744073709551615UL;

		/// <summary>Represents the smallest possible value of <see cref="T:System.UInt64" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BC1 RID: 3009
		public const ulong MinValue = 0UL;
	}
}
