using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Unity;

namespace System.Globalization
{
	/// <summary>Defines text properties and behaviors, such as casing, that are specific to a writing system. </summary>
	// Token: 0x02000428 RID: 1064
	[ComVisible(true)]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000B6A0D File Offset: 0x000B4C0D
		internal static TextInfo Invariant
		{
			get
			{
				if (TextInfo.s_Invariant == null)
				{
					TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
				}
				return TextInfo.s_Invariant;
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000B6A30 File Offset: 0x000B4C30
		internal TextInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_cultureName = this.m_cultureData.CultureName;
			this.m_textInfoName = this.m_cultureData.STEXTINFO;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000B6A61 File Offset: 0x000B4C61
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_cultureData = null;
			this.m_cultureName = null;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000B6A74 File Offset: 0x000B4C74
		private void OnDeserialized()
		{
			if (this.m_cultureData == null)
			{
				if (this.m_cultureName == null)
				{
					if (this.customCultureName != null)
					{
						this.m_cultureName = this.customCultureName;
					}
					else if (this.m_win32LangID == 0)
					{
						this.m_cultureName = "ar-SA";
					}
					else
					{
						this.m_cultureName = CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName;
					}
				}
				this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
				this.m_textInfoName = this.m_cultureData.STEXTINFO;
			}
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000B6AFE File Offset: 0x000B4CFE
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000B6B06 File Offset: 0x000B4D06
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_useUserOverride = false;
			this.customCultureName = this.m_cultureName;
			this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000B6B31 File Offset: 0x000B4D31
		internal static int GetHashCodeOrdinalIgnoreCase(string s)
		{
			return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000B6B3C File Offset: 0x000B4D3C
		internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
		{
			return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000B6B4B File Offset: 0x000B4D4B
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCase(string str1, string str2)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(str1, 0, str2, 0, str1.Length, str2.Length);
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000B6B62 File Offset: 0x000B4D62
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000B6B74 File Offset: 0x000B4D74
		internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (source.Length == 0 && value.Length == 0)
			{
				return 0;
			}
			int num = startIndex + count - value.Length;
			while (startIndex <= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex++;
			}
			return -1;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000B6BC4 File Offset: 0x000B4DC4
		internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (value.Length == 0)
			{
				return startIndex;
			}
			int num = startIndex - count + 1;
			if (value.Length > 0)
			{
				startIndex -= value.Length - 1;
			}
			while (startIndex >= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex--;
			}
			return -1;
		}

		/// <summary>Gets the American National Standards Institute (ANSI) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The ANSI code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000B6C1B File Offset: 0x000B4E1B
		public virtual int ANSICodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTANSICODEPAGE;
			}
		}

		/// <summary>Gets the original equipment manufacturer (OEM) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The OEM code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000B6C28 File Offset: 0x000B4E28
		public virtual int OEMCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTOEMCODEPAGE;
			}
		}

