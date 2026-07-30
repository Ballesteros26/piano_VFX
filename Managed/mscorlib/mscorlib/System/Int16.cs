using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a 16-bit signed integer.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200018F RID: 399
	[ComVisible(true)]
	[Serializable]
	public struct Int16 : IComparable, IFormattable, IConvertible, IComparable<short>, IEquatable<short>
	{
		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not an <see cref="T:System.Int16" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B4 RID: 4276 RVA: 0x0004743B File Offset: 0x0004563B
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is short)
			{
				return (int)(this - (short)value);
			}
			throw new ArgumentException(Environment.GetResourceString("Object must be of type Int16."));
		}

		/// <summary>Compares this instance to a specified 16-bit signed integer and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified 16-bit signed integer.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">An integer to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B5 RID: 4277 RVA: 0x00047463 File Offset: 0x00045663
		public int CompareTo(short value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Int16" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B6 RID: 4278 RVA: 0x00047469 File Offset: 0x00045669
		public override bool Equals(object obj)
		{
			return obj is short && this == (short)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Int16" /> value.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Int16" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B7 RID: 4279 RVA: 0x0004747F File Offset: 0x0004567F
		public bool Equals(short obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B8 RID: 4280 RVA: 0x00047486 File Offset: 0x00045686
		public override int GetHashCode()
		{
			return (int)((ushort)this) | ((int)this << 16);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a minus sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00047491 File Offset: 0x00045691
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010BA RID: 4282 RVA: 0x000474A0 File Offset: 0x000456A0
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010BB RID: 4283 RVA: 0x000474B0 File Offset: 0x000456B0
		public string ToString(string format)
		{
			return this.ToString(format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific formatting information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010BC RID: 4284 RVA: 0x000474BE File Offset: 0x000456BE
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000474D0 File Offset: 0x000456D0
		[SecuritySafeCritical]
		private string ToString(string format, NumberFormatInfo info)
		{
			if (this < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
			{
				return Number.FormatUInt32((uint)this & 65535U, format, info);
			}
			return Number.FormatInt32((int)this, format, info);
		}

		/// <summary>Converts the string representation of a number to its 16-bit signed integer equivalent.</summary>
		/// <returns>A 16-bit signed integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010BE RID: 4286 RVA: 0x0004751D File Offset: 0x0004571D
		public static short Parse(string s)
		{
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 16-bit signed integer equivalent.</summary>
		/// <returns>A 16-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. -or-<paramref name="s" /> includes non-zero fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010BF RID: 4287 RVA: 0x0004752B File Offset: 0x0004572B
		public static short Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 16-bit signed integer equivalent.</summary>
		/// <returns>A 16-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010C0 RID: 4288 RVA: 0x0004753F File Offset: 0x0004573F
		public static short Parse(string s, IFormatProvider provider)
		{
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 16-bit signed integer equivalent.</summary>
		/// <returns>A 16-bit signed integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. -or-<paramref name="s" /> includes non-zero fractional digits.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010C1 RID: 4289 RVA: 0x0004754E File Offset: 0x0004574E
		public static short Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00047564 File Offset: 0x00045764
		private static short Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."), ex);
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 65535)
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
				}
				return (short)num;
			}
			else
			{
				if (num < -32768 || num > 32767)
				{
					throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
				}
				return (short)num;
			}
		}

		/// <summary>Converts the string representation of a number to its 16-bit signed integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="result">When this method returns, contains the 16-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not of the correct format, or represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010C3 RID: 4291 RVA: 0x000475EC File Offset: 0x000457EC
		public static bool TryParse(string s, out short result)
		{
			return short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 16-bit signed integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. The string is interpreted using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the 16-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060010C4 RID: 4292 RVA: 0x000475FB File Offset: 0x000457FB
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out short result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00047614 File Offset: 0x00045814
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out short result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 65535)
				{
					return false;
				}
				result = (short)num;
				return true;
			}
			else
			{
				if (num < -32768 || num > 32767)
				{
					return false;
				}
				result = (short)num;
				return true;
			}
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Int16" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Int16" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010C6 RID: 4294 RVA: 0x0002A81F File Offset: 0x00028A1F
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int16;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010C7 RID: 4295 RVA: 0x00047666 File Offset: 0x00045866
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010C8 RID: 4296 RVA: 0x0004766F File Offset: 0x0004586F
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010C9 RID: 4297 RVA: 0x00047678 File Offset: 0x00045878
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010CA RID: 4298 RVA: 0x00047681 File Offset: 0x00045881
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010CB RID: 4299 RVA: 0x0004768A File Offset: 0x0004588A
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010CC RID: 4300 RVA: 0x0004768E File Offset: 0x0004588E
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010CD RID: 4301 RVA: 0x00047697 File Offset: 0x00045897
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010CE RID: 4302 RVA: 0x000476A0 File Offset: 0x000458A0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010CF RID: 4303 RVA: 0x000476A9 File Offset: 0x000458A9
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010D0 RID: 4304 RVA: 0x000476B2 File Offset: 0x000458B2
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010D1 RID: 4305 RVA: 0x000476BB File Offset: 0x000458BB
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010D2 RID: 4306 RVA: 0x000476C4 File Offset: 0x000458C4
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x060010D3 RID: 4307 RVA: 0x000476CD File Offset: 0x000458CD
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060010D4 RID: 4308 RVA: 0x000476D6 File Offset: 0x000458D6
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Int16", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Int16" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		// Token: 0x060010D5 RID: 4309 RVA: 0x000476FD File Offset: 0x000458FD
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000A28 RID: 2600
		internal short m_value;

		/// <summary>Represents the largest possible value of an <see cref="T:System.Int16" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000A29 RID: 2601
		public const short MaxValue = 32767;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Int16" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000A2A RID: 2602
		public const short MinValue = -32768;
	}
}
