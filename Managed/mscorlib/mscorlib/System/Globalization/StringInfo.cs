using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Provides functionality to split a string into text elements and to iterate through those text elements.</summary>
	// Token: 0x02000424 RID: 1060
	[ComVisible(true)]
	[Serializable]
	public class StringInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class. </summary>
		// Token: 0x060032C1 RID: 12993 RVA: 0x000B6051 File Offset: 0x000B4251
		public StringInfo()
			: this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class to a specified string.</summary>
		/// <param name="value">A string to initialize this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060032C2 RID: 12994 RVA: 0x000B605E File Offset: 0x000B425E
		public StringInfo(string value)
		{
			this.String = value;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000B606D File Offset: 0x000B426D
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_str = string.Empty;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000B607A File Offset: 0x000B427A
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_str.Length == 0)
			{
				this.m_indexes = null;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Globalization.StringInfo" /> object is equal to a specified object.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.StringInfo" /> object and its <see cref="P:System.Globalization.StringInfo.String" /> property equals the <see cref="P:System.Globalization.StringInfo.String" /> property of this <see cref="T:System.Globalization.StringInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object.</param>
		// Token: 0x060032C5 RID: 12997 RVA: 0x000B6090 File Offset: 0x000B4290
		[ComVisible(false)]
		public override bool Equals(object value)
		{
			StringInfo stringInfo = value as StringInfo;
			return stringInfo != null && this.m_str.Equals(stringInfo.m_str);
		}

		/// <summary>Calculates a hash code for the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code based on the string value of this <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		// Token: 0x060032C6 RID: 12998 RVA: 0x000B60BA File Offset: 0x000B42BA
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.m_str.GetHashCode();
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000B60C7 File Offset: 0x000B42C7
		private int[] Indexes
		{
			get
			{
				if (this.m_indexes == null && 0 < this.String.Length)
				{
					this.m_indexes = StringInfo.ParseCombiningCharacters(this.String);
				}
				return this.m_indexes;
			}
		}

		/// <summary>Gets or sets the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000B60F6 File Offset: 0x000B42F6
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x000B60FE File Offset: 0x000B42FE
		public string String
		{
			get
			{
				return this.m_str;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("String", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.m_str = value;
				this.m_indexes = null;
			}
		}

		/// <summary>Gets the number of text elements in the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>The number of base characters, surrogate pairs, and combining character sequences in this <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000B6126 File Offset: 0x000B4326
		public int LengthInTextElements
		{
			get
			{
				if (this.Indexes == null)
				{
					return 0;
				}
				return this.Indexes.Length;
			}
		}

		/// <summary>Retrieves a substring of text elements from the current <see cref="T:System.Globalization.StringInfo" /> object starting from a specified text element and continuing through the last text element.</summary>
		/// <returns>A substring of text elements in this <see cref="T:System.Globalization.StringInfo" /> object, starting from the text element index specified by the <paramref name="startingTextElement" /> parameter and continuing through the last text element in this object.</returns>
		/// <param name="startingTextElement">The zero-based index of a text element in this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startingTextElement" /> is less than zero.-or-The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object is the empty string ("").</exception>
		// Token: 0x060032CB RID: 13003 RVA: 0x000B613C File Offset: 0x000B433C
		public string SubstringByTextElements(int startingTextElement)
		{
			if (this.Indexes != null)
			{
				return this.SubstringByTextElements(startingTextElement, this.Indexes.Length - startingTextElement);
			}
			if (startingTextElement < 0)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Positive number required."));
			}
			throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Specified argument was out of the range of valid values."));
		}

		/// <summary>Retrieves a substring of text elements from the current <see cref="T:System.Globalization.StringInfo" /> object starting from a specified text element and continuing through the specified number of text elements.</summary>
		/// <returns>A substring of text elements in this <see cref="T:System.Globalization.StringInfo" /> object. The substring consists of the number of text elements specified by the <paramref name="lengthInTextElements" /> parameter and starts from the text element index specified by the <paramref name="startingTextElement" /> parameter.</returns>
		/// <param name="startingTextElement">The zero-based index of a text element in this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <param name="lengthInTextElements">The number of text elements to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startingTextElement" /> is less than zero.-or-<paramref name="startingTextElement" /> is greater than or equal to the length of the string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object.-or-<paramref name="lengthInTextElements" /> is less than zero.-or-The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object is the empty string ("").-or-<paramref name="startingTextElement" /> + <paramref name="lengthInTextElements" /> specify an index that is greater than the number of text elements in this <see cref="T:System.Globalization.StringInfo" /> object.</exception>
		// Token: 0x060032CC RID: 13004 RVA: 0x000B6190 File Offset: 0x000B4390
		public string SubstringByTextElements(int startingTextElement, int lengthInTextElements)
		{
			if (startingTextElement < 0)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Positive number required."));
			}
			if (this.String.Length == 0 || startingTextElement >= this.Indexes.Length)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Specified argument was out of the range of valid values."));
			}
			if (lengthInTextElements < 0)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("Positive number required."));
			}
			if (startingTextElement > this.Indexes.Length - lengthInTextElements)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("Specified argument was out of the range of valid values."));
			}
			int num = this.Indexes[startingTextElement];
			if (startingTextElement + lengthInTextElements == this.Indexes.Length)
			{
				return this.String.Substring(num);
			}
			return this.String.Substring(num, this.Indexes[lengthInTextElements + startingTextElement] - num);
		}

		/// <summary>Gets the first text element in a specified string.</summary>
		/// <returns>A string containing the first text element in the specified string.</returns>
		/// <param name="str">The string from which to get the text element. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x060032CD RID: 13005 RVA: 0x000B6259 File Offset: 0x000B4459
		public static string GetNextTextElement(string str)
		{
			return StringInfo.GetNextTextElement(str, 0);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000B6264 File Offset: 0x000B4464
		internal static int GetCurrentTextElementLen(string str, int index, int len, ref UnicodeCategory ucCurrent, ref int currentCharCount)
		{
			if (index + currentCharCount == len)
			{
				return currentCharCount;
			}
			int num;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index + currentCharCount, out num);
			if (CharUnicodeInfo.IsCombiningCategory(unicodeCategory) && !CharUnicodeInfo.IsCombiningCategory(ucCurrent) && ucCurrent != UnicodeCategory.Format && ucCurrent != UnicodeCategory.Control && ucCurrent != UnicodeCategory.OtherNotAssigned && ucCurrent != UnicodeCategory.Surrogate)
			{
				int num2 = index;
				for (index += currentCharCount + num; index < len; index += num)
				{
					unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
					if (!CharUnicodeInfo.IsCombiningCategory(unicodeCategory))
					{
						ucCurrent = unicodeCategory;
						currentCharCount = num;
						break;
					}
				}
				return index - num2;
			}
			int num3 = currentCharCount;
			ucCurrent = unicodeCategory;
			currentCharCount = num;
			return num3;
		}

		/// <summary>Gets the text element at the specified index of the specified string.</summary>
		/// <returns>A string containing the text element at the specified index of the specified string.</returns>
		/// <param name="str">The string from which to get the text element. </param>
		/// <param name="index">The zero-based index at which the text element starts. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for <paramref name="str" />. </exception>
		// Token: 0x060032CF RID: 13007 RVA: 0x000B62F4 File Offset: 0x000B44F4
		public static string GetNextTextElement(string str, int index)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (index >= 0 && index < length)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
				return str.Substring(index, StringInfo.GetCurrentTextElementLen(str, index, length, ref unicodeCategory, ref num));
			}
			if (index == length)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}

		/// <summary>Returns an enumerator that iterates through the text elements of the entire string.</summary>
		/// <returns>A <see cref="T:System.Globalization.TextElementEnumerator" /> for the entire string.</returns>
		/// <param name="str">The string to iterate through. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x060032D0 RID: 13008 RVA: 0x000B635A File Offset: 0x000B455A
		public static TextElementEnumerator GetTextElementEnumerator(string str)
		{
			return StringInfo.GetTextElementEnumerator(str, 0);
		}

		/// <summary>Returns an enumerator that iterates through the text elements of the string, starting at the specified index.</summary>
		/// <returns>A <see cref="T:System.Globalization.TextElementEnumerator" /> for the string starting at <paramref name="index" />.</returns>
		/// <param name="str">The string to iterate through. </param>
		/// <param name="index">The zero-based index at which to start iterating. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for <paramref name="str" />. </exception>
		// Token: 0x060032D1 RID: 13009 RVA: 0x000B6364 File Offset: 0x000B4564
		public static TextElementEnumerator GetTextElementEnumerator(string str, int index)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (index < 0 || index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return new TextElementEnumerator(str, index, length);
		}

		/// <summary>Returns the indexes of each base character, high surrogate, or control character within the specified string.</summary>
		/// <returns>An array of integers that contains the zero-based indexes of each base character, high surrogate, or control character within the specified string.</returns>
		/// <param name="str">The string to search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x060032D2 RID: 13010 RVA: 0x000B63AC File Offset: 0x000B45AC
		public static int[] ParseCombiningCharacters(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			int[] array = new int[length];
			if (length == 0)
			{
				return array;
			}
			int num = 0;
			int i = 0;
			int num2;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, 0, out num2);
			while (i < length)
			{
				array[num++] = i;
				i += StringInfo.GetCurrentTextElementLen(str, i, length, ref unicodeCategory, ref num2);
			}
			if (num < length)
			{
				int[] array2 = new int[num];
				Array.Copy(array, array2, num);
				return array2;
			}
			return array;
		}

		// Token: 0x04001AEE RID: 6894
		[OptionalField(VersionAdded = 2)]
		private string m_str;

		// Token: 0x04001AEF RID: 6895
		[NonSerialized]
		private int[] m_indexes;
	}
}
