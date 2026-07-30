using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a single-precision floating-point number.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C0 RID: 448
	[ComVisible(true)]
	[Serializable]
	public struct Single : IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
	{
		/// <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity.</summary>
		/// <returns>true if <paramref name="f" /> evaluates to <see cref="F:System.Single.PositiveInfinity" /> or <see cref="F:System.Single.NegativeInfinity" />; otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001306 RID: 4870 RVA: 0x0004D8C8 File Offset: 0x0004BAC8
		[SecuritySafeCritical]
		public unsafe static bool IsInfinity(float f)
		{
			return (*(int*)(&f) & int.MaxValue) == 2139095040;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
		/// <returns>true if <paramref name="f" /> evaluates to <see cref="F:System.Single.PositiveInfinity" />; otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001307 RID: 4871 RVA: 0x0004D8DB File Offset: 0x0004BADB
		[SecuritySafeCritical]
		public unsafe static bool IsPositiveInfinity(float f)
		{
			return *(int*)(&f) == 2139095040;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
		/// <returns>true if <paramref name="f" /> evaluates to <see cref="F:System.Single.NegativeInfinity" />; otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001308 RID: 4872 RVA: 0x0004D8E8 File Offset: 0x0004BAE8
		[SecuritySafeCritical]
		public unsafe static bool IsNegativeInfinity(float f)
		{
			return *(int*)(&f) == -8388608;
		}

		/// <summary>Returns a value that indicates whether the specified value is not a number (<see cref="F:System.Single.NaN" />).</summary>
		/// <returns>true if <paramref name="f" /> evaluates to not a number (<see cref="F:System.Single.NaN" />); otherwise, false.</returns>
		/// <param name="f">A single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001309 RID: 4873 RVA: 0x0004D8F5 File Offset: 0x0004BAF5
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static bool IsNaN(float f)
		{
			return (*(int*)(&f) & int.MaxValue) > 2139095040;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004D908 File Offset: 0x0004BB08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool IsFinite(float f)
		{
			return (*(int*)(&f) & int.MaxValue) < 2139095040;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Single.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- This instance and value are both not a number (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Single.NaN" />).-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Single" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600130B RID: 4875 RVA: 0x0004D91C File Offset: 0x0004BB1C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Single."));
			}
			float num = (float)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified single-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified single-precision floating-point number.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Single.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Single.NaN" />). </returns>
		/// <param name="value">A single-precision floating-point number to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600130C RID: 4876 RVA: 0x0004D978 File Offset: 0x0004BB78
		public int CompareTo(float value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Single" /> values are equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x0600130D RID: 4877 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		public static bool operator ==(float left, float right)
		{
			return left == right;
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Single" /> values are not equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x0600130E RID: 4878 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		public static bool operator !=(float left, float right)
		{
			return left != right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is less than another specified <see cref="T:System.Single" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x0600130F RID: 4879 RVA: 0x0003CBD9 File Offset: 0x0003ADD9
		public static bool operator <(float left, float right)
		{
			return left < right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is greater than another specified <see cref="T:System.Single" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06001310 RID: 4880 RVA: 0x0003CBDF File Offset: 0x0003ADDF
		public static bool operator >(float left, float right)
		{
			return left > right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is less than or equal to another specified <see cref="T:System.Single" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06001311 RID: 4881 RVA: 0x0003CBE5 File Offset: 0x0003ADE5
		public static bool operator <=(float left, float right)
		{
			return left <= right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is greater than or equal to another specified <see cref="T:System.Single" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06001312 RID: 4882 RVA: 0x0003CBEE File Offset: 0x0003ADEE
		public static bool operator >=(float left, float right)
		{
			return left >= right;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Single" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001313 RID: 4883 RVA: 0x0004D9A8 File Offset: 0x0004BBA8
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			return num == this || (float.IsNaN(num) && float.IsNaN(this));
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Single" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001314 RID: 4884 RVA: 0x0004D9DE File Offset: 0x0004BBDE
		public bool Equals(float obj)
		{
			return obj == this || (float.IsNaN(obj) && float.IsNaN(this));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001315 RID: 4885 RVA: 0x0004D9F8 File Offset: 0x0004BBF8
		[SecuritySafeCritical]
		public unsafe override int GetHashCode()
		{
			float num = this;
			if (num == 0f)
			{
				return 0;
			}
			return *(int*)(&num);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001316 RID: 4886 RVA: 0x0004DA16 File Offset: 0x0004BC16
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001317 RID: 4887 RVA: 0x0004DA25 File Offset: 0x0004BC25
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001318 RID: 4888 RVA: 0x0004DA35 File Offset: 0x0004BC35
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001319 RID: 4889 RVA: 0x0004DA44 File Offset: 0x0004BC44
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600131A RID: 4890 RVA: 0x0004DA54 File Offset: 0x0004BC54
		public static float Parse(string s)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600131B RID: 4891 RVA: 0x0004DA66 File Offset: 0x0004BC66
		public static float Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600131C RID: 4892 RVA: 0x0004DA7A File Offset: 0x0004BC7A
		public static float Parse(string s, IFormatProvider provider)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent.</summary>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a numeric value. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600131D RID: 4893 RVA: 0x0004DA8D File Offset: 0x0004BC8D
		public static float Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004DAA2 File Offset: 0x0004BCA2
		private static float Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseSingle(s, style, info);
		}

		/// <summary>Converts the string representation of a number to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string representing a number to convert. </param>
		/// <param name="result">When this method returns, contains single-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not a number in a valid format, or represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600131F RID: 4895 RVA: 0x0004DAAC File Offset: 0x0004BCAC
		public static bool TryParse(string s, out float result)
		{
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string representing a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the single-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />, or if <paramref name="style" /> is not a valid combination of <see cref="T:System.Globalization.NumberStyles" /> enumerated constants. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001320 RID: 4896 RVA: 0x0004DABF File Offset: 0x0004BCBF
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004DAD8 File Offset: 0x0004BCD8
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out float result)
		{
			if (s == null)
			{
				result = 0f;
				return false;
			}
			if (!Number.TryParseSingle(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = float.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = float.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = float.NaN;
				}
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Single" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Single" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001322 RID: 4898 RVA: 0x0004DB4B File Offset: 0x0004BD4B
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001323 RID: 4899 RVA: 0x0004DB4F File Offset: 0x0004BD4F
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />. </summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001324 RID: 4900 RVA: 0x0004DB58 File Offset: 0x0004BD58
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Single", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001325 RID: 4901 RVA: 0x0004DB7F File Offset: 0x0004BD7F
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001326 RID: 4902 RVA: 0x0004DB88 File Offset: 0x0004BD88
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001327 RID: 4903 RVA: 0x0004DB91 File Offset: 0x0004BD91
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001328 RID: 4904 RVA: 0x0004DB9A File Offset: 0x0004BD9A
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06001329 RID: 4905 RVA: 0x0004DBA3 File Offset: 0x0004BDA3
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600132A RID: 4906 RVA: 0x0004DBAC File Offset: 0x0004BDAC
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600132B RID: 4907 RVA: 0x0004DBB5 File Offset: 0x0004BDB5
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600132C RID: 4908 RVA: 0x0004DBBE File Offset: 0x0004BDBE
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600132D RID: 4909 RVA: 0x0004DBC7 File Offset: 0x0004BDC7
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600132E RID: 4910 RVA: 0x0004DBCB File Offset: 0x0004BDCB
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x0600132F RID: 4911 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001330 RID: 4912 RVA: 0x0004DBDD File Offset: 0x0004BDDD
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Single", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Single" /> value.</param>
		/// <param name="provider">An object that supplies information about the format of the returned value.</param>
		// Token: 0x06001331 RID: 4913 RVA: 0x0004DC04 File Offset: 0x0004BE04
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000AD4 RID: 2772
		internal float m_value;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AD5 RID: 2773
		public const float MinValue = -3.4028235E+38f;

		/// <summary>Represents the smallest positive <see cref="T:System.Single" /> value that is greater than zero. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AD6 RID: 2774
		public const float Epsilon = 1E-45f;

		/// <summary>Represents the largest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AD7 RID: 2775
		public const float MaxValue = 3.4028235E+38f;

		/// <summary>Represents positive infinity. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AD8 RID: 2776
		public const float PositiveInfinity = float.PositiveInfinity;

		/// <summary>Represents negative infinity. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AD9 RID: 2777
		public const float NegativeInfinity = float.NegativeInfinity;

		/// <summary>Represents not a number (NaN). This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000ADA RID: 2778
		public const float NaN = float.NaN;
	}
}
