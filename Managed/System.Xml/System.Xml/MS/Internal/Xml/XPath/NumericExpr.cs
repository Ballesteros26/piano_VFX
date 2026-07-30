using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000031 RID: 49
	internal sealed class NumericExpr : ValueQuery
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00005724 File Offset: 0x00003924
		public NumericExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Number)
			{
				opnd1 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Number)
			{
				opnd2 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd2);
			}
			this.op = op;
			this.opnd1 = opnd1;
			this.opnd2 = opnd2;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005770 File Offset: 0x00003970
		private NumericExpr(NumericExpr other)
			: base(other)
		{
			this.op = other.op;
			this.opnd1 = Query.Clone(other.opnd1);
			this.opnd2 = Query.Clone(other.opnd2);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000057A7 File Offset: 0x000039A7
		public override void SetXsltContext(XsltContext context)
		{
			this.opnd1.SetXsltContext(context);
			this.opnd2.SetXsltContext(context);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000057C1 File Offset: 0x000039C1
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return NumericExpr.GetValue(this.op, XmlConvert.ToXPathDouble(this.opnd1.Evaluate(nodeIterator)), XmlConvert.ToXPathDouble(this.opnd2.Evaluate(nodeIterator)));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000057F5 File Offset: 0x000039F5
		private static double GetValue(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.PLUS:
				return n1 + n2;
			case Operator.Op.MINUS:
				return n1 - n2;
			case Operator.Op.MUL:
				return n1 * n2;
			case Operator.Op.DIV:
				return n1 / n2;
			case Operator.Op.MOD:
				return n1 % n2;
			default:
				return 0.0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000226C File Offset: 0x0000046C
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005833 File Offset: 0x00003A33
		public override XPathNodeIterator Clone()
		{
			return new NumericExpr(this);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000583C File Offset: 0x00003A3C
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("op", this.op.ToString());
			this.opnd1.PrintQuery(w);
			this.opnd2.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x040000BF RID: 191
		private Operator.Op op;

		// Token: 0x040000C0 RID: 192
		private Query opnd1;

		// Token: 0x040000C1 RID: 193
		private Query opnd2;
	}
}
