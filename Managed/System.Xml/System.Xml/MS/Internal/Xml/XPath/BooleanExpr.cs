using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000D RID: 13
	internal sealed class BooleanExpr : ValueQuery
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000025D0 File Offset: 0x000007D0
		public BooleanExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Boolean)
			{
				opnd1 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Boolean)
			{
				opnd2 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd2);
			}
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
			this.isOr = op == Operator.Op.OR;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000261F File Offset: 0x0000081F
		private BooleanExpr(BooleanExpr other)
			: base(other)
		{
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
			this.isOr = other.isOr;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002656 File Offset: 0x00000856
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002670 File Offset: 0x00000870
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			object obj = this.opnd1.Evaluate(nodeIterator);
			if ((bool)obj == this.isOr)
			{
				return obj;
			}
			return this.opnd2.Evaluate(nodeIterator);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026A6 File Offset: 0x000008A6
		public override XPathNodeIterator Clone()
		{
			return new BooleanExpr(this);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026AE File Offset: 0x000008AE
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026B4 File Offset: 0x000008B4
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", (this.isOr ? Operator.Op.OR : Operator.Op.AND).ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000062 RID: 98
		private Query opnd1;

		// Token: 0x04000063 RID: 99
		private Query opnd2;

		// Token: 0x04000064 RID: 100
		private bool isOr;
	}
}
