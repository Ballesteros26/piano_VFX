using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents an 8-bit signed integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001BD RID: 445
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public struct SByte : IComparable, IFormattable, IConvertible, IComparable<sbyte>, IEquatable<sbyte>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="obj" />.Return Value Description Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.-or- <paramref name="obj" /> is null. </returns>
		/// <param name="obj">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not an <see cref="T:System.SByte" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012D9 RID: 4825 RVA: 0x0004D45B File Offset: 0x0004B65B
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is sbyte))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type SByte."));
			}
			return (int)(this - (sbyte)obj);
		}

		/// <summary>Compares this instance to a specified 8-bit signed integer and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An 8-bit signed integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012DA RID: 4826 RVA: 0x0004D483 File Offset: 0x0004B683
		public int CompareTo(sbyte value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.SByte" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012DB RID: 4827 RVA: 0x0004D489 File Offset: 0x0004B689
		public override bool Equals(object obj)
		{
			return obj is sbyte && this == (sbyte)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.SByte" /> value.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.SByte" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012DC RID: 4828 RVA: 0x0004D49F File Offset: 0x0004B69F
		public bool Equals(sbyte obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012DD RID: 4829 RVA: 0x0004D4A6 File Offset: 0x0004B6A6
		public override int GetHashCode()
		{
			return (int)this ^ ((int)this << 8);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a negative sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012DE RID: 4830 RVA: 0x0004D4AF File Offset: 0x0004B6AF
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance, as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012DF RID: 4831 RVA: 0x0004D4BE File Offset: 0x0004B6BE
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E0 RID: 4832 RVA: 0x0004D4CE File Offset: 0x0004B6CE
		public string ToString(string format)
		{
			return this.ToString(format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E1 RID: 4833 RVA: 0x0004D4DC File Offset: 0x0004B6DC
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004D4EC File Offset: 0x0004B6EC
		[SecuritySafeCritical]
		private string ToString(string format, NumberFormatInfo info)
		{
			if (this < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
			{
				return Number.FormatUInt32((uint)this & 255U, format, info);
			}
			return Number.FormatInt32((int)this, format, info);
		}

		/// <summary>Converts the string representation of a number to its 8-bit signed integer equivalent.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number contained in the <paramref name="s" /> parameter.</returns>
		/// <param name="s">A string that represents a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not consist of an optional sign followed by a sequence of digits (zero through nine). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E3 RID: 4835 RVA: 0x0004D539 File Offset: 0x0004B739
		[CLSCompliant(false)]
		public static sbyte Parse(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 8-bit signed integer equivalent.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format that is compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E4 RID: 4836 RVA: 0x0004D547 File Offset: 0x0004B747
		[CLSCompliant(false)]
		public static sbyte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 8-bit signed integer equivalent.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that represents a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E5 RID: 4837 RVA: 0x0004D55B File Offset: 0x0004B75B
		[CLSCompliant(false)]
		public static sbyte Parse(string s, IFormatProvider provider)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number that is in a specified style and culture-specific format to its 8-bit signed equivalent.</summary>
		/// <returns>An 8-bit signed byte value that is equivalent to the number specified in the <paramref name="s" /> parameter.</returns>
		/// <param name="s">A string that contains the number to convert. The string is interpreted by using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.-or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format that is compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.-or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E6 RID: 4838 RVA: 0x0004D56A File Offset: 0x0004B76A
		[CLSCompliant(false)]
		public static sbyte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004D580 File Offset: 0x0004B780
		private static sbyte Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."), ex);
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
				}
				return (sbyte)num;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
				}
				return (sbyte)num;
			}
		}

		/// <summary>Tries to convert the string representation of a number to its <see cref="T:System.SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="result">When this method returns, contains the 8-bit signed integer value that is equivalent to the number contained in <paramref name="s" /> if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in the correct format, or represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. This parameter is passed uninitialized.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E8 RID: 4840 RVA: 0x0004D600 File Offset: 0x0004B800
		[CLSCompliant(false)]
		public static bool TryParse(string s, out sbyte result)
		{
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string representing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 8-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012E9 RID: 4841 RVA: 0x0004D60F File Offset: 0x0004B80F
		[CLSCompliant(false)]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0004D628 File Offset: 0x0004B828
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out sbyte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.SByte" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.SByte" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060012EB RID: 4843 RVA: 0x000293E5 File Offset: 0x000275E5
		public TypeCode GetTypeCode()
		{
			return TypeCode.SByte;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is unused.</param>
		// Token: 0x060012EC RID: 4844 RVA: 0x0004D674 File Offset: 0x0004B874
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012ED RID: 4845 RVA: 0x0004D67D File Offset: 0x0004B87D
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012EE RID: 4846 RVA: 0x0004D686 File Offset: 0x0004B886
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is unused.</param>
		// Token: 0x060012EF RID: 4847 RVA: 0x0004D68A File Offset: 0x0004B88A
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F0 RID: 4848 RVA: 0x0004D693 File Offset: 0x0004B893
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F1 RID: 4849 RVA: 0x0004D69C File Offset: 0x0004B89C
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F2 RID: 4850 RVA: 0x0004D686 File Offset: 0x0004B886
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F3 RID: 4851 RVA: 0x0004D6A5 File Offset: 0x0004B8A5
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F4 RID: 4852 RVA: 0x0004D6AE File Offset: 0x0004B8AE
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F5 RID: 4853 RVA: 0x0004D6B7 File Offset: 0x0004B8B7
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F6 RID: 4854 RVA: 0x0004D6C0 File Offset: 0x0004B8C0
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060012F7 RID: 4855 RVA: 0x0004D6C9 File Offset: 0x0004B8C9
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is unused.</param>
		// Token: 0x060012F8 RID: 4856 RVA: 0x0004D6D2 File Offset: 0x0004B8D2
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. This conversion is not supported. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x060012F9 RID: 4857 RVA: 0x0004D6DB File Offset: 0x0004B8DB
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "SByte", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an object of type <paramref name="type" />.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to which to convert this <see cref="T:System.SByte" /> value.</param>
		/// <param name="provider">A <see cref="T:System.IFormatProvider" /> implementation that provides information about the format of the returned value.</param>
		// Token: 0x060012FA RID: 4858 RVA: 0x0004D702 File Offset: 0x0004B902
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000ACC RID: 2764
		private sbyte m_value;

		/// <summary>Represents the largest possible value of <see cref="T:System.SByte" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000ACD RID: 2765
		public const sbyte MaxValue = 127;

		/// <summary>Represents the smallest possible value of <see cref="T:System.SByte" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000ACE RID: 2766
		public const sbyte MinValue = -128;
	}
}
