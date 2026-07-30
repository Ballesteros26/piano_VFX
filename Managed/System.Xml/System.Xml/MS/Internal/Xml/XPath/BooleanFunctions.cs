using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000E RID: 14
	internal sealed class BooleanFunctions : ValueQuery
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002715 File Offset: 0x00000915
		public BooleanFunctions(Function.FunctionType funcType, Query arg)
		{
			this.arg = arg;
			this.funcType = funcType;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000272B File Offset: 0x0000092B
		private BooleanFunctions(BooleanFunctions other)
			: base(other)
		{
			this.arg = Query.Clone(other.arg);
			this.funcType = other.funcType;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002751 File Offset: 0x00000951
		public override void SetXsltContext(XsltContext context)
		{
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002768 File Offset: 0x00000968
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType functionType = this.funcType;
			switch (functionType)
			{
			case Function.FunctionType.FuncBoolean:
				return this.toBoolean(nodeIterator);
			case Function.FunctionType.FuncNumber:
				break;
			case Function.FunctionType.FuncTrue:
				return true;
			case Function.FunctionType.FuncFalse:
				return false;
			case Function.FunctionType.FuncNot:
				return this.Not(nodeIterator);
			default:
				if (functionType == Function.FunctionType.FuncLang)
				{
					return this.Lang(nodeIterator);
				}
				break;
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027DA File Offset: 0x000009DA
		internal static bool toBoolean(double number)
		{
			return number != 0.0 && !double.IsNaN(number);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027F3 File Offset: 0x000009F3
		internal static bool toBoolean(string str)
		{
			return str.Length > 0;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002800 File Offset: 0x00000A00
		internal bool toBoolean(XPathNodeIterator nodeIterator)
		{
			object obj = this.arg.Evaluate(nodeIterator);
			if (obj is XPathNodeIterator)
			{
				return this.arg.Advance() != null;
			}
			if (obj is string)
			{
				return BooleanFunctions.toBoolean((string)obj);
			}
			if (obj is double)
			{
				return BooleanFunctions.toBoolean((double)obj);
			}
			return !(obj is bool) || (bool)obj;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000026AE File Offset: 0x000008AE
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002869 File Offset: 0x00000A69
		private bool Not(XPathNodeIterator nodeIterator)
		{
			return !(bool)this.arg.Evaluate(nodeIterator);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002880 File Offset: 0x00000A80
		private bool Lang(XPathNodeIterator nodeIterator)
		{
			string text = this.arg.Evaluate(nodeIterator).ToString();
			string xmlLang = nodeIterator.Current.XmlLang;
			return xmlLang.StartsWith(text, StringComparison.OrdinalIgnoreCase) && (xmlLang.Length == text.Length || xmlLang[text.Length] == '-');
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028D7 File Offset: 0x00000AD7
		public override XPathNodeIterator Clone()
		{
			return new BooleanFunctions(this);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028E0 File Offset: 0x00000AE0
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

		// Token: 0x04000065 RID: 101
		private Query arg;

		// Token: 0x04000066 RID: 102
		private Function.FunctionType funcType;
	}
}
