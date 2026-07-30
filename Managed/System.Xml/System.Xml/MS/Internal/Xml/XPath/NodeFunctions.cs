using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002F RID: 47
	internal sealed class NodeFunctions : ValueQuery
	{
		// Token: 0x06000141 RID: 321 RVA: 0x000052C2 File Offset: 0x000034C2
		public NodeFunctions(Function.FunctionType funcType, Query arg)
		{
			this.funcType = funcType;
			this.arg = arg;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000052D8 File Offset: 0x000034D8
		public override void SetXsltContext(XsltContext context)
		{
			this.xsltContext = (context.Whitespace ? context : null);
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005300 File Offset: 0x00003500
		private XPathNavigator EvaluateArg(XPathNodeIterator context)
		{
			if (this.arg == null)
			{
				return context.Current;
			}
			this.arg.Evaluate(context);
			return this.arg.Advance();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000532C File Offset: 0x0000352C
		public override object Evaluate(XPathNodeIterator context)
		{
			switch (this.funcType)
			{
			case Function.FunctionType.FuncLast:
				return (double)context.Count;
			case Function.FunctionType.FuncPosition:
				return (double)context.CurrentPosition;
			case Function.FunctionType.FuncCount:
			{
				this.arg.Evaluate(context);
				int num = 0;
				if (this.xsltContext != null)
				{
					XPathNavigator xpathNavigator;
					while ((xpathNavigator = this.arg.Advance()) != null)
					{
						if (xpathNavigator.NodeType != XPathNodeType.Whitespace || this.xsltContext.PreserveWhitespace(xpathNavigator))
						{
							num++;
						}
					}
				}
				else
				{
					while (this.arg.Advance() != null)
					{
						num++;
					}
				}
				return (double)num;
			}
			case Function.FunctionType.FuncLocalName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.LocalName;
				}
				break;
			}
			case Function.FunctionType.FuncNameSpaceUri:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.NamespaceURI;
				}
				break;
			}
			case Function.FunctionType.FuncName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.Name;
				}
				break;
			}
			}
			return string.Empty;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000541D File Offset: 0x0000361D
		public override XPathResultType StaticType
		{
			get
			{
				return Function.ReturnTypes[(int)this.funcType];
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000542B File Offset: 0x0000362B
		public override XPathNodeIterator Clone()
		{
			return new NodeFunctions(this.funcType, Query.Clone(this.arg))
			{
				xsltContext = this.xsltContext
			};
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005450 File Offset: 0x00003650
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", this.funcType.ToString());
			if (this.arg != null)
			{
				this.arg.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x040000BA RID: 186
		private Query arg;

		// Token: 0x040000BB RID: 187
		private Function.FunctionType funcType;

		// Token: 0x040000BC RID: 188
		private XsltContext xsltContext;
	}
}
