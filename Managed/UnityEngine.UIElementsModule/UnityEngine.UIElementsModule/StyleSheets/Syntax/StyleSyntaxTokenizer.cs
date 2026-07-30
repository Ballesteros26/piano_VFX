using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x02000281 RID: 641
	internal class StyleSyntaxTokenizer
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000540F0 File Offset: 0x000522F0
		public StyleSyntaxToken current
		{
			get
			{
				bool flag = this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count;
				StyleSyntaxToken styleSyntaxToken;
				if (flag)
				{
					styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
				}
				else
				{
					styleSyntaxToken = this.m_Tokens[this.m_CurrentTokenIndex];
				}
				return styleSyntaxToken;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00054144 File Offset: 0x00052344
		public StyleSyntaxToken MoveNext()
		{
			StyleSyntaxToken styleSyntaxToken = this.current;
			bool flag = styleSyntaxToken.type == StyleSyntaxTokenType.Unknown;
			StyleSyntaxToken styleSyntaxToken2;
			if (flag)
			{
				styleSyntaxToken2 = styleSyntaxToken;
			}
			else
			{
				this.m_CurrentTokenIndex++;
				styleSyntaxToken = this.current;
				bool flag2 = this.m_CurrentTokenIndex == this.m_Tokens.Count;
				if (flag2)
				{
					this.m_CurrentTokenIndex = -1;
				}
				styleSyntaxToken2 = styleSyntaxToken;
			}
			return styleSyntaxToken2;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000541A4 File Offset: 0x000523A4
		public StyleSyntaxToken PeekNext()
		{
			int num = this.m_CurrentTokenIndex + 1;
			bool flag = this.m_CurrentTokenIndex < 0 || num >= this.m_Tokens.Count;
			StyleSyntaxToken styleSyntaxToken;
			if (flag)
			{
				styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
			}
			else
			{
				styleSyntaxToken = this.m_Tokens[num];
			}
			return styleSyntaxToken;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000541F8 File Offset: 0x000523F8
		public void Tokenize(string syntax)
		{
			this.m_Tokens.Clear();
			this.m_CurrentTokenIndex = 0;
			syntax = syntax.Trim(new char[] { ' ' }).ToLower();
			int i = 0;
			while (i < syntax.Length)
			{
				char c = syntax.get_Chars(i);
				char c2 = c;
				if (c2 <= '?')
				{
					switch (c2)
					{
					case ' ':
						i = StyleSyntaxTokenizer.GlobCharacter(syntax, i, ' ');
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
						break;
					case '!':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
						break;
					case '"':
					case '$':
					case '%':
					case '(':
					case ')':
						goto IL_02E6;
					case '#':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
						break;
					case '&':
					{
						bool flag = !StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '&');
						if (flag)
						{
							string text = ((i + 1 < syntax.Length) ? syntax.get_Chars(i + 1).ToString() : "EOF");
							Debug.LogAssertionFormat("Expected '&' got '{0}'", new object[] { text });
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
						}
						else
						{
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
							i++;
						}
						break;
					}
					case '\'':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
						break;
					case '*':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
						break;
					case '+':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
						break;
					case ',':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
						break;
					default:
						switch (c2)
						{
						case '<':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
							break;
						case '=':
							goto IL_02E6;
						case '>':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
							break;
						case '?':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
							break;
						default:
							goto IL_02E6;
						}
						break;
					}
				}
				else if (c2 != '[')
				{
					if (c2 != ']')
					{
						switch (c2)
						{
						case '{':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
							break;
						case '|':
						{
							bool flag2 = StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '|');
							if (flag2)
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
								i++;
							}
							else
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
							}
							break;
						}
						case '}':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
							break;
						default:
							goto IL_02E6;
						}
					}
					else
					{
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
					}
				}
				else
				{
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
				}
				IL_03C1:
				i++;
				continue;
				IL_02E6:
				bool flag3 = char.IsNumber(c);
				if (flag3)
				{
					int num = i;
					int num2 = 1;
					while (StyleSyntaxTokenizer.IsNextNumber(syntax, i))
					{
						i++;
						num2++;
					}
					string text2 = syntax.Substring(num, num2);
					int num3 = int.Parse(text2);
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, num3));
				}
				else
				{
					bool flag4 = char.IsLetter(c);
					if (flag4)
					{
						int num4 = i;
						int num5 = 1;
						while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, i))
						{
							i++;
							num5++;
						}
						string text3 = syntax.Substring(num4, num5);
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, text3));
					}
					else
					{
						Debug.LogAssertionFormat("Expected letter or number got '{0}'", new object[] { c });
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
					}
				}
				goto IL_03C1;
			}
			this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000545F0 File Offset: 0x000527F0
		private static bool IsNextCharacter(string s, int index, char c)
		{
			return index + 1 < s.Length && s.get_Chars(index + 1) == c;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0005461C File Offset: 0x0005281C
		private static bool IsNextLetterOrDash(string s, int index)
		{
			return index + 1 < s.Length && (char.IsLetter(s.get_Chars(index + 1)) || s.get_Chars(index + 1) == '-');
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0005465C File Offset: 0x0005285C
		private static bool IsNextNumber(string s, int index)
		{
			return index + 1 < s.Length && char.IsNumber(s.get_Chars(index + 1));
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0005468C File Offset: 0x0005288C
		private static int GlobCharacter(string s, int index, char c)
		{
			while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
			{
				index++;
			}
			return index;
		}

		// Token: 0x0400098A RID: 2442
		private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();

		// Token: 0x0400098B RID: 2443
		private int m_CurrentTokenIndex = -1;
	}
}