		/// <summary>Gets the Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000B6C35 File Offset: 0x000B4E35
		public virtual int MacCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTMACCODEPAGE;
			}
		}

		/// <summary>Gets the Extended Binary Coded Decimal Interchange Code (EBCDIC) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The EBCDIC code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000B6C42 File Offset: 0x000B4E42
		public virtual int EBCDICCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
			}
		}

		/// <summary>Gets the culture identifier for the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A number that identifies the culture from which the current <see cref="T:System.Globalization.TextInfo" /> object was created.</returns>
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000B6C4F File Offset: 0x000B4E4F
		[ComVisible(false)]
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
			}
		}

		/// <summary>Gets the name of the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The name of a culture. </returns>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000B6C61 File Offset: 0x000B4E61
		[ComVisible(false)]
		public string CultureName
		{
			get
			{
				return this.m_textInfoName;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.TextInfo" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000B6C69 File Offset: 0x000B4E69
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.TextInfo" /> object.</returns>
		// Token: 0x0600331D RID: 13085 RVA: 0x000B6C71 File Offset: 0x000B4E71
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> object specified by the <paramref name="textInfo" /> parameter, if <paramref name="textInfo" /> is read-only.-or-A read-only memberwise clone of the <see cref="T:System.Globalization.TextInfo" /> object specified by <paramref name="textInfo" />, if <paramref name="textInfo" /> is not read-only.</returns>
		/// <param name="textInfo">A <see cref="T:System.Globalization.TextInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textInfo" /> is null.</exception>
		// Token: 0x0600331E RID: 13086 RVA: 0x000B6C85 File Offset: 0x000B4E85
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000B6CB1 File Offset: 0x000B4EB1
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000B6CCB File Offset: 0x000B4ECB
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		/// <summary>Gets or sets the string that separates items in a list.</summary>
		/// <returns>The string that separates items in a list.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</exception>
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000B6CD4 File Offset: 0x000B4ED4
		// (set) Token: 0x06003322 RID: 13090 RVA: 0x000B6CF5 File Offset: 0x000B4EF5
		public virtual string ListSeparator
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = this.m_cultureData.SLIST;
				}
				return this.m_listSeparator;
			}
			[ComVisible(false)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.m_listSeparator = value;
			}
		}

		/// <summary>Converts the specified character to lowercase.</summary>
		/// <returns>The specified character converted to lowercase.</returns>
		/// <param name="c">The character to convert to lowercase. </param>
		// Token: 0x06003323 RID: 13091 RVA: 0x000B6D1C File Offset: 0x000B4F1C
		[SecuritySafeCritical]
		public virtual char ToLower(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return this.ToLowerInternal(c);
		}

		/// <summary>Converts the specified string to lowercase.</summary>
		/// <returns>The specified string converted to lowercase.</returns>
		/// <param name="str">The string to convert to lowercase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06003324 RID: 13092 RVA: 0x000B6D3C File Offset: 0x000B4F3C
		[SecuritySafeCritical]
		public virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToLowerInternal(str);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000B6D53 File Offset: 0x000B4F53
		private static char ToLowerAsciiInvariant(char c)
		{
			if ('A' <= c && c <= 'Z')
			{
				c |= ' ';
			}
			return c;
		}

		/// <summary>Converts the specified character to uppercase.</summary>
		/// <returns>The specified character converted to uppercase.</returns>
		/// <param name="c">The character to convert to uppercase. </param>
		// Token: 0x06003326 RID: 13094 RVA: 0x000B6D67 File Offset: 0x000B4F67
		[SecuritySafeCritical]
		public virtual char ToUpper(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return this.ToUpperInternal(c);
		}

		/// <summary>Converts the specified string to uppercase.</summary>
		/// <returns>The specified string converted to uppercase.</returns>
		/// <param name="str">The string to convert to uppercase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06003327 RID: 13095 RVA: 0x000B6D87 File Offset: 0x000B4F87
		[SecuritySafeCritical]
		public virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToUpperInternal(str);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000B6D9E File Offset: 0x000B4F9E
		private static char ToUpperAsciiInvariant(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c = (char)((int)c & -33);
			}
			return c;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000B6DB2 File Offset: 0x000B4FB2
		private static bool IsAscii(char c)
		{
			return c < '\u0080';
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000B6DBC File Offset: 0x000B4FBC
		private bool IsAsciiCasingSameAsInvariant
		{
			get
			{
				if (this.m_IsAsciiCasingSameAsInvariant == null)
				{
					this.m_IsAsciiCasingSameAsInvariant = new bool?(!(this.m_cultureData.SISO639LANGNAME == "az") && !(this.m_cultureData.SISO639LANGNAME == "tr"));
				}
				return this.m_IsAsciiCasingSameAsInvariant.Value;
			}
		}

		/// <summary>Determines whether the specified object represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Globalization.TextInfo" />. </param>
		// Token: 0x0600332B RID: 13099 RVA: 0x000B6E20 File Offset: 0x000B5020
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.TextInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x0600332C RID: 13100 RVA: 0x000B6E4A File Offset: 0x000B504A
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x0600332D RID: 13101 RVA: 0x000B6E57 File Offset: 0x000B5057
		public override string ToString()
		{
			return "TextInfo - " + this.m_cultureData.CultureName;
		}

		/// <summary>Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms)..</summary>
		/// <returns>The specified string converted to title case.</returns>
		/// <param name="str">The string to convert to title case. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x0600332E RID: 13102 RVA: 0x000B6E70 File Offset: 0x000B5070
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
				if (char.CheckLetter(unicodeCategory))
				{
					i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					int num2 = i;
					bool flag = unicodeCategory == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategory))
						{
							if (unicodeCategory == UnicodeCategory.LowercaseLetter)
							{
								flag = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategory))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000B6FBD File Offset: 0x000B51BD
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000B6FFC File Offset: 0x000B51FC
		private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000B70D6 File Offset: 0x000B52D6
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & (1 << (int)category)) != 0;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000B70E7 File Offset: 0x000B52E7
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object represents a writing system where text flows from right to left.</summary>
		/// <returns>true if text flows from right to left; otherwise, false.</returns>
		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000B70FE File Offset: 0x000B52FE
		[ComVisible(false)]
		public bool IsRightToLeft
		{
			get
			{
				return this.m_cultureData.IsRightToLeft;
			}
		}

		/// <summary>Raises the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06003334 RID: 13108 RVA: 0x000B6AFE File Offset: 0x000B4CFE
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000B710B File Offset: 0x000B530B
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str)
		{
			return this.GetCaseInsensitiveHashCode(str, false, 0L);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000B7117 File Offset: 0x000B5317
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (this != TextInfo.s_Invariant)
			{
				return StringComparer.CurrentCultureIgnoreCase.GetHashCode(str);
			}
			return this.GetInvariantCaseInsensitiveHashCode(str);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000B7144 File Offset: 0x000B5344
		private unsafe int GetInvariantCaseInsensitiveHashCode(string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr;
			char* ptr3 = ptr2 + str.Length - 1;
			int num = 0;
			while (ptr2 < ptr3)
			{
				num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
				num = (num << 5) - num + (int)char.ToUpperInvariant(ptr2[1]);
				ptr2 += 2;
			}
			ptr3++;
			if (ptr2 < ptr3)
			{
				num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
			}
			return num;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000B71C0 File Offset: 0x000B53C0
		private unsafe string ToUpperInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToUpper(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000B7244 File Offset: 0x000B5444
		private unsafe string ToLowerInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToLower(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000B72C8 File Offset: 0x000B54C8
		private char ToUpperInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c > 'ſ')
					{
						if (c <= 'ǈ')
						{
							if (c != 'ǅ' && c != 'ǈ')
							{
								goto IL_015A;
							}
						}
						else if (c != 'ǋ' && c != 'ǲ')
						{
							goto IL_015A;
						}
						return c - '\u0001';
					}
					if (c == 'µ')
					{
						return 'Μ';
					}
					if (c == 'ı')
					{
						return 'I';
					}
					if (c == 'ſ')
					{
						return 'S';
					}
				}
				else if (c <= 'ϰ')
				{
					if (c <= 'ς')
					{
						if (c == '\u0345')
						{
							return 'Ι';
						}
						if (c == 'ς')
						{
							return 'Σ';
						}
					}
					else
					{
						switch (c)
						{
						case 'ϐ':
							return 'Β';
						case 'ϑ':
							return 'Θ';
						case 'ϒ':
						case 'ϓ':
						case 'ϔ':
							break;
						case 'ϕ':
							return 'Φ';
						case 'ϖ':
							return 'Π';
						default:
							if (c == 'ϰ')
							{
								return 'Κ';
							}
							break;
						}
					}
				}
				else if (c <= 'ϵ')
				{
					if (c == 'ϱ')
					{
						return 'Ρ';
					}
					if (c == 'ϵ')
					{
						return 'Ε';
					}
				}
				else
				{
					if (c == 'ẛ')
					{
						return 'Ṡ';
					}
					if (c == 'ι')
					{
						return 'Ι';
					}
				}
				IL_015A:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'i')
					{
						return 'İ';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToUpperAsciiInvariant(c);
					}
				}
			}
			if (c >= 'à' && c <= 'ֆ')
			{
				return TextInfoToUpperData.range_00e0_0586[(int)(c - 'à')];
			}
			if (c >= 'ḁ' && c <= 'ῳ')
			{
				return TextInfoToUpperData.range_1e01_1ff3[(int)(c - 'ḁ')];
			}
			if (c >= 'ⅰ' && c <= 'ↄ')
			{
				return TextInfoToUpperData.range_2170_2184[(int)(c - 'ⅰ')];
			}
			if (c >= 'ⓐ' && c <= 'ⓩ')
			{
				return TextInfoToUpperData.range_24d0_24e9[(int)(c - 'ⓐ')];
			}
			if (c >= 'ⰰ' && c <= 'ⳣ')
			{
				return TextInfoToUpperData.range_2c30_2ce3[(int)(c - 'ⰰ')];
			}
			if (c >= 'ⴀ' && c <= 'ⴥ')
			{
				return TextInfoToUpperData.range_2d00_2d25[(int)(c - 'ⴀ')];
			}
			if (c >= 'ꙁ' && c <= 'ꚗ')
			{
				return TextInfoToUpperData.range_a641_a697[(int)(c - 'ꙁ')];
			}
			if (c >= 'ꜣ' && c <= 'ꞌ')
			{
				return TextInfoToUpperData.range_a723_a78c[(int)(c - 'ꜣ')];
			}
			if ('ａ' <= c && c <= 'ｚ')
			{
				return c - ' ';
			}
			if (c == 'ᵹ')
			{
				return 'Ᵹ';
			}
			if (c == 'ᵽ')
			{
				return 'Ᵽ';
			}
			if (c != 'ⅎ')
			{
				return c;
			}
			return 'Ⅎ';
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000B7584 File Offset: 0x000B5784
		private char ToLowerInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c <= 'ǅ')
					{
						if (c == 'İ')
						{
							return 'i';
						}
						if (c != 'ǅ')
						{
							goto IL_00D3;
						}
					}
					else if (c != 'ǈ' && c != 'ǋ' && c != 'ǲ')
					{
						goto IL_00D3;
					}
					return c + '\u0001';
				}
				if (c <= 'ẞ')
				{
					switch (c)
					{
					case 'ϒ':
						return 'υ';
					case 'ϓ':
						return 'ύ';
					case 'ϔ':
						return 'ϋ';
					default:
						if (c == 'ϴ')
						{
							return 'θ';
						}
						if (c == 'ẞ')
						{
							return 'ß';
						}
						break;
					}
				}
				else
				{
					if (c == 'Ω')
					{
						return 'ω';
					}
					if (c == 'K')
					{
						return 'k';
					}
					if (c == 'Å')
					{
						return 'å';
					}
				}
				IL_00D3:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'I')
					{
						return 'ı';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToLowerAsciiInvariant(c);
					}
				}
			}
			if (c >= 'À' && c <= 'Ֆ')
			{
				return TextInfoToLowerData.range_00c0_0556[(int)(c - 'À')];
			}
			if (c >= 'Ⴀ' && c <= 'Ⴥ')
			{
				return TextInfoToLowerData.range_10a0_10c5[(int)(c - 'Ⴀ')];
			}
			if (c >= 'Ḁ' && c <= 'ῼ')
			{
				return TextInfoToLowerData.range_1e00_1ffc[(int)(c - 'Ḁ')];
			}
			if (c >= 'Ⅰ' && c <= 'Ⅿ')
			{
				return TextInfoToLowerData.range_2160_216f[(int)(c - 'Ⅰ')];
			}
			if (c >= 'Ⓐ' && c <= 'Ⓩ')
			{
				return TextInfoToLowerData.range_24b6_24cf[(int)(c - 'Ⓐ')];
			}
			if (c >= 'Ⰰ' && c <= 'Ⱞ')
			{
				return TextInfoToLowerData.range_2c00_2c2e[(int)(c - 'Ⰰ')];
			}
			if (c >= 'Ⱡ' && c <= 'Ⳣ')
			{
				return TextInfoToLowerData.range_2c60_2ce2[(int)(c - 'Ⱡ')];
			}
			if (c >= 'Ꙁ' && c <= 'Ꚗ')
			{
				return TextInfoToLowerData.range_a640_a696[(int)(c - 'Ꙁ')];
			}
			if (c >= 'Ꜣ' && c <= 'Ꞌ')
			{
				return TextInfoToLowerData.range_a722_a78b[(int)(c - 'Ꜣ')];
			}
			if ('Ａ' <= c && c <= 'Ｚ')
			{
				return c + ' ';
			}
			if (c == 'Ⅎ')
			{
				return 'ⅎ';
			}
			if (c != 'Ↄ')
			{
				return c;
			}
			return 'ↄ';
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000B77CC File Offset: 0x000B59CC
		private unsafe static int InternalCompareStringOrdinalIgnoreCase(string strA, int indexA, string strB, int indexB, int lenA, int lenB)
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
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && strA == strB)
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
						char c = char.ToUpperInvariant(*ptr3);
						char c2 = char.ToUpperInvariant(*ptr5);
						if (c != c2)
						{
							return (int)(c - c2);
						}
					}
					ptr3++;
					ptr5++;
				}
				return num - num2;
			}
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal TextInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B0B RID: 6923
		[OptionalField(VersionAdded = 2)]
		private string m_listSeparator;

		// Token: 0x04001B0C RID: 6924
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001B0D RID: 6925
		[OptionalField(VersionAdded = 3)]
		private string m_cultureName;

		// Token: 0x04001B0E RID: 6926
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x04001B0F RID: 6927
		[NonSerialized]
		private string m_textInfoName;

		// Token: 0x04001B10 RID: 6928
		[NonSerialized]
		private bool? m_IsAsciiCasingSameAsInvariant;

		// Token: 0x04001B11 RID: 6929
		internal static volatile TextInfo s_Invariant;

		// Token: 0x04001B12 RID: 6930
		[OptionalField(VersionAdded = 2)]
		private string customCultureName;

		// Token: 0x04001B13 RID: 6931
		[OptionalField(VersionAdded = 1)]
		internal int m_nDataItem;

		// Token: 0x04001B14 RID: 6932
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04001B15 RID: 6933
		[OptionalField(VersionAdded = 1)]
		internal int m_win32LangID;

		// Token: 0x04001B16 RID: 6934
		private const int wordSeparatorMask = 536672256;
	}
}
