using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002D RID: 45
	internal sealed class MergeFilterQuery : CacheOutputQuery
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000509E File Offset: 0x0000329E
		public MergeFilterQuery(Query input, Query child)
			: base(input)
		{
			this.child = child;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000050AE File Offset: 0x000032AE
		private MergeFilterQuery(MergeFilterQuery other)
			: base(other)
		{
			this.child = Query.Clone(other.child);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000050C8 File Offset: 0x000032C8
		public override void SetXsltContext(XsltContext xsltContext)
		{
			base.SetXsltContext(xsltContext);
			this.child.SetXsltContext(xsltContext);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000050E0 File Offset: 0x000032E0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.Evaluate(nodeIterator);
			while (this.input.Advance() != null)
			{
				this.child.Evaluate(this.input);
				XPathNavigator xpathNavigator;
				while ((xpathNavigator = this.child.Advance()) != null)
				{
					base.Insert(this.outputBuffer, xpathNavigator);
				}
			}
			return this;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005138 File Offset: 0x00003338
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			XPathNavigator xpathNavigator = this.child.MatchNode(current);
			if (xpathNavigator == null)
			{
				return null;
			}
			xpathNavigator = this.input.MatchNode(xpathNavigator);
			if (xpathNavigator == null)
			{
				return null;
			}
			this.Evaluate(new XPathSingletonIterator(xpathNavigator.Clone(), true));
			for (XPathNavigator xpathNavigator2 = this.Advance(); xpathNavigator2 != null; xpathNavigator2 = this.Advance())
			{
				if (xpathNavigator2.IsSamePosition(current))
				{
					return xpathNavigator;
				}
			}
			return null;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000519B File Offset: 0x0000339B
		public override XPathNodeIterator Clone()
		{
			return new MergeFilterQuery(this);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000051A3 File Offset: 0x000033A3
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.input.PrintQuery(w);
			this.child.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x040000B8 RID: 184
		private Query child;
	}
}
