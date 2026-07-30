using System;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005C3 RID: 1475
	internal sealed class XPathScanner
	{
		// Token: 0x06003AA9 RID: 15017 RVA: 0x0014B7A6 File Offset: 0x001499A6
		public XPathScanner(string xpathExpr)
			: this(xpathExpr, 0)
		{
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x0014B7B0 File Offset: 0x001499B0
		public XPathScanner(string xpathExpr, int startFrom)
		{
			this.xpathExpr = xpathExpr;
			this.kind = LexKind.Unknown;
			this.SetSourceIndex(startFrom);
			this.NextLex();
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x0014B7DE File Offset: 0x001499DE
		public string Source
		{
			get
			{
				return this.xpathExpr;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x0014B7E6 File Offset: 0x001499E6
		public LexKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x0014B7EE File Offset: 0x001499EE
		public int LexStart
		{
			get
			{
				return this.lexStart;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x0014B7F6 File Offset: 0x001499F6
		public int LexSize
		{
			get
			{
				return this.curIndex - this.lexStart;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x0014B805 File Offset: 0x00149A05
		public int PrevLexEnd
		{
			get
			{
				return this.prevLexEnd;
			}
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x0014B80D File Offset: 0x00149A0D
		private void SetSourceIndex(int index)
		{
			this.curIndex = index - 1;
			this.NextChar();
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x0014B820 File Offset: 0x00149A20
		private void NextChar()
		{
			this.curIndex++;
			if (this.curIndex < this.xpathExpr.Length)
			{
				this.curChar = this.xpathExpr[this.curIndex];
				return;
			}
			this.curChar = '\0';
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x0014B86D File Offset: 0x00149A6D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x0014B875 File Offset: 0x00149A75
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x0014B87D File Offset: 0x00149A7D
		public string RawValue
		{
			get
			{
				if (this.kind == LexKind.Eof)
				{
					return this.LexKindToString(this.kind);
				}
				return this.xpathExpr.Substring(this.lexStart, this.curIndex - this.lexStart);
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x0014B8B4 File Offset: 0x00149AB4
		public string StringValue
		{
			get
			{
				return this.stringValue;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x0014B8BC File Offset: 0x00149ABC
		public bool CanBeFunction
		{
			get
			{
				return this.canBeFunction;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x0014B8C4 File Offset: 0x00149AC4
		public XPathAxis Axis
		{
			get
			{
				return this.axis;
			}
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x0014B8CC File Offset: 0x00149ACC
		private void SkipSpace()
		{
			while (this.xmlCharType.IsWhiteSpace(this.curChar))
			{
				this.NextChar();
			}
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x0011D05B File Offset: 0x0011B25B
		private static bool IsAsciiDigit(char ch)
		{
			return ch - '0' <= '\t';
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x0014B8EC File Offset: 0x00149AEC
		public void NextLex()
		{
			this.prevLexEnd = this.curIndex;
			this.prevKind = this.kind;
			this.SkipSpace();
			this.lexStart = this.curIndex;
			char c = this.curChar;
			if (c <= '[')
			{
				if (c != '\0')
				{
					switch (c)
					{
					case '!':
						this.NextChar();
						if (this.curChar == '=')
						{
							this.kind = LexKind.Ne;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Unknown;
						return;
					case '"':
					case '\'':
						this.kind = LexKind.String;
						this.ScanString();
						return;
					case '#':
					case '%':
					case '&':
					case ';':
					case '?':
						goto IL_027C;
					case '$':
					case '(':
					case ')':
					case ',':
					case '@':
						goto IL_00F2;
					case '*':
						this.kind = LexKind.Star;
						this.NextChar();
						this.CheckOperator(true);
						return;
					case '+':
						this.kind = LexKind.Plus;
						this.NextChar();
						return;
					case '-':
						this.kind = LexKind.Minus;
						this.NextChar();
						return;
					case '.':
						this.NextChar();
						if (this.curChar == '.')
						{
							this.kind = LexKind.DotDot;
							this.NextChar();
							return;
						}
						if (!XPathScanner.IsAsciiDigit(this.curChar))
						{
							this.kind = LexKind.Dot;
							return;
						}
						this.SetSourceIndex(this.lexStart);
						break;
					case '/':
						this.NextChar();
						if (this.curChar == '/')
						{
							this.kind = LexKind.SlashSlash;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Slash;
						return;
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						break;
					case ':':
						this.NextChar();
						if (this.curChar == ':')
						{
							this.kind = LexKind.ColonColon;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Unknown;
						return;
					case '<':
						this.NextChar();
						if (this.curChar == '=')
						{
							this.kind = LexKind.Le;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Lt;
						return;
					case '=':
						this.kind = LexKind.Eq;
						this.NextChar();
						return;
					case '>':
						this.NextChar();
						if (this.curChar == '=')
						{
							this.kind = LexKind.Ge;
							this.NextChar();
							return;
						}
						this.kind = LexKind.Gt;
						return;
					default:
						if (c != '[')
						{
							goto IL_027C;
						}
						goto IL_00F2;
					}
					this.kind = LexKind.Number;
					this.ScanNumber();
					return;
				}
				this.kind = LexKind.Eof;
				return;
			}
			else if (c != ']')
			{
				if (c == '|')
				{
					this.kind = LexKind.Union;
					this.NextChar();
					return;
				}
				if (c != '}')
				{
					goto IL_027C;
				}
			}
			IL_00F2:
			this.kind = (LexKind)this.curChar;
			this.NextChar();
			return;
			IL_027C:
			if (this.xmlCharType.IsStartNCNameSingleChar(this.curChar))
			{
				this.kind = LexKind.Name;
				this.name = this.ScanNCName();
				this.prefix = string.Empty;
				this.canBeFunction = false;
				this.axis = XPathAxis.Unknown;
				bool flag = false;
				int num = this.curIndex;
				if (this.curChar == ':')
				{
					this.NextChar();
					if (this.curChar == ':')
					{
						this.NextChar();
						flag = true;
						this.SetSourceIndex(num);
					}
					else if (this.curChar == '*')
					{
						this.NextChar();
						this.prefix = this.name;
						this.name = "*";
					}
					else if (this.xmlCharType.IsStartNCNameSingleChar(this.curChar))
					{
						this.prefix = this.name;
						this.name = this.ScanNCName();
						num = this.curIndex;
						this.SkipSpace();
						this.canBeFunction = this.curChar == '(';
						this.SetSourceIndex(num);
					}
					else
					{
						this.SetSourceIndex(num);
					}
				}
				else
				{
					this.SkipSpace();
					if (this.curChar == ':')
					{
						this.NextChar();
						if (this.curChar == ':')
						{
							this.NextChar();
							flag = true;
						}
						this.SetSourceIndex(num);
					}
					else
					{
						this.canBeFunction = this.curChar == '(';
					}
				}
				if (!this.CheckOperator(false) && flag)
				{
					this.axis = this.CheckAxis();
					return;
				}
			}
			else
			{
				this.kind = LexKind.Unknown;
				this.NextChar();
			}
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x0014BCE4 File Offset: 0x00149EE4
		private bool CheckOperator(bool star)
		{
			LexKind lexKind;
			if (star)
			{
				lexKind = LexKind.Multiply;
			}
			else
			{
				if (this.prefix.Length != 0 || this.name.Length > 3)
				{
					return false;
				}
				string text = this.name;
				if (!(text == "or"))
				{
					if (!(text == "and"))
					{
						if (!(text == "div"))
						{
							if (!(text == "mod"))
							{
								return false;
							}
							lexKind = LexKind.Modulo;
						}
						else
						{
							lexKind = LexKind.Divide;
						}
					}
					else
					{
						lexKind = LexKind.And;
					}
				}
				else
				{
					lexKind = LexKind.Or;
				}
			}
			if (this.prevKind <= LexKind.Union)
			{
				return false;
			}
			LexKind lexKind2 = this.prevKind;
			if (lexKind2 <= LexKind.LParens)
			{
				if (lexKind2 - LexKind.ColonColon > 1 && lexKind2 != LexKind.Dollar && lexKind2 != LexKind.LParens)
				{
					goto IL_00BE;
				}
			}
			else if (lexKind2 <= LexKind.Slash)
			{
				if (lexKind2 != LexKind.Comma && lexKind2 != LexKind.Slash)
				{
					goto IL_00BE;
				}
			}
			else if (lexKind2 != LexKind.At && lexKind2 != LexKind.LBracket)
			{
				goto IL_00BE;
			}
			return false;
			IL_00BE:
			this.kind = lexKind;
			return true;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x0014BDB8 File Offset: 0x00149FB8
		private XPathAxis CheckAxis()
		{
			this.kind = LexKind.Axis;
			string text = this.name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2535512472U)
			{
				if (num <= 1047347951U)
				{
					if (num != 21436113U)
					{
						if (num != 510973315U)
						{
							if (num == 1047347951U)
							{
								if (text == "attribute")
								{
									return XPathAxis.Attribute;
								}
							}
						}
						else if (text == "ancestor-or-self")
						{
							return XPathAxis.AncestorOrSelf;
						}
					}
					else if (text == "preceding-sibling")
					{
						return XPathAxis.PrecedingSibling;
					}
				}
				else if (num != 1683726967U)
				{
					if (num != 2452897184U)
					{
						if (num == 2535512472U)
						{
							if (text == "following")
							{
								return XPathAxis.Following;
							}
						}
					}
					else if (text == "ancestor")
					{
						return XPathAxis.Ancestor;
					}
				}
				else if (text == "self")
				{
					return XPathAxis.Self;
				}
			}
			else if (num <= 3726896370U)
			{
				if (num != 2944295921U)
				{
					if (num != 3402529440U)
					{
						if (num == 3726896370U)
						{
							if (text == "preceding")
							{
								return XPathAxis.Preceding;
							}
						}
					}
					else if (text == "namespace")
					{
						return XPathAxis.Namespace;
					}
				}
				else if (text == "descendant-or-self")
				{
					return XPathAxis.DescendantOrSelf;
				}
			}
			else if (num <= 3939368189U)
			{
				if (num != 3852476509U)
				{
					if (num == 3939368189U)
					{
						if (text == "parent")
						{
							return XPathAxis.Parent;
						}
					}
				}
				else if (text == "child")
				{
					return XPathAxis.Child;
				}
			}
			else if (num != 3998959382U)
			{
				if (num == 4042989175U)
				{
					if (text == "following-sibling")
					{
						return XPathAxis.FollowingSibling;
					}
				}
			}
			else if (text == "descendant")
			{
				return XPathAxis.Descendant;
			}
			this.kind = LexKind.Name;
			return XPathAxis.Unknown;
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x0014BFA8 File Offset: 0x0014A1A8
		private void ScanNumber()
		{
			while (XPathScanner.IsAsciiDigit(this.curChar))
			{
				this.NextChar();
			}
			if (this.curChar == '.')
			{
				this.NextChar();
				while (XPathScanner.IsAsciiDigit(this.curChar))
				{
					this.NextChar();
				}
			}
			if (((int)this.curChar & -33) == 69)
			{
				this.NextChar();
				if (this.curChar == '+' || this.curChar == '-')
				{
					this.NextChar();
				}
				while (XPathScanner.IsAsciiDigit(this.curChar))
				{
					this.NextChar();
				}
				throw this.CreateException("Scientific notation is not allowed.", Array.Empty<string>());
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0014C044 File Offset: 0x0014A244
		private void ScanString()
		{
			int num = this.curIndex + 1;
			int num2 = this.xpathExpr.IndexOf(this.curChar, num);
			if (num2 < 0)
			{
				this.SetSourceIndex(this.xpathExpr.Length);
				throw this.CreateException("String literal was not closed.", Array.Empty<string>());
			}
			this.stringValue = this.xpathExpr.Substring(num, num2 - num);
			this.SetSourceIndex(num2 + 1);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x0014C0B4 File Offset: 0x0014A2B4
		private string ScanNCName()
		{
			int num = this.curIndex;
			while (this.xmlCharType.IsNCNameSingleChar(this.curChar))
			{
				this.NextChar();
			}
			return this.xpathExpr.Substring(num, this.curIndex - num);
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x0014C0F7 File Offset: 0x0014A2F7
		public void PassToken(LexKind t)
		{
			this.CheckToken(t);
			this.NextLex();
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x0014C108 File Offset: 0x0014A308
		public void CheckToken(LexKind t)
		{
			if (this.kind == t)
			{
				return;
			}
			if (t == LexKind.Eof)
			{
				throw this.CreateException("Expected end of the expression, found '{0}'.", new string[] { this.RawValue });
			}
			throw this.CreateException("Expected token '{0}', found '{1}'.", new string[]
			{
				this.LexKindToString(t),
				this.RawValue
			});
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x0014C163 File Offset: 0x0014A363
		private string LexKindToString(LexKind t)
		{
			if (LexKind.Eof < t)
			{
				return new string((char)t, 1);
			}
			switch (t)
			{
			case LexKind.Name:
				return "<name>";
			case LexKind.String:
				return "<string literal>";
			case LexKind.Eof:
				return "<eof>";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x0014C1A1 File Offset: 0x0014A3A1
		public XPathCompileException CreateException(string resId, params string[] args)
		{
			return new XPathCompileException(this.xpathExpr, this.lexStart, this.curIndex, resId, args);
		}

		// Token: 0x04002644 RID: 9796
		private string xpathExpr;

		// Token: 0x04002645 RID: 9797
		private int curIndex;

		// Token: 0x04002646 RID: 9798
		private char curChar;

		// Token: 0x04002647 RID: 9799
		private LexKind kind;

		// Token: 0x04002648 RID: 9800
		private string name;

		// Token: 0x04002649 RID: 9801
		private string prefix;

		// Token: 0x0400264A RID: 9802
		private string stringValue;

		// Token: 0x0400264B RID: 9803
		private bool canBeFunction;

		// Token: 0x0400264C RID: 9804
		private int lexStart;

		// Token: 0x0400264D RID: 9805
		private int prevLexEnd;

		// Token: 0x0400264E RID: 9806
		private LexKind prevKind;

		// Token: 0x0400264F RID: 9807
		private XPathAxis axis;

		// Token: 0x04002650 RID: 9808
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
