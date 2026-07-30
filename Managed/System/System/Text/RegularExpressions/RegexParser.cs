using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000154 RID: 340
	internal sealed class RegexParser
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x0003429C File Offset: 0x0003249C
		internal static RegexTree Parse(string re, RegexOptions op)
		{
			RegexParser regexParser = new RegexParser(((op & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			regexParser._options = op;
			regexParser.SetPattern(re);
			regexParser.CountCaptures();
			regexParser.Reset(op);
			RegexNode regexNode = regexParser.ScanRegex();
			string[] array;
			if (regexParser._capnamelist == null)
			{
				array = null;
			}
			else
			{
				array = regexParser._capnamelist.ToArray();
			}
			return new RegexTree(regexNode, regexParser._caps, regexParser._capnumlist, regexParser._captop, regexParser._capnames, array, op);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0003431C File Offset: 0x0003251C
		internal static RegexReplacement ParseReplacement(string rep, Hashtable caps, int capsize, Hashtable capnames, RegexOptions op)
		{
			RegexParser regexParser = new RegexParser(((op & RegexOptions.CultureInvariant) != RegexOptions.None) ? CultureInfo.InvariantCulture : CultureInfo.CurrentCulture);
			regexParser._options = op;
			regexParser.NoteCaptures(caps, capsize, capnames);
			regexParser.SetPattern(rep);
			RegexNode regexNode = regexParser.ScanReplacement();
			return new RegexReplacement(rep, regexNode, caps);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003436C File Offset: 0x0003256C
		internal static string Escape(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (RegexParser.IsMetachar(input[i]))
				{
					StringBuilder stringBuilder = new StringBuilder();
					char c = input[i];
					stringBuilder.Append(input, 0, i);
					do
					{
						stringBuilder.Append('\\');
						switch (c)
						{
						case '\t':
							c = 't';
							break;
						case '\n':
							c = 'n';
							break;
						case '\f':
							c = 'f';
							break;
						case '\r':
							c = 'r';
							break;
						}
						stringBuilder.Append(c);
						i++;
						int num = i;
						while (i < input.Length)
						{
							c = input[i];
							if (RegexParser.IsMetachar(c))
							{
								break;
							}
							i++;
						}
						stringBuilder.Append(input, num, i - num);
					}
					while (i < input.Length);
					return stringBuilder.ToString();
				}
			}
			return input;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00034440 File Offset: 0x00032640
		internal static string Unescape(string input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == '\\')
				{
					StringBuilder stringBuilder = new StringBuilder();
					RegexParser regexParser = new RegexParser(CultureInfo.InvariantCulture);
					regexParser.SetPattern(input);
					stringBuilder.Append(input, 0, i);
					do
					{
						i++;
						regexParser.Textto(i);
						if (i < input.Length)
						{
							stringBuilder.Append(regexParser.ScanCharEscape());
						}
						i = regexParser.Textpos();
						int num = i;
						while (i < input.Length && input[i] != '\\')
						{
							i++;
						}
						stringBuilder.Append(input, num, i - num);
					}
					while (i < input.Length);
					return stringBuilder.ToString();
				}
			}
			return input;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000344F5 File Offset: 0x000326F5
		private RegexParser(CultureInfo culture)
		{
			this._culture = culture;
			this._optionsStack = new List<RegexOptions>();
			this._caps = new Hashtable();
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0003451A File Offset: 0x0003271A
		internal void SetPattern(string Re)
		{
			if (Re == null)
			{
				Re = string.Empty;
			}
			this._pattern = Re;
			this._currentPos = 0;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00034534 File Offset: 0x00032734
		internal void Reset(RegexOptions topopts)
		{
			this._currentPos = 0;
			this._autocap = 1;
			this._ignoreNextParen = false;
			if (this._optionsStack.Count > 0)
			{
				this._optionsStack.RemoveRange(0, this._optionsStack.Count - 1);
			}
			this._options = topopts;
			this._stack = null;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0003458C File Offset: 0x0003278C
		internal RegexNode ScanRegex()
		{
			bool flag = false;
			this.StartGroup(new RegexNode(28, this._options, 0, -1));
			while (this.CharsRight() > 0)
			{
				bool flag2 = flag;
				flag = false;
				this.ScanBlank();
				int num = this.Textpos();
				char c;
				if (this.UseOptionX())
				{
					while (this.CharsRight() > 0)
					{
						if (RegexParser.IsStopperX(c = this.RightChar()))
						{
							if (c != '{')
							{
								break;
							}
							if (this.IsTrueQuantifier())
							{
								break;
							}
						}
						this.MoveRight();
					}
				}
				else
				{
					while (this.CharsRight() > 0 && (!RegexParser.IsSpecial(c = this.RightChar()) || (c == '{' && !this.IsTrueQuantifier())))
					{
						this.MoveRight();
					}
				}
				int num2 = this.Textpos();
				this.ScanBlank();
				if (this.CharsRight() == 0)
				{
					c = '!';
				}
				else if (RegexParser.IsSpecial(c = this.RightChar()))
				{
					flag = RegexParser.IsQuantifier(c);
					this.MoveRight();
				}
				else
				{
					c = ' ';
				}
				if (num < num2)
				{
					int num3 = num2 - num - (flag ? 1 : 0);
					flag2 = false;
					if (num3 > 0)
					{
						this.AddConcatenate(num, num3, false);
					}
					if (flag)
					{
						this.AddUnitOne(this.CharAt(num2 - 1));
					}
				}
				if (c <= '?')
				{
					switch (c)
					{
					case ' ':
						continue;
					case '!':
						goto IL_0437;
					case '"':
					case '#':
					case '%':
					case '&':
					case '\'':
					case ',':
					case '-':
						goto IL_02B7;
					case '$':
						this.AddUnitType(this.UseOptionM() ? 15 : 20);
						break;
					case '(':
					{
						this.PushOptions();
						RegexNode regexNode;
						if ((regexNode = this.ScanGroupOpen()) == null)
						{
							this.PopKeepOptions();
							continue;
						}
						this.PushGroup();
						this.StartGroup(regexNode);
						continue;
					}
					case ')':
						if (this.EmptyStack())
						{
							throw this.MakeException(global::SR.GetString("Too many )'s."));
						}
						this.AddGroup();
						this.PopGroup();
						this.PopOptions();
						if (this.Unit() == null)
						{
							continue;
						}
						break;
					case '*':
					case '+':
						goto IL_0277;
					case '.':
						if (this.UseOptionS())
						{
							this.AddUnitSet("\0\u0001\0\0");
						}
						else
						{
							this.AddUnitNotone('\n');
						}
						break;
					default:
						if (c != '?')
						{
							goto IL_02B7;
						}
						goto IL_0277;
					}
				}
				else
				{
					switch (c)
					{
					case '[':
						this.AddUnitSet(this.ScanCharClass(this.UseOptionI()).ToStringClass());
						break;
					case '\\':
						this.AddUnitNode(this.ScanBackslash());
						break;
					case ']':
						goto IL_02B7;
					case '^':
						this.AddUnitType(this.UseOptionM() ? 14 : 18);
						break;
					default:
						if (c == '{')
						{
							goto IL_0277;
						}
						if (c != '|')
						{
							goto IL_02B7;
						}
						this.AddAlternate();
						continue;
					}
				}
				IL_02C8:
				this.ScanBlank();
				if (this.CharsRight() == 0 || !(flag = this.IsTrueQuantifier()))
				{
					this.AddConcatenate();
					continue;
				}
				c = this.MoveRightGetChar();
				while (this.Unit() != null)
				{
					int num4;
					int num5;
					if (c <= '+')
					{
						if (c != '*')
						{
							if (c != '+')
							{
								goto IL_03C6;
							}
							num4 = 1;
							num5 = int.MaxValue;
						}
						else
						{
							num4 = 0;
							num5 = int.MaxValue;
						}
					}
					else if (c != '?')
					{
						if (c != '{')
						{
							goto IL_03C6;
						}
						num = this.Textpos();
						num4 = (num5 = this.ScanDecimal());
						if (num < this.Textpos() && this.CharsRight() > 0 && this.RightChar() == ',')
						{
							this.MoveRight();
							if (this.CharsRight() == 0 || this.RightChar() == '}')
							{
								num5 = int.MaxValue;
							}
							else
							{
								num5 = this.ScanDecimal();
							}
						}
						if (num == this.Textpos() || this.CharsRight() == 0 || this.MoveRightGetChar() != '}')
						{
							this.AddConcatenate();
							this.Textto(num - 1);
							break;
						}
					}
					else
					{
						num4 = 0;
						num5 = 1;
					}
					this.ScanBlank();
					bool flag3;
					if (this.CharsRight() == 0 || this.RightChar() != '?')
					{
						flag3 = false;
					}
					else
					{
						this.MoveRight();
						flag3 = true;
					}
					if (num4 > num5)
					{
						throw this.MakeException(global::SR.GetString("Illegal {x,y} with x > y."));
					}
					this.AddConcatenate(flag3, num4, num5);
					continue;
					IL_03C6:
					throw this.MakeException(global::SR.GetString("Internal error in ScanRegex."));
				}
				continue;
				IL_0277:
				if (this.Unit() == null)
				{
					throw this.MakeException(flag2 ? global::SR.GetString("Nested quantifier {0}.", new object[] { c.ToString() }) : global::SR.GetString("Quantifier {x,y} following nothing."));
				}
				this.MoveLeft();
				goto IL_02C8;
				IL_02B7:
				throw this.MakeException(global::SR.GetString("Internal error in ScanRegex."));
			}
			IL_0437:
			if (!this.EmptyStack())
			{
				throw this.MakeException(global::SR.GetString("Not enough )'s."));
			}
			this.AddGroup();
			return this.Unit();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000349F8 File Offset: 0x00032BF8
		internal RegexNode ScanReplacement()
		{
			this._concatenation = new RegexNode(25, this._options);
			for (;;)
			{
				int num = this.CharsRight();
				if (num == 0)
				{
					break;
				}
				int num2 = this.Textpos();
				while (num > 0 && this.RightChar() != '$')
				{
					this.MoveRight();
					num--;
				}
				this.AddConcatenate(num2, this.Textpos() - num2, true);
				if (num > 0)
				{
					if (this.MoveRightGetChar() == '$')
					{
						this.AddUnitNode(this.ScanDollar());
					}
					this.AddConcatenate();
				}
			}
			return this._concatenation;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00034A7B File Offset: 0x00032C7B
		internal RegexCharClass ScanCharClass(bool caseInsensitive)
		{
			return this.ScanCharClass(caseInsensitive, false);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00034A88 File Offset: 0x00032C88
		internal RegexCharClass ScanCharClass(bool caseInsensitive, bool scanOnly)
		{
			char c = '\0';
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			RegexCharClass regexCharClass = (scanOnly ? null : new RegexCharClass());
			if (this.CharsRight() > 0 && this.RightChar() == '^')
			{
				this.MoveRight();
				if (!scanOnly)
				{
					regexCharClass.Negate = true;
				}
			}
			while (this.CharsRight() > 0)
			{
				bool flag4 = false;
				char c2 = this.MoveRightGetChar();
				if (c2 == ']')
				{
					if (!flag2)
					{
						flag3 = true;
						break;
					}
					goto IL_028B;
				}
				else
				{
					if (c2 == '\\' && this.CharsRight() > 0)
					{
						char c3;
						c2 = (c3 = this.MoveRightGetChar());
						if (c3 <= 'S')
						{
							if (c3 <= 'D')
							{
								if (c3 != '-')
								{
									if (c3 != 'D')
									{
										goto IL_0224;
									}
								}
								else
								{
									if (!scanOnly)
									{
										regexCharClass.AddRange(c2, c2);
										goto IL_03AA;
									}
									goto IL_03AA;
								}
							}
							else
							{
								if (c3 == 'P')
								{
									goto IL_01BC;
								}
								if (c3 != 'S')
								{
									goto IL_0224;
								}
								goto IL_013A;
							}
						}
						else
						{
							if (c3 <= 'd')
							{
								if (c3 != 'W')
								{
									if (c3 != 'd')
									{
										goto IL_0224;
									}
									goto IL_00F3;
								}
							}
							else
							{
								if (c3 == 'p')
								{
									goto IL_01BC;
								}
								if (c3 == 's')
								{
									goto IL_013A;
								}
								if (c3 != 'w')
								{
									goto IL_0224;
								}
							}
							if (scanOnly)
							{
								goto IL_03AA;
							}
							if (flag)
							{
								throw this.MakeException(global::SR.GetString("Cannot include class \\{0} in character range.", new object[] { c2.ToString() }));
							}
							regexCharClass.AddWord(this.UseOptionE(), c2 == 'W');
							goto IL_03AA;
						}
						IL_00F3:
						if (scanOnly)
						{
							goto IL_03AA;
						}
						if (flag)
						{
							throw this.MakeException(global::SR.GetString("Cannot include class \\{0} in character range.", new object[] { c2.ToString() }));
						}
						regexCharClass.AddDigit(this.UseOptionE(), c2 == 'D', this._pattern);
						goto IL_03AA;
						IL_013A:
						if (scanOnly)
						{
							goto IL_03AA;
						}
						if (flag)
						{
							throw this.MakeException(global::SR.GetString("Cannot include class \\{0} in character range.", new object[] { c2.ToString() }));
						}
						regexCharClass.AddSpace(this.UseOptionE(), c2 == 'S');
						goto IL_03AA;
						IL_01BC:
						if (scanOnly)
						{
							this.ParseProperty();
							goto IL_03AA;
						}
						if (flag)
						{
							throw this.MakeException(global::SR.GetString("Cannot include class \\{0} in character range.", new object[] { c2.ToString() }));
						}
						regexCharClass.AddCategoryFromName(this.ParseProperty(), c2 != 'p', caseInsensitive, this._pattern);
						goto IL_03AA;
						IL_0224:
						this.MoveLeft();
						c2 = this.ScanCharEscape();
						flag4 = true;
						goto IL_028B;
					}
					if (c2 != '[' || this.CharsRight() <= 0 || this.RightChar() != ':' || flag)
					{
						goto IL_028B;
					}
					int num = this.Textpos();
					this.MoveRight();
					this.ScanCapname();
					if (this.CharsRight() < 2 || this.MoveRightGetChar() != ':' || this.MoveRightGetChar() != ']')
					{
						this.Textto(num);
						goto IL_028B;
					}
					goto IL_028B;
				}
				IL_03AA:
				flag2 = false;
				continue;
				IL_028B:
				if (flag)
				{
					flag = false;
					if (scanOnly)
					{
						goto IL_03AA;
					}
					if (c2 == '[' && !flag4 && !flag2)
					{
						regexCharClass.AddChar(c);
						regexCharClass.AddSubtraction(this.ScanCharClass(caseInsensitive, false));
						if (this.CharsRight() > 0 && this.RightChar() != ']')
						{
							throw this.MakeException(global::SR.GetString("A subtraction must be the last element in a character class."));
						}
						goto IL_03AA;
					}
					else
					{
						if (c > c2)
						{
							throw this.MakeException(global::SR.GetString("[x-y] range in reverse order."));
						}
						regexCharClass.AddRange(c, c2);
						goto IL_03AA;
					}
				}
				else
				{
					if (this.CharsRight() >= 2 && this.RightChar() == '-' && this.RightChar(1) != ']')
					{
						c = c2;
						flag = true;
						this.MoveRight();
						goto IL_03AA;
					}
					if (this.CharsRight() >= 1 && c2 == '-' && !flag4 && this.RightChar() == '[' && !flag2)
					{
						if (scanOnly)
						{
							this.MoveRight(1);
							this.ScanCharClass(caseInsensitive, true);
							goto IL_03AA;
						}
						this.MoveRight(1);
						regexCharClass.AddSubtraction(this.ScanCharClass(caseInsensitive, false));
						if (this.CharsRight() > 0 && this.RightChar() != ']')
						{
							throw this.MakeException(global::SR.GetString("A subtraction must be the last element in a character class."));
						}
						goto IL_03AA;
					}
					else
					{
						if (!scanOnly)
						{
							regexCharClass.AddRange(c2, c2);
							goto IL_03AA;
						}
						goto IL_03AA;
					}
				}
			}
			if (!flag3)
			{
				throw this.MakeException(global::SR.GetString("Unterminated [] set."));
			}
			if (!scanOnly && caseInsensitive)
			{
				regexCharClass.AddLowercase(this._culture);
			}
			return regexCharClass;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00034E7C File Offset: 0x0003307C
		internal RegexNode ScanGroupOpen()
		{
			char c = '>';
			if (this.CharsRight() != 0 && this.RightChar() == '?' && (this.RightChar() != '?' || this.CharsRight() <= 1 || this.RightChar(1) != ')'))
			{
				this.MoveRight();
				if (this.CharsRight() != 0)
				{
					char c2 = this.MoveRightGetChar();
					int num;
					char c3;
					if (c2 <= '\'')
					{
						if (c2 == '!')
						{
							this._options &= ~RegexOptions.RightToLeft;
							num = 31;
							goto IL_0551;
						}
						if (c2 != '\'')
						{
							goto IL_0527;
						}
						c = '\'';
					}
					else if (c2 != '(')
					{
						switch (c2)
						{
						case ':':
							num = 29;
							goto IL_0551;
						case ';':
							goto IL_0527;
						case '<':
							break;
						case '=':
							this._options &= ~RegexOptions.RightToLeft;
							num = 30;
							goto IL_0551;
						case '>':
							num = 32;
							goto IL_0551;
						default:
							goto IL_0527;
						}
					}
					else
					{
						int num2 = this.Textpos();
						if (this.CharsRight() > 0)
						{
							c3 = this.RightChar();
							if (c3 >= '0' && c3 <= '9')
							{
								int num3 = this.ScanDecimal();
								if (this.CharsRight() <= 0 || this.MoveRightGetChar() != ')')
								{
									throw this.MakeException(global::SR.GetString("(?({0}) ) malformed.", new object[] { num3.ToString(CultureInfo.CurrentCulture) }));
								}
								if (this.IsCaptureSlot(num3))
								{
									return new RegexNode(33, this._options, num3);
								}
								throw this.MakeException(global::SR.GetString("(?({0}) ) reference to undefined group.", new object[] { num3.ToString(CultureInfo.CurrentCulture) }));
							}
							else if (RegexCharClass.IsWordChar(c3))
							{
								string text = this.ScanCapname();
								if (this.IsCaptureName(text) && this.CharsRight() > 0 && this.MoveRightGetChar() == ')')
								{
									return new RegexNode(33, this._options, this.CaptureSlotFromName(text));
								}
							}
						}
						num = 34;
						this.Textto(num2 - 1);
						this._ignoreNextParen = true;
						int num4 = this.CharsRight();
						if (num4 < 3 || this.RightChar(1) != '?')
						{
							goto IL_0551;
						}
						char c4 = this.RightChar(2);
						if (c4 == '#')
						{
							throw this.MakeException(global::SR.GetString("Alternation conditions cannot be comments."));
						}
						if (c4 == '\'')
						{
							throw this.MakeException(global::SR.GetString("Alternation conditions do not capture and cannot be named."));
						}
						if (num4 >= 4 && c4 == '<' && this.RightChar(3) != '!' && this.RightChar(3) != '=')
						{
							throw this.MakeException(global::SR.GetString("Alternation conditions do not capture and cannot be named."));
						}
						goto IL_0551;
					}
					if (this.CharsRight() == 0)
					{
						goto IL_055E;
					}
					c3 = (c2 = this.MoveRightGetChar());
					if (c2 != '!')
					{
						if (c2 == '=')
						{
							if (c != '\'')
							{
								this._options |= RegexOptions.RightToLeft;
								num = 30;
								goto IL_0551;
							}
							goto IL_055E;
						}
						else
						{
							this.MoveLeft();
							int num5 = -1;
							int num6 = -1;
							bool flag = false;
							if (c3 >= '0' && c3 <= '9')
							{
								num5 = this.ScanDecimal();
								if (!this.IsCaptureSlot(num5))
								{
									num5 = -1;
								}
								if (this.CharsRight() > 0 && this.RightChar() != c && this.RightChar() != '-')
								{
									throw this.MakeException(global::SR.GetString("Invalid group name: Group names must begin with a word character."));
								}
								if (num5 == 0)
								{
									throw this.MakeException(global::SR.GetString("Capture number cannot be zero."));
								}
							}
							else if (RegexCharClass.IsWordChar(c3))
							{
								string text2 = this.ScanCapname();
								if (this.IsCaptureName(text2))
								{
									num5 = this.CaptureSlotFromName(text2);
								}
								if (this.CharsRight() > 0 && this.RightChar() != c && this.RightChar() != '-')
								{
									throw this.MakeException(global::SR.GetString("Invalid group name: Group names must begin with a word character."));
								}
							}
							else
							{
								if (c3 != '-')
								{
									throw this.MakeException(global::SR.GetString("Invalid group name: Group names must begin with a word character."));
								}
								flag = true;
							}
							if ((num5 != -1 || flag) && this.CharsRight() > 0 && this.RightChar() == '-')
							{
								this.MoveRight();
								c3 = this.RightChar();
								if (c3 >= '0' && c3 <= '9')
								{
									num6 = this.ScanDecimal();
									if (!this.IsCaptureSlot(num6))
									{
										throw this.MakeException(global::SR.GetString("Reference to undefined group number {0}.", new object[] { num6 }));
									}
									if (this.CharsRight() > 0 && this.RightChar() != c)
									{
										throw this.MakeException(global::SR.GetString("Invalid group name: Group names must begin with a word character."));
									}
								}
								else
								{
									if (!RegexCharClass.IsWordChar(c3))
									{
										throw this.MakeException(global::SR.GetString("Invalid group name: Group names must begin with a word character."));
									}
									string text3 = this.ScanCapname();
									if (!this.IsCaptureName(text3))
									{
										throw this.MakeException(global::SR.GetString("Reference to undefined group name {0}.", new object[] { text3 }));
									}
									num6 = this.CaptureSlotFromName(text3);
									if (this.CharsRight() > 0 && this.RightChar() != c)
									{
										throw this.MakeException(global::SR.GetString("Invalid group name: Group names must begin with a word character."));
									}
								}
							}
							if ((num5 != -1 || num6 != -1) && this.CharsRight() > 0 && this.MoveRightGetChar() == c)
							{
								return new RegexNode(28, this._options, num5, num6);
							}
							goto IL_055E;
						}
					}
					else
					{
						if (c != '\'')
						{
							this._options |= RegexOptions.RightToLeft;
							num = 31;
							goto IL_0551;
						}
						goto IL_055E;
					}
					IL_0527:
					this.MoveLeft();
					num = 29;
					this.ScanOptions();
					if (this.CharsRight() == 0)
					{
						goto IL_055E;
					}
					if ((c3 = this.MoveRightGetChar()) == ')')
					{
						return null;
					}
					if (c3 != ':')
					{
						goto IL_055E;
					}
					IL_0551:
					return new RegexNode(num, this._options);
				}
				IL_055E:
				throw this.MakeException(global::SR.GetString("Unrecognized grouping construct."));
			}
			if (this.UseOptionN() || this._ignoreNextParen)
			{
				this._ignoreNextParen = false;
				return new RegexNode(29, this._options);
			}
			int num7 = 28;
			RegexOptions options = this._options;
			int autocap = this._autocap;
			this._autocap = autocap + 1;
			return new RegexNode(num7, options, autocap, -1);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000353F8 File Offset: 0x000335F8
		internal void ScanBlank()
		{
			if (this.UseOptionX())
			{
				for (;;)
				{
					if (this.CharsRight() <= 0 || !RegexParser.IsSpace(this.RightChar()))
					{
						if (this.CharsRight() == 0)
						{
							return;
						}
						if (this.RightChar() == '#')
						{
							while (this.CharsRight() > 0)
							{
								if (this.RightChar() == '\n')
								{
									break;
								}
								this.MoveRight();
							}
						}
						else
						{
							if (this.CharsRight() < 3 || this.RightChar(2) != '#' || this.RightChar(1) != '?' || this.RightChar() != '(')
							{
								return;
							}
							while (this.CharsRight() > 0 && this.RightChar() != ')')
							{
								this.MoveRight();
							}
							if (this.CharsRight() == 0)
							{
								break;
							}
							this.MoveRight();
						}
					}
					else
					{
						this.MoveRight();
					}
				}
				throw this.MakeException(global::SR.GetString("Unterminated (?#...) comment."));
			}
			while (this.CharsRight() >= 3 && this.RightChar(2) == '#' && this.RightChar(1) == '?' && this.RightChar() == '(')
			{
				while (this.CharsRight() > 0 && this.RightChar() != ')')
				{
					this.MoveRight();
				}
				if (this.CharsRight() == 0)
				{
					throw this.MakeException(global::SR.GetString("Unterminated (?#...) comment."));
				}
				this.MoveRight();
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00035538 File Offset: 0x00033738
		internal RegexNode ScanBackslash()
		{
			if (this.CharsRight() == 0)
			{
				throw this.MakeException(global::SR.GetString("Illegal \\ at end of pattern."));
			}
			char c2;
			char c = (c2 = this.RightChar());
			if (c2 <= 'Z')
			{
				if (c2 <= 'P')
				{
					switch (c2)
					{
					case 'A':
					case 'B':
					case 'G':
						break;
					case 'C':
					case 'E':
					case 'F':
						goto IL_0251;
					case 'D':
						this.MoveRight();
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\u0001\u0002\00:");
						}
						return new RegexNode(11, this._options, RegexCharClass.NotDigitClass);
					default:
						if (c2 != 'P')
						{
							goto IL_0251;
						}
						goto IL_01FD;
					}
				}
				else if (c2 != 'S')
				{
					if (c2 != 'W')
					{
						if (c2 != 'Z')
						{
							goto IL_0251;
						}
					}
					else
					{
						this.MoveRight();
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\u0001\n\00:A[_`a{İı");
						}
						return new RegexNode(11, this._options, RegexCharClass.NotWordClass);
					}
				}
				else
				{
					this.MoveRight();
					if (this.UseOptionE())
					{
						return new RegexNode(11, this._options, "\u0001\u0004\0\t\u000e !");
					}
					return new RegexNode(11, this._options, RegexCharClass.NotSpaceClass);
				}
			}
			else if (c2 <= 'p')
			{
				if (c2 != 'b')
				{
					if (c2 != 'd')
					{
						if (c2 != 'p')
						{
							goto IL_0251;
						}
						goto IL_01FD;
					}
					else
					{
						this.MoveRight();
						if (this.UseOptionE())
						{
							return new RegexNode(11, this._options, "\0\u0002\00:");
						}
						return new RegexNode(11, this._options, RegexCharClass.DigitClass);
					}
				}
			}
			else if (c2 != 's')
			{
				if (c2 != 'w')
				{
					if (c2 != 'z')
					{
						goto IL_0251;
					}
				}
				else
				{
					this.MoveRight();
					if (this.UseOptionE())
					{
						return new RegexNode(11, this._options, "\0\n\00:A[_`a{İı");
					}
					return new RegexNode(11, this._options, RegexCharClass.WordClass);
				}
			}
			else
			{
				this.MoveRight();
				if (this.UseOptionE())
				{
					return new RegexNode(11, this._options, "\0\u0004\0\t\u000e !");
				}
				return new RegexNode(11, this._options, RegexCharClass.SpaceClass);
			}
			this.MoveRight();
			return new RegexNode(this.TypeFromCode(c), this._options);
			IL_01FD:
			this.MoveRight();
			RegexCharClass regexCharClass = new RegexCharClass();
			regexCharClass.AddCategoryFromName(this.ParseProperty(), c != 'p', this.UseOptionI(), this._pattern);
			if (this.UseOptionI())
			{
				regexCharClass.AddLowercase(this._culture);
			}
			return new RegexNode(11, this._options, regexCharClass.ToStringClass());
			IL_0251:
			return this.ScanBasicBackslash();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0003579C File Offset: 0x0003399C
		internal RegexNode ScanBasicBackslash()
		{
			if (this.CharsRight() == 0)
			{
				throw this.MakeException(global::SR.GetString("Illegal \\ at end of pattern."));
			}
			bool flag = false;
			char c = '\0';
			int num = this.Textpos();
			char c2 = this.RightChar();
			if (c2 == 'k')
			{
				if (this.CharsRight() >= 2)
				{
					this.MoveRight();
					c2 = this.MoveRightGetChar();
					if (c2 == '<' || c2 == '\'')
					{
						flag = true;
						c = ((c2 == '\'') ? '\'' : '>');
					}
				}
				if (!flag || this.CharsRight() <= 0)
				{
					throw this.MakeException(global::SR.GetString("Malformed \\k<...> named back reference."));
				}
				c2 = this.RightChar();
			}
			else if ((c2 == '<' || c2 == '\'') && this.CharsRight() > 1)
			{
				flag = true;
				c = ((c2 == '\'') ? '\'' : '>');
				this.MoveRight();
				c2 = this.RightChar();
			}
			if (flag && c2 >= '0' && c2 <= '9')
			{
				int num2 = this.ScanDecimal();
				if (this.CharsRight() > 0 && this.MoveRightGetChar() == c)
				{
					if (this.IsCaptureSlot(num2))
					{
						return new RegexNode(13, this._options, num2);
					}
					throw this.MakeException(global::SR.GetString("Reference to undefined group number {0}.", new object[] { num2.ToString(CultureInfo.CurrentCulture) }));
				}
			}
			else if (!flag && c2 >= '1' && c2 <= '9')
			{
				if (this.UseOptionE())
				{
					int num3 = -1;
					int i = (int)(c2 - '0');
					int num4 = this.Textpos() - 1;
					while (i <= this._captop)
					{
						if (this.IsCaptureSlot(i) && (this._caps == null || (int)this._caps[i] < num4))
						{
							num3 = i;
						}
						this.MoveRight();
						if (this.CharsRight() == 0 || (c2 = this.RightChar()) < '0' || c2 > '9')
						{
							break;
						}
						i = i * 10 + (int)(c2 - '0');
					}
					if (num3 >= 0)
					{
						return new RegexNode(13, this._options, num3);
					}
				}
				else
				{
					int num5 = this.ScanDecimal();
					if (this.IsCaptureSlot(num5))
					{
						return new RegexNode(13, this._options, num5);
					}
					if (num5 <= 9)
					{
						throw this.MakeException(global::SR.GetString("Reference to undefined group number {0}.", new object[] { num5.ToString(CultureInfo.CurrentCulture) }));
					}
				}
			}
			else if (flag && RegexCharClass.IsWordChar(c2))
			{
				string text = this.ScanCapname();
				if (this.CharsRight() > 0 && this.MoveRightGetChar() == c)
				{
					if (this.IsCaptureName(text))
					{
						return new RegexNode(13, this._options, this.CaptureSlotFromName(text));
					}
					throw this.MakeException(global::SR.GetString("Reference to undefined group name {0}.", new object[] { text }));
				}
			}
			this.Textto(num);
			c2 = this.ScanCharEscape();
			if (this.UseOptionI())
			{
				c2 = char.ToLower(c2, this._culture);
			}
			return new RegexNode(9, this._options, c2);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00035A60 File Offset: 0x00033C60
		internal RegexNode ScanDollar()
		{
			if (this.CharsRight() == 0)
			{
				return new RegexNode(9, this._options, '$');
			}
			char c = this.RightChar();
			int num = this.Textpos();
			int num2 = num;
			bool flag;
			if (c == '{' && this.CharsRight() > 1)
			{
				flag = true;
				this.MoveRight();
				c = this.RightChar();
			}
			else
			{
				flag = false;
			}
			if (c >= '0' && c <= '9')
			{
				if (!flag && this.UseOptionE())
				{
					int num3 = -1;
					int num4 = (int)(c - '0');
					this.MoveRight();
					if (this.IsCaptureSlot(num4))
					{
						num3 = num4;
						num2 = this.Textpos();
					}
					while (this.CharsRight() > 0 && (c = this.RightChar()) >= '0' && c <= '9')
					{
						int num5 = (int)(c - '0');
						if (num4 > 214748364 || (num4 == 214748364 && num5 > 7))
						{
							throw this.MakeException(global::SR.GetString("Capture group numbers must be less than or equal to Int32.MaxValue."));
						}
						num4 = num4 * 10 + num5;
						this.MoveRight();
						if (this.IsCaptureSlot(num4))
						{
							num3 = num4;
							num2 = this.Textpos();
						}
					}
					this.Textto(num2);
					if (num3 >= 0)
					{
						return new RegexNode(13, this._options, num3);
					}
				}
				else
				{
					int num6 = this.ScanDecimal();
					if ((!flag || (this.CharsRight() > 0 && this.MoveRightGetChar() == '}')) && this.IsCaptureSlot(num6))
					{
						return new RegexNode(13, this._options, num6);
					}
				}
			}
			else if (flag && RegexCharClass.IsWordChar(c))
			{
				string text = this.ScanCapname();
				if (this.CharsRight() > 0 && this.MoveRightGetChar() == '}' && this.IsCaptureName(text))
				{
					return new RegexNode(13, this._options, this.CaptureSlotFromName(text));
				}
			}
			else if (!flag)
			{
				int num7 = 1;
				if (c <= '+')
				{
					switch (c)
					{
					case '$':
						this.MoveRight();
						return new RegexNode(9, this._options, '$');
					case '%':
						break;
					case '&':
						num7 = 0;
						break;
					case '\'':
						num7 = -2;
						break;
					default:
						if (c == '+')
						{
							num7 = -3;
						}
						break;
					}
				}
				else if (c != '_')
				{
					if (c == '`')
					{
						num7 = -1;
					}
				}
				else
				{
					num7 = -4;
				}
				if (num7 != 1)
				{
					this.MoveRight();
					return new RegexNode(13, this._options, num7);
				}
			}
			this.Textto(num);
			return new RegexNode(9, this._options, '$');
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00035CB4 File Offset: 0x00033EB4
		internal string ScanCapname()
		{
			int num = this.Textpos();
			while (this.CharsRight() > 0)
			{
				if (!RegexCharClass.IsWordChar(this.MoveRightGetChar()))
				{
					this.MoveLeft();
					break;
				}
			}
			return this._pattern.Substring(num, this.Textpos() - num);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00035CFC File Offset: 0x00033EFC
		internal char ScanOctal()
		{
			int num = 3;
			if (num > this.CharsRight())
			{
				num = this.CharsRight();
			}
			int num2 = 0;
			int num3;
			while (num > 0 && (num3 = (int)(this.RightChar() - '0')) <= 7)
			{
				this.MoveRight();
				num2 *= 8;
				num2 += num3;
				if (this.UseOptionE() && num2 >= 32)
				{
					break;
				}
				num--;
			}
			num2 &= 255;
			return (char)num2;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00035D5C File Offset: 0x00033F5C
		internal int ScanDecimal()
		{
			int num = 0;
			int num2;
			while (this.CharsRight() > 0 && (num2 = (int)((ushort)(this.RightChar() - '0'))) <= 9)
			{
				this.MoveRight();
				if (num > 214748364 || (num == 214748364 && num2 > 7))
				{
					throw this.MakeException(global::SR.GetString("Capture group numbers must be less than or equal to Int32.MaxValue."));
				}
				num *= 10;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00035DBC File Offset: 0x00033FBC
		internal char ScanHex(int c)
		{
			int num = 0;
			if (this.CharsRight() >= c)
			{
				int num2;
				while (c > 0 && (num2 = RegexParser.HexDigit(this.MoveRightGetChar())) >= 0)
				{
					num *= 16;
					num += num2;
					c--;
				}
			}
			if (c > 0)
			{
				throw this.MakeException(global::SR.GetString("Insufficient hexadecimal digits."));
			}
			return (char)num;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00035E10 File Offset: 0x00034010
		internal static int HexDigit(char ch)
		{
			int num;
			if ((num = (int)(ch - '0')) <= 9)
			{
				return num;
			}
			if ((num = (int)(ch - 'a')) <= 5)
			{
				return num + 10;
			}
			if ((num = (int)(ch - 'A')) <= 5)
			{
				return num + 10;
			}
			return -1;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00035E48 File Offset: 0x00034048
		internal char ScanControl()
		{
			if (this.CharsRight() <= 0)
			{
				throw this.MakeException(global::SR.GetString("Missing control character."));
			}
			char c = this.MoveRightGetChar();
			if (c >= 'a' && c <= 'z')
			{
				c -= ' ';
			}
			if ((c -= '@') < ' ')
			{
				return c;
			}
			throw this.MakeException(global::SR.GetString("Unrecognized control character."));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00035EA3 File Offset: 0x000340A3
		internal bool IsOnlyTopOption(RegexOptions option)
		{
			return option == RegexOptions.RightToLeft || option == RegexOptions.Compiled || option == RegexOptions.CultureInvariant || option == RegexOptions.ECMAScript;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00035EC0 File Offset: 0x000340C0
		internal void ScanOptions()
		{
			bool flag = false;
			while (this.CharsRight() > 0)
			{
				char c = this.RightChar();
				if (c == '-')
				{
					flag = true;
				}
				else if (c == '+')
				{
					flag = false;
				}
				else
				{
					RegexOptions regexOptions = RegexParser.OptionFromCode(c);
					if (regexOptions == RegexOptions.None || this.IsOnlyTopOption(regexOptions))
					{
						return;
					}
					if (flag)
					{
						this._options &= ~regexOptions;
					}
					else
					{
						this._options |= regexOptions;
					}
				}
				this.MoveRight();
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00035F30 File Offset: 0x00034130
		internal char ScanCharEscape()
		{
			char c = this.MoveRightGetChar();
			if (c >= '0' && c <= '7')
			{
				this.MoveLeft();
				return this.ScanOctal();
			}
			switch (c)
			{
			case 'a':
				return '\a';
			case 'b':
				return '\b';
			case 'c':
				return this.ScanControl();
			case 'd':
				break;
			case 'e':
				return '\u001b';
			case 'f':
				return '\f';
			default:
				switch (c)
				{
				case 'n':
					return '\n';
				case 'r':
					return '\r';
				case 't':
					return '\t';
				case 'u':
					return this.ScanHex(4);
				case 'v':
					return '\v';
				case 'x':
					return this.ScanHex(2);
				}
				break;
			}
			if (!this.UseOptionE() && RegexCharClass.IsWordChar(c))
			{
				throw this.MakeException(global::SR.GetString("Unrecognized escape sequence \\{0}.", new object[] { c.ToString() }));
			}
			return c;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00036014 File Offset: 0x00034214
		internal string ParseProperty()
		{
			if (this.CharsRight() < 3)
			{
				throw this.MakeException(global::SR.GetString("Incomplete \\p{X} character escape."));
			}
			char c = this.MoveRightGetChar();
			if (c != '{')
			{
				throw this.MakeException(global::SR.GetString("Malformed \\p{X} character escape."));
			}
			int num = this.Textpos();
			while (this.CharsRight() > 0)
			{
				c = this.MoveRightGetChar();
				if (!RegexCharClass.IsWordChar(c) && c != '-')
				{
					this.MoveLeft();
					break;
				}
			}
			string text = this._pattern.Substring(num, this.Textpos() - num);
			if (this.CharsRight() == 0 || this.MoveRightGetChar() != '}')
			{
				throw this.MakeException(global::SR.GetString("Incomplete \\p{X} character escape."));
			}
			return text;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000360C0 File Offset: 0x000342C0
		internal int TypeFromCode(char ch)
		{
			if (ch <= 'G')
			{
				if (ch == 'A')
				{
					return 18;
				}
				if (ch != 'B')
				{
					if (ch == 'G')
					{
						return 19;
					}
				}
				else
				{
					if (!this.UseOptionE())
					{
						return 17;
					}
					return 42;
				}
			}
			else
			{
				if (ch == 'Z')
				{
					return 20;
				}
				if (ch != 'b')
				{
					if (ch == 'z')
					{
						return 21;
					}
				}
				else
				{
					if (!this.UseOptionE())
					{
						return 16;
					}
					return 41;
				}
			}
			return 22;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00036120 File Offset: 0x00034320
		internal static RegexOptions OptionFromCode(char ch)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				ch += ' ';
			}
			if (ch <= 'e')
			{
				if (ch == 'c')
				{
					return RegexOptions.Compiled;
				}
				if (ch == 'e')
				{
					return RegexOptions.ECMAScript;
				}
			}
			else
			{
				if (ch == 'i')
				{
					return RegexOptions.IgnoreCase;
				}
				switch (ch)
				{
				case 'm':
					return RegexOptions.Multiline;
				case 'n':
					return RegexOptions.ExplicitCapture;
				case 'o':
				case 'p':
				case 'q':
					break;
				case 'r':
					return RegexOptions.RightToLeft;
				case 's':
					return RegexOptions.Singleline;
				default:
					if (ch == 'x')
					{
						return RegexOptions.IgnorePatternWhitespace;
					}
					break;
				}
			}
			return RegexOptions.None;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00036198 File Offset: 0x00034398
		internal void CountCaptures()
		{
			this.NoteCaptureSlot(0, 0);
			this._autocap = 1;
			while (this.CharsRight() > 0)
			{
				int num = this.Textpos();
				char c = this.MoveRightGetChar();
				if (c <= '(')
				{
					if (c != '#')
					{
						if (c == '(')
						{
							if (this.CharsRight() >= 2 && this.RightChar(1) == '#' && this.RightChar() == '?')
							{
								this.MoveLeft();
								this.ScanBlank();
							}
							else
							{
								this.PushOptions();
								if (this.CharsRight() > 0 && this.RightChar() == '?')
								{
									this.MoveRight();
									if (this.CharsRight() > 1 && (this.RightChar() == '<' || this.RightChar() == '\''))
									{
										this.MoveRight();
										c = this.RightChar();
										if (c != '0' && RegexCharClass.IsWordChar(c))
										{
											if (c >= '1' && c <= '9')
											{
												this.NoteCaptureSlot(this.ScanDecimal(), num);
											}
											else
											{
												this.NoteCaptureName(this.ScanCapname(), num);
											}
										}
									}
									else
									{
										this.ScanOptions();
										if (this.CharsRight() > 0)
										{
											if (this.RightChar() == ')')
											{
												this.MoveRight();
												this.PopKeepOptions();
											}
											else if (this.RightChar() == '(')
											{
												this._ignoreNextParen = true;
												continue;
											}
										}
									}
								}
								else if (!this.UseOptionN() && !this._ignoreNextParen)
								{
									int autocap = this._autocap;
									this._autocap = autocap + 1;
									this.NoteCaptureSlot(autocap, num);
								}
							}
							this._ignoreNextParen = false;
						}
					}
					else if (this.UseOptionX())
					{
						this.MoveLeft();
						this.ScanBlank();
					}
				}
				else if (c != ')')
				{
					if (c != '[')
					{
						if (c == '\\' && this.CharsRight() > 0)
						{
							this.MoveRight();
						}
					}
					else
					{
						this.ScanCharClass(false, true);
					}
				}
				else if (!this.EmptyOptionsStack())
				{
					this.PopOptions();
				}
			}
			this.AssignNameSlots();
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00036378 File Offset: 0x00034578
		internal void NoteCaptureSlot(int i, int pos)
		{
			if (!this._caps.ContainsKey(i))
			{
				this._caps.Add(i, pos);
				this._capcount++;
				if (this._captop <= i)
				{
					if (i == 2147483647)
					{
						this._captop = i;
						return;
					}
					this._captop = i + 1;
				}
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x000363E0 File Offset: 0x000345E0
		internal void NoteCaptureName(string name, int pos)
		{
			if (this._capnames == null)
			{
				this._capnames = new Hashtable();
				this._capnamelist = new List<string>();
			}
			if (!this._capnames.ContainsKey(name))
			{
				this._capnames.Add(name, pos);
				this._capnamelist.Add(name);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00036437 File Offset: 0x00034637
		internal void NoteCaptures(Hashtable caps, int capsize, Hashtable capnames)
		{
			this._caps = caps;
			this._capsize = capsize;
			this._capnames = capnames;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00036450 File Offset: 0x00034650
		internal void AssignNameSlots()
		{
			if (this._capnames != null)
			{
				for (int i = 0; i < this._capnamelist.Count; i++)
				{
					while (this.IsCaptureSlot(this._autocap))
					{
						this._autocap++;
					}
					string text = this._capnamelist[i];
					int num = (int)this._capnames[text];
					this._capnames[text] = this._autocap;
					this.NoteCaptureSlot(this._autocap, num);
					this._autocap++;
				}
			}
			if (this._capcount < this._captop)
			{
				this._capnumlist = new int[this._capcount];
				int num2 = 0;
				IDictionaryEnumerator enumerator = this._caps.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this._capnumlist[num2++] = (int)enumerator.Key;
				}
				Array.Sort<int>(this._capnumlist, Comparer<int>.Default);
			}
			if (this._capnames != null || this._capnumlist != null)
			{
				int num3 = 0;
				List<string> list;
				int num4;
				if (this._capnames == null)
				{
					list = null;
					this._capnames = new Hashtable();
					this._capnamelist = new List<string>();
					num4 = -1;
				}
				else
				{
					list = this._capnamelist;
					this._capnamelist = new List<string>();
					num4 = (int)this._capnames[list[0]];
				}
				for (int j = 0; j < this._capcount; j++)
				{
					int num5 = ((this._capnumlist == null) ? j : this._capnumlist[j]);
					if (num4 == num5)
					{
						this._capnamelist.Add(list[num3++]);
						num4 = ((num3 == list.Count) ? (-1) : ((int)this._capnames[list[num3]]));
					}
					else
					{
						string text2 = Convert.ToString(num5, this._culture);
						this._capnamelist.Add(text2);
						this._capnames[text2] = num5;
					}
				}
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00036661 File Offset: 0x00034861
		internal int CaptureSlotFromName(string capname)
		{
			return (int)this._capnames[capname];
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00036674 File Offset: 0x00034874
		internal bool IsCaptureSlot(int i)
		{
			if (this._caps != null)
			{
				return this._caps.ContainsKey(i);
			}
			return i >= 0 && i < this._capsize;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0003669F File Offset: 0x0003489F
		internal bool IsCaptureName(string capname)
		{
			return this._capnames != null && this._capnames.ContainsKey(capname);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000366B7 File Offset: 0x000348B7
		internal bool UseOptionN()
		{
			return (this._options & RegexOptions.ExplicitCapture) > RegexOptions.None;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000366C4 File Offset: 0x000348C4
		internal bool UseOptionI()
		{
			return (this._options & RegexOptions.IgnoreCase) > RegexOptions.None;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000366D1 File Offset: 0x000348D1
		internal bool UseOptionM()
		{
			return (this._options & RegexOptions.Multiline) > RegexOptions.None;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000366DE File Offset: 0x000348DE
		internal bool UseOptionS()
		{
			return (this._options & RegexOptions.Singleline) > RegexOptions.None;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000366EC File Offset: 0x000348EC
		internal bool UseOptionX()
		{
			return (this._options & RegexOptions.IgnorePatternWhitespace) > RegexOptions.None;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000366FA File Offset: 0x000348FA
		internal bool UseOptionE()
		{
			return (this._options & RegexOptions.ECMAScript) > RegexOptions.None;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0003670B File Offset: 0x0003490B
		internal static bool IsSpecial(char ch)
		{
			return ch <= '|' && RegexParser._category[(int)ch] >= 4;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00036721 File Offset: 0x00034921
		internal static bool IsStopperX(char ch)
		{
			return ch <= '|' && RegexParser._category[(int)ch] >= 2;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00036737 File Offset: 0x00034937
		internal static bool IsQuantifier(char ch)
		{
			return ch <= '{' && RegexParser._category[(int)ch] >= 5;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00036750 File Offset: 0x00034950
		internal bool IsTrueQuantifier()
		{
			int num = this.CharsRight();
			if (num == 0)
			{
				return false;
			}
			int num2 = this.Textpos();
			char c = this.CharAt(num2);
			if (c != '{')
			{
				return c <= '{' && RegexParser._category[(int)c] >= 5;
			}
			int num3 = num2;
			while (--num > 0 && (c = this.CharAt(++num3)) >= '0' && c <= '9')
			{
			}
			if (num == 0 || num3 - num2 == 1)
			{
				return false;
			}
			if (c == '}')
			{
				return true;
			}
			if (c != ',')
			{
				return false;
			}
			while (--num > 0 && (c = this.CharAt(++num3)) >= '0' && c <= '9')
			{
			}
			return num > 0 && c == '}';
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000367F4 File Offset: 0x000349F4
		internal static bool IsSpace(char ch)
		{
			return ch <= ' ' && RegexParser._category[(int)ch] == 2;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00036807 File Offset: 0x00034A07
		internal static bool IsMetachar(char ch)
		{
			return ch <= '|' && RegexParser._category[(int)ch] >= 1;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00036820 File Offset: 0x00034A20
		internal void AddConcatenate(int pos, int cch, bool isReplacement)
		{
			if (cch == 0)
			{
				return;
			}
			RegexNode regexNode;
			if (cch > 1)
			{
				string text = this._pattern.Substring(pos, cch);
				if (this.UseOptionI() && !isReplacement)
				{
					StringBuilder stringBuilder = new StringBuilder(text.Length);
					for (int i = 0; i < text.Length; i++)
					{
						stringBuilder.Append(char.ToLower(text[i], this._culture));
					}
					text = stringBuilder.ToString();
				}
				regexNode = new RegexNode(12, this._options, text);
			}
			else
			{
				char c = this._pattern[pos];
				if (this.UseOptionI() && !isReplacement)
				{
					c = char.ToLower(c, this._culture);
				}
				regexNode = new RegexNode(9, this._options, c);
			}
			this._concatenation.AddChild(regexNode);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000368E0 File Offset: 0x00034AE0
		internal void PushGroup()
		{
			this._group._next = this._stack;
			this._alternation._next = this._group;
			this._concatenation._next = this._alternation;
			this._stack = this._concatenation;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0003692C File Offset: 0x00034B2C
		internal void PopGroup()
		{
			this._concatenation = this._stack;
			this._alternation = this._concatenation._next;
			this._group = this._alternation._next;
			this._stack = this._group._next;
			if (this._group.Type() == 34 && this._group.ChildCount() == 0)
			{
				if (this._unit == null)
				{
					throw this.MakeException(global::SR.GetString("Illegal conditional (?(...)) expression."));
				}
				this._group.AddChild(this._unit);
				this._unit = null;
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000369C5 File Offset: 0x00034BC5
		internal bool EmptyStack()
		{
			return this._stack == null;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000369D0 File Offset: 0x00034BD0
		internal void StartGroup(RegexNode openGroup)
		{
			this._group = openGroup;
			this._alternation = new RegexNode(24, this._options);
			this._concatenation = new RegexNode(25, this._options);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00036A00 File Offset: 0x00034C00
		internal void AddAlternate()
		{
			if (this._group.Type() == 34 || this._group.Type() == 33)
			{
				this._group.AddChild(this._concatenation.ReverseLeft());
			}
			else
			{
				this._alternation.AddChild(this._concatenation.ReverseLeft());
			}
			this._concatenation = new RegexNode(25, this._options);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00036A6C File Offset: 0x00034C6C
		internal void AddConcatenate()
		{
			this._concatenation.AddChild(this._unit);
			this._unit = null;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00036A86 File Offset: 0x00034C86
		internal void AddConcatenate(bool lazy, int min, int max)
		{
			this._concatenation.AddChild(this._unit.MakeQuantifier(lazy, min, max));
			this._unit = null;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00036AA8 File Offset: 0x00034CA8
		internal RegexNode Unit()
		{
			return this._unit;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00036AB0 File Offset: 0x00034CB0
		internal void AddUnitOne(char ch)
		{
			if (this.UseOptionI())
			{
				ch = char.ToLower(ch, this._culture);
			}
			this._unit = new RegexNode(9, this._options, ch);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00036ADC File Offset: 0x00034CDC
		internal void AddUnitNotone(char ch)
		{
			if (this.UseOptionI())
			{
				ch = char.ToLower(ch, this._culture);
			}
			this._unit = new RegexNode(10, this._options, ch);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00036B08 File Offset: 0x00034D08
		internal void AddUnitSet(string cc)
		{
			this._unit = new RegexNode(11, this._options, cc);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00036B1E File Offset: 0x00034D1E
		internal void AddUnitNode(RegexNode node)
		{
			this._unit = node;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00036B27 File Offset: 0x00034D27
		internal void AddUnitType(int type)
		{
			this._unit = new RegexNode(type, this._options);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00036B3C File Offset: 0x00034D3C
		internal void AddGroup()
		{
			if (this._group.Type() == 34 || this._group.Type() == 33)
			{
				this._group.AddChild(this._concatenation.ReverseLeft());
				if ((this._group.Type() == 33 && this._group.ChildCount() > 2) || this._group.ChildCount() > 3)
				{
					throw this.MakeException(global::SR.GetString("Too many | in (?()|)."));
				}
			}
			else
			{
				this._alternation.AddChild(this._concatenation.ReverseLeft());
				this._group.AddChild(this._alternation);
			}
			this._unit = this._group;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00036BEC File Offset: 0x00034DEC
		internal void PushOptions()
		{
			this._optionsStack.Add(this._options);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00036BFF File Offset: 0x00034DFF
		internal void PopOptions()
		{
			this._options = this._optionsStack[this._optionsStack.Count - 1];
			this._optionsStack.RemoveAt(this._optionsStack.Count - 1);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00036C37 File Offset: 0x00034E37
		internal bool EmptyOptionsStack()
		{
			return this._optionsStack.Count == 0;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00036C47 File Offset: 0x00034E47
		internal void PopKeepOptions()
		{
			this._optionsStack.RemoveAt(this._optionsStack.Count - 1);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00036C61 File Offset: 0x00034E61
		internal ArgumentException MakeException(string message)
		{
			return new ArgumentException(global::SR.GetString("parsing \"{0}\" - {1}", new object[] { this._pattern, message }));
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00036C85 File Offset: 0x00034E85
		internal int Textpos()
		{
			return this._currentPos;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00036C8D File Offset: 0x00034E8D
		internal void Textto(int pos)
		{
			this._currentPos = pos;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00036C98 File Offset: 0x00034E98
		internal char MoveRightGetChar()
		{
			string pattern = this._pattern;
			int currentPos = this._currentPos;
			this._currentPos = currentPos + 1;
			return pattern[currentPos];
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00036CC1 File Offset: 0x00034EC1
		internal void MoveRight()
		{
			this.MoveRight(1);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00036CCA File Offset: 0x00034ECA
		internal void MoveRight(int i)
		{
			this._currentPos += i;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00036CDA File Offset: 0x00034EDA
		internal void MoveLeft()
		{
			this._currentPos--;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00036CEA File Offset: 0x00034EEA
		internal char CharAt(int i)
		{
			return this._pattern[i];
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00036CF8 File Offset: 0x00034EF8
		internal char RightChar()
		{
			return this._pattern[this._currentPos];
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00036D0B File Offset: 0x00034F0B
		internal char RightChar(int i)
		{
			return this._pattern[this._currentPos + i];
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00036D20 File Offset: 0x00034F20
		internal int CharsRight()
		{
			return this._pattern.Length - this._currentPos;
		}

		// Token: 0x04000F1C RID: 3868
		internal RegexNode _stack;

		// Token: 0x04000F1D RID: 3869
		internal RegexNode _group;

		// Token: 0x04000F1E RID: 3870
		internal RegexNode _alternation;

		// Token: 0x04000F1F RID: 3871
		internal RegexNode _concatenation;

		// Token: 0x04000F20 RID: 3872
		internal RegexNode _unit;

		// Token: 0x04000F21 RID: 3873
		internal string _pattern;

		// Token: 0x04000F22 RID: 3874
		internal int _currentPos;

		// Token: 0x04000F23 RID: 3875
		internal CultureInfo _culture;

		// Token: 0x04000F24 RID: 3876
		internal int _autocap;

		// Token: 0x04000F25 RID: 3877
		internal int _capcount;

		// Token: 0x04000F26 RID: 3878
		internal int _captop;

		// Token: 0x04000F27 RID: 3879
		internal int _capsize;

		// Token: 0x04000F28 RID: 3880
		internal Hashtable _caps;

		// Token: 0x04000F29 RID: 3881
		internal Hashtable _capnames;

		// Token: 0x04000F2A RID: 3882
		internal int[] _capnumlist;

		// Token: 0x04000F2B RID: 3883
		internal List<string> _capnamelist;

		// Token: 0x04000F2C RID: 3884
		internal RegexOptions _options;

		// Token: 0x04000F2D RID: 3885
		internal List<RegexOptions> _optionsStack;

		// Token: 0x04000F2E RID: 3886
		internal bool _ignoreNextParen;

		// Token: 0x04000F2F RID: 3887
		internal const int MaxValueDiv10 = 214748364;

		// Token: 0x04000F30 RID: 3888
		internal const int MaxValueMod10 = 7;

		// Token: 0x04000F31 RID: 3889
		internal const byte Q = 5;

		// Token: 0x04000F32 RID: 3890
		internal const byte S = 4;

		// Token: 0x04000F33 RID: 3891
		internal const byte Z = 3;

		// Token: 0x04000F34 RID: 3892
		internal const byte X = 2;

		// Token: 0x04000F35 RID: 3893
		internal const byte E = 1;

		// Token: 0x04000F36 RID: 3894
		internal static readonly byte[] _category = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 2,
			2, 0, 2, 2, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 2, 0, 0, 3, 4, 0, 0, 0,
			4, 4, 5, 5, 0, 0, 4, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 4, 4, 0, 4, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 5, 4, 0, 0, 0
		};
	}
}
