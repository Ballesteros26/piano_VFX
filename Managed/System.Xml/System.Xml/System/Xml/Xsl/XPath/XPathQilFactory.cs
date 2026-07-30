using System;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005C1 RID: 1473
	internal class XPathQilFactory : QilPatternFactory
	{
		// Token: 0x06003A80 RID: 14976 RVA: 0x0014AECC File Offset: 0x001490CC
		public XPathQilFactory(QilFactory f, bool debug)
			: base(f, debug)
		{
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x0014AED6 File Offset: 0x001490D6
		public QilNode Error(string res, QilNode args)
		{
			return base.Error(this.InvokeFormatMessage(base.String(res), args));
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x0014AEEC File Offset: 0x001490EC
		public QilNode Error(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			return base.Error(base.String(XslLoadException.CreateMessage(lineInfo, res, args)));
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0014AF04 File Offset: 0x00149104
		public QilIterator FirstNode(QilNode n)
		{
			QilIterator qilIterator = base.For(base.DocOrderDistinct(n));
			return base.For(base.Filter(qilIterator, base.Eq(base.PositionOf(qilIterator), base.Int32(1))));
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x0014AF40 File Offset: 0x00149140
		public bool IsAnyType(QilNode n)
		{
			XmlQueryType xmlType = n.XmlType;
			return !xmlType.IsStrict && !xmlType.IsNode;
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckAny(QilNode n)
		{
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckNode(QilNode n)
		{
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckNodeSet(QilNode n)
		{
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckNodeNotRtf(QilNode n)
		{
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckString(QilNode n)
		{
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckStringS(QilNode n)
		{
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckDouble(QilNode n)
		{
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckBool(QilNode n)
		{
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x0014AF68 File Offset: 0x00149168
		public bool CannotBeNodeSet(QilNode n)
		{
			XmlQueryType xmlType = n.XmlType;
			return xmlType.IsAtomicValue && !xmlType.IsEmpty && !(n is QilIterator);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x0014AF9C File Offset: 0x0014919C
		public QilNode SafeDocOrderDistinct(QilNode n)
		{
			XmlQueryType xmlType = n.XmlType;
			if (xmlType.MaybeMany)
			{
				if (xmlType.IsNode && xmlType.IsNotRtf)
				{
					return base.DocOrderDistinct(n);
				}
				if (!xmlType.IsAtomicValue)
				{
					QilIterator qilIterator;
					return base.Loop(qilIterator = base.Let(n), base.Conditional(base.Gt(base.Length(qilIterator), base.Int32(1)), base.DocOrderDistinct(base.TypeAssert(qilIterator, XmlQueryTypeFactory.NodeNotRtfS)), qilIterator));
				}
			}
			return n;
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x0014B016 File Offset: 0x00149216
		public QilNode InvokeFormatMessage(QilNode res, QilNode args)
		{
			return base.XsltInvokeEarlyBound(base.QName("format-message"), XsltMethods.FormatMessage, XmlQueryTypeFactory.StringX, new QilNode[] { res, args });
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x0014B044 File Offset: 0x00149244
		public QilNode InvokeEqualityOperator(QilNodeType op, QilNode left, QilNode right)
		{
			left = base.TypeAssert(left, XmlQueryTypeFactory.ItemS);
			right = base.TypeAssert(right, XmlQueryTypeFactory.ItemS);
			double num;
			if (op == QilNodeType.Eq)
			{
				num = 0.0;
			}
			else
			{
				num = 1.0;
			}
			return base.XsltInvokeEarlyBound(base.QName("EqualityOperator"), XsltMethods.EqualityOperator, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				base.Double(num),
				left,
				right
			});
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x0014B0BC File Offset: 0x001492BC
		public QilNode InvokeRelationalOperator(QilNodeType op, QilNode left, QilNode right)
		{
			left = base.TypeAssert(left, XmlQueryTypeFactory.ItemS);
			right = base.TypeAssert(right, XmlQueryTypeFactory.ItemS);
			double num;
			switch (op)
			{
			case QilNodeType.Gt:
				num = 4.0;
				goto IL_0065;
			case QilNodeType.Lt:
				num = 2.0;
				goto IL_0065;
			case QilNodeType.Le:
				num = 3.0;
				goto IL_0065;
			}
			num = 5.0;
			IL_0065:
			return base.XsltInvokeEarlyBound(base.QName("RelationalOperator"), XsltMethods.RelationalOperator, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				base.Double(num),
				left,
				right
			});
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		private void ExpectAny(QilNode n)
		{
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x0014B164 File Offset: 0x00149364
		public QilNode ConvertToType(XmlTypeCode requiredType, QilNode n)
		{
			if (requiredType == XmlTypeCode.Item)
			{
				return n;
			}
			if (requiredType != XmlTypeCode.Node)
			{
				switch (requiredType)
				{
				case XmlTypeCode.String:
					return this.ConvertToString(n);
				case XmlTypeCode.Boolean:
					return this.ConvertToBoolean(n);
				case XmlTypeCode.Double:
					return this.ConvertToNumber(n);
				}
				return null;
			}
			return this.EnsureNodeSet(n);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x0014B1BC File Offset: 0x001493BC
		public QilNode ConvertToString(QilNode n)
		{
			switch (n.XmlType.TypeCode)
			{
			case XmlTypeCode.String:
				return n;
			case XmlTypeCode.Boolean:
				if (n.NodeType == QilNodeType.True)
				{
					return base.String("true");
				}
				if (n.NodeType != QilNodeType.False)
				{
					return base.Conditional(n, base.String("true"), base.String("false"));
				}
				return base.String("false");
			case XmlTypeCode.Double:
				if (n.NodeType != QilNodeType.LiteralDouble)
				{
					return base.XsltConvert(n, XmlQueryTypeFactory.StringX);
				}
				return base.String(XPathConvert.DoubleToString((QilLiteral)n));
			}
			if (n.XmlType.IsNode)
			{
				return base.XPathNodeValue(this.SafeDocOrderDistinct(n));
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.StringX);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x0014B298 File Offset: 0x00149498
		public QilNode ConvertToBoolean(QilNode n)
		{
			switch (n.XmlType.TypeCode)
			{
			case XmlTypeCode.String:
				if (n.NodeType != QilNodeType.LiteralString)
				{
					return base.Ne(base.StrLength(n), base.Int32(0));
				}
				return base.Boolean(((QilLiteral)n).Length != 0);
			case XmlTypeCode.Boolean:
				return n;
			case XmlTypeCode.Double:
				if (n.NodeType != QilNodeType.LiteralDouble)
				{
					QilIterator qilIterator;
					return base.Loop(qilIterator = base.Let(n), base.Or(base.Lt(qilIterator, base.Double(0.0)), base.Lt(base.Double(0.0), qilIterator)));
				}
				return base.Boolean((QilLiteral)n < 0.0 || 0.0 < (QilLiteral)n);
			}
			if (n.XmlType.IsNode)
			{
				return base.Not(base.IsEmpty(n));
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.BooleanX);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x0014B3BC File Offset: 0x001495BC
		public QilNode ConvertToNumber(QilNode n)
		{
			switch (n.XmlType.TypeCode)
			{
			case XmlTypeCode.String:
				return base.XsltConvert(n, XmlQueryTypeFactory.DoubleX);
			case XmlTypeCode.Boolean:
				if (n.NodeType == QilNodeType.True)
				{
					return base.Double(1.0);
				}
				if (n.NodeType != QilNodeType.False)
				{
					return base.Conditional(n, base.Double(1.0), base.Double(0.0));
				}
				return base.Double(0.0);
			case XmlTypeCode.Double:
				return n;
			}
			if (n.XmlType.IsNode)
			{
				return base.XsltConvert(base.XPathNodeValue(this.SafeDocOrderDistinct(n)), XmlQueryTypeFactory.DoubleX);
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.DoubleX);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x0014B48F File Offset: 0x0014968F
		public QilNode ConvertToNode(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf && n.XmlType.IsSingleton)
			{
				return n;
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.NodeNotRtf);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x0014B4C6 File Offset: 0x001496C6
		public QilNode ConvertToNodeSet(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
			{
				return n;
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.NodeNotRtfS);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x0014B4F0 File Offset: 0x001496F0
		public QilNode TryEnsureNodeSet(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
			{
				return n;
			}
			if (this.CannotBeNodeSet(n))
			{
				return null;
			}
			return this.InvokeEnsureNodeSet(n);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x0014B520 File Offset: 0x00149720
		public QilNode EnsureNodeSet(QilNode n)
		{
			QilNode qilNode = this.TryEnsureNodeSet(n);
			if (qilNode == null)
			{
				throw new XPathCompileException("Expression must evaluate to a node-set.", Array.Empty<string>());
			}
			return qilNode;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x0014B53C File Offset: 0x0014973C
		public QilNode InvokeEnsureNodeSet(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ensure-node-set"), XsltMethods.EnsureNodeSet, XmlQueryTypeFactory.NodeSDod, new QilNode[] { n });
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x0014B564 File Offset: 0x00149764
		public QilNode Id(QilNode context, QilNode id)
		{
			if (id.XmlType.IsSingleton)
			{
				return base.Deref(context, this.ConvertToString(id));
			}
			QilIterator qilIterator;
			return base.Loop(qilIterator = base.For(id), base.Deref(context, this.ConvertToString(qilIterator)));
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x0014B5AA File Offset: 0x001497AA
		public QilNode InvokeStartsWith(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("starts-with"), XsltMethods.StartsWith, XmlQueryTypeFactory.BooleanX, new QilNode[] { str1, str2 });
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x0014B5D5 File Offset: 0x001497D5
		public QilNode InvokeContains(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("contains"), XsltMethods.Contains, XmlQueryTypeFactory.BooleanX, new QilNode[] { str1, str2 });
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0014B600 File Offset: 0x00149800
		public QilNode InvokeSubstringBefore(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring-before"), XsltMethods.SubstringBefore, XmlQueryTypeFactory.StringX, new QilNode[] { str1, str2 });
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x0014B62B File Offset: 0x0014982B
		public QilNode InvokeSubstringAfter(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring-after"), XsltMethods.SubstringAfter, XmlQueryTypeFactory.StringX, new QilNode[] { str1, str2 });
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0014B656 File Offset: 0x00149856
		public QilNode InvokeSubstring(QilNode str, QilNode start)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring"), XsltMethods.Substring2, XmlQueryTypeFactory.StringX, new QilNode[] { str, start });
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x0014B681 File Offset: 0x00149881
		public QilNode InvokeSubstring(QilNode str, QilNode start, QilNode length)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring"), XsltMethods.Substring3, XmlQueryTypeFactory.StringX, new QilNode[] { str, start, length });
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x0014B6B0 File Offset: 0x001498B0
		public QilNode InvokeNormalizeSpace(QilNode str)
		{
			return base.XsltInvokeEarlyBound(base.QName("normalize-space"), XsltMethods.NormalizeSpace, XmlQueryTypeFactory.StringX, new QilNode[] { str });
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x0014B6D7 File Offset: 0x001498D7
		public QilNode InvokeTranslate(QilNode str1, QilNode str2, QilNode str3)
		{
			return base.XsltInvokeEarlyBound(base.QName("translate"), XsltMethods.Translate, XmlQueryTypeFactory.StringX, new QilNode[] { str1, str2, str3 });
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0014B706 File Offset: 0x00149906
		public QilNode InvokeLang(QilNode lang, QilNode context)
		{
			return base.XsltInvokeEarlyBound(base.QName("lang"), XsltMethods.Lang, XmlQueryTypeFactory.BooleanX, new QilNode[] { lang, context });
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x0014B731 File Offset: 0x00149931
		public QilNode InvokeFloor(QilNode value)
		{
			return base.XsltInvokeEarlyBound(base.QName("floor"), XsltMethods.Floor, XmlQueryTypeFactory.DoubleX, new QilNode[] { value });
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x0014B758 File Offset: 0x00149958
		public QilNode InvokeCeiling(QilNode value)
		{
			return base.XsltInvokeEarlyBound(base.QName("ceiling"), XsltMethods.Ceiling, XmlQueryTypeFactory.DoubleX, new QilNode[] { value });
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x0014B77F File Offset: 0x0014997F
		public QilNode InvokeRound(QilNode value)
		{
			return base.XsltInvokeEarlyBound(base.QName("round"), XsltMethods.Round, XmlQueryTypeFactory.DoubleX, new QilNode[] { value });
		}
	}
}
