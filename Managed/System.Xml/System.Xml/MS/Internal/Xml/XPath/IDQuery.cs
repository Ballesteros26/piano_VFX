using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000028 RID: 40
	internal sealed class IDQuery : CacheOutputQuery
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x000035FF File Offset: 0x000017FF
		public IDQuery(Query arg)
			: base(arg)
		{
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003608 File Offset: 0x00001808
		private IDQuery(IDQuery other)
			: base(other)
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004518 File Offset: 0x00002718
		public override object Evaluate(XPathNodeIterator context)
		{
			object obj = base.Evaluate(context);
			XPathNavigator xpathNavigator = context.Current.Clone();
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				this.ProcessIds(xpathNavigator, StringFunctions.toString((double)obj));
				break;
			case XPathResultType.String:
				this.ProcessIds(xpathNavigator, (string)obj);
				break;
			case XPathResultType.Boolean:
				this.ProcessIds(xpathNavigator, StringFunctions.toString((bool)obj));
				break;
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator2;
				while ((xpathNavigator2 = this.input.Advance()) != null)
				{
					this.ProcessIds(xpathNavigator, xpathNavigator2.Value);
				}
				break;
			}
			case (XPathResultType)4:
				this.ProcessIds(xpathNavigator, ((XPathNavigator)obj).Value);
				break;
			}
			return this;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000045C8 File Offset: 0x000027C8
		private void ProcessIds(XPathNavigator contextNode, string val)
		{
			string[] array = XmlConvert.SplitString(val);
			for (int i = 0; i < array.Length; i++)
			{
				if (contextNode.MoveToId(array[i]))
				{
					base.Insert(this.outputBuffer, contextNode);
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004604 File Offset: 0x00002804
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			this.Evaluate(new XPathSingletonIterator(context, true));
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.Advance()) != null)
			{
				if (xpathNavigator.IsSamePosition(context))
				{
					return context;
				}
			}
			return null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004637 File Offset: 0x00002837
		public override XPathNodeIterator Clone()
		{
			return new IDQuery(this);
		}
	}
}
