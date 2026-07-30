using System;
using System.Collections;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000051 RID: 81
	internal class XPathParser
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000083BF File Offset: 0x000065BF
		private XPathParser(XPathScanner scanner)
		{
			this.scanner = scanner;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000083D0 File Offset: 0x000065D0
		public static AstNode ParseXPathExpresion(string xpathExpresion)
		{
			XPathScanner xpathScanner = new XPathScanner(xpathExpresion);
			AstNode astNode = new XPathParser(xpathScanner).ParseExpresion(null);
			if (xpathScanner.Kind != XPathScanner.LexKind.Eof)
			{
				throw XPathException.Create("'{0}' has an invalid token.", xpathScanner.SourceText);
			}
			return astNode;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000840C File Offset: 0x0000660C
		public static AstNode ParseXPathPattern(string xpathPattern)
		{
			XPathScanner xpathScanner = new XPathScanner(xpathPattern);
			AstNode astNode = new XPathParser(xpathScanner).ParsePattern(null);
			if (xpathScanner.Kind != XPathScanner.LexKind.Eof)
			{
				throw XPathException.Create("'{0}' has an invalid token.", xpathScanner.SourceText);
			}
			return astNode;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008448 File Offset: 0x00006648
		private AstNode ParseExpresion(AstNode qyInput)
		{
			int num = this.parseDepth + 1;
			this.parseDepth = num;
			if (num > 200)
			{
				throw XPathException.Create("The xpath query is too complex.");
			}
			AstNode astNode = this.ParseOrExpr(qyInput);
			this.parseDepth--;
			return astNode;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008490 File Offset: 0x00006690
		private AstNode ParseOrExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseAndExpr(qyInput);
			while (this.TestOp("or"))
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.OR, astNode, this.ParseAndExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000084CC File Offset: 0x000066CC
		private AstNode ParseAndExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseEqualityExpr(qyInput);
			while (this.TestOp("and"))
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.AND, astNode, this.ParseEqualityExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008508 File Offset: 0x00006708
		private AstNode ParseEqualityExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseRelationalExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this.scanner.Kind == XPathScanner.LexKind.Eq) ? Operator.Op.EQ : ((this.scanner.Kind == XPathScanner.LexKind.Ne) ? Operator.Op.NE : Operator.Op.INVALID));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseRelationalExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008560 File Offset: 0x00006760
		private AstNode ParseRelationalExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseAdditiveExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this.scanner.Kind == XPathScanner.LexKind.Lt) ? Operator.Op.LT : ((this.scanner.Kind == XPathScanner.LexKind.Le) ? Operator.Op.LE : ((this.scanner.Kind == XPathScanner.LexKind.Gt) ? Operator.Op.GT : ((this.scanner.Kind == XPathScanner.LexKind.Ge) ? Operator.Op.GE : Operator.Op.INVALID))));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseAdditiveExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000085DC File Offset: 0x000067DC
		private AstNode ParseAdditiveExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseMultiplicativeExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this.scanner.Kind == XPathScanner.LexKind.Plus) ? Operator.Op.PLUS : ((this.scanner.Kind == XPathScanner.LexKind.Minus) ? Operator.Op.MINUS : Operator.Op.INVALID));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseMultiplicativeExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008634 File Offset: 0x00006834
		private AstNode ParseMultiplicativeExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseUnaryExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this.scanner.Kind == XPathScanner.LexKind.Star) ? Operator.Op.MUL : (this.TestOp("div") ? Operator.Op.DIV : (this.TestOp("mod") ? Operator.Op.MOD : Operator.Op.INVALID)));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseUnaryExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000869C File Offset: 0x0000689C
		private AstNode ParseUnaryExpr(AstNode qyInput)
		{
			bool flag = false;
			while (this.scanner.Kind == XPathScanner.LexKind.Minus)
			{
				this.NextLex();
				flag = !flag;
			}
			if (flag)
			{
				return new Operator(Operator.Op.MUL, this.ParseUnionExpr(qyInput), new Operand(-1.0));
			}
			return this.ParseUnionExpr(qyInput);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000086F0 File Offset: 0x000068F0
		private AstNode ParseUnionExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParsePathExpr(qyInput);
			while (this.scanner.Kind == XPathScanner.LexKind.Union)
			{
				this.NextLex();
				AstNode astNode2 = this.ParsePathExpr(qyInput);
				this.CheckNodeSet(astNode.ReturnType);
				this.CheckNodeSet(astNode2.ReturnType);
				astNode = new Operator(Operator.Op.UNION, astNode, astNode2);
			}
			return astNode;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008748 File Offset: 0x00006948
		private static bool IsNodeType(XPathScanner scaner)
		{
			return scaner.Prefix.Length == 0 && (scaner.Name == "node" || scaner.Name == "text" || scaner.Name == "processing-instruction" || scaner.Name == "comment");
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000087AC File Offset: 0x000069AC
		private AstNode ParsePathExpr(AstNode qyInput)
		{
			AstNode astNode;
			if (XPathParser.IsPrimaryExpr(this.scanner))
			{
				astNode = this.ParseFilterExpr(qyInput);
				if (this.scanner.Kind == XPathScanner.LexKind.Slash)
				{
					this.NextLex();
					astNode = this.ParseRelativeLocationPath(astNode);
				}
				else if (this.scanner.Kind == XPathScanner.LexKind.SlashSlash)
				{
					this.NextLex();
					astNode = this.ParseRelativeLocationPath(new Axis(Axis.AxisType.DescendantOrSelf, astNode));
				}
			}
			else
			{
				astNode = this.ParseLocationPath(null);
			}
			return astNode;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000881C File Offset: 0x00006A1C
		private AstNode ParseFilterExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParsePrimaryExpr(qyInput);
			while (this.scanner.Kind == XPathScanner.LexKind.LBracket)
			{
				astNode = new Filter(astNode, this.ParsePredicate(astNode));
			}
			return astNode;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008851 File Offset: 0x00006A51
		private AstNode ParsePredicate(AstNode qyInput)
		{
			this.CheckNodeSet(qyInput.ReturnType);
			this.PassToken(XPathScanner.LexKind.LBracket);
			AstNode astNode = this.ParseExpresion(qyInput);
			this.PassToken(XPathScanner.LexKind.RBracket);
			return astNode;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008878 File Offset: 0x00006A78
		private AstNode ParseLocationPath(AstNode qyInput)
		{
			if (this.scanner.Kind == XPathScanner.LexKind.Slash)
			{
				this.NextLex();
				AstNode astNode = new Root();
				if (XPathParser.IsStep(this.scanner.Kind))
				{
					astNode = this.ParseRelativeLocationPath(astNode);
				}
				return astNode;
			}
			if (this.scanner.Kind == XPathScanner.LexKind.SlashSlash)
			{
				this.NextLex();
				return this.ParseRelativeLocationPath(new Axis(Axis.AxisType.DescendantOrSelf, new Root()));
			}
			return this.ParseRelativeLocationPath(qyInput);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000088EC File Offset: 0x00006AEC
		private AstNode ParseRelativeLocationPath(AstNode qyInput)
		{
			AstNode astNode = qyInput;
			for (;;)
			{
				astNode = this.ParseStep(astNode);
				if (XPathScanner.LexKind.SlashSlash == this.scanner.Kind)
				{
					this.NextLex();
					astNode = new Axis(Axis.AxisType.DescendantOrSelf, astNode);
				}
				else
				{
					if (XPathScanner.LexKind.Slash != this.scanner.Kind)
					{
						break;
					}
					this.NextLex();
				}
			}
			return astNode;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000893A File Offset: 0x00006B3A
		private static bool IsStep(XPathScanner.LexKind lexKind)
		{
			return lexKind == XPathScanner.LexKind.Dot || lexKind == XPathScanner.LexKind.DotDot || lexKind == XPathScanner.LexKind.At || lexKind == XPathScanner.LexKind.Axe || lexKind == XPathScanner.LexKind.Star || lexKind == XPathScanner.LexKind.Name;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000895C File Offset: 0x00006B5C
		private AstNode ParseStep(AstNode qyInput)
		{
			AstNode astNode;
			if (XPathScanner.LexKind.Dot == this.scanner.Kind)
			{
				this.NextLex();
				astNode = new Axis(Axis.AxisType.Self, qyInput);
			}
			else if (XPathScanner.LexKind.DotDot == this.scanner.Kind)
			{
				this.NextLex();
				astNode = new Axis(Axis.AxisType.Parent, qyInput);
			}
			else
			{
				Axis.AxisType axisType = Axis.AxisType.Child;
				XPathScanner.LexKind kind = this.scanner.Kind;
				if (kind != XPathScanner.LexKind.At)
				{
					if (kind == XPathScanner.LexKind.Axe)
					{
						axisType = this.GetAxis(this.scanner);
						this.NextLex();
					}
				}
				else
				{
					axisType = Axis.AxisType.Attribute;
					this.NextLex();
				}
				XPathNodeType xpathNodeType = ((axisType == Axis.AxisType.Attribute) ? XPathNodeType.Attribute : XPathNodeType.Element);
				astNode = this.ParseNodeTest(qyInput, axisType, xpathNodeType);
				while (XPathScanner.LexKind.LBracket == this.scanner.Kind)
				{
					astNode = new Filter(astNode, this.ParsePredicate(astNode));
				}
			}
			return astNode;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008A18 File Offset: 0x00006C18
		private AstNode ParseNodeTest(AstNode qyInput, Axis.AxisType axisType, XPathNodeType nodeType)
		{
			XPathScanner.LexKind kind = this.scanner.Kind;
			string text;
			string text2;
			if (kind != XPathScanner.LexKind.Star)
			{
				if (kind != XPathScanner.LexKind.Name)
				{
					throw XPathException.Create("Expression must evaluate to a node-set.", this.scanner.SourceText);
				}
				if (this.scanner.CanBeFunction && XPathParser.IsNodeType(this.scanner))
				{
					text = string.Empty;
					text2 = string.Empty;
					nodeType = ((this.scanner.Name == "comment") ? XPathNodeType.Comment : ((this.scanner.Name == "text") ? XPathNodeType.Text : ((this.scanner.Name == "node") ? XPathNodeType.All : ((this.scanner.Name == "processing-instruction") ? XPathNodeType.ProcessingInstruction : XPathNodeType.Root))));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					if (nodeType == XPathNodeType.ProcessingInstruction && this.scanner.Kind != XPathScanner.LexKind.RParens)
					{
						this.CheckToken(XPathScanner.LexKind.String);
						text2 = this.scanner.StringValue;
						this.NextLex();
					}
					this.PassToken(XPathScanner.LexKind.RParens);
				}
				else
				{
					text = this.scanner.Prefix;
					text2 = this.scanner.Name;
					this.NextLex();
					if (text2 == "*")
					{
						text2 = string.Empty;
					}
				}
			}
			else
			{
				text = string.Empty;
				text2 = string.Empty;
				this.NextLex();
			}
			return new Axis(axisType, qyInput, text, text2, nodeType);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008B88 File Offset: 0x00006D88
		private static bool IsPrimaryExpr(XPathScanner scanner)
		{
			return scanner.Kind == XPathScanner.LexKind.String || scanner.Kind == XPathScanner.LexKind.Number || scanner.Kind == XPathScanner.LexKind.Dollar || scanner.Kind == XPathScanner.LexKind.LParens || (scanner.Kind == XPathScanner.LexKind.Name && scanner.CanBeFunction && !XPathParser.IsNodeType(scanner));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008BDC File Offset: 0x00006DDC
		private AstNode ParsePrimaryExpr(AstNode qyInput)
		{
			AstNode astNode = null;
			XPathScanner.LexKind kind = this.scanner.Kind;
			if (kind <= XPathScanner.LexKind.LParens)
			{
				if (kind != XPathScanner.LexKind.Dollar)
				{
					if (kind == XPathScanner.LexKind.LParens)
					{
						this.NextLex();
						astNode = this.ParseExpresion(qyInput);
						if (astNode.Type != AstNode.AstType.ConstantOperand)
						{
							astNode = new Group(astNode);
						}
						this.PassToken(XPathScanner.LexKind.RParens);
					}
				}
				else
				{
					this.NextLex();
					this.CheckToken(XPathScanner.LexKind.Name);
					astNode = new Variable(this.scanner.Name, this.scanner.Prefix);
					this.NextLex();
				}
			}
			else if (kind != XPathScanner.LexKind.Number)
			{
				if (kind != XPathScanner.LexKind.Name)
				{
					if (kind == XPathScanner.LexKind.String)
					{
						astNode = new Operand(this.scanner.StringValue);
						this.NextLex();
					}
				}
				else if (this.scanner.CanBeFunction && !XPathParser.IsNodeType(this.scanner))
				{
					astNode = this.ParseMethod(null);
				}
			}
			else
			{
				astNode = new Operand(this.scanner.NumberValue);
				this.NextLex();
			}
			return astNode;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008CD8 File Offset: 0x00006ED8
		private AstNode ParseMethod(AstNode qyInput)
		{
			ArrayList arrayList = new ArrayList();
			string name = this.scanner.Name;
			string prefix = this.scanner.Prefix;
			this.PassToken(XPathScanner.LexKind.Name);
			this.PassToken(XPathScanner.LexKind.LParens);
			if (this.scanner.Kind != XPathScanner.LexKind.RParens)
			{
				for (;;)
				{
					arrayList.Add(this.ParseExpresion(qyInput));
					if (this.scanner.Kind == XPathScanner.LexKind.RParens)
					{
						break;
					}
					this.PassToken(XPathScanner.LexKind.Comma);
				}
			}
			this.PassToken(XPathScanner.LexKind.RParens);
			if (prefix.Length == 0)
			{
				XPathParser.ParamInfo paramInfo = (XPathParser.ParamInfo)XPathParser.functionTable[name];
				if (paramInfo != null)
				{
					int num = arrayList.Count;
					if (num < paramInfo.Minargs)
					{
						throw XPathException.Create("Function '{0}' in '{1}' has an invalid number of arguments.", name, this.scanner.SourceText);
					}
					if (paramInfo.FType == Function.FunctionType.FuncConcat)
					{
						for (int i = 0; i < num; i++)
						{
							AstNode astNode = (AstNode)arrayList[i];
							if (astNode.ReturnType != XPathResultType.String)
							{
								astNode = new Function(Function.FunctionType.FuncString, astNode);
							}
							arrayList[i] = astNode;
						}
					}
					else
					{
						if (paramInfo.Maxargs < num)
						{
							throw XPathException.Create("Function '{0}' in '{1}' has an invalid number of arguments.", name, this.scanner.SourceText);
						}
						if (paramInfo.ArgTypes.Length < num)
						{
							num = paramInfo.ArgTypes.Length;
						}
						for (int j = 0; j < num; j++)
						{
							AstNode astNode2 = (AstNode)arrayList[j];
							if (paramInfo.ArgTypes[j] != XPathResultType.Any && paramInfo.ArgTypes[j] != astNode2.ReturnType)
							{
								switch (paramInfo.ArgTypes[j])
								{
								case XPathResultType.Number:
									astNode2 = new Function(Function.FunctionType.FuncNumber, astNode2);
									break;
								case XPathResultType.String:
									astNode2 = new Function(Function.FunctionType.FuncString, astNode2);
									break;
								case XPathResultType.Boolean:
									astNode2 = new Function(Function.FunctionType.FuncBoolean, astNode2);
									break;
								case XPathResultType.NodeSet:
									if (!(astNode2 is Variable) && (!(astNode2 is Function) || astNode2.ReturnType != XPathResultType.Any))
									{
										throw XPathException.Create("The argument to function '{0}' in '{1}' cannot be converted to a node-set.", name, this.scanner.SourceText);
									}
									break;
								}
								arrayList[j] = astNode2;
							}
						}
					}
					return new Function(paramInfo.FType, arrayList);
				}
			}
			return new Function(prefix, name, arrayList);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008F04 File Offset: 0x00007104
		private AstNode ParsePattern(AstNode qyInput)
		{
			AstNode astNode = this.ParseLocationPathPattern(qyInput);
			while (this.scanner.Kind == XPathScanner.LexKind.Union)
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.UNION, astNode, this.ParseLocationPathPattern(qyInput));
			}
			return astNode;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008F44 File Offset: 0x00007144
		private AstNode ParseLocationPathPattern(AstNode qyInput)
		{
			AstNode astNode = null;
			XPathScanner.LexKind lexKind = this.scanner.Kind;
			if (lexKind != XPathScanner.LexKind.Slash)
			{
				if (lexKind != XPathScanner.LexKind.SlashSlash)
				{
					if (lexKind == XPathScanner.LexKind.Name)
					{
						if (this.scanner.CanBeFunction)
						{
							astNode = this.ParseIdKeyPattern(qyInput);
							if (astNode != null)
							{
								lexKind = this.scanner.Kind;
								if (lexKind != XPathScanner.LexKind.Slash)
								{
									if (lexKind != XPathScanner.LexKind.SlashSlash)
									{
										return astNode;
									}
									this.NextLex();
									astNode = new Axis(Axis.AxisType.DescendantOrSelf, astNode);
								}
								else
								{
									this.NextLex();
								}
							}
						}
					}
				}
				else
				{
					this.NextLex();
					astNode = new Axis(Axis.AxisType.DescendantOrSelf, new Root());
				}
			}
			else
			{
				this.NextLex();
				astNode = new Root();
				if (this.scanner.Kind == XPathScanner.LexKind.Eof || this.scanner.Kind == XPathScanner.LexKind.Union)
				{
					return astNode;
				}
			}
			return this.ParseRelativePathPattern(astNode);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009004 File Offset: 0x00007204
		private AstNode ParseIdKeyPattern(AstNode qyInput)
		{
			ArrayList arrayList = new ArrayList();
			if (this.scanner.Prefix.Length == 0)
			{
				if (this.scanner.Name == "id")
				{
					XPathParser.ParamInfo paramInfo = (XPathParser.ParamInfo)XPathParser.functionTable["id"];
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					this.CheckToken(XPathScanner.LexKind.String);
					arrayList.Add(new Operand(this.scanner.StringValue));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.RParens);
					return new Function(paramInfo.FType, arrayList);
				}
				if (this.scanner.Name == "key")
				{
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					this.CheckToken(XPathScanner.LexKind.String);
					arrayList.Add(new Operand(this.scanner.StringValue));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.Comma);
					this.CheckToken(XPathScanner.LexKind.String);
					arrayList.Add(new Operand(this.scanner.StringValue));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.RParens);
					return new Function("", "key", arrayList);
				}
			}
			return null;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009130 File Offset: 0x00007330
		private AstNode ParseRelativePathPattern(AstNode qyInput)
		{
			AstNode astNode = this.ParseStepPattern(qyInput);
			if (XPathScanner.LexKind.SlashSlash == this.scanner.Kind)
			{
				this.NextLex();
				astNode = this.ParseRelativePathPattern(new Axis(Axis.AxisType.DescendantOrSelf, astNode));
			}
			else if (XPathScanner.LexKind.Slash == this.scanner.Kind)
			{
				this.NextLex();
				astNode = this.ParseRelativePathPattern(astNode);
			}
			return astNode;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009188 File Offset: 0x00007388
		private AstNode ParseStepPattern(AstNode qyInput)
		{
			Axis.AxisType axisType = Axis.AxisType.Child;
			XPathScanner.LexKind kind = this.scanner.Kind;
			if (kind != XPathScanner.LexKind.At)
			{
				if (kind == XPathScanner.LexKind.Axe)
				{
					axisType = this.GetAxis(this.scanner);
					if (axisType != Axis.AxisType.Child && axisType != Axis.AxisType.Attribute)
					{
						throw XPathException.Create("'{0}' has an invalid token.", this.scanner.SourceText);
					}
					this.NextLex();
				}
			}
			else
			{
				axisType = Axis.AxisType.Attribute;
				this.NextLex();
			}
			XPathNodeType xpathNodeType = ((axisType == Axis.AxisType.Attribute) ? XPathNodeType.Attribute : XPathNodeType.Element);
			AstNode astNode = this.ParseNodeTest(qyInput, axisType, xpathNodeType);
			while (XPathScanner.LexKind.LBracket == this.scanner.Kind)
			{
				astNode = new Filter(astNode, this.ParsePredicate(astNode));
			}
			return astNode;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000921D File Offset: 0x0000741D
		private void CheckToken(XPathScanner.LexKind t)
		{
			if (this.scanner.Kind != t)
			{
				throw XPathException.Create("'{0}' has an invalid token.", this.scanner.SourceText);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009243 File Offset: 0x00007443
		private void PassToken(XPathScanner.LexKind t)
		{
			this.CheckToken(t);
			this.NextLex();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009252 File Offset: 0x00007452
		private void NextLex()
		{
			this.scanner.NextLex();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009260 File Offset: 0x00007460
		private bool TestOp(string op)
		{
			return this.scanner.Kind == XPathScanner.LexKind.Name && this.scanner.Prefix.Length == 0 && this.scanner.Name.Equals(op);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009296 File Offset: 0x00007496
		private void CheckNodeSet(XPathResultType t)
		{
			if (t != XPathResultType.NodeSet && t != XPathResultType.Any)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.", this.scanner.SourceText);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000092B8 File Offset: 0x000074B8
		private static Hashtable CreateFunctionTable()
		{
			return new Hashtable(36)
			{
				{
					"last",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLast, 0, 0, XPathParser.temparray1)
				},
				{
					"position",
					new XPathParser.ParamInfo(Function.FunctionType.FuncPosition, 0, 0, XPathParser.temparray1)
				},
				{
					"name",
					new XPathParser.ParamInfo(Function.FunctionType.FuncName, 0, 1, XPathParser.temparray2)
				},
				{
					"namespace-uri",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNameSpaceUri, 0, 1, XPathParser.temparray2)
				},
				{
					"local-name",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLocalName, 0, 1, XPathParser.temparray2)
				},
				{
					"count",
					new XPathParser.ParamInfo(Function.FunctionType.FuncCount, 1, 1, XPathParser.temparray2)
				},
				{
					"id",
					new XPathParser.ParamInfo(Function.FunctionType.FuncID, 1, 1, XPathParser.temparray3)
				},
				{
					"string",
					new XPathParser.ParamInfo(Function.FunctionType.FuncString, 0, 1, XPathParser.temparray3)
				},
				{
					"concat",
					new XPathParser.ParamInfo(Function.FunctionType.FuncConcat, 2, 100, XPathParser.temparray4)
				},
				{
					"starts-with",
					new XPathParser.ParamInfo(Function.FunctionType.FuncStartsWith, 2, 2, XPathParser.temparray5)
				},
				{
					"contains",
					new XPathParser.ParamInfo(Function.FunctionType.FuncContains, 2, 2, XPathParser.temparray5)
				},
				{
					"substring-before",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstringBefore, 2, 2, XPathParser.temparray5)
				},
				{
					"substring-after",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstringAfter, 2, 2, XPathParser.temparray5)
				},
				{
					"substring",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstring, 2, 3, XPathParser.temparray6)
				},
				{
					"string-length",
					new XPathParser.ParamInfo(Function.FunctionType.FuncStringLength, 0, 1, XPathParser.temparray4)
				},
				{
					"normalize-space",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNormalize, 0, 1, XPathParser.temparray4)
				},
				{
					"translate",
					new XPathParser.ParamInfo(Function.FunctionType.FuncTranslate, 3, 3, XPathParser.temparray7)
				},
				{
					"boolean",
					new XPathParser.ParamInfo(Function.FunctionType.FuncBoolean, 1, 1, XPathParser.temparray3)
				},
				{
					"not",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNot, 1, 1, XPathParser.temparray8)
				},
				{
					"true",
					new XPathParser.ParamInfo(Function.FunctionType.FuncTrue, 0, 0, XPathParser.temparray8)
				},
				{
					"false",
					new XPathParser.ParamInfo(Function.FunctionType.FuncFalse, 0, 0, XPathParser.temparray8)
				},
				{
					"lang",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLang, 1, 1, XPathParser.temparray4)
				},
				{
					"number",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNumber, 0, 1, XPathParser.temparray3)
				},
				{
					"sum",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSum, 1, 1, XPathParser.temparray2)
				},
				{
					"floor",
					new XPathParser.ParamInfo(Function.FunctionType.FuncFloor, 1, 1, XPathParser.temparray9)
				},
				{
					"ceiling",
					new XPathParser.ParamInfo(Function.FunctionType.FuncCeiling, 1, 1, XPathParser.temparray9)
				},
				{
					"round",
					new XPathParser.ParamInfo(Function.FunctionType.FuncRound, 1, 1, XPathParser.temparray9)
				}
			};
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009568 File Offset: 0x00007768
		private static Hashtable CreateAxesTable()
		{
			return new Hashtable(13)
			{
				{
					"ancestor",
					Axis.AxisType.Ancestor
				},
				{
					"ancestor-or-self",
					Axis.AxisType.AncestorOrSelf
				},
				{
					"attribute",
					Axis.AxisType.Attribute
				},
				{
					"child",
					Axis.AxisType.Child
				},
				{
					"descendant",
					Axis.AxisType.Descendant
				},
				{
					"descendant-or-self",
					Axis.AxisType.DescendantOrSelf
				},
				{
					"following",
					Axis.AxisType.Following
				},
				{
					"following-sibling",
					Axis.AxisType.FollowingSibling
				},
				{
					"namespace",
					Axis.AxisType.Namespace
				},
				{
					"parent",
					Axis.AxisType.Parent
				},
				{
					"preceding",
					Axis.AxisType.Preceding
				},
				{
					"preceding-sibling",
					Axis.AxisType.PrecedingSibling
				},
				{
					"self",
					Axis.AxisType.Self
				}
			};
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000965D File Offset: 0x0000785D
		private Axis.AxisType GetAxis(XPathScanner scaner)
		{
			object obj = XPathParser.AxesTable[scaner.Name];
			if (obj == null)
			{
				throw XPathException.Create("'{0}' has an invalid token.", this.scanner.SourceText);
			}
			return (Axis.AxisType)obj;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009690 File Offset: 0x00007890
		// Note: this type is marked as 'beforefieldinit'.
		static XPathParser()
		{
			XPathResultType[] array = new XPathResultType[3];
			array[0] = XPathResultType.String;
			XPathParser.temparray6 = array;
			XPathParser.temparray7 = new XPathResultType[]
			{
				XPathResultType.String,
				XPathResultType.String,
				XPathResultType.String
			};
			XPathParser.temparray8 = new XPathResultType[] { XPathResultType.Boolean };
			XPathParser.temparray9 = new XPathResultType[1];
			XPathParser.functionTable = XPathParser.CreateFunctionTable();
			XPathParser.AxesTable = XPathParser.CreateAxesTable();
		}

		// Token: 0x0400011C RID: 284
		private XPathScanner scanner;

		// Token: 0x0400011D RID: 285
		private int parseDepth;

		// Token: 0x0400011E RID: 286
		private const int MaxParseDepth = 200;

		// Token: 0x0400011F RID: 287
		private static readonly XPathResultType[] temparray1 = new XPathResultType[0];

		// Token: 0x04000120 RID: 288
		private static readonly XPathResultType[] temparray2 = new XPathResultType[] { XPathResultType.NodeSet };

		// Token: 0x04000121 RID: 289
		private static readonly XPathResultType[] temparray3 = new XPathResultType[] { XPathResultType.Any };

		// Token: 0x04000122 RID: 290
		private static readonly XPathResultType[] temparray4 = new XPathResultType[] { XPathResultType.String };

		// Token: 0x04000123 RID: 291
		private static readonly XPathResultType[] temparray5 = new XPathResultType[]
		{
			XPathResultType.String,
			XPathResultType.String
		};

		// Token: 0x04000124 RID: 292
		private static readonly XPathResultType[] temparray6;

		// Token: 0x04000125 RID: 293
		private static readonly XPathResultType[] temparray7;

		// Token: 0x04000126 RID: 294
		private static readonly XPathResultType[] temparray8;

		// Token: 0x04000127 RID: 295
		private static readonly XPathResultType[] temparray9;

		// Token: 0x04000128 RID: 296
		private static Hashtable functionTable;

		// Token: 0x04000129 RID: 297
		private static Hashtable AxesTable;

		// Token: 0x02000052 RID: 82
		private class ParamInfo
		{
			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000266 RID: 614 RVA: 0x0000973B File Offset: 0x0000793B
			public Function.FunctionType FType
			{
				get
				{
					return this.ftype;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x06000267 RID: 615 RVA: 0x00009743 File Offset: 0x00007943
			public int Minargs
			{
				get
				{
					return this.minargs;
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000268 RID: 616 RVA: 0x0000974B File Offset: 0x0000794B
			public int Maxargs
			{
				get
				{
					return this.maxargs;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000269 RID: 617 RVA: 0x00009753 File Offset: 0x00007953
			public XPathResultType[] ArgTypes
			{
				get
				{
					return this.argTypes;
				}
			}

			// Token: 0x0600026A RID: 618 RVA: 0x0000975B File Offset: 0x0000795B
			internal ParamInfo(Function.FunctionType ftype, int minargs, int maxargs, XPathResultType[] argTypes)
			{
				this.ftype = ftype;
				this.minargs = minargs;
				this.maxargs = maxargs;
				this.argTypes = argTypes;
			}

			// Token: 0x0400012A RID: 298
			private Function.FunctionType ftype;

			// Token: 0x0400012B RID: 299
			private int minargs;

			// Token: 0x0400012C RID: 300
			private int maxargs;

			// Token: 0x0400012D RID: 301
			private XPathResultType[] argTypes;
		}
	}
}
