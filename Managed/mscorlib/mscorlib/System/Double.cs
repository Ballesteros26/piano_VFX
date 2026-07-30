using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a double-precision floating-point number.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000152 RID: 338
	[ComVisible(true)]
	[Serializable]
	public struct Double : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
	{
		/// <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity </summary>
		/// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.PositiveInfinity" /> or <see cref="F:System.Double.NegativeInfinity" />; otherwise, false.</returns>
		/// <param name="d">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E76 RID: 3702 RVA: 0x0003CA76 File Offset: 0x0003AC76
		[SecuritySafeCritical]
		public unsafe static bool IsInfinity(double d)
		{
			return (*(long*)(&d) & long.MaxValue) == 9218868437227405312L;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
		/// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.PositiveInfinity" />; otherwise, false.</returns>
		/// <param name="d">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E77 RID: 3703 RVA: 0x0003CA91 File Offset: 0x0003AC91
		public static bool IsPositiveInfinity(double d)
		{
			return d == double.PositiveInfinity;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
		/// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.NegativeInfinity" />; otherwise, false.</returns>
		/// <param name="d">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E78 RID: 3704 RVA: 0x0003CAA2 File Offset: 0x0003ACA2
		public static bool IsNegativeInfinity(double d)
		{
			return d == double.NegativeInfinity;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003CAB3 File Offset: 0x0003ACB3
		[SecuritySafeCritical]
		internal unsafe static bool IsNegative(double d)
		{
			return (*(long*)(&d) & long.MinValue) == long.MinValue;
		}

		/// <summary>Returns a value that indicates whether the specified value is not a number (<see cref="F:System.Double.NaN" />).</summary>
		/// <returns>true if <paramref name="d" /> evaluates to <see cref="F:System.Double.NaN" />; otherwise, false.</returns>
		/// <param name="d">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E7A RID: 3706 RVA: 0x0003CACE File Offset: 0x0003ACCE
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static bool IsNaN(double d)
		{
			return (*(long*)(&d) & long.MaxValue) > 9218868437227405312L;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003CAE9 File Offset: 0x0003ACE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(double d)
		{
			return (BitConverter.DoubleToInt64Bits(d) & long.MaxValue) < 9218868437227405312L;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Value Description A negative integer This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- This instance and <paramref name="value" /> are both Double.NaN, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />A positive integer This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />).-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Double" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E7C RID: 3708 RVA: 0x0003CB08 File Offset: 0x0003AD08
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is double))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Double."));
			}
			double num = (double)value;
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
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified double-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified double-precision floating-point number.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return Value Description Less than zero This instance is less than <paramref name="value" />.-or- This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number. Zero This instance is equal to <paramref name="value" />.-or- Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Double.NaN" />), <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. Greater than zero This instance is greater than <paramref name="value" />.-or- This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />). </returns>
		/// <param name="value">A double-precision floating-point number to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E7D RID: 3709 RVA: 0x0003CB64 File Offset: 0x0003AD64
		public int CompareTo(double value)
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
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Double" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E7E RID: 3710 RVA: 0x0003CB94 File Offset: 0x0003AD94
		public override bool Equals(object obj)
		{
			if (!(obj is double))
			{
				return false;
			}
			double num = (double)obj;
			return num == this || (double.IsNaN(num) && double.IsNaN(this));
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Double" /> values are equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The first value to compare. </param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000E7F RID: 3711 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		public static bool operator ==(double left, double right)
		{
			return left == right;
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Double" /> values are not equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000E80 RID: 3712 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		public static bool operator !=(double left, double right)
		{
			return left != right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is less than another specified <see cref="T:System.Double" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000E81 RID: 3713 RVA: 0x0003CBD9 File Offset: 0x0003ADD9
		public static bool operator <(double left, double right)
		{
			return left < right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is greater than another specified <see cref="T:System.Double" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000E82 RID: 3714 RVA: 0x0003CBDF File Offset: 0x0003ADDF
		public static bool operator >(double left, double right)
		{
			return left > right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is less than or equal to another specified <see cref="T:System.Double" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000E83 RID: 3715 RVA: 0x0003CBE5 File Offset: 0x0003ADE5
		public static bool operator <=(double left, double right)
		{
			return left <= right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is greater than or equal to another specified <see cref="T:System.Double" /> value.</summary>
		/// <returns>true if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x0003CBEE File Offset: 0x0003ADEE
		public static bool operator >=(double left, double right)
		{
			return left >= right;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Double" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Double" /> object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E85 RID: 3717 RVA: 0x0003CBF7 File Offset: 0x0003ADF7
		public bool Equals(double obj)
		{
			return obj == this || (double.IsNaN(obj) && double.IsNaN(this));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E86 RID: 3718 RVA: 0x0003CC14 File Offset: 0x0003AE14
		[SecuritySafeCritical]
		public unsafe override int GetHashCode()
		{
			double num = this;
			if (num == 0.0)
			{
				return 0;
			}
			long num2 = *(long*)(&num);
			return (int)num2 ^ (int)(num2 >> 32);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E87 RID: 3719 RVA: 0x0003CC3F File Offset: 0x0003AE3F
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E88 RID: 3720 RVA: 0x0003CC4E File Offset: 0x0003AE4E
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E89 RID: 3721 RVA: 0x0003CC5D File Offset: 0x0003AE5D
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E8A RID: 3722 RVA: 0x0003CC6D File Offset: 0x0003AE6D
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its double-precision floating-point number equivalent.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E8B RID: 3723 RVA: 0x0003CC7D File Offset: 0x0003AE7D
		public static double Parse(string s)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its double-precision floating-point number equivalent.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is a combination of <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E8C RID: 3724 RVA: 0x0003CC8F File Offset: 0x0003AE8F
		public static double Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its double-precision floating-point number equivalent.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E8D RID: 3725 RVA: 0x0003CCA3 File Offset: 0x0003AEA3
		public static double Parse(string s, IFormatProvider provider)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its double-precision floating-point number equivalent.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a number to convert. </param>
		/// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a numeric value. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0003CCB6 File Offset: 0x0003AEB6
		public static double Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003CCCB File Offset: 0x0003AECB
		private static double Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseDouble(s, style, info);
		}

		/// <summary>Converts the string representation of a number to its double-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="result">When this method returns, contains the double-precision floating-point number equivalent to the <paramref name="s" /> parameter, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not a number in a valid format, or represents a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E90 RID: 3728 RVA: 0x0003CCD5 File Offset: 0x0003AED5
		public static bool TryParse(string s, out double result)
		{
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its double-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string containing a number to convert. </param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains a double-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />, or if <paramref name="style" /> is not a valid combination of <see cref="T:System.Globalization.NumberStyles" /> enumerated constants. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E91 RID: 3729 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003CD00 File Offset: 0x0003AF00
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out double result)
		{
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			if (!Number.TryParseDouble(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = double.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = double.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = double.NaN;
				}
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Double" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Double" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E93 RID: 3731 RVA: 0x0003CD83 File Offset: 0x0003AF83
		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E94 RID: 3732 RVA: 0x0003CD87 File Offset: 0x0003AF87
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />. </summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E95 RID: 3733 RVA: 0x0003CD90 File Offset: 0x0003AF90
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Double", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E96 RID: 3734 RVA: 0x0003CDB7 File Offset: 0x0003AFB7
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E97 RID: 3735 RVA: 0x0003CDC0 File Offset: 0x0003AFC0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E98 RID: 3736 RVA: 0x0003CDC9 File Offset: 0x0003AFC9
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored. </param>
		// Token: 0x06000E99 RID: 3737 RVA: 0x0003CDD2 File Offset: 0x0003AFD2
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E9A RID: 3738 RVA: 0x0003CDDB File Offset: 0x0003AFDB
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.   </param>
		// Token: 0x06000E9B RID: 3739 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E9C RID: 3740 RVA: 0x0003CDED File Offset: 0x0003AFED
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E9D RID: 3741 RVA: 0x0003CDF6 File Offset: 0x0003AFF6
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003CDFF File Offset: 0x0003AFFF
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0003CE08 File Offset: 0x0003B008
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003CE0C File Offset: 0x0003B00C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" /></summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003CE15 File Offset: 0x0003B015
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Double", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert this <see cref="T:System.Double" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003CE3C File Offset: 0x0003B03C
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040008E5 RID: 2277
		internal double m_value;

		/// <summary>Represents the smallest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008E6 RID: 2278
		public const double MinValue = -1.7976931348623157E+308;

		/// <summary>Represents the largest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008E7 RID: 2279
		public const double MaxValue = 1.7976931348623157E+308;

		/// <summary>Represents the smallest positive <see cref="T:System.Double" /> value that is greater than zero. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008E8 RID: 2280
		public const double Epsilon = 5E-324;

		/// <summary>Represents negative infinity. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008E9 RID: 2281
		public const double NegativeInfinity = double.NegativeInfinity;

		/// <summary>Represents positive infinity. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008EA RID: 2282
		public const double PositiveInfinity = double.PositiveInfinity;

		/// <summary>Represents a value that is not a number (NaN). This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008EB RID: 2283
		public const double NaN = double.NaN;

		// Token: 0x040008EC RID: 2284
		internal static double NegativeZero = BitConverter.Int64BitsToDouble(long.MinValue);
	}
}
