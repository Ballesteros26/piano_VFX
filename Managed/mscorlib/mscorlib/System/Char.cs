using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a character as a UTF-16 code unit.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000137 RID: 311
	[ComVisible(true)]
	[Serializable]
	public struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x00034CC8 File Offset: 0x00032EC8
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00034CD5 File Offset: 0x00032ED5
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00034CDF File Offset: 0x00032EDF
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.categoryForLatin1[(int)ch];
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3E RID: 2878 RVA: 0x00034CE8 File Offset: 0x00032EE8
		public override int GetHashCode()
		{
			return (int)(this | ((int)this << 16));
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Char" /> and equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B3F RID: 2879 RVA: 0x00034CF2 File Offset: 0x00032EF2
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified <see cref="T:System.Char" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter equals the value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B40 RID: 2880 RVA: 0x00034D08 File Offset: 0x00032F08
		public bool Equals(char obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.Return Value Description Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object to compare this instance to, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Char" /> object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B41 RID: 2881 RVA: 0x00034D0F File Offset: 0x00032F0F
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Char."));
			}
			return (int)(this - (char)value);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Char" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Char" /> object.</summary>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.Return Value Description Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />. </returns>
		/// <param name="value">A <see cref="T:System.Char" /> object to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B42 RID: 2882 RVA: 0x00034D37 File Offset: 0x00032F37
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B43 RID: 2883 RVA: 0x00034D3D File Offset: 0x00032F3D
		public override string ToString()
		{
			return char.ToString(this);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B44 RID: 2884 RVA: 0x00034D3D File Offset: 0x00032F3D
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		/// <summary>Converts the specified Unicode character to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of <paramref name="c" />.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B45 RID: 2885 RVA: 0x00034D46 File Offset: 0x00032F46
		public static string ToString(char c)
		{
			return new string(c, 1);
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character equivalent to the sole character in <paramref name="s" />.</returns>
		/// <param name="s">A string that contains a single character, or null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" /> is not 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B46 RID: 2886 RVA: 0x00034D4F File Offset: 0x00032F4F
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("String must be exactly one character long."));
			}
			return s[0];
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if the <paramref name="s" /> parameter was converted successfully; otherwise, false.</returns>
		/// <param name="s">A string that contains a single character, or null. </param>
		/// <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in <paramref name="s" />, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null or the length of <paramref name="s" /> is not 1. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B47 RID: 2887 RVA: 0x00034D7F File Offset: 0x00032F7F
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
		/// <returns>true if <paramref name="c" /> is a decimal digit; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B48 RID: 2888 RVA: 0x00034D9E File Offset: 0x00032F9E
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00034DC1 File Offset: 0x00032FC1
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a Unicode letter. </summary>
		/// <returns>true if <paramref name="c" /> is a letter; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4A RID: 2890 RVA: 0x00034DCA File Offset: 0x00032FCA
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00034E0A File Offset: 0x0003300A
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r') || c == '\u00a0' || c == '\u0085';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
		/// <returns>true if <paramref name="c" /> is white space; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4C RID: 2892 RVA: 0x00034E2E File Offset: 0x0003302E
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
		/// <returns>true if <paramref name="c" /> is an uppercase letter; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4D RID: 2893 RVA: 0x00034E45 File Offset: 0x00033045
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
		/// <returns>true if <paramref name="c" /> is a lowercase letter; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B4E RID: 2894 RVA: 0x00034E7A File Offset: 0x0003307A
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00034EAF File Offset: 0x000330AF
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
		/// <returns>true if <paramref name="c" /> is a punctuation mark; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B50 RID: 2896 RVA: 0x00034EBB File Offset: 0x000330BB
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00034EDC File Offset: 0x000330DC
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.</summary>
		/// <returns>true if <paramref name="c" /> is a letter or a decimal digit; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B52 RID: 2898 RVA: 0x00034EE9 File Offset: 0x000330E9
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
		/// <returns>The uppercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B53 RID: 2899 RVA: 0x00034F0A File Offset: 0x0003310A
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
		/// <returns>The uppercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B54 RID: 2900 RVA: 0x00034F26 File Offset: 0x00033126
		public static char ToUpper(char c)
		{
			return char.ToUpper(c, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
		/// <returns>The uppercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already uppercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B55 RID: 2901 RVA: 0x00034F33 File Offset: 0x00033133
		public static char ToUpperInvariant(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
		/// <returns>The lowercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B56 RID: 2902 RVA: 0x00034F40 File Offset: 0x00033140
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
		/// <returns>The lowercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B57 RID: 2903 RVA: 0x00034F5C File Offset: 0x0003315C
		public static char ToLower(char c)
		{
			return char.ToLower(c, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
		/// <returns>The lowercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B58 RID: 2904 RVA: 0x00034F69 File Offset: 0x00033169
		public static char ToLowerInvariant(char c)
		{
			return char.ToLower(c, CultureInfo.InvariantCulture);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Char" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Char" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B59 RID: 2905 RVA: 0x00028BDC File Offset: 0x00026DDC
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000B5A RID: 2906 RVA: 0x00034F76 File Offset: 0x00033176
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Char", "Boolean" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current <see cref="T:System.Char" /> object unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B5B RID: 2907 RVA: 0x00034F9D File Offset: 0x0003319D
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		/// <summary> For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B5C RID: 2908 RVA: 0x00034FA1 File Offset: 0x000331A1
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B5D RID: 2909 RVA: 0x00034FAA File Offset: 0x000331AA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary> For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B5E RID: 2910 RVA: 0x00034FB3 File Offset: 0x000331B3
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify null because the <paramref name="provider" /> parameter is ignored.)</param>
		// Token: 0x06000B5F RID: 2911 RVA: 0x00034FBC File Offset: 0x000331BC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B60 RID: 2912 RVA: 0x00034FC5 File Offset: 0x000331C5
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify null because the <paramref name="provider" /> parameter is ignored.)</param>
		// Token: 0x06000B61 RID: 2913 RVA: 0x00034FCE File Offset: 0x000331CE
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary> For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B62 RID: 2914 RVA: 0x00034FD7 File Offset: 0x000331D7
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify null because the <paramref name="provider" /> parameter is ignored.)</param>
		// Token: 0x06000B63 RID: 2915 RVA: 0x00034FE0 File Offset: 0x000331E0
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000B64 RID: 2916 RVA: 0x00034FE9 File Offset: 0x000331E9
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Char", "Single" }));
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000B65 RID: 2917 RVA: 0x00035010 File Offset: 0x00033210
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Char", "Double" }));
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000B66 RID: 2918 RVA: 0x00035037 File Offset: 0x00033237
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Char", "Decimal" }));
		}

		/// <summary>Note   This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000B67 RID: 2919 RVA: 0x0003505E File Offset: 0x0003325E
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Char", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>An object of the specified type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> object. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.Char" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter. </exception>
		// Token: 0x06000B68 RID: 2920 RVA: 0x00035085 File Offset: 0x00033285
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
		/// <returns>true if <paramref name="c" /> is a control character; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B69 RID: 2921 RVA: 0x00035095 File Offset: 0x00033295
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a control character.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a control character; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6A RID: 2922 RVA: 0x000350B4 File Offset: 0x000332B4
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a decimal digit; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6B RID: 2923 RVA: 0x0003510C File Offset: 0x0003330C
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a Unicode letter.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6C RID: 2924 RVA: 0x00035168 File Offset: 0x00033368
		public static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a letter or a decimal digit.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter or a decimal digit; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6D RID: 2925 RVA: 0x000351E0 File Offset: 0x000333E0
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a lowercase letter.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a lowercase letter; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B6E RID: 2926 RVA: 0x00035238 File Offset: 0x00033438
		public static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000352A3 File Offset: 0x000334A3
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
		/// <returns>true if <paramref name="c" /> is a number; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B70 RID: 2928 RVA: 0x000352AE File Offset: 0x000334AE
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a number.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a number; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B71 RID: 2929 RVA: 0x000352E8 File Offset: 0x000334E8
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a punctuation mark.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a punctuation mark; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B72 RID: 2930 RVA: 0x00035358 File Offset: 0x00033558
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000353AF File Offset: 0x000335AF
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000353BB File Offset: 0x000335BB
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
		/// <returns>true if <paramref name="c" /> is a separator character; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B75 RID: 2933 RVA: 0x000353CC File Offset: 0x000335CC
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a separator character.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a separator character; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B76 RID: 2934 RVA: 0x000353E8 File Offset: 0x000335E8
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the specified character has a surrogate code unit.</summary>
		/// <returns>true if <paramref name="c" /> is either a high surrogate or a low surrogate; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B77 RID: 2935 RVA: 0x0003543A File Offset: 0x0003363A
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		/// <summary>Indicates whether the character at the specified position in a specified string has a surrogate code unit.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a either a high surrogate or a low surrogate; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B78 RID: 2936 RVA: 0x00035451 File Offset: 0x00033651
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00035481 File Offset: 0x00033681
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.MathSymbol <= 3;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a symbol character.</summary>
		/// <returns>true if <paramref name="c" /> is a symbol character; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B7A RID: 2938 RVA: 0x0003548D File Offset: 0x0003368D
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a symbol character.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is a symbol character; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B7B RID: 2939 RVA: 0x000354B0 File Offset: 0x000336B0
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(s[index]));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as an uppercase letter.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is an uppercase letter; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B7C RID: 2940 RVA: 0x0003550C File Offset: 0x0003370C
		public static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as white space.</summary>
		/// <returns>true if the character at position <paramref name="index" /> in <paramref name="s" /> is white space; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B7D RID: 2941 RVA: 0x00035578 File Offset: 0x00033778
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		/// <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value that identifies the group that contains <paramref name="c" />.</returns>
		/// <param name="c">The Unicode character to categorize. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B7E RID: 2942 RVA: 0x000355C9 File Offset: 0x000337C9
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory((int)c);
		}

		/// <summary>Categorizes the character at the specified position in a specified string into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> enumerated constant that identifies the group that contains the character at position <paramref name="index" /> in <paramref name="s" />.</returns>
		/// <param name="s">A <see cref="T:System.String" />. </param>
		/// <param name="index">The character position in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B7F RID: 2943 RVA: 0x000355E0 File Offset: 0x000337E0
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		/// <summary>Converts the specified numeric Unicode character to a double-precision floating point number.</summary>
		/// <returns>The numeric value of <paramref name="c" /> if that character represents a number; otherwise, -1.0.</returns>
		/// <param name="c">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B80 RID: 2944 RVA: 0x00035631 File Offset: 0x00033831
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		/// <summary>Converts the numeric Unicode character at the specified position in a specified string to a double-precision floating point number.</summary>
		/// <returns>The numeric value of the character at position <paramref name="index" /> in <paramref name="s" /> if that character represents a number; otherwise, -1.</returns>
		/// <param name="s">A <see cref="T:System.String" />. </param>
		/// <param name="index">The character position in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B81 RID: 2945 RVA: 0x00035639 File Offset: 0x00033839
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValue(s, index);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a high surrogate.</summary>
		/// <returns>true if the numeric value of the <paramref name="c" /> parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
		/// <param name="c">The Unicode character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B82 RID: 2946 RVA: 0x00035664 File Offset: 0x00033864
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a high surrogate.</summary>
		/// <returns>true if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B83 RID: 2947 RVA: 0x0003567B File Offset: 0x0003387B
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a low surrogate.</summary>
		/// <returns>true if the numeric value of the <paramref name="c" /> parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
		/// <param name="c">The character to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B84 RID: 2948 RVA: 0x000356AF File Offset: 0x000338AF
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a low surrogate.</summary>
		/// <returns>true if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B85 RID: 2949 RVA: 0x000356C6 File Offset: 0x000338C6
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		/// <summary>Indicates whether two adjacent <see cref="T:System.Char" /> objects at a specified position in a string form a surrogate pair.</summary>
		/// <returns>true if the <paramref name="s" /> parameter includes adjacent characters at positions <paramref name="index" /> and <paramref name="index" /> + 1, and the numeric value of the character at position <paramref name="index" /> ranges from U+D800 through U+DBFF, and the numeric value of the character at position <paramref name="index" />+1 ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
		/// <param name="s">A string. </param>
		/// <param name="index">The starting position of the pair of characters to evaluate within <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B86 RID: 2950 RVA: 0x000356FC File Offset: 0x000338FC
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		/// <summary>Indicates whether the two specified <see cref="T:System.Char" /> objects form a surrogate pair.</summary>
		/// <returns>true if the numeric value of the <paramref name="highSurrogate" /> parameter ranges from U+D800 through U+DBFF, and the numeric value of the <paramref name="lowSurrogate" /> parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
		/// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair. </param>
		/// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B87 RID: 2951 RVA: 0x00035751 File Offset: 0x00033951
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		/// <summary>Converts the specified Unicode code point into a UTF-16 encoded string.</summary>
		/// <returns>A string consisting of one <see cref="T:System.Char" /> object or a surrogate pair of <see cref="T:System.Char" /> objects equivalent to the code point specified by the <paramref name="utf32" /> parameter.</returns>
		/// <param name="utf32">A 21-bit Unicode code point. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="utf32" /> is not a valid 21-bit Unicode code point ranging from U+0 through U+10FFFF, excluding the surrogate pair range from U+D800 through U+DFFF. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B88 RID: 2952 RVA: 0x0003577C File Offset: 0x0003397C
		public static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", Environment.GetResourceString("A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff)."));
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			return new string(new char[]
			{
				(char)(utf32 / 1024 + 55296),
				(char)(utf32 % 1024 + 56320)
			});
		}

		/// <summary>Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.</summary>
		/// <returns>The 21-bit Unicode code point represented by the <paramref name="highSurrogate" /> and <paramref name="lowSurrogate" /> parameters.</returns>
		/// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF). </param>
		/// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF). </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="highSurrogate" /> is not in the range U+D800 through U+DBFF, or <paramref name="lowSurrogate" /> is not in the range U+DC00 through U+DFFF. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B89 RID: 2953 RVA: 0x00035800 File Offset: 0x00033A00
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", Environment.GetResourceString("A valid high surrogate character is between 0xd800 and 0xdbff, inclusive."));
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", Environment.GetResourceString("A valid low surrogate character is between 0xdc00 and 0xdfff, inclusive."));
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		/// <summary>Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a Unicode code point.</summary>
		/// <returns>The 21-bit Unicode code point represented by the character or surrogate pair at the position in the <paramref name="s" /> parameter specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="s">A string that contains a character or surrogate pair. </param>
		/// <param name="index">The index position of the character or surrogate pair in <paramref name="s" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />. </exception>
		/// <exception cref="T:System.ArgumentException">The specified index position contains a surrogate pair, and either the first character in the pair is not a valid high surrogate or the second character in the pair is not a valid low surrogate. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B8A RID: 2954 RVA: 0x00035864 File Offset: 0x00033A64
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(Environment.GetResourceString("Found a low surrogate char without a preceding high surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", new object[] { index }), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", new object[] { index }), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(Environment.GetResourceString("Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.", new object[] { index }), "s");
		}

		// Token: 0x040007C8 RID: 1992
		internal char m_value;

		/// <summary>Represents the largest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007C9 RID: 1993
		public const char MaxValue = '\uffff';

		/// <summary>Represents the smallest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007CA RID: 1994
		public const char MinValue = '\0';

		// Token: 0x040007CB RID: 1995
		private static readonly byte[] categoryForLatin1 = new byte[]
		{
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 11, 24, 24, 24, 26, 24, 24, 24,
			20, 21, 24, 25, 24, 19, 24, 24, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 24, 24,
			25, 25, 25, 24, 24, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 20, 24, 21, 27, 18, 27, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 20, 25, 21, 25, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			11, 24, 26, 26, 26, 26, 28, 28, 27, 28,
			1, 22, 25, 19, 28, 27, 28, 25, 10, 10,
			27, 1, 28, 24, 27, 10, 1, 23, 10, 10,
			10, 24, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 25, 0, 0, 0, 0,
			0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 25, 1, 1,
			1, 1, 1, 1, 1, 1
		};

		// Token: 0x040007CC RID: 1996
		internal const int UNICODE_PLANE00_END = 65535;

		// Token: 0x040007CD RID: 1997
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x040007CE RID: 1998
		internal const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x040007CF RID: 1999
		internal const int HIGH_SURROGATE_START = 55296;

		// Token: 0x040007D0 RID: 2000
		internal const int LOW_SURROGATE_END = 57343;
	}
}
