using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a Boolean value.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000133 RID: 307
	[ComVisible(true)]
	[Serializable]
	public struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Boolean" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x000342DD File Offset: 0x000324DD
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is true, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEA RID: 2794 RVA: 0x000342E6 File Offset: 0x000324E6
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is true, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is false.</returns>
		/// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider" /> object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEB RID: 2795 RVA: 0x000342E6 File Offset: 0x000324E6
		public string ToString(IFormatProvider provider)
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Boolean" /> and has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEC RID: 2796 RVA: 0x000342F7 File Offset: 0x000324F7
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Boolean" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> has the same value as this instance; otherwise, false.</returns>
		/// <param name="obj">A <see cref="T:System.Boolean" /> value to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AED RID: 2797 RVA: 0x0003430D File Offset: 0x0003250D
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates their relationship to one another.</summary>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="obj" />.Return Value Condition Less than zero This instance is false and <paramref name="obj" /> is true. Zero This instance and <paramref name="obj" /> are equal (either both are true or both are false). Greater than zero This instance is true and <paramref name="obj" /> is false.-or- <paramref name="obj" /> is null. </returns>
		/// <param name="obj">An object to compare to this instance, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Boolean" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEE RID: 2798 RVA: 0x00034314 File Offset: 0x00032514
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Boolean."));
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Boolean" /> object and returns an integer that indicates their relationship to one another.</summary>
		/// <returns>A signed integer that indicates the relative values of this instance and <paramref name="value" />.Return Value Condition Less than zero This instance is false and <paramref name="value" /> is true. Zero This instance and <paramref name="value" /> are equal (either both are true or both are false). Greater than zero This instance is true and <paramref name="value" /> is false. </returns>
		/// <param name="value">A <see cref="T:System.Boolean" /> object to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AEF RID: 2799 RVA: 0x00034346 File Offset: 0x00032546
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Converts the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent, or throws an exception if the string is not equal to the value of <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</summary>
		/// <returns>true if <paramref name="value" /> is equal to the value of the <see cref="F:System.Boolean.TrueString" /> field; false if <paramref name="value" /> is equal to the value of the <see cref="F:System.Boolean.FalseString" /> field.</returns>
		/// <param name="value">A string containing the value to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to the value of the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x00034358 File Offset: 0x00032558
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			bool flag = false;
			if (!bool.TryParse(value, out flag))
			{
				throw new FormatException(Environment.GetResourceString("String was not recognized as a valid Boolean."));
			}
			return flag;
		}

		/// <summary>Tries to convert the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="value" /> was converted successfully; otherwise, false.</returns>
		/// <param name="value">A string containing the value to convert. </param>
		/// <param name="result">When this method returns, if the conversion succeeded, contains true if <paramref name="value" /> is equal to <see cref="F:System.Boolean.TrueString" /> or false if <paramref name="value" /> is equal to <see cref="F:System.Boolean.FalseString" />. If the conversion failed, contains false. The conversion fails if <paramref name="value" /> is null or is not equal to the value of either the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00034390 File Offset: 0x00032590
		public static bool TryParse(string value, out bool result)
		{
			result = false;
			if (value == null)
			{
				return false;
			}
			if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
				return true;
			}
			if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
				return true;
			}
			if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000343FC File Offset: 0x000325FC
		private static string TrimWhiteSpaceAndNull(string value)
		{
			int i = 0;
			int num = value.Length - 1;
			char c = '\0';
			while (i < value.Length)
			{
				if (!char.IsWhiteSpace(value[i]) && value[i] != c)
				{
					IL_0052:
					while (num >= i && (char.IsWhiteSpace(value[num]) || value[num] == c))
					{
						num--;
					}
					return value.Substring(i, num - i + 1);
				}
				i++;
			}
			goto IL_0052;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Boolean" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Boolean" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00028538 File Offset: 0x00026738
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true or false.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0003446B File Offset: 0x0003266B
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.Char" /> value. This conversion is not supported.</exception>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003446F File Offset: 0x0003266F
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Boolean", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AF6 RID: 2806 RVA: 0x00034496 File Offset: 0x00032696
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />. </summary>
		/// <returns>1 if the value of this instance is true; otherwise, 0. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003449F File Offset: 0x0003269F
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AF8 RID: 2808 RVA: 0x000344A8 File Offset: 0x000326A8
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AF9 RID: 2809 RVA: 0x000344B1 File Offset: 0x000326B1
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AFA RID: 2810 RVA: 0x000344BA File Offset: 0x000326BA
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AFB RID: 2811 RVA: 0x000344C3 File Offset: 0x000326C3
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AFC RID: 2812 RVA: 0x000344CC File Offset: 0x000326CC
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />. </summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AFD RID: 2813 RVA: 0x000344D5 File Offset: 0x000326D5
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AFE RID: 2814 RVA: 0x000344DE File Offset: 0x000326DE
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000AFF RID: 2815 RVA: 0x000344E7 File Offset: 0x000326E7
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />..</summary>
		/// <returns>1 if this instance is true; otherwise, 0.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000B00 RID: 2816 RVA: 0x000344F0 File Offset: 0x000326F0
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.DateTime" /> value. This conversion is not supported.</exception>
		// Token: 0x06000B01 RID: 2817 RVA: 0x000344F9 File Offset: 0x000326F9
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Boolean", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />. </summary>
		/// <returns>An object of the specified type, with a value that is equivalent to the value of this Boolean object.</returns>
		/// <param name="type">The desired type. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x06000B02 RID: 2818 RVA: 0x00034520 File Offset: 0x00032720
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040007BE RID: 1982
		private bool m_value;

		// Token: 0x040007BF RID: 1983
		internal const int True = 1;

		// Token: 0x040007C0 RID: 1984
		internal const int False = 0;

		// Token: 0x040007C1 RID: 1985
		internal const string TrueLiteral = "True";

		// Token: 0x040007C2 RID: 1986
		internal const string FalseLiteral = "False";

		/// <summary>Represents the Boolean value true as a string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007C3 RID: 1987
		public static readonly string TrueString = "True";

		/// <summary>Represents the Boolean value false as a string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040007C4 RID: 1988
		public static readonly string FalseString = "False";
	}
}
