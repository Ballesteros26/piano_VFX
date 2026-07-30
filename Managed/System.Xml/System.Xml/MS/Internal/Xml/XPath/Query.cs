using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003A RID: 58
	[DebuggerDisplay("{ToString()}")]
	internal abstract class Query : ResetableIterator
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00005CC1 File Offset: 0x00003EC1
		public Query()
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005CC9 File Offset: 0x00003EC9
		protected Query(Query other)
			: base(other)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public override bool MoveNext()
		{
			return this.Advance() != null;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public override int Count
		{
			get
			{
				if (this.count == -1)
				{
					Query query = (Query)this.Clone();
					query.Reset();
					this.count = 0;
					while (query.MoveNext())
					{
						this.count++;
					}
				}
				return this.count;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00002F50 File Offset: 0x00001150
		public virtual void SetXsltContext(XsltContext context)
		{
		}

		// Token: 0x06000186 RID: 390
		public abstract object Evaluate(XPathNodeIterator nodeIterator);

		// Token: 0x06000187 RID: 391
		public abstract XPathNavigator Advance();

		// Token: 0x06000188 RID: 392 RVA: 0x00005D2D File Offset: 0x00003F2D
		public virtual XPathNavigator MatchNode(XPathNavigator current)
		{
			throw XPathException.Create("'{0}' is an invalid XSLT pattern.");
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005D39 File Offset: 0x00003F39
		public virtual double XsltDefaultPriority
		{
			get
			{
				return 0.5;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018A RID: 394
		public abstract XPathResultType StaticType { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005D44 File Offset: 0x00003F44
		public virtual QueryProps Properties
		{
			get
			{
				return QueryProps.Merge;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005D48 File Offset: 0x00003F48
		public static Query Clone(Query input)
		{
			if (input != null)
			{
				return (Query)input.Clone();
			}
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005D5A File Offset: 0x00003F5A
		protected static XPathNodeIterator Clone(XPathNodeIterator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005D67 File Offset: 0x00003F67
		protected static XPathNavigator Clone(XPathNavigator input)
		{
			if (input != null)
			{
				return input.Clone();
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005D74 File Offset: 0x00003F74
		public bool Insert(List<XPathNavigator> buffer, XPathNavigator nav)
		{
			int i = 0;
			int num = buffer.Count;
			if (num != 0)
			{
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[num - 1], nav);
				if (xmlNodeOrder == XmlNodeOrder.Before)
				{
					buffer.Add(nav.Clone());
					return true;
				}
				if (xmlNodeOrder == XmlNodeOrder.Same)
				{
					return false;
				}
				num--;
			}
			while (i < num)
			{
				int median = Query.GetMedian(i, num);
				XmlNodeOrder xmlNodeOrder = Query.CompareNodes(buffer[median], nav);
				if (xmlNodeOrder != XmlNodeOrder.Before)
				{
					if (xmlNodeOrder == XmlNodeOrder.Same)
					{
						return false;
					}
					num = median;
				}
				else
				{
					i = median + 1;
				}
			}
			buffer.Insert(i, nav.Clone());
			return true;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005DF3 File Offset: 0x00003FF3
		private static int GetMedian(int l, int r)
		{
			return (int)((uint)(l + r) >> 1);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005DFC File Offset: 0x00003FFC
		public static XmlNodeOrder CompareNodes(XPathNavigator l, XPathNavigator r)
		{
			XmlNodeOrder xmlNodeOrder = l.ComparePosition(r);
			if (xmlNodeOrder == XmlNodeOrder.Unknown)
			{
				XPathNavigator xpathNavigator = l.Clone();
				xpathNavigator.MoveToRoot();
				string baseURI = xpathNavigator.BaseURI;
				if (!xpathNavigator.MoveTo(r))
				{
					xpathNavigator = r.Clone();
				}
				xpathNavigator.MoveToRoot();
				string baseURI2 = xpathNavigator.BaseURI;
				int num = string.CompareOrdinal(baseURI, baseURI2);
				xmlNodeOrder = ((num < 0) ? XmlNodeOrder.Before : ((num > 0) ? XmlNodeOrder.After : XmlNodeOrder.Unknown));
			}
			return xmlNodeOrder;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E64 File Offset: 0x00004064
		[Conditional("DEBUG")]
		private void AssertDOD(List<XPathNavigator> buffer, XPathNavigator nav, int pos)
		{
			if (nav.GetType().ToString() == "Microsoft.VisualStudio.Modeling.StoreNavigator")
			{
				return;
			}
			if (nav.GetType().ToString() == "System.Xml.DataDocumentXPathNavigator")
			{
				return;
			}
			if (0 < pos)
			{
				Query.CompareNodes(buffer[pos - 1], nav);
			}
			if (pos < buffer.Count)
			{
				Query.CompareNodes(nav, buffer[pos]);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005ECC File Offset: 0x000040CC
		[Conditional("DEBUG")]
		public static void AssertQuery(Query query)
		{
			if (query is FunctionQuery)
			{
				return;
			}
			query = Query.Clone(query);
			XPathNavigator xpathNavigator = null;
			int count = query.Clone().Count;
			int num = 0;
			XPathNavigator xpathNavigator2;
			while ((xpathNavigator2 = query.Advance()) != null)
			{
				if (xpathNavigator2.GetType().ToString() == "Microsoft.VisualStudio.Modeling.StoreNavigator")
				{
					return;
				}
				if (xpathNavigator2.GetType().ToString() == "System.Xml.DataDocumentXPathNavigator")
				{
					return;
				}
				if (xpathNavigator != null && (xpathNavigator.NodeType != XPathNodeType.Namespace || xpathNavigator2.NodeType != XPathNodeType.Namespace))
				{
					Query.CompareNodes(xpathNavigator, xpathNavigator2);
				}
				xpathNavigator = xpathNavigator2.Clone();
				num++;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005F5E File Offset: 0x0000415E
		protected XPathResultType GetXPathType(object value)
		{
			if (value is XPathNodeIterator)
			{
				return XPathResultType.NodeSet;
			}
			if (value is string)
			{
				return XPathResultType.String;
			}
			if (value is double)
			{
				return XPathResultType.Number;
			}
			if (value is bool)
			{
				return XPathResultType.Boolean;
			}
			return (XPathResultType)4;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005F89 File Offset: 0x00004189
		public virtual void PrintQuery(XmlWriter w)
		{
			w.WriteElementString(base.GetType().Name, string.Empty);
		}

		// Token: 0x040000E2 RID: 226
		public const XPathResultType XPathResultType_Navigator = (XPathResultType)4;
	}
}
