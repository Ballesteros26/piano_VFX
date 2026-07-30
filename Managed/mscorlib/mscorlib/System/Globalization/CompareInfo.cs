using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using Mono.Globalization.Unicode;
using Unity;

namespace System.Globalization
{
	/// <summary>Implements a set of methods for culture-sensitive string comparisons.</summary>
	// Token: 0x020003FF RID: 1023
	[ComVisible(true)]
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x0600303D RID: 12349 RVA: 0x000AC75B File Offset: 0x000AA95B
		internal CompareInfo(CultureInfo culture)
		{
			this.m_name = culture.m_name;
			this.m_sortName = culture.SortName;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="culture">An integer representing the culture identifier. </param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is of an invalid type. </exception>
		// Token: 0x0600303E RID: 12350 RVA: 0x000AC77C File Offset: 0x000AA97C
		public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException(Environment.GetResourceString("Only mscorlib's assembly is valid."));
			}
			return CompareInfo.GetCompareInfo(culture);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="name">A string representing the culture name. </param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="assembly" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name.-or- <paramref name="assembly" /> is of an invalid type. </exception>
		// Token: 0x0600303F RID: 12351 RVA: 0x000AC7D0 File Offset: 0x000AA9D0
		public static CompareInfo GetCompareInfo(string name, Assembly assembly)
		{
			if (name == null || assembly == null)
			{
				throw new ArgumentNullException((name == null) ? "name" : "assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException(Environment.GetResourceString("Only mscorlib's assembly is valid."));
			}
			return CompareInfo.GetCompareInfo(name);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified identifier.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="culture">An integer representing the culture identifier. </param>
		// Token: 0x06003040 RID: 12352 RVA: 0x000AC830 File Offset: 0x000AAA30
		public static CompareInfo GetCompareInfo(int culture)
		{
			if (CultureData.IsCustomCultureId(culture))
			{
				throw new ArgumentException(Environment.GetResourceString("Customized cultures cannot be passed by LCID, only by name.", new object[] { "culture" }));
			}
			return CultureInfo.GetCultureInfo(culture).CompareInfo;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified name.</summary>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="name">A string representing the culture name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name. </exception>
		// Token: 0x06003041 RID: 12353 RVA: 0x000AC863 File Offset: 0x000AAA63
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return CultureInfo.GetCultureInfo(name).CompareInfo;
		}

		/// <summary>Indicates whether a specified Unicode character is sortable.</summary>
		/// <returns>true if the <paramref name="ch" /> parameter is sortable; otherwise, false.</returns>
		/// <param name="ch">A Unicode character.</param>
		// Token: 0x06003042 RID: 12354 RVA: 0x000AC87E File Offset: 0x000AAA7E
		[ComVisible(false)]
		public static bool IsSortable(char ch)
		{
			return CompareInfo.IsSortable(ch.ToString());
		}

		/// <summary>Indicates whether a specified Unicode string is sortable.</summary>
		/// <returns>true if the <paramref name="str" /> parameter is not an empty string ("") and all the Unicode characters in <paramref name="str" /> are sortable; otherwise, false.</returns>
		/// <param name="text">A string of zero or more Unicode characters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x06003043 RID: 12355 RVA: 0x000AC88C File Offset: 0x000AAA8C
		[SecuritySafeCritical]
		[ComVisible(false)]
		public static bool IsSortable(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return text.Length != 0 && MSCompatUnicodeTable.IsSortable(text);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000AC8AC File Offset: 0x000AAAAC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_name = null;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000AC8B8 File Offset: 0x000AAAB8
		private void OnDeserialized()
		{
			CultureInfo cultureInfo;
			if (this.m_name == null)
			{
				cultureInfo = CultureInfo.GetCultureInfo(this.culture);
				this.m_name = cultureInfo.m_name;
			}
			else
			{
				cultureInfo = CultureInfo.GetCultureInfo(this.m_name);
			}
			this.m_sortName = cultureInfo.SortName;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000AC8FF File Offset: 0x000AAAFF
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000AC907 File Offset: 0x000AAB07
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. </param>
		// Token: 0x06003048 RID: 12360 RVA: 0x000AC8FF File Offset: 0x000AAAFF
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		/// <summary>Gets the name of the culture used for sorting operations by this <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x000AC91F File Offset: 0x000AAB1F
		[ComVisible(false)]
		public virtual string Name
		{
			get
			{
				if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
				{
					return this.m_name;
				}
				return this.m_sortName;
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000AC954 File Offset: 0x000AAB54
		internal static int GetNativeCompareFlags(CompareOptions options)
		{
			int num = 134217728;
			if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
			{
				num |= 1;
			}
			if ((options & CompareOptions.IgnoreKanaType) != CompareOptions.None)
			{
				num |= 65536;
			}
			if ((options & CompareOptions.IgnoreNonSpace) != CompareOptions.None)
			{
				num |= 2;
			}
			if ((options & CompareOptions.IgnoreSymbols) != CompareOptions.None)
			{
				num |= 4;
			}
			if ((options & CompareOptions.IgnoreWidth) != CompareOptions.None)
			{
				num |= 131072;
			}
			if ((options & CompareOptions.StringSort) != CompareOptions.None)
			{
				num |= 4096;
			}
			if (options == CompareOptions.Ordinal)
			{
				num = 1073741824;
			}
			return num;
		}

		/// <summary>Compares two strings. </summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater than <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		// Token: 0x0600304B RID: 12363 RVA: 0x000AC9BD File Offset: 0x000AABBD
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		/// <summary>Compares two strings using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero <paramref name="string1" /> is less than <paramref name="string2" />. greater than zero <paramref name="string1" /> is greater than <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600304C RID: 12364 RVA: 0x000AC9C8 File Offset: 0x000AABC8
		[SecuritySafeCritical]
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException(Environment.GetResourceString("CompareOption.Ordinal cannot be used with other options."), "options");
				}
				return string.CompareOrdinal(string1, string2);
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					return this.internal_compare_switch(string1, 0, string1.Length, string2, 0, string2.Length, options);
				}
			}
		}

		/// <summary>Compares a section of one string with a section of another string.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.-or- <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />. </exception>
		// Token: 0x0600304D RID: 12365 RVA: 0x000ACA58 File Offset: 0x000AAC58
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
		}

		/// <summary>Compares the end section of a string with the end section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600304E RID: 12366 RVA: 0x000ACA6A File Offset: 0x000AAC6A
		public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
		{
			return this.Compare(string1, offset1, (string1 == null) ? 0 : (string1.Length - offset1), string2, offset2, (string2 == null) ? 0 : (string2.Length - offset2), options);
		}

		/// <summary>Compares the end section of a string with the end section of another string.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />. </exception>
		// Token: 0x0600304F RID: 12367 RVA: 0x000ACA96 File Offset: 0x000AAC96
		public virtual int Compare(string string1, int offset1, string string2, int offset2)
		{
			return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
		}

		/// <summary>Compares a section of one string with a section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition zero The two strings are equal. less than zero The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />. greater than zero The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />. </returns>
		/// <param name="string1">The first string to compare. </param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing. </param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare. </param>
		/// <param name="string2">The second string to compare. </param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing. </param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare. </param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.-or- <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.-or- <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.-or- <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.-or- <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003050 RID: 12368 RVA: 0x000ACAA4 File Offset: 0x000AACA4
		[SecuritySafeCritical]
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				int num = string.Compare(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2, StringComparison.OrdinalIgnoreCase);
				if (length1 == length2 || num != 0)
				{
					return num;
				}
				if (length1 <= length2)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (length1 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", Environment.GetResourceString("Positive number required."));
				}
				if (offset1 < 0 || offset2 < 0)
				{
					throw new ArgumentOutOfRangeException((offset1 < 0) ? "offset1" : "offset2", Environment.GetResourceString("Positive number required."));
				}
				if (offset1 > ((string1 == null) ? 0 : string1.Length) - length1)
				{
					throw new ArgumentOutOfRangeException("string1", Environment.GetResourceString("Offset and length must refer to a position in the string."));
				}
				if (offset2 > ((string2 == null) ? 0 : string2.Length) - length2)
				{
					throw new ArgumentOutOfRangeException("string2", Environment.GetResourceString("Offset and length must refer to a position in the string."));
				}
				if ((options & CompareOptions.Ordinal) != CompareOptions.None)
				{
					if (options != CompareOptions.Ordinal)
					{
						throw new ArgumentException(Environment.GetResourceString("CompareOption.Ordinal cannot be used with other options."), "options");
					}
				}
				else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					if (options == CompareOptions.Ordinal)
					{
						return CompareInfo.CompareOrdinal(string1, offset1, length1, string2, offset2, length2);
					}
					return this.internal_compare_switch(string1, offset1, length1, string2, offset2, length2, options);
				}
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000ACC08 File Offset: 0x000AAE08
		[SecurityCritical]
		private static int CompareOrdinal(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			int num = string.nativeCompareOrdinalEx(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2);
			if (length1 == length2 || num != 0)
			{
				return num;
			}
			if (length1 <= length2)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>true if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="prefix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003052 RID: 12370 RVA: 0x000ACC3C File Offset: 0x000AAE3C
		[SecuritySafeCritical]
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null || prefix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "prefix", Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.StartsWith(prefix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsPrefix(source, prefix, options);
			}
			return source.Length >= prefix.Length && this.Compare(source, 0, prefix.Length, prefix, 0, prefix.Length, options) == 0;
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix.</summary>
		/// <returns>true if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="prefix" /> is null. </exception>
		// Token: 0x06003053 RID: 12371 RVA: 0x000ACCF5 File Offset: 0x000AAEF5
		public virtual bool IsPrefix(string source, string prefix)
		{
			return this.IsPrefix(source, prefix, CompareOptions.None);
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>true if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="suffix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003054 RID: 12372 RVA: 0x000ACD00 File Offset: 0x000AAF00
		[SecuritySafeCritical]
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null || suffix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "suffix", Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			if (suffix.Length == 0)
			{
				return true;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.EndsWith(suffix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsSuffix(source, suffix, options);
			}
			return source.Length >= suffix.Length && this.Compare(source, source.Length - suffix.Length, suffix.Length, suffix, 0, suffix.Length, options) == 0;
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix.</summary>
		/// <returns>true if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, false.</returns>
		/// <param name="source">The string to search in. </param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="suffix" /> is null. </exception>
		// Token: 0x06003055 RID: 12373 RVA: 0x000ACDC5 File Offset: 0x000AAFC5
		public virtual bool IsSuffix(string source, string suffix)
		{
			return this.IsSuffix(source, suffix, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		// Token: 0x06003056 RID: 12374 RVA: 0x000ACDD0 File Offset: 0x000AAFD0
		public virtual int IndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		// Token: 0x06003057 RID: 12375 RVA: 0x000ACDF0 File Offset: 0x000AAFF0
		public virtual int IndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="options">A value that defines how the strings should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003058 RID: 12376 RVA: 0x000ACE10 File Offset: 0x000AB010
		public virtual int IndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003059 RID: 12377 RVA: 0x000ACE30 File Offset: 0x000AB030
		public virtual int IndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x0600305A RID: 12378 RVA: 0x000ACE50 File Offset: 0x000AB050
		public virtual int IndexOf(string source, char value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x0600305B RID: 12379 RVA: 0x000ACE72 File Offset: 0x000AB072
		public virtual int IndexOf(string source, string value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600305C RID: 12380 RVA: 0x000ACE94 File Offset: 0x000AB094
		public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600305D RID: 12381 RVA: 0x000ACEB7 File Offset: 0x000AB0B7
		public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x0600305E RID: 12382 RVA: 0x000ACEDA File Offset: 0x000AB0DA
		public virtual int IndexOf(string source, char value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x0600305F RID: 12383 RVA: 0x000ACEE8 File Offset: 0x000AB0E8
		public virtual int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003060 RID: 12384 RVA: 0x000ACEF8 File Offset: 0x000AB0F8
		[SecuritySafeCritical]
		public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0 || startIndex > source.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			return this.internal_index_switch(source, startIndex, count, value, options, true);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003061 RID: 12385 RVA: 0x000ACFAC File Offset: 0x000AB1AC
		[SecuritySafeCritical]
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (count < 0 || startIndex > source.Length - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return source.IndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
				}
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
				}
				return this.internal_index_switch(source, startIndex, count, value, options, true);
			}
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		// Token: 0x06003062 RID: 12386 RVA: 0x000AD08E File Offset: 0x000AB28E
		public virtual int LastIndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		// Token: 0x06003063 RID: 12387 RVA: 0x000AD0B5 File Offset: 0x000AB2B5
		public virtual int LastIndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003064 RID: 12388 RVA: 0x000AD0DC File Offset: 0x000AB2DC
		public virtual int LastIndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003065 RID: 12389 RVA: 0x000AD103 File Offset: 0x000AB303
		public virtual int LastIndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x06003066 RID: 12390 RVA: 0x000AD12A File Offset: 0x000AB32A
		public virtual int LastIndexOf(string source, char value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		// Token: 0x06003067 RID: 12391 RVA: 0x000AD139 File Offset: 0x000AB339
		public virtual int LastIndexOf(string source, string value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003068 RID: 12392 RVA: 0x000AD148 File Offset: 0x000AB348
		public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x06003069 RID: 12393 RVA: 0x000AD158 File Offset: 0x000AB358
		public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x0600306A RID: 12394 RVA: 0x000AD168 File Offset: 0x000AB368
		public virtual int LastIndexOf(string source, char value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		// Token: 0x0600306B RID: 12395 RVA: 0x000AD176 File Offset: 0x000AB376
		public virtual int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The character to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600306C RID: 12396 RVA: 0x000AD184 File Offset: 0x000AB384
		[SecuritySafeCritical]
		public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				return -1;
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (startIndex == source.Length)
			{
				startIndex--;
				if (count > 0)
				{
					count--;
				}
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.LastIndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			return this.internal_index_switch(source, startIndex, count, value, options, false);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character. </returns>
		/// <param name="source">The string to search. </param>
		/// <param name="value">The string to locate within <paramref name="source" />. </param>
		/// <param name="startIndex">The zero-based starting index of the backward search. </param>
		/// <param name="count">The number of elements in the section to search. </param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.-or- <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.-or- <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		// Token: 0x0600306D RID: 12397 RVA: 0x000AD268 File Offset: 0x000AB468
		[SecuritySafeCritical]
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > source.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (startIndex == source.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
					if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
					{
						return startIndex;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return source.LastIndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
				}
				return this.internal_index_switch(source, startIndex, count, value, options, false);
			}
		}

		/// <summary>Gets a <see cref="T:System.Globalization.SortKey" /> object for the specified string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained. </param>
		/// <param name="options">A bitwise combination of one or more of the following enumeration values that define how the sort key is calculated: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600306E RID: 12398 RVA: 0x000AD373 File Offset: 0x000AB573
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			return this.CreateSortKey(source, options);
		}

		/// <summary>Gets the sort key for the specified string.</summary>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600306F RID: 12399 RVA: 0x000AD37D File Offset: 0x000AB57D
		public virtual SortKey GetSortKey(string source)
		{
			return this.CreateSortKey(source, CompareOptions.None);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000AD388 File Offset: 0x000AB588
		[SecuritySafeCritical]
		private SortKey CreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			if (string.IsNullOrEmpty(source))
			{
				source = "\0";
			}
			return this.CreateSortKeyCore(source, options);
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>true if the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" />; otherwise, false.</returns>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CompareInfo" />. </param>
		// Token: 0x06003071 RID: 12401 RVA: 0x000AD3D8 File Offset: 0x000AB5D8
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && this.Name == compareInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CompareInfo" /> for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x06003072 RID: 12402 RVA: 0x000AD402 File Offset: 0x000AB602
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000AD40F File Offset: 0x000AB60F
		public virtual int GetHashCode(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.GetHashCode();
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return TextInfo.GetHashCodeOrdinalIgnoreCase(source);
			}
			return this.GetHashCodeOfString(source, options, false, 0L);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000AD448 File Offset: 0x000AB648
		internal int GetHashCodeOfString(string source, CompareOptions options)
		{
			return this.GetHashCodeOfString(source, options, false, 0L);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000AD458 File Offset: 0x000AB658
		[SecuritySafeCritical]
		internal int GetHashCodeOfString(string source, CompareOptions options, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid."), "options");
			}
			if (source.Length == 0)
			{
				return 0;
			}
			return this.GetSortKey(source, options).GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</returns>
		// Token: 0x06003076 RID: 12406 RVA: 0x000AD4A5 File Offset: 0x000AB6A5
		public override string ToString()
		{
			return "CompareInfo - " + this.Name;
		}

		/// <summary>Gets the properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</summary>
		/// <returns>The properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x000AD4B7 File Offset: 0x000AB6B7
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.Name).LCID;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000AD4C9 File Offset: 0x000AB6C9
		internal static bool IsLegacy20SortingBehaviorRequested
		{
			get
			{
				return CompareInfo.InternalSortVersion == 4096U;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000AD4D7 File Offset: 0x000AB6D7
		private static uint InternalSortVersion
		{
			[SecuritySafeCritical]
			get
			{
				return 393473U;
			}
		}

		/// <summary>Gets information about the version of Unicode used for comparing and sorting strings.</summary>
		/// <returns>An object that contains information about the Unicode version used for comparing and sorting strings.</returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000AD4DE File Offset: 0x000AB6DE
		public SortVersion Version
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_SortVersion == null)
				{
					this.m_SortVersion = new SortVersion(393473, new Guid("00000001-57ee-1e5c-00b4-d0000bb1e11e"));
				}
				return this.m_SortVersion;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x000AD50E File Offset: 0x000AB70E
		private static bool UseManagedCollation
		{
			get
			{
				if (!CompareInfo.managedCollationChecked)
				{
					CompareInfo.managedCollation = Environment.internalGetEnvironmentVariable("MONO_DISABLE_MANAGED_COLLATION") != "yes" && MSCompatUnicodeTable.IsReady;
					CompareInfo.managedCollationChecked = true;
				}
				return CompareInfo.managedCollation;
			}
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000AD548 File Offset: 0x000AB748
		private SimpleCollator GetCollator()
		{
			if (this.collator != null)
			{
				return this.collator;
			}
			if (CompareInfo.collators == null)
			{
				Interlocked.CompareExchange<Dictionary<string, SimpleCollator>>(ref CompareInfo.collators, new Dictionary<string, SimpleCollator>(StringComparer.Ordinal), null);
			}
			Dictionary<string, SimpleCollator> dictionary = CompareInfo.collators;
			lock (dictionary)
			{
				if (!CompareInfo.collators.TryGetValue(this.m_sortName, out this.collator))
				{
					this.collator = new SimpleCollator(CultureInfo.GetCultureInfo(this.m_name));
					CompareInfo.collators[this.m_sortName] = this.collator;
				}
			}
			return this.collator;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000AD5F8 File Offset: 0x000AB7F8
		private SortKey CreateSortKeyCore(string source, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().GetSortKey(source, options);
			}
			SortKey sortKey = new SortKey(this.culture, source, options);
			this.assign_sortkey(sortKey, source, options);
			return sortKey;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000AD634 File Offset: 0x000AB834
		private int internal_index_switch(string s, int sindex, int count, char c, CompareOptions opt, bool first)
		{
			if (opt == CompareOptions.Ordinal && first)
			{
				return s.IndexOfUnchecked(c, sindex, count);
			}
			if (!CompareInfo.UseManagedCollation)
			{
				return this.internal_index(s, sindex, count, c, opt, first);
			}
			return this.internal_index_managed(s, sindex, count, c, opt, first);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000AD680 File Offset: 0x000AB880
		private int internal_index_switch(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (opt == CompareOptions.Ordinal && first)
			{
				return s1.IndexOfUnchecked(s2, sindex, count);
			}
			if (!CompareInfo.UseManagedCollation)
			{
				return this.internal_index(s1, sindex, count, s2, opt, first);
			}
			return this.internal_index_managed(s1, sindex, count, s2, opt, first);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000AD6CC File Offset: 0x000AB8CC
		private int internal_compare_switch(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			if (!CompareInfo.UseManagedCollation)
			{
				return this.internal_compare(str1, offset1, length1, str2, offset2, length2, options);
			}
			return this.internal_compare_managed(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000AD6F8 File Offset: 0x000AB8F8
		private int internal_compare_managed(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return this.GetCollator().Compare(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000AD710 File Offset: 0x000AB910
		private int internal_index_managed(string s, int sindex, int count, char c, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s, c, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s, c, sindex, count, opt);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000AD73B File Offset: 0x000AB93B
		private int internal_index_managed(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s1, s2, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s1, s2, sindex, count, opt);
		}

		// Token: 0x06003084 RID: 12420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void assign_sortkey(object key, string source, CompareOptions options);

		// Token: 0x06003085 RID: 12421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_compare(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options);

		// Token: 0x06003086 RID: 12422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_index(string source, int sindex, int count, char value, CompareOptions options, bool first);

		// Token: 0x06003087 RID: 12423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int internal_index(string source, int sindex, int count, string value, CompareOptions options, bool first);

		// Token: 0x06003088 RID: 12424 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal CompareInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001922 RID: 6434
		private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x04001923 RID: 6435
		private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x04001924 RID: 6436
		private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x04001925 RID: 6437
		[OptionalField(VersionAdded = 2)]
		private string m_name;

		// Token: 0x04001926 RID: 6438
		[NonSerialized]
		private string m_sortName;

		// Token: 0x04001927 RID: 6439
		[OptionalField(VersionAdded = 1)]
		private int win32LCID;

		// Token: 0x04001928 RID: 6440
		private int culture;

		// Token: 0x04001929 RID: 6441
		private const int LINGUISTIC_IGNORECASE = 16;

		// Token: 0x0400192A RID: 6442
		private const int NORM_IGNORECASE = 1;

		// Token: 0x0400192B RID: 6443
		private const int NORM_IGNOREKANATYPE = 65536;

		// Token: 0x0400192C RID: 6444
		private const int LINGUISTIC_IGNOREDIACRITIC = 32;

		// Token: 0x0400192D RID: 6445
		private const int NORM_IGNORENONSPACE = 2;

		// Token: 0x0400192E RID: 6446
		private const int NORM_IGNORESYMBOLS = 4;

		// Token: 0x0400192F RID: 6447
		private const int NORM_IGNOREWIDTH = 131072;

		// Token: 0x04001930 RID: 6448
		private const int SORT_STRINGSORT = 4096;

		// Token: 0x04001931 RID: 6449
		private const int COMPARE_OPTIONS_ORDINAL = 1073741824;

		// Token: 0x04001932 RID: 6450
		internal const int NORM_LINGUISTIC_CASING = 134217728;

		// Token: 0x04001933 RID: 6451
		private const int RESERVED_FIND_ASCII_STRING = 536870912;

		// Token: 0x04001934 RID: 6452
		private const int SORT_VERSION_WHIDBEY = 4096;

		// Token: 0x04001935 RID: 6453
		private const int SORT_VERSION_V4 = 393473;

		// Token: 0x04001936 RID: 6454
		[OptionalField(VersionAdded = 3)]
		private SortVersion m_SortVersion;

		// Token: 0x04001937 RID: 6455
		[NonSerialized]
		private SimpleCollator collator;

		// Token: 0x04001938 RID: 6456
		private static Dictionary<string, SimpleCollator> collators;

		// Token: 0x04001939 RID: 6457
		private static bool managedCollation;

		// Token: 0x0400193A RID: 6458
		private static bool managedCollationChecked;
	}
}
