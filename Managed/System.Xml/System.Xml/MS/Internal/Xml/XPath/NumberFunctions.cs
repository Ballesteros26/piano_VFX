using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000030 RID: 48
	internal sealed class NumberFunctions : ValueQuery
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000054A4 File Offset: 0x000036A4
		public NumberFunctions(Function.FunctionType ftype, Query arg)
		{
			this.arg = arg;
			this.ftype = ftype;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000054BA File Offset: 0x000036BA
		private NumberFunctions(NumberFunctions other)
			: base(other)
		{
			this.arg = Query.Clone(other.arg);
			this.ftype = other.ftype;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000054E0 File Offset: 0x000036E0
		public override void SetXsltContext(XsltContext context)
		{
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000054F6 File Offset: 0x000036F6
		internal static double Number(bool arg)
		{
			if (!arg)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000550E File Offset: 0x0000370E
		internal static double Number(string arg)
		{
			return XmlConvert.ToXPathDouble(arg);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005518 File Offset: 0x00003718
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType functionType = this.ftype;
			if (functionType == Function.FunctionType.FuncNumber)
			{
				return this.Number(nodeIterator);
			}
			switch (functionType)
			{
			case Function.FunctionType.FuncSum:
				return this.Sum(nodeIterator);
			case Function.FunctionType.FuncFloor:
				return this.Floor(nodeIterator);
			case Function.FunctionType.FuncCeiling:
				return this.Ceiling(nodeIterator);
			case Function.FunctionType.FuncRound:
				return this.Round(nodeIterator);
			default:
				return null;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005590 File Offset: 0x00003790
		private double Number(XPathNodeIterator nodeIterator)
		{
			if (this.arg == null)
			{
				return XmlConvert.ToXPathDouble(nodeIterator.Current.Value);
			}
			object obj = this.arg.Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				return (double)obj;
			case XPathResultType.String:
				return NumberFunctions.Number((string)obj);
			case XPathResultType.Boolean:
				return NumberFunctions.Number((bool)obj);
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this.arg.Advance();
				if (xpathNavigator != null)
				{
					return NumberFunctions.Number(xpathNavigator.Value);
				}
				break;
			}
			case (XPathResultType)4:
				return NumberFunctions.Number(((XPathNavigator)obj).Value);
			}
			return double.NaN;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000563C File Offset: 0x0000383C
		private double Sum(XPathNodeIterator nodeIterator)
		{
			double num = 0.0;
			this.arg.Evaluate(nodeIterator);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.arg.Advance()) != null)
			{
				num += NumberFunctions.Number(xpathNavigator.Value);
			}
			return num;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005680 File Offset: 0x00003880
		private double Floor(XPathNodeIterator nodeIterator)
		{
			return Math.Floor((double)this.arg.Evaluate(nodeIterator));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005698 File Offset: 0x00003898
		private double Ceiling(XPathNodeIterator nodeIterator)
		{
			return Math.Ceiling((double)this.arg.Evaluate(nodeIterator));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000056B0 File Offset: 0x000038B0
		private double Round(XPathNodeIterator nodeIterator)
		{
			return XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this.arg.Evaluate(nodeIterator)));
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000226C File Offset: 0x0000046C
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000056C8 File Offset: 0x000038C8
		public override XPathNodeIterator Clone()
		{
			return new NumberFunctions(this);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000056D0 File Offset: 0x000038D0
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", this.ftype.ToString());
			if (this.arg != null)
			{
				this.arg.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x040000BD RID: 189
		private Query arg;

		// Token: 0x040000BE RID: 190
		private Function.FunctionType ftype;
	}
}
