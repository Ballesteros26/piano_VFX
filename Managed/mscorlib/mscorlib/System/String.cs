using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>Represents text as a series of Unicode characters.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C2 RID: 450
	[ComVisible(true)]
	[Serializable]
	public sealed class String : IComparable, ICloneable, IConvertible, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
	{
		/// <summary>Concatenates all the elements of a string array, using the specified separator between each element. </summary>
		/// <returns>A string that consists of the elements in <paramref name="value" /> delimited by the <paramref name="separator" /> string. If <paramref name="value" /> is an empty array, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="value" /> has more than one element.</param>
		/// <param name="value">An array that contains the elements to concatenate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001336 RID: 4918 RVA: 0x0004DC5A File Offset: 0x0004BE5A
		public static string Join(string separator, params string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return string.Join(separator, value, 0, value.Length);
		}

		/// <summary>Concatenates the elements of an object array, using the specified separator between each element.</summary>
		/// <returns>A string that consists of the elements of <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> is an empty array, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="values" /> has more than one element.</param>
		/// <param name="values">An array that contains the elements to concatenate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x06001337 RID: 4919 RVA: 0x0004DC78 File Offset: 0x0004BE78
		[ComVisible(false)]
		public static string Join(string separator, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length == 0 || values[0] == null)
			{
				return string.Empty;
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			string text = values[0].ToString();
			if (text != null)
			{
				stringBuilder.Append(text);
			}
			for (int i = 1; i < values.Length; i++)
			{
				stringBuilder.Append(separator);
				if (values[i] != null)
				{
					text = values[i].ToString();
					if (text != null)
					{
						stringBuilder.Append(text);
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Concatenates the members of a collection, using the specified separator between each member.</summary>
		/// <returns>A string that consists of the members of <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> has no members, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="values" /> has more than one element.</param>
		/// <param name="values">A collection that contains the objects to concatenate.</param>
		/// <typeparam name="T">The type of the members of <paramref name="values" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x06001338 RID: 4920 RVA: 0x0004DD00 File Offset: 0x0004BF00
		[ComVisible(false)]
		public static string Join<T>(string separator, IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string text;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text2 = t.ToString();
						if (text2 != null)
						{
							stringBuilder.Append(text2);
						}
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							T t = enumerator.Current;
							string text3 = t.ToString();
							if (text3 != null)
							{
								stringBuilder.Append(text3);
							}
						}
					}
					text = StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}
			return text;
		}

		/// <summary>Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection of type <see cref="T:System.String" />, using the specified separator between each member.</summary>
		/// <returns>A string that consists of the members of <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> has no members, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="values" /> has more than one element.</param>
		/// <param name="values">A collection that contains the strings to concatenate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x06001339 RID: 4921 RVA: 0x0004DDDC File Offset: 0x0004BFDC
		[ComVisible(false)]
		public static string Join(string separator, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string text;
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					text = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (enumerator.Current != null)
					{
						stringBuilder.Append(enumerator.Current);
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							stringBuilder.Append(enumerator.Current);
						}
					}
					text = StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}
			return text;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x0004DE80 File Offset: 0x0004C080
		internal char FirstChar
		{
			get
			{
				return this.m_firstChar;
			}
		}

		/// <summary>Concatenates the specified elements of a string array, using the specified separator between each element. </summary>
		/// <returns>A string that consists of the strings in <paramref name="value" /> delimited by the <paramref name="separator" /> string. -or-<see cref="F:System.String.Empty" /> if <paramref name="count" /> is zero, <paramref name="value" /> has no elements, or <paramref name="separator" /> and all the elements of <paramref name="value" /> are <see cref="F:System.String.Empty" />.</returns>
		/// <param name="separator">The string to use as a separator. <paramref name="separator" /> is included in the returned string only if <paramref name="value" /> has more than one element.</param>
		/// <param name="value">An array that contains the elements to concatenate. </param>
		/// <param name="startIndex">The first element in <paramref name="value" /> to use. </param>
		/// <param name="count">The number of elements of <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is less than 0.-or- <paramref name="startIndex" /> plus <paramref name="count" /> is greater than the number of elements in <paramref name="value" />. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600133B RID: 4923 RVA: 0x0004DE88 File Offset: 0x0004C088
		[SecuritySafeCritical]
		public unsafe static string Join(string separator, string[] value, int startIndex, int count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (startIndex > value.Length - count)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index and count must refer to a location within the buffer."));
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			int num = 0;
			int num2 = startIndex + count - 1;
			for (int i = startIndex; i <= num2; i++)
			{
				if (value[i] != null)
				{
					num += value[i].Length;
				}
			}
			num += (count - 1) * separator.Length;
			if (num < 0 || num + 1 < 0)
			{
				throw new OutOfMemoryException();
			}
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				UnSafeCharBuffer unSafeCharBuffer = new UnSafeCharBuffer(ptr2, num);
				unSafeCharBuffer.AppendString(value[startIndex]);
				for (int j = startIndex + 1; j <= num2; j++)
				{
					unSafeCharBuffer.AppendString(separator);
					unSafeCharBuffer.AppendString(value[j]);
				}
			}
			return text;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004DFA4 File Offset: 0x0004C1A4
		[SecuritySafeCritical]
		private unsafe static int CompareOrdinalIgnoreCaseHelper(string strA, string strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (num != 0)
					{
						int num2 = (int)(*ptr5);
						int num3 = (int)(*ptr6);
						if (num2 - 97 <= 25)
						{
							num2 -= 32;
						}
						if (num3 - 97 <= 25)
						{
							num3 -= 32;
						}
						if (num2 != num3)
						{
							return num2 - num3;
						}
						ptr5++;
						ptr6++;
						num--;
					}
					return strA.Length - strB.Length;
				}
			}
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004E034 File Offset: 0x0004C234
		[SecuritySafeCritical]
		internal unsafe static string SmallCharToUpper(string strIn)
		{
			int length = strIn.Length;
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &strIn.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &text.m_firstChar)
				{
					char* ptr4 = ptr3;
					for (int i = 0; i < length; i++)
					{
						int num = (int)ptr2[i];
						if (num - 97 <= 25)
						{
							num -= 32;
						}
						ptr4[i] = (char)num;
					}
					ptr = null;
				}
				return text;
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004E0A8 File Offset: 0x0004C2A8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecuritySafeCritical]
		private unsafe static bool EqualsHelper(string strA, string strB)
		{
			int i = strA.Length;
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					if (Environment.Is64BitProcess)
					{
						while (i >= 12)
						{
							if (*(long*)ptr5 != *(long*)ptr6)
							{
								return false;
							}
							if (*(long*)(ptr5 + 4) != *(long*)(ptr6 + 4))
							{
								return false;
							}
							if (*(long*)(ptr5 + 8) != *(long*)(ptr6 + 8))
							{
								return false;
							}
							ptr5 += 12;
							ptr6 += 12;
							i -= 12;
						}
					}
					else
					{
						while (i >= 10)
						{
							if (*(int*)ptr5 != *(int*)ptr6)
							{
								return false;
							}
							if (*(int*)(ptr5 + 2) != *(int*)(ptr6 + 2))
							{
								return false;
							}
							if (*(int*)(ptr5 + 4) != *(int*)(ptr6 + 4))
							{
								return false;
							}
							if (*(int*)(ptr5 + 6) != *(int*)(ptr6 + 6))
							{
								return false;
							}
							if (*(int*)(ptr5 + 8) != *(int*)(ptr6 + 8))
							{
								return false;
							}
							ptr5 += 10;
							ptr6 += 10;
							i -= 10;
						}
					}
					while (i > 0 && *(int*)ptr5 == *(int*)ptr6)
					{
						ptr5 += 2;
						ptr6 += 2;
						i -= 2;
					}
					return i <= 0;
				}
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004E1D8 File Offset: 0x0004C3D8
		[SecuritySafeCritical]
		private unsafe static int CompareOrdinalHelper(string strA, string strB)
		{
			int i = Math.Min(strA.Length, strB.Length);
			int num = -1;
			fixed (char* ptr = &strA.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &strB.m_firstChar)
				{
					char* ptr4 = ptr3;
					char* ptr5 = ptr2;
					char* ptr6 = ptr4;
					while (i >= 10)
					{
						if (*(int*)ptr5 != *(int*)ptr6)
						{
							num = 0;
							break;
						}
						if (*(int*)(ptr5 + 2) != *(int*)(ptr6 + 2))
						{
							num = 2;
							break;
						}
						if (*(int*)(ptr5 + 4) != *(int*)(ptr6 + 4))
						{
							num = 4;
							break;
						}
						if (*(int*)(ptr5 + 6) != *(int*)(ptr6 + 6))
						{
							num = 6;
							break;
						}
						if (*(int*)(ptr5 + 8) != *(int*)(ptr6 + 8))
						{
							num = 8;
							break;
						}
						ptr5 += 10;
						ptr6 += 10;
						i -= 10;
					}
					if (num != -1)
					{
						ptr5 += num;
						ptr6 += num;
						int num2;
						if ((num2 = (int)(*ptr5 - *ptr6)) != 0)
						{
							return num2;
						}
						return (int)(ptr5[1] - ptr6[1]);
					}
					else
					{
						while (i > 0 && *(int*)ptr5 == *(int*)ptr6)
						{
							ptr5 += 2;
							ptr6 += 2;
							i -= 2;
						}
						if (i <= 0)
						{
							return strA.Length - strB.Length;
						}
						int num3;
						if ((num3 = (int)(*ptr5 - *ptr6)) != 0)
						{
							return num3;
						}
						return (int)(ptr5[1] - ptr6[1]);
					}
				}
			}
		}

		/// <summary>Determines whether this instance and a specified object, which must also be a <see cref="T:System.String" /> object, have the same value.</summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.String" /> and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="obj">The string to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001340 RID: 4928 RVA: 0x0004E31C File Offset: 0x0004C51C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public override bool Equals(object obj)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			string text = obj as string;
			return text != null && (this == obj || (this.Length == text.Length && string.EqualsHelper(this, text)));
		}

		/// <summary>Determines whether this instance and another specified <see cref="T:System.String" /> object have the same value.</summary>
		/// <returns>true if the value of the <paramref name="value" /> parameter is the same as this instance; otherwise, false.</returns>
		/// <param name="value">The string to compare to this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001341 RID: 4929 RVA: 0x0004E35B File Offset: 0x0004C55B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public bool Equals(string value)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			return value != null && (this == value || (this.Length == value.Length && string.EqualsHelper(this, value)));
		}

		/// <summary>Determines whether this string and a specified <see cref="T:System.String" /> object have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.</summary>
		/// <returns>true if the value of the <paramref name="value" /> parameter is the same as this string; otherwise, false.</returns>
		/// <param name="value">The string to compare to this instance.</param>
		/// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001342 RID: 4930 RVA: 0x0004E388 File Offset: 0x0004C588
		[SecuritySafeCritical]
		public bool Equals(string value, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
			if (this == value)
			{
				return true;
			}
			if (value == null)
			{
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.None) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(this, value, CompareOptions.IgnoreCase) == 0;
			case StringComparison.Ordinal:
				return this.Length == value.Length && string.EqualsHelper(this, value);
			case StringComparison.OrdinalIgnoreCase:
				if (this.Length != value.Length)
				{
					return false;
				}
				if (this.IsAscii() && value.IsAscii())
				{
					return string.CompareOrdinalIgnoreCaseHelper(this, value) == 0;
				}
				return TextInfo.CompareOrdinalIgnoreCase(this, value) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false. If both <paramref name="a" /> and <paramref name="b" /> are null, the method returns true.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001343 RID: 4931 RVA: 0x0004E49A File Offset: 0x0004C69A
		public static bool Equals(string a, string b)
		{
			return a == b || (a != null && b != null && a.Length == b.Length && string.EqualsHelper(a, b));
		}

		/// <summary>Determines whether two specified <see cref="T:System.String" /> objects have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.</summary>
		/// <returns>true if the value of the <paramref name="a" /> parameter is equal to the value of the <paramref name="b" /> parameter; otherwise, false.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the comparison. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001344 RID: 4932 RVA: 0x0004E4C4 File Offset: 0x0004C6C4
		[SecuritySafeCritical]
		public static bool Equals(string a, string b, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.None) == 0;
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(a, b, CompareOptions.IgnoreCase) == 0;
			case StringComparison.Ordinal:
				return a.Length == b.Length && string.EqualsHelper(a, b);
			case StringComparison.OrdinalIgnoreCase:
				if (a.Length != b.Length)
				{
					return false;
				}
				if (a.IsAscii() && b.IsAscii())
				{
					return string.CompareOrdinalIgnoreCaseHelper(a, b) == 0;
				}
				return TextInfo.CompareOrdinalIgnoreCase(a, b) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
		}

		/// <summary>Determines whether two specified strings have the same value.</summary>
		/// <returns>true if the value of <paramref name="a" /> is the same as the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001345 RID: 4933 RVA: 0x0004E5D9 File Offset: 0x0004C7D9
		public static bool operator ==(string a, string b)
		{
			return string.Equals(a, b);
		}

		/// <summary>Determines whether two specified strings have different values.</summary>
		/// <returns>true if the value of <paramref name="a" /> is different from the value of <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The first string to compare, or null. </param>
		/// <param name="b">The second string to compare, or null. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06001346 RID: 4934 RVA: 0x0004E5E2 File Offset: 0x0004C7E2
		public static bool operator !=(string a, string b)
		{
			return !string.Equals(a, b);
		}

		/// <summary>Gets the <see cref="T:System.Char" /> object at a specified position in the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The object at position <paramref name="index" />.</returns>
		/// <param name="index">A position in the current string. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of this object or less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700023B RID: 571
		[IndexerName("Chars")]
		public unsafe char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_stringLength)
				{
					throw new IndexOutOfRangeException();
				}
				fixed (char* ptr = &this.m_firstChar)
				{
					return ptr[index];
				}
			}
		}

		/// <summary>Copies a specified number of characters from a specified position in this instance to a specified position in an array of Unicode characters.</summary>
		/// <param name="sourceIndex">The index of the first character in this instance to copy. </param>
		/// <param name="destination">An array of Unicode characters to which characters in this instance are copied. </param>
		/// <param name="destinationIndex">The index in <paramref name="destination" /> at which the copy operation begins. </param>
		/// <param name="count">The number of characters in this instance to copy to <paramref name="destination" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sourceIndex" />, <paramref name="destinationIndex" />, or <paramref name="count" /> is negative -or- <paramref name="count" /> is greater than the length of the substring from <paramref name="startIndex" /> to the end of this instance -or- <paramref name="count" /> is greater than the length of the subarray from <paramref name="destinationIndex" /> to the end of <paramref name="destination" /></exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001348 RID: 4936 RVA: 0x0004E620 File Offset: 0x0004C820
		[SecuritySafeCritical]
		public unsafe void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count > this.Length - sourceIndex)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", Environment.GetResourceString("Index and count must refer to a location within the string."));
			}
			if (destinationIndex > destination.Length - count || destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", Environment.GetResourceString("Index and count must refer to a location within the string."));
			}
			if (count > 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					fixed (char[] array = destination)
					{
						char* ptr3;
						if (destination == null || array.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array[0];
						}
						string.wstrcpy(ptr3 + destinationIndex, ptr2 + sourceIndex, count);
					}
				}
			}
		}

		/// <summary>Copies the characters in this instance to a Unicode character array.</summary>
		/// <returns>A Unicode character array whose elements are the individual characters of this instance. If this instance is an empty string, the returned array is empty and has a zero length.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001349 RID: 4937 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		[SecuritySafeCritical]
		public unsafe char[] ToCharArray()
		{
			int length = this.Length;
			char[] array = new char[length];
			if (length > 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					char[] array2;
					char* ptr3;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array2[0];
					}
					string.wstrcpy(ptr3, ptr2, length);
					array2 = null;
				}
			}
			return array;
		}

		/// <summary>Copies the characters in a specified substring in this instance to a Unicode character array.</summary>
		/// <returns>A Unicode character array whose elements are the <paramref name="length" /> number of characters in this instance starting from character position <paramref name="startIndex" />.</returns>
		/// <param name="startIndex">The starting position of a substring in this instance. </param>
		/// <param name="length">The length of the substring in this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="length" /> is greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134A RID: 4938 RVA: 0x0004E748 File Offset: 0x0004C948
		[SecuritySafeCritical]
		public unsafe char[] ToCharArray(int startIndex, int length)
		{
			if (startIndex < 0 || startIndex > this.Length || startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			char[] array = new char[length];
			if (length > 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					char[] array2;
					char* ptr3;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array2[0];
					}
					string.wstrcpy(ptr3, ptr2 + startIndex, length);
					array2 = null;
				}
			}
			return array;
		}

		/// <summary>Indicates whether the specified string is null or an <see cref="F:System.String.Empty" /> string.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is null or an empty string (""); otherwise, false.</returns>
		/// <param name="value">The string to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134B RID: 4939 RVA: 0x0004E7DF File Offset: 0x0004C9DF
		public static bool IsNullOrEmpty(string value)
		{
			return value == null || value.Length == 0;
		}

		/// <summary>Indicates whether a specified string is null, empty, or consists only of white-space characters.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is null or <see cref="F:System.String.Empty" />, or if <paramref name="value" /> consists exclusively of white-space characters. </returns>
		/// <param name="value">The string to test.</param>
		// Token: 0x0600134C RID: 4940 RVA: 0x0004E7F0 File Offset: 0x0004C9F0
		public static bool IsNullOrWhiteSpace(string value)
		{
			if (value == null)
			{
				return true;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600134D RID: 4941 RVA: 0x0004E824 File Offset: 0x0004CA24
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecuritySafeCritical]
		public unsafe override int GetHashCode()
		{
			char* ptr = this;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 5381;
			int num2 = num;
			char* ptr2 = ptr;
			int num3;
			while ((num3 = (int)(*ptr2)) != 0)
			{
				num = ((num << 5) + num) ^ num3;
				num3 = (int)ptr2[1];
				if (num3 == 0)
				{
					break;
				}
				num2 = ((num2 << 5) + num2) ^ num3;
				ptr2 += 2;
			}
			return num + num2 * 1566083941;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0004E888 File Offset: 0x0004CA88
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecuritySafeCritical]
		internal unsafe int GetLegacyNonRandomizedHashCode()
		{
			char* ptr = this;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = 5381;
			int num2 = num;
			char* ptr2 = ptr;
			int num3;
			while ((num3 = (int)(*ptr2)) != 0)
			{
				num = ((num << 5) + num) ^ num3;
				num3 = (int)ptr2[1];
				if (num3 == 0)
				{
					break;
				}
				num2 = ((num2 << 5) + num2) ^ num3;
				ptr2 += 2;
			}
			return num + num2 * 1566083941;
		}

		/// <summary>Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array.</summary>
		/// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600134F RID: 4943 RVA: 0x0004E8E9 File Offset: 0x0004CAE9
		public string[] Split(params char[] separator)
		{
			return this.SplitInternal(separator, int.MaxValue, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this instance that are delimited by elements of a specified Unicode character array. A parameter specifies the maximum number of substrings to return.</summary>
		/// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this instance, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001350 RID: 4944 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
		public string[] Split(char[] separator, int count)
		{
			return this.SplitInternal(separator, count, StringSplitOptions.None);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. A parameter specifies whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001351 RID: 4945 RVA: 0x0004E903 File Offset: 0x0004CB03
		[ComVisible(false)]
		public string[] Split(char[] separator, StringSplitOptions options)
		{
			return this.SplitInternal(separator, int.MaxValue, options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified Unicode character array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of Unicode characters that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001352 RID: 4946 RVA: 0x0004E912 File Offset: 0x0004CB12
		[ComVisible(false)]
		public string[] Split(char[] separator, int count, StringSplitOptions options)
		{
			return this.SplitInternal(separator, count, options);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0004E920 File Offset: 0x0004CB20
		[ComVisible(false)]
		internal string[] SplitInternal(char[] separator, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { options }));
			}
			bool flag = options == StringSplitOptions.RemoveEmptyEntries;
			if (count == 0 || (flag && this.Length == 0))
			{
				return new string[0];
			}
			int[] array = new int[this.Length];
			int num = this.MakeSeparatorList(separator, ref array);
			if (num == 0 || count == 1)
			{
				return new string[] { this };
			}
			if (flag)
			{
				return this.InternalSplitOmitEmptyEntries(array, null, num, count);
			}
			return this.InternalSplitKeepEmptyEntries(array, null, num, count);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. A parameter specifies whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001354 RID: 4948 RVA: 0x0004E9C7 File Offset: 0x0004CBC7
		[ComVisible(false)]
		public string[] Split(string[] separator, StringSplitOptions options)
		{
			return this.Split(separator, int.MaxValue, options);
		}

		/// <summary>Returns a string array that contains the substrings in this string that are delimited by elements of a specified string array. Parameters specify the maximum number of substrings to return and whether to return empty array elements.</summary>
		/// <returns>An array whose elements contain the substrings in this string that are delimited by one or more strings in <paramref name="separator" />. For more information, see the Remarks section.</returns>
		/// <param name="separator">An array of strings that delimit the substrings in this string, an empty array that contains no delimiters, or null. </param>
		/// <param name="count">The maximum number of substrings to return. </param>
		/// <param name="options">
		///   <see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not one of the <see cref="T:System.StringSplitOptions" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001355 RID: 4949 RVA: 0x0004E9D8 File Offset: 0x0004CBD8
		[ComVisible(false)]
		public string[] Split(string[] separator, int count, StringSplitOptions options)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)options }));
			}
			bool flag = options == StringSplitOptions.RemoveEmptyEntries;
			if (separator == null || separator.Length == 0)
			{
				return this.SplitInternal(null, count, options);
			}
			if (count == 0 || (flag && this.Length == 0))
			{
				return new string[0];
			}
			int[] array = new int[this.Length];
			int[] array2 = new int[this.Length];
			int num = this.MakeSeparatorList(separator, ref array, ref array2);
			if (num == 0 || count == 1)
			{
				return new string[] { this };
			}
			if (flag)
			{
				return this.InternalSplitOmitEmptyEntries(array, array2, num, count);
			}
			return this.InternalSplitKeepEmptyEntries(array, array2, num, count);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0004EAA0 File Offset: 0x0004CCA0
		private string[] InternalSplitKeepEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
		{
			int num = 0;
			int num2 = 0;
			count--;
			int num3 = ((numReplaces < count) ? numReplaces : count);
			string[] array = new string[num3 + 1];
			int num4 = 0;
			while (num4 < num3 && num < this.Length)
			{
				array[num2++] = this.Substring(num, sepList[num4] - num);
				num = sepList[num4] + ((lengthList == null) ? 1 : lengthList[num4]);
				num4++;
			}
			if (num < this.Length && num3 >= 0)
			{
				array[num2] = this.Substring(num);
			}
			else if (num2 == num3)
			{
				array[num2] = string.Empty;
			}
			return array;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004EB30 File Offset: 0x0004CD30
		private string[] InternalSplitOmitEmptyEntries(int[] sepList, int[] lengthList, int numReplaces, int count)
		{
			int num = ((numReplaces < count) ? (numReplaces + 1) : count);
			string[] array = new string[num];
			int num2 = 0;
			int num3 = 0;
			int i = 0;
			while (i < numReplaces && num2 < this.Length)
			{
				if (sepList[i] - num2 > 0)
				{
					array[num3++] = this.Substring(num2, sepList[i] - num2);
				}
				num2 = sepList[i] + ((lengthList == null) ? 1 : lengthList[i]);
				if (num3 == count - 1)
				{
					while (i < numReplaces - 1)
					{
						if (num2 != sepList[++i])
						{
							break;
						}
						num2 += ((lengthList == null) ? 1 : lengthList[i]);
					}
					break;
				}
				i++;
			}
			if (num2 < this.Length)
			{
				array[num3++] = this.Substring(num2);
			}
			string[] array2 = array;
			if (num3 != num)
			{
				array2 = new string[num3];
				for (int j = 0; j < num3; j++)
				{
					array2[j] = array[j];
				}
			}
			return array2;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004EC08 File Offset: 0x0004CE08
		[SecuritySafeCritical]
		private unsafe int MakeSeparatorList(char[] separator, ref int[] sepList)
		{
			int num = 0;
			if (separator == null || separator.Length == 0)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					int num2 = 0;
					while (num2 < this.Length && num < sepList.Length)
					{
						if (char.IsWhiteSpace(ptr2[num2]))
						{
							sepList[num++] = num2;
						}
						num2++;
					}
				}
			}
			else
			{
				int num3 = sepList.Length;
				int num4 = separator.Length;
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr3 = ptr;
					fixed (char[] array = separator)
					{
						char* ptr4;
						if (separator == null || array.Length == 0)
						{
							ptr4 = null;
						}
						else
						{
							ptr4 = &array[0];
						}
						int num5 = 0;
						while (num5 < this.Length && num < num3)
						{
							char* ptr5 = ptr4;
							int i = 0;
							while (i < num4)
							{
								if (ptr3[num5] == *ptr5)
								{
									sepList[num++] = num5;
									break;
								}
								i++;
								ptr5++;
							}
							num5++;
						}
						ptr = null;
					}
				}
			}
			return num;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
		[SecuritySafeCritical]
		private unsafe int MakeSeparatorList(string[] separators, ref int[] sepList, ref int[] lengthList)
		{
			int num = 0;
			int num2 = sepList.Length;
			int num3 = separators.Length;
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				int num4 = 0;
				while (num4 < this.Length && num < num2)
				{
					foreach (string text in separators)
					{
						if (!string.IsNullOrEmpty(text))
						{
							int length = text.Length;
							if (ptr2[num4] == text[0] && length <= this.Length - num4 && (length == 1 || string.CompareOrdinal(this, num4, text, 0, length) == 0))
							{
								sepList[num] = num4;
								lengthList[num] = length;
								num++;
								num4 += length - 1;
								break;
							}
						}
					}
					num4++;
				}
			}
			return num;
		}

		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and continues to the end of the string.</summary>
		/// <returns>A string that is equivalent to the substring that begins at <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance.</returns>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135A RID: 4954 RVA: 0x0004EDA2 File Offset: 0x0004CFA2
		public string Substring(int startIndex)
		{
			return this.Substring(startIndex, this.Length - startIndex);
		}

		/// <summary>Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.</summary>
		/// <returns>A string that is equivalent to the substring of length <paramref name="length" /> that begins at <paramref name="startIndex" /> in this instance, or <see cref="F:System.String.Empty" /> if <paramref name="startIndex" /> is equal to the length of this instance and <paramref name="length" /> is zero.</returns>
		/// <param name="startIndex">The zero-based starting character position of a substring in this instance. </param>
		/// <param name="length">The number of characters in the substring. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> plus <paramref name="length" /> indicates a position not within this instance.-or- <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135B RID: 4955 RVA: 0x0004EDB4 File Offset: 0x0004CFB4
		[SecuritySafeCritical]
		public string Substring(int startIndex, int length)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("startIndex cannot be larger than length of string."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Length cannot be less than zero."));
			}
			if (startIndex > this.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index and length must refer to a location within the string."));
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (startIndex == 0 && length == this.Length)
			{
				return this;
			}
			return this.InternalSubString(startIndex, length);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0004EE50 File Offset: 0x0004D050
		[SecurityCritical]
		private unsafe string InternalSubString(int startIndex, int length)
		{
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &this.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4 + startIndex, length);
				}
			}
			return text;
		}

		/// <summary>Removes all leading and trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of the characters in the <paramref name="trimChars" /> parameter are removed from the start and end of the current string. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135D RID: 4957 RVA: 0x0004EE88 File Offset: 0x0004D088
		public string Trim(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimHelper(2);
			}
			return this.TrimHelper(trimChars, 2);
		}

		/// <summary>Removes all leading occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of characters in the <paramref name="trimChars" /> parameter are removed from the start of the current string. If <paramref name="trimChars" /> is null or an empty array, white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600135E RID: 4958 RVA: 0x0004EEA1 File Offset: 0x0004D0A1
		public string TrimStart(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimHelper(0);
			}
			return this.TrimHelper(trimChars, 0);
		}

		/// <summary>Removes all trailing occurrences of a set of characters specified in an array from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all occurrences of the characters in the <paramref name="trimChars" /> parameter are removed from the end of the current string. If <paramref name="trimChars" /> is null or an empty array, Unicode white-space characters are removed instead.</returns>
		/// <param name="trimChars">An array of Unicode characters to remove, or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600135F RID: 4959 RVA: 0x0004EEBA File Offset: 0x0004D0BA
		public string TrimEnd(params char[] trimChars)
		{
			if (trimChars == null || trimChars.Length == 0)
			{
				return this.TrimHelper(1);
			}
			return this.TrimHelper(trimChars, 1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of Unicode characters.</summary>
		/// <param name="value">A pointer to a null-terminated array of Unicode characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies an array that contains an invalid Unicode character, or <paramref name="value" /> specifies an address less than 64000.</exception>
		// Token: 0x06001360 RID: 4960
		[CLSCompliant(false)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of Unicode characters, a starting character position within that array, and a length.</summary>
		/// <param name="value">A pointer to an array of Unicode characters. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero, <paramref name="value" /> + <paramref name="startIndex" /> cause a pointer overflow, or the current process does not have read access to all the addressed characters.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies an array that contains an invalid Unicode character, or <paramref name="value" /> + <paramref name="startIndex" /> specifies an address less than 64000.</exception>
		// Token: 0x06001361 RID: 4961
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(char* value, int startIndex, int length);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a pointer to an array of 8-bit signed integers.</summary>
		/// <param name="value">A pointer to a null-terminated array of 8-bit signed integers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded in ANSI. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of the new string to initialize, which is determined by the null termination character of <paramref name="value" />, is too large to allocate. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" /> specifies an invalid address.</exception>
		// Token: 0x06001362 RID: 4962
		[CLSCompliant(false)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting position within that array, and a length.</summary>
		/// <param name="value">A pointer to an array of 8-bit signed integers. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. -or-The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception>
		/// <exception cref="T:System.ArgumentException">The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is less than 64K.-or- A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded in ANSI. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" />, <paramref name="startIndex" />, and <paramref name="length" /> collectively specify an invalid address.</exception>
		// Token: 0x06001363 RID: 4963
		[CLSCompliant(false)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified pointer to an array of 8-bit signed integers, a starting position within that array, a length, and an <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <param name="value">A pointer to an array of 8-bit signed integers. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <param name="enc">An object that specifies how the array referenced by <paramref name="value" /> is encoded. If <paramref name="enc" /> is null, ANSI encoding is assumed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero. -or-The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is too large for the current platform; that is, the address calculation overflowed. -or-The length of the new string to initialize is too large to allocate.</exception>
		/// <exception cref="T:System.ArgumentException">The address specified by <paramref name="value" /> + <paramref name="startIndex" /> is less than 64K.-or- A new instance of <see cref="T:System.String" /> could not be initialized using <paramref name="value" />, assuming <paramref name="value" /> is encoded as specified by <paramref name="enc" />. </exception>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="value" />, <paramref name="startIndex" />, and <paramref name="length" /> collectively specify an invalid address.</exception>
		// Token: 0x06001364 RID: 4964
		[CLSCompliant(false)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe extern String(sbyte* value, int startIndex, int length, Encoding enc);

		// Token: 0x06001365 RID: 4965 RVA: 0x0004EED4 File Offset: 0x0004D0D4
		[SecurityCritical]
		internal unsafe static string CreateStringFromEncoding(byte* bytes, int byteLength, Encoding encoding)
		{
			int charCount = encoding.GetCharCount(bytes, byteLength, null);
			if (charCount == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(charCount);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				encoding.GetChars(bytes, byteLength, ptr2, charCount, null);
			}
			return text;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0004EF14 File Offset: 0x0004D114
		internal unsafe int GetBytesFromEncoding(byte* pbNativeBuffer, int cbNativeBuffer, Encoding encoding)
		{
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				return encoding.GetBytes(ptr2, this.m_stringLength, pbNativeBuffer, cbNativeBuffer);
			}
		}

		/// <summary>Indicates whether this string is in Unicode normalization form C.</summary>
		/// <returns>true if this string is in normalization form C; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001367 RID: 4967 RVA: 0x0004EF3A File Offset: 0x0004D13A
		public bool IsNormalized()
		{
			return this.IsNormalized(NormalizationForm.FormC);
		}

		/// <summary>Indicates whether this string is in the specified Unicode normalization form.</summary>
		/// <returns>true if this string is in the normalization form specified by the <paramref name="normalizationForm" /> parameter; otherwise, false.</returns>
		/// <param name="normalizationForm">A Unicode normalization form. </param>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001368 RID: 4968 RVA: 0x0004EF43 File Offset: 0x0004D143
		[SecuritySafeCritical]
		public bool IsNormalized(NormalizationForm normalizationForm)
		{
			return (this.IsFastSort() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD)) || Normalization.IsNormalized(this, normalizationForm);
		}

		/// <summary>Returns a new string whose textual value is the same as this string, but whose binary representation is in Unicode normalization form C.</summary>
		/// <returns>A new, normalized string whose textual value is the same as this string, but whose binary representation is in normalization form C.</returns>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001369 RID: 4969 RVA: 0x0004EF66 File Offset: 0x0004D166
		public string Normalize()
		{
			return this.Normalize(NormalizationForm.FormC);
		}

		/// <summary>Returns a new string whose textual value is the same as this string, but whose binary representation is in the specified Unicode normalization form.</summary>
		/// <returns>A new string whose textual value is the same as this string, but whose binary representation is in the normalization form specified by the <paramref name="normalizationForm" /> parameter.</returns>
		/// <param name="normalizationForm">A Unicode normalization form. </param>
		/// <exception cref="T:System.ArgumentException">The current instance contains invalid Unicode characters.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600136A RID: 4970 RVA: 0x0004EF6F File Offset: 0x0004D16F
		[SecuritySafeCritical]
		public string Normalize(NormalizationForm normalizationForm)
		{
			if (this.IsAscii() && (normalizationForm == NormalizationForm.FormC || normalizationForm == NormalizationForm.FormKC || normalizationForm == NormalizationForm.FormD || normalizationForm == NormalizationForm.FormKD))
			{
				return this;
			}
			return Normalization.Normalize(this, normalizationForm);
		}

		// Token: 0x0600136B RID: 4971
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string FastAllocateString(int length);

		// Token: 0x0600136C RID: 4972 RVA: 0x0004EF94 File Offset: 0x0004D194
		[SecuritySafeCritical]
		private unsafe static void FillStringChecked(string dest, int destPos, string src)
		{
			if (src.Length > dest.Length - destPos)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (char* ptr = &dest.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &src.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2 + destPos, ptr4, src.Length);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by an array of Unicode characters, a starting character position within that array, and a length.</summary>
		/// <param name="value">An array of Unicode characters. </param>
		/// <param name="startIndex">The starting position within <paramref name="value" />. </param>
		/// <param name="length">The number of characters within <paramref name="value" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is less than zero.-or- The sum of <paramref name="startIndex" /> and <paramref name="length" /> is greater than the number of elements in <paramref name="value" />. </exception>
		// Token: 0x0600136D RID: 4973
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value, int startIndex, int length);

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by an array of Unicode characters.</summary>
		/// <param name="value">An array of Unicode characters. </param>
		// Token: 0x0600136E RID: 4974
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char[] value);

		// Token: 0x0600136F RID: 4975 RVA: 0x0004EFE1 File Offset: 0x0004D1E1
		[SecurityCritical]
		internal unsafe static void wstrcpy(char* dmem, char* smem, int charCount)
		{
			Buffer.Memcpy((byte*)dmem, (byte*)smem, charCount * 2);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004EFF0 File Offset: 0x0004D1F0
		[SecuritySafeCritical]
		private unsafe string CtorCharArray(char[] value)
		{
			if (value != null && value.Length != 0)
			{
				string text2;
				string text = (text2 = string.FastAllocateString(value.Length));
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (char[] array = value)
				{
					char* ptr2;
					if (value == null || array.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array[0];
					}
					string.wstrcpy(ptr, ptr2, value.Length);
					text2 = null;
				}
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004F048 File Offset: 0x0004D248
		[SecuritySafeCritical]
		private unsafe string CtorCharArrayStartLength(char[] value, int startIndex, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Length cannot be less than zero."));
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (length > 0)
			{
				string text2;
				string text = (text2 = string.FastAllocateString(length));
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (char[] array = value)
				{
					char* ptr2;
					if (value == null || array.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array[0];
					}
					string.wstrcpy(ptr, ptr2 + startIndex, length);
					text2 = null;
				}
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004F0FC File Offset: 0x0004D2FC
		[SecuritySafeCritical]
		private unsafe string CtorCharCount(char c, int count)
		{
			if (count > 0)
			{
				string text = string.FastAllocateString(count);
				if (c != '\0')
				{
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						char* ptr2 = ptr;
						while ((ptr2 & 3U) != 0U && count > 0)
						{
							*(ptr2++) = c;
							count--;
						}
						uint num = (uint)(((uint)c << 16) | c);
						if (count >= 4)
						{
							count -= 4;
							do
							{
								*(int*)ptr2 = (int)num;
								*(int*)(ptr2 + 2) = (int)num;
								ptr2 += 4;
								count -= 4;
							}
							while (count >= 0);
						}
						if ((count & 2) != 0)
						{
							*(int*)ptr2 = (int)num;
							ptr2 += 2;
						}
						if ((count & 1) != 0)
						{
							*ptr2 = c;
						}
					}
				}
				return text;
			}
			if (count == 0)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("'{0}' must be non-negative.", new object[] { "count" }));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		[SecurityCritical]
		private unsafe static int wcslen(char* ptr)
		{
			char* ptr2 = ptr;
			while ((ptr2 & 3U) != 0U && *ptr2 != '\0')
			{
				ptr2++;
			}
			if (*ptr2 != '\0')
			{
				for (;;)
				{
					if ((*ptr2 & ptr2[1]) == '\0')
					{
						if (*ptr2 == '\0')
						{
							break;
						}
						if (ptr2[1] == '\0')
						{
							break;
						}
					}
					ptr2 += 2;
				}
			}
			while (*ptr2 != '\0')
			{
				ptr2++;
			}
			return (int)((long)(ptr2 - ptr));
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0004F204 File Offset: 0x0004D404
		[SecurityCritical]
		private unsafe string CtorCharPtr(char* ptr)
		{
			if (ptr == null)
			{
				return string.Empty;
			}
			string text;
			try
			{
				int num = string.wcslen(ptr);
				if (num == 0)
				{
					text = string.Empty;
				}
				else
				{
					string text2 = string.FastAllocateString(num);
					try
					{
						fixed (string text3 = text2)
						{
							char* ptr2 = text3;
							if (ptr2 != null)
							{
								ptr2 += RuntimeHelpers.OffsetToStringData / 2;
							}
							string.wstrcpy(ptr2, ptr, num);
						}
					}
					finally
					{
						string text3 = null;
					}
					text = text2;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("Pointer startIndex and length do not refer to a valid string."));
			}
			return text;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0004F290 File Offset: 0x0004D490
		[SecurityCritical]
		private unsafe string CtorCharPtrStartLength(char* ptr, int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Length cannot be less than zero."));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			char* ptr2 = ptr + startIndex;
			if (ptr2 < ptr)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Pointer startIndex and length do not refer to a valid string."));
			}
			if (length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(length);
			string text3;
			try
			{
				try
				{
					fixed (string text2 = text)
					{
						char* ptr3 = text2;
						if (ptr3 != null)
						{
							ptr3 += RuntimeHelpers.OffsetToStringData / 2;
						}
						string.wstrcpy(ptr3, ptr2, length);
					}
				}
				finally
				{
					string text2 = null;
				}
				text3 = text;
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", Environment.GetResourceString("Pointer startIndex and length do not refer to a valid string."));
			}
			return text3;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.String" /> class to the value indicated by a specified Unicode character repeated a specified number of times.</summary>
		/// <param name="c">A Unicode character. </param>
		/// <param name="count">The number of times <paramref name="c" /> occurs. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		// Token: 0x06001376 RID: 4982
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern String(char c, int count);

		/// <summary>Compares two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001377 RID: 4983 RVA: 0x0004F358 File Offset: 0x0004D558
		public static int Compare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001378 RID: 4984 RVA: 0x0004F36C File Offset: 0x0004D56C
		public static int Compare(string strA, string strB, bool ignoreCase)
		{
			if (ignoreCase)
			{
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects using the specified rules, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare.</param>
		/// <param name="strB">The second string to compare. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="T:System.StringComparison" /> is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001379 RID: 4985 RVA: 0x0004F398 File Offset: 0x0004D598
		[SecuritySafeCritical]
		public static int Compare(string strA, string strB, StringComparison comparisonType)
		{
			if (comparisonType - StringComparison.CurrentCulture > 5)
			{
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
			if (strA == strB)
			{
				return 0;
			}
			if (strA == null)
			{
				return -1;
			}
			if (strB == null)
			{
				return 1;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				if (strA.m_firstChar - strB.m_firstChar != '\0')
				{
					return (int)(strA.m_firstChar - strB.m_firstChar);
				}
				return string.CompareOrdinalHelper(strA, strB);
			case StringComparison.OrdinalIgnoreCase:
				if (strA.IsAscii() && strB.IsAscii())
				{
					return string.CompareOrdinalIgnoreCaseHelper(strA, strB);
				}
				return TextInfo.CompareOrdinalIgnoreCase(strA, strB);
			default:
				throw new NotSupportedException(Environment.GetResourceString("The string comparison type passed in is currently not supported."));
			}
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two strings to each other in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between <paramref name="strA" /> and <paramref name="strB" />, as shown in the following tableValueConditionLess than zero<paramref name="strA" /> is less than <paramref name="strB" />.Zero<paramref name="strA" /> equals <paramref name="strB" />.Greater than zero<paramref name="strA" /> is greater than <paramref name="strB" />.</returns>
		/// <param name="strA">The first string to compare.  </param>
		/// <param name="strB">The second string to compare.</param>
		/// <param name="culture">The culture that supplies culture-specific comparison information.</param>
		/// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		// Token: 0x0600137A RID: 4986 RVA: 0x0004F493 File Offset: 0x0004D693
		public static int Compare(string strA, string strB, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.CompareInfo.Compare(strA, strB, options);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and using culture-specific information to influence the comparison, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> equals <paramref name="strB" />. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false. </param>
		/// <param name="culture">An object that supplies culture-specific comparison information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600137B RID: 4987 RVA: 0x0004F4B1 File Offset: 0x0004D6B1
		public static int Compare(string strA, string strB, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (ignoreCase)
			{
				return culture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase);
			}
			return culture.CompareInfo.Compare(strA, strB, CompareOptions.None);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600137C RID: 4988 RVA: 0x0004F4E4 File Offset: 0x0004D6E4
		public static int Compare(string strA, int indexA, string strB, int indexB, int length)
		{
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects, ignoring or honoring their case, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600137D RID: 4989 RVA: 0x0004F53C File Offset: 0x0004D73C
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase)
		{
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			if (ignoreCase)
			{
				return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects, ignoring or honoring their case and using culture-specific information to influence the comparison, and returns an integer that indicates their relative position in the sort order.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false. </param>
		/// <param name="culture">An object that supplies culture-specific comparison information. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="strA" /> or <paramref name="strB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600137E RID: 4990 RVA: 0x0004F5B0 File Offset: 0x0004D7B0
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, bool ignoreCase, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			if (ignoreCase)
			{
				return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
			}
			return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects using the specified comparison options and culture-specific information to influence the comparison, and returns an integer that indicates the relationship of the two substrings to each other in the sort order.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two substrings, as shown in the following table.ValueConditionLess than zeroThe substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />.ZeroThe substrings are equal or <paramref name="length" /> is zero.Greater than zeroThe substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />.</returns>
		/// <param name="strA">The first string to use in the comparison.   </param>
		/// <param name="indexA">The starting position of the substring within <paramref name="strA" />.</param>
		/// <param name="strB">The second string to use in the comparison.</param>
		/// <param name="indexB">The starting position of the substring within <paramref name="strB" />.</param>
		/// <param name="length">The maximum number of characters in the substrings to compare.</param>
		/// <param name="culture">An object that supplies culture-specific comparison information.</param>
		/// <param name="options">Options to use when performing the comparison (such as ignoring case or symbols).  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.Length.-or-<paramref name="indexB" /> is greater than <paramref name="strB" />.Length.-or-<paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative.-or-Either <paramref name="strA" /> or <paramref name="strB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null.</exception>
		// Token: 0x0600137F RID: 4991 RVA: 0x0004F62C File Offset: 0x0004D82C
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			int num = length;
			int num2 = length;
			if (strA != null && strA.Length - indexA < num)
			{
				num = strA.Length - indexA;
			}
			if (strB != null && strB.Length - indexB < num2)
			{
				num2 = strB.Length - indexB;
			}
			return culture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, options);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects using the specified rules, and returns an integer that indicates their relative position in the sort order. </summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.Value Condition Less than zero The substring in the <paramref name="strA" /> parameter is less than the substring in the <paramref name="strB" /> parameter.Zero The substrings are equal, or the <paramref name="length" /> parameter is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The position of the substring within <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison.</param>
		/// <param name="indexB">The position of the substring within <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules to use in the comparison. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. -or-Either <paramref name="indexA" /> or <paramref name="indexB" /> is null, and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001380 RID: 4992 RVA: 0x0004F690 File Offset: 0x0004D890
		[SecuritySafeCritical]
		public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType)
		{
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
			if (strA == null || strB == null)
			{
				if (strA == strB)
				{
					return 0;
				}
				if (strA != null)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Length cannot be less than zero."));
				}
				if (indexA < 0)
				{
					throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (indexB < 0)
				{
					throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (strA.Length - indexA < 0)
				{
					throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (strB.Length - indexB < 0)
				{
					throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (length == 0 || (strA == strB && indexA == indexB))
				{
					return 0;
				}
				int num = length;
				int num2 = length;
				if (strA != null && strA.Length - indexA < num)
				{
					num = strA.Length - indexA;
				}
				if (strB != null && strB.Length - indexB < num2)
				{
					num2 = strB.Length - indexB;
				}
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
				case StringComparison.InvariantCulture:
					return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.None);
				case StringComparison.InvariantCultureIgnoreCase:
					return CultureInfo.InvariantCulture.CompareInfo.Compare(strA, indexA, num, strB, indexB, num2, CompareOptions.IgnoreCase);
				case StringComparison.Ordinal:
					return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
				case StringComparison.OrdinalIgnoreCase:
					return TextInfo.CompareOrdinalIgnoreCaseEx(strA, indexA, strB, indexB, num, num2);
				default:
					throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."));
				}
			}
		}

		/// <summary>Compares this instance with a specified <see cref="T:System.Object" /> and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value" /> parameter.Value Condition Less than zero This instance precedes <paramref name="value" />. Zero This instance has the same position in the sort order as <paramref name="value" />. Greater than zero This instance follows <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">An object that evaluates to a <see cref="T:System.String" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.String" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001381 RID: 4993 RVA: 0x0004F846 File Offset: 0x0004DA46
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is string))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type String."));
			}
			return string.Compare(this, (string)value, StringComparison.CurrentCulture);
		}

		/// <summary>Compares this instance with a specified <see cref="T:System.String" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.String" />.</summary>
		/// <returns>A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the sort order as the <paramref name="value" /> parameter.Value Condition Less than zero This instance precedes <paramref name="strB" />. Zero This instance has the same position in the sort order as <paramref name="strB" />. Greater than zero This instance follows <paramref name="strB" />.-or- <paramref name="strB" /> is null. </returns>
		/// <param name="strB">The string to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001382 RID: 4994 RVA: 0x0004F872 File Offset: 0x0004DA72
		public int CompareTo(string strB)
		{
			if (strB == null)
			{
				return 1;
			}
			return CultureInfo.CurrentCulture.CompareInfo.Compare(this, strB, CompareOptions.None);
		}

		/// <summary>Compares two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each string.</summary>
		/// <returns>An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero <paramref name="strA" /> is less than <paramref name="strB" />. Zero <paramref name="strA" /> and <paramref name="strB" /> are equal. Greater than zero <paramref name="strA" /> is greater than <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to compare. </param>
		/// <param name="strB">The second string to compare. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001383 RID: 4995 RVA: 0x0004F88B File Offset: 0x0004DA8B
		public static int CompareOrdinal(string strA, string strB)
		{
			if (strA == strB)
			{
				return 0;
			}
			if (strA == null)
			{
				return -1;
			}
			if (strB == null)
			{
				return 1;
			}
			if (strA.m_firstChar - strB.m_firstChar != '\0')
			{
				return (int)(strA.m_firstChar - strB.m_firstChar);
			}
			return string.CompareOrdinalHelper(strA, strB);
		}

		/// <summary>Compares substrings of two specified <see cref="T:System.String" /> objects by evaluating the numeric values of the corresponding <see cref="T:System.Char" /> objects in each substring. </summary>
		/// <returns>A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero The substring in <paramref name="strA" /> is less than the substring in <paramref name="strB" />. Zero The substrings are equal, or <paramref name="length" /> is zero. Greater than zero The substring in <paramref name="strA" /> is greater than the substring in <paramref name="strB" />. </returns>
		/// <param name="strA">The first string to use in the comparison. </param>
		/// <param name="indexA">The starting index of the substring in <paramref name="strA" />. </param>
		/// <param name="strB">The second string to use in the comparison. </param>
		/// <param name="indexB">The starting index of the substring in <paramref name="strB" />. </param>
		/// <param name="length">The maximum number of characters in the substrings to compare. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="strA" /> is not null and <paramref name="indexA" /> is greater than <paramref name="strA" />.<see cref="P:System.String.Length" />.-or- <paramref name="strB" /> is not null and<paramref name="indexB" /> is greater than <paramref name="strB" />.<see cref="P:System.String.Length" />.-or- <paramref name="indexA" />, <paramref name="indexB" />, or <paramref name="length" /> is negative. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001384 RID: 4996 RVA: 0x0004F8C1 File Offset: 0x0004DAC1
		[SecuritySafeCritical]
		public static int CompareOrdinal(string strA, int indexA, string strB, int indexB, int length)
		{
			if (strA != null && strB != null)
			{
				return string.nativeCompareOrdinalEx(strA, indexA, strB, indexB, length);
			}
			if (strA == strB)
			{
				return 0;
			}
			if (strA != null)
			{
				return 1;
			}
			return -1;
		}

		/// <summary>Returns a value indicating whether the specified <see cref="T:System.String" /> object occurs within this string.</summary>
		/// <returns>true if the <paramref name="value" /> parameter occurs within this string, or if <paramref name="value" /> is the empty string (""); otherwise, false.</returns>
		/// <param name="value">The string to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001385 RID: 4997 RVA: 0x0004F8E1 File Offset: 0x0004DAE1
		public bool Contains(string value)
		{
			return this.IndexOf(value, StringComparison.Ordinal) >= 0;
		}

		/// <summary>Determines whether the end of this string instance matches the specified string.</summary>
		/// <returns>true if <paramref name="value" /> matches the end of this instance; otherwise, false.</returns>
		/// <param name="value">The string to compare to the substring at the end of this instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001386 RID: 4998 RVA: 0x0004F8F1 File Offset: 0x0004DAF1
		public bool EndsWith(string value)
		{
			return this.EndsWith(value, StringComparison.CurrentCulture);
		}

		/// <summary>Determines whether the end of this string instance matches the specified string when compared using the specified comparison option.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the end of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare to the substring at the end of this instance. </param>
		/// <param name="comparisonType">One of the enumeration values that determines how this string and <paramref name="value" /> are compared. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06001387 RID: 4999 RVA: 0x0004F8FC File Offset: 0x0004DAFC
		[ComVisible(false)]
		[SecuritySafeCritical]
		public bool EndsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
			if (this == value)
			{
				return true;
			}
			if (value.Length == 0)
			{
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsSuffix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Length >= value.Length && string.nativeCompareOrdinalEx(this, this.Length - value.Length, value, 0, value.Length) == 0;
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && TextInfo.CompareOrdinalIgnoreCaseEx(this, this.Length - value.Length, value, 0, value.Length, value.Length) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
		}

		/// <summary>Determines whether the end of this string instance matches the specified string when compared using the specified culture.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the end of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare to the substring at the end of this instance. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <param name="culture">Cultural information that determines how this instance and <paramref name="value" /> are compared. If <paramref name="culture" /> is null, the current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001388 RID: 5000 RVA: 0x0004FA2C File Offset: 0x0004DC2C
		public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo;
			if (culture == null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
			else
			{
				cultureInfo = culture;
			}
			return cultureInfo.CompareInfo.IsSuffix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004FA70 File Offset: 0x0004DC70
		internal bool EndsWith(char value)
		{
			int length = this.Length;
			return length != 0 && this[length - 1] == value;
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified Unicode character in this string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600138A RID: 5002 RVA: 0x0004FA96 File Offset: 0x0004DC96
		public int IndexOf(char value)
		{
			return this.IndexOf(value, 0, this.Length);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified Unicode character in this string. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 (zero) or greater than the length of the string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600138B RID: 5003 RVA: 0x0004FAA6 File Offset: 0x0004DCA6
		public int IndexOf(char value, int startIndex)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex);
		}

		/// <summary>Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600138C RID: 5004 RVA: 0x0004FAB8 File Offset: 0x0004DCB8
		public int IndexOfAny(char[] anyOf)
		{
			return this.IndexOfAny(anyOf, 0, this.Length);
		}

		/// <summary>Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the number of characters in this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600138D RID: 5005 RVA: 0x0004FAC8 File Offset: 0x0004DCC8
		public int IndexOfAny(char[] anyOf, int startIndex)
		{
			return this.IndexOfAny(anyOf, startIndex, this.Length - startIndex);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is 0.</returns>
		/// <param name="value">The string to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600138E RID: 5006 RVA: 0x0004FADA File Offset: 0x0004DCDA
		public int IndexOf(string value)
		{
			return this.IndexOf(value, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in this instance. The search starts at a specified character position.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 (zero) or greater than the length of this string.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600138F RID: 5007 RVA: 0x0004FAE4 File Offset: 0x0004DCE4
		public int IndexOf(string value, int startIndex)
		{
			return this.IndexOf(value, startIndex, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this string.-or-<paramref name="count" /> is greater than the length of this string minus <paramref name="startIndex" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001390 RID: 5008 RVA: 0x0004FAF0 File Offset: 0x0004DCF0
		public int IndexOf(string value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0 || count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			return this.IndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. A parameter specifies the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is 0.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06001391 RID: 5009 RVA: 0x0004FB4D File Offset: 0x0004DD4D
		public int IndexOf(string value, StringComparison comparisonType)
		{
			return this.IndexOf(value, 0, this.Length, comparisonType);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string and the type of search to use for the specified string.</summary>
		/// <returns>The zero-based index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than 0 (zero) or greater than the length of this string. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06001392 RID: 5010 RVA: 0x0004FB5E File Offset: 0x0004DD5E
		public int IndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.IndexOf(value, startIndex, this.Length - startIndex, comparisonType);
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified string in the current <see cref="T:System.String" /> object. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.</summary>
		/// <returns>The zero-based index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is <paramref name="startIndex" />.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this instance.-or-<paramref name="count" /> is greater than the length of this string minus <paramref name="startIndex" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x06001393 RID: 5011 RVA: 0x0004FB74 File Offset: 0x0004DD74
		[SecuritySafeCritical]
		public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0 || startIndex > this.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
			case StringComparison.OrdinalIgnoreCase:
				if (value.IsAscii() && this.IsAscii())
				{
					return CultureInfo.InvariantCulture.CompareInfo.IndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				}
				return TextInfo.IndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
			default:
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">The Unicode character to seek. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001394 RID: 5012 RVA: 0x0004FCA9 File Offset: 0x0004DEA9
		public int LastIndexOf(char value)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="value">The Unicode character to seek. </param>
		/// <param name="startIndex">The starting position of the search. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than or equal to the length of this instance.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001395 RID: 5013 RVA: 0x0004FCC0 File Offset: 0x0004DEC0
		public int LastIndexOf(char value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1);
		}

		/// <summary>Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001396 RID: 5014 RVA: 0x0004FCCD File Offset: 0x0004DECD
		public int LastIndexOfAny(char[] anyOf)
		{
			return this.LastIndexOfAny(anyOf, this.Length - 1, this.Length);
		}

		/// <summary>Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the string.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> specifies a position that is not within this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001397 RID: 5015 RVA: 0x0004FCE4 File Offset: 0x0004DEE4
		public int LastIndexOfAny(char[] anyOf, int startIndex)
		{
			return this.LastIndexOfAny(anyOf, startIndex, startIndex + 1);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001398 RID: 5016 RVA: 0x0004FCF1 File Offset: 0x0004DEF1
		public int LastIndexOf(string value)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than the length of the current instance. -or-The current instance equals <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001399 RID: 5017 RVA: 0x0004FD09 File Offset: 0x0004DF09
		public int LastIndexOf(string value, int startIndex)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and  <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> - <paramref name="count" /> + 1 specifies a position that is not within this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600139A RID: 5018 RVA: 0x0004FD17 File Offset: 0x0004DF17
		public int LastIndexOf(string value, int startIndex, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			return this.LastIndexOf(value, startIndex, count, StringComparison.CurrentCulture);
		}

		/// <summary>Reports the zero-based index of the last occurrence of a specified string within the current <see cref="T:System.String" /> object. A parameter specifies the type of search to use for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600139B RID: 5019 RVA: 0x0004FD3C File Offset: 0x0004DF3C
		public int LastIndexOf(string value, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, this.Length - 1, this.Length, comparisonType);
		}

		/// <summary>Reports the zero-based index of the last occurrence of a specified string within the current <see cref="T:System.String" /> object. The search starts at a specified character position and proceeds backward toward the beginning of the string. A parameter specifies the type of comparison to perform when searching for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than the length of the current instance. -or-The current instance equals <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600139C RID: 5020 RVA: 0x0004FD54 File Offset: 0x0004DF54
		public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
		{
			return this.LastIndexOf(value, startIndex, startIndex + 1, comparisonType);
		}

		/// <summary>Reports the zero-based index position of the last occurrence of a specified string within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for the specified number of character positions. A parameter specifies the type of comparison to perform when searching for the specified string.</summary>
		/// <returns>The index position of the <paramref name="value" /> parameter if that string is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />. If <paramref name="value" /> is <see cref="F:System.String.Empty" />, the return value is the smaller of <paramref name="startIndex" /> and the last index position in this instance.</returns>
		/// <param name="value">The string to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <param name="comparisonType">One of the enumeration values that specifies the rules for the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is negative.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is greater than the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> + 1 - <paramref name="count" /> specifies a position that is not within this instance. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a valid <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x0600139D RID: 5021 RVA: 0x0004FD64 File Offset: 0x0004DF64
		[SecuritySafeCritical]
		public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > this.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
				}
				if (startIndex == this.Length)
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
				switch (comparisonType)
				{
				case StringComparison.CurrentCulture:
					return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
				case StringComparison.CurrentCultureIgnoreCase:
					return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				case StringComparison.InvariantCulture:
					return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.None);
				case StringComparison.InvariantCultureIgnoreCase:
					return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
				case StringComparison.Ordinal:
					return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.Ordinal);
				case StringComparison.OrdinalIgnoreCase:
					if (value.IsAscii() && this.IsAscii())
					{
						return CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(this, value, startIndex, count, CompareOptions.IgnoreCase);
					}
					return TextInfo.LastIndexOfStringOrdinalIgnoreCase(this, value, startIndex, count);
				default:
					throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
				}
			}
		}

		/// <summary>Returns a new string that right-aligns the characters in this instance by padding them with spaces on the left, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many spaces as needed to create a length of <paramref name="totalWidth" />. However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600139E RID: 5022 RVA: 0x0004FEDE File Offset: 0x0004E0DE
		public string PadLeft(int totalWidth)
		{
			return this.PadHelper(totalWidth, ' ', false);
		}

		/// <summary>Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many <paramref name="paddingChar" /> characters as needed to create a length of <paramref name="totalWidth" />. However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <param name="paddingChar">A Unicode padding character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600139F RID: 5023 RVA: 0x0004FEEA File Offset: 0x0004E0EA
		public string PadLeft(int totalWidth, char paddingChar)
		{
			return this.PadHelper(totalWidth, paddingChar, false);
		}

		/// <summary>Returns a new string that left-aligns the characters in this string by padding them with spaces on the right, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but left-aligned and padded on the right with as many spaces as needed to create a length of <paramref name="totalWidth" />. However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013A0 RID: 5024 RVA: 0x0004FEF5 File Offset: 0x0004E0F5
		public string PadRight(int totalWidth)
		{
			return this.PadHelper(totalWidth, ' ', true);
		}

		/// <summary>Returns a new string that left-aligns the characters in this string by padding them on the right with a specified Unicode character, for a specified total length.</summary>
		/// <returns>A new string that is equivalent to this instance, but left-aligned and padded on the right with as many <paramref name="paddingChar" /> characters as needed to create a length of <paramref name="totalWidth" />.  However, if <paramref name="totalWidth" /> is less than the length of this instance, the method returns a reference to the existing instance. If <paramref name="totalWidth" /> is equal to the length of this instance, the method returns a new string that is identical to this instance.</returns>
		/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
		/// <param name="paddingChar">A Unicode padding character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="totalWidth" /> is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013A1 RID: 5025 RVA: 0x0004FF01 File Offset: 0x0004E101
		public string PadRight(int totalWidth, char paddingChar)
		{
			return this.PadHelper(totalWidth, paddingChar, true);
		}

		/// <summary>Determines whether the beginning of this string instance matches the specified string.</summary>
		/// <returns>true if <paramref name="value" /> matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013A2 RID: 5026 RVA: 0x0004FF0C File Offset: 0x0004E10C
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.StartsWith(value, StringComparison.CurrentCulture);
		}

		/// <summary>Determines whether the beginning of this string instance matches the specified string when compared using the specified comparison option.</summary>
		/// <returns>true if this instance begins with <paramref name="value" />; otherwise, false.</returns>
		/// <param name="value">The string to compare. </param>
		/// <param name="comparisonType">One of the enumeration values that determines how this string and <paramref name="value" /> are compared. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="comparisonType" /> is not a <see cref="T:System.StringComparison" /> value.</exception>
		// Token: 0x060013A3 RID: 5027 RVA: 0x0004FF24 File Offset: 0x0004E124
		[ComVisible(false)]
		[SecuritySafeCritical]
		public bool StartsWith(string value, StringComparison comparisonType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (comparisonType < StringComparison.CurrentCulture || comparisonType > StringComparison.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
			if (this == value)
			{
				return true;
			}
			if (value.Length == 0)
			{
				return true;
			}
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.InvariantCulture:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.None);
			case StringComparison.InvariantCultureIgnoreCase:
				return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this, value, CompareOptions.IgnoreCase);
			case StringComparison.Ordinal:
				return this.Length >= value.Length && string.nativeCompareOrdinalEx(this, 0, value, 0, value.Length) == 0;
			case StringComparison.OrdinalIgnoreCase:
				return this.Length >= value.Length && TextInfo.CompareOrdinalIgnoreCaseEx(this, 0, value, 0, value.Length, value.Length) == 0;
			default:
				throw new ArgumentException(Environment.GetResourceString("The string comparison type passed in is currently not supported."), "comparisonType");
			}
		}

		/// <summary>Determines whether the beginning of this string instance matches the specified string when compared using the specified culture.</summary>
		/// <returns>true if the <paramref name="value" /> parameter matches the beginning of this string; otherwise, false.</returns>
		/// <param name="value">The string to compare. </param>
		/// <param name="ignoreCase">true to ignore case during the comparison; otherwise, false.</param>
		/// <param name="culture">Cultural information that determines how this string and <paramref name="value" /> are compared. If <paramref name="culture" /> is null, the current culture is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013A4 RID: 5028 RVA: 0x0005003C File Offset: 0x0004E23C
		public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this == value)
			{
				return true;
			}
			CultureInfo cultureInfo;
			if (culture == null)
			{
				cultureInfo = CultureInfo.CurrentCulture;
			}
			else
			{
				cultureInfo = culture;
			}
			return cultureInfo.CompareInfo.IsPrefix(this, value, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		/// <summary>Returns a copy of this string converted to lowercase.</summary>
		/// <returns>A string in lowercase.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060013A5 RID: 5029 RVA: 0x0005007E File Offset: 0x0004E27E
		public string ToLower()
		{
			return this.ToLower(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a copy of this string converted to lowercase, using the casing rules of the specified culture.</summary>
		/// <returns>The lowercase equivalent of the current string.</returns>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060013A6 RID: 5030 RVA: 0x0005008B File Offset: 0x0004E28B
		public string ToLower(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(this);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> object converted to lowercase using the casing rules of the invariant culture.</summary>
		/// <returns>The lowercase equivalent of the current string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060013A7 RID: 5031 RVA: 0x000500A7 File Offset: 0x0004E2A7
		public string ToLowerInvariant()
		{
			return this.ToLower(CultureInfo.InvariantCulture);
		}

		/// <summary>Returns a copy of this string converted to uppercase.</summary>
		/// <returns>The uppercase equivalent of the current string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060013A8 RID: 5032 RVA: 0x000500B4 File Offset: 0x0004E2B4
		public string ToUpper()
		{
			return this.ToUpper(CultureInfo.CurrentCulture);
		}

		/// <summary>Returns a copy of this string converted to uppercase, using the casing rules of the specified culture.</summary>
		/// <returns>The uppercase equivalent of the current string.</returns>
		/// <param name="culture">An object that supplies culture-specific casing rules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060013A9 RID: 5033 RVA: 0x000500C1 File Offset: 0x0004E2C1
		public string ToUpper(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(this);
		}

		/// <summary>Returns a copy of this <see cref="T:System.String" /> object converted to uppercase using the casing rules of the invariant culture.</summary>
		/// <returns>The uppercase equivalent of the current string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060013AA RID: 5034 RVA: 0x000500DD File Offset: 0x0004E2DD
		public string ToUpperInvariant()
		{
			return this.ToUpper(CultureInfo.InvariantCulture);
		}

		/// <summary>Returns this instance of <see cref="T:System.String" />; no actual conversion is performed.</summary>
		/// <returns>The current string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013AB RID: 5035 RVA: 0x00002119 File Offset: 0x00000319
		public override string ToString()
		{
			return this;
		}

		/// <summary>Returns this instance of <see cref="T:System.String" />; no actual conversion is performed.</summary>
		/// <returns>The current string.</returns>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013AC RID: 5036 RVA: 0x00002119 File Offset: 0x00000319
		public string ToString(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Returns a reference to this instance of <see cref="T:System.String" />.</summary>
		/// <returns>This instance of <see cref="T:System.String" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013AD RID: 5037 RVA: 0x00002119 File Offset: 0x00000319
		public object Clone()
		{
			return this;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00015ED5 File Offset: 0x000140D5
		private static bool IsBOMWhitespace(char c)
		{
			return false;
		}

		/// <summary>Removes all leading and trailing white-space characters from the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The string that remains after all white-space characters are removed from the start and end of the current string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013AF RID: 5039 RVA: 0x000500EA File Offset: 0x0004E2EA
		public string Trim()
		{
			return this.TrimHelper(2);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x000500F4 File Offset: 0x0004E2F4
		[SecuritySafeCritical]
		private string TrimHelper(int trimType)
		{
			int num = this.Length - 1;
			int num2 = 0;
			if (trimType != 1)
			{
				num2 = 0;
				while (num2 < this.Length && (char.IsWhiteSpace(this[num2]) || string.IsBOMWhitespace(this[num2])))
				{
					num2++;
				}
			}
			if (trimType != 0)
			{
				num = this.Length - 1;
				while (num >= num2 && (char.IsWhiteSpace(this[num]) || string.IsBOMWhitespace(this[num2])))
				{
					num--;
				}
			}
			return this.CreateTrimmedString(num2, num);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00050178 File Offset: 0x0004E378
		[SecuritySafeCritical]
		private string TrimHelper(char[] trimChars, int trimType)
		{
			int i = this.Length - 1;
			int j = 0;
			if (trimType != 1)
			{
				for (j = 0; j < this.Length; j++)
				{
					char c = this[j];
					int num = 0;
					while (num < trimChars.Length && trimChars[num] != c)
					{
						num++;
					}
					if (num == trimChars.Length)
					{
						break;
					}
				}
			}
			if (trimType != 0)
			{
				for (i = this.Length - 1; i >= j; i--)
				{
					char c2 = this[i];
					int num2 = 0;
					while (num2 < trimChars.Length && trimChars[num2] != c2)
					{
						num2++;
					}
					if (num2 == trimChars.Length)
					{
						break;
					}
				}
			}
			return this.CreateTrimmedString(j, i);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00050214 File Offset: 0x0004E414
		[SecurityCritical]
		private string CreateTrimmedString(int start, int end)
		{
			int num = end - start + 1;
			if (num == this.Length)
			{
				return this;
			}
			if (num == 0)
			{
				return string.Empty;
			}
			return this.InternalSubString(start, num);
		}

		/// <summary>Returns a new string in which a specified string is inserted at a specified index position in this instance. </summary>
		/// <returns>A new string that is equivalent to this instance, but with <paramref name="value" /> inserted at position <paramref name="startIndex" />.</returns>
		/// <param name="startIndex">The zero-based index position of the insertion. </param>
		/// <param name="value">The string to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is negative or greater than the length of this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B3 RID: 5043 RVA: 0x00050244 File Offset: 0x0004E444
		[SecuritySafeCritical]
		public unsafe string Insert(int startIndex, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex < 0 || startIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int length = this.Length;
			int length2 = value.Length;
			int num = length + length2;
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &value.m_firstChar)
				{
					char* ptr4 = ptr3;
					fixed (char* ptr5 = &text.m_firstChar)
					{
						char* ptr6 = ptr5;
						string.wstrcpy(ptr6, ptr2, startIndex);
						string.wstrcpy(ptr6 + startIndex, ptr4, length2);
						string.wstrcpy(ptr6 + startIndex + length2, ptr2 + startIndex, length - startIndex);
					}
				}
			}
			return text;
		}

		/// <summary>Returns a new string in which all occurrences of a specified Unicode character in this instance are replaced with another specified Unicode character.</summary>
		/// <returns>A string that is equivalent to this instance except that all instances of <paramref name="oldChar" /> are replaced with <paramref name="newChar" />. If <paramref name="oldChar" /> is not found in the current instance, the method returns the current instance unchanged. </returns>
		/// <param name="oldChar">The Unicode character to be replaced. </param>
		/// <param name="newChar">The Unicode character to replace all occurrences of <paramref name="oldChar" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B4 RID: 5044 RVA: 0x000502F6 File Offset: 0x0004E4F6
		public string Replace(char oldChar, char newChar)
		{
			return this.ReplaceInternal(oldChar, newChar);
		}

		/// <summary>Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.</summary>
		/// <returns>A string that is equivalent to the current string except that all instances of <paramref name="oldValue" /> are replaced with <paramref name="newValue" />. If <paramref name="oldValue" /> is not found in the current instance, the method returns the current instance unchanged. </returns>
		/// <param name="oldValue">The string to be replaced. </param>
		/// <param name="newValue">The string to replace all occurrences of <paramref name="oldValue" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldValue" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldValue" /> is the empty string (""). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B5 RID: 5045 RVA: 0x00050300 File Offset: 0x0004E500
		public string Replace(string oldValue, string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			return this.ReplaceInternal(oldValue, newValue);
		}

		/// <summary>Returns a new string in which a specified number of characters in the current this instance beginning at a specified position have been deleted.</summary>
		/// <returns>A new string that is equivalent to this instance except for the removed characters.</returns>
		/// <param name="startIndex">The zero-based position to begin deleting characters. </param>
		/// <param name="count">The number of characters to delete. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Either <paramref name="startIndex" /> or <paramref name="count" /> is less than zero.-or- <paramref name="startIndex" /> plus <paramref name="count" /> specify a position outside this instance. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B6 RID: 5046 RVA: 0x00050318 File Offset: 0x0004E518
		[SecuritySafeCritical]
		public unsafe string Remove(int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (count > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Index and count must refer to a location within the string."));
			}
			int num = this.Length - count;
			if (num == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(num);
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &text.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr4, ptr2, startIndex);
					string.wstrcpy(ptr4 + startIndex, ptr2 + startIndex + count, num - startIndex);
				}
			}
			return text;
		}

		/// <summary>Returns a new string in which all the characters in the current instance, beginning at a specified position and continuing through the last position, have been deleted.</summary>
		/// <returns>A new string that is equivalent to this string except for the removed characters.</returns>
		/// <param name="startIndex">The zero-based position to begin deleting characters. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is less than zero.-or- <paramref name="startIndex" /> specifies a position that is not within this string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B7 RID: 5047 RVA: 0x000503C8 File Offset: 0x0004E5C8
		public string Remove(int startIndex)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("StartIndex cannot be less than zero."));
			}
			if (startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("startIndex must be less than length of string."));
			}
			return this.Substring(0, startIndex);
		}

		/// <summary>Replaces one or more format items in a specified string with the string representation of a specified object.</summary>
		/// <returns>A copy of <paramref name="format" /> in which any format items are replaced by the string representation of <paramref name="arg0" />.</returns>
		/// <param name="format">A composite format string. </param>
		/// <param name="arg0">The object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format item in <paramref name="format" /> is invalid.-or- The index of a format item is not zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B8 RID: 5048 RVA: 0x00050414 File Offset: 0x0004E614
		public static string Format(string format, object arg0)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0));
		}

		/// <summary>Replaces the format items in a specified string with the string representation of two specified objects.</summary>
		/// <returns>A copy of <paramref name="format" /> in which format items are replaced by the string representations of <paramref name="arg0" /> and <paramref name="arg1" />.</returns>
		/// <param name="format">A composite format string. </param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is not zero or one. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013B9 RID: 5049 RVA: 0x00050423 File Offset: 0x0004E623
		public static string Format(string format, object arg0, object arg1)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		/// <summary>Replaces the format items in a specified string with the string representation of three specified objects.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representations of <paramref name="arg0" />, <paramref name="arg1" />, and <paramref name="arg2" />.</returns>
		/// <param name="format">A composite format string.</param>
		/// <param name="arg0">The first object to format. </param>
		/// <param name="arg1">The second object to format. </param>
		/// <param name="arg2">The third object to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than two. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BA RID: 5050 RVA: 0x00050433 File Offset: 0x0004E633
		public static string Format(string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args" />.</returns>
		/// <param name="format">A composite format string.</param>
		/// <param name="args">An object array that contains zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BB RID: 5051 RVA: 0x00050444 File Offset: 0x0004E644
		public static string Format(string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0005046B File Offset: 0x0004E66B
		public static string Format(IFormatProvider provider, string format, object arg0)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0005047A File Offset: 0x0004E67A
		public static string Format(IFormatProvider provider, string format, object arg0, object arg1)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0005048A File Offset: 0x0004E68A
		public static string Format(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
		{
			return string.FormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		/// <summary>Replaces the format items in a specified string with the string representations of corresponding objects in a specified array. A parameter supplies culture-specific formatting information.</summary>
		/// <returns>A copy of <paramref name="format" /> in which the format items have been replaced by the string representation of the corresponding objects in <paramref name="args" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <param name="format">A composite format string. </param>
		/// <param name="args">An object array that contains zero or more objects to format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013BF RID: 5055 RVA: 0x0005049C File Offset: 0x0004E69C
		public static string Format(IFormatProvider provider, string format, params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException((format == null) ? "format" : "args");
			}
			return string.FormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000504C3 File Offset: 0x0004E6C3
		private static string FormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(format.Length + args.Length * 8).AppendFormatHelper(provider, format, args));
		}

		/// <summary>Creates a new instance of <see cref="T:System.String" /> with the same value as a specified <see cref="T:System.String" />.</summary>
		/// <returns>A new string with the same value as <paramref name="str" />.</returns>
		/// <param name="str">The string to copy. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C1 RID: 5057 RVA: 0x000504F8 File Offset: 0x0004E6F8
		[SecuritySafeCritical]
		public unsafe static string Copy(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			string text = string.FastAllocateString(length);
			fixed (char* ptr = &text.m_firstChar)
			{
				char* ptr2 = ptr;
				fixed (char* ptr3 = &str.m_firstChar)
				{
					char* ptr4 = ptr3;
					string.wstrcpy(ptr2, ptr4, length);
				}
			}
			return text;
		}

		/// <summary>Creates the string  representation of a specified object.</summary>
		/// <returns>The string representation of the value of <paramref name="arg0" />, or <see cref="F:System.String.Empty" /> if <paramref name="arg0" /> is null.</returns>
		/// <param name="arg0">The object to represent, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C2 RID: 5058 RVA: 0x00050540 File Offset: 0x0004E740
		public static string Concat(object arg0)
		{
			if (arg0 == null)
			{
				return string.Empty;
			}
			return arg0.ToString();
		}

		/// <summary>Concatenates the string representations of two specified objects.</summary>
		/// <returns>The concatenated string representations of the values of <paramref name="arg0" /> and <paramref name="arg1" />.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C3 RID: 5059 RVA: 0x00050551 File Offset: 0x0004E751
		public static string Concat(object arg0, object arg1)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString();
		}

		/// <summary>Concatenates the string representations of three specified objects.</summary>
		/// <returns>The concatenated string representations of the values of <paramref name="arg0" />, <paramref name="arg1" />, and <paramref name="arg2" />.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <param name="arg2">The third object to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C4 RID: 5060 RVA: 0x00050578 File Offset: 0x0004E778
		public static string Concat(object arg0, object arg1, object arg2)
		{
			if (arg0 == null)
			{
				arg0 = string.Empty;
			}
			if (arg1 == null)
			{
				arg1 = string.Empty;
			}
			if (arg2 == null)
			{
				arg2 = string.Empty;
			}
			return arg0.ToString() + arg1.ToString() + arg2.ToString();
		}

		/// <summary>Concatenates the string representations of four specified objects and any objects specified in an optional variable length parameter list.</summary>
		/// <returns>The concatenated string representation of each value in the parameter list.</returns>
		/// <param name="arg0">The first object to concatenate. </param>
		/// <param name="arg1">The second object to concatenate. </param>
		/// <param name="arg2">The third object to concatenate. </param>
		/// <param name="arg3">The fourth object to concatenate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C5 RID: 5061 RVA: 0x000505B0 File Offset: 0x0004E7B0
		[CLSCompliant(false)]
		public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int num = argIterator.GetRemainingCount() + 4;
			object[] array = new object[num];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array[i] = TypedReference.ToObject(argIterator.GetNextArg());
			}
			return string.Concat(array);
		}

		/// <summary>Concatenates the string representations of the elements in a specified <see cref="T:System.Object" /> array.</summary>
		/// <returns>The concatenated string representations of the values of the elements in <paramref name="args" />.</returns>
		/// <param name="args">An object array that contains the elements to concatenate. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C6 RID: 5062 RVA: 0x00050608 File Offset: 0x0004E808
		public static string Concat(params object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			string[] array = new string[args.Length];
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				object obj = args[i];
				array[i] = ((obj == null) ? string.Empty : obj.ToString());
				if (array[i] == null)
				{
					array[i] = string.Empty;
				}
				num += array[i].Length;
				if (num < 0)
				{
					throw new OutOfMemoryException();
				}
			}
			return string.ConcatArray(array, num);
		}

		/// <summary>Concatenates the members of an <see cref="T:System.Collections.Generic.IEnumerable`1" /> implementation.</summary>
		/// <returns>The concatenated members in <paramref name="values" />.</returns>
		/// <param name="values">A collection object that implements the <see cref="T:System.Collections.Generic.IEnumerable`1" /> interface.</param>
		/// <typeparam name="T">The type of the members of <paramref name="values" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x060013C7 RID: 5063 RVA: 0x0005067C File Offset: 0x0004E87C
		[ComVisible(false)]
		public static string Concat<T>(IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text = t.ToString();
						if (text != null)
						{
							stringBuilder.Append(text);
						}
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable`1" /> collection of type <see cref="T:System.String" />.</summary>
		/// <returns>The concatenated strings in <paramref name="values" />.</returns>
		/// <param name="values">A collection object that implements <see cref="T:System.Collections.Generic.IEnumerable`1" /> and whose generic type argument is <see cref="T:System.String" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		// Token: 0x060013C8 RID: 5064 RVA: 0x00050700 File Offset: 0x0004E900
		[ComVisible(false)]
		public static string Concat(IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			using (IEnumerator<string> enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current != null)
					{
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		/// <summary>Concatenates two specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" /> and <paramref name="str1" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013C9 RID: 5065 RVA: 0x0005076C File Offset: 0x0004E96C
		[SecuritySafeCritical]
		public static string Concat(string str0, string str1)
		{
			if (string.IsNullOrEmpty(str0))
			{
				if (string.IsNullOrEmpty(str1))
				{
					return string.Empty;
				}
				return str1;
			}
			else
			{
				if (string.IsNullOrEmpty(str1))
				{
					return str0;
				}
				int length = str0.Length;
				string text = string.FastAllocateString(length + str1.Length);
				string.FillStringChecked(text, 0, str0);
				string.FillStringChecked(text, length, str1);
				return text;
			}
		}

		/// <summary>Concatenates three specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" />, <paramref name="str1" />, and <paramref name="str2" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <param name="str2">The third string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CA RID: 5066 RVA: 0x000507C0 File Offset: 0x0004E9C0
		[SecuritySafeCritical]
		public static string Concat(string str0, string str1, string str2)
		{
			if (str0 == null && str1 == null && str2 == null)
			{
				return string.Empty;
			}
			if (str0 == null)
			{
				str0 = string.Empty;
			}
			if (str1 == null)
			{
				str1 = string.Empty;
			}
			if (str2 == null)
			{
				str2 = string.Empty;
			}
			string text = string.FastAllocateString(str0.Length + str1.Length + str2.Length);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			return text;
		}

		/// <summary>Concatenates four specified instances of <see cref="T:System.String" />.</summary>
		/// <returns>The concatenation of <paramref name="str0" />, <paramref name="str1" />, <paramref name="str2" />, and <paramref name="str3" />.</returns>
		/// <param name="str0">The first string to concatenate. </param>
		/// <param name="str1">The second string to concatenate. </param>
		/// <param name="str2">The third string to concatenate. </param>
		/// <param name="str3">The fourth string to concatenate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CB RID: 5067 RVA: 0x0005083C File Offset: 0x0004EA3C
		[SecuritySafeCritical]
		public static string Concat(string str0, string str1, string str2, string str3)
		{
			if (str0 == null && str1 == null && str2 == null && str3 == null)
			{
				return string.Empty;
			}
			if (str0 == null)
			{
				str0 = string.Empty;
			}
			if (str1 == null)
			{
				str1 = string.Empty;
			}
			if (str2 == null)
			{
				str2 = string.Empty;
			}
			if (str3 == null)
			{
				str3 = string.Empty;
			}
			string text = string.FastAllocateString(str0.Length + str1.Length + str2.Length + str3.Length);
			string.FillStringChecked(text, 0, str0);
			string.FillStringChecked(text, str0.Length, str1);
			string.FillStringChecked(text, str0.Length + str1.Length, str2);
			string.FillStringChecked(text, str0.Length + str1.Length + str2.Length, str3);
			return text;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000508E8 File Offset: 0x0004EAE8
		[SecuritySafeCritical]
		private static string ConcatArray(string[] values, int totalLength)
		{
			string text = string.FastAllocateString(totalLength);
			int num = 0;
			for (int i = 0; i < values.Length; i++)
			{
				string.FillStringChecked(text, num, values[i]);
				num += values[i].Length;
			}
			return text;
		}

		/// <summary>Concatenates the elements of a specified <see cref="T:System.String" /> array.</summary>
		/// <returns>The concatenated elements of <paramref name="values" />.</returns>
		/// <param name="values">An array of string instances. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <exception cref="T:System.OutOfMemoryException">Out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013CD RID: 5069 RVA: 0x00050924 File Offset: 0x0004EB24
		public static string Concat(params string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = 0;
			string[] array = new string[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				string text = values[i];
				array[i] = ((text == null) ? string.Empty : text);
				num += array[i].Length;
				if (num < 0)
				{
					throw new OutOfMemoryException();
				}
			}
			return string.ConcatArray(array, num);
		}

		/// <summary>Retrieves the system's reference to the specified <see cref="T:System.String" />.</summary>
		/// <returns>The system's reference to <paramref name="str" />, if it is interned; otherwise, a new reference to a string with the value of <paramref name="str" />.</returns>
		/// <param name="str">A string to search for in the intern pool. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013CE RID: 5070 RVA: 0x00050986 File Offset: 0x0004EB86
		[SecuritySafeCritical]
		public static string Intern(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIntern(str);
		}

		/// <summary>Retrieves a reference to a specified <see cref="T:System.String" />.</summary>
		/// <returns>A reference to <paramref name="str" /> if it is in the common language runtime intern pool; otherwise, null.</returns>
		/// <param name="str">The string to search for in the intern pool. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013CF RID: 5071 RVA: 0x0005099C File Offset: 0x0004EB9C
		[SecuritySafeCritical]
		public static string IsInterned(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return string.InternalIsInterned(str);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for class <see cref="T:System.String" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.String" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013D0 RID: 5072 RVA: 0x000509B2 File Offset: 0x0004EBB2
		public TypeCode GetTypeCode()
		{
			return TypeCode.String;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />. </summary>
		/// <returns>true if the value of the current string is <see cref="F:System.Boolean.TrueString" />; false if the value of the current string is <see cref="F:System.Boolean.FalseString" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.FormatException">The value of the current string is not <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x060013D1 RID: 5073 RVA: 0x000509B6 File Offset: 0x0004EBB6
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <returns>The character at index 0 in the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x060013D2 RID: 5074 RVA: 0x000509BF File Offset: 0x0004EBBF
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		// Token: 0x060013D3 RID: 5075 RVA: 0x000509C8 File Offset: 0x0004EBC8
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		// Token: 0x060013D4 RID: 5076 RVA: 0x000509D1 File Offset: 0x0004EBD1
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x060013D5 RID: 5077 RVA: 0x000509DA File Offset: 0x0004EBDA
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />.</exception>
		// Token: 0x060013D6 RID: 5078 RVA: 0x000509E3 File Offset: 0x0004EBE3
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x060013D7 RID: 5079 RVA: 0x000509EC File Offset: 0x0004EBEC
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number greater <see cref="F:System.UInt32.MaxValue" /> or less than <see cref="F:System.UInt32.MinValue" /></exception>
		// Token: 0x060013D8 RID: 5080 RVA: 0x000509F5 File Offset: 0x0004EBF5
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x060013D9 RID: 5081 RVA: 0x000509FE File Offset: 0x0004EBFE
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x060013DA RID: 5082 RVA: 0x00050A07 File Offset: 0x0004EC07
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x060013DB RID: 5083 RVA: 0x00050A10 File Offset: 0x0004EC10
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		// Token: 0x060013DC RID: 5084 RVA: 0x00050A19 File Offset: 0x0004EC19
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">The value of the current <see cref="T:System.String" /> object cannot be parsed. </exception>
		/// <exception cref="T:System.OverflowException">The value of the current <see cref="T:System.String" /> object is a number less than <see cref="F:System.Decimal.MinValue" /> or than <see cref="F:System.Decimal.MaxValue" /> greater. </exception>
		// Token: 0x060013DD RID: 5085 RVA: 0x00050A22 File Offset: 0x0004EC22
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDateTime(System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="provider">An object that provides culture-specific formatting information. </param>
		// Token: 0x060013DE RID: 5086 RVA: 0x00050A2B File Offset: 0x0004EC2B
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(this, provider);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>The converted value of the current <see cref="T:System.String" /> object.</returns>
		/// <param name="type">The type of the returned object. </param>
		/// <param name="provider">An object that provides culture-specific formatting information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.String" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter. </exception>
		// Token: 0x060013DF RID: 5087 RVA: 0x0003A52C File Offset: 0x0003872C
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Retrieves an object that can iterate through the individual characters in this string.</summary>
		/// <returns>An enumerator object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E0 RID: 5088 RVA: 0x00050A34 File Offset: 0x0004EC34
		public CharEnumerator GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00050A34 File Offset: 0x0004EC34
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through the current <see cref="T:System.String" /> object. </summary>
		/// <returns>An enumerator that can be used to iterate through the current string.</returns>
		// Token: 0x060013E2 RID: 5090 RVA: 0x00050A34 File Offset: 0x0004EC34
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CharEnumerator(this);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00050A3C File Offset: 0x0004EC3C
		[SecurityCritical]
		internal unsafe static void InternalCopy(string src, IntPtr dest, int len)
		{
			if (len == 0)
			{
				return;
			}
			fixed (char* ptr = &src.m_firstChar)
			{
				byte* ptr2 = (byte*)ptr;
				byte* ptr3 = (byte*)(void*)dest;
				Buffer.Memcpy(ptr3, ptr2, len);
			}
		}

		/// <summary>Gets the number of characters in the current <see cref="T:System.String" /> object.</summary>
		/// <returns>The number of characters in the current string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00050A69 File Offset: 0x0004EC69
		public int Length
		{
			get
			{
				return this.m_stringLength;
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00050A74 File Offset: 0x0004EC74
		internal unsafe static int CompareOrdinalUnchecked(string strA, int indexA, int lenA, string strB, int indexB, int lenB)
		{
			if (strA == null)
			{
				if (strB != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.m_stringLength - indexA);
				int num2 = Math.Min(lenB, strB.m_stringLength - indexB);
				if (num == num2 && indexA == indexB && strA == strB)
				{
					return 0;
				}
				char* ptr = strA;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = strB;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr3 = ptr + indexA;
				char* ptr4 = ptr3 + Math.Min(num, num2);
				char* ptr5 = ptr2 + indexB;
				while (ptr3 < ptr4)
				{
					if (*ptr3 != *ptr5)
					{
						return (int)(*ptr3 - *ptr5);
					}
					ptr3++;
					ptr5++;
				}
				return num - num2;
			}
		}

		/// <summary>Reports the zero-based index of the first occurrence of the specified character in this instance. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not.</returns>
		/// <param name="value">A Unicode character to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="startIndex" /> is greater than the length of this string.-or-<paramref name="count" /> is greater than the length of this string minus <paramref name="startIndex" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013E6 RID: 5094 RVA: 0x00050B2C File Offset: 0x0004ED2C
		public int IndexOf(char value, int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this.m_stringLength)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Cannot be negative and must be< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (startIndex > this.m_stringLength - count)
			{
				throw new ArgumentOutOfRangeException("count", "startIndex + count > this.m_stringLength");
			}
			if ((startIndex == 0 && this.m_stringLength == 0) || startIndex == this.m_stringLength || count == 0)
			{
				return -1;
			}
			return this.IndexOfUnchecked(value, startIndex, count);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00050BA8 File Offset: 0x0004EDA8
		internal unsafe int IndexOfUnchecked(char value, int startIndex, int count)
		{
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startIndex;
				char* ptr4 = ptr3 + (count >> 3 << 3);
				while (ptr3 != ptr4)
				{
					if (*ptr3 == value)
					{
						return (int)((long)(ptr3 - ptr2));
					}
					if (ptr3[1] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 1L);
					}
					if (ptr3[2] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 2L);
					}
					if (ptr3[3] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 3L);
					}
					if (ptr3[4] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 4L);
					}
					if (ptr3[5] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 5L);
					}
					if (ptr3[6] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 6L);
					}
					if (ptr3[7] == value)
					{
						return (int)((long)(ptr3 - ptr2) + 7L);
					}
					ptr3 += 8;
				}
				ptr4 += count & 7;
				while (ptr3 != ptr4)
				{
					if (*ptr3 == value)
					{
						return (int)((long)(ptr3 - ptr2));
					}
					ptr3++;
				}
				return -1;
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00050CAC File Offset: 0x0004EEAC
		internal unsafe int IndexOfUnchecked(string value, int startIndex, int count)
		{
			int length = value.Length;
			if (count < length)
			{
				return -1;
			}
			if (length > 1)
			{
				fixed (char* ptr = &this.m_firstChar)
				{
					char* ptr2 = ptr;
					fixed (string text = value)
					{
						char* ptr3 = text;
						if (ptr3 != null)
						{
							ptr3 += RuntimeHelpers.OffsetToStringData / 2;
						}
						char* ptr4 = ptr2 + startIndex;
						char* ptr5 = ptr4 + count - length + 1;
						while (ptr4 != ptr5)
						{
							if (*ptr4 == *ptr3)
							{
								for (int i = 1; i < length; i++)
								{
									if (ptr4[i] != ptr3[i])
									{
										goto IL_0090;
									}
								}
								return (int)((long)(ptr4 - ptr2));
							}
							IL_0090:
							ptr4++;
						}
						ptr = null;
					}
					return -1;
				}
			}
			if (length == 1)
			{
				return this.IndexOfUnchecked(value[0], startIndex, count);
			}
			return startIndex;
		}

		/// <summary>Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode characters. The search starts at a specified character position and examines a specified number of character positions.</summary>
		/// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. </param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- <paramref name="count" /> + <paramref name="startIndex" /> is greater than the number of characters in this instance. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E9 RID: 5097 RVA: 0x00050D5C File Offset: 0x0004EF5C
		public int IndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (startIndex < 0 || startIndex > this.m_stringLength)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (count < 0 || startIndex > this.m_stringLength - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be negative, and startIndex + count must be less than m_stringLength of the string.");
			}
			return this.IndexOfAnyUnchecked(anyOf, startIndex, count);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00050DB0 File Offset: 0x0004EFB0
		private unsafe int IndexOfAnyUnchecked(char[] anyOf, int startIndex, int count)
		{
			if (anyOf.Length == 0)
			{
				return -1;
			}
			if (anyOf.Length == 1)
			{
				return this.IndexOfUnchecked(anyOf[0], startIndex, count);
			}
			fixed (char[] array = anyOf)
			{
				char* ptr;
				if (anyOf == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				int num = (int)(*ptr);
				int num2 = (int)(*ptr);
				char* ptr2 = ptr + anyOf.Length;
				char* ptr3 = ptr;
				while (++ptr3 != ptr2)
				{
					if ((int)(*ptr3) > num)
					{
						num = (int)(*ptr3);
					}
					else if ((int)(*ptr3) < num2)
					{
						num2 = (int)(*ptr3);
					}
				}
				fixed (char* ptr4 = &this.m_firstChar)
				{
					char* ptr5 = ptr4;
					char* ptr6 = ptr5 + startIndex;
					char* ptr7 = ptr6 + count;
					while (ptr6 != ptr7)
					{
						if ((int)(*ptr6) > num || (int)(*ptr6) < num2)
						{
							ptr6++;
						}
						else
						{
							if (*ptr6 == *ptr)
							{
								return (int)((long)(ptr6 - ptr5));
							}
							ptr3 = ptr;
							while (++ptr3 != ptr2)
							{
								if (*ptr6 == *ptr3)
								{
									return (int)((long)(ptr6 - ptr5));
								}
							}
							ptr6++;
						}
					}
				}
			}
			return -1;
		}

		/// <summary>Reports the zero-based index position of the last occurrence of the specified Unicode character in a substring within this instance. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.</summary>
		/// <returns>The zero-based index position of <paramref name="value" /> if that character is found, or -1 if it is not found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="value">The Unicode character to seek. </param>
		/// <param name="startIndex">The starting position of the search. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> is less than zero or greater than or equal to the length of this instance.-or-The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> - <paramref name="count" /> + 1 is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060013EB RID: 5099 RVA: 0x00050EA0 File Offset: 0x0004F0A0
		public int LastIndexOf(char value, int startIndex, int count)
		{
			if (this.m_stringLength == 0)
			{
				return -1;
			}
			if (startIndex < 0 || startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || >= this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			return this.LastIndexOfUnchecked(value, startIndex, count);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00050F10 File Offset: 0x0004F110
		internal unsafe int LastIndexOfUnchecked(char value, int startIndex, int count)
		{
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startIndex;
				char* ptr4 = ptr3 - (count >> 3 << 3);
				while (ptr3 != ptr4)
				{
					if (*ptr3 == value)
					{
						return (int)((long)(ptr3 - ptr2));
					}
					if (ptr3[-1] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 1;
					}
					if (ptr3[-2] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 2;
					}
					if (ptr3[-3] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 3;
					}
					if (ptr3[-4] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 4;
					}
					if (ptr3[-5] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 5;
					}
					if (ptr3[-6] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 6;
					}
					if (ptr3[-7] == value)
					{
						return (int)((long)(ptr3 - ptr2)) - 7;
					}
					ptr3 -= 8;
				}
				ptr4 -= count & 7;
				while (ptr3 != ptr4)
				{
					if (*ptr3 == value)
					{
						return (int)((long)(ptr3 - ptr2));
					}
					ptr3--;
				}
				return -1;
			}
		}

		/// <summary>Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the string for a specified number of character positions.</summary>
		/// <returns>The index position of the last occurrence in this instance where any character in <paramref name="anyOf" /> was found; -1 if no character in <paramref name="anyOf" /> was found or if the current instance equals <see cref="F:System.String.Empty" />.</returns>
		/// <param name="anyOf">A Unicode character array containing one or more characters to seek. </param>
		/// <param name="startIndex">The search starting position. The search proceeds from <paramref name="startIndex" /> toward the beginning of this instance.</param>
		/// <param name="count">The number of character positions to examine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="anyOf" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="count" /> or <paramref name="startIndex" /> is negative.-or- The current instance does not equal <see cref="F:System.String.Empty" />, and <paramref name="startIndex" /> minus <paramref name="count" /> + 1 is less than zero. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013ED RID: 5101 RVA: 0x00051014 File Offset: 0x0004F214
		public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
		{
			if (anyOf == null)
			{
				throw new ArgumentNullException();
			}
			if (this.m_stringLength == 0)
			{
				return -1;
			}
			if (startIndex < 0 || startIndex >= this.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "< 0 || > this.Length");
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentOutOfRangeException("count", "< 0 || > this.Length");
			}
			if (startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex - count + 1 < 0");
			}
			if (this.m_stringLength == 0)
			{
				return -1;
			}
			return this.LastIndexOfAnyUnchecked(anyOf, startIndex, count);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00051094 File Offset: 0x0004F294
		private unsafe int LastIndexOfAnyUnchecked(char[] anyOf, int startIndex, int count)
		{
			if (anyOf.Length == 1)
			{
				return this.LastIndexOfUnchecked(anyOf[0], startIndex, count);
			}
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3;
				if (anyOf == null || anyOf.Length == 0)
				{
					ptr3 = null;
				}
				else
				{
					ptr3 = &anyOf[0];
				}
				char* ptr4 = ptr2 + startIndex;
				char* ptr5 = ptr4 - count;
				char* ptr6 = ptr3 + anyOf.Length;
				while (ptr4 != ptr5)
				{
					for (char* ptr7 = ptr3; ptr7 != ptr6; ptr7++)
					{
						if (*ptr7 == *ptr4)
						{
							return (int)((long)(ptr4 - ptr2));
						}
					}
					ptr4--;
				}
				return -1;
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00051124 File Offset: 0x0004F324
		internal static int nativeCompareOrdinalEx(string strA, int indexA, string strB, int indexB, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count cannot be less than zero."));
			}
			if (indexA < 0 || indexA > strA.Length)
			{
				throw new ArgumentOutOfRangeException("indexA", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (indexB < 0 || indexB > strB.Length)
			{
				throw new ArgumentOutOfRangeException("indexB", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return string.CompareOrdinalUnchecked(strA, indexA, count, strB, indexB, count);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0005119C File Offset: 0x0004F39C
		private unsafe string ReplaceInternal(char oldChar, char newChar)
		{
			if (this.m_stringLength == 0 || oldChar == newChar)
			{
				return this;
			}
			int num = this.IndexOfUnchecked(oldChar, 0, this.m_stringLength);
			if (num == -1)
			{
				return this;
			}
			if (num < 4)
			{
				num = 0;
			}
			string text = string.FastAllocateString(this.m_stringLength);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (char* ptr2 = &this.m_firstChar)
				{
					char* ptr3 = ptr2;
					if (num != 0)
					{
						string.CharCopy(ptr, ptr3, num);
					}
					char* ptr4 = ptr + this.m_stringLength;
					char* ptr5 = ptr + num;
					char* ptr6 = ptr3 + num;
					while (ptr5 != ptr4)
					{
						if (*ptr6 == oldChar)
						{
							*ptr5 = newChar;
						}
						else
						{
							*ptr5 = *ptr6;
						}
						ptr6++;
						ptr5++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00051252 File Offset: 0x0004F452
		internal string ReplaceInternal(string oldValue, string newValue)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("oldValue is the empty string.");
			}
			if (this.Length == 0)
			{
				return this;
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			return this.ReplaceUnchecked(oldValue, newValue);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00051294 File Offset: 0x0004F494
		private unsafe string ReplaceUnchecked(string oldValue, string newValue)
		{
			if (oldValue.m_stringLength > this.m_stringLength)
			{
				return this;
			}
			if (oldValue.m_stringLength == 1 && newValue.m_stringLength == 1)
			{
				return this.Replace(oldValue[0], newValue[0]);
			}
			int* ptr = stackalloc int[(UIntPtr)800];
			fixed (char* ptr2 = &this.m_firstChar)
			{
				char* ptr3 = ptr2;
				char* ptr4 = newValue;
				if (ptr4 != null)
				{
					ptr4 += RuntimeHelpers.OffsetToStringData / 2;
				}
				int i = 0;
				int num = 0;
				while (i < this.m_stringLength)
				{
					int num2 = this.IndexOfUnchecked(oldValue, i, this.m_stringLength - i);
					if (num2 < 0)
					{
						break;
					}
					if (num >= 200)
					{
						return this.ReplaceFallback(oldValue, newValue, 200);
					}
					ptr[(IntPtr)(num++) * 4] = num2;
					i = num2 + oldValue.m_stringLength;
				}
				if (num == 0)
				{
					return this;
				}
				int num3 = 0;
				try
				{
					num3 = checked(this.m_stringLength + (newValue.m_stringLength - oldValue.m_stringLength) * num);
				}
				catch (OverflowException)
				{
					throw new OutOfMemoryException();
				}
				string text = string.FastAllocateString(num3);
				int num4 = 0;
				int num5 = 0;
				fixed (string text2 = text)
				{
					char* ptr5 = text2;
					if (ptr5 != null)
					{
						ptr5 += RuntimeHelpers.OffsetToStringData / 2;
					}
					for (int j = 0; j < num; j++)
					{
						int num6 = ptr[j] - num5;
						string.CharCopy(ptr5 + num4, ptr3 + num5, num6);
						num4 += num6;
						num5 = ptr[j] + oldValue.m_stringLength;
						string.CharCopy(ptr5 + num4, ptr4, newValue.m_stringLength);
						num4 += newValue.m_stringLength;
					}
					string.CharCopy(ptr5 + num4, ptr3 + num5, this.m_stringLength - num5);
				}
				return text;
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0005144C File Offset: 0x0004F64C
		private string ReplaceFallback(string oldValue, string newValue, int testedCount)
		{
			StringBuilder stringBuilder = new StringBuilder(this.m_stringLength + (newValue.m_stringLength - oldValue.m_stringLength) * testedCount);
			int num;
			for (int i = 0; i < this.m_stringLength; i = num + oldValue.m_stringLength)
			{
				num = this.IndexOfUnchecked(oldValue, i, this.m_stringLength - i);
				if (num < 0)
				{
					stringBuilder.Append(this.InternalSubString(i, this.m_stringLength - i));
					break;
				}
				stringBuilder.Append(this.InternalSubString(i, num - i));
				stringBuilder.Append(newValue);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000514D8 File Offset: 0x0004F6D8
		private unsafe string PadHelper(int totalWidth, char paddingChar, bool isRightPadded)
		{
			if (totalWidth < 0)
			{
				throw new ArgumentOutOfRangeException("totalWidth", "Non-negative number required");
			}
			if (totalWidth <= this.m_stringLength)
			{
				return this;
			}
			string text = string.FastAllocateString(totalWidth);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (char* ptr2 = &this.m_firstChar)
				{
					char* ptr3 = ptr2;
					if (isRightPadded)
					{
						string.CharCopy(ptr, ptr3, this.m_stringLength);
						char* ptr4 = ptr + totalWidth;
						char* ptr5 = ptr + this.m_stringLength;
						while (ptr5 < ptr4)
						{
							*(ptr5++) = paddingChar;
						}
					}
					else
					{
						char* ptr6 = ptr;
						char* ptr7 = ptr6 + totalWidth - this.m_stringLength;
						while (ptr6 < ptr7)
						{
							*(ptr6++) = paddingChar;
						}
						string.CharCopy(ptr6, ptr3, this.m_stringLength);
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0005159B File Offset: 0x0004F79B
		internal bool StartsWithOrdinalUnchecked(string value)
		{
			return this.m_stringLength >= value.m_stringLength && string.CompareOrdinalUnchecked(this, 0, value.m_stringLength, value, 0, value.m_stringLength) == 0;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000515C8 File Offset: 0x0004F7C8
		internal unsafe bool IsAscii()
		{
			fixed (char* ptr = &this.m_firstChar)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + this.m_stringLength;
				for (char* ptr4 = ptr2; ptr4 != ptr3; ptr4++)
				{
					if (*ptr4 >= '\u0080')
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal bool IsFastSort()
		{
			return false;
		}

		// Token: 0x060013F8 RID: 5112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalIsInterned(string str);

		// Token: 0x060013F9 RID: 5113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalIntern(string str);

		// Token: 0x060013FA RID: 5114 RVA: 0x00051604 File Offset: 0x0004F804
		internal unsafe static void CharCopy(char* dest, char* src, int count)
		{
			if (((dest | src) & 3) != 0)
			{
				if ((dest & 2) != 0 && (src & 2) != 0 && count > 0)
				{
					*dest = (char)(*(short*)src);
					dest++;
					src++;
					count--;
				}
				if (((dest | src) & 2) != 0)
				{
					Buffer.memcpy2((byte*)dest, (byte*)src, count * 2);
					return;
				}
			}
			Buffer.memcpy4((byte*)dest, (byte*)src, count * 2);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0005165C File Offset: 0x0004F85C
		private unsafe static void memset(byte* dest, int val, int len)
		{
			if (len < 8)
			{
				while (len != 0)
				{
					*dest = (byte)val;
					dest++;
					len--;
				}
				return;
			}
			if (val != 0)
			{
				val |= val << 8;
				val |= val << 16;
			}
			int num = dest & 3;
			if (num != 0)
			{
				num = 4 - num;
				len -= num;
				do
				{
					*dest = (byte)val;
					dest++;
					num--;
				}
				while (num != 0);
			}
			while (len >= 16)
			{
				*(int*)dest = val;
				*(int*)(dest + 4) = val;
				*(int*)(dest + (IntPtr)2 * 4) = val;
				*(int*)(dest + (IntPtr)3 * 4) = val;
				dest += 16;
				len -= 16;
			}
			while (len >= 4)
			{
				*(int*)dest = val;
				dest += 4;
				len -= 4;
			}
			while (len > 0)
			{
				*dest = (byte)val;
				dest++;
				len--;
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00051706 File Offset: 0x0004F906
		private unsafe static void memcpy(byte* dest, byte* src, int size)
		{
			Buffer.Memcpy(dest, src, size);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00051710 File Offset: 0x0004F910
		internal unsafe static void bzero(byte* dest, int len)
		{
			string.memset(dest, 0, len);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0005171A File Offset: 0x0004F91A
		internal unsafe static void bzero_aligned_1(byte* dest, int len)
		{
			*dest = 0;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0005171F File Offset: 0x0004F91F
		internal unsafe static void bzero_aligned_2(byte* dest, int len)
		{
			*(short*)dest = 0;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00051724 File Offset: 0x0004F924
		internal unsafe static void bzero_aligned_4(byte* dest, int len)
		{
			*(int*)dest = 0;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00051729 File Offset: 0x0004F929
		internal unsafe static void bzero_aligned_8(byte* dest, int len)
		{
			*(long*)dest = 0L;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0005172F File Offset: 0x0004F92F
		internal unsafe static void memcpy_aligned_1(byte* dest, byte* src, int size)
		{
			*dest = *src;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00051735 File Offset: 0x0004F935
		internal unsafe static void memcpy_aligned_2(byte* dest, byte* src, int size)
		{
			*(short*)dest = *(short*)src;
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0005173B File Offset: 0x0004F93B
		internal unsafe static void memcpy_aligned_4(byte* dest, byte* src, int size)
		{
			*(int*)dest = *(int*)src;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00051741 File Offset: 0x0004F941
		internal unsafe static void memcpy_aligned_8(byte* dest, byte* src, int size)
		{
			*(long*)dest = *(long*)src;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00051748 File Offset: 0x0004F948
		private unsafe string CreateString(sbyte* value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			byte* ptr = (byte*)value;
			int num = 0;
			try
			{
				while (*(ptr++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return this.CreateString(value, 0, num, null);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000517A0 File Offset: 0x0004F9A0
		private unsafe string CreateString(sbyte* value, int startIndex, int length)
		{
			return this.CreateString(value, startIndex, length, null);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000517AC File Offset: 0x0004F9AC
		private unsafe string CreateString(char* value)
		{
			return this.CtorCharPtr(value);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000517B5 File Offset: 0x0004F9B5
		private unsafe string CreateString(char* value, int startIndex, int length)
		{
			return this.CtorCharPtrStartLength(value, startIndex, length);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000517C0 File Offset: 0x0004F9C0
		private string CreateString(char[] val, int startIndex, int length)
		{
			return this.CtorCharArrayStartLength(val, startIndex, length);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x000517CB File Offset: 0x0004F9CB
		private string CreateString(char[] val)
		{
			return this.CtorCharArray(val);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x000517D4 File Offset: 0x0004F9D4
		private unsafe string CreateString(char c, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(count);
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + count;
				while (ptr2 < ptr3)
				{
					*ptr2 = c;
					ptr2++;
				}
			}
			return text;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0005182C File Offset: 0x0004FA2C
		private unsafe string CreateString(sbyte* value, int startIndex, int length, Encoding enc)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Non-negative number required.");
			}
			if (value + startIndex < value)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Value, startIndex and length do not refer to a valid string.");
			}
			if (enc == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (length == 0)
				{
					return string.Empty;
				}
				enc = Encoding.Default;
			}
			byte[] array = new byte[length];
			if (length != 0)
			{
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				try
				{
					if (value == null)
					{
						throw new ArgumentOutOfRangeException("ptr", "Value, startIndex and length do not refer to a valid string.");
					}
					string.memcpy(ptr, (byte*)(value + startIndex), length);
				}
				catch (NullReferenceException)
				{
					throw new ArgumentOutOfRangeException("ptr", "Value, startIndex and length do not refer to a valid string.");
				}
				array2 = null;
			}
			return enc.GetString(array);
		}

		// Token: 0x04000ADB RID: 2779
		[NonSerialized]
		private int m_stringLength;

		// Token: 0x04000ADC RID: 2780
		[NonSerialized]
		private char m_firstChar;

		// Token: 0x04000ADD RID: 2781
		private const int TrimHead = 0;

		// Token: 0x04000ADE RID: 2782
		private const int TrimTail = 1;

		// Token: 0x04000ADF RID: 2783
		private const int TrimBoth = 2;

		/// <summary>Represents the empty string. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000AE0 RID: 2784
		public static readonly string Empty;

		// Token: 0x04000AE1 RID: 2785
		private const int charPtrAlignConst = 1;

		// Token: 0x04000AE2 RID: 2786
		private const int alignConst = 3;
	}
}
