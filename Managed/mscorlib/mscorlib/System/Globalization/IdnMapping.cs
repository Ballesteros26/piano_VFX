using System;
using System.Text;

namespace System.Globalization
{
	/// <summary>Supports the use of non-ASCII characters for Internet domain names. This class cannot be inherited.</summary>
	// Token: 0x02000445 RID: 1093
	public sealed class IdnMapping
	{
		/// <summary>Gets or sets a value that indicates whether unassigned Unicode code points are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>true if unassigned code points are used in operations; otherwise, false.</returns>
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000C2367 File Offset: 0x000C0567
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000C236F File Offset: 0x000C056F
		public bool AllowUnassigned
		{
			get
			{
				return this.allow_unassigned;
			}
			set
			{
				this.allow_unassigned = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether standard or relaxed naming conventions are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>true if standard naming conventions are used in operations; otherwise, false.</returns>
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000C2378 File Offset: 0x000C0578
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000C2380 File Offset: 0x000C0580
		public bool UseStd3AsciiRules
		{
			get
			{
				return this.use_std3;
			}
			set
			{
				this.use_std3 = value;
			}
		}

		/// <summary>Indicates whether a specified object and the current <see cref="T:System.Globalization.IdnMapping" /> object are equal.</summary>
		/// <returns>true if the object specified by the <paramref name="obj" /> parameter is derived from <see cref="T:System.Globalization.IdnMapping" /> and its <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties are equal; otherwise, false. </returns>
		/// <param name="obj">The object to compare to the current object.</param>
		// Token: 0x06003471 RID: 13425 RVA: 0x000C238C File Offset: 0x000C058C
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this.allow_unassigned == idnMapping.allow_unassigned && this.use_std3 == idnMapping.use_std3;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>One of four 32-bit signed constants derived from the properties of an <see cref="T:System.Globalization.IdnMapping" /> object.  The return value has no special meaning and is not suitable for use in a hash code algorithm.</returns>
		// Token: 0x06003472 RID: 13426 RVA: 0x000C23C1 File Offset: 0x000C05C1
		public override int GetHashCode()
		{
			return (this.allow_unassigned ? 2 : 0) + (this.use_std3 ? 1 : 0);
		}

		/// <summary>Encodes a string of domain name labels that consist of Unicode characters to a string of displayable Unicode characters in the US-ASCII character range. The string is formatted according to the IDNA standard.</summary>
		/// <returns>The equivalent of the string specified by the <paramref name="unicode" /> parameter, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003473 RID: 13427 RVA: 0x000C23DC File Offset: 0x000C05DC
		public string GetAscii(string unicode)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, 0, unicode.Length);
		}

