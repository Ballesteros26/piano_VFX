using System;
using System.Globalization;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000171 RID: 369
	internal struct __DTString
	{
		// Token: 0x06000FE1 RID: 4065 RVA: 0x00044B55 File Offset: 0x00042D55
		internal __DTString(string str, DateTimeFormatInfo dtfi, bool checkDigitToken)
		{
			this = new __DTString(str, dtfi);
			this.m_checkDigitToken = checkDigitToken;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00044B68 File Offset: 0x00042D68
		internal __DTString(string str, DateTimeFormatInfo dtfi)
		{
			this.Index = -1;
			this.Value = str;
			this.len = this.Value.Length;
			this.m_current = '\0';
			if (dtfi != null)
			{
				this.m_info = dtfi.CompareInfo;
				this.m_checkDigitToken = (dtfi.FormatFlags & DateTimeFormatFlags.UseDigitPrefixInTokens) > DateTimeFormatFlags.None;
				return;
			}
			this.m_info = Thread.CurrentThread.CurrentCulture.CompareInfo;
			this.m_checkDigitToken = false;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00044BD9 File Offset: 0x00042DD9
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.m_info;
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00044BE1 File Offset: 0x00042DE1
		internal bool GetNext()
		{
			this.Index++;
			if (this.Index < this.len)
			{
				this.m_current = this.Value[this.Index];
				return true;
			}
			return false;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x00044C19 File Offset: 0x00042E19
		internal bool AtEnd()
		{
			return this.Index >= this.len;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00044C2C File Offset: 0x00042E2C
		internal bool Advance(int count)
		{
			this.Index += count;
			if (this.Index < this.len)
			{
				this.m_current = this.Value[this.Index];
				return true;
			}
			return false;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00044C64 File Offset: 0x00042E64
		[SecurityCritical]
		internal void GetRegularToken(out TokenType tokenType, out int tokenValue, DateTimeFormatInfo dtfi)
		{
			tokenValue = 0;
			if (this.Index >= this.len)
			{
				tokenType = TokenType.EndOfString;
				return;
			}
			tokenType = TokenType.UnknownToken;
			IL_0019:
			while (!DateTimeParse.IsDigit(this.m_current))
			{
				if (char.IsWhiteSpace(this.m_current))
				{
					for (;;)
					{
						int num = this.Index + 1;
						this.Index = num;
						if (num >= this.len)
						{
							break;
						}
						this.m_current = this.Value[this.Index];
						if (!char.IsWhiteSpace(this.m_current))
						{
							goto IL_0019;
						}
					}
					tokenType = TokenType.EndOfString;
					return;
				}
				dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType, out tokenValue, ref this);
				return;
			}
			tokenValue = (int)(this.m_current - '0');
			int index = this.Index;
			for (;;)
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.len)
				{
					break;
				}
				this.m_current = this.Value[this.Index];
				int num2 = (int)(this.m_current - '0');
				if (num2 < 0 || num2 > 9)
				{
					break;
				}
				tokenValue = tokenValue * 10 + num2;
			}
			if (this.Index - index > 8)
			{
				tokenType = TokenType.NumberToken;
				tokenValue = -1;
			}
			else if (this.Index - index < 3)
			{
				tokenType = TokenType.NumberToken;
			}
			else
			{
				tokenType = TokenType.YearNumberToken;
			}
			if (!this.m_checkDigitToken)
			{
				return;
			}
			int index2 = this.Index;
			char current = this.m_current;
			this.Index = index;
			this.m_current = this.Value[this.Index];
			TokenType tokenType2;
			int num3;
			if (dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType2, out num3, ref this))
			{
				tokenType = tokenType2;
				tokenValue = num3;
				return;
			}
			this.Index = index2;
			this.m_current = current;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00044DE8 File Offset: 0x00042FE8
		[SecurityCritical]
		internal TokenType GetSeparatorToken(DateTimeFormatInfo dtfi, out int indexBeforeSeparator, out char charBeforeSeparator)
		{
			indexBeforeSeparator = this.Index;
			charBeforeSeparator = this.m_current;
			if (!this.SkipWhiteSpaceCurrent())
			{
				return TokenType.SEP_End;
			}
			TokenType tokenType;
			if (!DateTimeParse.IsDigit(this.m_current))
			{
				int num;
				if (!dtfi.Tokenize(TokenType.SeparatorTokenMask, out tokenType, out num, ref this))
				{
					tokenType = TokenType.SEP_Space;
				}
			}
			else
			{
				tokenType = TokenType.SEP_Space;
			}
			return tokenType;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00044E41 File Offset: 0x00043041
		internal bool MatchSpecifiedWord(string target)
		{
			return this.MatchSpecifiedWord(target, target.Length + this.Index);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00044E58 File Offset: 0x00043058
		internal bool MatchSpecifiedWord(string target, int endIndex)
		{
			int num = endIndex - this.Index;
			return num == target.Length && this.Index + num <= this.len && this.m_info.Compare(this.Value, this.Index, num, target, 0, num, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00044EAC File Offset: 0x000430AC
		internal bool MatchSpecifiedWords(string target, bool checkWordBoundary, ref int matchLength)
		{
			int num = this.Value.Length - this.Index;
			matchLength = target.Length;
			if (matchLength > num || this.m_info.Compare(this.Value, this.Index, matchLength, target, 0, matchLength, CompareOptions.IgnoreCase) != 0)
			{
				int num2 = 0;
				int num3 = this.Index;
				int num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2);
				if (num4 == -1)
				{
					return false;
				}
				for (;;)
				{
					int num5 = num4 - num2;
					if (num3 >= this.Value.Length - num5)
					{
						break;
					}
					if (num5 == 0)
					{
						matchLength--;
					}
					else
					{
						if (!char.IsWhiteSpace(this.Value[num3 + num5]))
						{
							return false;
						}
						if (this.m_info.Compare(this.Value, num3, num5, target, num2, num5, CompareOptions.IgnoreCase) != 0)
						{
							return false;
						}
						num3 = num3 + num5 + 1;
					}
					num2 = num4 + 1;
					while (num3 < this.Value.Length && char.IsWhiteSpace(this.Value[num3]))
					{
						num3++;
						matchLength++;
					}
					if ((num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2)) < 0)
					{
						goto Block_8;
					}
				}
				return false;
				Block_8:
				if (num2 < target.Length)
				{
					int num6 = target.Length - num2;
					if (num3 > this.Value.Length - num6)
					{
						return false;
					}
					if (this.m_info.Compare(this.Value, num3, num6, target, num2, num6, CompareOptions.IgnoreCase) != 0)
					{
						return false;
					}
				}
			}
			if (checkWordBoundary)
			{
				int num7 = this.Index + matchLength;
				if (num7 < this.Value.Length && char.IsLetter(this.Value[num7]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00045034 File Offset: 0x00043234
		internal bool Match(string str)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.len)
			{
				return false;
			}
			if (str.Length > this.Value.Length - this.Index)
			{
				return false;
			}
			if (this.m_info.Compare(this.Value, this.Index, str.Length, str, 0, str.Length, CompareOptions.Ordinal) == 0)
			{
				this.Index += str.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000450BC File Offset: 0x000432BC
		internal bool Match(char ch)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.len)
			{
				return false;
			}
			if (this.Value[this.Index] == ch)
			{
				this.m_current = ch;
				return true;
			}
			this.Index--;
			return false;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00045110 File Offset: 0x00043310
		internal int MatchLongestWords(string[] words, ref int maxMatchStrLen)
		{
			int num = -1;
			for (int i = 0; i < words.Length; i++)
			{
				string text = words[i];
				int length = text.Length;
				if (this.MatchSpecifiedWords(text, false, ref length) && length > maxMatchStrLen)
				{
					maxMatchStrLen = length;
					num = i;
				}
			}
			return num;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00045150 File Offset: 0x00043350
		internal int GetRepeatCount()
		{
			char c = this.Value[this.Index];
			int num = this.Index + 1;
			while (num < this.len && this.Value[num] == c)
			{
				num++;
			}
			int num2 = num - this.Index;
			this.Index = num - 1;
			return num2;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000451A8 File Offset: 0x000433A8
		internal bool GetNextDigit()
		{
			int num = this.Index + 1;
			this.Index = num;
			return num < this.len && DateTimeParse.IsDigit(this.Value[this.Index]);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000451E6 File Offset: 0x000433E6
		internal char GetChar()
		{
			return this.Value[this.Index];
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000451F9 File Offset: 0x000433F9
		internal int GetDigit()
		{
			return (int)(this.Value[this.Index] - '0');
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0004520F File Offset: 0x0004340F
		internal void SkipWhiteSpaces()
		{
			while (this.Index + 1 < this.len)
			{
				if (!char.IsWhiteSpace(this.Value[this.Index + 1]))
				{
					return;
				}
				this.Index++;
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0004524C File Offset: 0x0004344C
		internal bool SkipWhiteSpaceCurrent()
		{
			if (this.Index >= this.len)
			{
				return false;
			}
			if (!char.IsWhiteSpace(this.m_current))
			{
				return true;
			}
			do
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.len)
				{
					return false;
				}
				this.m_current = this.Value[this.Index];
			}
			while (char.IsWhiteSpace(this.m_current));
			return true;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000452B8 File Offset: 0x000434B8
		internal void TrimTail()
		{
			int num = this.len - 1;
			while (num >= 0 && char.IsWhiteSpace(this.Value[num]))
			{
				num--;
			}
			this.Value = this.Value.Substring(0, num + 1);
			this.len = this.Value.Length;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00045314 File Offset: 0x00043514
		internal void RemoveTrailingInQuoteSpaces()
		{
			int num = this.len - 1;
			if (num <= 1)
			{
				return;
			}
			char c = this.Value[num];
			if ((c == '\'' || c == '"') && char.IsWhiteSpace(this.Value[num - 1]))
			{
				num--;
				while (num >= 1 && char.IsWhiteSpace(this.Value[num - 1]))
				{
					num--;
				}
				this.Value = this.Value.Remove(num, this.Value.Length - 1 - num);
				this.len = this.Value.Length;
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000453B0 File Offset: 0x000435B0
		internal void RemoveLeadingInQuoteSpaces()
		{
			if (this.len <= 2)
			{
				return;
			}
			int num = 0;
			char c = this.Value[num];
			if (c != '\'')
			{
				if (c != '"')
				{
					return;
				}
			}
			while (num + 1 < this.len && char.IsWhiteSpace(this.Value[num + 1]))
			{
				num++;
			}
			if (num != 0)
			{
				this.Value = this.Value.Remove(1, num);
				this.len = this.Value.Length;
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00045430 File Offset: 0x00043630
		internal DTSubString GetSubString()
		{
			DTSubString dtsubString = default(DTSubString);
			dtsubString.index = this.Index;
			dtsubString.s = this.Value;
			while (this.Index + dtsubString.length < this.len)
			{
				char c = this.Value[this.Index + dtsubString.length];
				DTSubStringType dtsubStringType;
				if (c >= '0' && c <= '9')
				{
					dtsubStringType = DTSubStringType.Number;
				}
				else
				{
					dtsubStringType = DTSubStringType.Other;
				}
				if (dtsubString.length == 0)
				{
					dtsubString.type = dtsubStringType;
				}
				else if (dtsubString.type != dtsubStringType)
				{
					break;
				}
				dtsubString.length++;
				if (dtsubStringType != DTSubStringType.Number)
				{
					break;
				}
				if (dtsubString.length > 8)
				{
					dtsubString.type = DTSubStringType.Invalid;
					return dtsubString;
				}
				int num = (int)(c - '0');
				dtsubString.value = dtsubString.value * 10 + num;
			}
			if (dtsubString.length == 0)
			{
				dtsubString.type = DTSubStringType.End;
				return dtsubString;
			}
			return dtsubString;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0004550A File Offset: 0x0004370A
		internal void ConsumeSubString(DTSubString sub)
		{
			this.Index = sub.index + sub.length;
			if (this.Index < this.len)
			{
				this.m_current = this.Value[this.Index];
			}
		}

		// Token: 0x04000988 RID: 2440
		internal string Value;

		// Token: 0x04000989 RID: 2441
		internal int Index;

		// Token: 0x0400098A RID: 2442
		internal int len;

		// Token: 0x0400098B RID: 2443
		internal char m_current;

		// Token: 0x0400098C RID: 2444
		private CompareInfo m_info;

		// Token: 0x0400098D RID: 2445
		private bool m_checkDigitToken;

		// Token: 0x0400098E RID: 2446
		private static char[] WhiteSpaceChecks = new char[] { ' ', '\u00a0' };
	}
}
