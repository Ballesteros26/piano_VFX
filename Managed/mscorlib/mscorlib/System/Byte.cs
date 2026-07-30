using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents an 8-bit unsigned integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000135 RID: 309
	[ComVisible(true)]
	[Serializable]
	public struct Byte : IComparable, IFormattable, IConvertible, IComparable<byte>, IEquatable<byte>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Byte" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B16 RID: 2838 RVA: 0x00034A60 File Offset: 0x00032C60
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is byte))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Byte."));
			}
			return (int)(this - (byte)value);
		}

		/// <summary>Compares this instance to a specified 8-bit unsigned integer and returns an indication of their relative values.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An 8-bit unsigned integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B17 RID: 2839 RVA: 0x00034A88 File Offset: 0x00032C88
		public int CompareTo(byte value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Byte" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B18 RID: 2840 RVA: 0x00034A8E File Offset: 0x00032C8E
		public override bool Equals(object obj)
		{
			return obj is byte && this == (byte)obj;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Byte" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B19 RID: 2841 RVA: 0x0003430D File Offset: 0x0003250D
		public bool Equals(byte obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Byte" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B1A RID: 2842 RVA: 0x0003446B File Offset: 0x0003266B
		public override int GetHashCode()
		{
			return (int)this;
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B1B RID: 2843 RVA: 0x00034AA4 File Offset: 0x00032CA4
		public static byte Parse(string s)
		{
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the style specified by <paramref name="style" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B1C RID: 2844 RVA: 0x00034AB2 File Offset: 0x00032CB2
		public static byte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style. </param>
		/// <param name="provider">An object that supplies culture-specific parsing information about <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B1D RID: 2845 RVA: 0x00034AC6 File Offset: 0x00032CC6
		public static byte Parse(string s, IFormatProvider provider)
		{
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A byte value that is equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the style specified by <paramref name="style" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. -or-<paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B1E RID: 2846 RVA: 0x00034AD5 File Offset: 0x00032CD5
		public static byte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00034AEC File Offset: 0x00032CEC
		private static byte Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."), ex);
			}
			if (num < 0 || num > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)num;
		}

		/// <summary>Tries to convert the string representation of a number to its <see cref="T:System.Byte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false. </returns>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Byte" /> value equivalent to the number contained in <paramref name="s" /> if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B20 RID: 2848 RVA: 0x00034B48 File Offset: 0x00032D48
		public static bool TryParse(string s, out byte result)
		{
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.Byte" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. The string is interpreted using the style specified by <paramref name="style" />. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. If <paramref name="provider" /> is null, the thread current culture is used. </param>
		/// <param name="result">When this method returns, contains the 8-bit unsigned integer value equivalent to the number contained in <paramref name="s" /> if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B21 RID: 2849 RVA: 0x00034B57 File Offset: 0x00032D57
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00034B70 File Offset: 0x00032D70
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out byte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if (num < 0 || num > 255)
			{
				return false;
			}
			result = (byte)num;
			return true;
		}

		/// <summary>Converts the value of the current <see cref="T:System.Byte" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this object, which consists of a sequence of digits that range from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B23 RID: 2851 RVA: 0x00034BA1 File Offset: 0x00032DA1
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.Byte" /> object to its equivalent string representation using the specified format.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Byte" /> object, formatted as specified by the <paramref name="format" /> parameter.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B24 RID: 2852 RVA: 0x00034BB0 File Offset: 0x00032DB0
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatInt32((int)this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of the current <see cref="T:System.Byte" /> object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of the value of this object in the format specified by the <paramref name="provider" /> parameter.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B25 RID: 2853 RVA: 0x00034BBF File Offset: 0x00032DBF
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of the current <see cref="T:System.Byte" /> object to its equivalent string representation using the specified format and culture-specific formatting information.</summary>
		/// <returns>The string representation of the current <see cref="T:System.Byte" /> object, formatted as specified by the <paramref name="format" /> and <paramref name="provider" /> parameters.</returns>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> includes an unsupported specifier. Supported format specifiers are listed in the Remarks section.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B26 RID: 2854 RVA: 0x00034BCF File Offset: 0x00032DCF
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Byte" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Byte" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B27 RID: 2855 RVA: 0x00029D4E File Offset: 0x00027F4E
		public TypeCode GetTypeCode()
		{
			return TypeCode.Byte;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B28 RID: 2856 RVA: 0x00034BDF File Offset: 0x00032DDF
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B29 RID: 2857 RVA: 0x00034BE8 File Offset: 0x00032DE8
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B2A RID: 2858 RVA: 0x00034BF1 File Offset: 0x00032DF1
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B2B RID: 2859 RVA: 0x0003446B File Offset: 0x0003266B
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B2C RID: 2860 RVA: 0x00034BFA File Offset: 0x00032DFA
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B2D RID: 2861 RVA: 0x00034C03 File Offset: 0x00032E03
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B2E RID: 2862 RVA: 0x00034C0C File Offset: 0x00032E0C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B2F RID: 2863 RVA: 0x00034C15 File Offset: 0x00032E15
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B30 RID: 2864 RVA: 0x00034C1E File Offset: 0x00032E1E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B31 RID: 2865 RVA: 0x00034C27 File Offset: 0x00032E27
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B32 RID: 2866 RVA: 0x00034C30 File Offset: 0x00032E30
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B33 RID: 2867 RVA: 0x00034C39 File Offset: 0x00032E39
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B34 RID: 2868 RVA: 0x00034C42 File Offset: 0x00032E42
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000B35 RID: 2869 RVA: 0x00034C4B File Offset: 0x00032E4B
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Byte", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Byte" /> value. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x06000B36 RID: 2870 RVA: 0x00034C72 File Offset: 0x00032E72
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040007C5 RID: 1989
		private byte m_value;

		/// <summary>Represents the largest possible value of a <see cref="T:System.Byte" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007C6 RID: 1990
		public const byte MaxValue = 255;

		/// <summary>Represents the smallest possible value of a <see cref="T:System.Byte" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007C7 RID: 1991
		public const byte MinValue = 0;
	}
}
