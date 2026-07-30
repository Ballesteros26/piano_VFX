using System;
using System.Collections.Generic;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005C0 RID: 1472
	internal class XPathParser<Node>
	{
		// Token: 0x06003A68 RID: 14952 RVA: 0x0014A310 File Offset: 0x00148510
		public Node Parse(XPathScanner scanner, IXPathBuilder<Node> builder, LexKind endLex)
		{
			Node node = default(Node);
			this.scanner = scanner;
			this.builder = builder;
			this.posInfo.Clear();
			try
			{
				builder.StartBuild();
				node = this.ParseExpr();
				scanner.CheckToken(endLex);
			}
			catch (XPathCompileException ex)
			{
				if (ex.queryString == null)
				{
					ex.queryString = scanner.Source;
					this.PopPosInfo(out ex.startChar, out ex.endChar);
				}
				throw;
			}
			finally
			{
				node = builder.EndBuild(node);
			}
			return node;
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x0014A3A4 File Offset: 0x001485A4
		internal static bool IsStep(LexKind lexKind)
		{
			return lexKind == LexKind.Dot || lexKind == LexKind.DotDot || lexKind == LexKind.At || lexKind == LexKind.Axis || lexKind == LexKind.Star || lexKind == LexKind.Name;
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x0014A3C8 File Offset: 0x001485C8
		private Node ParseLocationPath()
		{
			if (this.scanner.Kind == LexKind.Slash)
			{
				this.scanner.NextLex();
				Node node = this.builder.Axis(XPathAxis.Root, XPathNodeType.All, null, null);
				if (XPathParser<Node>.IsStep(this.scanner.Kind))
				{
					node = this.builder.JoinStep(node, this.ParseRelativeLocationPath());
				}
				return node;
			}
			if (this.scanner.Kind == LexKind.SlashSlash)
			{
				this.scanner.NextLex();
				return this.builder.JoinStep(this.builder.Axis(XPathAxis.Root, XPathNodeType.All, null, null), this.builder.JoinStep(this.builder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativeLocationPath()));
			}
			return this.ParseRelativeLocationPath();
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x0014A488 File Offset: 0x00148688
		private Node ParseRelativeLocationPath()
		{
			int num = this.parseRelativePath + 1;
			this.parseRelativePath = num;
			if (num > 1024 && XsltConfigSection.LimitXPathComplexity)
			{
				throw this.scanner.CreateException("The stylesheet is too complex.", Array.Empty<string>());
			}
			Node node = this.ParseStep();
			if (this.scanner.Kind == LexKind.Slash)
			{
				this.scanner.NextLex();
				node = this.builder.JoinStep(node, this.ParseRelativeLocationPath());
			}
			else if (this.scanner.Kind == LexKind.SlashSlash)
			{
				this.scanner.NextLex();
				node = this.builder.JoinStep(node, this.builder.JoinStep(this.builder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativeLocationPath()));
			}
			this.parseRelativePath--;
			return node;
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x0014A558 File Offset: 0x00148758
		private Node ParseStep()
		{
			Node node;
			if (LexKind.Dot == this.scanner.Kind)
			{
				this.scanner.NextLex();
				node = this.builder.Axis(XPathAxis.Self, XPathNodeType.All, null, null);
				if (LexKind.LBracket == this.scanner.Kind)
				{
					throw this.scanner.CreateException("Abbreviated step '.' cannot be followed by a predicate. Use the full form 'self::node()[predicate]' instead.", Array.Empty<string>());
				}
			}
			else if (LexKind.DotDot == this.scanner.Kind)
			{
				this.scanner.NextLex();
				node = this.builder.Axis(XPathAxis.Parent, XPathNodeType.All, null, null);
				if (LexKind.LBracket == this.scanner.Kind)
				{
					throw this.scanner.CreateException("Abbreviated step '..' cannot be followed by a predicate. Use the full form 'parent::node()[predicate]' instead.", Array.Empty<string>());
				}
			}
			else
			{
				LexKind kind = this.scanner.Kind;
				XPathAxis xpathAxis;
				if (kind <= LexKind.Name)
				{
					if (kind == LexKind.Axis)
					{
						xpathAxis = this.scanner.Axis;
						this.scanner.NextLex();
						this.scanner.NextLex();
						goto IL_012D;
					}
					if (kind != LexKind.Name)
					{
						goto IL_0108;
					}
				}
				else if (kind != LexKind.Star)
				{
					if (kind != LexKind.At)
					{
						goto IL_0108;
					}
					xpathAxis = XPathAxis.Attribute;
					this.scanner.NextLex();
					goto IL_012D;
				}
				xpathAxis = XPathAxis.Child;
				goto IL_012D;
				IL_0108:
				throw this.scanner.CreateException("Unexpected token '{0}' in the expression.", new string[] { this.scanner.RawValue });
				IL_012D:
				node = this.ParseNodeTest(xpathAxis);
				while (LexKind.LBracket == this.scanner.Kind)
				{
					node = this.builder.Predicate(node, this.ParsePredicate(), XPathParser<Node>.IsReverseAxis(xpathAxis));
				}
			}
			return node;
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x0014A6C5 File Offset: 0x001488C5
		private static bool IsReverseAxis(XPathAxis axis)
		{
			return axis == XPathAxis.Ancestor || axis == XPathAxis.Preceding || axis == XPathAxis.AncestorOrSelf || axis == XPathAxis.PrecedingSibling;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x0014A6DC File Offset: 0x001488DC
		private Node ParseNodeTest(XPathAxis axis)
		{
			int lexStart = this.scanner.LexStart;
			XPathNodeType xpathNodeType;
			string text;
			string text2;
			XPathParser<Node>.InternalParseNodeTest(this.scanner, axis, out xpathNodeType, out text, out text2);
			this.PushPosInfo(lexStart, this.scanner.PrevLexEnd);
			Node node = this.builder.Axis(axis, xpathNodeType, text, text2);
			this.PopPosInfo();
			return node;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x0014A730 File Offset: 0x00148930
		private static bool IsNodeType(XPathScanner scanner)
		{
			return scanner.Prefix.Length == 0 && (scanner.Name == "node" || scanner.Name == "text" || scanner.Name == "processing-instruction" || scanner.Name == "comment");
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x0014A794 File Offset: 0x00148994
		private static XPathNodeType PrincipalNodeType(XPathAxis axis)
		{
			if (axis == XPathAxis.Attribute)
			{
				return XPathNodeType.Attribute;
			}
			if (axis != XPathAxis.Namespace)
			{
				return XPathNodeType.Element;
			}
			return XPathNodeType.Namespace;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x0014A7A4 File Offset: 0x001489A4
		internal static void InternalParseNodeTest(XPathScanner scanner, XPathAxis axis, out XPathNodeType nodeType, out string nodePrefix, out string nodeName)
		{
			LexKind kind = scanner.Kind;
			if (kind != LexKind.Name)
			{
				if (kind != LexKind.Star)
				{
					throw scanner.CreateException("Expected a node test, found '{0}'.", new string[] { scanner.RawValue });
				}
				nodePrefix = null;
				nodeName = null;
				nodeType = XPathParser<Node>.PrincipalNodeType(axis);
				scanner.NextLex();
				return;
			}
			else
			{
				if (scanner.CanBeFunction && XPathParser<Node>.IsNodeType(scanner))
				{
					nodePrefix = null;
					nodeName = null;
					string name = scanner.Name;
					if (!(name == "comment"))
					{
						if (!(name == "text"))
						{
							if (!(name == "node"))
							{
								nodeType = XPathNodeType.ProcessingInstruction;
							}
							else
							{
								nodeType = XPathNodeType.All;
							}
						}
						else
						{
							nodeType = XPathNodeType.Text;
						}
					}
					else
					{
						nodeType = XPathNodeType.Comment;
					}
					scanner.NextLex();
					scanner.PassToken(LexKind.LParens);
					if (nodeType == XPathNodeType.ProcessingInstruction && scanner.Kind != LexKind.RParens)
					{
						scanner.CheckToken(LexKind.String);
						nodePrefix = string.Empty;
						nodeName = scanner.StringValue;
						scanner.NextLex();
					}
					scanner.PassToken(LexKind.RParens);
					return;
				}
				nodePrefix = scanner.Prefix;
				nodeName = scanner.Name;
				nodeType = XPathParser<Node>.PrincipalNodeType(axis);
				scanner.NextLex();
				if (nodeName == "*")
				{
					nodeName = null;
					return;
				}
				return;
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x0014A8D2 File Offset: 0x00148AD2
		private Node ParsePredicate()
		{
			this.scanner.PassToken(LexKind.LBracket);
			Node node = this.ParseExpr();
			this.scanner.PassToken(LexKind.RBracket);
			return node;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x0014A8F4 File Offset: 0x00148AF4
		private Node ParseExpr()
		{
			return this.ParseSubExpr(0);
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x0014A900 File Offset: 0x00148B00
		private Node ParseSubExpr(int callerPrec)
		{
			int num = this.parseSubExprDepth + 1;
			this.parseSubExprDepth = num;
			if (num > 1024 && XsltConfigSection.LimitXPathComplexity)
			{
				throw this.scanner.CreateException("The stylesheet is too complex.", Array.Empty<string>());
			}
			Node node;
			if (this.scanner.Kind == LexKind.Minus)
			{
				XPathOperator xpathOperator = XPathOperator.UnaryMinus;
				int num2 = XPathParser<Node>.XPathOperatorPrecedence[(int)xpathOperator];
				this.scanner.NextLex();
				node = this.builder.Operator(xpathOperator, this.ParseSubExpr(num2), default(Node));
			}
			else
			{
				node = this.ParseUnionExpr();
			}
			for (;;)
			{
				XPathOperator xpathOperator = (XPathOperator)((this.scanner.Kind <= LexKind.Union) ? this.scanner.Kind : LexKind.Unknown);
				int num3 = XPathParser<Node>.XPathOperatorPrecedence[(int)xpathOperator];
				if (num3 <= callerPrec)
				{
					break;
				}
				this.scanner.NextLex();
				node = this.builder.Operator(xpathOperator, node, this.ParseSubExpr(num3));
			}
			this.parseSubExprDepth--;
			return node;
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x0014A9EC File Offset: 0x00148BEC
		private Node ParseUnionExpr()
		{
			int num = this.scanner.LexStart;
			Node node = this.ParsePathExpr();
			if (this.scanner.Kind == LexKind.Union)
			{
				this.PushPosInfo(num, this.scanner.PrevLexEnd);
				node = this.builder.Operator(XPathOperator.Union, default(Node), node);
				this.PopPosInfo();
				while (this.scanner.Kind == LexKind.Union)
				{
					this.scanner.NextLex();
					num = this.scanner.LexStart;
					Node node2 = this.ParsePathExpr();
					this.PushPosInfo(num, this.scanner.PrevLexEnd);
					node = this.builder.Operator(XPathOperator.Union, node, node2);
					this.PopPosInfo();
				}
			}
			return node;
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x0014AAA8 File Offset: 0x00148CA8
		private Node ParsePathExpr()
		{
			if (this.IsPrimaryExpr())
			{
				int lexStart = this.scanner.LexStart;
				Node node = this.ParseFilterExpr();
				int prevLexEnd = this.scanner.PrevLexEnd;
				if (this.scanner.Kind == LexKind.Slash)
				{
					this.scanner.NextLex();
					this.PushPosInfo(lexStart, prevLexEnd);
					node = this.builder.JoinStep(node, this.ParseRelativeLocationPath());
					this.PopPosInfo();
				}
				else if (this.scanner.Kind == LexKind.SlashSlash)
				{
					this.scanner.NextLex();
					this.PushPosInfo(lexStart, prevLexEnd);
					node = this.builder.JoinStep(node, this.builder.JoinStep(this.builder.Axis(XPathAxis.DescendantOrSelf, XPathNodeType.All, null, null), this.ParseRelativeLocationPath()));
					this.PopPosInfo();
				}
				return node;
			}
			return this.ParseLocationPath();
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x0014AB7C File Offset: 0x00148D7C
		private Node ParseFilterExpr()
		{
			int lexStart = this.scanner.LexStart;
			Node node = this.ParsePrimaryExpr();
			int prevLexEnd = this.scanner.PrevLexEnd;
			while (this.scanner.Kind == LexKind.LBracket)
			{
				this.PushPosInfo(lexStart, prevLexEnd);
				node = this.builder.Predicate(node, this.ParsePredicate(), false);
				this.PopPosInfo();
			}
			return node;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x0014ABDC File Offset: 0x00148DDC
		private bool IsPrimaryExpr()
		{
			return this.scanner.Kind == LexKind.String || this.scanner.Kind == LexKind.Number || this.scanner.Kind == LexKind.Dollar || this.scanner.Kind == LexKind.LParens || (this.scanner.Kind == LexKind.Name && this.scanner.CanBeFunction && !XPathParser<Node>.IsNodeType(this.scanner));
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0014AC54 File Offset: 0x00148E54
		private Node ParsePrimaryExpr()
		{
			LexKind kind = this.scanner.Kind;
			Node node;
			if (kind <= LexKind.String)
			{
				if (kind == LexKind.Number)
				{
					node = this.builder.Number(XPathConvert.StringToDouble(this.scanner.RawValue));
					this.scanner.NextLex();
					return node;
				}
				if (kind == LexKind.String)
				{
					node = this.builder.String(this.scanner.StringValue);
					this.scanner.NextLex();
					return node;
				}
			}
			else
			{
				if (kind == LexKind.Dollar)
				{
					int lexStart = this.scanner.LexStart;
					this.scanner.NextLex();
					this.scanner.CheckToken(LexKind.Name);
					this.PushPosInfo(lexStart, this.scanner.LexStart + this.scanner.LexSize);
					node = this.builder.Variable(this.scanner.Prefix, this.scanner.Name);
					this.PopPosInfo();
					this.scanner.NextLex();
					return node;
				}
				if (kind == LexKind.LParens)
				{
					this.scanner.NextLex();
					node = this.ParseExpr();
					this.scanner.PassToken(LexKind.RParens);
					return node;
				}
			}
			node = this.ParseFunctionCall();
			return node;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0014AD88 File Offset: 0x00148F88
		private Node ParseFunctionCall()
		{
			List<Node> list = new List<Node>();
			string name = this.scanner.Name;
			string prefix = this.scanner.Prefix;
			int lexStart = this.scanner.LexStart;
			this.scanner.PassToken(LexKind.Name);
			this.scanner.PassToken(LexKind.LParens);
			if (this.scanner.Kind != LexKind.RParens)
			{
				for (;;)
				{
					list.Add(this.ParseExpr());
					if (this.scanner.Kind != LexKind.Comma)
					{
						break;
					}
					this.scanner.NextLex();
				}
				this.scanner.CheckToken(LexKind.RParens);
			}
			this.scanner.NextLex();
			this.PushPosInfo(lexStart, this.scanner.PrevLexEnd);
			Node node = this.builder.Function(prefix, name, list);
			this.PopPosInfo();
			return node;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x0014AE50 File Offset: 0x00149050
		private void PushPosInfo(int startChar, int endChar)
		{
			this.posInfo.Push(startChar);
			this.posInfo.Push(endChar);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0014AE6A File Offset: 0x0014906A
		private void PopPosInfo()
		{
			this.posInfo.Pop();
			this.posInfo.Pop();
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x0014AE84 File Offset: 0x00149084
		private void PopPosInfo(out int startChar, out int endChar)
		{
			endChar = this.posInfo.Pop();
			startChar = this.posInfo.Pop();
		}

		// Token: 0x04002615 RID: 9749
		private XPathScanner scanner;

		// Token: 0x04002616 RID: 9750
		private IXPathBuilder<Node> builder;

		// Token: 0x04002617 RID: 9751
		private Stack<int> posInfo = new Stack<int>();

		// Token: 0x04002618 RID: 9752
		private const int MaxParseRelativePathDepth = 1024;

		// Token: 0x04002619 RID: 9753
		private int parseRelativePath;

		// Token: 0x0400261A RID: 9754
		private const int MaxParseSubExprDepth = 1024;

		// Token: 0x0400261B RID: 9755
		private int parseSubExprDepth;

		// Token: 0x0400261C RID: 9756
		private static int[] XPathOperatorPrecedence = new int[]
		{
			0, 1, 2, 3, 3, 4, 4, 4, 4, 5,
			5, 6, 6, 6, 7, 8
		};
	}
}
