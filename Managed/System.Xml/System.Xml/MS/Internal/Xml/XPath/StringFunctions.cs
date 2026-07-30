using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000044 RID: 68
	internal sealed class StringFunctions : ValueQuery
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00006F49 File Offset: 0x00005149
		public StringFunctions(Function.FunctionType funcType, IList<Query> argList)
		{
			this.funcType = funcType;
			this.argList = argList;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006F60 File Offset: 0x00005160
		private StringFunctions(StringFunctions other)
			: base(other)
		{
			this.funcType = other.funcType;
			Query[] array = new Query[other.argList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other.argList[i]);
			}
			this.argList = array;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006FBC File Offset: 0x000051BC
		public override void SetXsltContext(XsltContext context)
		{
			for (int i = 0; i < this.argList.Count; i++)
			{
				this.argList[i].SetXsltContext(context);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006FF4 File Offset: 0x000051F4
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			switch (this.funcType)
			{
			case Function.FunctionType.FuncString:
				return this.toString(nodeIterator);
			case Function.FunctionType.FuncConcat:
				return this.Concat(nodeIterator);
			case Function.FunctionType.FuncStartsWith:
				return this.StartsWith(nodeIterator);
			case Function.FunctionType.FuncContains:
				return this.Contains(nodeIterator);
			case Function.FunctionType.FuncSubstringBefore:
				return this.SubstringBefore(nodeIterator);
			case Function.FunctionType.FuncSubstringAfter:
				return this.SubstringAfter(nodeIterator);
			case Function.FunctionType.FuncSubstring:
				return this.Substring(nodeIterator);
			case Function.FunctionType.FuncStringLength:
				return this.StringLength(nodeIterator);
			case Function.FunctionType.FuncNormalize:
				return this.Normalize(nodeIterator);
			case Function.FunctionType.FuncTranslate:
				return this.Translate(nodeIterator);
			}
			return string.Empty;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000070B2 File Offset: 0x000052B2
		internal static string toString(double num)
		{
			return num.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000070C5 File Offset: 0x000052C5
		internal static string toString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000070D8 File Offset: 0x000052D8
		private string toString(XPathNodeIterator nodeIterator)
		{
			if (this.argList.Count <= 0)
			{
				return nodeIterator.Current.Value;
			}
			object obj = this.argList[0].Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.String:
				return (string)obj;
			case XPathResultType.Boolean:
				if (!(bool)obj)
				{
					return "false";
				}
				return "true";
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this.argList[0].Advance();
				if (xpathNavigator == null)
				{
					return string.Empty;
				}
				return xpathNavigator.Value;
			}
			case (XPathResultType)4:
				return ((XPathNavigator)obj).Value;
			default:
				return StringFunctions.toString((double)obj);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000718B File Offset: 0x0000538B
		public override XPathResultType StaticType
		{
			get
			{
				if (this.funcType == Function.FunctionType.FuncStringLength)
				{
					return XPathResultType.Number;
				}
				if (this.funcType == Function.FunctionType.FuncStartsWith || this.funcType == Function.FunctionType.FuncContains)
				{
					return XPathResultType.Boolean;
				}
				return XPathResultType.String;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000071B0 File Offset: 0x000053B0
		private string Concat(XPathNodeIterator nodeIterator)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < this.argList.Count)
			{
				stringBuilder.Append(this.argList[i++].Evaluate(nodeIterator).ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007200 File Offset: 0x00005400
		private bool StartsWith(XPathNodeIterator nodeIterator)
		{
			string text = this.argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this.argList[1].Evaluate(nodeIterator).ToString();
			return text.Length >= text2.Length && string.CompareOrdinal(text, 0, text2, 0, text2.Length) == 0;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007260 File Offset: 0x00005460
		private bool Contains(XPathNodeIterator nodeIterator)
		{
			string text = this.argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this.argList[1].Evaluate(nodeIterator).ToString();
			return StringFunctions.compareInfo.IndexOf(text, text2, CompareOptions.Ordinal) >= 0;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000072B4 File Offset: 0x000054B4
		private string SubstringBefore(XPathNodeIterator nodeIterator)
		{
			string text = this.argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this.argList[1].Evaluate(nodeIterator).ToString();
			if (text2.Length == 0)
			{
				return text2;
			}
			int num = StringFunctions.compareInfo.IndexOf(text, text2, CompareOptions.Ordinal);
			if (num >= 1)
			{
				return text.Substring(0, num);
			}
			return string.Empty;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007320 File Offset: 0x00005520
		private string SubstringAfter(XPathNodeIterator nodeIterator)
		{
			string text = this.argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this.argList[1].Evaluate(nodeIterator).ToString();
			if (text2.Length == 0)
			{
				return text;
			}
			int num = StringFunctions.compareInfo.IndexOf(text, text2, CompareOptions.Ordinal);
			if (num >= 0)
			{
				return text.Substring(num + text2.Length);
			}
			return string.Empty;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007394 File Offset: 0x00005594
		private string Substring(XPathNodeIterator nodeIterator)
		{
			string text = this.argList[0].Evaluate(nodeIterator).ToString();
			double num = XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this.argList[1].Evaluate(nodeIterator))) - 1.0;
			if (double.IsNaN(num) || (double)text.Length <= num)
			{
				return string.Empty;
			}
			if (this.argList.Count != 3)
			{
				if (num < 0.0)
				{
					num = 0.0;
				}
				return text.Substring((int)num);
			}
			double num2 = XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this.argList[2].Evaluate(nodeIterator)));
			if (double.IsNaN(num2))
			{
				return string.Empty;
			}
			if (num < 0.0 || num2 < 0.0)
			{
				num2 = num + num2;
				if (num2 <= 0.0)
				{
					return string.Empty;
				}
				num = 0.0;
			}
			double num3 = (double)text.Length - num;
			if (num2 > num3)
			{
				num2 = num3;
			}
			return text.Substring((int)num, (int)num2);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000074A2 File Offset: 0x000056A2
		private double StringLength(XPathNodeIterator nodeIterator)
		{
			if (this.argList.Count > 0)
			{
				return (double)this.argList[0].Evaluate(nodeIterator).ToString().Length;
			}
			return (double)nodeIterator.Current.Value.Length;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000074E4 File Offset: 0x000056E4
		private string Normalize(XPathNodeIterator nodeIterator)
		{
			string text;
			if (this.argList.Count > 0)
			{
				text = this.argList[0].Evaluate(nodeIterator).ToString();
			}
			else
			{
				text = nodeIterator.Current.Value;
			}
			text = XmlConvert.TrimString(text);
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			XmlCharType instance = XmlCharType.Instance;
			while (i < text.Length)
			{
				if (!instance.IsWhiteSpace(text[i]))
				{
					flag = true;
					stringBuilder.Append(text[i]);
				}
				else if (flag)
				{
					flag = false;
					stringBuilder.Append(' ');
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007584 File Offset: 0x00005784
		private string Translate(XPathNodeIterator nodeIterator)
		{
			string text = this.argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this.argList[1].Evaluate(nodeIterator).ToString();
			string text3 = this.argList[2].Evaluate(nodeIterator).ToString();
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < text.Length)
			{
				int num = text2.IndexOf(text[i]);
				if (num != -1)
				{
					if (num < text3.Length)
					{
						stringBuilder.Append(text3[num]);
					}
				}
				else
				{
					stringBuilder.Append(text[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007637 File Offset: 0x00005837
		public override XPathNodeIterator Clone()
		{
			return new StringFunctions(this);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007640 File Offset: 0x00005840
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", this.funcType.ToString());
			foreach (Query query in this.argList)
			{
				query.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000101 RID: 257
		private Function.FunctionType funcType;

		// Token: 0x04000102 RID: 258
		private IList<Query> argList;

		// Token: 0x04000103 RID: 259
		private static readonly CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
	}
}