		/// <summary>Encodes a substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard.  </summary>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" /> and <paramref name="index" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring to convert. The conversion operation continues to the end of the <paramref name="unicode" /> string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003474 RID: 13428 RVA: 0x000C23FA File Offset: 0x000C05FA
		public string GetAscii(string unicode, int index)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, index, unicode.Length - index);
		}

		/// <summary>Encodes the specified number of characters in a  substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard. </summary>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" />, <paramref name="index" />, and <paramref name="count" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by  <paramref name="index" /> in the <paramref name="unicode" /> string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003475 RID: 13429 RVA: 0x000C241C File Offset: 0x000C061C
		public string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument unicode string");
			}
			return this.Convert(unicode, index, count, true);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000C246C File Offset: 0x000C066C
		private string Convert(string input, int index, int count, bool toAscii)
		{
			string text = input.Substring(index, count);
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] >= '\u0080')
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					break;
				}
			}
			string[] array = text.Split(new char[] { '.', '。', '．', '｡' });
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length != 0 || j + 1 != array.Length)
				{
					if (toAscii)
					{
						array[j] = this.ToAscii(array[j], num);
					}
					else
					{
						array[j] = this.ToUnicode(array[j], num);
					}
				}
				num += array[j].Length;
			}
			return string.Join(".", array);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000C252C File Offset: 0x000C072C
		private string ToAscii(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < ' ' || s[i] == '\u007f')
				{
					throw new ArgumentException(string.Format("Not allowed character was found, at {0}", offset + i));
				}
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (this.use_std3)
			{
				this.VerifyStd3AsciiRules(s, offset);
			}
			int j = 0;
			while (j < s.Length)
			{
				if (s[j] >= '\u0080')
				{
					if (s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(string.Format("The input string must not start with ACE (xn--), at {0}", offset + j));
					}
					s = this.puny.Encode(s, offset);
					s = "xn--" + s;
					break;
				}
				else
				{
					j++;
				}
			}
			this.VerifyLength(s, offset);
			return s;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000C260E File Offset: 0x000C080E
		private void VerifyLength(string s, int offset)
		{
			if (s.Length == 0)
			{
				throw new ArgumentException(string.Format("A label in the input string resulted in an invalid zero-length string, at {0}", offset));
			}
			if (s.Length > 63)
			{
				throw new ArgumentException(string.Format("A label in the input string exceeded the length in ASCII representation, at {0}", offset));
			}
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000C2650 File Offset: 0x000C0850
		private string NamePrep(string s, int offset)
		{
			s = s.Normalize(NormalizationForm.FormKC);
			this.VerifyProhibitedCharacters(s, offset);
			if (!this.allow_unassigned)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (char.GetUnicodeCategory(s, i) == UnicodeCategory.OtherNotAssigned)
					{
						throw new ArgumentException(string.Format("Use of unassigned Unicode characer is prohibited in this IdnMapping, at {0}", offset + i));
					}
				}
			}
			return s;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000C26AC File Offset: 0x000C08AC
		private void VerifyProhibitedCharacters(string s, int offset)
		{
			int i = 0;
			while (i < s.Length)
			{
				switch (char.GetUnicodeCategory(s, i))
				{
				case UnicodeCategory.SpaceSeparator:
					if (s[i] >= '\u0080')
					{
						goto IL_0111;
					}
					break;
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Format:
					goto IL_006E;
				case UnicodeCategory.Control:
					if (s[i] == '\0' || s[i] >= '\u0080')
					{
						goto IL_0111;
					}
					break;
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_0111;
				default:
					goto IL_006E;
				}
				IL_0129:
				i++;
				continue;
				IL_0111:
				throw new ArgumentException(string.Format("Not allowed character was in the input string, at {0}", offset + i));
				IL_006E:
				char c = s[i];
				if (('\ufddf' <= c && c <= '\ufdef') || (c & '\uffff') == '\ufffe' || ('\ufff9' <= c && c <= '\ufffd') || ('⿰' <= c && c <= '⿻') || ('\u202a' <= c && c <= '\u202e') || ('\u206a' <= c && c <= '\u206f'))
				{
					goto IL_0111;
				}
				if (c <= '\u200e')
				{
					if (c != '\u0340' && c != '\u0341' && c != '\u200e')
					{
						goto IL_0129;
					}
					goto IL_0111;
				}
				else
				{
					if (c == '\u200f' || c == '\u2028' || c == '\u2029')
					{
						goto IL_0111;
					}
					goto IL_0129;
				}
			}
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000C27F4 File Offset: 0x000C09F4
		private void VerifyStd3AsciiRules(string s, int offset)
		{
			if (s.Length > 0 && s[0] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at head of a sequence in STD3 mode, found at {0}", offset));
			}
			if (s.Length > 0 && s[s.Length - 1] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at tail of a sequence in STD3 mode, found at {0}", offset + s.Length - 1));
			}
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c != '-' && (c <= '/' || (':' <= c && c <= '@') || ('[' <= c && c <= '`') || ('{' <= c && c <= '\u007f')))
				{
					throw new ArgumentException(string.Format("Not allowed character in STD3 mode, found at {0}", offset + i));
				}
			}
		}

		/// <summary>Decodes a string of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> parameter.</returns>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600347C RID: 13436 RVA: 0x000C28BE File Offset: 0x000C0ABE
		public string GetUnicode(string ascii)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, 0, ascii.Length);
		}

		/// <summary>Decodes a substring of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> and <paramref name="index" /> parameters.</returns>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring to decode. The decoding operation continues to the end of the <paramref name="ascii" /> string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600347D RID: 13437 RVA: 0x000C28DC File Offset: 0x000C0ADC
		public string GetUnicode(string ascii, int index)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, index, ascii.Length - index);
		}

		/// <summary>Decodes a substring of a specified length that contains one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" />, <paramref name="index" />, and <paramref name="count" /> parameters.</returns>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring. </param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="index" /> in the <paramref name="ascii" /> string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600347E RID: 13438 RVA: 0x000C28FC File Offset: 0x000C0AFC
		public string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument ascii string");
			}
			return this.Convert(ascii, index, count, false);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000C294C File Offset: 0x000C0B4C
		private string ToUnicode(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (!s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
			{
				return s;
			}
			s = s.ToLower(CultureInfo.InvariantCulture);
			string text = s;
			s = s.Substring(4);
			s = this.puny.Decode(s, offset);
			string text2 = s;
			s = this.ToAscii(s, offset);
			if (string.Compare(text, s, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(string.Format("ToUnicode() failed at verifying the result, at label part from {0}", offset));
			}
			return text2;
		}

		// Token: 0x04001C0A RID: 7178
		private bool allow_unassigned;

		// Token: 0x04001C0B RID: 7179
		private bool use_std3;

		// Token: 0x04001C0C RID: 7180
		private Punycode puny = new Punycode();
	}
}
