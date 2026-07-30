using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000053 RID: 83
	internal sealed class XPathScanner
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00009780 File Offset: 0x00007980
		public XPathScanner(string xpathExpr)
		{
			if (xpathExpr == null)
			{
				throw XPathException.Create("'{0}' is an invalid expression.", string.Empty);
			}
			this.xpathExpr = xpathExpr;
			this.NextChar();
			this.NextLex();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000097D5 File Offset: 0x000079D5
		public string SourceText
		{
			get
			{
				return this.xpathExpr;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000097DD File Offset: 0x000079DD
		private char CurerntChar
		{
			get
			{
				return this.currentChar;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000097E8 File Offset: 0x000079E8
		private bool NextChar()
		{
			if (this.xpathExprIndex < this.xpathExpr.Length)
			{
				string text = this.xpathExpr;
				int num = this.xpathExprIndex;
				this.xpathExprIndex = num + 1;
				this.currentChar = text[num];
				return true;
			}
			this.currentChar = '\0';
			return false;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009834 File Offset: 0x00007A34
		public XPathScanner.LexKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000983C File Offset: 0x00007A3C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00009844 File Offset: 0x00007A44
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000984C File Offset: 0x00007A4C
		public string StringValue
		{
			get
			{
				return this.stringValue;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009854 File Offset: 0x00007A54
		public double NumberValue
		{
			get
			{
				return this.numberValue;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000985C File Offset: 0x00007A5C
		public bool CanBeFunction
		{
			get
			{
				return this.canBeFunction;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009864 File Offset: 0x00007A64
		private void SkipSpace()
		{
			while (this.xmlCharType.IsWhiteSpace(this.CurerntChar) && this.NextChar())
			{
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009884 File Offset: 0x00007A84
		public bool NextLex()
		{
			this.SkipSpace();
			char curerntChar = this.CurerntChar;
			if (curerntChar <= '@')
			{
				if (curerntChar == '\0')
				{
					this.kind = XPathScanner.LexKind.Eof;
					return false;
				}
				switch (curerntChar)
				{
				case '!':
					this.kind = XPathScanner.LexKind.Bang;
					this.NextChar();
					if (this.CurerntChar == '=')
					{
						this.kind = XPathScanner.LexKind.Ne;
						this.NextChar();
						return true;
					}
					return true;
				case '"':
				case '\'':
					this.kind = XPathScanner.LexKind.String;
					this.stringValue = this.ScanString();
					return true;
				case '#':
				case '$':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '=':
				case '@':
					break;
				case '%':
				case '&':
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
				case ':':
				case ';':
				case '?':
					goto IL_021D;
				case '.':
					this.kind = XPathScanner.LexKind.Dot;
					this.NextChar();
					if (this.CurerntChar == '.')
					{
						this.kind = XPathScanner.LexKind.DotDot;
						this.NextChar();
						return true;
					}
					if (XmlCharType.IsDigit(this.CurerntChar))
					{
						this.kind = XPathScanner.LexKind.Number;
						this.numberValue = this.ScanFraction();
						return true;
					}
					return true;
				case '/':
					this.kind = XPathScanner.LexKind.Slash;
					this.NextChar();
					if (this.CurerntChar == '/')
					{
						this.kind = XPathScanner.LexKind.SlashSlash;
						this.NextChar();
						return true;
					}
					return true;
				case '<':
					this.kind = XPathScanner.LexKind.Lt;
					this.NextChar();
					if (this.CurerntChar == '=')
					{
						this.kind = XPathScanner.LexKind.Le;
						this.NextChar();
						return true;
					}
					return true;
				case '>':
					this.kind = XPathScanner.LexKind.Gt;
					this.NextChar();
					if (this.CurerntChar == '=')
					{
						this.kind = XPathScanner.LexKind.Ge;
						this.NextChar();
						return true;
					}
					return true;
				default:
					goto IL_021D;
				}
			}
			else if (curerntChar != '[' && curerntChar != ']' && curerntChar != '|')
			{
				goto IL_021D;
			}
			this.kind = (XPathScanner.LexKind)Convert.ToInt32(this.CurerntChar, CultureInfo.InvariantCulture);
			this.NextChar();
			return true;
			IL_021D:
			if (XmlCharType.IsDigit(this.CurerntChar))
			{
				this.kind = XPathScanner.LexKind.Number;
				this.numberValue = this.ScanNumber();
			}
			else
			{
				if (!this.xmlCharType.IsStartNCNameSingleChar(this.CurerntChar))
				{
					throw XPathException.Create("'{0}' has an invalid token.", this.SourceText);
				}
				this.kind = XPathScanner.LexKind.Name;
				this.name = this.ScanName();
				this.prefix = string.Empty;
				if (this.CurerntChar == ':')
				{
					this.NextChar();
					if (this.CurerntChar == ':')
					{
						this.NextChar();
						this.kind = XPathScanner.LexKind.Axe;
					}
					else
					{
						this.prefix = this.name;
						if (this.CurerntChar == '*')
						{
							this.NextChar();
							this.name = "*";
						}
						else
						{
							if (!this.xmlCharType.IsStartNCNameSingleChar(this.CurerntChar))
							{
								throw XPathException.Create("'{0}' has an invalid qualified name.", this.SourceText);
							}
							this.name = this.ScanName();
						}
					}
				}
				else
				{
					this.SkipSpace();
					if (this.CurerntChar == ':')
					{
						this.NextChar();
						if (this.CurerntChar != ':')
						{
							throw XPathException.Create("'{0}' has an invalid qualified name.", this.SourceText);
						}
						this.NextChar();
						this.kind = XPathScanner.LexKind.Axe;
					}
				}
				this.SkipSpace();
				this.canBeFunction = this.CurerntChar == '(';
			}
			return true;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009C04 File Offset: 0x00007E04
		private double ScanNumber()
		{
			int num = this.xpathExprIndex - 1;
			int num2 = 0;
			while (XmlCharType.IsDigit(this.CurerntChar))
			{
				this.NextChar();
				num2++;
			}
			if (this.CurerntChar == '.')
			{
				this.NextChar();
				num2++;
				while (XmlCharType.IsDigit(this.CurerntChar))
				{
					this.NextChar();
					num2++;
				}
			}
			return XmlConvert.ToXPathDouble(this.xpathExpr.Substring(num, num2));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009C78 File Offset: 0x00007E78
		private double ScanFraction()
		{
			int num = this.xpathExprIndex - 2;
			int num2 = 1;
			while (XmlCharType.IsDigit(this.CurerntChar))
			{
				this.NextChar();
				num2++;
			}
			return XmlConvert.ToXPathDouble(this.xpathExpr.Substring(num, num2));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009CBC File Offset: 0x00007EBC
		private string ScanString()
		{
			char curerntChar = this.CurerntChar;
			this.NextChar();
			int num = this.xpathExprIndex - 1;
			int num2 = 0;
			while (this.CurerntChar != curerntChar)
			{
				if (!this.NextChar())
				{
					throw XPathException.Create("This is an unclosed string.");
				}
				num2++;
			}
			this.NextChar();
			return this.xpathExpr.Substring(num, num2);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009D18 File Offset: 0x00007F18
		private string ScanName()
		{
			int num = this.xpathExprIndex - 1;
			int num2 = 0;
			while (this.xmlCharType.IsNCNameSingleChar(this.CurerntChar))
			{
				this.NextChar();
				num2++;
			}
			return this.xpathExpr.Substring(num, num2);
		}

		// Token: 0x0400012E RID: 302
		private string xpathExpr;

		// Token: 0x0400012F RID: 303
		private int xpathExprIndex;

		// Token: 0x04000130 RID: 304
		private XPathScanner.LexKind kind;

		// Token: 0x04000131 RID: 305
		private char currentChar;

		// Token: 0x04000132 RID: 306
		private string name;

		// Token: 0x04000133 RID: 307
		private string prefix;

		// Token: 0x04000134 RID: 308
		private string stringValue;

		// Token: 0x04000135 RID: 309
		private double numberValue = double.NaN;

		// Token: 0x04000136 RID: 310
		private bool canBeFunction;

		// Token: 0x04000137 RID: 311
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000054 RID: 84
		public enum LexKind
		{
			// Token: 0x04000139 RID: 313
			Comma = 44,
			// Token: 0x0400013A RID: 314
			Slash = 47,
			// Token: 0x0400013B RID: 315
			At = 64,
			// Token: 0x0400013C RID: 316
			Dot = 46,
			// Token: 0x0400013D RID: 317
			LParens = 40,
			// Token: 0x0400013E RID: 318
			RParens,
			// Token: 0x0400013F RID: 319
			LBracket = 91,
			// Token: 0x04000140 RID: 320
			RBracket = 93,
			// Token: 0x04000141 RID: 321
			Star = 42,
			// Token: 0x04000142 RID: 322
			Plus,
			// Token: 0x04000143 RID: 323
			Minus = 45,
			// Token: 0x04000144 RID: 324
			Eq = 61,
			// Token: 0x04000145 RID: 325
			Lt = 60,
			// Token: 0x04000146 RID: 326
			Gt = 62,
			// Token: 0x04000147 RID: 327
			Bang = 33,
			// Token: 0x04000148 RID: 328
			Dollar = 36,
			// Token: 0x04000149 RID: 329
			Apos = 39,
			// Token: 0x0400014A RID: 330
			Quote = 34,
			// Token: 0x0400014B RID: 331
			Union = 124,
			// Token: 0x0400014C RID: 332
			Ne = 78,
			// Token: 0x0400014D RID: 333
			Le = 76,
			// Token: 0x0400014E RID: 334
			Ge = 71,
			// Token: 0x0400014F RID: 335
			And = 65,
			// Token: 0x04000150 RID: 336
			Or = 79,
			// Token: 0x04000151 RID: 337
			DotDot = 68,
			// Token: 0x04000152 RID: 338
			SlashSlash = 83,
			// Token: 0x04000153 RID: 339
			Name = 110,
			// Token: 0x04000154 RID: 340
			String = 115,
			// Token: 0x04000155 RID: 341
			Number = 100,
			// Token: 0x04000156 RID: 342
			Axe = 97,
			// Token: 0x04000157 RID: 343
			Eof = 69
		}
	}
}
