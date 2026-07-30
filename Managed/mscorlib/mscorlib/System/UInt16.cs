using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a 16-bit unsigned integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001E3 RID: 483
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public struct UInt16 : IComparable, IFormattable, IConvertible, IComparable<ushort>, IEquatable<ushort>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.UInt16" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600160D RID: 5645 RVA: 0x00058B27 File Offset: 0x00056D27
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is ushort)
			{
				return (int)(this - (ushort)value);
			}
			throw new ArgumentException(Environment.GetResourceString("Object must be of type UInt16."));
		}

		/// <summary>Compares this instance to a specified 16-bit unsigned integer and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An unsigned integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600160E RID: 5646 RVA: 0x00034D37 File Offset: 0x00032F37
		public int CompareTo(ushort value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.UInt16" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600160F RID: 5647 RVA: 0x00058B4F File Offset: 0x00056D4F
		public override bool Equals(object obj)
		{
			return obj is ushort && this == (ushort)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.UInt16" /> value.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">A 16-bit unsigned integer to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001610 RID: 5648 RVA: 0x00034D08 File Offset: 0x00032F08
		public bool Equals(ushort obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001611 RID: 5649 RVA: 0x00034F9D File Offset: 0x0003319D
		public override int GetHashCode()
		{
			return (int)this;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, which consists of a sequence of digits ranging from 0 to 9, without a sign or leading zeros.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001612 RID: 5650 RVA: 0x00058B65 File Offset: 0x00056D65
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatUInt32((uint)this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance, which consists of a sequence of digits ranging from 0 to 9, without a sign or leading zeros.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001613 RID: 5651 RVA: 0x00058B74 File Offset: 0x00056D74
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001614 RID: 5652 RVA: 0x00058B84 File Offset: 0x00056D84
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatUInt32((uint)this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001615 RID: 5653 RVA: 0x00058B93 File Offset: 0x00056D93
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 16-bit unsigned integer equivalent.</summary>
		/// <returns>A 16-bit unsigned integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001616 RID: 5654 RVA: 0x00058BA3 File Offset: 0x00056DA3
		[CLSCompliant(false)]
		public static ushort Parse(string s)
		{
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 16-bit unsigned integer equivalent.</summary>
		/// <returns>A 16-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001617 RID: 5655 RVA: 0x00058BB1 File Offset: 0x00056DB1
		[CLSCompliant(false)]
		public static ushort Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 16-bit unsigned integer equivalent.</summary>
		/// <returns>A 16-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001618 RID: 5656 RVA: 0x00058BC5 File Offset: 0x00056DC5
		[CLSCompliant(false)]
		public static ushort Parse(string s, IFormatProvider provider)
		{
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 16-bit unsigned integer equivalent.</summary>
		/// <returns>A 16-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001619 RID: 5657 RVA: 0x00058BD4 File Offset: 0x00056DD4
		[CLSCompliant(false)]
		public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00058BEC File Offset: 0x00056DEC
		private static ushort Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			uint num = 0U;
			try
			{
				num = Number.ParseUInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."), ex);
			}
			if (num > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)num;
		}

		/// <summary>Tries to convert the string representation of a number to its 16-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that represents the number to convert. </param>
		/// <param name="result">When this method returns, contains the 16-bit unsigned integer value that is equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in the correct format. , or represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600161B RID: 5659 RVA: 0x00058C44 File Offset: 0x00056E44
		[CLSCompliant(false)]
		public static bool TryParse(string s, out ushort result)
		{
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its 16-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 16-bit unsigned integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600161C RID: 5660 RVA: 0x00058C53 File Offset: 0x00056E53
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00058C6C File Offset: 0x00056E6C
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out ushort result)
		{
			result = 0;
			uint num;
			if (!Number.TryParseUInt32(s, style, info, out num))
			{
				return false;
			}
			if (num > 65535U)
			{
				return false;
			}
			result = (ushort)num;
			return true;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.UInt16" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.UInt16" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600161E RID: 5662 RVA: 0x00058C99 File Offset: 0x00056E99
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600161F RID: 5663 RVA: 0x00058C9C File Offset: 0x00056E9C
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001620 RID: 5664 RVA: 0x00058CA5 File Offset: 0x00056EA5
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001621 RID: 5665 RVA: 0x00058CAE File Offset: 0x00056EAE
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001622 RID: 5666 RVA: 0x00058CB7 File Offset: 0x00056EB7
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001623 RID: 5667 RVA: 0x00058CC0 File Offset: 0x00056EC0
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001624 RID: 5668 RVA: 0x00034F9D File Offset: 0x0003319D
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of this instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001625 RID: 5669 RVA: 0x00058CC9 File Offset: 0x00056EC9
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001626 RID: 5670 RVA: 0x00058CD2 File Offset: 0x00056ED2
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001627 RID: 5671 RVA: 0x00058CDB File Offset: 0x00056EDB
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001628 RID: 5672 RVA: 0x00058CE4 File Offset: 0x00056EE4
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The current value pf this instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001629 RID: 5673 RVA: 0x00058CED File Offset: 0x00056EED
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600162A RID: 5674 RVA: 0x00058CF6 File Offset: 0x00056EF6
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600162B RID: 5675 RVA: 0x00058CFF File Offset: 0x00056EFF
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600162C RID: 5676 RVA: 0x00058D08 File Offset: 0x00056F08
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "UInt16", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The current value of this instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.UInt16" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		// Token: 0x0600162D RID: 5677 RVA: 0x00058D2F File Offset: 0x00056F2F
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000BB9 RID: 3001
		private ushort m_value;

		/// <summary>Represents the largest possible value of <see cref="T:System.UInt16" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BBA RID: 3002
		public const ushort MaxValue = 65535;

		/// <summary>Represents the smallest possible value of <see cref="T:System.UInt16" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BBB RID: 3003
		public const ushort MinValue = 0;
	}
}
